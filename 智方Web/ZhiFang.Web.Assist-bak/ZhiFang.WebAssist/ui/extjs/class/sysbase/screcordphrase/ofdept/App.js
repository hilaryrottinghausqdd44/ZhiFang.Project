/**
 * 记录项结语-按科室
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordphrase.ofdept.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '记录项结语-按科室',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.DeptGrid.on({
			itemclick: function(v, record) {
				me.loadData(record);
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.CenterPanel.clearData();
			}
		});

		me.DeptGrid.onSearch();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		
		me.DeptGrid = Ext.create('Shell.class.sysbase.department.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'DeptGrid'
		});
		
		me.CenterPanel = Ext.create('Shell.class.sysbase.screcordphrase.ofdept.CenterPanel', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.DeptGrid, me.CenterPanel];
	},
	loadData: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.DeptGrid.PKField);
			me.CenterPanel.DeptId = id;
			me.CenterPanel.loadData(id);
		}, null, 500);
	}
});