/**
	@name：医嘱申请主单列表
	@author：longfc
	@version 2019-07-2
 */
layui.extend({
	bloodsconfig: 'ux/bloodsconfig',
	breqFormTable: '/views/bloodtransfusion/doctorstation/basic/breqFormTable'
}).define(['table', 'uxutil', 'dataadapter', 'breqFormTable',"bloodsconfig"], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var breqFormTable = $.extend(true, {}, layui.breqFormTable);
	var bloodsconfig=layui.bloodsconfig;

	var breqFormApplyTable = {
		config: {
			elem: '',
			toolbar:'default',
			/**默认传入参数*/
			defaultParams: {

			},
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**列表当前排序*/
			sort: [{
				"property": 'BloodBReqForm_ReqTime',
				"direction": 'DESC'
			},{
				"property": 'BloodBReqForm_PrintTotal',
				"direction": 'ASC'
			},{
				"property": 'BloodBReqForm_PatNo',
				"direction": 'ASC'
			}],
			selectUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormEntityListByHql?isPlanish=true",
			confirmUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormOfConfirmApplyByReqFormId"
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		//me =$.extend(true,{}, breqFormTable);
		me.config = $.extend({}, breqFormApplyTable.config, me.config, options);
		var inst = $.extend(true, {},breqFormTable, breqFormApplyTable, me); //table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//获取查询Url
	breqFormApplyTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//确认申请提交
	breqFormApplyTable.onConfirmApply = function(data, callback) {
		var me = this;
		var reqFormId = data["BloodBReqForm_Id"];
		var statusId = data["BloodBReqForm_BreqStatusID"];
		//判断是否可以进行确认申请提交操作:暂存,上级审核退回
		if(statusId != "1" && statusId != "4") {
			var satusName = data["BloodBReqForm_BreqStatusName"];
			parent.layer.open({
				type: 1,
				offset: "auto",
				content: '<div style="padding: 20px 10px;">当前医嘱状态为:<span style="color:red;">' + satusName + '</span>,不能操作!</div>',
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
			return;
		}
		var autoupload=false;
		var userInfo=bloodsconfig.getCurOper();
		var empID = userInfo.empID;
		var empName = userInfo.empName;
		var fields="Id,BreqStatusID";
		var entity={
			"Id":reqFormId,
			"BreqStatusID":2,
			"ApplyID":empID,
			"ApplyName":empName
		};
		var useTimeTypeId=""+data["BloodBReqForm_BUseTimeTypeID"];
		//紧急用血自动完成审核
		if (useTimeTypeId=="1"&&bloodsconfig.HisInterface.ISBUSETIMETYPEIDAUTOUPLOADADD==true) {
			entity.BreqStatusID = 9; //审批完成
			entity.CheckCompleteFlag = 1; //审核完成
			fields=fields+",CheckCompleteFlag";
			autoupload = true; //需要自动上传
		}
		var params={
			"entity":entity,
			"fields":fields,
			"empID":empID,
			"empName":empName
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
		layer.load();
		uxutil.server.ajax(config, function (result) {
			layer.closeAll('loading');
			//隐藏遮罩层
			if (result.success==false) {
				layer.msg(result.msg);
			}
			if (callback) callback(result);
		});
	};
	//核心入口
	breqFormApplyTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		inst.getSearchWhere();
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqFormApplyTable', breqFormApplyTable);
});