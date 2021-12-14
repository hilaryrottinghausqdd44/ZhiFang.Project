layui.extend({
	bloodBaseTable:'views/bloodtransfusion/sysbase/basic/bloodBaseTable'
}).define(['table', 'form','uxutil', 'dataadapter','bloodBaseTable'], function(exports){
	"use strict";
	
    var $ = layui.$,
	    table = layui.table,
	    form = layui.form,
	    uxutil = layui.uxutil,
	    dataadapter = layui.dataadapter,
	    bloodBaseTable = layui.bloodBaseTable;

	
	var BloodTestItemTable = function() {
	    	var me = this,
	    	    fields = [];
            bloodBaseTable.constructor.call(me);
            //检索配置
			me.searchInfo.fields = me.searchInfo.fields || [];
			me.searchInfo.fields.push("bloodbtestitem.Id");
			me.searchInfo.fields.push("bloodbtestitem.CName");
			me.searchInfo.fields.push("bloodbtestitem.Shortcode");
			
			me.config.height = 'full-150';
			me.config.elem = '#LAY-bloodbtestitem-table';
			//数据操作URL
			me.config.addUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBTestItem";
			me.config.editUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBTestItemByField";
			me.config.delUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBTestItem";
			me.config.defLoadUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItemByHQL?isPlanish=true";
			//初始化字段列，这样写更直观，更简单
			me.config.cols = me.config.cols || [];
			fields.push({type:'numbers', sort: true, width: 55, title: '序号'});
			fields.push({field:'BloodBTestItem_Id', sort: true, width: 150, title: '项目 编码'});
			fields.push({field:'BloodBTestItem_CName', sort: true, width: 150, title: '项目名称'});
			fields.push({field:'BloodBTestItem_HisOrderCode', sort: true, width: 150, title: 'HIS对照码'});
			fields.push({field:'BloodBTestItem_EName', sort: true, width: 150, title: '英文名称'});
			fields.push({field:'BloodBTestItem_Shortcode', sort: true, width: 150, title: '简码'});
			fields.push({field:'BloodBTestItem_DispOrder', sort: true, width: 150, title: '显示次序'});
			fields.push({field:'BloodBTestItem_Visible', sort: true, width: 150, title: '是否使用',
				templet: function(data){
					if (data["BloodBTestItem_Visible"] == 1){
						return "是";
					} else{
						return "否";	
					}
				}
			});
			me.config.cols.push(fields);
			me.setDefaultWhere("bloodbtestitem.Visible = 1"); //默认装载
	    }
	    
	
	BloodTestItemTable.prototype = bloodBaseTable;
	
    //核心入口
	BloodTestItemTable.prototype.render = function (options) {
		var me = this;
		me.config = $.extend({}, me.config, options);
		me.config.url =  me.config.defLoadUrl;
		me.tableIns = table.render(me.config);
		return me;
	};
	
	var bloodTestItemTable = new BloodTestItemTable(); 
	//导出对象
	exports("bloodTestItemTable", bloodTestItemTable);
})