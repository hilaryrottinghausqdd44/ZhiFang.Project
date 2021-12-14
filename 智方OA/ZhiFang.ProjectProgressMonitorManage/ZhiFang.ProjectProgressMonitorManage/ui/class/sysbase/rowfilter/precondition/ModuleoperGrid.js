/***
 * 模块服务信息
 * @author longfc
 * @version 2017-05-02
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.ModuleoperGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '模块服务信息',


	selectIndex: 0,
	autoSelect: true,
	autoScroll: true,
	sortableColumns: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,

	/**是否启用查询框*/
	hasSearch: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
	editUrl: JShell.System.Path.ROOT + '/' + 'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField',
	defaultOrderBy: [{
		property: 'RBACModuleOper_UseRowFilter',
		direction: 'DESC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || "";
		if(me.defaultWhere) {
			me.defaultWhere += " and rbacmoduleoper.IsUse=1";
		} else {
			me.defaultWhere = "rbacmoduleoper.IsUse=1";
		}
		me.addEvents('udateUseRowFilter');
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '模块操作名称',
			isLike: true,
			fields: ['rbacmoduleoper.CName']
		};
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			text: '模块服务',
			dataIndex: 'RBACModuleOper_CName',
			flex: 1,
			minWidth: 180,
			sortable: false,
			hideable: true
		},  {
			text: '默认数据过滤条件ID',
			dataIndex: 'RBACModuleOper_RBACRowFilter_Id',
			width: 20,
			sortable: false,
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			text: '数据对象',
			dataIndex: 'RBACModuleOper_RowFilterBase',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '数据对象中文名称',
			dataIndex: 'RBACModuleOper_RowFilterBaseCName',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '模块操作主键ID',
			dataIndex: 'RBACModuleOper_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '模块名称',
			dataIndex: 'RBACModuleOper_RBACModule_CName',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '模块主键ID',
			dataIndex: 'RBACModuleOper_RBACModule_Id',
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}];
		return columns;
	}
});