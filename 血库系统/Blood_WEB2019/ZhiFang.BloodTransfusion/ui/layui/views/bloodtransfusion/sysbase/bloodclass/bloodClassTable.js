layui.extend({
   	bloodBaseTable:"views/bloodtransfusion/sysbase/basic/bloodBaseTable"
}).define(['form', 'table', 'bloodBaseTable'], function(exports){
	"use strict";
	var $ = layui.$,
	    table = layui.table,
	    form = layui.form,
	    uxutil = layui.uxutil,
	    dataadapter = layui.dataadapter,
	    bloodBaseTable = layui.bloodBaseTable;
	    	
	var BloodClassTable = function(){
		var me = this,
		    fields = [];
		bloodBaseTable.constructor.call(me);
		//检索配置
		me.searchInfo.fields = me.searchInfo.fields || [];
		me.searchInfo.fields.push("bloodclass.Id");
		me.searchInfo.fields.push("bloodclass.CName");
		me.searchInfo.fields.push("bloodclass.ShortCode");
		me.config.height = 'full-150';
		me.config.elem = '#bloodclass_table';
		//数据操作URL
		me.config.addUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_AddBloodClass";
		me.config.editUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClassByField";
		me.config.delUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_DelBloodClass";
		me.config.defLoadUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassByHQL?isPlanish=true";
		//初始化字段列，这样写更直观，更简单
		me.config.cols = me.config.cols || [];
		fields.push({type:'numbers', sort: true, width: 55, title: '序号'});
		fields.push({field:'BloodClass_Id', sort: true, width: 150, title: '编号'});
		fields.push({field:'BloodClass_CName', sort: true, width: 150, title: '名称'});
		fields.push({field:'BloodClass_BCCode', sort: true, width: 150, title: '代码'});
		fields.push({field:'BloodClass_Iswarn', sort: true, width: 150, title: '是否预警'});
		fields.push({field:'BloodClass_IslargeUse', sort: true, width: 150, title: '计算用血'});
		fields.push({field:'BloodClass_Visible', sort: true, width: 150, title: '是否使用', 
		    templet: function(data){
				if (data["BloodClass_Visible"] == "true"){
					return "是";
				} else{
					return "否";	
				}
		    }
		  });
		fields.push({field:'BloodClass_DispOrder', sort: true, width: 150, title: '显示次序'});
		me.config.cols.push(fields);
		me.setDefaultWhere("bloodclass.Visible = 1"); //默认装载
	}
	
	
	BloodClassTable.prototype = bloodBaseTable;
	
    //核心入口
	BloodClassTable.prototype.render = function (options) {
		var me = this;
		me.config = $.extend({}, me.config, options);
		me.config.url = me.config.defLoadUrl;
		me.tableIns = table.render(me.config);
		return me;
	};
	
	var bloodClassTable = new BloodClassTable();
	exports("bloodClassTable", bloodClassTable);
})
