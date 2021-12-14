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
	
	
	panel = Ext.create('Ext.panel.Panel', {
	    afterRender: function () {
	        Shell.util.Win.open('Shell.class.siteQuery.addEmpDept.addPanel', {
	            formtype: 'add',
	            resizable: false,
	            maximizable: false,//是否带最大化功能
	            closable: false,
	            listeners: {
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
