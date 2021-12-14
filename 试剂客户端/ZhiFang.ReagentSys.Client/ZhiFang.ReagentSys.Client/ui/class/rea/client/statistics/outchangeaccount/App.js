/**
 * 试剂出库变更台账
 * @author zhaoqi
 * @version 2020-11-18
 */
Ext.define('Shell.class.rea.client.statistics.outchangeaccount.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '试剂出库变更台账',
	
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.statistics.outchangeaccount.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DtlGrid];
		return appInfos;
	}
});