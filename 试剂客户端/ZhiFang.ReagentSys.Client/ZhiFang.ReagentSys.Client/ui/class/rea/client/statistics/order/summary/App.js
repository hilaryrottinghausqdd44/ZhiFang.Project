/**
 * 订单汇总统计
 * @author longfc
 * @version 2018-09-10
 */
Ext.define('Shell.class.rea.client.statistics.order.summary.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '订单汇总统计',
	
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.statistics.order.summary.DtlGrid', {
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