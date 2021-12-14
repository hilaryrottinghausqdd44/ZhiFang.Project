/**
	@name：打印格式列表
	@author：liangyl
	@version 2019-10-26
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	//获取打印格式列表数据
	var GET_PRINT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionPrintByHQL?isPlanish=true';
	var sectionprinttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:false,
			data:[],
			defaultOrderBy:"[{property: 'LBSectionPrint_LBItem_DispOrder',direction: 'ASC'}]",
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBSectionPrint_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBSectionPrint_LBSection_Id',title:'小组编号',width:150,sort:true,hide:true},
				{field:'LBSectionPrint_PrintFormat',title:'打印格式',width:150,sort:true},
				{field:'LBSectionPrint_PrintProgram',title:'打印程序',width:150,sort:true},
				{field:'LBSectionPrint_DefPrinter',title:'打印机',flex:1,sort:true,minWidth:150}

			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,sectionprinttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj,GroupID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_PRINT_LIST_URL+'&fields='+fields.join(',')+'&sort='+me.config.defaultOrderBy;
		var obj = {};
		if(GroupID)obj.where="lbsectionprint.LBSection.Id="+GroupID;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};
	//主入口
	sectionprinttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		result.loadData = me.loadData;
		if(me.config.defaultLoad){
			//加载数据
			result.loadData(me.config.where);
		}
		return result;
	};
	//暴露接口
	exports('sectionprinttable',sectionprinttable);
});