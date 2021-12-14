/**
 * 部门
 * @author 
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.file.copyuser.CheckRoleGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '角色选择列表',
	width: 295,
	height: 500,
	checkOne: false,
	PKCheckField: 'RBACRole_Id',
	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true',
	initComponent: function() {
		var me = this;
		//查询框信息me.searchInfo||
		me.searchInfo = {
			width: 170,
			emptyText: '角色名称',
			isLike: true,
			fields: ['btestitem.CName']
		};
		//数据列
		me.columns = [{
			dataIndex: 'RBACRole_CName',
			text: '角色名称',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'RBACRole_Id',
			text: '角色ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'RBACRole_DataTimeStamp',
			text: '角色时间戳',
			hidden: true,
			hideable: false
		}];
		me.fireEvent('load', me);
		me.callParent(arguments);
	}
});