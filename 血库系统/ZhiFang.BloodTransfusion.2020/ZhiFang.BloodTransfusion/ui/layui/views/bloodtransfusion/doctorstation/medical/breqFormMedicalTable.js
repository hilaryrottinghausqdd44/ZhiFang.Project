/**
	@name：医嘱申请-医务处审批
	@author：longfc
	@version 2019-07-06
 */
layui.extend({
	breqFormTable: '/views/bloodtransfusion/doctorstation/basic/breqFormTable'
}).define(['table', 'uxutil', 'dataadapter', 'breqFormTable'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var breqFormTable = $.extend(true, {}, layui.breqFormTable);

	var breqFormMedicalTable = {
		config: {
			elem: '',
			toolbar: "",
			//是否包含默认申请状态查询信息
			hasStausWhere:true,
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
			selectUrl: uxutil.path.ROOT +
				"/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormEntityListByHql?isPlanish=true"
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		//me =$.extend(true,{}, breqFormTable);
		me.config = $.extend({}, breqFormTable.config, breqFormMedicalTable.config, options);
		var inst = $.extend(true, {}, breqFormTable, breqFormMedicalTable, me); //table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//获取查询Url
	breqFormMedicalTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//列表申请状态处理
	breqFormTable.getStausWhere = function() {
		var me = this;
		var stausWhere = [];
		//医嘱状态不等于暂存
		stausWhere.push("bloodbreqform.BreqStatusID!=1");
		//医嘱状态不等于提交申请
		stausWhere.push("bloodbreqform.BreqStatusID!=2");
		//医嘱状态不等于上级审核通过
		stausWhere.push("bloodbreqform.BreqStatusID!=3");
		//医嘱状态不等于上级审核退回
		stausWhere.push("bloodbreqform.BreqStatusID!=4");
		//医嘱状态不等于科主任审核退回
		stausWhere.push("bloodbreqform.BreqStatusID!=6");
		stausWhere = "(" + stausWhere.join(" and ") + ")";
		return stausWhere;
	};
	//核心入口
	breqFormMedicalTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		inst.getSearchWhere();
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqFormMedicalTable', breqFormMedicalTable);
});
