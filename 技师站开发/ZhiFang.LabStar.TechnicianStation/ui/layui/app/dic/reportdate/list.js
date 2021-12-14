/**
	@name：取单时间分类列表信息
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
		
	//获取取单时间分类列表数据
	var GET_REPORTDATA_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateByHQL?isPlanish=true';
	//删除取单时间分类
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBReportDate';
    var  table_ind = null;
	var ReportDateList = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBReportDate_DispOrder',direction: 'ASC'}]",
			RowID:null,//操作行ID
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBReportDate_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBReportDate_CName',title:'分组名称',width:100,sort:true},
				{field:'LBReportDate_IsUse',width:60,title:'使用',templet:function(d){
					var arr = [
						'<div style="color:#FF5722;text-align:center;">否</div>',
						'<div style="color:#009688;text-align:center;">是</div>'
					];
					var result = d.LBReportDate_IsUse == 'true' ? arr[1] : arr[0];
					
					return result;
				}},
				{field:'LBReportDate_DispOrder',title:'显示次序',width:100,sort:true},
				{field:'LBReportDate_ReportDateDesc',title:'描述',width:100,sort:true,hide:true},
				{fixed: 'right', title:'操作', toolbar: '#barDemo', width:70}
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
				}
			}
		},me.config,ReportDateList.config,setings);
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
		var url = GET_REPORTDATA_LIST_URL;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
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
                    table_ind.loadData({});
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	};
	//  //打开取单分类窗体
    Class.pt.openWinForm = function(id){
		var title = '新增取单时间分组'	;
		if(id)title = '编辑取单时间分组';
		layer.open({
			title:title,
			type:2,
			content:'form.html?id=' + id +'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:false,
			area:['500px','300px']
		});
	};
	//联动监听
	Class.pt.initListeners = function(){
		var me = this;
		//新增分类
    	$('#add').on('click',function(){
    		me.openWinForm('');
    	});
		//监听工具条
		table_ind.table.on('tool(reportdate-table)', function(obj){
		    var data = obj.data;
		    if(obj.event === 'del'){
		    	me.onDelClick(data.LBReportDate_Id);
		    }
		});
	};
	//主入口
	ReportDateList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
//		result.onDelClick = me.onDelClick;
		result.openWinForm = me.openWinForm;
		me.initListeners();
		return result;
	};

	//暴露接口
	exports('ReportDateList',ReportDateList);
});