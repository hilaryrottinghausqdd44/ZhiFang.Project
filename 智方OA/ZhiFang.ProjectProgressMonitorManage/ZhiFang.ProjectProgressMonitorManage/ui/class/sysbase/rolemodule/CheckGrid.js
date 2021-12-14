/**
 * 模块角色选择列表
 * @author longfc
 * @version 2017-06-21
 */
Ext.define('Shell.class.sysbase.rolemodule.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '模块角色选择列表',
	width: 270,
	height: 480,

	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'RBACRoleModule_RBACRole_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'rbacrolemodule.RBACRole.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '角色名称',
			isLike: true,
			fields: ['rbacrolemodule.RBACRole.CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '角色名称',
			dataIndex: 'RBACRoleModule_RBACRole_CName',
			flex: 1,
			minWidth: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '角色Id',
			dataIndex: 'RBACRoleModule_RBACRole_Id',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'RBACRoleModule_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});