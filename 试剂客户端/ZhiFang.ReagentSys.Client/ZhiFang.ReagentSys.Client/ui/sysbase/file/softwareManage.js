/**
 * 软件管理
 * @author Jcall
 * @version 2014-08-26
 */
Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.rootPath+'/ui'}
});
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	var panel = Ext.create('Shell.sysbase.manage.SoftwareApp');
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:2,
		items:[panel]
	});
});