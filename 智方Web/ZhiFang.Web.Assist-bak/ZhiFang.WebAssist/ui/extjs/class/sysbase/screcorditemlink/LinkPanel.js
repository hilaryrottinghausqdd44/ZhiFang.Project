/**
 * 记录项类型与记录项字典关系
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcorditemlink.LinkPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '记录项字典',
	RecordTypeID: "",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.LinkGrid.on({
			itemclick: function(v, record) {
				me.loadGrid(record);
			},
			select: function(RowModel, record) {
				me.loadGrid(record);
			},
			nodata: function(p) {

			}
		});

		me.LinkGrid.onSearch();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.LinkGrid = Ext.create('Shell.class.sysbase.screcorditemlink.LinkGrid', {
			region: 'center',
			header: false,
			itemId: 'LinkGrid'
		});
		me.PhraseGrid = Ext.create('Shell.class.sysbase.screcorditemlink.PhraseGrid', {
			region: 'south',
			split: true,
			collapsible: false,
			height: 240,
			itemId: 'PhraseGrid'
		});
		return [me.LinkGrid, me.PhraseGrid];
	},
	loadData: function(id) {
		var me = this;
		me.RecordTypeID = id;
		me.LinkGrid.RecordTypeID = id;
		me.LinkGrid.defaultWhere = 'screcorditemlink.SCRecordType.Id=' + id;
		me.LinkGrid.onSearch();
	},
	loadGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.LinkGrid.PKField);
			me.PhraseGrid.BObjectId = id;
			me.PhraseGrid.defaultWhere = 'screcordphrase.BObjectId=' + id;
			me.PhraseGrid.onSearch();
		}, null, 500);
	}
});
