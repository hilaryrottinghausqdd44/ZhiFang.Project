/**
 * 模块服务选择列表
 * @author longfc
 * @version 2017-06-21
 */
Ext.define('Shell.class.sysbase.moduleoper.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '模块服务选择列表',
	width: 270,
	height: 480,

	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'RBACModuleOper_DispOrder',
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
		me.defaultWhere += 'rbacmoduleoper.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '服务名称',
			isLike: true,
			fields: ['rbacmoduleoper.ServiceURLCName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '模块服务',
			dataIndex: 'RBACModuleOper_CName',
			flex:1,
			minWidth: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '模块服务URL',
			dataIndex: 'RBACModuleOper_ServiceURLEName',
			width: 100,
			hidden:true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '数据对象',
			dataIndex: 'RBACModuleOper_RowFilterBase',
			width: 100,
			hidden:true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '数据对象中文名称',
			dataIndex: 'RBACModuleOper_RowFilterBaseCName',
			width: 100,
			hidden:true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '主键ID',
			dataIndex: 'RBACModuleOper_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];

		return columns;
	}
});