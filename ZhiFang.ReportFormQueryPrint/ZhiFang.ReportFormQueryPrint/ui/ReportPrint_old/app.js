Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	Shell.util.Win.begin();//屏蔽快捷键
	
	//根据参数决定是否显示log信息
	var params = Shell.util.Path.getRequestParams();
	for(var i in params){
		if(i.toLowerCase() === "showlog" && params[i] === "true"){
			Shell.util.Config.showLog = true;
		}else if(i.toLowerCase() === "showlogwin" && params[i] === "true"){
			Shell.util.Config.showLogWin = true;
		}
	}
	
	panel = Ext.create('Shell.ReportPrint.class.PrintApp', {
	    defaultWhere: '',
	    header: false,
	    hasPrint: false,//是否开启打印功能
	    requestParamsArr: ['PATNO', 'ZDY4', 'CNAME', 'RECEIVEDATE'],//定义的接收参数
	    defaultPageSize:500,//默认每页数量[10,20,50,100,200,300,400,500]
	    A4Type: 1,//A4纸张类型，1(A4) 2(16开)
	    printType: '双A5',//A4,A5,双A5
	    defaultOrderBy: [{ field: 'RECEIVEDATE', order: 'DESC' }, { field: 'CNAME', order: 'ASC' }],//默认排序字段
	    checkUnprint: false,//默认勾选未打印框
	    checkFilter: false,//默认勾选过滤框
	    headCollapsed:false,//默认收起查询框
	    defaultDates: 90,//默认查询天数
	    autoSelect: false//默认勾选
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