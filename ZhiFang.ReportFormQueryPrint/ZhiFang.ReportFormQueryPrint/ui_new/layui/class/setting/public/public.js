/**
 * 有显示列和配置项的公共页面
 * @author 王耀宗
 * @version 2021-6-2
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'tree', 'element' ,'table'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		element = layui.element,
		tree = layui.tree,
		table = layui.table,
		$ = layui.jquery;
    
	var app = {};
    app.url = {
        //模块表单列表关系
		linkUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleModuleFormGridLinkByModuleID',
		GridListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridList',
        //某个列表的列集合（基础表）
        GridControlListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridControlListByGridID',
        //列表设置集合
		GridControlSetUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridControlSetByGridCode'
	};
	
	//get参数
	app.paramsObj = {
		ModuleID: '123',//模块id
		module:''//模块
	};

	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		//获取模块id
		if (params.MODULEID) {
			me.paramsObj.ModuleID = params.MODULEID;
		}
		if (params.MODULE) {
			me.paramsObj.module = params.MODULE;
		}
		
	};
	//初始化  
	app.init = function () {
		var me = this;
		$("#leftContent").css("height", ($(document).height() - 25) + "px"); //设置左侧容器高度
		$("#rightContent").css("height", ($(document).height() - 25) + "px"); //设置右侧容器高度
		this.getParams();
		//站点查询的页面有科室设置
		if (me.paramsObj.module == 'siteQuery') {
			$("#deptSetting").removeAttr("style");
			$("#column").removeAttr("style");
			$("#globalSetting").removeAttr("style");
			$("#queryConditions").removeAttr("style");
		} else if (me.paramsObj.module == 'focusPrint') {
			$("#column").css("display", "none"); 
			$("#globalSetting").css("display", "none");
			$("#deptSetting").css("display", "none");
			$("#queryConditions").removeAttr("style");
		} else if (me.paramsObj.module == 'labStar') {
			$("#column").removeAttr("style");
			$("#queryConditions").css("display", "none");
			$("#globalSetting").css("display", "none");
			$("#deptSetting").css("display", "none");
		}else {
			$("#column").removeAttr("style");
			$("#globalSetting").removeAttr("style");
			$("#queryConditions").removeAttr("style");
			$("#deptSetting").css("display", "none");
		}
		//默认先加载医生站点的显示列配置
		var src = uxutil.path.UI + '/layui/class/setting/public/tableColumn_client/app.html?ModuleID=' + me.paramsObj.ModuleID;
		$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		me.listeners();
	};

	//监听事件
	app.listeners = function () {
		var me = this;
		//左侧的点击事件
		$('#column').on('click', function () {
			$(this).addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式	
			var src = uxutil.path.UI + '/layui/class/setting/public/tableColumn_client/app.html?ModuleID=' + me.paramsObj.ModuleID;
			$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		});
		$('#queryConditions').on('click', function () {
			$(this).addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			var src = uxutil.path.UI + '/layui/class/setting/public/formItem_client/app.html?ModuleID=' + me.paramsObj.ModuleID;
			$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		});
		$('#deptSetting').on('click', function () {
			$(this).addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			var src = uxutil.path.UI + '/layui/class/setting/public/empDeptLinks/empDeptLinks.html?ModuleID=' + me.paramsObj.ModuleID + "&module=" + me.paramsObj.module;
			$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		});
		$('#globalSetting').on('click', function () {
			$(this).addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			var src = uxutil.path.UI + '/layui/class/setting/public/globalSetting/globalSetting.html?ModuleID=' + me.paramsObj.ModuleID + "&module=" + me.paramsObj.module;
			$('#rightContent').html('<iframe src="' + src + '" height="100%" width="100%" frameborder="0"' + '></iframe>');
		});
	}

	app.init();
});