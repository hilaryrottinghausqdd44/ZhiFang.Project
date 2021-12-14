/**
 * 任务类型树（根据权限读出任务类型树）
 * @author liangyl
 * @version 2016-06-22
 */
Ext.define('Shell.class.wfm.task.new.Tree', {
	extend: 'Shell.ux.tree.CheckPanel',
	title: '任务类型树',
	width: 240,
	height: 500,
	multiSelect: true,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkToTreeBySessionHREmpID',
	/**默认加载数据*/
	defaultLoad: true,
	searchWidth: 120,
	defaultWhere: '',
	rootVisible: false,
	isTrue: false,
	/**二审*/
	TwoAudit: null,
	/**任务分配*/
	Publish: null,
	selectId:"5753060783994672008",
	root: {
		text: '任务类型',
		iconCls: 'main-package-16',
		id: "5753060783994672008",
		tid: "5753060783994672008",
		pid: 0,
		leaf: false,
		expanded: false
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//二审	
		if(me.TwoAudit) {
			me.selectUrl = me.selectUrl + "?where=" + me.TwoAudit;
		}
		//任务分配
		if(me.Publish) {
			me.selectUrl = me.selectUrl + "?where=" + me.Publish;
		}
		me.topToolbar = me.topToolbar || ['-', {
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
		}, '->', {
			iconCls: 'button-right',
			tooltip: '<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		}];
		me.callParent(arguments);
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
	createDockedItems: function() {
		var me = this;
		var dockedItems = me.callParent(arguments);
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		return dockedItems;
	},
	changeData: function(data) {
		var me = this;
		//手工添加任务类型节点,默认选择任务类型节点
		if(data.Tree && data.Tree.length > 0) {
			data.Tree = [{
				text: '任务类型',
				iconCls: 'main-package-16',
				id: "5753060783994672008",
				tid: "5753060783994672008",
				pid: 0,
				leaf: false,
				expanded: false,
				Tree: data.Tree
			}];
		}
		var changeNode = function(node) {
			//图片地址处理
			node['icon'] = JShell.System.Path.MODULE_ICON_ROOT_16 + "/" +
				(node['leaf'] ? 'dictionary.PNG' : 'package.PNG');

			var children = node[me.defaultRootProperty];
			if(children) {
				changeChildren(children);
			}
		};

		var changeChildren = function(children) {
			Ext.Array.each(children, changeNode);
		};
		var children = data[me.defaultRootProperty];
		changeChildren(children);
		return data;
	}
});