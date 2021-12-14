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
		
	var config = {
		
	};
	
	var Class = {};
	
	var uxaudio = {
		//核心入口
		render:function(options){
			return table.render(config);
		},
		changeData:function(){}
	};
	
	//暴露接口
	exports('uxaudio',uxaudio);
});