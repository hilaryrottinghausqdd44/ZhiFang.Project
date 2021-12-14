/**
 * 按机构货品最大包装单位进行综合统计
 * @author longfc
 * @version 2018-09-11
 */
Ext.define('Shell.class.rea.client.statistics.complex1.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title:'按机构货品综合统计',
	
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.statistics.complex1.DtlGrid', {
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