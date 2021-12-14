/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.DocForm', {
	extend: 'Shell.class.rea.client.confirm.basic.DocForm',

	title: '验货单信息',
	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?isPlanish=true',

	OTYPE: "reaorder",
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
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
	},
	createItems: function() {
		var me = this,
			items = me.callParent(arguments);
		//供货总单号隐藏
		//items[3].hidden = true;

		//		items.splice(3, 0, {
		//			fieldLabel: '订货单号',
		//			name: 'ReaBmsCenSaleDocConfirm_OrderDocNo',
		//			itemId: 'ReaBmsCenSaleDocConfirm_OrderDocNo',
		//			colspan: 2,
		//			width: me.defaults.width * 2,
		//			readOnly: true,
		//			locked: true
		//		});

		items.push({
			fieldLabel: '订单Id',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_OrderDocID',
			itemId: 'ReaBmsCenSaleDocConfirm_OrderDocID',
			readOnly: true,
			locked: true
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = me.callParent(arguments);
		entity.entity.OrderDocNo = values.ReaBmsCenSaleDocConfirm_OrderDocNo;
		var orderId = values.ReaBmsCenSaleDocConfirm_OrderDocID;
		if(orderId) {
			entity.entity.OrderDocID = orderId;
		}
		return entity;
	}
});