/**
 * 用户角色维护
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.userrole.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '用户角色维护',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.DeptGrid.on({
			itemclick: function(v, record) {
				me.loadDeptUserGrid(record);
			},
			select: function(RowModel, record) {
				me.loadDeptUserGrid(record);
			}
		});

		me.DeptUserGrid.on({
			itemclick: function(v, record) {
				me.loadRoleCheckGrid(record);
			},
			select: function(RowModel, record) {
				me.loadRoleCheckGrid(record);
			},
			nodata: function() {
				me.RoleCheckGrid.clearData();
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
		me.DeptGrid = Ext.create('Shell.class.sysbase.department.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			/**默认加载*/
			defaultLoad: true,
			itemId: 'DeptGrid',
			width: 320
		});
		me.DeptUserGrid = Ext.create('Shell.class.sysbase.jurisdiction.userrole.DeptUserGrid', {
			region: 'center',
			header: false,
			itemId: 'DeptUserGrid'
		});
		me.RoleCheckGrid = Ext.create('Shell.class.sysbase.jurisdiction.userrole.RoleCheckGrid', {
			region: 'east',
			header: false,
			itemId: 'RoleCheckGrid',
			split: true,
			collapsible: true
		});

		return [me.DeptGrid, me.DeptUserGrid, me.RoleCheckGrid];
	},
	loadDeptUserGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = ""+record.get(me.DeptGrid.PKField);
			me.DeptUserGrid.DepartmentId = id;
			me.DeptUserGrid.defaultWhere = 'departmentuser.Department.Id=' + id;
			me.DeptUserGrid.onSearch();
		}, null, 500);
	},
	loadRoleCheckGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get("DepartmentUser_PUser_Id");
			me.RoleCheckGrid.UserId=id;
			me.RoleCheckGrid.loadLinkByUserId(id);
		}, null, 500);
	}
});
