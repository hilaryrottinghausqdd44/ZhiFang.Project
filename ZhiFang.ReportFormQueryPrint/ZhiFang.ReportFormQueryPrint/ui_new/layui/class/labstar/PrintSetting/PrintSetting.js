/**
 * 小组打印模板设置页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'element','table'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		table = layui.table,
		$ = layui.jquery;
	var app = {};
	//服务地址
	app.url = {
		//小组模板
		selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/LabStarGetSectionPrintList',
		//小组
		selectPGroupUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/LabStarGetPGroup?fields=SectionNo,CName',
		//删除模板
		deleteSectionPrintUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/DeleteSectionPrint',
		//就诊类型
		sickTypeselectUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/LabStarGetSickType?fields=SickTypeNo,CName',
		//添加小组打印
		addSectionPrintUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/AddSectionPrint',
		//添加小组打印
		updateSectionPrintUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/UpdateSectionPrint'
	};
	app.dataList=[];
	app.cols = {
		left: [
			[
				{ type: 'checkbox'},
				{field: 'SectionNo',
					title: '小组号',
					width: 100,
					//align: 'center',
					hide: true,
					sort: false
				},
				{field: 'SPID',
					title: 'id',
					width: 100,
					//align: 'center',
					hide: true,
					sort: false
				},
				{field: 'CName',
					title: '小组名称',
					width: 140,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'PrintFormat',
					title: '打印格式',
					width: 250,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'PrintProgram',
					title: '模板名称',
					width: 280,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'DefPrinter',
					title: '默认打印机',
					width: 130,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'TestItemNo',
					title: '特殊项目号',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				},
				
				{field: 'ItemCountMin',
					title: '项目最小数量',
					width: 110,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'ItemCountMax',
					title: '项目最大数量',
					width: 110,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'sicktypeno',
					title: '就诊类型',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'PrintOrder',
					title: '打印排序',
					width: 80,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'microattribute',
					title: '微生物属性',
					width: 80,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'IsRFGraphdataPDf',
					title: '是否外送单',
					width: 80,
					//align: 'center',
					//hide: true,
					sort: false,
					templet: function(d){
						
						var s="否";
						if(d.IsRFGraphdataPDf+""=="true"){
							s="是";
						}
						//得到当前行数据，并拼接成自定义模板
						return '<span>'+s+'</span>'
					  }
				}
			]
		]
	};
	app.PGroupList=[];
	app.SickTypeList=[];
	//初始化  
	app.init = function () {
		var me = this;
		me.initBottomTable("");
		me.initPGroupSelect();
		me.initSickTypeSelect();
		me.listeners();
	};

	//监听事件
	app.listeners = function () {
		var me=this;
		//头工具栏事件
		table.on('toolbar(printSettingTable)', function(obj){
			var checkStatus = table.checkStatus(obj.config.id);
			var data = checkStatus.data;
			var defaultData={
				"SPID": "",
				"SectionNo": "",
				"PrintFormat": "",
				"PrintProgram": "",
				"DefPrinter": "站点默认打印机",
				"TestItemNo": "",
				"ItemCountMin": "",
				"ItemCountMax": "",
				"sicktypeno": "",
				"PrintOrder": "1",
				"microattribute": "",
				"IsRFGraphdataPDf": "false"
			}
			switch(obj.event){
			case 'add':
				//layer.msg(uxutil.path.ROOT);
				layer.open({
					title:'添加',
					type:1,
					content:$("#addAndUpdateForm"),
					area:['400px','430px'],//宽高
					skin: 'layui-layer-molv',//样式类名
					btn: ['提交'],
					yes:function(index, layero){
						
						var flag=me.addOrUpdateData("add");
						if(flag){
							layer.close(index);
							me.init();
						}
						
					},
					end: function(){
						form.val('dataForm',defaultData);
					}
				});
				// layer.open({
				// 	title:'添加',
				// 	type:2,
				// 	content:uxutil.path.ROOT + "/ui_new/layui/class/setting/xmlConfig/addAndUpdate/addAndUpdate.html?formType=add",
				// 	area:['400px','220px'],//宽高
				// 	skin: 'layui-layer-molv',//样式类名
				// 	end: function(){
				// 		me.init();
				// 	}
				// });
			break;
			case 'modify':
				if(data.length!=1){
					layer.open({
						title: '错误信息'
						, content: '请正确选择修改项!'
						, skin: 'layui-layer-molv' //样式类名
					});
				}else{
					data[0].IsRFGraphdataPDf=data[0].IsRFGraphdataPDf+"";
					form.val('dataForm',data[0]);
					layer.open({
						title:'修改',
						type:1,
						content:$("#addAndUpdateForm"),
						area:['400px','430px'],//宽高
						skin: 'layui-layer-molv',//样式类名
						btn: ['提交','取消'],
						yes:function(index, layero){
							var flag=me.addOrUpdateData("update");
							if(flag){
								layer.close(index);
								me.init();
							}
						},
						btn2: function(index, layero){
							//按钮【按钮二】的回调
							
						},
						end: function(){
							form.val('dataForm',defaultData);
						}
					});
				}
			break;
			case 'delete':
				if(data.length<1){
					layer.open({
						title: '错误信息'
						, content: '请至少选择一条数据!'
						, skin: 'layui-layer-molv' //样式类名
					});
				}else{
					layer.open({
						title: '删除确认'
						, content: '确定要删除吗！'
						,btn:['确认','取消']
						, skin: 'layui-layer-molv' //样式类名
						,yes: function(index, layero){
							//确认按钮
							me.deleteTemplate(data);
							app.init();
							layer.close(index); //如果设定了yes回调，需进行手工关闭
						}
						,btn2: function(index, layero){
						
						}
					});
					
				}
				
				
			break;
			
			};
		});
		//select选中事件
		form.on('select(PGroupSelect)', function(data){
			me.initBottomTable("?SectionNo="+data.value);
			//选中某个选项后me.initBottomTable()执行过后，小组选择会变空，问题未知？
			if(me.PGroupList.length>0){
				var tempAjax = "";
				for (var i = 0; i < me.PGroupList.length; i++) {
					tempAjax = "<option value='" + me.PGroupList[i].SectionNo + "'>" + me.PGroupList[i].CName + "</option>";
					
					$("#PGroupSelect").append(tempAjax);
				}
				form.render('select'); //需要渲染一下;
			}
			
			
		  });   
	}
	//初始化底部侧列表  
	app.initBottomTable = function (where) {
		var me = this;
		
		//page和limit默认会传
		var url = me.url.selectUrl+where;
		table.render({
			elem: '#printSettingTable',
			height: 'full-5',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: url,
			toolbar: '#toolbarDemo', //开启头部工具栏，并为其绑定左侧模板
			defaultToolbar:[],
			cols: me.cols.left,
			limit: 50,
			limits: [10, 20, 50, 100, 200],
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
				
				
			}
		});
	};
	app.initPGroupSelect=function(){
		var me = this;
		var url = me.url.selectPGroupUrl;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (data) {
				
			if (data) {
				if (!data.success) {
					layer.msg(data.ErrorInfo);
					return;
				}
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
						//me[dataListName] = value.list;
					} else {
						value = value + "";
					}
				}
				if (!value) return;	
				me.PGroupList=value;
				var tempAjax = "";
				for (var i = 0; i < value.length; i++) {
					tempAjax = "<option value='" + value[i].SectionNo + "'>" + value[i].CName + "</option>";
					$("#PGroupSelect").append(tempAjax);
					$("#addAndUpdatePGroup").append(tempAjax);
				}
				
				form.render('select'); //需要渲染一下;			
			} else {
				layer.msg(data.msg);
			}
		});
	}
	//初始化弹出层就诊类型下拉框
	app.initSickTypeSelect=function(){
		var me = this;
		var url = me.url.sickTypeselectUrl;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (data) {
				
			if (data) {
				if (!data.success) {
					layer.msg(data.ErrorInfo);
					return;
				}
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
						//me[dataListName] = value.list;
					} else {
						value = value + "";
					}
				}
				if (!value) return;	
				me.SickTypeList=value;
				var tempAjax = "";
				for (var i = 0; i < value.rows.length; i++) {
					tempAjax = "<option value='" + value.rows[i].SickTypeNo + "'>" + value.rows[i].CName + "</option>";
					
					$("#SickType").append(tempAjax);
				}
				
				form.render('select'); //需要渲染一下;			
			} else {
				layer.msg(data.msg);
			}
		});
	}
	/**
	 * 添加或更新一条数据
	 */
	 app.addOrUpdateData=function(formType){
		var me=this;
		var flag=false;
		var data=form.val('dataForm');//获取表单值
		var url='';
		if(formType=="add"){
			url=me.url.addSectionPrintUrl;
		}else{
			url=me.url.updateSectionPrintUrl;
		}
		
		if(data.PrintFormat.replace(/\s/g, "")==""){
			layer.open({
				title: '提示'
				,content: '数据不能为空',
				skin: 'layui-layer-molv'//样式类名
			  
			}); 
		}else{
			//没有值的置为null，数字类型的将字符串转为数字
			for(var key in data){
				if(key==""){
					data[key]=null;
				}else if(!isNaN(data[key])){//是否全为数字
					data[key]=Number(data[key]);
				}
			}
			//属性字符串改为bool
			if(data.IsRFGraphdataPDf=="true"){
				data.IsRFGraphdataPDf=true;
			}else{
				data.IsRFGraphdataPDf=false;
			}
			uxutil.server.ajax({
				url: url,
				async: false,
				type: "post",
				data: JSON.stringify({entity:data})
			}, function (data) {
				if (data) {
					if (data.success) {
						parent.layer.msg("保存成功");
						flag=true;
					} else {
						parent.layer.msg("保存失败");
					}
	
				} else {
					parent.layer.msg("保存失败");
				}
			});
			
		}
		return flag;
	}
	//删除方法
    app.deleteTemplate= function (records) {
		var me=this;
        var delLength = [];
        var str;
        
        for (var i = 0; i < records.length; i++) {
            delLength.push(records[i].SPID);
            
        }
        
		uxutil.server.ajax({
			url: me.url.deleteSectionPrintUrl,
			async: false,
			type: "post",
			data: JSON.stringify({SPID:delLength})
		}, function (data) {
			if (data) {
				if (data.success) {
					layer.msg("删除成功");
					
				} else {
					layer.msg("删除失败");
				}

			} else {
				layer.msg("删除失败");
			}
		});
    }
	app.init();
});