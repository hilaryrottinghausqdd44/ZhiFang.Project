/**
 * 角色权限的角色列表
 * @author longfc
 * @version 2017-06-22
 */
Ext.define('Shell.class.sysbase.rowfilter.roleright.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '角色列表',
	width: 280,
	height: 380,
	hasRefresh:false,
	defaultLoad: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL?isPlanish=true',
	delUrl: '/RBACService.svc/RBAC_UDTO_DelRBACRoleRight',

	initComponent: function() {
		var me = this;
		me.dockedItems = me.createDockedItems();
		me.defaultWhere = me.defaultWhere || '';
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createButtontoolbar());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'button',
			text: '分配角色',
			iconCls: 'button-edit',
			itemId: 'btnAssignRoles',
			//cls: "btn btn-default btn-sm active",
			style: {
				marginRight: '10px'
			},
			listeners: {
				click: function(com, e, eOpts) {
					me.openAppShowWin();
				}
			}
		});
		var toolbar =Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
		return toolbar;
	},

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '名称',
			dataIndex: 'RBACRoleRight_RBACRole_CName',
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '是否新增',
			isBool: true,
			dataIndex: 'IsAdd',
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'RBACRoleRight_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '角色ID',
			dataIndex: 'RBACRoleRight_RBACRole_Id',
			hidden: true,
			hideable: false
		}, {
			xtype: "actioncolumn",
			sortable: false,
			text: "操作",
			width: 55,
			align: "center",
			itemId: "Action",
			items: [{
				iconCls: "build-button-delete hand",
				tooltip: "删除",
				handler: function(grid, rowIndex, colIndex, item, e, record) {
					var isAdd = record.get("IsAdd");
					var id = record.get(me.PKField);
					if(isAdd == true && id) {
						me.store.remove(record);
					} else {
						me.onDeleteClick(record);
					}
				}
			}]
		}]
		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		for(var i = 0; i < data.length; i++) {
			data[i]["IsAdd"] = false;
		}
		return data;
	},
	/**删除按钮点击处理方法*/
	onDeleteClick: function(record) {
		var me = this;
		JShell.Msg.del(function(but) {
			if(but != "ok") return;
			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = 1;
			me.showMask(me.delText); //显示遮罩层
			var id = record.get(me.PKField);
			me.delOneById(10, id);
		});
	},
	//打开分配角色应用效果窗口
	openAppShowWin: function() {
		var me = this;
	}
});