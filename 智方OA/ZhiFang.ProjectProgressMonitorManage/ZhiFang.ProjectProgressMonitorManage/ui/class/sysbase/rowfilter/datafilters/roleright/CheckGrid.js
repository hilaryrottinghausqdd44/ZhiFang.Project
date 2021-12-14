/**
 * 角色权限的角色选择列表
 * @author longfc
 * @version 2017-05-04
 */
Ext.define('Shell.class.sysbase.rowfilter.datafilters.roleright.CheckGrid', {
	extend: 'Shell.class.sysbase.rowfilter.roleright.CheckGrid',
	title: '角色选择列表',
	width: 280,
	height: 380,
	
	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByModuleIdAndModuleOperId?isPlanish=true',
	/**模块ID*/
	moduleId: null,
	//模块操作Id
	moduleOperId: null,
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'rbacrolemodule.RBACRole.IsUse=1';
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;		
		var url=me.callParent(arguments);
		url +=("&moduleId="+me.moduleId+"&moduleOperId="+me.moduleOperId);
		return url;
	}
});