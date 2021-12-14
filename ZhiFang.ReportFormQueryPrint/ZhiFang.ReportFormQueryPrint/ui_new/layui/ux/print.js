/**
	@name：layui.ux.print 打印组件
	@author：Jcall
	@version 2020-11-16
 */
layui.extend({
	uxutil:'ux/util'
}).define(['jquery','uxutil','layer'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		layer = layui.layer,
		MOD_NAME = "print";
	
	//打印控件地址
	var PrintFileUrl = uxutil.path.ROOT + '/web_src/c_lodop/zhifang/print/Print.js?t=' + new Date().getTime();
	
	var Class = function(){};
	//加载打印基础文件
	Class.prototype.loadFile = function(callback){
		var me = this;
		$.getScript(PrintFileUrl).done(function(){
			me.initZFPrint();
			callback && callback();
		}).fail(function(){
			layer.msg("打印组件加载失败，请重新尝试打印！");
		});
	};
	//初始化打印组件
	Class.prototype.initZFPrint = function(){
		var me = this;
		
		//重写弹出下载CLodop文件弹框
		ZFPrint.openFileUploadWindow = function(type){
			var msg = this.getMsgContent(type);
			layer.alert(msg);
		};
		ZFPrint.init();
	};
	
	var me = new Class();
	//加载打印基础文件
	me.loadFile(function(){
		me.instance = window.ZFPrint;//打印实例
	});
	
	//暴露接口
	exports(MOD_NAME,me);
});