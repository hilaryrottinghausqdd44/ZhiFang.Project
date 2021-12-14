layui.extend({
	treeSelect: 'src/treeSelect'
}).define(["treeSelect", "uxutil"], function(exports) {
	"use strict";
	
	var $ = layui.jquery;
	var treeSelect = layui.treeSelect;
	var uxutil = layui.uxutil;

	var watchClassTreeSelect = {
		config: {
			elem: '',			
			/**是否显示根节点*/
			rootVisible: true,			
			//原treeSelect的data为异步数据url地址,调整后支持传入data或selectUrl
			selectUrl: uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassTreeById?id=0",
			click: function(node) {
				//点击回调
			},
			success: function(d) {
				//渲染完成后回调
			}
		},
		set: function(options) {
			var me = this;
			me.config = $.extend({}, me.config, options);
		},
		refresh: function() {
			var me = this;
			treeSelect.refresh(me.config.elem);
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, watchClassTreeSelect.config, options);
		//原treeSelect的data为异步数据url地址,调整后支持传入data或selectUrl
		if (!me.config.data) me.config.data = me.config.selectUrl;
		return me.render();
	};
	Class.pt=Class.prototype;
	//默认配置
	Class.pt.config = {
		/**根节点*/
		root: {
			name: '树根节点',
			id: 0,
			tid: 0,
			pid: 0,
			pId: 0,
			leaf: false,
			isParent: true,
			expanded: true,
			open: true,
			checked: false,
			children: []
		},
		/**子节点的属性名*/
		defaultRootProperty: 'children'
	};
	//初始渲染
	Class.pt.render = function(options) {
		var me = this;
		if(options)me.config = $.extend({}, me.config, options);
		return treeSelect.render(me.config);
	};
	//核心入口
	watchClassTreeSelect.render = function(options) {
		var me = this;
		var inst = new Class(options);
		//暂时用这种方式继承Class
		inst = $.extend({}, Class.pt, inst);
		//watchClassTreeSelect=inst;
		return inst;
	};
	//暴露接口
	exports('watchClassTreeSelect', watchClassTreeSelect);
});
