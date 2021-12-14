/**
 * 用户订单项目列表
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.userorder.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',

	title: '用户订单项目信息 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderItemByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**复选框*/
	multiSelect: false,
	/**是否启用刷新按钮*/
	hasRefresh:false,
	/**是否启用新增按钮*/
	hasAdd:false,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用删除按钮*/
	hasDel:false,
	/**是否启用保存按钮*/
	//hasSave:true,
	/**是否启用查询框*/
	hasSearch:false,	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'项目名称',dataIndex:'OSUserOrderItem_ItemAllItem_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'市场价格',dataIndex:'OSUserOrderItem_MarketPrice',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'大家价格',dataIndex:'OSUserOrderItem_GreatMasterPrice',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'折扣价格',dataIndex:'OSUserOrderItem_DiscountPrice',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'折扣率',dataIndex:'OSUserOrderItem_Discount',
			width:80,align:'center',sortable:false,menuDisabled:true
		},{
			text:'主键ID',dataIndex:'OSUserOrderItem_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'用户订单ID',dataIndex:'OSUserOrderItem_UOFID',width:170,hidden:true//,hideable:false
		}]
		
		return columns;
	}
});