/**
 * 工作日志--我的月工作计划
 * @author longfc
 * @version 2016-08-01
 */
Ext.define('Shell.class.oa.worklog.monthlog.MonthLogGrid', {
	extend: 'Shell.class.oa.worklog.basic.Grid',
	title: '我的月工作计划',
	width: 360,
	height: 500,
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DataAddTime',
		direction: 'DESC'
	}],
	defaultPageSize: 50,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,

	hasPagingtoolbar: true,
	/**周默认值*/
	WeekValue: '1',
	hasRownumberer: false,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**查询栏包含周选项*/
	hasWeeked: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	columnLines: true,
	/**查询对象*/
	objectEName: 'PWorkMonthLog',
	worklogtype: 'WorkLogMonth',
	sendtype: 'MEOWN',
	
	/**日志外键名称
	 * @author Jcall
	 * @version 2016-08-19
	 */
	LogName:'WorkMonthLog',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取带查询条件*/
	getSearchWhereParams: function() {
		var me = this,
			whereParams = "",
			EndDate = "",
			params = [],
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Year = buttonsToolbar.getComponent('Year').getValue(),
			Month = buttonsToolbar.getComponent('Month').getValue();
		var BeginDate = Year + '-' + Month + '-01';
		EndDate = JcallShell.Date.getMonthLastDate(Year, Month);
		params.push("&sendtype=" + me.sendtype);
		params.push("&worklogtype=" + me.worklogtype);
		//params.push("&ownempid=" + me.ownempid);
		if(BeginDate && EndDate) {
			params.push("&sd=" + JShell.Date.toString(BeginDate, true) + "");
			params.push("&ed=" + JShell.Date.toString(JShell.Date.getNextDate(EndDate,0), true) + "");
		}

		var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(EmpID != "-1" && EmpID != "") {
			params.push("&empid=" + EmpID);
			params.push("&ownempid=" + EmpID);
		}
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	}
});