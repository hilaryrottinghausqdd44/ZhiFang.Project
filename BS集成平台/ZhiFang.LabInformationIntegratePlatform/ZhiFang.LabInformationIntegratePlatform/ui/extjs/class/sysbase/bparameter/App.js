/**
 * 系统参数应用
 * @author longfc
 * @version 2016-09-27
 */
Ext.define('Shell.class.sysbase.bparameter.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '系统参数',

	width: 800,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create("Shell.class.sysbase.bparameter.Grid", {
			region: 'center',
			header: false,
			itemId: 'Grid',
			defaultWhere:" 1=1 "
		});
		me.Form = Ext.create('Shell.class.sysbase.bparameter.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			width: 380,
			collapsible: false
		});
		return [me.Grid, me.Form];
	},
	/*程序列表的事件监听**/
	listenersGrid: function() {
		var me = this;

		me.Grid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
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
			onAddClick: function() {
				me.Form.formtype = "add";
				me.Form.isAdd();
			},
			onEditClick: function(grid, record) {
				var id = record.get(me.Grid.PKField);
				me.Form.isEdit(id);
			},
			nodata: function(p) {
				me.Form.getForm().reset();
				me.Form.disableControl();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			}
		});
	}
});