layui.extend({
	bloodBaseTable:'views/bloodtransfusion/sysbase/basic/bloodBaseTable'
}).define(['form', 'table', 'bloodBaseTable'], function(exports){
	"use strict";
	
	var $ = layui.$,
	    table = layui.table,
	    form = layui.form,
	    uxutil = layui.uxutil,
	    dataadapter = layui.dataadapter,
	    bloodBaseTable = layui.bloodBaseTable;
	   	
	var BloodUnitTable = function(){
		var me = this,
		    fields = [];
		bloodBaseTable.constructor.call(me);
		//检索配置
		me.searchInfo.fields = me.searchInfo.fields || [];
		me.searchInfo.fields.push("bloodunit.Id");
		me.searchInfo.fields.push("bloodunit.CName");
		me.searchInfo.fields.push("bloodunit.ShortCode");
		
		me.config.height = 'full-100';
		me.config.elem = '#LAY-bloodunit-table';
		//me.config.toolbar = '#LAY-app-table-toolbar-bloodunit';
		//数据操作URL
		me.config.addUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodUnit";
		me.config.editUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUnitByField";
		me.config.delUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodUnit";
		me.config.defLoadUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitByHQL?isPlanish=true";
		//初始化字段列，这样写更直观，更简单
		me.config.cols = me.config.cols || [];
		fields.push({type:'numbers', sort: true, width: 55, title: '序号'});
		fields.push({field:'BloodUnit_Id', sort: true, width: 150, title: '单位编号'});
		fields.push({field:'BloodUnit_CName', sort: true, width: 150, title: '单位名称'});
		fields.push({field:'BloodUnit_EName', sort: true, width: 150, title: '英文名称'});
		fields.push({field:'BloodUnit_BloodScale', sort: true, width: 150, title: '换算比率'});
		fields.push({field:'BloodUnit_BloodScaleUnit', sort: true, width: 150, title: '换算单位'});
		fields.push({field:'BloodUnit_Visible', sort: true, width: 150, title: '是否使用'});
		fields.push({field:'BloodUnit_HisDispCode', sort: true, width: 150, title: 'HIS对照码'});
		fields.push({field:'BloodUnit_ShortCode', sort: true, width: 150, title: '简码'});
		fields.push({field:'BloodUnit_DispOrder', sort: true, width: 150, title: '显示次序'});
		me.config.cols.push(fields);
		me.setDefaultWhere("bloodunit.Visible = 1"); //默认装载
	}
	
	BloodUnitTable.prototype = bloodBaseTable;
	
	//核心入口
	BloodUnitTable.prototype.render = function (options) {
		var me = this;
		me.config = $.extend({}, me.config, options);
		me.config.url = me.config.defLoadUrl;
		me.tableIns = table.render(me.config);
		return me;
	};
	
	var bloodUnitTable = new BloodUnitTable(); 
	
	exports("bloodUnitTable", bloodUnitTable);
	
})