/**
 * 客户端订单维护
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.apply.EditPanel', {
	extend: 'Shell.class.rea.client.order.basic.EditPanel',

	title: '订单信息',
	header: false,
	border: false,

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
		var me = this;
		me.OrderDtlGrid = Ext.create('Shell.class.rea.client.order.apply.OrderDtlGrid', {
			header: false,
			itemId: 'OrderDtlGrid',
			region: 'center',
			collapsible: false,
			defaultLoad: false,
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
			OTYPE: "entry"
		});
		var appInfos = [me.OrderDtlGrid, me.DocForm];
		return appInfos;
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	}
});