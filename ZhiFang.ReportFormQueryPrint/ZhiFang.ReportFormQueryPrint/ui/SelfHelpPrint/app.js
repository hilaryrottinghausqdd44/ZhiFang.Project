Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = [];
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	Shell.util.Win.begin();//屏蔽快捷键
	
	//根据参数决定是否显示log信息
	var params = Shell.util.Path.getRequestParams(true);
	for(var i in params){
		if(i.toLowerCase() === "SHOWLOG" && params[i] === "true"){
			Shell.util.Config.showLog = true;
		}else if(i.toLowerCase() === "SHOWLOGWIN" && params[i] === "true"){
			Shell.util.Config.showLogWin = true;
		}
	}
	
	panel.push(Ext.create("Shell.SelfHelpPrint.class.PrintApp", {
	    region: 'center'
	}));
	panel.push(Ext.create("Shell.SelfHelpPrint.class.PrintSouth", {
	    region: 'south'
	}));
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'border',
		padding:2,
		items:panel
	});
    //底部时间
	Ext.TaskManager.start({
	    run: function () {
	        viewport.getComponent("southtime").getComponent("autotimes").setText(Ext.Date.format(new Date(), 'g:i:s A'));
	    },
	    interval: 1000
	});

});