/**
 * 记录项短语维护
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordphrase.App', {
	extend: 'Shell.ux.panel.AppPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '记录项短语维护',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
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
		return [me.Grid, me.Form];
	}
});
