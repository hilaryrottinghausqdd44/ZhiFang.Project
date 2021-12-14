/**
 * @name：views/pre/order/apply_model/modules/tree
 * @author：zhangda
 * @version 2021-08-12
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil','tree'],function(exports){
	"use strict";
	
	var $ = layui.$,
		tree = layui.tree,
		uxutil = layui.uxutil,
		MOD_NAME = 'PreOrderModelItemTree';
	
	//获取项目树服务地址
	var GET_TREE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/GetOrderModelTree";
	
	//项目树
	var PreOrderModelItemTree = {
		//对外参数
		config:{
			domId: null,
			where: null,
			OrderModelTypeID : null,
			listeners:{
				
			}
		},
		//内部树参数
		treeConfig:{
			elem:null,
			id:null,
			onlyIconControl:true,//是否仅允许节点左侧图标控制展开收缩
			//showCheckbox:true,//是否显示复选框
			data:[],
			text:{
				defaultNodeName:'项目',//节点默认名称
				none:'无数据'//数据为空时的提示文本
			},
			click: function(obj){
				console.log(obj.data); //得到当前点击的节点数据
			}
		}
	};
	
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreOrderModelItemTree.config,setings);
		me.treeConfig = $.extend({},me.treeConfig,PreOrderModelItemTree.treeConfig);
		
		me.treeConfig.elem = "#" + me.config.domId;
		me.treeConfig.id = me.config.domId + "-tree";
		if(me.config.listeners && me.config.listeners.click){
			me.treeConfig.click = me.config.listeners.click;
		}
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		
	};
	//重新加载数据
	Class.prototype.reload = function(){
		var me = this;
		me.getItemsTree(function(data){
			tree.reload(me.treeConfig.id,{
				data:uxutil.server.changeTreeData(data)
			});
		});
	};
	//获取项目树
	Class.prototype.getItemsTree = function(callback){
		var me = this,
			where = me.config.where || null,
			OrderModelTypeID = me.config.OrderModelTypeID;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			type: 'post',
			url: GET_TREE_URL,
			data: JSON.stringify({ OrderModelTypeID: OrderModelTypeID,ItemWhere: where })
		}, function (data) {
			layer.close(loadIndex);//关闭加载层
			if (data.success) {
				callback(data.value || []);
			} else {
				layer.msg(data.msg);
				callback();
			}
		});
	};
	
	//获取所有子孙的叶子结点
	Class.prototype.getAllLeafByNode = function(node){
		var me = this;
		var list = [];
		if(node.children && node.children.length > 0){
			for(var i in node.children){
				list = list.concat(me.getAllLeafByNode(node.children[i]));
			}
		}else{
			list.push(node);
		}
		
		return list;
	};
	//核心入口
	PreOrderModelItemTree.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//实例化表单
		me.tree = tree.render(me.treeConfig);
		//监听事件
		me.initListeners();
		//加载数据
		me.reload();
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,PreOrderModelItemTree);
});