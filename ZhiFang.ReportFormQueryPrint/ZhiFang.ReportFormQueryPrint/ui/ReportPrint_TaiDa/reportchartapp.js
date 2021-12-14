Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});

Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	Shell.util.Win.begin();//屏蔽快捷键
	
	var panel = Ext.create('Shell.ReportPrint.class.PrintChart',{hasCollapseButton:false}),
		params = Shell.util.Path.getRequestParams(),
		obj = {};
	
	if(params["PATNO"]) obj.PatNo = params["PATNO"];
	if(params["ITEMNO"]) obj.ItemNo = params["ITEMNO"];
	if(params["SECTIONTYPE"]) obj.Table = params["SECTIONTYPE"].toLocaleLowerCase();
	if(params["RECEIVEDATE"]) obj.ReceiveDate = params["RECEIVEDATE"];
	
	panel.on({
		boxready:function(){
			panel.load(obj);
		}
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:2,
		items:[panel]
	});
});