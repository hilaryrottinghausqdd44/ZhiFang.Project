/**
	@name：取单时间段规则列表
	@author：liangyl
	@version 2021-07-02
 */
layui.extend({
	uxtable:'ux/table'
}).define(['uxutil','uxtable','table','form','laydate'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		laydate = layui.laydate,
		uxtable = layui.uxtable;
		
	//获取取单时间段规则数据服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateRuleByHQL?isPlanish=true';
	//删除取单时间段规则
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBReportDateRule';
   	//新增取单时间段规则
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBReportDateRule';
	//修改取单时间段规则
	var EDIT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateRuleByField';

    //取单时间分类明细ID
    var ReportDateDescID = null;
    //实例
    var table_ind = null;
	var ReportDateRuleList = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
				cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBReportDateRule_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBReportDateRule_ReportDateDescID',title:'ID',width:150,sort:true,hide:true},
				{field:'LBReportDateRule_BeginWeekDay',title:'开始星期',width:100},
                {field:'LBReportDateRule_EndWeekDay',title:'结束星期',width:100},
				{field:'LBReportDateRule_BeginTime',title:'开始时间',width:100,templet:function(d){
	                //开始时间
					var strTime="";
					//取出时分秒
                	if(d["LBReportDateRule_BeginTime"]){
                		var str1 = d["LBReportDateRule_BeginTime"].split(' ')[1];
                		strTime = str1.split(':')[0]+":"+str1.split(':')[1];
                	}
	                return strTime;
				}},
                {field:'LBReportDateRule_EndTime',title:'结束时间',width:100,templet:function(d){
	                //结束时间
					var endTime="";
					//取出时分秒
                	if(d["LBReportDateRule_EndTime"]){
                		var str1 = d["LBReportDateRule_EndTime"].split(' ')[1];
                		endTime = str1.split(':')[0]+":"+str1.split(':')[1];
                	}
	                return endTime;
				}},
				{fixed: 'right', title:'操作', toolbar: '#barRule', width:70}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
					//监听行双击事件
					that.table.on('rowDouble(' + filter + ')', function(obj){
						me.onpenWin(obj.data.LBReportDateRule_Id,obj.data.LBReportDateRule_ReportDateDescID);
					});
				}
			}
		},me.config,ReportDateRuleList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(id){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	    ReportDateDescID = id;
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
		var whereObj = {"where":'lbreportdaterule.ReportDateDescID='+ReportDateDescID};
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	Class.pt.clearData = function(id){
		var  me = this;
	    ReportDateDescID = null;
		me.instance.reload({
			url:'',
			data:[]
		});
	};
	
	Class.pt.initListeners = function(){
		var me = this;
		var filter = $(me.config.elem).attr("lay-filter");
		//新增取单时间规则
		$('#addrule').on('click',function(){
			if(!ReportDateDescID){
    			layer.msg("请先新增取单时间分类明细");
    			return;
    		}
		    me.onpenWin('',ReportDateDescID);
        });
		
    	//监听工具条
		table_ind.table.on('tool(reportdaterule-table)', function(obj){
		    var data = obj.data;
		    if(obj.event === 'del'){
		    	me.onDelClick(data.LBReportDateRule_Id);
		    }
		});
	};
	
   //弹出方法 
	Class.pt.onpenWin = function(id,ReportDateDescID){
		var title = '新增取单时间段规则'	;
	    if(id)title = '编辑取单时间段规则';
		layer.open({
			title:title,
			type:2,
			content:'ruleform.html?id=' + id +'&ReportDateDescID='+ReportDateDescID+'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:false,
			area:['400px','370px']
		});
	};
	 //删除方法 
	Class.pt.onDelClick = function(id){
		var me = this;
        if(!id)return;
    	var url = DEL_URL +'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
					layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                    table_ind.loadData(ReportDateDescID);
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	};
	//主入口
	ReportDateRuleList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		result.clearData = me.clearData;
		me.initListeners();
		return result;
	};
	function afterUpdateRule(){
		table_ind.loadData(ReportDateDescID);
	}
	window.afterUpdateRule  = afterUpdateRule;
	//暴露接口
	exports('ReportDateRuleList',ReportDateRuleList);
});