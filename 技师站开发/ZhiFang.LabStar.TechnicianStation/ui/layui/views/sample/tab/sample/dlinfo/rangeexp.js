/**
	@name：参考范围后处理
	@author：liangyl	
	@version 2021-06-01
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
	
		
	  //获取项目后处理
    var GET_ITEM_RANGE_EXP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeExpByHQL?isPlanish=true';
    var table_ind = null;
	var rangeexptable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			defaultOrderBy:"[{'property':'LBItemRangeExp_DispOrder','direction':'ASC'}]",
			cols: [[
				{field: 'LBItemRangeExp_DispOrder',title: '判定次序',minWidth: 100},
				{field: 'LBItemRangeExp_JudgeValue',title: '判定值',minWidth: 100,sort: true},
				{field: 'LBItemRangeExp_ResultReport',title: '报告值',minWidth: 100},
				{field: 'LBItemRangeExp_IsAddReport',title: '报告值是否替换',minWidth: 140,
		            templet: function (data) {
		                var str = "<span>否</span> ";
		                if (data.LBItemRangeExp_IsAddReport.toString() == "true") {
		                    str = "<span  style='color:red'>是</span>"
		                }
		                return str;
		            }
		        }, 
		        {field: 'LBItemRangeExp_ResultComment',title: '结果说明',minWidth: 140}
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
		},me.config,rangeexptable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
//	Class.pt.loadData = function(where){
//		var  me = this,
//  		cols = me.config.cols[0],
//			fields = [];
//	     
//		for(var i in cols){
//			if(cols[i].field)fields.push(cols[i].field);
//		}
//		var url = GET_ITEM_RANGE_EXP_LIST_URL+'&fields='+fields.join(',')+'&sort='+JSON.stringify(me.config.defaultOrderBy);
//      url+= '&where='+where;
//		me.instance.reload({
//			url:url
//		});
//	};
	 //参考范围后处理查询
	Class.pt.loadData = function(where,callback){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var	url = GET_ITEM_RANGE_EXP_LIST_URL+'&where='+where;
		url += '&fields=' + fields.join(',');
		url+='&sort='+table_ind.config.defaultOrderBy;
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.msg);
			}
		});
	}
	//主入口
	rangeexptable.render = function(options){
		var me = new Class(options);
		table_ind = uxtable.render(me.config);
		
		table_ind.loadData = me.loadData;
	
		return table_ind;
	};
	//暴露接口
	exports('rangeexptable',rangeexptable);
});