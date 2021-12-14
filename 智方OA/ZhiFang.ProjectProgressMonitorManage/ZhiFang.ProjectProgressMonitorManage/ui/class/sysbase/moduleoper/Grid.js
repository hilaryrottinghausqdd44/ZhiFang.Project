/***
 * 模块服务管理
 * @author longfc
 * @version 2017-05-17
 */
Ext.define('Shell.class.sysbase.moduleoper.Grid', {
	extend: 'Shell.class.sysbase.moduleoper.basic.Grid',
	title: '模块服务列表',

	/**是否启用序号列*/
	hasRownumberer: false,
	hasAdd: true,
	hasEdit: true,
	hasCheckIsUse: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否显示被禁用的数据*/
	isShowDel: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	//当前模块ID
	moduleId: null,
	editUrl: JShell.System.Path.ROOT + '/' + 'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField',
	/**将选择的模块服务新增复制到指定的模块中*/
	copyModuleOperUrl: JShell.System.Path.ROOT + '/' + 'RBACService.svc/RBAC_UDTO_CopyRBACModuleOperOfModuleId',
	hiddenCName: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('udateUseRowFilter');
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			text: '模块服务代码',
			dataIndex: 'RBACModuleOper_UseCode',
			width: 85,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '数据对象',
			dataIndex: 'RBACModuleOper_RowFilterBaseCName',
			width: 100,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '数据对象',
			dataIndex: 'RBACModuleOper_RowFilterBase',
			width: 120,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'RBACModuleOper_DispOrder',
			width: 50,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}, {
			text: '采用行过滤条件',
			dataIndex: 'RBACModuleOper_UseRowFilter',
			width: 90,
			sortable: false,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			text: '使用',
			dataIndex: 'RBACModuleOper_IsUse',
			width: 40,
			align: 'center',
			isBool: true,
			type: 'bool'
		});
		return columns;
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
				text: '复制拷贝',
				itemId: 'btnCopy',
				iconCls: 'build-button-add',
				listeners: {
					click: function(com, e, eOpts) {
						if(!me.moduleId) {
							JShell.Msg.error(" 请选择需要复制新增的模块节点后再操作!");
							return;
						} else {
							me.onCopyClick();
						}
					}
				}
			});
			if(me.hasCheckIsUse) {
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
			}
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
			params.push("rbacmoduleoper.IsUse=1");
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
	},
	onCopyClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.sysbase.moduleoper.copy.App', {
			resizable: true,
			width: 780,
			height: 520,
			SUB_WIN_NO: 1,
			moduleId: me.moduleId,
			listeners: {
				accept: function(p, records) {
					me.onCopyAccept(p, records);
				}
			}
		}).show();
	},
	onCopyAccept: function(p, records) {
		var me = this;
		var copyIdStr = "";
		if(!records || records.length < 1) {
			JShell.Msg.error(" 请选择需要复制的模块服务行后再操作!");
			return;
		}
		for (var i=0;i<records.length;i++) {
			copyIdStr+=records[i].get("RBACModuleOper_Id");
			if(i<records.length-1)copyIdStr+=",";
		}
		if(me.moduleId && copyIdStr) {
			var url = JShell.System.Path.getRootUrl(me.copyModuleOperUrl) + "?moduleId=" + me.moduleId + "&copyModuleOpeIdStr=" + copyIdStr;
			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					p.close();
					me.onSearch();
					JShell.Msg.alert("选择的模块服务复制到选择的模块成功", null, 2000);
				} else {
					me.fireEvent('saveerror', me);
					JShell.Msg.error(data.msg);
				}
			});
		}		
	}
});