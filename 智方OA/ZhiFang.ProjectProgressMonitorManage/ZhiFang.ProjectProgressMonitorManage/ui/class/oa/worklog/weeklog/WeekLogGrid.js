/**
 * 工作日志--我的周工作计划
 * @author longfc
 * @version 2016-08-01
 */
Ext.define('Shell.class.oa.worklog.weeklog.WeekLogGrid', {
	extend: 'Shell.class.oa.worklog.basic.Grid',
	title: '我的周工作计划',
	width: 360,
	height: 500,
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DataAddTime',
		direction: 'DESC'
	}],
	defaultPageSize: 15,
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
	hasWeeked: true,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	columnLines: true,
	/**查询对象*/
	objectEName: 'PWorkWeekLog',
	worklogtype: 'WorkLogWeek',
	
	/**日志外键名称
	 * @author Jcall
	 * @version 2016-08-19
	 */
	LogName:'WorkWeekLog',
	
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
			params = [],
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Year = buttonsToolbar.getComponent('Year').getValue(),
			Month = buttonsToolbar.getComponent('Month').getValue();
		params.push("&sendtype=" + me.sendtype);
		params.push("&worklogtype=" + me.worklogtype);
		
		var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(EmpID != "-1" && EmpID != "") {
			params.push("&empid=" + EmpID);
			params.push("&ownempid=" + EmpID);
		}
		if(me.hasWeeked) {
			var Week = buttonsToolbar.getComponent('Week').getValue();
			var weekdate = JcallShell.Date.getWeekStartDateAndEndDate(Year, Month, Week);
			if(weekdate && weekdate.StartDate && weekdate.EndDate) {
				params.push("&sd=" + weekdate.StartDate + "");
				params.push("&ed=" + weekdate.EndDate + "");
			}
		}
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
	
	/**初始化功能按钮栏内容
	 * @author Jcall
	 * @version 2016-08-18
	 */
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		
		//月
		me.buttonToolbarItems[1].listeners.change = function(com, newValue, oldValue, eOpts){
			if(newValue && newValue != null && newValue != "") {
				me.changeWeek();
				me.onSearch();
			}
		}
	},
	/**初始化送检时间
	 * @author Jcall
	 * @version 2016-08-18
	 */
	initDate:function(){
		var me =this,
			date = JShell.System.Date.getDate(),
			weeks = JShell.Date.getMonthTotalWeekByDate(date);
			
		me.YearValue = date.getFullYear();//年
		me.MonthValue = date.getMonth() + 1;//月
		me.WeekValue = JShell.Date.getMonthWeekByDate(date) + '';//周
		
		//周列表
		me.DateTimeList = [];
		for(var i=0;i<weeks;i++){
			me.DateTimeList.push([(1+i) + '','第' + (i+1) + '周']);
		}
	},
	/**更改周组件的数据
	 * @author Jcall
	 * @version 2016-08-18
	 */
	changeWeek:function(){
		var me =this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Year = buttonsToolbar.getComponent('Year'),
			Month = buttonsToolbar.getComponent('Month'),
			Week = buttonsToolbar.getComponent('Week');
			
		//周列表
		var DateTimeList = [];
		var weeks = JShell.Date.getMonthTotalWeekByYearMonth(Year.getValue(),Month.getValue());
		for(var i=0;i<weeks;i++){
			DateTimeList.push([(1+i) + '','第' + (i+1) + '周']);
		}
		
		Week.store.loadData(DateTimeList);
		Week.setValue('1');//默认第一周
	}
});