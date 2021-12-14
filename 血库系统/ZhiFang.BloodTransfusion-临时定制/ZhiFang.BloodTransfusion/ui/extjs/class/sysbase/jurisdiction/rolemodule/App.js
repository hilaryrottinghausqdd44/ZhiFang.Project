/**
 * 角色模块维护
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.rolemodule.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '角色模块维护',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Tree.loadByRoleId(id);
				}, null, 200);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Tree.loadByRoleId(id);
				}, null, 200);
			},
			nodata: function() {
				me.Tree.clearData();
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

		me.Grid = Ext.create('Shell.class.sysbase.role.SimpleGrid', {
			region: 'west',
			header: false,
			itemId: 'Grid',
			split: true,
			collapsible: true
		});
		me.Tree = Ext.create('Shell.class.sysbase.jurisdiction.rolemodule.TreeGrid', {
			region: 'center',
			header: false,
			itemId: 'Tree'
		});

		return [me.Grid, me.Tree];
	}
});
