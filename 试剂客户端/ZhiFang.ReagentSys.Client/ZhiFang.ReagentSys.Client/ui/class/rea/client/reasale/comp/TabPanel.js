/**
 * 供货商供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.comp.TabPanel', {
	extend: 'Shell.class.rea.client.reasale.basic.TabPanel',

	title: '货品信息',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.activeTab = 0;
	}
});