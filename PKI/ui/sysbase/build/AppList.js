/**
 * 应用列表
 * @author Jcall
 * @version 2014-08-19
 */
Ext.define('Shell.sysbase.build.AppList',{
	extend:'Shell.ux.panel.Grid',
	
	alias:'widget.buildapplist',
	title:'应用信息列表',
	tooltip:true,
	multiSelect:true,
	width:600,
	height:250,
	pagingtoolbar:'number',
	defaultLoad:true,
	defaultPageSize:10,
	
	delUrl:'/ConstructionService.svc/CS_UDTO_DelBTDAppComponents',
	selectUrl:'/ConstructionService.svc/CS_UDTO_SearchRefBTDAppComponentsByHQLAndId?isPlanish=true&fields=' + 
	'BTDAppComponents_CName,BTDAppComponents_Id,BTDAppComponents_ModuleOperCode,BTDAppComponents_BuildType,' +
	'BTDAppComponents_AppType,BTDAppComponents_DataAddTime,BTDAppComponents_ModuleOperInfo',
	
	columns:[
		{dataIndex:'BTDAppComponents_CName',text:'中文名称',width:150},
		{dataIndex:'BTDAppComponents_Id',text:'应用ID',type:'key',width:150},
		{dataIndex:'BTDAppComponents_ModuleOperCode',text:'功能编码',width:150},
		{dataIndex:'BTDAppComponents_BuildType',text:'构建'},
		{dataIndex:'BTDAppComponents_AppType',text:'类型'},
		{dataIndex:'BTDAppComponents_DataAddTime',text:'创建时间',type:'datetime',width:140},
		{dataIndex:'BTDAppComponents_ModuleOperInfo',text:'备注',width:200}
	],
	appTypeList:[
		{text:'应用',appType:3,className:'Shell.sysbase.build.BuildApp',iconCls:'button-app'},
		{text:'列表',appType:1,className:'Shell.sysbase.build.BuildList',iconCls:'button-list'},
		{text:'表单',appType:2,className:'Shell.sysbase.build.BuildForm',iconCls:'button-form'}
	],
	initComponent:function(){
		var me = this,
			appTypeList = me.appTypeList,
			length = appTypeList.length;
		
		for(var i=0;i<length;i++){
			appTypeList[i].listeners = {click:function(but){me.onAddClick(but);}};
		}
		
		me.toolbars = [
			{dock:'top',buttons:[
				'refresh','-',
				//'add','edit','show',
				{btype:'add',menu:appTypeList},
//				{btype:'edit',className:'Shell.test.class.BCountryForm'},
//				{btype:'show',className:'Shell.test.class.BCountryForm'},
				'del','-','print','exp','-',
				{btype:'combo',width:80,emptyText:'构建类型',searchField:'btdappcomponents.BuildType',
					data:[['所有程序',null],['构建程序','1'],['定制程序','0']]
				},
				{btype:'combo',width:80,emptyText:'应用类型',searchField:'btdappcomponents.AppType',
					data:[['所有类型',null],['应用','3'],['列表','1'],['表单','2']]
				},
				'-',
				{btype:'searchtext',width:160,emptyText:'中文名称/功能编码'},
				'->',
				{btype:'help',text:'',iframeUrl:'http://www.baidu.com'}],
				searchFields:['btdappcomponents.CName','btdappcomponents.ModuleOperCode']
			}
		]
		
		me.callParent(arguments);
	}
});