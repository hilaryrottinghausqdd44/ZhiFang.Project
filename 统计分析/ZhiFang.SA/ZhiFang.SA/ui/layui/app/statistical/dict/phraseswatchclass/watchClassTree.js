layui.define(["tree", "uxutil"], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;

	var watchClassTree = {
		config: {
			elem: '',
			id: '',
			/**是否显示根节点*/
			rootVisible: true,
			nodes: [],
			selectUrl: uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassTreeById?id=0",
			click: function(node) {

			}
		},
		set: function(options) {
			var me = this;
			me.config = $.extend({}, me.config, options);
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, watchClassTree.config, options);
		return me.render();
	};
	Class.pt=Class.prototype;
	//默认配置
	Class.pt.config = {
		/**根节点*/
		root: {
			name: '树根节点',
			tid: 0,
			pid: 0,
			leaf: false,
			spread: true, //是否展开
			expanded: true,
			children: []
		},
		/**子节点的属性名*/
		defaultRootProperty: 'children'
	};
	//获取查询Url
	Class.pt.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
	};
	//获取查询Url
	Class.pt.loadData = function(options) {
		var me = this;
		uxutil.server.ajax({
			url: me.config.selectUrl
		}, function(data) {
			if (data.success) {
				var nodes = me.changeData(data.value || []);
				me.config.root.children = nodes;
				options.nodes = [me.config.root];
				return layui.tree(options);
			} else {
				return layui.tree(options);
			}
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
		if (!node.id) node.id = node.tid;
		if (!node.name) node.name = node.text;
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
		if (me.config && (me.config.refresh || !me.config.nodes || me.config.nodes.length <= 0)) {
			var elem = $(me.config.elem);
			if (elem) elem[0].innerHTML = "";
			me.loadData(me.config);
		} else {
			layui.tree(me.config);
		}
		//暂时用这种方式继承tree
		inst = $.extend({}, Class.pt, watchClassTree);
		return inst;
	};
	//核心入口
	watchClassTree.render = function(options) {
		var me = this;
		var inst = new Class(options);
		return inst;
	};
	//对外公开返回对象
	Class.pt.result = function(that){
		that=that||new Class();
		return that;
	};
	//暴露接口
	exports('watchClassTree', watchClassTree);
});
