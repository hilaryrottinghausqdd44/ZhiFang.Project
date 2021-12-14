/**
 * 记录项类型字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordtype.App', {
	extend: 'Shell.ux.panel.AppPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '记录项类型字典',
	
	/**是否检验项目对照*/
	ISTESTITEM:true,
	
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
		me.Grid = Ext.create('Shell.class.sysbase.screcordtype.Grid', {
			region: 'center',
			header: false,
			/**是否检验项目对照*/
			ISTESTITEM:me.ISTESTITEM,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.screcordtype.Form', {
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
