Ext.Loader.setConfig({
	enabled:true,
	paths:{
		'Shell':JShell.System.Path.UI,
		'Ext.ux':JShell.System.Path.UI + '/extjs/ux'
	}
});

Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	JShell.Window = Ext.create('Ext.window.Window',{
		layout:'fit',
		header:false,
		border:false,
		margin:0,
		padding:0,
		modal:true,
		plain:true,
		draggable:false,
		resizable:false,
		closeAction:'hide',
		close:function(){return JShell.Window.closeFun();}
	});
	Ext.create('Shell.class.sysbase.main.Viewport');
});