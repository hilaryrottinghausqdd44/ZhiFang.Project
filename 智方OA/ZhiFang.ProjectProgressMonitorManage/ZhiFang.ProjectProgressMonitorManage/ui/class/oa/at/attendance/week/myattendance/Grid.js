/**
 * 我的周考勤
 * @author 
 * @version 2016-07-22
 */
Ext.define('Shell.class.oa.at.attendance.week.myattendance.Grid', {
	extend:'Shell.class.oa.at.attendance.week.Grid',
	title:'我的周考勤',
	/**查询栏包含部门选项*/
	hasHRDept: false,
	/**部门列显示*/
	hiddenDept: false,
	/**姓名列显示*/
	hiddenCName: false,
	Type:0
//	defaultWhere:' and (atemplistweeklog.EmpId =' + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)+')'
});