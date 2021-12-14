/**
	@name：采样组列表(所有采样组名称及采样管名称及颜色)
	@author：liangyl
	@version 2019-09-30
 */
layui.extend({
	
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取采样组列表数据
	var GET_SAMPLINGGROUP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QueryLBSamplingGroupByHQL?isPlanish=true';

	var samplinggrouptable = {
		//参数配置
		config:{
             page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBSamplingGroup_DispOrder',direction: 'ASC'}]",
			cols:[[
//			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBSamplingGroup_CName',title:'采样组名称',width:150,sort:true},
				{field:'LBSamplingGroup_LBTcuvete_CName',title:'采样管',width:90,sort:true},
				{field:'LBSamplingGroup_LBTcuvete_ColorValue',title:'颜色',width:90,hide:true},
				{field:'LBSamplingGroup_Id',title:'采样组编号',width:150,sort:true,hide:true}

			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
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
		},me.config,samplinggrouptable.config,setings);
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
		var url = GET_SAMPLINGGROUP_LIST_URL;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	
   	//主入口
	samplinggrouptable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		result.loadData(me.config.where);

		return result;
	};
	//暴露接口
	exports('samplinggrouptable',samplinggrouptable);
});