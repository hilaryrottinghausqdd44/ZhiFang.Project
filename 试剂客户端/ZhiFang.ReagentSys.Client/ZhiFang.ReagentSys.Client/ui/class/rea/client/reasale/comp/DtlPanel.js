/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.comp.DtlPanel', {
	extend: 'Shell.class.rea.client.reasale.basic.DtlPanel',

	title: '供货信息',
	header: false,
	border: false,

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DtlGrid.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
				me.TabPanel.collapse();
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
				me.TabPanel.expand();
			},
			select: function(RowModel, record) {
				me.loadTabPanel(record);
			},
			onAddAfter: function(grid) {
				me.fireEvent('onAddAfter', grid);
			},
			onDelAfter: function(grid) {
				me.fireEvent('onDelAfter', grid);
			},
			onEditAfter: function(grid) {
				JShell.Action.delay(function() {
					me.fireEvent('onEditAfter', grid);
				}, null, 500);
			},
			nodata: function(grid) {
				me.DtlGrid.nodata();
				me.fireEvent('onDelAfter', grid);
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.reasale.comp.add.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.TabPanel = Ext.create('Shell.class.rea.client.reasale.comp.TabPanel', {
			header: false,
			itemId: 'TabPanel',
			region: 'east',
			width: 305,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});

		var appInfos = [me.DtlGrid, me.TabPanel];
		return appInfos;
	},
	/**加载供货明细列表*/
	loadDtl: function(record) {
		var me = this;
		me.callParent(arguments);

		var reaLab = {
			"ReaLabID": record.get("ReaBmsCenSaleDoc_LabcID"),
			"ReaLabCName": record.get("ReaBmsCenSaleDoc_LabcName"),
			"PlatformOrgNo": record.get("ReaBmsCenSaleDoc_ReaServerLabcCode")
		};
		me.DtlGrid.setReaLabInfo(reaLab);
	},
	/**加载tab页签内容*/
	loadTabPanel: function(record) {
		var me = this;
		me.TabPanel.expand();
		me.TabPanel.loadData(record);
	},
	/**表单的订货方选择后联动供货明细列表*/
	setReaLabInfo: function(reaLab) {
		var me = this;
		me.DtlGrid.setReaLabInfo(reaLab);
	},
	/**新增供货单时先清空订货方信息*/
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.DtlGrid.setReaLabInfo(null);
	}
});