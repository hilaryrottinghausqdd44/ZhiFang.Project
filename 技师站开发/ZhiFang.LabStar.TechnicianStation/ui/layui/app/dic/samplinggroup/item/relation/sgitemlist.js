/**
	@name：采样组和项目关系查询采样组信息列表
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
	
	//获取根据采样组和项目关系查询采样组信息列表数据
	var GET_SAMPLINGITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QuerySamplingGroupIsMultiItem?isPlanish=true';

    var samplinggroupitemtable = {
		//参数配置
		config:{
             page: false,
			limit: 5000,
			loading : true,
			//多个采样组的项目TRUE,单个采样组项目false
			isMulti:true,
			defaultOrderBy:"[{property: 'LBSamplingGroup_DispOrder',direction: 'ASC'}]",
			cols:[[
//			    {type: 'numbers',title: '行号',fixed: 'left'},
//			    {field:'LBSamplingGroup_Id',title:'采样组编号',width:150,sort:true},
//			    {field:'LBSamplingGroup_DispOrder',title:'次序',width:100,sort:true},
				{field:'LBSamplingGroup_CName',title:'采样组',width:150,sort:true},
				{field:'LBSamplingGroup_LBTcuvete_CName',title:'采样管',width:100,sort:true},
				{field:'LBSamplingGroup_LBTcuvete_ColorValue',title:'采样管颜色',width:100,sort:true,hide:true}
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
		},me.config,samplinggroupitemtable.config,setings);
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
		var url = GET_SAMPLINGITEM_LIST_URL;
		
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				isMulti : me.config.isMulti,
				sort:me.config.defaultOrderBy
			})
		});
	};
   
  	/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	Class.pt.doAutoSelect = function (rowIndex,that) {
		var  me = this;
		var data = table.cache[that.id] || [];
		if (!data || data.length <= 0) return;
		rowIndex = rowIndex || 0;
		var tableDiv = $("#"+that.id+"+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		var filter = $(that.elem).find('lay-filter');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function () {
				table.cache[that.id][index] = [];
				tr.remove();
				that.scrollPatch();
			},
			updte:{}
		};
		layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		thatrow.click();
	};
	//主入口
	samplinggroupitemtable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		result.loadData(me.config.where);

		return result;
	};
	//对外公开
	samplinggroupitemtable.onSearch = function(where){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SAMPLINGITEM_LIST_URL;
	    table.reload('onerow-samplinggroup-table',{
	    	url:url,
	    	where:{
	    		where:where,
	    		sort:me.config.defaultOrderBy,
	    		fields:fields.join(',')
	    	}
	    });
	};
	//暴露接口
	exports('samplinggroupitemtable',samplinggroupitemtable);
});