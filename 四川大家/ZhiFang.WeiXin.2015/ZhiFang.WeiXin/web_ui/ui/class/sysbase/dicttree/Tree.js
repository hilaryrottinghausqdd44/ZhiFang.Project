/**
 * 类型树
 * @author 
 * @version 2016-06-22
 */
Ext.define('Shell.class.sysbase.dicttree.Tree', {
	extend: 'Shell.ux.tree.CheckPanel',
	title: '类型树',
	width: 240,
	height: 500,
	multiSelect: true,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeListTreeByIdListStr?fields=BDictTree_Id,BDictTree_DataTimeStamp',
	/**默认加载数据*/
	defaultLoad: true,
	searchWidth: 120,
	defaultWhere: '',
	rootVisible: true,
	isTrue: false,
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	treeShortcodeWhere: '',
	root: {
		text: '类型树',
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
			load: function(store, records, successful, eOpts) {
				me.getSelectionModel().select(me.root.tid);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.FTYPE = me.FTYPE || "";
		me.IDS = me.IDS || "";
		me.LEVEL = me.LEVEL || "";
		me.ROLEHREMPLOYEECNAME = me.ROLEHREMPLOYEECNAME || "";
		if(me.IDS && me.IDS.toString().length > 0) {
			me.treeShortcodeWhere = "idListStr=" + me.IDS;
		} else {
			me.treeShortcodeWhere = me.treeShortcodeWhere || "";
		}
		if(me.treeShortcodeWhere != '') {
			me.selectUrl = me.selectUrl + "&" + me.treeShortcodeWhere;
		}
		if(me.LEVEL && me.LEVEL.toString().length > 0) {
			me.selectUrl = me.selectUrl + "&maxLevelStr=" + me.LEVEL;
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

		//		me.viewConfig = {
		//			plugins: {
		//				ptype: 'treeviewdragdrop',
		//				dragText : "{0} 正在移动",
		//				allowLeafInserts: true
		//			},
		//			listeners: {
		//				beforedrop: function(node, data, overModel, dropPosition, dropFunction, eOpts) {
		//				},
		//				drop: function(node, data, overModel, dropPosition, options) {
		//					me.isTrue = false;
		//		            var records = me.getSelectionModel().getSelection();
		//					for (var i = 0; i < records.length; i++) {
		//						var editUrl = '/ProjectProgressMonitorManageService.svc/QMS_UDTO_UpdateBDictTreeByField';
		//						var url = (editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + editUrl;
		//						if (records[i].get('tid') != 0) {
		//							var entity = {
		//								ParentID: overModel.get("tid"),
		//								Id: records[i].get('tid')
		//							};
		//							var fields = "Id,ParentID";
		//							var params = Ext.JSON.encode({
		//								entity: entity,
		//								fields: fields
		//							});
		//							JShell.Server.post(url, params, function(data) {
		//								if (data.success) {
		//									me.isTrue = true;
		//								} else {
		//									me.isTrue = false;
		//									JShell.Msg.error(data.msg);
		//								}
		//							}, false);
		//						}
		//					}
		//				}
		//			}
		//		};

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
    	var changeNode = function(node){
    		//图片地址处理
			node['icon'] = JShell.System.Path.MODULE_ICON_ROOT_16 + "/" + 
    			(node['leaf'] ? 'dictionary.PNG' : 'package.PNG');
    		
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
	}
});