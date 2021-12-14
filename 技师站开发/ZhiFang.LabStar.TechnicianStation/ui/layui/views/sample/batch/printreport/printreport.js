/**
 * 批量打印报告
 * @author 王耀宗
 * @version 2021-5-7
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		
		$ = layui.jquery;
	var app = {};
	var SECTIONID='';
	//初始化  
	app.init = function () {
		var me = this;
		
		var src = uxutil.path.REPORTFORMQUERYPRINTROOTPATH + "/ui_new/layui/class/labstar/batch/printreport/printreport.html?SECTIONNO=" + SECTIONID;
		$("#printreportIframeinfo").html('<iframe src="' + src + '" style="border: medium none;height:100%;width:100%;"></iframe>');
		
	};
	//初始化数据
	window.initData = function(data,afterSave){
		SECTIONID = data;
		//初始化调用入口
	app.init();
	};
	
	
});