/**
 * @name：仪器项目结果列表
 * @author liangyl
 * @version 2021-05-07
 */
layui.extend({
	uxtable:'ux/table',
}).define(['uxutil','uxtable','table','form','uxbasic'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbasic = layui.uxbasic,
		uxtable = layui.uxtable;

  	 //获取仪器项目结果列表服务
	var GET_LIST_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true';

	var righttable = {
		//参数配置
		config:{
            page: false,
			limit: 5000,
			loading : true,
			defaultOrderBy: [{ property: 'LisEquipItem_IExamine', direction: 'DESC' }],
			cols:[[
//			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LisEquipItem_Id', width:180, title: '主键Id', sort: true,hide:true},
				{field:'LisEquipItem_LBItem_Id', width:180, title: '项目Id', sort: true,hide:true},
				{field:'LisEquipItem_LBItem_CName', width:180, title: '项目名称'},
				{field:'LisEquipItem_EReportValue', width:100, title: '项目结果'}
		    ]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
		
				var type = typeof res.ResultDataValue,
					data = res.ResultDataValue || {},
					list = [];
					
				if(res.success){
					if(type == 'string'){
						data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
					}
				}
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
		},me.config,righttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(TestFormID){
		var me = this;
	    Class.pt.onLoad(TestFormID,me);
	};
	//数据加载
	Class.pt.onLoad = function(id,me){
		var cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
		me.instance.reload({
			url:url,
			where:$.extend({},Class.pt.getWhere(id),{
				fields:fields.join(','),
				sort:JSON.stringify(me.config.defaultOrderBy)
			})
		});
	};
	Class.pt.getWhere = function(id){
		var params = [],
		    where = '';
		//样本单Id
		if(id) {
			params.push("EquipFormID=" + id + "");
		}else{
			params.push("EquipFormID=-1");
		}
		if(params.length > 0) {
			where+=  params.join(' and ');
		}
		return {"where":where};
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
	};
	//主入口
	righttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
//		result.instance.reload({data:[]});//清空列表数据
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('righttable',righttable);
});