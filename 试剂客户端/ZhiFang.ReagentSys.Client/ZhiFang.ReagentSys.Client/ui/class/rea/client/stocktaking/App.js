/**
 * 盘库管理
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.stocktaking.App', {
	//extend: 'Shell.ux.panel.AppPanel',
	extend: 'Ext.panel.Panel',

	layout: 'border',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocGrid.on({
			onAddClick: function() {
				me.isAdd();
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.TabPanel.on({
			save: function(p, id) {
				//id为盘库单Id
				me.DocGrid.autoSelect = id;				
				me.DocGrid.expand();
				JShell.Action.delay(function() {	
					me.DocGrid.onSearch();
				}, null, 200);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.stocktaking.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		me.TabPanel = Ext.create('Shell.class.rea.client.stocktaking.TabPanel', {
			header: false,
			itemId: 'TabPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.TabPanel];
		return appInfos;
	},
	clearData: function() {
		var me = this;
	},
	nodata: function() {
		var me = this;
		me.TabPanel.nodata();
		me.clearData();
	},
	loadData: function(record) {
		var me = this;
		me.TabPanel.loadData(record);
	},
	isAdd: function() {
		var me = this;
		me.DocGrid.collapse();
		me.TabPanel.isAdd();
	}
});