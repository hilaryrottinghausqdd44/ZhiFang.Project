Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	Shell.util.Win.begin();//屏蔽快捷键
	
	panel = Ext.create('Shell.ReportSearchDoctor.class.App', {
	    header: false,
	    /**报告日期字段*/
		ReportDateField:'ZDY10',
	    //医生编号映射字段
    	DoctorNoField:'ZDY9',
	    //默认每页数量[10,20,50,100,200,300,400,500]
	    defaultPageSize: 500,
	    //是否开启历史对比图
	    showChart: true,
        //默认报告天数
		defaultDates: 3,
	    //默认排序字段
	    defaultOrderBy: [
	    	{ field: 'ZDY10', order: 'DESC' }, 
	    	{ field: 'CNAME', order: 'ASC' }
	    ]
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:2,
		items:[panel]
	});
});

/**用于结果页面的行点击调用,变更历史对比信息*/
function printResult(PatNo,ItemNo,Table,ReceiveDate){
    var PrintChart = panel.getComponent('PrintChart');

    if (!PrintChart) return;
		
	PrintChart.load({
		PatNo:PatNo,
		ItemNo:ItemNo,
		Table:Table,
		ReceiveDate:ReceiveDate
	});
}