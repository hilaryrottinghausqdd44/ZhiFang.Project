/**
 * 重提仪器结果
 * @author GHX
 * @version 2021-04-22
 */
layui.extend({
}).define(['uxutil', 'table', 'form', 'laydate','uxbase'], function (exports) {
	"use strict";
	var $ = layui.jquery,
		table = layui.table,
		form = layui.form,
		laydate = layui.laydate,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
	var app = {};
	app.cols = {
		left: [
			[ {
				field: 'LisEquipForm_EquipID',
				minWidth: 60,
				title: '仪器Id',
				sort: false,
				hide: true
			}, {
					field: 'LisEquipForm_ETestDate',
					title: '仪器检验日期',
					width: 80,
					hide: false,
					sort: false,
					templet: function (data) { var value = data["LisEquipForm_ETestDate"];return uxutil.date.toString(value, true) || ''; }
				}, {
					field: 'LisEquipForm_ESampleNo',
					title: '样本编号',
					minWidth: 60,
					hide: false,
					sort: false
				}, {
					field: 'LisEquipForm_EBarCode',
					title: '条码号',
					width: 80,
					hide: false,
					sort: false
				}, {
					field: 'LisEquipForm_Id',
					title: '仪器样本单Id',
					width: 80,
					hide: true,
					sort: false
				}
			]
		],
		right: [
			[{
					field: 'LisEquipItem_Id',
					width: 60,
					title: 'Id',
					sort: false,
					hide: true
				}, {
					field: 'LisEquipItem_LBItem_CName',
					title: '项目名称',
					minWidth: 130,
					hide: false,
					sort: false
				}, {
					field: 'LisEquipItem_EReportValue',
					title: '报告结果',
					width: 80,
					hide: false,
					sort: false
				}, {
					field: 'LisEquipItem_EOriginalValue',
					title: '原始结果',
					width: 80,
					hide: false,
					sort: false
				}, {
					field: 'LisEquipItem_EOriginalResultStatus',
					title: '原始结果状态',
					width: 80,
					hide: false,
					sort: false
				}, {
					field: 'LisEquipItem_EResultStatus',
					title: '结果状态',
					width: 80,
					hide: false,
					sort: false
				}, {
					field: 'LisEquipItem_EResultAlarm',
					title: '结果报警',
					width: 80,
					hide: false,
					sort: false
				}
			]
		]
	};
	app.params={
		LisTestFormId:null, //仪器样本单ID
		recode:null,
		isReadOnly:true,
		sectionId:null,
		checkRowData:[],
		rcheckRowData:[],
		userID: null,//登录人ID
		localTotalName: 'LabStar_TS',// localStorage中存储小组的名称
		localSectionName: 'OpenedSectionList'// localStorage中存储小组的名称
	};
	app.indexload = null;
	app.url={
		selectUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipFormByHQL?isPlanish=true',
		equipitemurl:uxutil.path.ROOT+
			'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true',
		extractUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisItemResultByEquipResult',
		equipUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true'
	};
	app.sort={
		left:'[{"property":"LisEquipForm_ESampleNoForOrder","direction":"ASC"}]',
		right:'[{"property":"LisEquipItem_IExamine","direction":"ASC"}]'
	}
	app.isload = false;
	//初始化
	app.init = function (recode,isReadOnly,sectionId) {
		var me = this;
		if (recode) {
			me.params.recode = recode;
			me.params.LisTestFormId = recode.LisTestForm_Id;
		}
		me.params.isReadOnly = isReadOnly;
		me.params.sectionId = sectionId;
		me.params.userID = uxutil.cookie.get(uxutil.cookie.map.USERID);
		if (!app.isload) {
			app.indexload = layer.load();
			me.EquipInit();
			me.DateInit();
			me.initTable();
			me.listeners(); 
			app.isload = true;
		}
		
	};
	//初始化时间
	app.DateInit = function () {
		var me = this,
			today = new Date();
		//赋值日期框
		$("#equipTestDate").val(uxutil.date.toString(today, true));
		//监听日期图标
		$("#extrctEquipResultForm input.myDate+i.layui-icon").on("click", function () {
			var elemID = $(this).prev().attr("id");
			if ($("#" + elemID).hasClass("layui-disabled")) return false;
			var key = $("#" + elemID).attr("lay-key");
			if ($('#layui-laydate' + key).length > 0) {
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
			}
			var datatype = $("#" + elemID).attr("data-type");
			if (datatype == "text") {
				me.initDate(elemID);
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
				var key = $("#" + elemID).attr("lay-key");
				$('#layui-laydate' + key).remove();
			}
		});
		//监听日期input -- 不弹出日期框
		$("#extrctEquipResultForm").on('focus', '.myDate', function () {
			var device = layui.device();
			if (device.ie) {
				window.event.returnValue = false;
			} else {
				window.event.preventDefault();
			}
			layui.stope(window.event);
			return false;
		});
	};
	//初始化yyyy-mm-dd
	app.initDate = function (id) {
		var me = this;
		//检测日期 yyyy-MM-dd
		laydate.render({//没有默认值
			elem: '#' + id,//'#LisTestForm_GTestDate',
			eventElem: '#' + id + '+i.layui-icon',
			type: 'date',
			show: true
		});
	};
	
	//初始化左侧列表
	app.initTable = function() {
		var me = this,
			euqip = $("#cSectionEquip").val(),
			date  = $("#equipTestDate").val(),
			sampleno  = $("#equipSampleno").val(),
			url = "",
			where = "(1=1";
		
		if(euqip){
			where +=" and LBEquip.Id="+ euqip;
			if(date){
				where +=" and ETestDate>='"+ date+" 00:00:00' and ETestDate<='"+date+" 23:59:59'";
			}
			if(sampleno){
				where += " and ESampleNo="+sampleno;
			}
			where += ")";
			url = app.url.selectUrl+"&fields="+me.GetTableFields(me.cols.left[0],true)+"&where="+where;
			if(app.sort.left){
				url += "&sort="+app.sort.left;
			}
			url += "&_t="+new Date().getTime();
		}
		table.render({
			elem: '#EquipSample',
			height: 'full-170',
			defaultToolbar: ['filter'],
			size: 'sm',
			page: false,
			data: [],
			url: url,
			cols: me.cols.left,
			limit: 99999,
			autoSort: true, //禁用前端自动排序
			text: {
				none: '暂无相关数据'
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
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function(res, curr, count) {				
				if(count>0){
					if ($("#EquipSample+div .layui-table-body table.layui-table tbody tr:first-child")[0])
					    $("#EquipSample+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
				}else{
					app.initRTable(-1);
				}
			}
		});
	};
	//初始化左侧列表
	app.initRTable = function (EquipFormID) {
		var me = this;
		var url = me.url.equipitemurl+"&fields="+me.GetTableFields(me.cols.right[0],true)+"&where=(EquipFormID="+EquipFormID+")";
		url+="&sort="+app.sort.right+"&start=0&_t="+new Date().getTime();
		table.render({
			elem: '#EquipItemResult',
			height: 'full-170',
			defaultToolbar: ['filter'],
			size: 'sm',
			page: false,
			//data: data,
			url: url,
			cols: me.cols.right,
			limit: 99999,
			autoSort: true, //禁用前端自动排序
			text: {
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				if(app.indexload){
					layer.close(app.indexload);
				}
				if(count>0){
					if ($("#EquipItemResult+div .layui-table-body table.layui-table tbody tr:first-child")[0])
					    $("#EquipItemResult+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
				}		
			}
		});
	};
	app.EquipInit = function(){
		var me = this,
			sectionwhere = "";
		if(app.params.sectionId){
			sectionwhere = app.params.sectionId;
		}else{
			var sectionids = app.getLocalSection();
			if(sectionids){
				sectionwhere = sectionids;
			}
		}
		var url = "";
		if(sectionwhere){
			url = me.url.equipUrl+"&where=LBSection in ("+sectionwhere + ')&fields=LBEquip_CName,LBEquip_Id';
		}else{
			url = me.url.equipUrl+"&where=1=2&fields=LBEquip_CName,LBEquip_Id";
		}
		uxutil.server.ajax({
			url: url,
			async: false
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
				var tempAjax = "";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].LBEquip_Id + "'>" + value.list[i].LBEquip_CName + "</option>";
					$("#cSectionEquip").empty();
					$("#cSectionEquip").append(tempAjax);
	
				}
				form.render('select'); //需要渲染一下;
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	};
	//获得local中的小组
	app.getLocalSection = function () {
	    var me = this,
	        sectionIDs = [];
	    var local = uxutil.localStorage.get(me.params.localTotalName, true);
	    if (local) {
	        if (local[me.params.userID]) {//存在当前等录人记录
	            if (local[me.params.userID][me.params.localSectionName] && local[me.params.userID][me.params.localSectionName].length > 0) {//local中存储打开的小组
	                $.each(local[me.params.userID][me.params.localSectionName], function (i, item) {
	                    sectionIDs.push(item["Id"]);
	                });
	            }
	        }
	    }
	    return sectionIDs.join();
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
		//监听行单击事件
		table.on('row(EquipSample)', function (obj) {
		    //标注选中样式
		    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			app.initRTable(obj.data.LisEquipForm_Id);
			me.params.checkRowData = [];
			me.params.checkRowData.push(obj.data);
		});
		//监听行单击事件
		table.on('row(EquipItemResult)', function (obj) {
		    //标注选中样式
		    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			me.params.rcheckRowData = [];
			me.params.rcheckRowData.push(obj.data);
		});
		$("#cequipSearch").click(function(){
			var euqip = $("#cSectionEquip").val(),
				date  = $("#equipTestDate").val(),
				sampleno  = $("#equipSampleno").val(),
				url = "",
				where = "(1=1";
			if(euqip){
				where +=" and LBEquip.Id="+ euqip;
				if(date){
					where +=" and ETestDate>='"+ date+" 00:00:00' and ETestDate<='"+date+" 23:59:59'";
				}
				if(sampleno){
					where += " and ESampleNo="+sampleno;
				}
				where += ")";
				url = app.url.selectUrl+"&fields="+me.GetTableFields(me.cols.left[0],true)+"&where="+where;
				if(app.sort.left){
					url += "&sort="+app.sort.left;
				}
				url += "&_t="+new Date().getTime();
			}
			app.indexload = layer.load();
			table.reload('EquipSample',{
				url:url
			})
		});
		$("#cequipSearchExecute").click(function(){
			if(!me.params.isReadOnly){
				if(!me.params.recode || !me.params.LisTestFormId){
					uxbase.MSG.onWarn("未选择检验单!");
					return;
				}
				if(me.params.checkRowData.length <= 0){
					uxbase.MSG.onWarn("未选择仪器样本单!");
					return;
				}
				if(me.params.rcheckRowData.length <= 0){
					uxbase.MSG.onWarn("未选择仪器项目!");
					return;
				}
				var testFormId = me.params.recode.LisTestForm_Id,
					msg = [],
					LisTestForm_GSampleNo = me.params.recode.LisTestForm_GSampleNo,
					LisTestForm_GTestDate = me.params.recode.LisTestForm_GTestDate,
					LisTestForm_GTestDate = me.testFormRecord.get("LisTestForm_GTestDate"),//检验单时间
					equipFormID = null,//仪器检验单Id long类型
					equipItemIDList = [],//仪器检验项目单id string类型
					LeftGridCheckRow = me.params.checkRowData,//仪器样本单选中数据
					RightGridCheckRow = me.params.rcheckRowData,//仪器样本项目选中数据
					isChangeSampleNo = $("#equipExtrctAddItemDel").prop("checked"),//是否改变检验样本单样本号
					changeTestFormID = true,//是否改变仪器样本单对应的检验样本单
					isDelAuotAddItem = $("#equipExtrctSampleNoChange").prop("checked");//是否删除检验单中仪器自增项目
				
				equipFormID = LeftGridCheckRow[0].LisEquipForm_Id;
				for(var i=0;i<RightGridCheckRow.length;i++){
					equipItemIDList.push(RightGridCheckRow[i].LisEquipItem_Id);
				}
				if (!equipFormID || equipItemIDList.length <= 0) return;
				//不是同一天，同一个样本号，提示
				if (LisTestForm_GSampleNo != LeftGridCheckRow[0].LisEquipForm_ESampleNo || LisTestForm_GTestDate != LeftGridCheckRow[0].LisEquipForm_ETestDate) {
					msg.push("对应样本 日期：" + LisTestForm_GTestDate + ",样本号：" + LisTestForm_GSampleNo);
					msg.push("对应仪器样本 日期：" + LeftGridCheckRow[0].LisEquipForm_ETestDate + ",样本号：" + LeftGridCheckRow[0].LisEquipForm_ESampleNo);
					msg.push("日期不一致，或者样本号不一致，确定要重新提取吗？");
				}
				if (msg.length == 0) msg.push("确定要重新提取吗？");
				layer.confirm(msg.join("<br/>"), {icon: 3, title:'提示'}, function(index){
					var configs = {
						type: "POST",
						url: me.url.extractUrl,
						data: JSON.stringify({
							testFormID: testFormId,
							equipFormID: equipFormID,
							equipItemID: '',
							isChangeSampleNo : isChangeSampleNo,
							changeTestFormID:changeTestFormID,
							isDelAuotAddItem:isDelAuotAddItem
						})
					};
					var loadIndex = layer.load();
					uxutil.server.ajax(configs, function(res) {
						//隐藏遮罩层
						layer.close(loadIndex);
						if (res.success) {
							layui.event('common', "refreshTestFormListRecord", {id:testFormId});
						} else {
							uxbase.MSG.onError("执行失败，失败信息：" +res.ErrorInfo);
						}
					});
					layer.close(index);
				});
			}
		});
	};
	//暴露接口
	exports('extrctEquipResult', app);
});
