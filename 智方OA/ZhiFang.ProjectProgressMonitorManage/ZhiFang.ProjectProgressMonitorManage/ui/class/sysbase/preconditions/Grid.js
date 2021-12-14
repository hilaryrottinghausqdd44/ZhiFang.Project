/***
 * 预置条件
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.preconditions.Grid', {
	extend: 'Shell.class.sysbase.preconditions.basic.Grid',
	title: '预置条件信息',

	/**是否启用序号列*/
	hasRownumberer: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	hasAdd: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否显示被禁用的数据*/
	isShowDel: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	editUrl: JShell.System.Path.ROOT + '/' + 'RBACService.svc/RBAC_UDTO_UpdateRBACPreconditionsByField',
	afterRender: function() {
		var me = this;		
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '预置条件名称',
			isLike: true,
			fields: ['rbacpreconditions.CName']
		};
		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	/**@overwrite 编辑按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('editclick', me, records[0]);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		if(items.length == 0) {
			if(me.hasRefresh) items.push('refresh');
			if(me.hasAdd) items.push('add');
			if(me.hasEdit) items.push('edit');
			items.push({
				xtype: 'button',
				text: '复制新增',
				itemId: 'btnCopy',
				iconCls: 'build-button-add',
				listeners: {
					click: function(com, e, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(!records || records.length != 1) {
							JShell.Msg.error(" 请选择行后再操作!");
							return;
						}
						me.fireEvent('copyClick', me, records[0]);
					}
				}
			});
			items.push('-', {
				boxLabel: '显示禁用',
				itemId: 'checkIsUse',
				checked: me.isShowDel,
				value: me.isShowDel,
				inputValue: false,
				xtype: 'checkbox',
				style: {
					marginRight: '4px'
				},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue == true) {
							me.isShowDel = true;
						} else {
							me.isShowDel = false;
						}
						me.onSearch();
					}
				}
			});
			if(me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = [],
			search = null;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar) {
			search = buttonsToolbar.getComponent('search');
		}
		//是否显示被禁用的数据,如果不显示
		if(me.isShowDel == false) {
			params.push("rbacpreconditions.IsUse=1");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}

		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}

		return me.callParent(arguments);
	}
});