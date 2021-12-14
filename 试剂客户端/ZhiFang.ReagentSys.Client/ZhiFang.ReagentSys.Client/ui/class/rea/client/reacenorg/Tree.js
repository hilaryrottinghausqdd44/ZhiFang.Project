/**
 * 本地机构
 * @author longfc
 * @version 2018-01-30
 */
Ext.define('Shell.class.rea.client.reacenorg.Tree', {
	extend: 'Shell.ux.tree.Panel',
	requires: [
		'Shell.ux.form.field.TextSearchTrigger'
	],
	title: '机构信息',
	width: 300,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgListTreeByOrgID',
	
	/**默认加载数据*/
	defaultLoad: true,
	/**机构类型*/
	OrgType: null,
	/**是显示平台机构编码还是机构编码*/
	ShowPlatformOrgNo: null,
	/**根节点*/
	root: {
		text: '所有机构',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: false
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.store.on({
			load: function(store, node, records) {
				if(node || node.childNodes.length > 0) {
					var select = node;
					if(!me.rootVisible && node.get("tid") == "0")
						select = node.childNodes[0];
					me.getSelectionModel().select(select);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.createTopToolbar();
		me.callParent(arguments);
	},
	createTopToolbar: function() {
		var me = this;
		me.topToolbar = me.topToolbar || ['-', {
			xtype: 'textSearchTrigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: 125,
			triggerCls: 'x-form-clear-trigger',
			enableKeyEvents: true,
			listeners: {
				onSearchClick: {
					fn: function(field, newValue, e) {
						JShell.Action.delay(function() {
							me.filterByText(newValue);
						}, null, 300);
					}
				},
				onClearClick: {
					fn: function(field, newValue, e) {
						this.setValue('');
						me.clearFilter();
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
	},
	/**
	 * 根据显示文字过滤
	 * @private
	 * @param {} text
	 */
	filterByText: function(text) {
		this.filterBy(text, ['text']);
	},
	/**
	 * 根据值和字段过滤
	 * @private
	 * @param {} text 过滤的值
	 * @param {} byArr 过滤的字段数组
	 */
	filterBy: function(text, byArr) {
		this.clearFilter();
		if(!text) return;

		var view = this.getView(),
			me = this,
			nodesAndParents = [];

		this.getRootNode().cascadeBy(function(tree, view) {
			var currNode = this;
			if(!currNode) return;

			var isRight = false;
			for(var i in byArr) {
				var data = currNode.data;
				var arr = byArr[i].split('.');
				for(var j in arr) {
					data = data[arr[j]];
				}

				if(data) {
					//节点的匹配判断逻辑-包含输入的文字，不区分大小写，可修改
					if(data.toString().toLowerCase().indexOf(text.toLowerCase()) > -1) {
						me.expandPath(currNode.getPath());
						while(currNode.parentNode) {
							nodesAndParents.push(currNode.id);
							currNode = currNode.parentNode;
						}
					}
				}
			}
		}, null, [me, view]);

		me.onPlusClick(); //全部展开

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
		var changeNode = function(node) {
			switch(me.ShowPlatformOrgNo) {
				case true:
					if(node.value && node.value.PlatformOrgNo)
						node.text = '[' + node.value.PlatformOrgNo + '] ' + node.text;
					else
						node.text = '<b style="color:red;">[无平台机构码]</b> ' + node.text;
					break;
				default:
					//机构编号
					if(node.value && node.value.OrgNo)
						node.text = '[' + node.value.OrgNo + '] ' + node.text;
					break;
			}
			var children = node[me.defaultRootProperty];
			if(children) changeChildren(children);
		};

		var changeChildren = function(children) {
			Ext.Array.each(children, changeNode);
		};
		var children = data[me.defaultRootProperty];
		changeChildren(children);

		return data;
	},
	onBeforeLoad: function() {
		var me = this;
		if(me.OrgType == null || me.OrgType == undefined || me.OrgType == "") return false;
		me.store.proxy.url = me.getLoadUrl();
	},
	getSearchFields: function() {
		var me = this;
		return "ReaCenOrg_Id,ReaCenOrg_OrgNo,ReaCenOrg_PlatformOrgNo";
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + ('fields=' + me.getSearchFields() + "&orgType=" + me.OrgType);

		return url;
	}
});