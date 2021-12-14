/**
 * 角色员工选择列表
 * @author 
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.file.copyuser.CheckEmployeeRoleGrid', {
	extend: 'Shell.class.sysbase.user.role.CheckGrid',
	title: '员工选择列表',
	width: 425,
	height: 485,
	RoleHREmployeeCName: "",
	checkOne: false,
	PKCheckField: 'RBACEmpRoles_HREmployee_Id',
	defaultWhere:'',
	initComponent: function() {
		var me = this;
		me.searchInfoWidth=me.searchInfoWidth||"70%";
		me.defaultWhere=me.defaultWhere||"";
		me.RoleHREmployeeCName = me.RoleHREmployeeCName || "";
		me.checkOne = me.checkOne || false;
		me.fireEvent('load', me);
		me.callParent(arguments);
	}
});