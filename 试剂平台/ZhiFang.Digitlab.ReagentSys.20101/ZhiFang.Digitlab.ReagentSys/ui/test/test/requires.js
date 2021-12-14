Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	Ext.Loader.setPath('Ext.zhifangux', '../../zhifangux');
	
	var panel = Ext.create('Ext.zhifangux.FormPanel',{
		title:'自动加载文件测试',
		items:[{
			xtype:''
		}]
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});