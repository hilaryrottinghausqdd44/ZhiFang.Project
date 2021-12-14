Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载

	Ext.Loader.setPath('Ext.manage.bnodeTable', getRootPath() +'/ui/manage/class/bnodeTable');
	var panel = Ext.create('Ext.manage.bnodeTable.bnodeTableApp');
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});