Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	panel = Ext.create('Shell.class.manage.delete.Panel');
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:1,
		items:[panel]
	});
});