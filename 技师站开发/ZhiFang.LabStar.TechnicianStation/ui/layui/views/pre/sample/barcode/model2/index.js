/**
   @Name：样本条码模式1
   @Author：GHX
   @version 2021-11-05
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'element', 'layer'], function () {
	"use strict";
	var $ = layui.$,
		element = layui.element,
		layer = layui.layer,
		uxutil = layui.uxutil;
	var app = {};
	//get参数
	app.paramsObj = {
		sectionId: null, //小组ID
		sectionCName: null,//小组名称
		tabtype: 0
	};
	//初始化
	app.init = function () {
		var me = this;
		me.getParams();
		me.initListeners();
		me.inittab();
	};
	app.inittab = function () {
		var me = this;
		element.tabChange('Tab', 'testconfirm_tab_title' + app.paramsObj.tabtype);
	}
	//获得参数
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
	};
	//设置dom元素高度
	app.setDomHeight = function () {
		var me = this;
		//设置iframe父元素高度
		$(".layui-tab-content").css("height", ($(window).height() - 62) + "px");
	};
	//监听
	app.initListeners = function () {
		var me = this;
		// 窗体大小改变时，调整高度显示
        /* $(window).resize(function () {
            clearTimeout(iTime);
            iTime = setTimeout(function () {
                me.setDomHeight();
            }, 500);
        }); */
		element.on('tab(Tab)', function (data) {
			if (data.index == 0) {//未确认条码
				var url = uxutil.path.ROOT + '/ui/layui/views/pre/sample/barcode/model2/confirmBarCode.html';
				me.openTabsPage(url);
			} else if (data.index == 1) {//已确认条码
				var url = uxutil.path.ROOT + '/ui/layui/views/pre/sample/barcode/model2/notConfirmBarCode.html';
				me.openTabsPage(url);
			} 
		});
	};
	//打开标签页
	app.openTabsPage = function (url) {
		var str = '<div class="layui-tab-item layui-show"><iframe id=""  src=' + url + '  style="border: medium none;overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;"></iframe></div>';
		$('#testconfirm_tab_content').html(str);
	}
	//初始化
	app.init();
});