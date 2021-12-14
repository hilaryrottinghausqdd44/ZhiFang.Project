Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var panel = Ext.create('Ext.build.ComponentDefaultValueConfig',{
		width:250,
		height:300,
		collapsible:true,
		title:'初始值配置',
		entityName:'HREmployee'
//		valueType:'1',
//		fixedValueType:'2',
//		valueField:'HREmployee_Id',
//		displayField:'HREmployee_CName',
//		valueServer:'RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}',
//		valueList:'4'
	});
	
	panel.on({
		valueChange:function(obj){
			var str = "";
			str = 
				"【valueType=" + obj.valueType + ";】" + 
				"【fixedValueType=" + obj.fixedValueType + ";】" + 
				"【value=" + obj.value + ";】" + 
				"【valueField=" + obj.valueField + ";】" + 
				"【displayField=" + obj.displayField + ";】" + 
				"【valueServer=" + obj.valueServer + ";】";
			alert(str);
		}
	});
	
//	var obj = {
//		valueType:'1',
//		fixedValueType:'2',
//		value:'4',
//		valueField:'HREmployee_Id',
//		displayField:'HREmployee_CName',
//		valueServer:'RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}'
//	};
//	panel.setDefaultValue(obj);
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		//layout:'fit',
		items:[panel]
	});
});