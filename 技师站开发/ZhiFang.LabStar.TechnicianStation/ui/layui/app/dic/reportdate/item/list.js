/**
	@name：取单时间分类项目明细列表
	@author：liangyl
	@version 2019-10-15
 */
layui.extend({
	uxtable:'ux/table'
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取取单时间分类项目明细列表数据
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QueryLBReportDateItemByHQL?isPlanish=true';
    //分类ID
    var REPORTDATEID = "";
    var table_ind = null;
	var ReportDateItemList = {
		//参数配置
		config:{
            page: true,
			limit: 50,
			loading : true,
			defaultLoad:false,
			data:[],
			defaultOrderBy:"[{property: 'LBReportDateItem_LBItem_DispOrder',direction: 'ASC'}]",
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBReportDateItem_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBReportDateItem_LBItem_Id',title:'项目编号',width:150,sort:true,hide:true},
				{field:'LBReportDateItem_LBItem_CName',title:'项目名称',flex:1,sort:true}
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
		},me.config,ReportDateItemList.config,setings);
	};
	
	Class.pt = Class.prototype;

	//数据加载
	Class.pt.loadData = function(LBReportDateID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_ITEM_LIST_URL;
		var whereObj = {"where":'lbreportdateitem.LBReportDate.Id='+LBReportDateID};
        var obj = {
        	fields:fields.join(','),
			sort:me.config.defaultOrderBy
        };
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};
    Class.pt.clearData = function(){
		var  me = this;
	    REPORTDATEID = null;
		
		me.instance.reload({
			url:'',
			data:[]
		});
	};
	//打开新增取单分类项目
	Class.pt.openWin = function(LBReportDateID,LBReportDateCName,callback){
		var win = $(window),
		    maxWidth = win.width()-100,
			maxHeight = win.height()-80,
			width = maxWidth > 800 ? maxWidth : 800,
			height = maxHeight > 380 ? maxHeight : maxHeight;
		var flag = false;
		layer.open({
            type: 2,
		    area:[width+'px',height+'px'],
            fixed: false,
            maxmin: false,
            title:'选择取单分类项目',
            content: 'item/transfer/app.html?ReportDateID='+LBReportDateID+'&ReportDateCName='+LBReportDateCName,
            cancel: function (index, layero) {
            	flag = true;
//              parent.layer.closeAll('iframe');
            },
            success: function(layero, index){
	        },
	        end : function() {
	        	if(flag)return;
	        	callback();
	        }
        });
	};
		//联动监听
	Class.pt.initListeners = function(){
		var me = this;
		
	};
	//主入口
	ReportDateItemList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		result.openWin = me.openWin;
		result.clearData = me.clearData;
		me.initListeners();
		return result;
	};
	//暴露接口
	exports('ReportDateItemList',ReportDateItemList);
});