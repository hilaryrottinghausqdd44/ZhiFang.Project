layui.extend({
   	bloodBaseTable:'views/bloodtransfusion/sysbase/basic/bloodBaseTable'
}).define(['table', 'bloodBaseTable'], function(exports){
	"use strict";
	
	var $ = layui.$,
	    table = layui.table,
	    uxutil = layui.uxutil,
	    dataadapter = layui.dataadapter,
	    bloodBaseTable = layui.bloodBaseTable;
	    	
	var BloodUseTypeTable = function(){
		var me = this,
		    fields = [];
		bloodBaseTable.constructor.call(me);
		//检索配置
		me.searchInfo.fields = me.searchInfo.fields || [];
		me.searchInfo.fields.push("bloodusetype.Id");
		me.searchInfo.fields.push("bloodusetype.CName");
		me.searchInfo.fields.push("bloodusetype.ShortCode");
		me.config.height = 'full-150';
		me.config.elem = '#LAY-bloodusetype-table';
		//数据操作URL
		me.config.addUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodUseType";
		me.config.editUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseTypeByField";
		me.config.delUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodUseType";
		me.config.defLoadUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseTypeByHQL?isPlanish=true";
		//初始化字段列，这样写更直观，更简单
		me.config.cols = me.config.cols || [];
		fields.push({type:'numbers', sort: true, width: 55, title: '序号'});
		fields.push({field:'BloodUseType_Id', sort: true, width: 150, title: '类型编号'});
		fields.push({field:'BloodUseType_CName', sort: true, width: 150, title: '类型名称'});
		fields.push({field:'BloodUseType_BeforTime', sort: true, width: 150, title: '提前申请时间'});
		fields.push({field:'BloodUseType_BeforUnit', sort: true, width: 150, title: '申请时间单位'});
		fields.push({field:'BloodUseType_ShortCode', sort: true, width: 150, title: '简码'});
		fields.push({field:'BloodUseType_DispOrder', sort: true, width: 150, title: '显示次序'})
		fields.push({field:'BloodUseType_Visible', sort: true, width: 150, title: '是否使用', 
		    templet: function(data){
				if (data["BloodClass_Visible"] == "true"){
					return "是";
				} else{
					return "否";	
				}
		    }
		  });
		me.config.cols.push(fields);
		me.setDefaultWhere("bloodusetype.Visible = 1"); //默认装载
	}
	
	BloodUseTypeTable.prototype = bloodBaseTable;
	
	//核心入口
	BloodUseTypeTable.prototype.render = function (options) {
		var me = this;
		me.config = $.extend({}, me.config, options);
		me.config.url = me.config.defLoadUrl;
		me.tableIns = table.render(me.config);
		return me;
	};
	
	var bloodUseTypeTable = new BloodUseTypeTable();
	exports("bloodUseTypeTable", bloodUseTypeTable);
})
