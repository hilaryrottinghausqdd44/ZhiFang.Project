/**
 * 模板配置信息页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'element','table'], function () {
	var uxutil = layui.uxutil,
		table = layui.table,
		$ = layui.jquery;
	var app = {};
	//服务地址
	app.url = {
		
		selectUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/LoadConfig?fileName=ReportFromShowXslConfig.xml',
		AddUrl: uxutil.path.ROOT +'/ServiceWCF/ReportFormService.svc/SaveConfig'
	};
	app.dataList=[];
	app.cols = {
		left: [
			[
				{ type: 'checkbox'},
				{field: 'ReportType',
					title: '小组类别',
					width: 200,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'XSLName',
					title: '模板名称',
					width: 200,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'PageName',
					title: '页面名称',
					width: 200,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'Name',
					title: '样式名',
					width: 200,
					//align: 'center',
					//hide: true,
					sort: false
				}
			]
		]
	};
	//初始化  
	app.init = function () {
		var me = this;
		me.initBottomTable();
		me.listeners();
		
	};

	//监听事件
	app.listeners = function () {
		var me=this;
		//头工具栏事件
		table.on('toolbar(xmlTable)', function(obj){
			var checkStatus = table.checkStatus(obj.config.id);
			var data = checkStatus.data;
			switch(obj.event){
			case 'add':
				//layer.msg(uxutil.path.ROOT);
				layer.open({
					title:'添加',
					type:2,
					content:uxutil.path.ROOT + "/ui_new/layui/class/setting/xmlConfig/addAndUpdate/addAndUpdate.html?formType=add",
					area:['400px','220px'],//宽高
					skin: 'layui-layer-molv',//样式类名
					end: function(){
						me.init();
					}
				});
			break;
			case 'modify':
				if(data.length!=1){
					layer.open({
						title: '错误信息'
						, content: '请正确选择修改项!'
						, skin: 'layui-layer-molv' //样式类名
					});
					
				}else{
					var engData=app.toEnglish(data);
					var record=JSON.stringify(engData[0]);
					var url=uxutil.path.ROOT + '/ui_new/layui/class/setting/xmlConfig/addAndUpdate/addAndUpdate.html?formType=update'+'&record='+encodeURI(JSON.stringify(record));//+'&dataList='+encodeURI(JSON.stringify(dataList));//对象参数要进行编码，不然获取参数时会截取错误					
					layer.open({
						title:'更新',
						type:2,
						content:url,
						area:['400px','220px'],//宽高
						skin: 'layui-layer-molv',//样式类名
						end: function(){
							me.init();
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
							var engRecords=app.toEnglish(data);//选中的数据
							var engDataList=app.toEnglish(app.dataList);//原始数据
							me.deleteTemplate(engRecords,engDataList);
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
	}
	//初始化底部侧列表  
	app.initBottomTable = function () {
		var me = this;
		
		//page和limit默认会传
		var url = me.url.selectUrl;
		table.render({
			elem: '#xmlTable',
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
		
				$.each(data.DataSet.ReportFromShowXslConfig,function(i,item){
					item["number"]=i;
				})
				app.dataList=data.DataSet.ReportFromShowXslConfig;//修改时传参
				var dataList=app.toChinese(data.DataSet.ReportFromShowXslConfig);
				return {
					"code": res.success ? 0 : 1, //解析接口状态		
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.DataSet.ReportFromShowXslConfig.length || 0, //解析数据长度	
					"data": dataList || []
				};
			},
			done: function (res, curr, count) {
				
				
			}
		});
	};

	/*显示时小组类别将数据库中英文字段改为汉字*/
	app.toChinese=function(data){
		var result = data;
		if(result.length>0){
			for(var i =0 ;i<result.length;i++){
				switch(result[i].ReportType){
					case "Normal":
						result[i].ReportType = "生化类";
						break;
					case "Micro":
						result[i].ReportType = "微生物";
						break;
					case "NormalIncImage":
						result[i].ReportType = "生化类(图)";
						break;
					case "MicroIncImage":
						result[i].ReportType = "微生物(图)";
						break;
					case "CellMorphology":
						result[i].ReportType = "细胞形态学";
						break;
					case "FishCheck":
						result[i].ReportType = "Fish检测(图)";
						break;
					case "SensorCheck":
						result[i].ReportType = "院感检测(图)";
						break;
					case "ChromosomeCheck":
						result[i].ReportType = "染色体检测(图)";
						break;
					case "PathologyCheck":
						result[i].ReportType = "病理检测(图)";
						break;
				}
			}	
		}
		
		return data;
	}
	/*小组类别将汉字段改为英文保存到数据库*/
	app.toEnglish=function(list){
		var result = list;
		if(result.length>0){
			for(var i =0 ;i<result.length;i++){
				switch(result[i].ReportType){
					case "生化类":
						result[i].ReportType = "Normal";
						break;
					case "微生物":
						result[i].ReportType = "Micro";
						break;
					case "生化类(图)":
						result[i].ReportType = "NormalIncImage";
						break;
					case "微生物(图)":
						result[i].ReportType = "MicroIncImage";
						break;
					case "细胞形态学":
						result[i].ReportType = "CellMorphology";
						break;
					case "Fish检测(图)":
						result[i].ReportType = "FishCheck";
						break;
					case "院感检测(图)":
						result[i].ReportType = "SensorCheck";
						break;
					case "染色体检测(图)":
						result[i].ReportType = "ChromosomeCheck";
						break;
					case "病理检测(图)":
						result[i].ReportType = "PathologyCheck";
						break;
				}
			}	
		}
		list = result;
		return list;
	}
	//删除方法
    app.deleteTemplate= function (records,dataList) {
		var me=this;
		var saveDataList=[];
		for(var i=0;i<dataList.length;i++){
			var flag=true;
			for(var j=0;j<records.length;j++){
				if(dataList[i].number==records[j].number){
					flag=false;
					break;
				}
			}
			if(flag){
				saveDataList.push({"ReportType":dataList[i].ReportType,"XSLName":dataList[i].XSLName,"PageName":dataList[i].PageName,"Name":dataList[i].Name})
			}
		}
		var config = {
			"?xml": {"@version":"1.0","@encoding":"utf-8"},
			"DataSet": { "ReportFromShowXslConfig": saveDataList }
		   };
		config = JSON.stringify(config);
		var saveData={
			"fileName": "ReportFromShowXslConfig.xml",
			"configStr": config
		}
		//console.log(JSON.stringify(saveData));
		uxutil.server.ajax({
			url: me.url.AddUrl,
			async: false,
			type: "post",
			data: JSON.stringify(saveData)
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