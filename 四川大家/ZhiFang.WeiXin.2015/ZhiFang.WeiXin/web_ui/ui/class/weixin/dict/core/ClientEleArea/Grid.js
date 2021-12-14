Ext.define("Shell.class.weixin.dict.core.ClientEleArea.Grid",{
	
	extend:'Shell.ux.grid.IsUseGrid',
	title:'区域',
	PKField: 'SickType_Id',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaAndCLIENTELEByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_DelClientEleArea',
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用保存按钮*/
	hasSave: false,
	copyTims:0,
	/**默认加载*/
	defaultLoad: true,

    searchInfo: {
		width: 120,
			emptyText: '编码/名称',
			isLike: true,
			itemId: 'search',
			fields: ['clientelearea.AreaCName',"clientelearea.Id"]
	},
    
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	
	initComponent:function(){
		var me = this;
		me.columns=me.createGcolumns();
		me.callParent(arguments);
	},
	
	createGcolumns:function(){
		var me =this;
		var columns=[
		{
			text:'区域编码',
			dataIndex:'ClientEleArea_Id',
			width:100,
			isKey:true,
			hidden:false,
		},{
			text:'中文名称',
			dataIndex:'ClientEleArea_AreaCName',
			width:150,
		},{
			text:'简称',
			dataIndex:'ClientEleArea_AreaShortName',
			width:100,
		},{
			text:'区域总医院',
			dataIndex:'ClientEleArea_clienteleName',
			width:280,
		},
		{
			text:'ClientEleArea_clienteleId',
			dataIndex:"ClientEleArea_clienteleId",
			width:100,
			hidden:true,
		}
		];
		return columns;
	},
});
