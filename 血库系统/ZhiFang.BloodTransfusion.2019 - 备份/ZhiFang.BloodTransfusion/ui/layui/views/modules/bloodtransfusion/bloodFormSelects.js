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
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentByHQL?isPlanish=true&fields=Department_Id,Department_CName&page=1&start=0&limit=10000",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			},
			/***
			 * @desc 医生等级下拉选择配置信息 
			 * @param {Object} inst:"BlooddocGrade"
			 * @param {Object} options:{}
			 */
			BlooddocGrade: function (options) {
				var config = {
					keyVal: 'BlooddocGrade_Id',
					keyName: 'BlooddocGrade_CName',
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorByHQL?isPlanish=true&fields=BlooddocGrade_Id,BlooddocGrade_CName&page=1&start=0&limit=1000",
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
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorByHQL?isPlanish=true&fields=Doctor_Id,Doctor_CName",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			},
			/***
			 * @desc 就诊类型下拉选择配置信息 
			 * @param {Object} inst:"BloodBReqType"
			 * @param {Object} options:{}
			 */
			BloodBReqType: function (options) {
				var config = {
					keyVal: 'BloodBReqType_Id',
					keyName: 'BloodBReqType_CName',//&page=1&start=0&limit=1000
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeByHQL?isPlanish=true&fields=BloodBReqType_Id,BloodBReqType_CName",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			},
			/***
			 * @desc 申请类型下拉选择配置信息 
			 * @param {Object} inst:"BloodUseType"
			 * @param {Object} options:{}
			 */
			BloodUseType: function (options) {
				var config = {
					keyVal: 'BloodUseType_Id',
					keyName: 'BloodUseType_CName',//&page=1&start=0&limit=1000
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseTypeByHQL?isPlanish=true&fields=BloodUseType_Id,BloodUseType_CName",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			},
			/***
			 * @desc 血制品单位下拉选择配置信息 
			 * @param {Object} inst:"BloodUnit"
			 * @param {Object} options:{}
			 */
			BloodUnit: function (options) {
				var config = {
					keyVal: 'BloodUnit_Id',
					keyName: 'BloodUnit_CName',//&page=1&start=0&limit=1000
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitByHQL?isPlanish=true&fields=BloodUnit_CName,BloodUnit_Id",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			},
			/***
			 * @desc 血制品分类下拉选择配置信息 
			 * @param {Object} inst:"BloodClass"
			 * @param {Object} options:{}
			 */
			BloodClass: function (options) {
				var config = {
					keyVal: 'BloodClass_Id',
					keyName: 'BloodClass_CName',//&page=1&start=0&limit=10000
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassByHQL?isPlanish=true&fields=BloodClass_Id,BloodClass_CName",
					beforeSuccess: function (id, url, searchVal, result) {
						return dataadapter.toList(result);
					},
					response: dataadapter.toResponse()
				};
				if (options) config = $.extend(true, {}, config, options);
				return config;
			},
			/***
			 * @desc 血制品下拉选择配置信息 
			 * @param {Object} inst:"Department"
			 * @param {Object} options:{}
			 */
			Bloodstyle: function (options) {
				var config = {
					keyVal: 'Bloodstyle_Id',
					keyName: 'Bloodstyle_CName',//&page=1&start=0&limit=10000
					selectUrl: "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleByHQL?isPlanish=true&fields=Bloodstyle_Id,Bloodstyle_CName",
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