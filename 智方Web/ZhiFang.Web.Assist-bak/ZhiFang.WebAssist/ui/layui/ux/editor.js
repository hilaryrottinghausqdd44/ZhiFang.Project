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
			'strong','italic','underline','del','addhr','code',
			'|','removeformat','fontFomatt','fontfamily','fontSize','fontBackColor','colorpicker','face',
			'|','left','center','right',
			'|','link','unlink','images','image_alt','video','attachment','anchors',
			'|','table','customlink','undo','redo',
			'|','html','preview'
		]
	});
	
	//暴露接口
	exports('editor',editor);
});