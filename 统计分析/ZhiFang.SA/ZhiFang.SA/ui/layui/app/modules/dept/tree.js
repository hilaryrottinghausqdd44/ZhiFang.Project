/**
	@name：layui.ux.modules.org.tree 部门树
	@author：Jcall
	@version 2019-04-29
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil','tree','layer'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		tree = layui.tree;
	
	//默认参数配置
	var config = {
		//附属的元素
		elem:null,
		//获取部门树服务路径
		url:uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id,HRDept_UseCode&node=0&id=0'
	};
	var depttree = {
		//核心入口
		render:function(options){
			//获取数据
			uxutil.server.ajax({
				url:config.url
			},function(data){
				var nodes = [];
				if(data.success){
					nodes = depttree.changeDate((data.value || {}).Tree || []);
				}else{
					layer.alert(data.msg);
				}
				//开始渲染
				tree({
					elem:options.elem,
					nodes:nodes
				});
			});
		},
		//数据转换
		changeDate:function(tree){
	    	var changeNode = function(index,node){
	    		node.name = node.text;//节点名称
	    		//node.spread = node.expanded;//是否展开
	    		
	    		node.children = node.Tree;
	    		var children = node.Tree;
	    		if(children){
	    			changeChildren(children);
	    		}
	    	};
	    	
	    	var changeChildren = function(children){
	    		$.each(children,changeNode);
	    	};
	    	
	    	changeChildren(tree);
	    	
	    	return tree;
		}
	};
	
	//暴露接口
	exports('depttree',depttree);
});