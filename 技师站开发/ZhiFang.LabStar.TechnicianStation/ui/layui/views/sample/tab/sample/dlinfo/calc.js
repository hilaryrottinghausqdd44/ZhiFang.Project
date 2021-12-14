/**
	@name：计算项目
	@author：liangyl	
	@version 2021-06-01
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		//获取计算项目
    var GET_ITEM_CACL_FORMULA_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcFormulaByHQL?isPlanish=true';
    var table_ind = null;
	var calctable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			defaultOrderBy:[{property:"LBItemCalcFormula_DispOrder",direction:"ASC"}],
			cols: [[
			    {field: 'LBItemCalcFormula_IsDefault',title: '默认',minWidth: 100,
	                templet: function (data) {
	                    var str = "<span>否</span> ";
	                    if (data.LBItemCalcFormula_IsDefault.toString() == "true") {
	                        str = "<span  style='color:red'>是</span>"
	                    }
	                    return str;
	                }
                }, 
                {field: 'LBItemCalcFormula_CalcFormulaInfo',title: '计算公式描述',minWidth: 200},
                {field: 'LBItemCalcFormula_FormulaConditionInfo',title: '计算条件描述',minWidth: 200},
                {field: 'LBItemCalcFormula_HAge',title: '年龄范围上限',width: 120}, 
                {field: 'LBItemCalcFormula_LAge',title: '年龄范围下限',width: 120},
                {field: 'LBItemCalcFormula_HValueComp',title: '范围高限对比类型',width: 160}, 
                {field: 'LBItemCalcFormula_UWeight', title: '体重上限',width: 80},
                {field: 'LBItemCalcFormula_LWeight',title: '体重下限',width: 80},
                {field: 'LBItemCalcFormula_IsUse',title: '是否使用',width: 100,
	                templet: function (data) {
	                    var str = "<span>否</span> ";
	                    if (data.LBItemCalcFormula_IsUse.toString() == "true") {
	                        str = "<span  style='color:red'>是</span>"
	                    }
	                    return str;
	                }
                }, {
	                field: 'LBItemCalcFormula_CalcType', title: '计算类型',width: 140,
	                templet: function (data) {
	                    var str = "<span>数值计算</span> ";
	                    if (data.LBItemCalcFormula_CalcType == 1) {
	                        str = "<span  style='color:red'>仅替换结果得到报告值</span>"
	                    }
	                    return str;
	                }
	            }, {
	                field: 'LBItemCalcFormula_IsKeepInvalid',title: '无效结果保留',width: 100,
	                templet: function (data) {
	                    var str = "<span>否</span> ";
	                    if (data.LBItemCalcFormula_IsKeepInvalid.toString() == "true") {
	                        str = "<span  style='color:red'>是</span>"
	                    }
	                    return str;
	                }
	            }, 
	            { field: 'LBItemCalcFormula_DispOrder',title: '显示次序',width: 100}
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
		},me.config,calctable.config,setings);
	};
	
	Class.pt = Class.prototype;
	
		//数据加载
	Class.pt.loadData = function(id){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_ITEM_CACL_FORMULA_LIST_URL+'&fields='+fields.join(',')+'&sort='+JSON.stringify(me.config.defaultOrderBy);
        url+= '&where=lbitemcalcformula.LBItem.Id='+id;
		me.instance.reload({
			url:url
		});
	};
	//主入口
	calctable.render = function(options){
		var me = new Class(options);
		table_ind = uxtable.render(me.config);
		
		table_ind.loadData = me.loadData;
	
		return table_ind;
	};
	//暴露接口
	exports('calctable',calctable);
});