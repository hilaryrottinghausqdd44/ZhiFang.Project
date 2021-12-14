/**
 * 列表构建类
 * @author Jcall
 * @version 2014-08-19
 */
Ext.define('Shell.sysbase.build.BuildList',{
	extend:'Shell.sysbase.build.BuildBase',
	title:'列表构建',
	
	help:{iframeUrl:'http://www.baidu.com'},
	
	toolbars:[{dock:'top',buttons:['->','saveas','save']}],
	
	apps:[
		{className:'Shell.sysbase.build.panel.ShowPanel',itemId:'showpanel',region:'center'},
		{className:'Shell.sysbase.build.panel.ParamsPanel',itemId:'paramspanel',hasLayoutInfo:false,
			header:false,region:'east',width:250,split:true,collapsible:true}
	]
});