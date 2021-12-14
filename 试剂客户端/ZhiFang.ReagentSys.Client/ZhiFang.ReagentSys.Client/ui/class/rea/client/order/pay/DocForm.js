/**
 * @description 订单付款
 * @author longfc
 * @version 2019-01-03
 */
Ext.define('Shell.class.rea.client.order.pay.DocForm', {
	extend: 'Shell.class.rea.client.order.basic.DocForm',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '订单信息',

	width: 640,
	height: 300,

	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = me.callParent(arguments);
		//供货商确认
		items.push({
			xtype: 'displayfield',
			fieldLabel: '付款人',
			name: 'ReaBmsCenOrderDoc_PayUserCName',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'displayfield',
			fieldLabel: '付款日期',
			name: 'ReaBmsCenOrderDoc_PayTime',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '付款备注',
			itemId: 'ReaBmsCenOrderDoc_PayMemo',
			name: 'ReaBmsCenOrderDoc_PayMemo',
			colspan: 2,
			width: me.defaults.width * 2
		});
		return items;
	}
});