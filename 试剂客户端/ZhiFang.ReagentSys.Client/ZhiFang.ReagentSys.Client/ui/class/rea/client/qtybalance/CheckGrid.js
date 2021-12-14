/**
 * 库存结转
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.qtybalance.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	
	title: '库存结转',
	width: 680,
	height: 480,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**是否单选*/
	checkOne: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsQtyBalanceDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'qtybalance.CheckGrid',
	/**用户UI配置Name*/
	userUIName: "库存结转选择列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere="reabmsqtybalancedoc.Visible=1";
		//查询框信息
		me.searchInfo = {
			emptyText: '结转单号/操作人',
			itemId: 'Search',
			//flex: 1,
			width: '70%',
			isLike: true,
			fields: ['reabmsqtybalancedoc.QtyBalanceDocNo', 'reabmsqtybalancedoc.OperName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsQtyBalanceDoc_PreBalanceDateTime',
			text: '上次结转日期',
			width: 135,
			isDate: true,
			hasTime: true
		},{
			dataIndex: 'ReaBmsQtyBalanceDoc_DataAddTime',
			text: '结转日期',
			align: 'center',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_QtyBalanceDocNo',
			text: '结转单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_OperName',
			text: '操作人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyBalanceDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	}
});