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
	
	var ClassName = JShell.Page.getParams(true).CLASSNAME;
	var panel = null;
	if(ClassName){
		panel = Ext.create(ClassName);
		//总体布局
		
	}else{
		panel = Ext.create('Ext.panel.Panel',{
			html:'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">请传递ClassName参数</div>'
		});
	}
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});
