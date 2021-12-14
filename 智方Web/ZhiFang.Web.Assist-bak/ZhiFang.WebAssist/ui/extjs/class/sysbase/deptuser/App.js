/**
 * 科室人员维护
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.deptuser.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '科室人员维护',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		/**/
		me.DeptGrid.on({
			itemclick: function(v, record) {
				me.loadGridByDeptGrid(record);
			},
			select: function(RowModel, record) {
				me.loadGridByDeptGrid(record);
			},
			nodata: function(p) {
				me.Grid.clearData();
			}
		});
		/* me.Tree.on({
			itemclick:function(v, record) {
				me.loadGridByDeptTree(record);
			},
			select:function(RowModel, record){
				me.loadGridByDeptTree(record);
			}
		}); */
		me.Grid.on({
			itemclick: function(v, record) {

			},
			nodata: function(p) {}
		});
		me.DeptGrid.onSearch();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.DeptGrid = Ext.create('Shell.class.sysbase.department.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'DeptGrid',
			width: 320
		});
		/* me.Tree = Ext.create('Shell.class.sysbase.department.Tree', {
			region: 'west',
			width: 280,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		}); */
		me.Grid = Ext.create('Shell.class.sysbase.deptuser.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.DeptGrid, me.Grid];
	},
	loadGridByDeptTree: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get('tid');
			var name = record.get('text');
			
			me.Grid.DepartmentId = id;
			me.Grid.defaultWhere = 'departmentuser.Department.Id=' + id;
			me.Grid.onSearch();
		}, null, 500);
	},
	loadGridByDeptGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = "" + record.get(me.DeptGrid.PKField);
			me.Grid.DepartmentId = id;
			me.Grid.defaultWhere = 'departmentuser.Department.Id=' + id;
			me.Grid.onSearch();
		}, null, 500);
	}
});
