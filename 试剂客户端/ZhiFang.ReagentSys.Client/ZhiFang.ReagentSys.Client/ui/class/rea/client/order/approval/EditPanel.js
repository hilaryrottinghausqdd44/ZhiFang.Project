/**
 * @description 订单审批
 * @author longfc
 * @version 2018-12-05
 */
Ext.define('Shell.class.rea.client.order.approval.EditPanel', {
	extend: 'Shell.class.rea.client.order.basic.EditPanel',

	title: '订单审批',
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
		me.OrderDtlGrid = Ext.create('Shell.class.rea.client.order.approval.OrderDtlGrid', {
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