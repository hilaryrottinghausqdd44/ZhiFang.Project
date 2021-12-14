/**
 * @name：检验单样本单信息
 * @author liangyl
 * @version 2021-05-07
 */
layui.extend({
	uxtable:'ux/table',
	basicStatus:'views/sample/basic/status'//状态公共方法
}).define(['uxutil','uxtable','table','form','uxbasic','basicStatus'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbasic = layui.uxbasic,
		basicStatus = layui.basicStatus,
		uxtable = layui.uxtable;
	
		
	//获取样本单列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormBySampleNo?isPlanish=true';
	//默认查询条件
	var DEFAULTWHERE = 'listestform.MainStatusID=2';
    //查询条件内容
    var SERACHOBJ={};

	var formtable = {
		//参数配置
		config:{
            page: false,
			limit: 5000,
			loading : true,
			defaultOrderBy:[{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}],
			cols:[[
			    {type: 'checkbox', fixed: 'left'},
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LisTestForm_Id', width:180, title: 'ID', sort: true,hide:true},
				{field:'LisTestForm_GTestDate', width:100, title: '检验日期', templet:function(record){
					var value = record["LisTestForm_GTestDate"],
	                    v = uxutil.date.toString(value, true) || '';
	                return v;
				}},
				{field:'LisTestForm_GSampleNo', width:80, title: '样本号' },
				{field:'LisTestForm_BarCode', width:70, title: '条码号'},
				{field:'LisTestForm_CName', width:70, title: '姓名'},
				{field:'LisTestForm_MainStatusID',width:70,title:'状态', templet: function (data) { return basicStatus.onStatusRenderer(data); }}
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
					for(var i in data.list){
						list.push(me.changeData(data.list[i],i));
					}
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
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
		},me.config,formtable.config,setings);
	};
	
	Class.pt = Class.prototype;

	//数据加载
	Class.pt.loadData = function(obj){
		var me = this,
		SERACHOBJ = obj;
		Class.pt.onLoad(obj,me);
	};
	Class.pt.getUrl = function(SERACHOBJ,url){
		//样本号范围
		if(SERACHOBJ.beginSampleNo)url+='&beginSampleNo='+SERACHOBJ.beginSampleNo;
		if(SERACHOBJ.endSampleNo)url+='&endSampleNo='+SERACHOBJ.endSampleNo;
        return url;
	};
	//数据加载
	Class.pt.onLoad = function(obj,me){
		var cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = Class.pt.getUrl(obj,GET_LIST_URL);
		
		me.instance.reload({
			url:url,
			where:$.extend({},Class.pt.getWhere(obj),{
				fields:fields.join(','),
				sort:JSON.stringify(me.config.defaultOrderBy)
			})
		});
	};
    Class.pt.getWhere =function(obj){
		var params = [],
		    where = DEFAULTWHERE;
			//小组Id
		if(obj.SECTIONID) {
			params.push("listestform.LBSection.Id=" + obj.SECTIONID + "");
		}
		if(obj.StartDate){
			params.push("listestform.GTestDate>='" + obj.StartDate + "'");
		}
		if(obj.EndDate){
			params.push("listestform.GTestDate<='" + obj.EndDate + "'");
		}
		if(obj.DeptID){//科室
			params.push("listestform.DeptID=" + obj.DeptID + "");
		}
		if(obj.SickTypeID){//就诊类型
			params.push("listestform.SickTypeID=" + obj.SickTypeID + "");
		}
	
		if(params.length > 0) {
			where+= ' and '+ params.join(' and ');
		}
		return {"where":where};
	};
	//数据处理
	Class.pt.changeData =function(data,i){
	
		return data;
	};
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
	};
	//主入口
	formtable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		result.instance.reload({data:[]});//清空列表数据
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('formtable',formtable);
});