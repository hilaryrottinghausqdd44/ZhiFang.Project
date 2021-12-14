
Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var grid = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Shell.util.Win.begin();//屏蔽快捷键
	grid = Ext.create("Shell.class.setting.xmlConfig.grid", {
			//style:"margin-top:10px;margin-bottom:10px",
			width:1000,
			height:500,
			appType:"me.appType",
			title:"模板配置信息"
		});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:2,
		items:[grid]
	});
});
