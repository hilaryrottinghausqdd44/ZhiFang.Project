/**
	@name：短语页签
	@author：liangyl
	@version 2019-10-30
 */
layui.extend({
	commonzf:'/modules/common/zf',
    itemphrasetab:'app/dic/section/phrase/item/index',
    samplephrasetab:'app/dic/section/phrase/sample/index'
}).define(['table','uxutil','element','itemphrasetab','samplephrasetab'],function(exports){
	"use strict";
	var $=layui.$,
	    itemphrasetab = layui.itemphrasetab,
	    samplephrasetab = layui.samplephrasetab,
	    element = layui.element,
	    uxutil = layui.uxutil,
		table = layui.table;

	var phrasetab  = {
		//全局项
		config:{
			itemphrasepanel:null,
			samplephrasepanel:null,
			SectionID:'',
			PK:null,
			currTabIndex:0,
			isLoadTabArr:[],
			isSampleTabLoad:false//样本短语页签是否已加载
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,phrasetab.config,setings);
	};
	Class.pt = Class.prototype;
		
	Class.pt.initFilterListeners =  function(){
		var me = this;
		element.on('tab(phraseTab)', function(obj){
	    	me.config.currTabIndex=obj.index;
	        var isLoad = false;
	        if(obj.index==1 &&!me.config.isSampleTabLoad){//样本短语
	        	me.config.samplephrasepanel = samplephrasetab.render({titie:'样本短语',SectionID:me.config.SectionID});
	        	me.config.isSampleTabLoad=true;
	        }
	        
	        me.config.samplephrasepanel.SectionID = me.config.SectionID;
	    });
	};
	Class.pt.load  =  function(id){
		var me = this;
		me.config.SectionID=id;
		
		for(var i =0;i<me.config.isLoadTabArr.length;i++){
    		//当前页签
    		if(me.config.isLoadTabArr[i].index == me.config.currTabIndex){
    			me.config.isLoadTabArr.splice(i, 1); //删除下标为i的元素
    			var obj1 = {index:me.config.currTabIndex,curRowId:id};
    	        me.config.isLoadTabArr.push(obj1);
    		}
    	}
    	//初始化，默认页签为表单页签
    	if(me.config.isLoadTabArr.length==0){
    	    var obj1 = {index:me.config.currTabIndex,curRowId:id};
    		me.config.isLoadTabArr.push(obj1);
    	}
    	setTimeout(function() {
	    	switch (me.config.currTabIndex){
	    	    case 0 ://表单
	    	     me.config.itemphrasepanel.loadData(id);
	    	     break;
	    		case 1 ://小组项目维护
	    		    me.config.samplephrasepanel.loadData(id);
	    		    break;
	    		default:
	    			break;
	    	}
    	},200);
		
//		me.config.itemphrasepanel.loadData(id);
//	    if(me.config.samplephrasepanel)me.config.samplephrasepanel.loadData(id);
	};
	Class.pt.init = function(){
		var me = this;
		element.render();
		me.config.itemphrasepanel = itemphrasetab.render({});
	};
    //核心入口
	phrasetab.render = function(options){
		var me = new Class(options);
		me.init();
	    me.loadData = Class.pt.load;
		me.initFilterListeners();
		return me;
	};
    //暴露接口
	exports('phrasetab',phrasetab);
});