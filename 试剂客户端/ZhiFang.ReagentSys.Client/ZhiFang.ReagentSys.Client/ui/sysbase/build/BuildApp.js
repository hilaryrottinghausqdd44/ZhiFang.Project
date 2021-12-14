/**
 * 应用构建类
 * @author Jcall
 * @version 2014-08-19
 */
Ext.define('Shell.sysbase.build.BuildApp',{
	extend:'Shell.sysbase.build.BuildBase',
	title:'应用构建',
	
	help:{iframeUrl:'http://www.baidu.com'},
	
	toolbars:[{dock:'top',buttons:['->','saveas','save']}],
	
	apps:[
		{className:'Shell.sysbase.build.panel.ShowPanel',itemId:'showpanel',region:'center'},
		{className:'Shell.sysbase.build.panel.ParamsPanel',itemId:'paramspanel',hasObjectInfo:false,
			header:false,region:'east',width:250,split:true,collapsible:true}
	],
	/**初始化监听*/
	initListeners:function(){
		var me = this,
			showpanel = me.getComponent('showpanel'),
			paramspanel = me.getComponent('paramspanel');
			
		paramspanel.on({
			panelInfoBlur:function(panel,field){
				//alert(panel.title + ';' + field.getValue(true));
			},
			panelInfoChange:function(panel,field,newV,oldV){
				alert(newV + ';' + oldV);
			},
			layoutTypeChange:function(panel,v){
				alert(v.value + ";" + v.name + ";" + v.layout.layout + ';' + v.info);
			}
		});
	}
});