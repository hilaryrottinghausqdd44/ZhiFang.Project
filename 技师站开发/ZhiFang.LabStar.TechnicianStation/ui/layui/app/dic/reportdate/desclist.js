/**
	@name：取单时间分类明细列表
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
		
	//获取取单时间分类明细列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateDescByHQL?isPlanish=true';
	//删除取单时间分类明细
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBReportDateDesc';
	//新增取单时间分类
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBReportDateDesc';
	//修改取单时间分类
	var EDIT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateDescByField';
    //取单时间分类实例
    var table_ind = null;
    //取单时间分类ID
    var ReportDateID = null;
    
    var saveErrorCount = 0,
		saveCount = 0,
		saveLength = 0;
    
	var ReportDateDescList = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBReportDateDesc_DispOrder',direction: 'ASC'}]",
			RowID:null,//操作行ID
			cols:[[
				{type: 'numbers',title: '行号',fixed: 'left'},
                {field:'LBReportDateDesc_IsUse',width:60,title:'使用',hide:true,templet:function(d){
					var arr = [
						'<div style="color:#FF5722;text-align:center;">否</div>',
						'<div style="color:#009688;text-align:center;">是</div>'
					];
					var result = d.LBReportDateDesc_IsUse == 'true' ? arr[1] : arr[0];
					return result;
				}},
				{field:'LBReportDateDesc_LBReportDate_Id',title:'LBReportDate_ID',width:150,sort:true,hide:true},
				{field:'LBReportDateDesc_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBReportDateDesc_ReportDateDesc',title:'取单描述',flex:1,sort:false,edit: 'text'},
				{field:'LBReportDateDesc_DispOrder',title:'显示次序',width:100,sort:false,hide:true},
				{fixed: 'right', title:'操作', toolbar: '#barReportDate', width:68}
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
						me.openWinDesc(obj.data.LBReportDateDesc_Id,obj.data.LBReportDateDesc_LBReportDate_Id);
					});
				}
			}
		},me.config,ReportDateDescList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(id){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	    ReportDateID =  id;
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
		var whereObj  = {"where":"lbreportdatedesc.LBReportDate.Id="+id};
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
	    ReportDateID = null;
		
		me.instance.reload({
			url:'',
			data:[]
		});
	};
	//主入口
	ReportDateDescList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		result.clearData = me.clearData;
		me.initListeners();
		return result;
	};
	Class.pt.initListeners = function(){
		var me = this;
		//新增空行(取单时间描述项)
		$('#adddesc').on('click',function(){
			me.openWinDesc('',ReportDateID);
		});
		//监听工具条
		table_ind.table.on('tool(reportdatedesc-table)', function(obj){
		    var data = obj.data;
		    if(obj.event === 'del'){
		    	me.onDelClick(data.LBReportDateDesc_Id);
		    }
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
                    table_ind.loadData(ReportDateID);
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	};
	 //弹出方法 
	Class.pt.openWinDesc = function(id,ReportDateID){
		var title = '新增取单时间分类明细'	;
	    if(id)title = '编辑取单时间分类明细';
		layer.open({
			title:title,
			type:2,
			content:'descform.html?id=' + id +'&ReportDateID='+ReportDateID+'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:false,
			area:['400px','200px']
		});
	};
	function afterUpdateDesc(){
		table_ind.loadData(ReportDateID);
	}
	window.afterUpdateDesc  = afterUpdateDesc;
	//暴露接口
	exports('ReportDateDescList',ReportDateDescList);
});