/**
	@name：layui系统运行参数
	@author：longfc
	@version 2020-05-06
 */
layui.extend({
	uxutil: 'ux/util',
	cachedata: '/views/modules/bloodtransfusion/cachedata'
}).define(['uxutil', 'jquery', 'cachedata'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	//var layer = layui.layer;
	var cachedata = layui.cachedata;
	var win = window;

	var runParams = {
		config: {

		},
		//系统运行参数集合信息
		JObjectList: null,
		//是否初始化运行参数
		ISInitRunParams: false,
		//初始化运行参数总数
		InitRunParamsCount: 13,
		/**初始化系统运行参数的某一个运行参数获取完成后处理*/
		getRunParamsAfter: function(callback, count) {
			if (count == this.InitRunParamsCount) {
				this.ISInitRunParams = true;
				cachedata.setCache("ISInitRunParams", true, win);
				if (callback) callback();
			}
		},
		/**
		 * @description 系统登录后初始化部分系统运行参数信息
		 * @param {Object} callback
		 */
		initRunParams: function(callback) {
			var me = this;
			var isInitRunParams = cachedata.getCache("ISInitRunParams");
			if (me.ISInitRunParams == true || isInitRunParams == true) {
				if (callback) {
					return callback();
				} else {
					return;
				}
			}

			var count = 0;
			//系统运行参数"启用用户UI配置"
			me.getRunParamsValue("EnableUserUIConfig", false, function(data) {
				if (data.value && data.value.ParaValue && data.value.ParaValue == 1) {
					//JShell.BLTF.BUserUIConfig.getUIConfigByUSERID("", null);
				}
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//用血申请传入的患者参数非空字段
			me.getRunParamsValue("NotNullParamField", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//系统运行参数"列表默认分页记录数"
			me.getRunParamsValue("BLTFUIDefaultPageSize", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//系统运行参数"血袋扫码识别字段"
			me.getRunParamsValue("BloodBagScanCodeIDField", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//系统运行参数"输血登记是否需要交接登记完成后"
			me.getRunParamsValue("BloodTransISNeedBloodBag", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//系统运行参数"CS服务访问URL"
			me.getRunParamsValue("CSServiceAccessURL", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//是否允许手工选择患者ABO及患者Rh
			me.getRunParamsValue("IsAllowPatabBOAndRHOPT", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//获取几天内的LIS检验结果
			me.getRunParamsValue("GetLisResultDays", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//结果为空时默认值
			me.getRunParamsValue("LisDefaulltItemsResult", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//用血申请确认后是否自动完成审批
			me.getRunParamsValue("ConfirmedIsAutoCompleted", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//用血申请审核完成后是否返回给HIS
			me.getRunParamsValue("ReviewCompletedIsToHIS", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//紧急用血是否在用血申请确认提交时上传HIS
			me.getRunParamsValue("IsBUseTimeTypeIDAUToUHis", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
			//申请作废时是否调用HIS作废接口
			me.getRunParamsValue("IsToHISOBSOLETE", false, function(data) {
				count++;
				me.getRunParamsAfter(callback, count);
			});
		},
		getRunParamsList: function() {},
		/**
		 * @description 旧的运行参数集合
		 */
		Lists: {
			/**@description 集成平台服务访问URL*/
			IPlatformServiceAccessURL: {
				Id: "BL-SYSE-LURL-0002",
				CName: "集成平台服务访问URL"
			},
			/**列表默认分页记录数*/
			BLTFUIDefaultPageSize: {
				Id: "BL-LRMP-UIPA-0007",
				CName: "列表默认分页记录数"
			},
			/**@description 启用用户UI配置*/
			EnableUserUIConfig: {
				Id: "BL-EUSE-UICF-0008",
				CName: "启用用户UI配置"
			},
			/**@description CS服务访问URL*/
			CSServiceAccessURL: {
				Id: "BL-SYSE-CSRL-0011",
				CName: "CS服务访问URL"
			},
			/**@description 是否输血过程记录登记后才能血袋回收登记*/
			RecycleIsHasTrans: {
				Id: "BL-BBBO-BBRR-0012",
				CName: "是否输血过程记录登记后才能血袋回收登记"
			},
			/**@description 获取几天内的LIS检验结果*/
			GetLisResultDays: {
				Id: "BL-LISG-DAYS-0015",
				CName: "获取几天内的LIS检验结果"
			},
			/**@description LIS结果为空时默认值*/
			LisDefaulltItemsResult: {
				Id: "BL-LISR-DEVL-0016",
				CName: "LIS结果为空时默认值"
			},
			/**@description 用血申请传入的患者参数非空字段*/
			NotNullParamField: {
				Id: "BL-HISN-FIED-0017",
				CName: "用血申请传入的患者参数非空字段"
			},
			/**@description 用血申请审核完成后是否返回给HIS*/
			ReviewCompletedIsToHIS: {
				Id: "BL-ISTO-HISR-0018",
				CName: "用血申请审核完成后是否返回给HIS"
			},
			/**@description 申请作废时是否调用HIS作废接口*/
			IsToHISOBSOLETE: {
				Id: "BL-ISTO-HISO-0019",
				CName: "申请作废时是否调用HIS作废接口"
			},
			/**@description 紧急用血是否在用血申请确认提交时上传HIS*/
			IsBUseTimeTypeIDAUToUHis: {
				Id: "BL-ISTO-HISJ-0020",
				CName: "紧急用血是否在用血申请确认提交时上传HIS"
			},
			/**@description 是否允许手工选择患者ABO及患者Rh*/
			IsAllowPatabBOAndRHOPT: {
				Id: "BL-NULL-ISBH-0021",
				CName: "是否允许手工选择患者ABO及患者Rh"
			},
			/**@description 血袋扫码识别字段*/
			BloodBagScanCodeIDField: {
				Id: "BL-BBSC-IDFT-0022",
				CName: "血袋扫码识别字段"
			},
			/**@description 输血登记是否需要交接登记完成后*/
			BloodTransISNeedBloodBag: {
				Id: "BL-BLTF-ISNB-0023",
				CName: "输血登记是否需要交接登记完成后"
			},
			/**@description 用血申请确认后是否自动完成审批*/
			ConfirmedIsAutoCompleted: {
				Id: "BL-BLCF-ISAC-0024",
				CName: "用血申请确认后是否自动完成审批"
			}
		},
		/***
		 * @description 获取运行参数值
		 * @param {Object} paraKey 运行参数key
		 * @param {Object} isRefresh 是否重新获取运行参数值
		 * @param {Object} callback 回调
		 */
		getRunParamsValue: function(paraKey, isRefresh, callback) {
			var me = this;
			//运行参数编码为空
			var result = {
				success: true,
				value: {
					ParaValue: null
				},
				msg: ""
			};
			if (!paraKey) {
				result.success = false;
				if (callback) {
					return callback(result);
				} else {
					return;
				}
			}
			if (!isRefresh) {
				var paraValue1 = cachedata.getCache(paraKey);
				if (!paraValue1) isRefresh = true;
				//运行参数值已存在,且不用重新调用服务获取,直接返回
				if (!isRefresh) {
					result.success = true;
					result.value.ParaValue = paraValue1;
					if (callback) {
						return callback(result);
					} else {
						return;
					}
				}
			}
			var paraNo = me.Lists[paraKey].Id;
			var url = uxutil.path.ROOT + "/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=" + paraNo +
				"&t=" + (new Date().getTime());
			var config = {
				url: url
			};
			uxutil.server.ajax(config, function(data) {
				var paraValue = "";
				if (data.success) {
					var obj = data.value;
					if (obj.ParaValue) paraValue = obj.ParaValue;
					if (!paraValue && paraValue != 0) paraValue = "";
					cachedata.setCache(paraKey, paraValue, win);
				}
				me.Lists[paraKey].Value = paraValue;
				if (callback) {
					return callback(data);
				} else {
					return;
				}
			});
		},
		/**
		 * @description 刷新同步Bloodsconfig
		 * @param {Object} bloodsconfig
		 */
		renderBloodsconfig: function(bloodsconfig) {
			//CS血库服务端的域名或服务访问的IP
			var csbaseurl = cachedata.getCache(bloodsconfig.cachekeys.CSBASEURL_KEY);
			if (csbaseurl) {
				bloodsconfig.setCsBaseUrl(csbaseurl);
			}
			//用血申请传入的患者参数非空字段
			var runPVal0 = cachedata.getCache("NotNullParamField");
			if (runPVal0 != "" && runPVal0 != undefined) {
				if (runPVal0 == "true" || runPVal0 == "1" || runPVal0 == true) {
					runPVal0 = true;
				} else if (runPVal0 == "false" || runPVal0 == "0" || runPVal0 == false) {
					runPVal0 = false;
				}
				bloodsconfig.HisInterface.NONEMPTYFIELD = runPVal0;
			}

			//医嘱申请获取患者的多少天内LIS检验结果
			var runPVal210 = cachedata.getCache("GetLisResultDays");
			if (runPVal210) {
				runPVal210 = parseInt(runPVal210);
				if (!runPVal210) runPVal210 = bloodsconfig.Common.GET_LISRESULT_DAYS;
				bloodsconfig.Common.GET_LISRESULT_DAYS = runPVal210;
			}

			//新增医嘱申请时,检验项目LIS结果为空时,设置的默认值
			var runPVal211 = cachedata.getCache("LisDefaulltItemsResult");
			if (runPVal211 != undefined) {
				bloodsconfig.Common.LIS_DEFAULT_ITEMSRESULT = runPVal211;
			}

			//用血申请审核完成后是否返回给HIS
			var runPVal1 = cachedata.getCache("ReviewCompletedIsToHIS");
			if (runPVal1 != "" && runPVal1 != undefined) {
				if (runPVal1 == "true" || runPVal1 == "1" || runPVal1 == true) {
					runPVal1 = true;
				} else if (runPVal1 == "false" || runPVal1 == "0" || runPVal1 == false) {
					runPVal1 = false;
				}
				bloodsconfig.HisInterface.ISTOHISDATA = runPVal1;
			}

			//紧急用血是否在用血申请确认提交时上传HIS
			var runPVal2 = cachedata.getCache("IsBUseTimeTypeIDAUToUHis");
			if (runPVal2 != "" && runPVal2 != undefined) {
				if (runPVal2 == "true" || runPVal2 == "1" || runPVal2 == true) {
					runPVal2 = true;
				} else if (runPVal2 == "false" || runPVal2 == "0" || runPVal2 == false) {
					runPVal2 = false;
				}
				bloodsconfig.HisInterface.ISBUSETIMETYPEIDAUTOUPLOADADD = runPVal2;
			}

			//申请作废时是否调用HIS作废接口
			var runPVal3 = cachedata.getCache("IsToHISOBSOLETE");
			if (runPVal3 != "" && runPVal3 != undefined) {
				if (runPVal3 == "true" || runPVal3 == "1" || runPVal3 == true) {
					runPVal3 = true;
				} else if (runPVal3 == "false" || runPVal3 == "0" || runPVal3 == false) {
					runPVal3 = false;
				}
				bloodsconfig.HisInterface.ISTOHISOBSOLETE = runPVal3;
			}

			//用血申请确认后是否自动完成审批
			var runPVal4 = cachedata.getCache("ConfirmedIsAutoCompleted");
			if (runPVal4 != "" && runPVal4 != undefined) {
				if (runPVal4 == "true" || runPVal4 == "1" || runPVal4 == true) {
					runPVal4 = true;
				} else if (runPVal4 == "false" || runPVal4 == "0" || runPVal4 == false) {
					runPVal4 = false;
				}
				bloodsconfig.Common.ConfirmedIsAutoCompleted = runPVal4;
			}

			//是否允许手工选择患者ABO及患者Rh
			var runPVal5 = cachedata.getCache("IsAllowPatabBOAndRHOPT");
			if (runPVal5 != "" && runPVal5 != undefined) {
				if (runPVal5 == "true" || runPVal5 == "1" || runPVal5 == true) {
					runPVal5 = true;
				} else if (runPVal5 == "false" || runPVal5 == "0" || runPVal5 == false) {
					runPVal5 = false;
				}
				bloodsconfig.HisInterface.ISALLOWPATABOANDRHOPT = runPVal5;
			}

			layui.bloodsconfig = bloodsconfig;
			return bloodsconfig;
		}
	};

	//暴露接口
	exports('runParams', runParams);
});
