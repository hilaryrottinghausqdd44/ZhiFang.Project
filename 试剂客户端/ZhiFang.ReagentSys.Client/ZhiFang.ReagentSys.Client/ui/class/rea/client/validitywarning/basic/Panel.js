/**
 * @description 效期预警
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.validitywarning.basic.Panel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '效期预警',
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
		me.QtyGrid = Ext.create('Shell.class.rea.client.validitywarning.basic.QtyGrid', {
			header: false,
			itemId: 'QtyGrid',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.QtyGrid];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		var me = this;
		me.PK = null;
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	/**加载库存数据*/
	loadData: function() {
		var me = this;
		me.QtyGrid.onSearch();
	}
});