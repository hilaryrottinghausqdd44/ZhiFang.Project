/**
 * 库房(货架)试剂维护
 * @author longfc	
 * @version 2019-07-09
 */
Ext.define('Shell.class.rea.client.storagegoodslink.StorageGrid',{
    extend:'Shell.class.rea.client.basic.GridPanel',
    
    title:'库房列表',
    width:800,
    height:500,
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',

    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:false,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用删除按钮*/
	hasDel:false,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'ReaStorage_DispOrder',direction:'ASC'}],

    /**默认每页数量*/
	defaultPageSize: 50,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**原始行数*/
	oldCount: 0,
	CenOrgList:[],
    CenOrgEnum:null,
    /**用户UI配置Key*/
	userUIKey: 'storagegoodslink.StorageGrid',
	/**用户UI配置Name*/
	userUIName: "库房列表",
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:185,emptyText:'库房名称/英文名称',isLike:true,itemId:'Search',
			fields:['reastorage.CName','reastorage.EName']
		};		
		me.buttonToolbarItems = ['refresh','->',{
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			dataIndex: 'ReaStorage_CName',
			text: '库房名称',
			minWidth: 150,flex:1,
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_EName',
			text: '英文名称',
			width: 100,hidden:true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	}
	
});