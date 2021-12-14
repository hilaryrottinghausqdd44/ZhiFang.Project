/**
 * 条码模板设计
 * @author longfc
 * @version 2018-08-08
 */
Ext.define('Shell.class.rea.client.printbarcode.design.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '条码模板设计',

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
		me.DesinForm = Ext.create('Shell.class.rea.client.printbarcode.design.DesinForm', {
			header: false,
			width:me.width,
			itemId: 'DesinForm',
			region: 'center',
			split: false,
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.DesinForm];
		return appInfos;
	}
});