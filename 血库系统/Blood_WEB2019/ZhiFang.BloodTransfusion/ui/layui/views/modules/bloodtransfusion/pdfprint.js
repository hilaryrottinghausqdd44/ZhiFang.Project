/**
	@name：PDF.JS的打印处理
	@author：longfc
	@version 2019-12-23
 */
layui.extend({
	uxutil: 'ux/util',
}).use(["uxutil", 'layer', ], function() {
	"use strict";

	var $ = layui.jquery;
	var layer = layui.layer;
	var uxutil = layui.uxutil;
	//是否直接弹出打印确认框
	var isprint = false;
	//layer.msg
	var layerIndex = null;
	//setInterval
	var interval = null;
	//
	var setTimeoutCount=0;
	
	$(function() {
		//初始化
		function initParm() {
			var params = uxutil.params.get();
			if (params["isprint"]) isprint = params["isprint"];
			if (isprint == "true" || isprint == true) {
				isprint = true;
			} else {
				isprint = false;
			}
			//每1秒（1000 毫秒）调用 "loadPdf" :
			interval = setInterval(function() {
				loadPdf();
				setTimeoutCount=setTimeoutCount+1;
			}, 1000);
		};
		//PDF加载处理		
		function loadPdf() {
			//循环8次后,还
			if(setTimeoutCount==8){
				if (interval != null) clearInterval(interval);
				if (layerIndex != null) layer.close(layerIndex);
				return;
			}
			if (PDFViewerApplication.pdfDocument == null) {
				//console.info('PDF加载中...');
				if (layerIndex == null) {
					layerIndex = layer.msg('PDF处理中', {
						icon: 16,
						shade: 0.01
					});
				}
			} else {
				if (interval != null) clearInterval(interval);
				if (layerIndex != null) layer.close(layerIndex);
				if (isprint == true) {
					setTimeout(function() {
						window.print();
					}, 500);
				}
			}
		};
		initParm();
	});
});
