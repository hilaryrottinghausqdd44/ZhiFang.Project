/**
 * 模块--表单（查询条件）/显示列 配置界面
 * @author 王耀宗
 * @version 2021-5-27
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'layer','element'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		layer = layui.layer, element = layui.element,
		$ = layui.jquery;
	var app = {};
	
	

	//服务地址
	app.url = {
		/**登录服务地址*/
		
	};

	//初始化  
	app.init = function () {
		var me = this;
		$("#leftContent").css("height", ($(document).height() - 25) + "px"); //设置左侧容器高度
		$("#rightContent").css("height", ($(document).height() - 25) + "px"); //设置右侧容器高度
		//this.getParams();
		
		//默认先加载医生站点的显示列配置
		var src = uxutil.path.UI + '/layui/class/setting/public/tableColumn/app.html';
		$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		me.listeners();
	};

	//监听事件
	app.listeners = function () {
		var me = this;
		//左侧的点击事件
		$('#columnDevelopSetting').on('click', function () {
			$(this).addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式	
			var src = uxutil.path.UI + '/layui/class/setting/public/tableColumn/app.html';
			$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		});
		$('#queryConditionsDevelopSetting').on('click', function () {
			$(this).addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			var src = uxutil.path.UI + '/layui/class/setting/public/formItem/app.html';
			$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		});
		
		$('#moduleFormGridLink').on('click', function () {
			$(this).addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			var src = uxutil.path.UI + '/layui/class/setting/moduleFormColumnConfiguration/module_form_grid_link/app.html?role=system';
			$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		});
	}

	app.init();
});