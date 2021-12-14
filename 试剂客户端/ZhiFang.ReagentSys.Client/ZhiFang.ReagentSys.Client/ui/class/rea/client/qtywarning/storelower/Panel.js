/**
 * @description 低库存预警
 * @author longfc
 * @version 2018-03-23
 */
Ext.define('Shell.class.rea.client.qtywarning.storelower.Panel', {
	extend: 'Shell.class.rea.client.qtywarning.basic.Panel',

	title: '低库存预警',
	header: false,
	border: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.QtyGrid = Ext.create('Shell.class.rea.client.qtywarning.storelower.QtyGrid', {
			header: false,
			itemId: 'QtyGrid',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.QtyGrid];
		return appInfos;
	}
});