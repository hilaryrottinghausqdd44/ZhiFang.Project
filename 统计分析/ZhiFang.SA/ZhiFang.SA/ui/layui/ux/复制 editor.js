/**
	@name：layui.ux.editor 对layedit文本编辑器扩展
	@author：Jcall
	@version 2019-03-26
 */
layui.define(['uxutil','element','layedit'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		element = layui.element,
		editor = layui.layedit;
	
	//更改全局设置
	$.extend(editor.config,{
		tool: [
			'html', 'undo', 'redo', 'code', 'strong', 'italic', 'underline', 'del', 'addhr', '|','removeformat', 'fontFomatt', 'fontfamily','fontSize', 'fontBackColor', 'colorpicker', 'face'
			, '|', 'left', 'center', 'right', '|', 'link', 'unlink', 'images', 'image_alt', 'video','attachment', 'anchors'
			, '|'
			, 'table','customlink'
			, 'fullScreen','preview'
		]
	});
	
	//创建文本编辑器
	editor.create = function(id, settings){
		var html = editor.createHtml(id);
		var div = $("#" + id).html(html);
		
		var tabItems = $(div).find('.layui-tab-item');
		
		var index = editor.build(tabItems[0].children[0], settings);
		element.render();
		element.on('tab(component-tabs-brief)', function(obj){
			editor['onTabClick' + obj.index](tabItems[obj.index].children[0],index);
		});
		return index;
	}
	//创建HTML
	editor.createHtml = function(id){
		var html = 
		'<div class="layui-tab layui-tab-brief" lay-filter="component-tabs-brief">' +
			'<ul class="layui-tab-title">' +
				'<li class="layui-this">页面设计</li>' +
				'<li>代码浏览</li>' +
				'<li>效果浏览</li>' +
			'</ul>' +
			'<div class="layui-tab-content" style="padding:10px 0;">' +
				'<div class="layui-tab-item layui-show"><div></div></div>' +
				'<div class="layui-tab-item"><xmp></xmp></div>' +
				'<div class="layui-tab-item"><div></div></div>' +
			'</div>' +
			'<div style="text-align:right">' +
				'<a href="" class="layui-btn">保存结果</a>' +
			'</div>' +
		'</div>';
		$("#" + id).html(html);
	}
	
	//页面设计
	editor.onTabClick0 = function(tab,index){
		
	}
	//代码浏览
	editor.onTabClick1 = function(tab,index){
		var html = editor.getContent(index);
		$(tab).html(html);
	}
	//效果浏览
	editor.onTabClick2 = function(tab,index){
		var html = editor.getContent(index);
		$(tab).html(html);
	}
	
	//暴露接口
	exports('editor',editor);
});