/**
	@name：BS血库下拉选择框工具类
	@author：longfc
	@version 2019-06-25
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter'
}).define(['jquery', 'uxutil', 'dataadapter'], function (exports) {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;

	var bloodFormSelects = {
		/***
		 * @description 血库下拉选择框配置信息
		 */
		formSelectsConfig: {
			/***
			 * @desc 部门下拉选择配置信息 
			 * @param {Object} inst:"Department"
			 * @param {Object} options:{}
			 */
			Department: function (options) {
				var config = {
					keyVal: 'Department_Id',
					keyName: 'Department_CName',
					selectUrl: "/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDepartmentByHQL?isPlanish=true&fields=Department_Id,Department_CName&page=1&start=0&limit=10000",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			},
			/***
			 * @desc 医生下拉选择配置信息 
			 * @param {Object} inst:"Doctor"
			 * @param {Object} options:{}
			 */
			Doctor: function (options) {
				var config = {
					keyVal: 'Doctor_Id',
					keyName: 'Doctor_CName',//&page=1&start=0&limit=10000
					selectUrl: "/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDoctorByHQL?isPlanish=true&fields=Doctor_Id,Doctor_CName",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			}
			
		},
		/***
		 * @desc 血库下拉选择框集合内部封装 
		 * @desc bloodUtil.formSelects(inst,options).getConfig();
		 * @param {Object} inst:"Department"
		 * @param {Object} options:{}
		 */
		getFormSelectsConfig: function (inst, options) {
			var me = this;
			return me.formSelectsConfig[inst] && me.formSelectsConfig[inst](options);
		}
	};

	//暴露接口
	exports('bloodFormSelects', bloodFormSelects);
});