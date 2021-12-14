/**
 * 记录项类型与记录项字典关系
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcorditemlink.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '记录项字典',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.TypeGrid.on({
			itemclick: function(v, record) {
				me.loadData(record);
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				
			}
		});

		me.TypeGrid.onSearch();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.TypeGrid = Ext.create('Shell.class.sysbase.screcordtype.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'TypeGrid'
		});
		me.LinkPanel = Ext.create('Shell.class.sysbase.screcorditemlink.LinkPanel', {
			region: 'center',
			header: false,
			itemId: 'LinkPanel'
		});
		return [me.TypeGrid, me.LinkPanel];
	},
	loadData: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.TypeGrid.PKField);
			me.LinkPanel.RecordTypeID=id;
			me.LinkPanel.loadData(id);
		}, null, 500);
	}
});