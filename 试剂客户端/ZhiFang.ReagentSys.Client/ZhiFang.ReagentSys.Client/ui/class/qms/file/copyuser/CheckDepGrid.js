/**
 * 部门列表
 * @author 
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.file.copyuser.CheckDepGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '部门选择列表',
	width: 295,
	height: 500,
	checkOne: false,
	PKCheckField: 'RBACRole_Id',
	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true',
	initComponent: function() {
		var me = this;
		//查询框信息me.searchInfo||
		me.searchInfo = {
			width: 170,
			emptyText: '部门名称',
			isLike: true,
			fields: ['hrdept.CName']
		};
		//数据列
		me.columns = [{
			dataIndex: 'HRDept_CName',
			text: '部门名称',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'HRDept_Id',
			text: '部门ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'HRDept_DataTimeStamp',
			text: '部门时间戳',
			hidden: true,
			hideable: false
		}];
		me.fireEvent('load', me);
		me.callParent(arguments);
	}
});