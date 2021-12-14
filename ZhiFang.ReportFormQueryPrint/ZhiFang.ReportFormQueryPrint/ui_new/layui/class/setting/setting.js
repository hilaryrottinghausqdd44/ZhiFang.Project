/**
 * 设置页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form','element','tree'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		element = layui.element,tree=layui.tree,
		$ = layui.jquery;
	var app = {};
	//var data1 = [{
	//	title: '模块'
	//	, id: 1
	//	,spread:true//是否默认展开
	//	, children: [
	//		{
	//			title: '医生页面设置'
	//			, id: 1001
	//			, html: 'public/public.html'
	//			,module:'doctor'
	//		},{
	//			title: '护士页面设置'
	//			, id: 1002
	//			, html: 'public/public.html'
	//			, module: 'nurse'
	//		}, {
	//			title: '查询台页面设置'
	//			, id: 1003
	//			, html: 'public/public.html'
	//			, module: 'odp'
	//		},{
	//			title: '技师站页面设置'
	//			, id: 1004
	//			, html: 'public/public.html'
	//			, module: 'lis'
	//		}, {
	//			title: '自助打印页面设置'
	//			, id: 1005
	//			, html: 'selfhelp/selfhelp.html'
	//			, module: 'selfhelp'
	//		}, {
	//			title: '站点查询设置'
	//			, id: 1006
	//			, html: 'public/public.html'
	//			, module: 'siteQuery'
	//		}, {
	//			title: '分库查询设置'
	//			, id: 1007
	//			, html: 'public/public.html'
	//			, module: 'historyAndBackups'
	//		}, {
	//			title: '集中打印设置'
	//			, id: 1008
	//			, html: 'public/public.html'
	//			, module: 'focusPrint'
	//		}, {
	//			title: '检验前后查询设置'
	//			, id: 1009
	//			, html: 'public/public.html'
	//			, module: 'CheckReportRequest'
	//		}, {
	//			title: 'LabStar调用设置'
	//			, id: 1010
	//			, html: 'public/public.html'
	//			, module: 'labStar'
	//		}, {
	//			title: '模板配置信息'
	//			, id: 1011
	//			, html: 'xmlConfig/xmlConfig.html'
	//			, module: 'xmlConfig'
	//		}, {
	//			title: '小组打印模板设置'
	//			, id: 1012
	//			, html: 'PrintSetting/PrintSetting.html'
	//			, module: 'PrintSetting'
	//		}, {
	//			title: '清除打印次数'
	//			, id: 1013
	//			, html: 'clearPrintCount/clearPrintCount.html'
	//			, module: 'clearPrintCount'
	//		}, {
	//			title: '程序基础配置'
	//			, id: 1014
	//			, html: 'webconfig/webconfig.html'
	//			, module: 'webconfig'
	//		}, {
	//			title: '程序升级脚本'
	//			, id: 1015
	//			, html: 'ScriptingOptions/ScriptingOptions.html'
	//			, module: 'ScriptingOptions'
	//		}
	//		//,
	//		//{
	//		//	title: '模块表单显示列配置'
	//		//	, id: 1016
	//		//	, html: 'public.html'
	//		//	, module: 'nurse'
	//		//}
	//	]
	//}];
	//初始化  
	app.init = function () {
		var me = this;
		
		//根据参数决定是否显示log信息
		//var params = Shell.util.Path.getRequestParams(true);
		//for (var i in params) {
		//	if (i.toLowerCase() === "SHOWLOG" && params[i] === "true") {
		//		Shell.util.Config.showLog = true;
		//	} else if (i.toLowerCase() === "SHOWLOGWIN" && params[i] === "true") {
		//		Shell.util.Config.showLogWin = true;
		//	}
		//}
		//判断用户是否登录
		function getCookie(name) {
			var cookies = document.cookie;
			var list = cookies.split("; ");    // 解析出名/值对列表

			for (var i = 0; i < list.length; i++) {
				var arr = list[i].split("=");  // 解析出名和值
				if (arr[0] == name)
					return decodeURIComponent(arr[1]);  // 对cookie值解码
			}
			return "";
		}
		var webconfigStaticUser = getCookie("webconfigStaticUser");
		var cookie = { webconfigStaticUser: webconfigStaticUser };

		if (webconfigStaticUser == null || webconfigStaticUser == "") {
			window.location.href = uxutil.path.UI + '/layui/class/setting/userSign/index.html';
		}
		//$("#settingLeftContent").css("height", ($(document).height() - 40) + "px"); //设置左侧容器高度
		//$("#settingRightContent").css("height", ($(document).height() - 40) + "px"); //设置右侧容器高度
		$("#moduleTabContent").css("height", ($(document).height() - 25) + "px");
		//默认加载医生页面的设置
		var src = uxutil.path.UI + '/layui/class/setting/';
		var conditions = '?module=doctor' + '&moduleID=1';
		var htmlType = 'public/public.html';
		var iframe = '<iframe src="' + uxutil.path.UI + '/layui/class/setting/public/public.html?module=doctor&moduleID=1' + '" height="100%" width="100%" frameborder="0"' + '></iframe>';
		$("#moduleTabContent").html(iframe);
		me.listeners();
		
	};

	//监听事件
	app.listeners = function () {
		//监听小组页签切换
		element.on('tab(moduleTab)', function (data) {
			var layid = $(this).attr("lay-id");
			var moduleName = $(this).attr("name");
			//if (layid=='1') {
			//	$("#moduleTabContent").html("aaa");
			//}
			var src = uxutil.path.UI + '/layui/class/setting/';
			var conditions = '?module=' + moduleName + '&moduleID=' + layid;
			var htmlType = 'public/public.html';
			
			switch (layid) {
				
				case "5": htmlType = 'selfhelp/selfhelp.html';
					break;
				case "11": htmlType = 'xmlConfig/xmlConfig.html';
					break;
				case "12": htmlType = 'PrintSetting/PrintSetting.html';
					break;
				case "13": htmlType = 'clearPrintCount/clearPrintCount.html';
					break;
				case "14": htmlType = 'webconfig/webconfig.html';
					break;
				case "15": htmlType = 'ScriptingOptions/ScriptingOptions.html';
					break;
				case "16": htmlType = 'moduleFormColumnConfiguration/configuration.html';
					break;
				default: htmlType = 'public/public.html';
			}
			var iframe = '<iframe src="' + src + htmlType + conditions + '" height="100%" width="100%" frameborder="0"' + '></iframe>';
			$("#moduleTabContent").html(iframe);
		});
		//监听左侧树形结构点击事件
		//tree.render({
		//	elem: '#test1' //默认是点击节点可进行收缩
		//	, data: data1
		//	, click: function (obj) {
		//		//当前节点下是否有子节点,如果没有子节点则页面右侧跳转相关页面
		//		if (!obj.data.children) {
		//			//obj.data.html obj.data.module
		//			var src = uxutil.path.UI + '/layui/class/setting/' + obj.data.html + "?module=" + obj.data.module +'&moduleID'+obj.data.id;
		//			$("#settingRightContent").html('<iframe src="' + src +'" height="100%" width="100%" frameborder="0"'  +'></iframe>');					
		//		}
		//	}
		//});
    }

	app.init();
});