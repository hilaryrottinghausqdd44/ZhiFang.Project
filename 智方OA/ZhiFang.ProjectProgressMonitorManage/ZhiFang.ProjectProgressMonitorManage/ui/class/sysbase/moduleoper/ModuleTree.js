/**
 * 模块树
 * @author longfc
 * @version 2017-05-17
 */
Ext.define('Shell.class.sysbase.moduleoper.ModuleTree', {
	extend: 'Shell.ux.tree.Panel',

	title: '模块树',
	width: 260,
	height: 500,
	searchWidth: 130,
	/**默认加载数据*/
	defaultLoad: true,
	/**是否显示根节点*/
	rootVisible: false,
	selectId: '',
	/**根节点*/
	root: {
		text: '所有模块',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: false
	},
	/**获取数据服务路径*/
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleToListTree?fields=RBACModule_Id,RBACModule_ModuleType',

	getValue: function() {
		var myTree = this;
		var arrTemp = myTree.getSelectionModel().getSelection();
		return arrTemp;
	},
	initComponent: function() {
		var me = this;
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.onMinusClick();
		JShell.Action.delay(function() {
			if(records && records.length > 0) {
				me.selectId = me.getRootNode().childNodes[0].get("tid");
			}
			me.selectNode(me.selectId);
		}, null, 500);
	},
	changeData:function(data){
		var me = this;
    	var changeNode = function(node){
    		//图片地址处理
    		if(node['icon'] && node['icon'] != ''){
    			node['icon'] = JShell.System.Path.MODULE_ICON_ROOT_16 + "/" + node['icon'];
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	
    	var children = data[me.defaultRootProperty];
    	changeChildren(children);
    	
    	return data;
	},
	createDockedItems: function() {
		var me = this;
		var items = [{
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
		}, {
			xtype: 'trigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: me.searchWidth,
			triggerCls: 'x-form-clear-trigger',
			enableKeyEvents: true,
			onTriggerClick: function() {
				this.setValue('');
				me.clearFilter();
			},
			listeners: {
				keyup: {
					fn: function(field, e) {
						var bo = Ext.EventObject.ESC == e.getKey();
						bo ? field.onTriggerClick() : me.filterByText(this.getRawValue());
					}
				}
			}
		}];

		return [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		}];
	},
	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;
		var fields = ['CName', 'value', 'ParentID', 'Id', 'tid', 'url', 'icon', 'leaf', 'DataTimeStamp', 'text', 'expanded', 'ModuleType'];
		return fields;
	},
	/**
	 * 清空过滤
	 * @private
	 */
	clearFilter: function() {
		var view = this.getView();
		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode) {
				Ext.get(uiNode).setDisplayed('table-row');
			}
		}, null, [this, view]);
	},
	/**
	 * 根据显示文字过滤
	 * @private
	 * @param {} text
	 */
	filterByText: function(text) {
		this.filterBy(text, 'text');
	},
	/**
	 * 根据值和字段过滤
	 * @private
	 * @param {} text 过滤的值
	 * @param {} by 过滤的字段
	 */
	filterBy: function(text, by) {
		this.clearFilter();
		var view = this.getView(),
			me = this,
			nodesAndParents = [];

		this.getRootNode().cascadeBy(function(tree, view) {
			var currNode = this;
			if(currNode && currNode.data[by]) {
				//节点的匹配判断逻辑-包含输入的文字，不区分大小写，可修改
				if(currNode.data[by].toString().toLowerCase().indexOf(text.toLowerCase()) > -1) {
					me.expandPath(currNode.getPath());
					while(currNode.parentNode) {
						nodesAndParents.push(currNode.id);
						currNode = currNode.parentNode;
					}
				}
			}
		}, null, [me, view]);

		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {
				Ext.get(uiNode).setDisplayed('none');
			}
		}, null, [me, view]);
	}
});