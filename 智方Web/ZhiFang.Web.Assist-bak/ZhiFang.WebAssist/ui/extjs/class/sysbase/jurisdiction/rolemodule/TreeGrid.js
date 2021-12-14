/**
 * 角色模块列表树
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.rolemodule.TreeGrid', {
	extend: 'Shell.class.sysbase.module.Tree',
	requires: ['Ext.ux.CheckColumn'],
	title: '角色模块列表树',

	selectLinkUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACRoleModule',

	width: 240,
	height: 400,

	rootVisible: false,

	/**原始数据*/
	RawData: null,
	/**角色ID*/
	RoleId: null,

	/**关系数据*/
	LinkData: null,
	/**默认勾选的数据*/
	DefaultCheckedList: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.columns = [{
			xtype: 'treecolumn',
			text: '模块名称',
			dataIndex: 'text',
			width: 180,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				if (value) {
					value = Ext.typeOf(value) == 'string' ? value.replace(/"/g, '&quot;') : value;
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				}
				return value;
			}
		}, {
			xtype: 'checkcolumn',
			text: '勾选',
			dataIndex: 'IsLinked',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			xtype: 'actioncolumn',
			text: '展开',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'button-arrow-out',
				tooltip: '展开该节点',
				handler: function(grid, rowIndex, colIndex) {
					var node = grid.getStore().getAt(rowIndex);
					node.expand(true);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '收缩',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'button-arrow-in',
				tooltip: '收缩该节点',
				handler: function(grid, rowIndex, colIndex) {
					var node = grid.getStore().getAt(rowIndex);
					node.collapse(true);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '全选',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'button-accept',
				tooltip: '选中该节点及其子孙节点',
				handler: function(grid, rowIndex, colIndex) {
					var node = grid.getStore().getAt(rowIndex);
					me.onCheckAllByNode(node);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '全不选',
			align: 'center',
			width: 50,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'button-cancel',
				tooltip: '取消选中的该节点及其子孙节点',
				handler: function(grid, rowIndex, colIndex) {
					var node = grid.getStore().getAt(rowIndex);
					me.onUnCheckAllByNode(node);
				}
			}]
		}, {
			text: '关系主键ID',
			dataIndex: 'LinkId',
			isKey: true,
			hidden: true,
			hideable: false
		}];

		me.callParent(arguments);
	},
	createDockedItems: function() {
		var me = this;
		var dockedItems = me.callParent(arguments);
		dockedItems[0].items = dockedItems[0].items.slice(0, -3);
		dockedItems[0].items.splice(1, 0, '-', {
			xtype: 'checkbox',
			boxLabel: '本节点',
			itemId: 'check'
		});

		dockedItems[0].items.push('-', {
			text: '<b style="color:green;">全勾选</b>',
			tooltip: '勾选全部模块',
			handler: function() {
				var root = me.getRootNode();
				me.onCheckAllByNode(root);
			}
		}, {
			text: '<b style="color:red;">全不选</b>',
			tooltip: '取消全部勾选',
			handler: function() {
				var root = me.getRootNode();
				me.onUnCheckAllByNode(root);
			}
		});
		dockedItems.push({
			xtype: 'toolbar',
			dock: 'bottom',
			itemId: 'bottomToolbar',
			items: ['->', {
				iconCls: 'button-save',
				itemId: 'save',
				text: '保存',
				tooltip: '保存',
				handler: function() {
					me.onSaveClick();
				}
			}]
		});

		return dockedItems;
	},

	/**勾选该节点下的全部子孙节点*/
	onCheckAllByNode: function(node) {
		var me = this;

		if (node.data.tid) {
			node.data.IsLinked = true;
			node.commit();
		}

		me.onNodeHandlerByParentNode(node, function(cNode) {
			cNode.data.IsLinked = true;
			cNode.commit();
		}, function() {});
	},
	/**取消勾选该节点下的全部子孙节点*/
	onUnCheckAllByNode: function(node) {
		var me = this;

		if (node.data.tid) {
			node.data.IsLinked = false;
			node.commit();
		}

		me.onNodeHandlerByParentNode(node, function(cNode) {
			cNode.data.IsLinked = false;
			cNode.commit();
		}, function() {});
	},
	/**获取选中的节点*/
	getCheckRootNode: function() {
		var me = this,
			topToolbar = me.getComponent('topToolbar'),
			check = topToolbar.getComponent('check').getValue();

		if (check) {
			var nodes = me.getSelectionModel().getSelection();
			if (nodes.length == 0) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return null;
			} else {
				return nodes[0];
			}
		} else {
			return me.getRootNode();
		}
	},

	/**根据父节点处理所有子孙节点*/
	onNodeHandlerByParentNode: function(node, nodeCallback, overCallback) {
		var me = this;

		//处理回调函数必须存在，否则直接退出方法
		if (!nodeCallback || Ext.typeOf(nodeCallback) != 'function') {
			return;
		}

		function onNodeHandler(node, callback) {
			var childNodes = node.childNodes,
				len = childNodes.length;

			for (var i = 0; i < len; i++) {
				var cNode = childNodes[i];
				callback(cNode);
				onNodeHandler(cNode, callback);
			}
		}

		onNodeHandler(node, nodeCallback);

		if (overCallback && Ext.typeOf(overCallback) == 'function') {
			overCallback();
		}
	},
	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;
		var fields = me.callParent(arguments);
		fields.push({
			name: 'IsLinked',
			type: 'bool'
		}, {
			name: 'LinkId',
			type: 'string'
		});

		return fields;
	},

	/**根据角色加载关系数据*/
	loadByRoleId: function(id) {
		var me = this;
		me.RolieId = id;

		me.showMask(me.loadingText); //显示遮罩层
		//加载关系数据
		me.loadLinkData(function() {
			//默认勾选关系
			me.onDefaultCheckLink();
			me.hideMask(); //隐藏遮罩层
		});
	},
	/**加载关系数据*/
	loadLinkData: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectLinkUrl;
		url += '&fields=RBACRoleModule_Id,RBACRoleModule_RBACModule_Id';
		url += '&where=rbacrolemodule.RBACRole.Id=' + me.RolieId;

		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.LinkData = data.value.list;
				callback();
			} else {
				me.LinkData = [];
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**默认勾选关系*/
	onDefaultCheckLink: function() {
		var me = this,
			root = me.getRootNode(),
			list = me.LinkData || [],
			len = list.length,
			checkedList = [];

		me.onUnCheckAllByNode(root);

		for (var i = 0; i < len; i++) {
			var node = me.getNodesByField('tid', list[i].RBACRoleModule_RBACModule_Id, true);
			if (node.length == 1) {
				node[0].data.IsLinked = true;
				checkedList.push({
					Id: list[i].RBACRoleModule_Id,
					ModuleId: list[i].RBACRoleModule_RBACModule_Id
				});
				node[0].commit();
			}
		}
		me.DefaultCheckedList = checkedList;
	},
	/**根据属性和值来查找Node数组*/
	getNodesByField: function(field, value, isOnlyOne) {
		var me = this,
			root = me.getRootNode(),
			nodes = [];

		function onNodeHandler(node) {
			var childNodes = node.childNodes,
				len = childNodes.length;

			for (var i = 0; i < len; i++) {
				var cNode = childNodes[i];
				if (cNode.data[field] == value) {
					nodes.push(cNode);
					if (isOnlyOne) break;
				} else {
					onNodeHandler(cNode);
				}

				if (isOnlyOne && nodes.length > 0) break;
			}
		}

		onNodeHandler(root);

		return nodes;
	},

	/**保存*/
	onSaveClick: function() {
		var me = this,
			checkedList = Ext.clone(me.DefaultCheckedList),
			nodes = me.getNodesByField('IsLinked', true),
			len = nodes.length,
			addModuleIds = [],
			delIds = [];

		for (var i = 0; i < len; i++) {
			var isInDefaultCheckedList = false;
			for (var j in checkedList) {
				if (!checkedList[j].ModuleId) continue;
				if (checkedList[j].ModuleId == nodes[i].data.tid) {
					isInDefaultCheckedList = true;
					checkedList[j].ModuleId = null;
					break;
				}
			}
			if (!isInDefaultCheckedList) {
				addModuleIds.push(nodes[i].data.tid);
			}
		}

		for (var i in checkedList) {
			if (checkedList[i].ModuleId) {
				delIds.push(checkedList[i].Id);
			}
		}

		//保存数据
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = ids.length;

		for (var i in addModuleIds) {
			me.addLink(addModuleIds[i]);
		}
		for (var i in delIds) {
			me.delLink(delIds[i]);
		}
	},
	/**新增关系*/
	addLink: function(moduleId) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.addUrl;
		var entity = {
			IsUse: true,
			RBACModule: {
				Id: moduleId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			},
			RBACRole: {
				Id: me.RoleId,
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
			me.onSaveEnd();
		});
	},
	/**删除关系*/
	delLink: function(id) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.delUrl + '?id=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	},
	onSaveEnd: function() {
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			me.hideMask(); //隐藏遮罩层
			me.loadByRoleId(me.RoleId);
			if (me.saveErrorCount == 0) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
			} else {
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
		}
	}
});
