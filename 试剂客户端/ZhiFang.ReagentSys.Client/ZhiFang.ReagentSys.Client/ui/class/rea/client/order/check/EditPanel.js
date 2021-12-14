/**
 * @description 订单审核
 * @author longfc
 * @version 2017-11-17
 */
Ext.define('Shell.class.rea.client.order.check.EditPanel', {
	extend: 'Shell.class.rea.client.order.basic.EditPanel',

	title: '订单审核',
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
		me.OrderDtlGrid = Ext.create('Shell.class.rea.client.order.check.OrderDtlGrid', {
			header: false,
			itemId: 'OrderDtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype
		});
		me.DocForm = Ext.create('Shell.class.rea.client.order.basic.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width:me.width,
			height: 220,
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