/**
 * 模块角色列表
 * @author longfc
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.moduleoper.modulerole.Grid', {
	extend: 'Shell.ux.grid.Panel',
	//requires: ['Ext.ux.CheckColumn'],

	title: '模块角色列表 ',
	width: 800,
	height: 500,
	/**默认加载*/
	defaultLoad: false,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**是否启用刷新按钮*/
	hasRefresh: true,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**预置条件*/
	PreConditions: null,
	/**模块ID*/
	ModuleId: null,

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
	/**删除数据服务路径*/
	delUrl: '/RBACService.svc/RBAC_UDTO_DelRBACRoleModule',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push(me.createSetPreconditions());
		columns.push({
			text: '角色名称',
			dataIndex: 'RBACRoleModule_RBACRole_CName',
			width: 120
		}, {
			text: '角色编码',
			dataIndex: 'RBACRoleModule_RBACRole_UseCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '角色描述',
			dataIndex: 'RBACRoleModule_RBACRole_Comment',
			flex: 1,
			minWidth: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'RBACRoleModule_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '角色ID',
			dataIndex: 'RBACRoleModule_RBACRole_Id',
			hidden: true,
			hideable: false
		});

		return columns;
	},
	/**根据模块ID加载数据*/
	loadByModuleId: function(id) {
		var me = this;
		me.ModuleId = id;
		me.defaultWhere = 'rbacrolemodule.RBACModule.Id=' + me.ModuleId;
		me.onSearch();
	},
	/*创建行数据条件启用或禁用列**/
	createSetPreconditions: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '设置',
			align: 'center',
			width: 45,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>设置预置条件值</b>"';
					return 'button-edit hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					if(!me.PreConditions) {
						JcallShell.Msg.alert("预置条件为空", null, 1800)
					}
				}
			}]
		};
	}
});