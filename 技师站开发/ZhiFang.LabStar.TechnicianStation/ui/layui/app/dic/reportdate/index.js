/**
   @Name：取单时间
   @Author：liangyl
   @version 2021-07-01
 */
layui.extend({
	uxutil:'/ux/util',
    uxtable:'/ux/table',
	ReportDateList:'app/dic/reportdate/list',//取单时间分类
	ReportDateDescList:'app/dic/reportdate/desclist',//取单时间分类明细
	ReportDateRuleList:'app/dic/reportdate/rulelist',//取单时间段规则
	ReportDateItemList:'app/dic/reportdate/item/list' //取单时间分类项目明细
}).use(['uxutil','table','ReportDateList','ReportDateDescList','ReportDateRuleList','ReportDateItemList'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		ReportDateList = layui.ReportDateList,
		ReportDateDescList = layui.ReportDateDescList,
		ReportDateRuleList = layui.ReportDateRuleList,
		ReportDateItemList = layui.ReportDateItemList,
		table = layui.table;
	
	//取单时间分类实例
	var table_ind0=null;
	//取单时间分类明细实例
	var table_ind1=null;
    //取单时间段规则实例
	var table_ind2=null;
	 //取单时间分类项目明细实例
	var table_ind3=null;
	//取单时间分类ID  用于定位行
	var REPORTDATEID = null;
	//选中的取单分类行
	var CHECK_ROW_DATA_LIST = [];
	//选择的取单时间分类明细行
	var CHECK_DESC_ROW_DATA_LIST = [];
	//最大高度
	var win = $(window),
		maxHeight = win.height()-50;
	
	//取单时间分类明细实例初始化
	table_ind0 = ReportDateList.render({
		elem:'#reportdate-table',
    	title:'取单时间分类列表',
    	height:'full-55',
    	size: 'sm', //小尺寸的表格
    	defaultOrderBy: JSON.stringify([{property: 'LBReportDate_DispOrder',direction: 'ASC'}]),
    	done: function(res, curr, count) {
			if(count>0){
				//默认选择第一行
				var rowIndex = 0;
	            for (var i = 0; i < res.data.length; i++) {
	                if (res.data[i].LBReportDate_Id ==REPORTDATEID) {
	              	    rowIndex=res.data[i].LAY_TABLE_INDEX;
	              	    break;
	                }
	            }
	            //默认选择行	
	            var tableDiv = $("#reportdate-table+div .layui-table-body.layui-table-body.layui-table-main");
		        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		        thatrow.click();
			}else{
				if(table_ind1)table_ind1.clearData();
				if(table_ind2)table_ind2.clearData();
				if(table_ind3)table_ind3.clearData();
			}
		}
	});
	//取单时间分类明细实例初始化
	table_ind1 = ReportDateDescList.render({
		elem:'#reportdatedesc-table',
    	title:'取单时间分类明细列表',
    	height:maxHeight/2-15,
    	size: 'sm', //小尺寸的表格
    	defaultOrderBy: JSON.stringify([{property: 'LBReportDateDesc_DispOrder',direction: 'ASC'}]),
    	done: function(res, curr, count) {
    		$(".layui-table").find('td').data('edit', false);
			if(count>0){
				//默认选择第一行
				var rowIndex = 0;
				//默认选择行
	            var tableDiv = $("#reportdatedesc-table+div .layui-table-body.layui-table-body.layui-table-main");
		        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		        thatrow.click();
			}else{
				if(table_ind2)table_ind2.clearData();
			}
		}
	});
	table_ind1.instance.reload({data:[]});
	//取单时间段规则实例初始化
	table_ind2 = ReportDateRuleList.render({
		elem:'#reportdaterule-table',
    	title:'取单时间分类明细列表',
    	height:maxHeight/2-15,
    	size: 'sm' //小尺寸的表格
	});
	table_ind2.instance.reload({data:[]});
	//取单时间分类项目明细实例初始化
	table_ind3 = ReportDateItemList.render({
		elem:'#reportdateitem-table',
    	title:'取单时间分类明细列表',
    	height:'full-56',
    	size: 'sm', //小尺寸的表格
    	done: function(res, curr, count) {
			if(count>0){
				//默认选择第一行
				var rowIndex = 0;
				//默认选择行
	            var tableDiv = $("#reportdateitem-table+div .layui-table-body.layui-table-body.layui-table-main");
		        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		        thatrow.click();
			}
		}
	});
	
	//取单分类列表选择行监听
	table_ind0.table.on('row(reportdate-table)', function(obj){
		CHECK_ROW_DATA_LIST = [];
		CHECK_ROW_DATA_LIST.push(obj.data);
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	    //取单时间分类明细加载
	    table_ind1.loadData(obj.data.LBReportDate_Id);
	    //取单时间分类项目明细
	    table_ind3.loadData(obj.data.LBReportDate_Id);
	});
  	//取单分类 编辑
	table_ind0.table.on('rowDouble(reportdate-table)', function(obj){
		layer.open({
			title:'编辑取单时间分组',
			type:2,
			content:'form.html?id=' + obj.data.LBReportDate_Id +'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:false,
			area:['500px','300px']
		});
	});
	//取单时间分类明细选择行监听
	table_ind1.table.on('row(reportdatedesc-table)', function(obj){
		CHECK_DESC_ROW_DATA_LIST = [];
		CHECK_DESC_ROW_DATA_LIST.push(obj.data);
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	    if(obj.data.LBReportDateDesc_Id){
	    	//取单时间段规则
	        table_ind2.loadData(obj.data.LBReportDateDesc_Id);
	    }else{
	    	table_ind2.instance.reload({data:[]});
	    }
	});

	//新增取单分类项目
	$('#addItem').on('click',function(){
		if(CHECK_ROW_DATA_LIST.length>0)table_ind3.openWin(CHECK_ROW_DATA_LIST[0].LBReportDate_Id,CHECK_ROW_DATA_LIST[0].LBReportDate_CName,function(){
			//取单时间分类项目明细
	        table_ind3.loadData(CHECK_ROW_DATA_LIST[0].LBReportDate_Id);
		});
	});
    //初始化
    function init(){
    	table_ind0.loadData();
    }
    function afterUpdate(data){
    	if(data && data.value )REPORTDATEID = data.value.id;
    	if(data)table_ind0.loadData({});
    }
    //取单项目分类
    function afterItemUpdate(data){
    	if(data)table_ind3.loadData(CHECK_ROW_DATA_LIST[0].LBReportDate_Id);
    }

    window.afterUpdate = afterUpdate;
    window.afterItemUpdate = afterItemUpdate;
	//初始化
	init();
});