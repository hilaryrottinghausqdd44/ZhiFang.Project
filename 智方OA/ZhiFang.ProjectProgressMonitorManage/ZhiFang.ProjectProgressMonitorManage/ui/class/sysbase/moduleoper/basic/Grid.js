/***
 * 模块服务管理
 * @author longfc
 * @version 2017-05-17
 */
Ext.define('Shell.class.sysbase.moduleoper.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '模块服务管理',

	autoSelect: true,
	/**是否启用序号列*/
	hasRownumberer: false,
	hasAdd: false,
	hasEdit: false,
	hasSave: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,

	/**是否启用查询框*/
	hasSearch: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
	hiddenCName:true,
	defaultOrderBy: [{
		property: 'RBACModuleOper_DispOrder',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('udateUseRowFilter');
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '模块服务名称',
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
			text: '模块服务名称',
			dataIndex: 'RBACModuleOper_CName',
			flex: 1,
			minWidth: 100,
			sortable: false,
			hideable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var useRowFilter = "" + record.get('RBACModuleOper_UseRowFilter');
				if(useRowFilter == "false" || useRowFilter == "0")
					meta.style = 'background:red;color:#FFF;';
				return value;
			}
		},{
			text: '模块操作主键ID',
			dataIndex: 'RBACModuleOper_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		},{
			text: '是否采用数据过滤条件',
			dataIndex: 'RBACModuleOper_UseRowFilter',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**根据模块ID加载数据*/
	loadByModuleId: function(id) {
		var me = this;
		var hqlWhere = 'rbacmoduleoper.RBACModule.Id=' + id;
		me.defaultWhere = hqlWhere;
		me.onSearch();
	}
});