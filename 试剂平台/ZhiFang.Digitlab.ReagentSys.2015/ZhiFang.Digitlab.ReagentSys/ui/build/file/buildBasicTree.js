var id = getQueryString("id");
if(!id){
	id = -1;
}

Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载

	var sysParameters = {webPath:getRootPath()};
	Ext.state.Manager.set('sysParameters',sysParameters);
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		padding:'4 4 4 4',
		layout:'fit',
		items:[{
			xtype:'basictreepanel',
			appId:id
		}]
	});
});