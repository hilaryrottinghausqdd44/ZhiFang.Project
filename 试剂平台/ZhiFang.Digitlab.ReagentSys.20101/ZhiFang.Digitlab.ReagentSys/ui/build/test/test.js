Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var appInfos = [
		{appId:'4645548760144166405',itemId:'list',region:'center',title:'AAAAA'},//省份列表
		{appId:'4936489756416909303',itemId:'form',region:'east',title:'',width:200}//省份表单
	];
	
	var link = "me.getComponent('list').on({itemclick:function(view,record){" + 
			   		"me.getComponent('form').load(record.get(this.objectName+'_Id'));" + 
			   "}});";
			   
//	var appInfos = [
//		{appId:'4645548760144166400',itemId:'list',region:'center',title:'-'},//省份列表
//		{appId:'4936489756416909300',itemId:'form',region:'east',title:'-',width:200}//省份表单
//	];

//	var panel = Ext.create('Ext.build.TestApp',{
//		appInfos:appInfos,
//		title:'应用引用测试',
//		width:600,
//		height:300,
//		layout:'border',
//		link:link
//	});
	
	//应用信息
	var appInfo = {
		appId:'4645548760144166405',
		title:'AAAAA',
		children:[{
			appId:'4645548760144166405',itemId:'list',region:'center',title:'AAAAA'
		},{
			appId:'4936489756416909303',itemId:'form',region:'east',title:'',width:200
		}]
	};
	
	var panel = Ext.create('Ext.build.TestApp',{
		
	});
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});