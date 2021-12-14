/**
 * 仪器结果
 * @author GHX
 * @version 2021-04-22
 */
layui.extend({
}).define(['uxutil', 'table', 'form','uxbase'], function (exports) {
	"use strict";

	var $ = layui.jquery,
		table = layui.table,
		form = layui.form,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
	var app = {};
	app.url={
		selectUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true',
		editUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarService.svc/LS_UDTO_AddItemReCheckResultByEquipResult',
		equipItemUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true'
	};
	app.sort={
		talbe:'[{"property":"LisEquipItem_LBItem_DispOrder","direction":"ASC"},{"property":"LisEquipItem_IExamine","direction":"ASC"}]'
	}
	app.cols = {
		left: [
			[ {type:'checkbox'},{
				field: 'LisEquipItem_BItemResultFlag',
				minWidth: 60,
				title: '采用',
				sort: true,
				hide: false,
				templet: function (data) {
					if (data["LisEquipItem_BItemResultFlag"] == "true") {
						return '<span style="color:green">是</span>';
					} else {
						return '<span style="color:red">否</span>';
					}
				}
			}, {
					field: 'LisEquipItem_LBItem_CName',
					title: '对应项目',
					minWidth: 130,
					hide: false,
					sort: true
				}, {
					field: 'LisEquipItem_EReportValue',
					title: '检验结果',
					minWidth: 130,
					hide: false,
					sort: true
				}, {
					field: 'LisEquipItem_IExamine',
					title: '检测序号',
					minWidth: 130,
					hide: false,
					sort: true
				}, {
					field: 'LisEquipItem_Id',
					title: 'ID',
					minWidth: 130,
					hide: true,
					sort: false
				}, {
					field: 'LisEquipItem_LBItem_DispOrder',
					title: '',
					minWidth: 130,
					hide: true,
					sort: false
				}, {
					field: 'LisEquipItem_LBItem_Id',
					title: '',
					minWidth: 130,
					hide: true,
					sort: false
				}
			]
		]
	};
	app.params={
		EquipFormID:null, //仪器样本单ID
		recode:null,
		isReadOnly:true
	};
	app.isload = false;
	//初始化
	app.init = function (testFormRecord,isReadOnly) {    
		var me = this;
		//初始化列表
		me.initTable("");
		if(testFormRecord){
			var EquipFormID = testFormRecord.LisTestForm_EquipFormID;
			me.params.recode = testFormRecord;
			me.params.isReadOnly =isReadOnly;
			if(!EquipFormID){//仪器样本单ID不存在
				//layer.msg("检验单汇总不存在仪器样本单ID！");
				me.params.EquipFormID = null;
				if (app.isload) {
					table.reload('EquipResult', {
						url:'',
						data: []
					})
				}
				return;
			}
			else{
				//与原先的不一致需要重新加载数据
				if (EquipFormID != me.params.EquipFormID) {
					app.isload = true;
					me.params.EquipFormID = EquipFormID; 					
					var url = app.url.selectUrl + "&where=EquipFormID=" + me.params.EquipFormID + "&fields=" + me.GetTableFields(me.cols.left[0], true) + "&sort=" + me.sort.talbe+"&_t="+new Date().getTime();
					table.reload('EquipResult', {
						url: url,
						data: []
					})
					me.listeners();
				 }
			} 
		}else{
			table.reload('EquipResult', {
				url: '',
				data: []
			})
		}
	};
	//初始化左侧列表
	app.initTable = function(url) {
		var me = this,
			url = url ? url : "";
		table.render({
			elem: '#EquipResult',
			height: 'full-110',
			defaultToolbar: ['filter'],
			size: 'sm',
			page: false,
			data: [],
			url: url,
			cols: me.cols.left,
			limit: 99999,
			autoSort: true, //禁用前端自动排序
			text: {
				none: '没有对应的仪器结果'
			},
			response: function() {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function(res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				me.EquipItemInit(data.list);
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function(res, curr, count) {
			}
		});
	};
	//获取查询Fields
	app.GetTableFields = function(col, isString) {
		var me = this,
			columns = col || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	app.listeners = function(){
		var me = this;
		//采用结果
		$("#useRecheckResult").off().on('click',function() {
			//只读、没有检验单、不是检验中的单子不可以进行操作
			if(me.params.isReadOnly || !me.params.recode || me.params.recode.LisTestForm_MainStatusID != "0"){
				return;
			}
			//获得选中的数据
			var checkData = table.checkStatus('EquipResult').data;
			var testformrecode = me.params.recode;
			if(checkData.length <= 0){
				uxbase.MSG.onWarn("请选择数据!");
				return;
			}
			var equipitemids = [];
			for(var i=0;i<checkData.length;i++){
				equipitemids.push(checkData[i].LisEquipItem_Id);
			}
			var configs = {
				type: "POST",
				url: me.url.editUrl,
				data: JSON.stringify({
					testFormID:testformrecode.LisTestForm_Id,
					equipFormID:testformrecode.LisTestForm_EquipFormID,
					equipItemID:equipitemids.join(","),
					reCheckMemoInfo:""
				})
			};
			var loadIndex = layer.load();
			uxutil.server.ajax(configs, function(res) {
				//隐藏遮罩层
				layer.close(loadIndex);
				if (res.success) {
					uxbase.MSG.onSuccess("采用结果完成!");
				} else {
					uxbase.MSG.onError(res.msg);
				}
			});
		});
		//采用标记并复检登记
		$("#cancelRecheckResult").off().on('click',function() {
			//只读、没有检验单、不是检验中的单子不可以进行操作
			if(me.params.isReadOnly || !me.params.recode || me.params.recode.LisTestForm_MainStatusID != "0"){
				return;
			}
			//获得选中的数据
			var checkData = table.checkStatus('EquipResult').data;
			var testformrecode = me.params.recode;
			if(checkData.length <= 0){
				uxbase.MSG.onWarn("请选择数据!");
				return;
			}
			layer.prompt({title: '复检登记', formType: 2}, function(pass, index){
				var equipitemids = [];
				for(var i=0;i<checkData.length;i++){
					equipitemids.push(checkData[i].LisEquipItem_Id);
				}
				var configs = {
					type: "POST",
					url: me.url.editUrl,
					data: JSON.stringify({
						testFormID:testformrecode.LisTestForm_Id,
						equipFormID:testformrecode.LisTestForm_EquipFormID,
						equipItemID:equipitemids.join(","),
						reCheckMemoInfo:pass
					})
				};
				var loadIndex = layer.load();
				uxutil.server.ajax(configs, function(res) {
					//隐藏遮罩层
					layer.close(loadIndex);
					if (res.success) {
						layer.close(index);
						uxbase.MSG.onSuccess("采用结果完成!");
					} else {
						uxbase.MSG.onError(res.msg);
					}
				});
				
			});
			
		});
		//清除过滤项
		$("#equipDelWhere").off().on('click',function(){
			$("#rquipEquipItem").val("");
			$("#resultNo").val("");
		});
		//查询监听
		$("#equipSeach").off().on('click',function(){
			var eitem= $("#rquipEquipItem").val();
			var rno = $("#resultNo").val();
			var  where =  "EquipFormID="+app.params.EquipFormID;
			if(rno){
				where += " and IExamine="+rno;
			}
			if(eitem){
				where += " and ItemID=" + itemid;
			}
			var url = app.url.selectUrl+"&where="+where+"&fields="+me.GetTableFields(me.cols.left[0],true)+"&sort="+me.sort.talbe+"&_t="+new Date().getTime();
			var load = layer.load();
			table.reload("EquipResult",{
				url:url,
				done:function(){
					layer.close(load);
				}
			})
		});
		//监听右侧列表排序事件
		table.on('sort(EquipResult)', function(obj) {
			var field = obj.field, //排序字段
				type = obj.type, //升序还是降序
				eitem = $("#rquipEquipItem").val(),
				rno = $("#resultNo").val(),
				url = me.url.selectUrl + "&fields=" + me.GetTableFields(me.cols.left[0], true);
		
			if (type == null) return;
			var  where =  "EquipFormID="+app.params.EquipFormID;
			if(rno){
				where += " and IExamine="+rno;
			}
			if(eitem){
				where += " and ItemID=" + itemid;
			}
			url += "&where=" + where;
			if (url.indexOf("sort") != -1) { //存在
				var start = url.indexOf("sort=[");
				var end = url.indexOf("]") + 1;
				var oldStr = url.slice(start, end);
				var newStr = 'sort=[{property:"' + field + '",direction:"' + type + '"}]';
				url = url.replace(oldStr, newStr);
			} else {
				url = url + '&sort=[{property:"' + field + '",direction:"' + type + '"}]';
			}
			url += "&t=" + new Date().getTime();
			var load = layer.load();
			table.reload("EquipResult",{
				url:encodeURI(url),
				done:function(){
					layer.close(load);
				}
			});
		});
	};
	
	
	app.EquipItemInit = function(data){
		var me = this;
		if (data.length <= 0) return;
		var tempAjax = "<option value=''>请选择</option>";
		var obj = {};
		for (var i = 0; i < data.length; i++) {
			if (!obj[data[i].LisEquipItem_LBItem_Id]) {
				tempAjax += "<option value='" + data[i].LisEquipItem_LBItem_Id + "'>" + data[i].LisEquipItem_LBItem_CName + "</option>";
				$("#rquipEquipItem").empty();
				$("#rquipEquipItem").append(tempAjax);
				obj[data[i].LisEquipItem_LBItem_Id] = true;
			}
		}
		form.render('select'); //需要渲染一下;
		/*var url =me.url.equipItemUrl + '&fields=LisEquipItem_LBItem_CName,LisEquipItem_LBItem_Id';
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = "<option value=''>请选择</option>";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].LisEquipItem_LBItem_Id + "'>" + value.list[i].LisEquipItem_LBItem_CName + "</option>";
					$("#rquipEquipItem").empty();
					$("#rquipEquipItem").append(tempAjax);
	
				}
				form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});*/
	};
	//暴露接口
	exports('equipResult', app);
});
