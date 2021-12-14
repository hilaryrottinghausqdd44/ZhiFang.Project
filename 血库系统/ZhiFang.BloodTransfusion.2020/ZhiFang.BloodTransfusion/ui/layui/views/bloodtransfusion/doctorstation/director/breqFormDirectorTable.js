/**
	@name：医嘱申请-科主任审核
	@author：longfc
	@version 2019-07-06
 */
layui.extend({
	breqFormTable: '/views/bloodtransfusion/doctorstation/basic/breqFormTable'
}).define(['table', 'uxutil', 'dataadapter', 'breqFormTable'], function (exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var breqFormTable = $.extend(true, {}, layui.breqFormTable);

	var breqFormDirectorTable = {
		config: {
			elem: '',
			toolbar: "",
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
			selectUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormEntityListByHql?isPlanish=true"
		}
	};
	//构造器
	var Class = function (options) {
		var me = this;
		//me =$.extend(true,{}, breqFormTable);
		me.config = $.extend({}, breqFormTable.config,breqFormDirectorTable.config,  options);
		var inst = $.extend(true, {},breqFormTable, breqFormDirectorTable, me);//table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//获取查询Url
	breqFormDirectorTable.getLoadUrl = function () {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//核心入口
	breqFormDirectorTable.render = function (options) {
		var me = this;
		var inst = new Class(options);
		inst.getSearchWhere();
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqFormDirectorTable', breqFormDirectorTable);
});