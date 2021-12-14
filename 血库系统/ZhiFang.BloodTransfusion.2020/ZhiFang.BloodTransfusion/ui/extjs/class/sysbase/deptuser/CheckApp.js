/**
 * 科室人员选择
 * @author longfc
 * @version 2020-03-27
 */
Ext.define('Shell.class.sysbase.deptuser.CheckApp', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '科室人员选择',

	width: 680,
	height: 520,

	/**是否单选*/
	checkOne: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.DeptGrid.on({
			itemclick: function(v, record) {
				me.loadGrid(record);
			},
			select: function(RowModel, record) {
				me.loadGrid(record);
			},
			nodata: function(p) {
				me.Grid.clearData();
			}
		});

		me.Grid.on({
			accept: function(p, record) {
				me.fireEvent('accept', me, record);
			}
		});
		JShell.Action.delay(function() {
			//默认按当前科室
			var searchValue = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
			me.DeptGrid.onSearchClick(null,searchValue);
		}, null, 300);
	},

	initComponent: function() {
		var me = this;
		//me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		//查询栏默认值
		var searchValue = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		if (!searchValue) searchValue = "";
		me.DeptGrid = Ext.create('Shell.class.sysbase.department.SimpleGrid', {
			region: 'west',
			header: false,
			width: 280,
			split: true,
			collapsible: true,
			itemId: 'DeptGrid',
			searchValue: searchValue
		});
		me.Grid = Ext.create('Shell.class.sysbase.deptuser.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			checkOne: me.checkOne,
			/**默认加载*/
			defaultLoad: false
		});

		return [me.DeptGrid, me.Grid];
	},
	loadGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = "" + record.get(me.DeptGrid.PKField);
			me.Grid.DepartmentId = id;
			me.Grid.defaultWhere = 'departmentuser.Department.Id=' + id;
			me.Grid.onSearch();
		}, null, 500);
	}
});
