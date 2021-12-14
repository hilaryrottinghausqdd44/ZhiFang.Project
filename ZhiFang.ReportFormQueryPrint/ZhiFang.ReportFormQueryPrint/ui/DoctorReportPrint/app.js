Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.useShims=true;//防止PDF挡住下拉框
	Shell.util.Win.begin();//屏蔽快捷键
	
	//根据参数决定是否显示log信息
	var params = Shell.util.Path.getRequestParams(true);
	for(var i in params){
		if(i.toLowerCase() === "SHOWLOG" && params[i] === "true"){
			Shell.util.Config.showLog = true;
		}else if(i.toLowerCase() === "SHOWLOGWIN" && params[i] === "true"){
			Shell.util.Config.showLogWin = true;
		}
	}
	
	panel = Ext.create('Shell.DoctorReportPrint.class.PrintApp', {
	    header: false,
	    isListHidden:false,//当报告列表数量<=1时隐藏报告列表，直接显示报告内容
	    defaultWhere: '',//默认条件//resultsend=1
	    //hasPrint: false,//是否开启打印
	    requestParamsArr: ['PATNO', 'CNAME', 'RECEIVEDATE', 'SAMPLENO','ZDY1','SECTIONNO'],//定义的接收参数
	    defaultPageSize:50,//默认每页数量[10,20,50,100,200,300,400,500]
	    A4Type: 1,//A4纸张类型，1(A4) 2(16开)
	    printType: '双A5',//A4,A5,双A5
	    defaultOrderBy: [{ field: 'Bed', order: 'ASC' }, { field: 'PatNo', order: 'ASC' },{ field: 'RECEIVEDATE', order: 'DESC' }],//默认排序字段
	    checkUnprint: false,//默认勾选未打印框
	    checkFilter: false,//默认勾选过滤框
	    headCollapsed:false,//默认收起查询框
	    defaultDates: 1,//默认查询天数
	    autoSelect: false,//默认勾选
		maxPrintTimes:100,//最大打印次数
	    mergePageCount: 2,//双A5合并的数量
        openAddPrintTimes: true,//开启打印次数累加功能
        CheckOnly: true,//点击复选框才选中行
        DateField: 'RECEIVEDATE',//报告时间字段
        ForcedPagingField: { dataIndex: 'PatNo', text: '病历号' },//强制分页字段
	    hasReportPage: true,//报告页签
	    hasResultPage: true,//结果页签
	    defaultCheckedPage:1,//默认勾选的页签,1=报告页签，2=结果页签
	    hasPdfPrinter:false,//是否需要选择打印机，需要降低IE浏览器安全性
	    pdfPrinterList:[]//['Microsoft Print to PDF']//PDF文件打印机数组,不填只有默认打印机
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