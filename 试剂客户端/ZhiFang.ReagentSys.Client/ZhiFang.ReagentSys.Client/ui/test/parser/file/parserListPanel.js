Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	Ext.Loader.setPath('Ext.zhifangux', '../../zhifangux');
	Ext.Loader.setPath('Ext.build', '../class');
	
	var panel = Ext.create('Ext.build.ParserPanel',{
		type:'list',
		defaultLoad:true,//是否默认加载数据
		remoteSort:true//是否开启远程排序
	});
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});