/**
 * 预置条件项选择列表
 * @author longfc
 * @version 2017-08-22
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.copy.PreconditionsGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '预置条件项选择列表',
	width: 270,
	height: 480,

	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACPreconditionsByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'RBACPreconditions_DispOrder',
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
		me.defaultWhere += 'rbacpreconditions.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: "98%",
			emptyText: '预置条件名称',
			isLike: true,
			fields: ['rbacpreconditions.CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '所属模块服务',
			dataIndex: 'RBACPreconditions_RBACModuleOper_CName',
			flex: 1,
			minWidth: 200,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '预置条件',
			dataIndex: 'RBACPreconditions_CName',
			width: 90,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '实体编码',
			dataIndex: 'RBACPreconditions_EntityCode',
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'RBACPreconditions_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}];

		return columns;
	}
});