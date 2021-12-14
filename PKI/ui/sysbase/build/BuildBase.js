/**
 * 构建基础类
 * @author Jcall
 * @version 2014-08-25
 */
Ext.define('Shell.sysbase.build.BuildBase',{
	extend:'Shell.ux.panel.AppPanel',
	
	title:'构建基础面板',
	layout:{type:'border'},
	
	width:1200,
	height:600,
	
	help:{className:'Shell.help.BuildBase'},
	
	apps:[
		{className:'Shell.sysbase.build.panel.ShowPanel',itemId:'showpanel',region:'center'},
		{className:'Shell.sysbase.build.panel.ParamsPanel',itemId:'paramspanel',
			header:false,region:'east',width:250,split:true,collapsible:true}
	],
	/**初始化监听*/
	initListeners:function(){
		var me = this;
	},
	/**面板准备就绪*/
	boxIsReady:function(){
		
	},
	/**点击保存按钮*/
	onSaveClick:function(but){
		alert('保存');
	},
	/**点击另存按钮*/
	onSaveasClick:function(but){
		alert('另存');
	}
});