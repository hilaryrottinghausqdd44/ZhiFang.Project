/**
 * @name：modules/common/select/section 检验小组下拉框
 * @author：zhangda
 * @version 2020-09-15
 */
layui.extend({
	uxutil: 'ux/util',
	CommonSelectBasic: 'modules/common/select/basic'
}).define(['uxutil', 'CommonSelectBasic'], function (exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		CommonSelectBasic = layui.CommonSelectBasic,
		MOD_NAME = 'CommonSelectSection';

	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true' +
		'&sort=[{property:"LBRight_LBSection_DispOrder",direction:"ASC"}]' +
		"&fields=LBRight_LBSection_Id,LBRight_LBSection_CName" +
		"&where=lbright.RoleID is null and lbright.EmpID=" + uxutil.cookie.get(uxutil.cookie.map.USERID);

	//下拉框基础类
	var Module = {
		//对外参数
		config: {
			domId: null,

			url: SELECT_URL,//服务地址，包含所有参数
			keyField: 'LBRight_LBSection_Id',//值字段
			valueField: 'LBRight_LBSection_CName',//显示文字字段

			defaultName: '请选择',//默认文字显示内容
			isFromRender: true,//加载完数据后是否立即表单渲染
			afterLoad: function (data) { return data; },//数据加载后未渲染前处理
			done: function (instance) { }//下拉框渲染完毕后回调
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, Module.config, setings);
	};

	//核心入口
	Module.render = function (options) {
		var me = new Class(options);

		CommonSelectBasic.render(me.config);

		return me;
	};

	//暴露接口
	exports(MOD_NAME, Module);
});