/***
 * 数据过滤条件角色树
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.RowFilterTree', {
	extend: 'Shell.class.sysbase.rowfilter.basic.RowFilterTree',

	title: '数据过滤条件角色树',

	/**模块ID*/
	moduleId: null,
	//预置条件Id
	preconditionId: null,
	//预置条件选中行记录
	preconditionSelect: null,

	objectName: null,
	objectCName: null,
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_RJ_SearchRBACRowFilterTreeByPreconditionsId',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				if(record) {}
			},
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				if(record && record != null) {
					var objectType = '' + record.get('objectType');
					//角色节点
					if(objectType == 'RBACRole') {
						var tid = record.get('tid');
						var roleCName = record.get('text');
						var hqlWhere = 'rbacemproles.RBACRole.Id=' + tid;
						me.openRoleListShowWin(hqlWhere, roleCName);
					} else if(objectType == 'RBACRowFilter') {
						me.openAppEditWin('edit', record);
					}
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			text: '新增过滤条件',
			itemId: 'btnAddDatafilters',
			name: 'btnAddDatafilters',
			//cls: "btn btn-default btn-sm active",
			style: {
				marginLeft: '10px'
			},
			listeners: {
				click: function(com, e, eOpts) {
					me.openAppEditWin('add', null);
				}
			}
		});
		items.push({
			xtype: 'button',
			text: '行数据条件复制拷贝',
			itemId: 'btnCopy',
			iconCls: 'build-button-add',
			listeners: {
				click: function(com, e, eOpts) {
					if(me.preconditionSelect) {
						me.onCopyClick();
					} else {
						JShell.Msg.error(" 请在预置条件列表选择预置条件行后再操作!");
						return;
					}
				}
			}
		});
		return {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar2',
			items: items
		};
	},
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = [];
		items.push(me.createtoolsAddFilter());
		items.push({
			iconCls: 'button-refresh',
			itemId: 'refresh',
			tooltip: '刷新数据',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			iconCls: 'button-arrow-in',
			itemId: 'minus',
			tooltip: '全部收缩',
			handler: function() {
				me.onMinusClick();
			}
		}, {
			iconCls: 'button-arrow-out',
			itemId: 'plus',
			tooltip: '全部展开',
			handler: function() {
				me.onPlusClick();
			}
		});
		return {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		};
	},
	/**删除行过滤条件及基角色权限信息*/
	onDelRBACRowFilter: function(record) {
		var me = this;
		var roleRightId = record.get('Para');
		JShell.Msg.del(function(but) {
			if(but != "ok") return;
			var rowId = record.get('tid'); //节点id值,角色的数据过滤行条件id
			var deleteUrl = JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_DeleteRBACRowFilterAndRBACRoleRightByPreconditionsId';
			deleteUrl = deleteUrl + "?id=" + rowId + "&preconditionsId=" + me.preconditionId;
			JShell.Server.get(deleteUrl, function(data) {
				if(data.success) {
					me.load(me.preconditionId);
				} else {
					JShell.Msg.error(data.msg);
				}
			}, null, false);
		});
	},
	//打开新增或编辑数据过滤条件设置应用
	openAppEditWin: function(formtype, record) {
		var me = this;
		if(!me.preconditionId) {
			JShell.Msg.alert('请先选中预置条件数据行', null, 1000);
			return;
		}
		var title = '',
			EntityCode = "",
			EntityCName = "";
		var id = null;
		//模块操作选中行记录
		if(record) {
			title = "修改数据过滤条件>>";
			id = record.get("tid");
		} else {
			title = "新增数据过滤条件>>";
		}
		var maxWidth = document.body.clientWidth * 0.92;
		var maxHeight = document.body.clientHeight * 0.86;
		var config = {
			title: title,
			SUB_WIN_NO: "1",
			width: 780,
			height: 540,
			maxWidth: maxWidth,
			maxHeight: maxHeight,
			layout: 'border',
			PK: id,
			moduleId: me.moduleId,
			preconditionId: me.preconditionId,
			preconditionSelect: me.preconditionSelect,
			formtype: formtype,
			resizable: true, //可变大小
			closable: true, //有关闭按钮
			draggable: true,
			listeners: {
				saveClick: function(win, rowId) {
					me.fireEvent('saveClick', me, rowId);
					win.close();
					me.load(me.preconditionId);
				},
				comeBackClick: function(win) {
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.sysbase.rowfilter.precondition.EditPanel', config).show();
	},
	onCopyClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.sysbase.rowfilter.precondition.copy.App', {
			resizable: true,
			width: 780,
			height: 520,
			SUB_WIN_NO: 1,
			moduleId: me.moduleId,
			preconditionId: me.preconditionId,
			entityCode: me.preconditionSelect.get("RBACPreconditions_EntityCode"),
			listeners: {
				copyAccept: function(p) {
					p.close();
				}
			}
		}).show();
	}
});