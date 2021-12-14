/**
 * 模板配置信息页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'element'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		$ = layui.jquery;
	var app = {};
	//服务地址
	app.url = {
		//获取版本
		selectUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/LoadConfig?fileName=ReportFromShowXslConfig.xml',
		AddUrl: uxutil.path.ROOT +'/ServiceWCF/ReportFormService.svc/SaveConfig'
	};
	
	//get参数
	app.paramsObj = {
		formType: '',//添加add还是更新update
		record:'',//要修改的对象
		dataList:[]
	};
	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		//获取模块id
		if (params.FORMTYPE) {
			me.paramsObj.formType = params.FORMTYPE;
		}
		if (params.RECORD) {
			me.paramsObj.record = $.parseJSON($.parseJSON(params.RECORD));//转对象
		}
		// if (params.DATALIST) {
		// 	me.paramsObj.dataList = $.parseJSON($.parseJSON(params.DATALIST));//转对象
		// }
		
		
	};
	//初始化  
	app.init = function () {
		
		var me = this;
		me.getData();
		me.getParams();
		form.val('dataForm',me.paramsObj.record);//获取表单值
		me.listeners();
		
	};

	//监听事件
	app.listeners = function () {
		var me=this;
		$("#saveButton").click(function(){
			me.addOrUpdateData();
		});

		//自定义验证规则
		form.verify({
			notnull: function(value){
			  if(value){
				//value.replace(/\s/g, "");去掉所有空格
				if(value.replace(/\s/g, "")==""){
					return '数据不能为空';
				}
				
			  }else{
				return '数据不能为空';
			  }
			}
			
			
		  });
	}
	/**
	 * 添加或更新一条数据
	 */
	app.addOrUpdateData=function(){
		var me=this;
		var data=form.val('dataForm');//获取表单值
		if(data.ReportType.replace(/\s/g, "")==""||data.XSLName.replace(/\s/g, "")==""||data.PageName.replace(/\s/g, "")==""||data.Name.replace(/\s/g, "")==""){
			parent.layer.open({
				title: '提示'
				,content: '数据不能为空',
				skin: 'layui-layer-molv'//样式类名
			  
			  }); 
			var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
			parent.layer.close(index);//关闭弹出层
			
		}else{
			var saveDataList=[];
			if(me.paramsObj.formType=='add'){
				for(var i=0;i<me.paramsObj.dataList.length;i++){
					saveDataList.push({"ReportType":me.paramsObj.dataList[i].ReportType,"XSLName":me.paramsObj.dataList[i].XSLName,"PageName":me.paramsObj.dataList[i].PageName,"Name":me.paramsObj.dataList[i].Name})					
				}
				saveDataList.push(data);
			}else if(me.paramsObj.formType=='update'){
				//因为查询出的数据做了处理，添加了number字段，在保存时要重新构造一下对象
				for(var i=0;i<me.paramsObj.dataList.length;i++){
					if(me.paramsObj.record.number==me.paramsObj.dataList[i].number){
						saveDataList.push({"ReportType":data.ReportType,"XSLName":data.XSLName,"PageName":data.PageName,"Name":data.Name})						
					}else{
						saveDataList.push({"ReportType":me.paramsObj.dataList[i].ReportType,"XSLName":me.paramsObj.dataList[i].XSLName,"PageName":me.paramsObj.dataList[i].PageName,"Name":me.paramsObj.dataList[i].Name})
					}
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
						parent.layer.msg("保存成功");
						
					} else {
						parent.layer.msg("保存失败");
					}
	
				} else {
					parent.layer.msg("保存失败");
				}
			});
			var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
			parent.layer.close(index);//关闭弹出层
		}
		
	}
	/**
	 * 获取表格数据列表
	 */
	app.getData=function(){
		var me = this;
		var url = me.url.selectUrl;
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
						
						$.each(value.DataSet.ReportFromShowXslConfig,function(i,item){
							item["number"]=i;
						})
						me.paramsObj.dataList = value.DataSet.ReportFromShowXslConfig;
					} else {
						value = value + "";
					}
				}
				if (!value) return;
			} else {
				layer.msg(data.msg);
			}
		});
	}
	
	app.init();
});