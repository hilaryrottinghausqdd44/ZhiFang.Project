/**
 * 系统运行参数
 * @author longfc
 * @version 2018-03-08
 */
Ext.define('Shell.class.rea.client.bparameters.runparams.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '系统运行参数',
	width: 800,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create("Shell.class.rea.client.bparameters.runparams.Grid", {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.rea.client.bparameters.runparams.Form', {
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
	onListeners: function() {
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
			onEditClick: function(grid, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
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