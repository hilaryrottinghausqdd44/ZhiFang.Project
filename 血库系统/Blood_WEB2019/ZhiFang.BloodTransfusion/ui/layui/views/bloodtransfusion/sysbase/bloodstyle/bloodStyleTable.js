layui.extend({
	bloodBaseTable: "views/bloodtransfusion/sysbase/basic/bloodBaseTable"
}).define(['table', 'bloodBaseTable'], function(exports){
	"use strict";
	
	var $ = layui.$;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var bloodBaseTable = layui.bloodBaseTable;
	
		
	var BloodStyleTable = function(){
		var me = this,
		    fields = [];
		bloodBaseTable.constructor.call(me);
		//检索配置
		me.searchInfo.fields = me.searchInfo.fields || [];
		me.searchInfo.fields.push("bloodstyle.Id");
		me.searchInfo.fields.push("bloodstyle.CName");
		me.searchInfo.fields.push("bloodstyle.ShortCode");
		
		me.config.height = 'full-120';
		me.config.elem = '#LAY-bloodstyle-table';
		//数据操作URL
		me.config.addUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_AddBloodstyle";
		me.config.editUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodstyleByField";
		me.config.delUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_DelBloodstyle";
		me.config.defLoadUrl = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleByHQL?isPlanish=true";
		//初始化字段列，这样写更直观，更简单
		me.config.cols = me.config.cols || []; 
		//fields.push({type:'numbers', sort: true, width: 55, title: '序号'});
		fields.push({type:'checkbox', fixed: 'left', title: '操作'});
		fields.push({field:'Bloodstyle_Id', sort: true, width: 150, title: '血制品编号'});
		fields.push({field:'Bloodstyle_CName', sort: true, width: 150, title: '血制品名称'});
		fields.push({field:'Bloodstyle_ShortCode', sort: true, width: 150, title: '简码'});
		fields.push({field:'Bloodstyle_BisDispCode', sort: true, width: 150, title: '血站对照码'});
		fields.push({field:'Bloodstyle_HemolysisTime', sort: true, width: 150, title: '溶血时间'});
		fields.push({field:'Bloodstyle_HemolysisUnit', sort: true, width: 150, title: '溶血单位'});
		fields.push({field:'Bloodstyle_BloodScale', sort: true, width: 150, title: '换算比率'});
		fields.push({field:'Bloodstyle_StoreDays', sort: true, width: 150, title: '贮存时长'});
		fields.push({field:'Bloodstyle_StoreUnit', sort: true, width: 150, title: '贮存时长单位'});
		fields.push({field:'Bloodstyle_BloodClass_Id', sort: true, width: 150, title: '分类编号'});
		fields.push({field:'Bloodstyle_IsMakeBlood', sort: true, width: 150, title: '是否配血'});
		fields.push({field:'Bloodstyle_StoreCondNo', sort: true, width: 150, title: '贮存温度'});
		fields.push({field:'Bloodstyle_WarnDays', sort: true, width: 150, title: '预警时长'});
		fields.push({field:'Bloodstyle_WarnUnit', sort: true, width: 150, title: '预警单位'});
		fields.push({field:'Bloodstyle_BloodUnit_Id', sort: true, width: 150, title: '单位'});
		fields.push({field:'Bloodstyle_HisDispCode', sort: true, width: 150, title: 'HIS对照吗'});
		fields.push({field:'Bloodstyle_DispOrder', sort: true, width: 150, title: '显示次序'});
		fields.push({field:'Bloodstyle_Visible', sort: true, width: 150, title: '是否使用',
		    templet: function(data){
				if (data["Bloodstyle_Visible"] == '1'){
					return "是";
				} else{
					return "否";	
				}
		    }
		  });
		fields.push({width:120, align:'center', fixed: 'right', toolbar: '#LAY-tool-operate'}) ; 
		me.config.cols.push(fields);
		me.setDefaultWhere("bloodstyle.Visible = 1"); //默认装载
	}
	
	
	BloodStyleTable.prototype = bloodBaseTable;
	
	//核心入口
	BloodStyleTable.prototype.render = function (options) {
		var me = this;
		me.config = $.extend({}, me.config, options);
		me.config.url = me.config.defLoadUrl;
		me.tableIns = table.render(me.config);
		return me;
	};
	
	var bloodStyleTable = new BloodStyleTable();
	exports("bloodStyleTable", bloodStyleTable);	
	
})
