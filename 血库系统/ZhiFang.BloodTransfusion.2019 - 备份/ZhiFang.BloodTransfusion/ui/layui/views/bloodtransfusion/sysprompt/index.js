layui.extend({
	uxutil: 'ux/util',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: 'config/bloodsconfig'
}).use(['uxutil', 'cachedata', 'bloodsconfig'], function() {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	//var layer = layui.layer;
	var bloodsconfig = layui.bloodsconfig;

	//初始化默认传入参数信息
	function initDefaultParams() {
		//接收传入参数
		var params = uxutil.params.get();
		var info = "";
		//his医生Id
		if (params["info"]) info = params["info"];

		if (info.length > 0) {
			info = info + "<br />请重新登录系统！";
			$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
		} else {
			$("#info").removeClass("layui-hide").addClass("layui-show").html("这里是系统提示页面,获取到的系统提示信息为空!!!<br />请重新登录系统！");
		}
	};

	initDefaultParams();

});
