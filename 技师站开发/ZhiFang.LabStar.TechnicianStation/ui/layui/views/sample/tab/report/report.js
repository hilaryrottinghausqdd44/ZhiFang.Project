/**
 * 相关医嘱报告
 * @author GHX
 * @version 2021-05-06
 */
layui.extend({
}).define(['uxutil', 'table', 'form'], function (exports) {
	"use strict";
	var $ = layui.jquery,
		table = layui.table,
		form = layui.form,
		uxutil = layui.uxutil;
	var app = {};
	app.url = {};
	app.params = {
		recode: null,
		isReadOnly: true
	};
	//初始化
	app.init = function (testFormRecord, isReadOnly) {
		var me = this;
		var height=$(window).height()-60;
		
		if (testFormRecord) {
			me.params.recode = testFormRecord;
			me.params.isReadOnly = isReadOnly;
			var patno = me.params.recode.LisTestForm_PatNo;
			var src = uxutil.path.REPORTFORMQUERYPRINTROOTPATH + "/ui_new/layui/class/labstar/report/report.html?patno=" + patno;
			$("#reportIframeinfo").css("height", height);
			$("#reportIframeinfo").html('<iframe src="' + src + '" style="border: medium none;height:100%;width:100%;"></iframe>');
		} else {
			$("#reportIframeinfo").html('<div style="line-height: 26px;padding: 15px;text-align: center;color: #999;">暂无相关数据</div>');
		}
	};
	//暴露接口
	exports('report', app);
});
