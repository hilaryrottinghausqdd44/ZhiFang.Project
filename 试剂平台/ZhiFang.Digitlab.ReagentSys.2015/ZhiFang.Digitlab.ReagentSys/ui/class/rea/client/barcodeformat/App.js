/**
 * 供货方条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.App', {
	extend: 'Ext.panel.Panel',

	title: '供货方条码规则维护',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.CenOrgGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.loadGridData(record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.loadGridData(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.Grid.defaultWhere = "";
				me.Grid.PlatformOrgNo = null;
				me.Grid.clearData();
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
		me.CenOrgGrid = Ext.create('Shell.class.rea.client.barcodeformat.CenOrgGrid', {
			header: false,
			itemId: 'CenOrgGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.Grid = Ext.create('Shell.class.rea.client.barcodeformat.Grid', {
			header: false,
			itemId: 'Grid',
			region: 'center',
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.CenOrgGrid, me.Grid];
		return appInfos;
	},
	loadGridData: function(record) {
		var me = this;
		var orgNo = record.get("ReaCenOrg_PlatformOrgNo");
		me.Grid.PlatformOrgNo = orgNo;
		me.Grid.defaultWhere = "reacenbarcodeformat.PlatformOrgNo=" + orgNo;
		me.Grid.onSearch();
	}
});