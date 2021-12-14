//消息卡片对外接口
;!function(win){
	"use strict";
	
	var doc = document,
		head = doc.head,
		ROOT,LOCAL,UI,EXTJS,LAYUI;
	
	//初始化地址信息
	function initPath(){
		var scripts = document.head.getElementsByTagName('script'),//获所有script标签
			script = scripts[scripts.length - 1],
			url = script.src;
			
		ROOT = url.split('/ui/layui/interface/msg_cv.js')[0];
		LOCAL = ROOT.substring(0,ROOT.lastIndexOf('/'));
		UI = ROOT + '/ui';
		EXTJS = UI + '/extjs';
		LAYUI = UI + '/layui';
	};initPath();
	
	//加载JS文件
	function loadScript(url,callback){
		var node = doc.createElement('script');
		
		node.async = true;
		node.charset = 'utf-8';
		node.src = url;
		head.appendChild(node);
		
		if(node.attachEvent && !(node.attachEvent.toString && node.attachEvent.toString().indexOf('[native code') < 0) && !isOpera){
			node.attachEvent('onreadystatechange',function(e){
				callback();
			});
		}else{
			node.addEventListener('load',function(e){
				callback();
			},false);
		}
	};
	
	//加载jquery文件
	loadScript(UI + '/src/jquery-1.11.1.min.js',function(){
		$("head").append("<link>");
		var css = $("head").children(":last");
		css.attr({
			rel:"stylesheet",
			type:"text/css",
			href:UI + "/src/layui/dist/css/layui.css"
		});
		$.getScript(UI + '/src/layui/dist/layui.js',function(){
			layui.config({
				base:LAYUI + '/',
				dir:UI + '/src/layui/dist/'
			}).extend({
				msgcard_cv:'modules/msg/cv'
			}).use(['msgcard_cv'],function(){
				window.msgcard_cv = layui.msgcard_cv;
//				layui.msgcard_cv.render({
//					loginInfo:{
//						"Account":"测试",
//						"PWD":"123456",
//						"Name":"测试",
//						"Sex":"男",
//						"DeptName":"测试科室",
//						"DeptHISCode":"测试科室HIS编码",
//						"Phone":"1234"
//					}
//				});
			});
		});
	});
}(window);
