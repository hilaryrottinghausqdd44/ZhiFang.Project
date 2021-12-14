/**
 * @name：views/sample/cv/basic/info 危急值报告发送列表
 * @author：zhangda
 * @version 2021-09-06
 */
layui.extend({
	uxutil: 'ux/util'
}).define(['uxutil', 'form'], function (exports) {
	"use strict";

	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		MOD_NAME = 'info';

	//内部列表+表头dom
	var INFO_DOM = [
		'<form id="{formId}" lay-filter="{formId}" class="layui-form" style="padding:2px;margin-top:32px;margin-bottom:2px;border:1px solid #e6e6e6;">',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">检验小组:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LBSection_CName" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">检验日期:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_GTestDate" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">样本号:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_GSampleNo" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">姓名:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_CName" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">性别:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LisPatient_GenderName" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">年龄:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LisPatient_AgeDesc" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">科室:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LisPatient_DeptName" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">医生:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LisPatient_DoctorName" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">病区:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LisPatient_DistrictName" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">病床:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LisPatient_Bed" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'<div class="layui-form-item">',
		'<label class="layui-form-label">临床诊断:</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="LisTestFormMsg_LisTestForm_LisPatient_DiagName" placeholder="" readonly autocomplete="off" class="layui-input" />',
		'</div>',
		'</div>',
		'</form>',
		'<style>',
		'#{formId} .layui-form-label{width:60px;}',
		'#{formId} .layui-input-block{margin-left:70px;}',
		'</style>'
	];

	//医嘱单列表
	var info = {
		formId: null,//表单ID

		//对外参数
		config: {
			domId: null,
			height: null,

			values: {}//表单值集合
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, info.config, setings);

		me.formId = me.config.domId + "-form";
		if (me.config.height) {
			$("#" + me.formId).css("height", me.config.height);
		}

	};
	//初始化HTML
	Class.prototype.initHtml = function () {
		var me = this;
		var html = INFO_DOM.join("").replace(/{formId}/g, me.formId);
		$('#' + me.config.domId).append(html);
	};
	//监听事件
	Class.prototype.initListeners = function () {
		var me = this;
	};
	//赋值表单
	Class.prototype.setValues = function (obj) {
		var me = this,
			obj = obj || {};

		form.val(me.formId, obj);
	};
	//核心入口
	info.render = function (options) {
		var me = new Class(options);

		if (!me.config.domId) {
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//赋值
		if (me.config.values)
			me.setValues(me.config.values);
		//监听事件
		me.initListeners();

		return me;
	};

	//暴露接口
	exports(MOD_NAME, info);
});