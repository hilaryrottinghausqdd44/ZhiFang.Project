/**
 * 部门员工月考勤
 * @author longfc
 * @version 2016-07-27
 */
Ext.define('Shell.class.oa.at.attendance.monthlog.DeptOfEmpMonthLogApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '部门员工月考勤',
	/**登录员工*/
	EMPID:'',
//	EMPID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
	/**月考勤类型:dept:为部门考勤,all:为公司考勤*/
	TYPE: 'dept',
	/**查询月份*/
	MONTTCODE: JShell.Date.getDate(new Date()).getMonth() + 1,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.UserGrid.getStore().on('load', function() {
			me.UserGrid.mergeCells(me.UserGrid, [1]);
		});
		me.UserGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.UserGrid.PKField);
					var HREmployeeId = record.get("HREmployee_Id");
					me.EmpMonthLogGrid.EMPID = HREmployeeId;
					me.EmpMonthLogGrid.load();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.UserGrid.PKField);
					var HREmployeeId = record.get("HREmployee_Id");
					me.EmpMonthLogGrid.EMPID = HREmployeeId;
					me.EmpMonthLogGrid.load();
				}, null, 500);
			},
			nodata: function() {
				me.EmpMonthLogGrid.EMPID = "";
				me.EmpMonthLogGrid.clearData();
			}
		});
		JShell.Action.delay(function() {
			var externalWhere = "";
			switch(me.TYPE) {
				case 'dept': //登录者下的和管理部门
				    externalWhere = "1=1";
					break;
				case 'all':
				    externalWhere = "1=1 and IsUse=true";
					break;
				default:
					break;
			}
			if(externalWhere != "" && externalWhere != null) {
				me.UserGrid.load(externalWhere);
			}
		}, null, 500);
	},

	initComponent: function() {
		var me = this;
	   me.EMPID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
	    var me = this;

		me.UserGrid = Ext.create('Shell.class.oa.at.attendance.monthlog.UserGrid', {
			region: 'west',
			width: 280,
			header: false,
			itemId: 'UserGrid',
			split: true,
			collapsible: false,
		    datarangetype:me.TYPE
		});
		me.EmpMonthLogGrid = Ext.create('Shell.class.oa.at.attendance.monthlog.EmpMonthLogGrid', {
			region: 'center',
			header: false,
			/**不加载时默认禁用功能按钮*/
			defaultDisableControl: true,
			/**默认加载数据*/
			defaultLoad: false,
			hasRefresh: false,
			MONTTCODE: me.MONTTCODE,
			itemId: 'EmpMonthLogGrid',
			datarangetype:me.TYPE
		});
		return [me.UserGrid, me.EmpMonthLogGrid];
	}
});