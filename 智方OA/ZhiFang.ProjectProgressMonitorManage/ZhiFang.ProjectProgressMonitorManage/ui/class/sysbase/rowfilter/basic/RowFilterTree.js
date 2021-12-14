/***
 * 数据过滤条件角色树
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.rowfilter.basic.RowFilterTree', {
	extend: 'Shell.ux.tree.Panel',

	title: '数据过滤条件角色树',
	filterfield: 'text',
	childrenField: 'Tree',

	width: 460,
	lines: true,
	useArrows: true,
	isTure: false,
	rootVisible: false,
	autoScroll: true,

	moduleId: null,
	moduleOperId: null,
	//模块操作选中行记录
	moduleOperSelect: null,
	objectName: null,
	objectCName: null,
	/**是否预置条件的行数据还是单表的行数据*/
	IsPreconditions: false,
	selectUrl: JShell.System.Path.ROOT + "",
	delRoleRightUrl: '/RBACService.svc/RBAC_UDTO_DelRBACRoleRight',
	root: {
		text: '',
		objectType: 'string',
		leaf: false,
		id: 0,
		expanded: false
	},
	/***
	 * 是否开始粘贴按钮操作
	 * false:未开始,
	 * true:开始
	 * @type Boolean
	 */
	ismodulePaste: false,
	/***
	 * 模块操作的信息
	 * moduleCopy:{Id:模块操作id,CName:模块操作名称}
	 * modulePaste:{Id:'',CName:''},//粘贴模块操作
	 * objectCName:选择中的模块操作对象名称
	 * moduleCopyCName:"",//复制模块中文名称
	 * modulePasteCName:""//粘贴模块中文名称
	 * @type 
	 */
	operateInfo: {
		moduleCopy: {
			Id: '',
			CName: ''
		}, //复制模块操作
		modulePaste: {
			Id: '',
			CName: ''
		}, //粘贴模块操作{Id:'',CName:''}
		objectCName: "",
		moduleCopyCName: "", //复制模块中文名称
		modulePasteCName: "" //粘贴模块中文名称
	},
	afterRender: function() {
		var me = this;
		me.addEvents('saveClick');
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.selectUrl = me.selectUrl + "?isPreconditions=" + me.IsPreconditions;
		me.columns = me.createTreeColumns();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createTreeColumns: function() {
		var me = this;
		var columns = [{
			xtype: "actioncolumn",
			text: "操作列",
			width: 80,
			align: "center",
			itemId: "Action",
			items: [{
					iconCls: "build-button-edit hand",
					tooltip: "修改数据过滤条件信息",
					handler: function(grid, rowIndex, colIndex, item, e, record) {
						me.editClick(grid, rowIndex, colIndex, item, e, record);
					}
				},
				{
					iconCls: "blank16",
					tooltip: "",
					getClass: function(v, meta, record) {
						return '';
					}
				},
				{
					iconCls: "build-button-delete hand",
					tooltip: "删除数据过滤条件/角色信息",
					handler: function(grid, rowIndex, colIndex, item, e, record) {
						me.deleteClick(grid, rowIndex, colIndex, item, e, record);
					}
				}
			]
		}, {
			xtype: 'treecolumn',
			text: '行数据条件',
			flex: 1,
			minWidth: 300,
			sortable: true,
			dataIndex: 'text'
		}, {
			text: '行数据条件主键ID',
			dataIndex: 'id',
			width: 10,
			sortable: false,
			hidden: true,
			hideable: false
		}, {
			text: '角色主键ID',
			dataIndex: 'tid',
			width: 10,
			sortable: false,
			hidden: true,
			hideable: false
		}];
		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createButtontoolbar());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtontoolbar: function() {
		var me = this,
			items = [];

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
	createtoolsAddFilter: function() {
		var me = this;
		var filter = {
			xtype: 'textfield',
			fieldLabel: '检索过滤',
			itemId: 'filterText',
			labelAlign: 'right',
			triggerCls: 'x-form-clear-trigger',
			labelWidth: 65,
			enableKeyEvents: true,
			listeners: {
				keyup: {
					fn: function(field, e) {
						if(Ext.EventObject.ESC == e.getKey()) {
							this.setValue('');
							me.clearFilter();
						} else {
							me.filterByText(this.getRawValue());
						}
					}
				}
			}
		};
		return filter;
	},
	filterByText: function(text) {
		this.filterBy(text, this.filterfield);
	},
	filterBy: function(text, by) {
		var me = this;
		this.clearFilter();
		var view = this.getView();
		var tempValue = '';
		nodesAndParents = [];
		this.getRootNode().cascadeBy(function(tree, view) {
			var textValue = text.toLowerCase();
			var byValue = by.toString().toLowerCase().split(',');
			if(isNaN(parseInt(text, 10))) {
				textValue = String(text.toLowerCase()).trim();
			} else {
				textValue = String(text).trim();
			}
			for(var i = 0; i < byValue.length; i++) {
				var currNode = this;
				if(currNode && currNode.data[byValue[i]] && currNode.data[byValue[i]].indexOf(textValue) > -1) {
					me.expandPath(currNode.getPath());
					while(currNode.parentNode) {
						nodesAndParents.push(currNode.id);
						currNode = currNode.parentNode;
					}
				}
			}
		}, null, [this, view]);
		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {
				Ext.get(uiNode).setDisplayed('none');
			}
		}, null, [me, view]);
	},
	clearFilter: function() {
		var me = this;
		var view = this.getView();
		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode) {
				Ext.get(uiNode).setDisplayed('table-row');
			}
		}, null, [this, view]);
	},
	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;
		var fields = ['pid', 'value', 'Para', 'ParentID', 'Id', 'tid', 'url', 'icon', 'leaf', 'objectType', 'text', 'expanded'];
		return fields;
	},
	changeStoreData: function(response) {
		var me = this;
		var result = JShell.JSON.decode(response.responseText);
		if(result && result.success) {
			result[me.childrenField] = [];
			if(result.ResultDataValue && result.ResultDataValue != '') {
				var ResultDataValue = JShell.JSON.decode(result.ResultDataValue);
				result[me.childrenField] = ResultDataValue.Tree;
			}
			var changeNode = function(node) {
				var value = node['value'];
				for(var i in value) {
					node[i] = value[i];
					//图片地址处理
					if(node[i]['objectType'] && node[i]['objectType'] == "RBACRowFilter") {
						node[i]['icon'] = JShell.System.Path.ROOT + "/ui/css/images/icons/list.PNG";
					} else if(node[i]['objectType'] == "RBACRole") {
						node[i]['icon'] = JShell.System.Path.ROOT + "/ui/css/images/icons/default.png";
					}
				}
				//默认所有都不展开
				node['expanded'] = false;
				//图片地址处理
				if(node['objectType'] && node['objectType'] == "RBACRowFilter") {
					node['icon'] = JShell.System.Path.ROOT + "/ui/css/images/icons/list.PNG";
				} else if(node['objectType'] == "RBACRole") {
					node['icon'] = JShell.System.Path.ROOT + "/ui/css/images/icons/default.png";
				}
				var children = node[me.childrenField];
				if(children) {
					changeChildren(children);
				}
			};
			var changeChildren = function(children) {
				Ext.Array.each(children, changeNode);
			};

			var children = result[me.childrenField];
			changeChildren(children);
		} else {
			JShell.Msg.error('错误信息【<b style="color:red">' + '获取数据失败' + '</b>】');
		}
		response.responseText = JShell.JSON.encode(result);
		return response;
	},
	load: function(id) {
		var me = this;
		id = (id && id != '') ? id : 0;
		me.canLoad = true;
		var root = me.getRootNode();
		root.setId(id);
		me.store.load({
			node: root
		});
	},
	//打开某角色下的员工信息列表应用窗口
	openRoleListShowWin: function(hqlWhere, roleCName) {
		var me = this;
		var maxHeight = document.body.clientHeight * 0.98;
		var maxWidth = document.body.clientWidth * 0.98;
		var title = "" + roleCName + "的员工信息";
		var config = {
			id: -1,
			SUB_WIN_NO: "2",
			title: title,
			internalWhere: '',
			externalWhere: hqlWhere,
			maxWidth: maxWidth,
			autoScroll: true,
			closable: true, //有关闭按钮
			resizable: true, //可变大小
			draggable: true,
			listeners: {}
		};

		if(config.height > maxHeight) {
			config.height = maxHeight;
		}
		var win = JShell.Win.open('Shell.class.sysbase.user.role.SimpleGrid', config).show();
	},
	//打开新增或编辑数据过滤条件设置应用
	openAppEditWin: function(appType, id) {
		var me = this;
	},
	//查看某角色下的员工角色数据
	showClick: function(grid, rowIndex, colIndex, item, e, record) {
		var me = this;
		if(record && record != null) {
			//objectType为RBACRowFilter是数据过滤条件节点
			var objectType = '' + record.get('objectType');
			if(objectType == 'RBACRole') {
				var tid = record.get('tid'); //角色
				var roleCName = record.get('text'); //角色名称
				var hqlWhere = 'rbacemproles.RBACRole.Id=' + tid;
				me.openRoleListShowWin(hqlWhere, roleCName);
			} else if(objectType == 'RBACRowFilter') {
				JShell.Msg.alert('请选择角色查看', null, 1000);
			}
		}
	},
	//编辑行过滤条件信息
	editClick: function(grid, rowIndex, colIndex, item, e, record) {
		var me = this;
		//objectType为RBACRowFilter是数据过滤条件节点
		var objectType = '' + record.get('objectType');
		if(objectType == 'RBACRowFilter') {
			var appId = record.get('tid'); //
			me.openAppEditWin('edit', appId);
		} else if(objectType == 'RBACRole') {
			var item1 = item;
			var parentNode = record.parentNode;
			if(parentNode && parentNode != undefined) {
				var appId = parentNode.get('tid'); //
				me.openAppEditWin('edit', appId);
			} else {
				JShell.Msg.alert('请选择数据过滤条件', null, 1000);
			}
		}
	},
	//行过滤条件或行过滤条件的角色删除操作
	deleteClick: function(grid, rowIndex, colIndex, item, e, record) {
		var me = this;
		if(record) {
			var objectType = '' + record.get('objectType');
			switch(objectType) {
				case 'RBACRole':
					me.onDelRBACRoleRight(record);
					break;
				case 'RBACRowFilter':
					me.onDelRBACRowFilter(record);
					break;
				default:
					break;
			}
		}
	},
	/**删除行过滤条件的某一角色*/
	onDelRBACRoleRight: function(record) {
		var me = this;
		var roleRightId = record.get('Para');
		JShell.Msg.del(function(but) {
			if(but != "ok") return;
			var deleteUrl = JShell.System.Path.ROOT + me.delRoleRightUrl + "?id=" + roleRightId;
			JShell.Server.get(deleteUrl, function(data) {
				if(data.success) {
					me.load(me.moduleOperId);
				} else {
					JShell.Msg.error(data.msg);
				}
			}, null, false);
		});
	},
	/**删除行过滤条件及基角色权限信息*/
	onDelRBACRowFilter: function(record) {
		var me = this;
		var roleRightId = record.get('Para');
		JShell.Msg.del(function(but) {
			if(but != "ok") return;
			var rowId = record.get('tid'); //节点id值,角色的数据过滤行条件id
			var deleteUrl = JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_DeleteRBACRoleRightByModuleOperId';
			deleteUrl = deleteUrl + "?id=" + rowId + "&moduleOperId=" + me.moduleOperId;
			JShell.Server.get(deleteUrl, function(data) {
				if(data.success) {
					me.load(me.moduleOperId);
				} else {
					JShell.Msg.error(data.msg);
				}
			}, null, false);
		});
	}
});