/**
 * 收费组合项目维护
 * @author longfc	
 * @version 2020-08-08
 */
Ext.define('Shell.class.sysbase.chargegroupitem.GChargeItemGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	title: '收费组合项目列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeItem',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemByField',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeItem',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodChargeItem_DispOrder',
		direction: 'ASC'
	}],
	/**默认每页数量*/
	defaultPageSize: 50,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**用户UI配置Key*/
	userUIKey: 'chargegroupitem.SimpleGrid',
	/**用户UI配置Name*/
	userUIName: "收费组合项目列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//组合项目
		me.defaultWhere = "bloodchargeitem.IsGroup=1";
		//查询框信息
		me.searchInfo = {
			width: 185,
			emptyText: '名称/快捷码',
			isLike: true,
			itemId: 'Search',
			fields: ['bloodchargeitem.CName', 'bloodchargeitem.ShortCode']
		};
		me.buttonToolbarItems = ['refresh', '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BloodChargeItem_Id',
			text: '组合项目编号',
			width: 100,
			isKey: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BloodChargeItem_CName',
			text: '组合项目名称',
			minWidth: 150,
			flex: 1,
			defaultRenderer: true
		}, {
			dataIndex: 'BloodChargeItem_ShortCode',
			text: '快捷码',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}];

		return columns;
	}

});
