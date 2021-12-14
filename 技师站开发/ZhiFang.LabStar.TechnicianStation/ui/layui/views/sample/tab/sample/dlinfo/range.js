/**
	@name：参考范围
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
	
		
	//获取参考范围数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeByHQL?isPlanish=true';
    var table_ind = null;
	var rangetable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			defaultOrderBy:[{property:"LBItemRange_DispOrder",direction:"ASC"}],
			cols:[[
			   {field: 'LBItemRange_IsDefault',title: '缺省',minWidth: 60,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBItemRange_IsDefault.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
               },
               {field: 'LBItemRange_RefRange',title: '参考范围描述',minWidth: 120}, 
               {field: 'LBItemRange_ConditionName',title: '条件说明',minWidth: 200},
               {field: 'LBItemRange_DispOrder',title: '判定次序',minWidth: 80},
               {field: 'LBItemRange_LValue',title: '范围低限',minWidth: 80}, 
               {field: 'LBItemRange_HValue',title: '范围高限',minWidth: 80},
               {field: 'LBItemRange_LLValue',title: '异常低限',minWidth: 80},
               {field: 'LBItemRange_HHValue',title: '异常高限',minWidth: 80}, 
               {field: 'LBItemRange_InvalidLValue', title: '无效低限',minWidth: 80},
               {field: 'LBItemRange_InvalidHValue',title: '无效高限',minWidth: 80}
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
		},me.config,rangetable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(itemid){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL+'&fields='+fields.join(',')+'&sort='+JSON.stringify(me.config.defaultOrderBy);
        url+= '&where=ItemID='+itemid;
		me.instance.reload({
			url:url
		});
	};
	
	//主入口
	rangetable.render = function(options){
		var me = new Class(options);
		table_ind = uxtable.render(me.config);
		
		table_ind.loadData = me.loadData;
	
		return table_ind;
	};
	//暴露接口
	exports('rangetable',rangetable);
});