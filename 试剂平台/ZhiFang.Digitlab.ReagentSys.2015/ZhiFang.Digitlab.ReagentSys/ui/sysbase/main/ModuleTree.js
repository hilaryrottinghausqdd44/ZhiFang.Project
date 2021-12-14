/**
 * 功能模块树
 * @author Jcall
 * @version 2014-08-18
 */
Ext.define('Shell.sysbase.main.ModuleTree',{
	extend:'Shell.ux.panel.Tree',
	
	/**根节点对象*/
	root1:{
		text:'功能菜单',expanded:true,leaf:false,
		Tree:[
			{text:'应用信息列表',expanded:true,leaf:true,tid:'1',className:'Shell.sysbase.build.AppList'},
			{text:'应用测试',expanded:true,leaf:true,tid:'2',className:'Shell.test.class.BCountryApp'},
			{text:'按钮测试',expanded:true,leaf:true,tid:'3',url:'test/buttons.html'},
			{text:'查询栏测试',expanded:true,leaf:true,tid:'4',url:'test/layout.html'},
			{text:'ECharts测试',expanded:true,leaf:true,tid:'5',url:'test/echarts.html'},
			{text:'错误测试',expanded:true,tid:'6',leaf:true},
			{text:'多文件上传测试',expanded:true,leaf:true,tid:'7',url:'test/fileupdate.html'},
			{text:'软件信息维护',expanded:true,leaf:true,tid:'8',className:'Shell.sysbase.manage.SoftwareApp'}
		]
	}
});