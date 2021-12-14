/**
 * 记录项字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordtypeitem.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '记录项字典',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

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

		me.Grid.onSearch();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.Grid = Ext.create('Shell.class.sysbase.screcordtypeitem.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.screcordtypeitem.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			collapsible: false,
			width: 240
		});

		return [ me.Grid, me.Form];
	},
	loadForm: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.Grid.PKField);
			me.Form.isEdit(id);
		}, null, 500);
	}
});