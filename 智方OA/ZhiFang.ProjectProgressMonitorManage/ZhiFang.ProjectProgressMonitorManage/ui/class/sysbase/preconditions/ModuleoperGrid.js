/***
 * 模块服务管理
 * @author longfc
 * @version 2017-05-17
 */
Ext.define('Shell.class.sysbase.preconditions.ModuleoperGrid', {
	extend: 'Shell.class.sysbase.moduleoper.basic.Grid',
	title: '模块服务列表',

	/**是否启用序号列*/
	hasRownumberer: false,
	hasAdd: false,
	hasEdit: false,
	hasCheckIsUse: false,
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
	moduleId: null,
	/**新增复制模块服务的全部预置条件项到选择模块服务*/
	copyPreconditionsUrl: JShell.System.Path.ROOT + '/' + 'RBACService.svc/RBAC_UDTO_CopyPreconditionsOfModuleOperId',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		if(items.length == 0) {
			if(me.hasRefresh) items.push('refresh');
			items.push({
				xtype: 'button',
				text: '复制拷贝',
				itemId: 'btnCopy',
				iconCls: 'build-button-add',
				listeners: {
					click: function(com, e, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(!records || records.length != 1) {
							JShell.Msg.error(" 请选择模块服务行后再操作!");
							return;
						}
						me.onCopyClick(records[0]);
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
	onCopyClick: function(record) {
		var me = this;
		JShell.Win.open('Shell.class.sysbase.preconditions.copy.App', {
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
		for(var i = 0; i < records.length; i++) {
			copyIdStr += records[i].get("RBACModuleOper_Id");
			if(i < records.length - 1) copyIdStr += ",";
		}
		var moduleoperId = me.getSelectionModel().getSelection()[0].get("RBACModuleOper_Id");
		if(moduleoperId && copyIdStr) {
			var url = JShell.System.Path.getRootUrl(me.copyPreconditionsUrl) + "?moduleoperId=" + moduleoperId + "&copyModuleOpeIdStr=" + copyIdStr;
			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					p.close();
					me.onSearch();
					JShell.Msg.alert("模块服务的预置条件项复制到指定的模块服务成功", null, 2000);
				} else {
					me.fireEvent('saveerror', me);
					JShell.Msg.error(data.msg);
				}
			});
		}
	}
});