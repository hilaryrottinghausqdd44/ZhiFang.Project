/**
 * 样本结果详细列表
 * @author liangyl	
 * @version 2021-05-27
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		
    /**获取样本单项目数据服务路径*/
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true';
    var table_ind = null;
	var resulttable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
		    defaultOrderBy:[{property:"LisTestItem_PLBItem_DispOrder",direction:"ASC"},{property:"LisTestItem_LBItem_DispOrder",direction:"ASC"}],
			cols:[[
				{field: 'LisTestItem_LisTestForm_Id',width: 60,title: '样本单ID',sort: true,hide: true},
                {field: 'LisTestItem_LBItem_Id',width: 60,title: '项目编号',sort: true,hide: true},
                {field:'LisTestItem_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
				{field:'LisTestItem_ReportValue', width:90, title: '项目结果', sort: true}			
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
		},me.config,resulttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		
	    me.instance.reload({
			url:GET_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:JSON.stringify(me.config.defaultOrderBy)
			})
		});
	};
		//数据加载
	Class.pt.clearData= function(whereObj){
		var filter = $(table_ind.config.elem).attr("lay-filter");
		table_ind.instance.config.instance.layMain.html('<div class="layui-none">暂无数据</div>');
	};
	//主入口
	resulttable.render = function(options){
		var me = new Class(options);
		table_ind = uxtable.render(me.config);
		table_ind.loadData = me.loadData;
		table_ind.clearData = me.clearData;
		return table_ind;
	};
	//暴露接口
	exports('resulttable',resulttable);
});