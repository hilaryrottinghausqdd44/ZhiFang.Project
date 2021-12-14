/**
 * 角色权限的角色选择列表
 * @author longfc
 * @version 2017-06-22
 */
Ext.define('Shell.class.sysbase.rowfilter.roleright.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '角色选择列表',
	width: 280,
	height: 380,

	/**是否单选*/
	checkOne: true,
	/**默认加载数据*/
	defaultLoad: true,
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
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
			text: '名称',
			dataIndex: 'RBACRoleVO_CName',
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'RBACRoleVO_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});