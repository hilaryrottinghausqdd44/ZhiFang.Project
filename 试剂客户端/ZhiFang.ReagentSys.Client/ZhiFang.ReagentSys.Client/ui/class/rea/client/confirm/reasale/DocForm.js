/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reasale.DocForm', {
	extend: 'Shell.class.rea.client.confirm.basic.DocForm',

	title: '验货单信息',
	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?isPlanish=true',

	OTYPE: "reasale",
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 5 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.width = me.width || 405;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 160) me.defaults.width = 160;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push({
			fieldLabel: '供货单Id',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_SaleDocID',
			itemId: 'ReaBmsCenSaleDocConfirm_SaleDocID',
			readOnly: true,
			locked: true
		});
		return items;
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
	}
});