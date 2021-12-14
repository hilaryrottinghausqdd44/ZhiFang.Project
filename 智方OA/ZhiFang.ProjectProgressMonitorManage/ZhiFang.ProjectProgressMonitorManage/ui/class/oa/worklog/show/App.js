/**
 * 日总结/周总结/月总结应用
 * @author longfc
 * @version 2016-09-27
 */
Ext.define('Shell.class.oa.worklog.show.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '日 / 周 / 月总结',

	/**对外公开:字典类型系统参数Id*/
	IDS: "",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	width: 800,
	initComponent: function() {
		var me = this;
		me.title = me.title || "日/周/月总结";
		me.IDS = me.IDS || "";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		var Tree = Ext.create("Shell.class.oa.worklog.show.Tree", {
			region: 'west',
			split: true,
			collapsible: true,
			IDS: me.IDS,
			header: false,
			title: "部门员工",
			width: 260,
			itemId: 'Tree'
		});
		me.GridTabPanel = Ext.create("Shell.class.oa.worklog.show.GridTabPanel", {
			region: 'center',
			header: false,
			title: me.title || "日/周/月总结信息",
			itemId: 'GridTabPanel'
		});
		return [Tree, me.GridTabPanel];
	},

	/*程序列表的事件监听**/
	listenersGrid: function() {
		var me = this;
		var Tree = me.getComponent('Tree');
		Tree.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				me.GridSearch(record);
			},
			select: function(RowModel, record) {
				me.GridSearch(record);
			},
			nodata: function(p) {
				me.clearData();
			}
		});
	},
	/*程序列表的事件监听**/
	GridSearch: function(record) {
		var me = this;
		var DeptEmpGrid = me.GridTabPanel.getComponent('DeptEmpGrid');
		var EmpGrid = me.GridTabPanel.getComponent('EmpGrid');

		JShell.Action.delay(function() {
			var id = record.get('tid');
			var leaf = record.get('leaf');
			var pid = record.raw.pid;
			switch(pid) {
				case "0":
					DeptEmpGrid.IsIncludeSubDept = true;
					break;
				default:
					DeptEmpGrid.IsIncludeSubDept = false;
					break;
			}
			switch(record.raw.objectType) {
				case "HRDept":
					DeptEmpGrid.DeptId = id;
					DeptEmpGrid.EMPID = "";

					DeptEmpGrid.onSearch();
					me.GridTabPanel.showTab(0);
					me.GridTabPanel.hideTab(1);
					EmpGrid.EMPID = "";
					break;
				case "HREmployee":
					DeptEmpGrid.DeptId = "";
					DeptEmpGrid.EMPID = "";

					EmpGrid.DeptId = "";
					EmpGrid.EMPID = id;
					EmpGrid.GridSearch();
					me.GridTabPanel.hideTab(0);
					me.GridTabPanel.showTab(1);
					break;
				default:
					break;
			}

		}, null, 300);
	}
});