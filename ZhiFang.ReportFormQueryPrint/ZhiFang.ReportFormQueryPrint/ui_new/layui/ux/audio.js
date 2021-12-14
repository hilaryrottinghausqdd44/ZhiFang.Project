/**
	@name：layui.ux.uxaudio 声音功能
	@author：Jcall
	@version 2019-03-25
 */
layui.extend({
	uxutil:'ux/util'
}).define(['jquery','uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil;
		
	var uxaudio = {
		//全局项
		config:{
			index:0
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({},me.config,uxaudio.config,setings);
	};
	Class.pt = Class.prototype;
	//声音播放
	Class.pt.play = function(url){
		var me = this;
		var audio = '<object id="' + me.config.index +'" height="100px" width="100px" data="' + (url || me.config.url) +'"></object>';
		$("body").append(audio);
	};
	
	//核心入口
	uxaudio.render = function(options){
		var me = new Class(options);
		me.config.index = new Date().getTime();
		return me;
	};
	
	//暴露接口
	exports('uxaudio',uxaudio);
});