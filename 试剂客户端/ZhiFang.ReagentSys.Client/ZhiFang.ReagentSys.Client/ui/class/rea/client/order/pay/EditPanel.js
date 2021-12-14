/**
 * @description 订单付款
 * @author longfc
 * @version 2019-01-03
 */
Ext.define('Shell.class.rea.client.order.pay.EditPanel', {
	extend: 'Shell.class.rea.client.order.basic.EditPanel',

	title: '订单付款',
	header: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.OrderDtlGrid.on({
			nodata: function(p) {
				me.OrderDtlGrid.enableControl();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.formtype = me.formtype || "show";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OrderDtlGrid = Ext.create('Shell.class.rea.client.order.show.OrderDtlGrid', {
			header: false,
			itemId: 'OrderDtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype,
			/**用户UI配置Key*/
			userUIKey: 'order.pay.OrderDtlGrid',
			/**用户UI配置Name*/
			userUIName: "订单付款明细列表"
		});
		me.DocForm = Ext.create('Shell.class.rea.client.order.pay.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 245,
			split: false,
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: "check"
		});
		var appInfos = [me.OrderDtlGrid, me.DocForm];
		return appInfos;
	}
});