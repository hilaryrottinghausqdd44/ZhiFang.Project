/**
 * 模块角色列表
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.modulerole.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],

	title: '模块角色列表 ',
	width: 800,
	height: 500,

	/**模块ID*/
	ModuleId: null,

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
	/**删除数据服务路径*/
	delUrl: '/RBACService.svc/RBAC_UDTO_DelRBACRoleModule',

	/**是否默认勾选*/
	autoSelect: false,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[500, 500]
	],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	/**序号列宽度*/
	rowNumbererWidth: 40,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: true,

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

		var columns = [{
			text: '角色名称',
			dataIndex: 'RBACRoleModule_RBACRole_CName',
			width: 80
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
			width: 200,
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
		}];

		return columns;
	},

	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.sysbase.role.CheckGrid', {
			resizable: false,
			title: '角色选择',
			checkOne: false,
			listeners: {
				accept: function(p, records) {
					me.onAddLink(p, records);
				}
			}
		}).show();
	},

	/**根据模块ID加载数据*/
	loadByModuleId: function(id) {
		var me = this;
		me.ModuleId = id;
		me.defaultWhere = 'rbacrolemodule.RBACModule.Id=' + me.ModuleId;
		me.onSearch();
	},

	/**保存关系*/
	onAddLink: function(p, records) {
		var me = this,
			len = records.length,
			ids = [];

		for (var i = 0; i < len; i++) {
			var roleId = records[i].get('RBACRole_Id');
			var rec = me.store.findRecord('RBACRoleModule_RBACRole_Id', roleId);
			if (!rec) {
				ids.push(roleId);
			}
		}

		//勾选的角色已在关系中，不需要保存
		if (ids.length == 0) {
			p.close();
			return;
		}

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = ids.length;

		for (var i in ids) {
			var roleId = ids[i];
			me.addLink(p, roleId);
		}
	},
	/**添加一条关系*/
	addLink: function(p, roleId) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.addUrl;
		var entity = {
			IsUse: true,
			RBACModule: {
				Id: me.ModuleId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			},
			RBACRole: {
				Id: roleId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		};
		JShell.Server.post(url, Ext.JSON.encode({
			entity: entity
		}), function(data) {
			if (data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}
			me.onSaveEnd(p);
		});
	},
	onSaveEnd: function(win) {
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			win.close();
			me.hideMask(); //隐藏遮罩层
			if (me.saveErrorCount == 0) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
			} else {
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
			me.onSearch();
		}
	}
});
