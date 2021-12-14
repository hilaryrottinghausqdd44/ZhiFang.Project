layui.extend({
	treeTable: 'src/treeTable'
}).define(["treeTable", "uxutil"], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var treeTable = layui.treeTable;
	var uxutil = layui.uxutil;

	var watchClassTreeTable = {
		config: {
			elem: '',
			/**是否显示根节点*/
			rootVisible: true,
			is_checkbox: true,
			cols: [{
					key: 'title',
					title: '名称',
					width: '100px',
					template: function(item) {
						if (item.level == 0) {
							return '<span style="color:red;">' + item.title + '</span>';
						} else if (item.level == 1) {
							return '<span style="color:green;">' + item.title + '</span>';
						} else if (item.level == 2) {
							return '<span style="color:#aaa;">' + item.title + '</span>';
						}
					}
				},
				{
					key: 'id',
					title: 'ID',
					width: '100px',
					align: 'center',
				},
				{
					key: 'pid',
					title: '父ID',
					width: '100px',
					align: 'center',
				}, {
					key: 'ShortCode',
					title: '简称',
					width: '100px',
					align: 'center',
				}
			],
			nodes: []
		},
		set: function(options) {
			var me = this;
			me.config = $.extend({}, me.config, options);
		}
	};
	//操作当前实例
	var thisClass = function() {
		var that = this,
			options = that.config;
		return {
			config: options
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, watchClassTreeTable.config, options);
		me.render();
	};
	Class.pt=Class.prototype;
	//默认配置
	Class.pt.config = {
		/**根节点*/
		root: {
			title: '树根节点',
			tid: 0,
			id: 0,
			pid: 0,
			leaf: false,
			spread: true, //是否展开
			expanded: true,
			children: []
		},
		/**子节点的属性名*/
		defaultRootProperty: 'children',
		/**子节点的属性名*/
		defaultRootProperty: 'children',
		icon_key: 'title',
		url: uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassListTreeById?id=0",
		fields: "PhrasesWatchClass_QIndicatorTypeCName,PhrasesWatchClass_ShortCode,PhrasesWatchClass_DispOrder"
	};
	//获取查询Url
	Class.pt.getLoadUrl = function() {
		var me = this;
		var url = this.config.url;
	};
	//获取查询Url
	Class.pt.loadData = function(options) {
		var me = this;
		var url = me.config.url;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.config.fields;
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if (data.success) {
				var nodes = me.changeData(data.value || []);
				me.config.root.children = nodes;
				options.nodes = [me.config.root];
			}
			return treeTable.render(options);
		});
	};
	//请求加载数据前处理
	Class.pt.onBeforeLoad = function() {
		var me = this;
	};
	//请求加载数据完成后处理
	Class.pt.onAfterLoad = function() {
		var me = this;
	};
	//node转换处理处理
	Class.pt.changeNode = function(node) {
		var me = this;
		console.log(node);
		if (!node.id) node.id = node.tid;
		if (!node.name) node.name = node.text;
		if (!node.title) node.title = node.text;
		if (!node.spread) node.spread = node.expanded;
		return node;
	};
	//数据转换处理
	Class.pt.changeData = function(data) {
		var me = this;
		var nodes = [];
		var tree = data.Tree || [],
			len = tree.length;
		tree.children = data.Tree;
		for (var i = 0; i < len; i++) {
			var node = tree[i];
			node = me.changeNode(node);
			if (node.leaf) {
				delete node.Tree;
				node.children = [];
				nodes.push(node);
			} else {
				node.children = me.changeData(node);
				delete node.Tree;
				nodes.push(node);
			}
		}
		return nodes;
	};
	//初始渲染
	Class.pt.render = function(options) {
		var me = this;
		if (options) me.config = $.extend({}, me.config, options);
		var inst = null;
		if (options && (options.refreshData || !options.nodes || options.nodes.length <= 0)) {
			inst = me.loadData(me.config);
		} else {
			inst = treeTable.render(me.config);
		}
		//暂时用这种方式继承Form
		inst = $.extend({}, Class.pt, inst);
		return inst;
	};
	//核心入口
	watchClassTreeTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		return inst;
	};
	//暴露接口
	exports('watchClassTreeTable', watchClassTreeTable);
});
