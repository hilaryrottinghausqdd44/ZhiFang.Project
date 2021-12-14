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
		InitRunParamsCount: 4,
		/**初始化系统运行参数的某一个运行参数获取完成后处理*/
		getRunParamsAfter: function(callback, count) {
			if (count == this.InitRunParamsCount) {
				this.ISInitRunParams = true;
				cachedata.setCache("ISInitRunParams", true, win);
				if (callback) callback();
			}
		},
		/**系统登录后初始化部分系统运行参数信息*/
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
			JcallShell.BLTF.RunParams.getRunParamsValue("BloodTransISNeedBloodBag", false, function(data) {
				count++;
				JcallShell.BLTF.RunParams.getRunParamsAfter(callback, count);
			});
		},
		getRunParamsList: function() {},
		/**旧的运行参数集合*/
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
			/**@description 是否允许手工选择患者ABO及患者Rh*/
			ISALLOWPATABOANDRHOPT: {
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
			}
		},
		/***
		 * 获取运行参数值
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
			var url = uxutil.path.ROOT + "/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=" + paraNo +
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
		}

	};

	//暴露接口
	exports('runParams', runParams);
});
