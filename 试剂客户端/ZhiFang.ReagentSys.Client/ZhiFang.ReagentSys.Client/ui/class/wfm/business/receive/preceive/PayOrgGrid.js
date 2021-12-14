/**
 * 付款单位列表
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.PayOrgGrid',{
	extend: 'Shell.ux.grid.Panel',
    title: '客户列表',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePClientByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPClient',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PClient_ProvinceName',
		direction: 'ASC'
	},{
		property: 'PClient_Name',
		direction: 'ASC'
	}],
	checkOne: false,
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
     /**带分页栏*/
//	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	defaultWhere:'pclient.IsUse=1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
	
		//查询框信息
		me.searchInfo = {
			width:145,emptyText:'名称',isLike:true,itemId:'search',
			fields:['pclient.Name']
		};
		
		me.buttonToolbarItems = ['refresh','->',{
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'客户名称',dataIndex:'PClient_Name',width:120,
			sortable:false,defaultRenderer:true
		},{
			text:'区域',dataIndex:'PClient_ClientAreaName',width:70,
			sortable:false,defaultRenderer:true
		},
		{
			text:'客户主键ID',dataIndex:'PClient_Id',
			isKey:true,hidden:true,hideable:false
		}];
		return columns;
	}
});