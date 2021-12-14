Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	var panel = Ext.create('Shell.sysbase.user.UserManage');
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});