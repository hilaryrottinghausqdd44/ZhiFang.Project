/**
	@name：医嘱申请+(包括医嘱申请,上级审核,科主任审核)
	@author：longfc
	@version 2019-08-21
 */
layui.extend({
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig',
	runParams: '/config/runParams',
	breqFormTable: '/views/bloodtransfusion/doctorstation/basic/breqFormTable'
}).define(['table', 'uxutil', 'dataadapter', 'breqFormTable', "cachedata", "runParams", 'bloodsconfig'], function(
	exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var breqFormTable = $.extend(true, {}, layui.breqFormTable);

	var bloodsconfig = layui.bloodsconfig;
	var runParams = layui.runParams;
	var cachedata = layui.cachedata;

	var breqFormApplyReviewTable = {
		config: {
			elem: '',
			toolbar: 'default',
			/**默认传入参数*/
			defaultParams: {

			},
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			sort: [{
				"property": 'BloodBReqForm_ReqTime',
				"direction": 'DESC'
			}, {
				"property": 'BloodBReqForm_PrintTotal',
				"direction": 'ASC'
			}, {
				"property": 'BloodBReqForm_PatNo',
				"direction": 'ASC'
			}],
			selectUrl: uxutil.path.ROOT +
				"/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormEntityListByHql?isPlanish=true",
			confirmUrl: uxutil.path.ROOT +
				"/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormOfConfirmApplyByReqFormId"
		}
	};
	/**
	 * @description 构造器
	 * @param {Object} options
	 */
	var Class = function(options) {
		var me = this;
		//me =$.extend(true,{}, breqFormTable);
		me.config = $.extend({}, breqFormApplyReviewTable.config, me.config, options);
		var inst = $.extend(true, {}, breqFormTable, breqFormApplyReviewTable, me); //table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	/**
	 * @description 获取查询Url
	 */
	breqFormApplyReviewTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	/**
	 * @description 刷新同步Bloodsconfig
	 */
	breqFormApplyReviewTable.renderBloodsconfig = function() {
		var me = this;
		runParams.initRunParams(function() {
			bloodsconfig = runParams.renderBloodsconfig(bloodsconfig);
		});
	};
	/**
	 * @description 确认申请提交
	 * @param {Object} data
	 * @param {Object} callback
	 */
	breqFormApplyReviewTable.onConfirmApply = function(data, callback) {
		var me = this;
		var reqFormId = data["BloodBReqForm_Id"];
		var statusId = data["BloodBReqForm_BreqStatusID"];
		//判断是否可以进行确认申请提交操作:暂存,上级审核退回
		if (statusId != "1" && statusId != "4") {
			var satusName = data["BloodBReqForm_BreqStatusName"];
			parent.layer.open({
				type: 1,
				offset: "auto",
				content: '<div style="padding: 20px 10px;">当前医嘱状态为:<span style="color:red;">' + satusName +
					'</span>,不能操作!</div>',
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
			return;
		}

		var userInfo = bloodsconfig.getCurOper();
		var empID = userInfo.empID;
		var empName = userInfo.empName;
		var autoupload = false;
		var fields = "Id,BreqStatusID";
		var entity = {
			"Id": reqFormId,
			"BreqStatusID": 2,
			"ApplyID": empID,
			"ApplyName": empName
		};

		//用血申请确认后是否自动完成审批
		var runPVal4 = cachedata.getCache("ConfirmedIsAutoCompleted");
		if (runPVal4 == "true" || runPVal4 == "1" || runPVal4 == true) {
			runPVal4 = true;
		} else if (runPVal4 == "false" || runPVal4 == "0" || runPVal4 == false) {
			runPVal4 = false;
		}
		if (runPVal4 == "" || runPVal4 == undefined) {
			runPVal4 = bloodsconfig.Common.ConfirmedIsAutoCompleted;
		}
		var useTimeTypeId = "" + data["BloodBReqForm_BUseTimeTypeID"];
		//紧急用血是否在用血申请确认提交时上传HIS
		var isU = bloodsconfig.HisInterface.ISBUSETIMETYPEIDAUTOUPLOADADD;

		//抢救用血或用血申请确认后是否自动完成审批为“是”时,自动完成审核
		if ((useTimeTypeId == "1" && isU == true) || (runPVal4 == true)) {
			entity.BreqStatusID = 9; //审批完成
			entity.CheckCompleteFlag = 1; //审核完成
			fields = fields + ",CheckCompleteFlag";
			autoupload = true; //需要自动上传
		}

		var params = {
			"entity": entity,
			"fields": fields,
			"empID": empID,
			"empName": empName
		};
		//配置类信息
		var bloodsConfigVO = {
			"Common": bloodsconfig.Common,
			"CSServer": bloodsconfig.CSServer,
			"HisInterface": bloodsconfig.HisInterface
		};
		params.bloodsConfigVO = bloodsConfigVO;

		params = JSON.stringify(params);

		var config = {
			type: "POST",
			url: me.config.confirmUrl,
			timeout: uxutil.BS_TIME_OUT,
			data: params
		};
		//显示遮罩层
		//layer.load();
		uxutil.server.ajax(config, function(result) {
			result["id"] = reqFormId;
			result["autoupload"] = autoupload;
			var msg = "";
			if (result.ErrorInfo && result.ErrorInfo.length > 0) {
				msg = result.ErrorInfo;
			} else if (result.msg && result.msg.length > 0) {
				msg = result.msg;
			}
			//是否需要显示审批提示信息
			if (entity.CheckCompleteFlag && entity.CheckCompleteFlag == 1) {
				result["reviewTips"] = "";
			} else {
				result["reviewTips"] = msg;
			}
			//隐藏遮罩层
			if (result.success == false) {
				layer.msg(msg);
			}
			if (callback) callback(result);
		});
	};
	//核心入口
	breqFormApplyReviewTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		inst.renderBloodsconfig();
		inst.getSearchWhere();
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqFormApplyReviewTable', breqFormApplyReviewTable);
});
