/**
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
*/
Ext.Loader.setConfig({
    enabled: true,
    paths: { 'Shell': Shell.util.Path.uiPath }
});
var panel = null;
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
	
	panel = Ext.create('Ext.panel.Panel', {
	    afterRender: function () {
	        Shell.util.Win.open('Shell.class.userSign.app.App', {
	            formtype: 'add',
	            resizable: false,
	            maximizable: false,//是否带最大化功能
	            closable: false,
	            listeners: {
	                login: function (p) {
	                    p.close();
	                    window.location.href = Shell.util.Path.uiPath + '/class/nurse/';
	                }
	            }
	        }).show();
	    }
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:1,
		items:[panel]
	});
});
