/***
 * 预置条件配置
 * @author longfc
 * @version 2017-06-22
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.App', {
	extend: 'Ext.panel.Panel',

	title: '预置条件配置',
	layout: {
		type: 'border'
	},
	header: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var ModuleTree = me.getComponent("ModuleTree");
		me.ModuleTree.on({
			select: function(rowModel, record, index, eOpts) {
				me.RowFilterTree.moduleId = record.get("tid");
				me.loadModuleoperGrid(record.get("tid"));
			},
		});
		me.ModuleoperGrid.on({
			select: function(RowModel, record) {
				me.loadGridData(record);
			},
			nodata: function(p) {
				me.Grid.clearData();
			}
		});
		me.Grid.on({
			select: function(RowModel, record) {
				me.loadTreeData(record);
			},
			nodata: function(p) {
				me.clearTreeData();
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
		me.ModuleTree = Ext.create('Shell.class.sysbase.moduleoper.ModuleTree', {
			width: 220,
			header: true,
			itemId: 'ModuleTree',
			name: 'ModuleTree',
			title: '模块树',
			region: 'west',
			split: true
		});
		me.ModuleoperGrid = Ext.create('Shell.class.sysbase.rowfilter.precondition.ModuleoperGrid', {
			header: true,
			width: 240,
			itemId: 'ModuleoperGrid',
			region: 'west',
			split: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.rowfilter.precondition.Grid', {
			header: true,
			width: 380,
			itemId: 'Grid',
			region: 'west',
			split: true
		});
		me.RowFilterTree = Ext.create('Shell.class.sysbase.rowfilter.precondition.RowFilterTree', {
			region: 'center',
			split: true,
			itemId: 'RowFilterTree'
		});
		var appInfos = [me.ModuleTree,me.ModuleoperGrid, me.Grid, me.RowFilterTree];
		return appInfos;
	},
	loadModuleoperGrid: function(moduleId) {
		var me = this;
		if(!moduleId) return;
		setTimeout(function() {
			var hqlWhere = 'rbacmoduleoper.RBACModule.Id=' + moduleId;
			me.ModuleoperGrid.defaultWhere = hqlWhere;
			me.ModuleoperGrid.onSearch();
		}, null, 500);
	},
	loadGridData: function(record) {
		var me = this;
		var id=record.get("RBACModuleOper_Id");
		if(!id) return;
		setTimeout(function() {
			var hqlWhere = 'rbacpreconditions.RBACModuleOper.Id=' + id;
			me.Grid.defaultWhere = hqlWhere;
			me.Grid.onSearch();
		}, null, 500);
	},
	loadTreeData: function(record) {
		var me = this;
		if(!record) return;
		JShell.Action.delay(function() {
			var id = record.get(me.Grid.PKField);
			me.RowFilterTree.preconditionId = id;
			me.RowFilterTree.preconditionSelect = record;
			me.RowFilterTree.objectName = record.get("RBACPreconditions_EName");
			me.RowFilterTree.objectCName = record.get("RBACPreconditions_CName");
			me.RowFilterTree.load(id);
		}, null, 500);
	},
	clearTreeData: function() {
		var me = this;
		me.RowFilterTree.preconditionId = null;
		me.RowFilterTree.preconditionSelect = null;
		me.RowFilterTree.objectName = null;
		me.RowFilterTree.objectCName = null;
		var root = me.RowFilterTree.load("");
	}
});