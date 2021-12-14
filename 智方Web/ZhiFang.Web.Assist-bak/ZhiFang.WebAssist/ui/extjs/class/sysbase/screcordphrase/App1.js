/**
 * 记录项类型短语维护
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordphrase.App1', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '记录项类型短语维护',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.TypeGrid.on({
			itemclick: function(v, record) {
				me.loadGrid(record);
			},
			select: function(RowModel, record) {
				me.loadGrid(record);
			},
			nodata: function(p) {
				me.Form.BObjectId = null;
				me.Grid.clearData();
				me.Form.disableControl();
			}
		});

		me.Grid.on({
			itemclick: function(v, record) {

			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.Form.isAdd();
			},
			nodata: function(p) {
				me.Form.clearData();
				me.Form.disableControl();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
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
			/**默认数据条件:过滤不良反应分类*/
			//defaultWhere: 'screcordtype.ContentTypeID!=2',
			itemId: 'TypeGrid'
		});
		me.Grid = Ext.create('Shell.class.sysbase.screcordphrase.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.screcordphrase.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			collapsible: false,
			width: 240
		});

		return [me.TypeGrid, me.Grid, me.Form];
	},
	loadGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.TypeGrid.PKField);
			me.Form.BObjectId = id;
			me.Grid.defaultWhere = 'screcordphrase.BObjectId=' + id;
			me.Grid.onSearch();
		}, null, 500);
	},
	loadForm: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.Grid.PKField);
			me.Form.isEdit(id);
		}, null, 500);
	}
});