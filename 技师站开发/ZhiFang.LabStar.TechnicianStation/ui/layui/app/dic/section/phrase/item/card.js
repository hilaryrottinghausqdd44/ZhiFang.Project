/**
	@name：短语
	@author：liangyl
	@version 2019-10-30
 */
layui.extend({
	phrasetable:'app/dic/section/phrase/item/phraselist'
}).define(['uxutil','commonzf','phrasetable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		phrasetable = layui.phrasetable,
		commonzf = layui.commonzf;	
	//外部接口
	var phrasecard = {
		//全局项
		config:{
			tableArr:[],
			tableNameArr:[],
			//针对类型1：小组样本 2：检验项目
			ObjectType:2,
			DATA_LIST:[]
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
		me.config = $.extend({},me.config,phrasecard.config,setings);
	};
	
	Class.pt = Class.prototype;
	
	//创建表格
	Class.pt.createTable = function(obj,html){
		var me = this;
		var height =0;
		var tabHeight = $(window).height() - $("#itemphraseBox").offset().top - 80;
        if((tabHeight/3)<150)height = 150;
        var itemPhraseObj = {
			elem:'#itemPhrase'+obj.Id,
	    	title:obj.Name,
	    	CODE:obj.Name,
	    	TypeCode:obj.Code,
	    	ObjectType:me.config.ObjectType,
	    	defaultToolbar: [],
	    	height:(tabHeight/3)>150 ? (tabHeight/3)+'px' : height+'px'
	    };
	    me.config.tableNameArr.push('itemPhrase'+obj.Id);
	    me.config.tableArr['itemPhrase'+obj.Id] = phrasetable.render(itemPhraseObj);
    };
    
	Class.pt.load = function(ItemID){
		var me = this;
     	for(var i=0;i<me.config.tableNameArr.length;i++){
     		me.config.tableArr[me.config.tableNameArr[i]].loadData({},ItemID)
     	}
	};
	//核心入口
	phrasecard.render = function(options){
		var me = new Class(options);
		$("#itemphraseBox").html('');
		commonzf.classdict.init('ItemPhrase',function(){
			var list=commonzf.classdict.ItemPhrase;
			me.config.DATA_LIST = list;
			var html="";
			for(var i=0;i<list.length;i++){
				var background="";
				html += '<span class="layui-badge layui-bg-green" style="display:inline-block;width:98%;">' + list[i].Name + '</span><table id="itemPhrase' + list[i].Id + '" lay-filter="itemPhrase' + list[i].Id + '"></table>';
			}
			$("#itemphraseBox").html(html);
			for(var i=0;i<list.length;i++){
			   me.createTable(list[i],html);
			}
		});
		me.loadData = Class.pt.load;
		//监听事件
		me.initListeners();
		return me;
	};
	Class.pt.initListeners = function(){
	    var me = this;
	};
	//对外公开返回对象
	Class.pt.result = function(that){
		return {
			index:that.config.index,
			loadData:that.loadData,
			changeSize:that.changeSize
		}
	};
	//暴露接口
	exports('phrasecard',phrasecard);
	
});