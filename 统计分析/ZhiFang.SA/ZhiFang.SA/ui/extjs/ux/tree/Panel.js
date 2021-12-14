/**
 * 基础树
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.tree.Panel', {
	extend: 'Ext.tree.Panel',
	alias: 'widget.uxTreePanel',

	/**获取数据服务路径*/
	selectUrl: JShell.System.Path.DEFAULT_ERROR_URL,
	/**删除数据服务路径*/
	delUrl: JShell.System.Path.DEFAULT_ERROR_URL,

	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**删除数据提示*/
	delText: JShell.Server.DEL_TEXT,

	/**默认数据条件*/
	defaultWhere: '',
	/**内部数据条件*/
	internalWhere: '',
	/**外部数据条件*/
	externalWhere: '',

	/**主键列*/
	PKField: 'Id',
	/**删除标志字段*/
	DelField: 'delState',
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: true,
	/**
	 * 排序字段
	 * @exception 
	 * [{property:'DContractPrice_ContractPrice',direction:'ASC'}]
	 */
	defaultOrderBy: [],
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**消息信息样式*/
	msgFormat: '<div style="color:blue;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**错误信息*/
	errorInfo: null,

	/**自定义按钮功能栏*/
	buttonToolbarItems: null,

	/**子节点的属性名*/
	defaultRootProperty: 'Tree',
	/**根节点*/
	root: {
		text: 'ROOT',
		iconCls: 'main-package-16',
		tid: 0,
		leaf: false,
		expanded: false
	},
	/**是否显示根节点*/
	rootVisible:true,
	/**默认选中节点ID*/
	selectId: null,
	/**默认隐藏ID，隐藏该节点及所有子孙节点*/
	hideNodeId: null,
	
	/**是否可加载数据*/
	canLoad:false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.store.on({
			beforeload: function() {
				if (me.hasLoadMask) {
					me.mk = me.mk || new Ext.LoadMask(me.getEl(), {
						msg: '数据加载中...',
						removeMask: true
					});
					me.mk.show(); //显示遮罩层
				}
			},
			load: function() {
				if (me.mk) {
					me.mk.hide();
				} //隐藏遮罩层
			}
		});

		if (me.defaultLoad) {
			me.load();
		}else{
			var toolbar = me.getComponent('topToolbar');
			toolbar.disable();
		}
	},
	initComponent: function() {
		var me = this;
		me.dockedItems = me.createDockedItems();
		me.store = me.createStore();
		me.callParent(arguments);
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
		}];

		return [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		}];
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			me.getPathRoot()) + me.selectUrl;

		return Ext.create('Ext.data.TreeStore', {
			fields: me.getStoreFields(),
			defaultRootProperty: me.defaultRootProperty,
			root: me.root,
			proxy: {
				type: 'ajax',
				url: url,
				extractResponseData: function(response) {
					return me.changeStoreData(response);
				}
			},
			listeners: {
				beforeload: function() {
					if(!me.canLoad) return false;
					return me.onBeforeLoad();
				},
				load: function (store, node, records, successful) {
					me.onAfterLoad(records, successful);
				}
			},
			defaultLoad: false
		});
	},
	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;

		var fields = [{
				name: 'text',
				type: 'auto'
			}, //默认的现实字段
			{
				name: 'expanded',
				type: 'auto'
			}, //是否默认展开
			{
				name: 'leaf',
				type: 'auto'
			}, //是否叶子节点
			{
				name: 'icon',
				type: 'auto'
			}, //图标
			{
				name: 'url',
				type: 'auto'
			}, //地址
			{
				name: 'tid',
				type: 'auto'
			}, //默认ID号
			{
				name: 'value',
				type: 'auto'
			}
		];

		return fields;
	},
	/**点击刷新按钮*/
	onRefreshClick: function() {
		this.canLoad = true;
		this.store.load();
	},
	/**点击收缩按钮*/
	onMinusClick: function() {
		var me = this;
		var toolbar = me.getComponent('topToolbar');
		toolbar.disable();

		me.collapseAll(function() {
			toolbar.enable();
		});
		
		var root = me.getRootNode();
		root.expand();
		
		//根节点隐藏&&根节点只有一个子节点的时候，展开第一个子节点
		var children = root.childNodes,
			len = children.length;
			
		if(!me.rootVisible && len == 1){
			children[0].expand();
		}
	},
	/**点击展开按钮*/
	onPlusClick: function() {
		var me = this;
		me.getEl().mask('所有节点展开中...');
		var toolbar = me.getComponent('topToolbar');
		toolbar.disable();

		me.expandAll(function() {
			me.getEl().unmask();
			toolbar.enable();
		});
	},
	onBeforeLoad: function() {},
	onAfterLoad: function() {
		var me = this;
		me.onMinusClick();
		me.selectNode(me.selectId);
	},
	/**树形数据转换*/
	changeStoreData: function(response) {
		var me = this;
		var data = Ext.JSON.decode(response.responseText);

		data[me.defaultRootProperty] = [];
		if (data.ResultDataValue && data.ResultDataValue != "") {
			var value = Ext.JSON.decode(data.ResultDataValue);
			if (value) {
				data[me.defaultRootProperty] = value[me.defaultRootProperty];
			}
		}
		delete data.ResultDataValue;

		if (me.hideNodeId) {
			me.doHideNode(data);
		}

		data = me.changeData(data);

		response.responseText = Ext.JSON.encode(data);

		return response;
	},
	/**隐藏节点*/
	doHideNode: function(data) {
		var me = this;

		var children = data[me.defaultRootProperty];

		me.changeChildren(children);
	},
	/**修改所有子节点*/
	changeChildren: function(children) {
		var me = this;
		for (var i = 0; i < children.length; i++) {
			var bo = me.changeNode(children[i]);
			if (bo) {
				children.splice(i, 1);
				i--;
			}
		}
		return children;
	},
	/**修改节点*/
	changeNode: function(node) {
		var me = this;
		//需要剔除的节点
		if (node.tid == me.hideNodeId) {
			return true;
		}

		var children = node[me.defaultRootProperty];
		if (children) {
			me.changeChildren(children);
		}

		return false
	},

	/**@overwrite 修改数据方法*/
	changeData: function(data) {
		return data;
	},
	/**
	 * 选中默认的一行数据
	 * @private
	 */
	selectNode: function(id) {
		if (id == null) return;

		var me = this,
			id = id + '',
			root = me.getRootNode();

		var node = id == '0' ? root : root.findChild('tid', id, true);

		if (node) {
			me.openParentNode(node);
			setTimeout(function(){
				me.getSelectionModel().select(node);
			},300);
			
		}
		
		return node;
	},
	/**
	 * 打开上级节点
	 * @private
	 * @param {Object} node
	 */
	openParentNode: function(node) {
		var me = this,
			parentNode = node.parentNode;

		if (parentNode) {
			parentNode.expand();
			me.openParentNode(parentNode);
		} else {
			return;
		}
	},

	/**
	 * @public
	 * 加载数据
	 */
	load: function() {
		this.onRefreshClick();
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];

		for (var i = 0; i < length; i++) {
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			items[i][enable ? 'enable' : 'disable']();
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(false);
	},
	showError:function(msg){
		var me = this;
		me.errorMk = me.errorMk || new Ext.LoadMask(me.getEl(), {
			msg: msg || '错误',
			removeMask: true
		});
		me.errorMk.show(); //显示遮罩层
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(); //启用所有的操作功能
	},
	/**获取ROOTURL*/
	getPathRoot:function(){
		return JShell.System.Path.ROOT;
	}
});