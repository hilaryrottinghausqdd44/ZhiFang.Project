Ext.define('Shell.sysbase.main.Main',{
	extend:'Shell.ux.panel.AppPanel',
	
	title:'总控界面',
	layout:{type:'border'},
	
	help:{iframeUrl:'http://www.baidu.com'},
	
	apps:[
		{className:'Shell.sysbase.main.ModuleTree',itemId:'tree',header:true,region:'west',width:240,split:true,collapsible:true},
		{className:'Shell.sysbase.main.TabPanel',itemId:'tabpanel',header:false,region:'center'}
	],
	initListeners:function(){
		var me = this,
			tree = me.getComponent('tree'),
			tabpanel = me.getComponent('tabpanel');
			
		tree.on({
			itemclick:function(com,record){//模块树点击事件处理
				//添加应用
				tabpanel.setApp({
					itemId:record.get('tid'),
					title:record.get('text'),
					className:record.get('className'),
					url:record.get('url'),
					closable:true
				},true);
			}
		});
	},
	boxIsReady:function(){
		//this.getComponent('tree').load(null,true);
	}
});