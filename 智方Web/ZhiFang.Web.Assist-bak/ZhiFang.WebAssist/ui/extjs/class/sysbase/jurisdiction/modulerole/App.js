/**
 * 模块角色维护
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.modulerole.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '模块角色维护',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Tree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.Grid.loadByModuleId(id);
				}, null, 200);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.Grid.loadByModuleId(id);
				}, null, 200);
			},
			nodata: function() {
				me.Grid.clearData();
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

		me.Tree = Ext.create('Shell.class.sysbase.module.Tree', {
			region: 'west',
			header: false,
			itemId: 'Tree',
			rootVisible: false, //是否显示根节点
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.jurisdiction.modulerole.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});

		return [me.Tree, me.Grid];
	}
});
