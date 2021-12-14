/**
 * 所有页面配置
 * @author jing
 * @version 2018-09-17
 */
Ext.define('Shell.class.historyAndBackups.table.App', {
    extend: 'Ext.tab.Panel',
	printCountSetting: 100,//批量打印数量配置
    /**默认数据条件,过滤没有文件的结果*/
	defaultWhere: '',//'resultsend=1',
	/**外部数据条件*/
	externalWhere:'',
	/**错误信息*/
	errorInfo: [],
    appType:'',//页面类型
    /**A4纸张类型，1(A4) 2(16开)*/
	A4Type: 1,
    /**默认打印类型*/
    printType:'A4',
    /**默认勾选过滤框*/
	checkFilter: false,
    /**默认勾选未打印框*/
	checkUnprint: false,
    /**默认查询天数*/
	defaultDates: 7,
    /**默认每页数量*/
	defaultPageSize: 50,
    /**默认顺序*/
	defaultOrderBy: [],
	
    /**收缩*/
	headCollapsed: false,
	/**是否开启打印功能*/
	hasPrint: true,
    /**定义的接收参数*/
	requestParamsArr: ['PATNO', 'ZDY3', 'CNAME', 'RECEIVEDATE'],
	hisRequestParamsArr:[],
    /**默认勾选*/
	autoSelect: true,
	/**最大打印数量*/
    maxPrintTimes:2,
	
    /**报告时间字段*/
	DateField: 'RECEIVEDATE',
    /**强制分页字段*/
	ForcedPagingField: '',
    /**点击复选框才选中行*/
	CheckOnly: false,
	/**报告页签*/
    hasReportPage: true,
    /**结果页签*/
    hasResultPage: true,
    /**默认勾选的页签,1=报告页签，2=结果页签*/
    defaultCheckedPage:1,
    /**默认勾选双面打印*/
	checkDoublePrint: false,
	/**是否需要选择打印机*/
	hasPdfPrinter:false,
	/**PDF文件打印机数组*/
	pdfPrinterList: [],
	
	//当报告列表数量<=1时隐藏报告列表，直接显示报告内容
	isListHidden:false,
	
	//查询是否区分大小写
	isCaseSensitive:false,
	
	//是否开启部分审核报告
	IsbTempReport:false,
	
	//列表宽度
	listWidth:'',
	//是否查询request表
	IsQueryRequest:false,
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems:function () {
        var me = this;
        me.doctor = Ext.create("Shell.class.historyAndBackups.basic.App", {
            title: '正式库',
            appType: me.appType,
            printCountSetting: me.printCountSetting,
			defaultWhere: me.defaultWhere,
			externalWhere:me.externalWhere,
			errorInfo:me.errorInfo,
			A4Type: me.A4Type,
		    printType:me.printType,
			checkFilter: me.checkFilter,
			checkUnprint: me.checkUnprint,
			defaultDates: me.defaultDates,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy: me.defaultOrderBy,
			headCollapsed: me.headCollapsed,
			hasPrint: me.hasPrint,
			requestParamsArr: me.requestParamsArr,
			hisRequestParamsArr:me.hisRequestParamsArr,
			autoSelect: me.autoSelect,
		    maxPrintTimes:me.maxPrintTimes,
			DateField: me.DateField,
			ForcedPagingField: me.ForcedPagingField,
			CheckOnly: me.CheckOnly,
		    hasReportPage: me.hasReportPage,
		    hasResultPage: me.hasResultPage,
		    defaultCheckedPage:me.defaultCheckedPage,
			checkDoublePrint: me.checkDoublePrint,
			hasPdfPrinter:me.hasPdfPrinter,
			pdfPrinterList: me.pdfPrinterList,
			isListHidden:me.isListHidden,
			isCaseSensitive:me.isCaseSensitive,
			IsbTempReport:me.IsbTempReport,
			listWidth:me.listWidth,
			IsQueryRequest:me.IsQueryRequest
        });
        me.historyBasic = Ext.create("Shell.class.historyAndBackups.historyBasic.App", {
            title: '历史库',
            appType: me.appType,
            printCountSetting: me.printCountSetting,
			defaultWhere: me.defaultWhere,
			externalWhere:me.externalWhere,
			errorInfo:me.errorInfo,
			A4Type: me.A4Type,
		    printType:me.printType,
			checkFilter: me.checkFilter,
			checkUnprint: me.checkUnprint,
			defaultDates: me.defaultDates,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy: me.defaultOrderBy,
			headCollapsed: me.headCollapsed,
			hasPrint: me.hasPrint,
			requestParamsArr: me.requestParamsArr,
			hisRequestParamsArr:me.hisRequestParamsArr,
			autoSelect: me.autoSelect,
		    maxPrintTimes:me.maxPrintTimes,
			DateField: me.DateField,
			ForcedPagingField: me.ForcedPagingField,
			CheckOnly: me.CheckOnly,
		    hasReportPage: me.hasReportPage,
		    hasResultPage: me.hasResultPage,
		    defaultCheckedPage:me.defaultCheckedPage,
			checkDoublePrint: me.checkDoublePrint,
			hasPdfPrinter:me.hasPdfPrinter,
			pdfPrinterList: me.pdfPrinterList,
			isListHidden:me.isListHidden,
			isCaseSensitive:me.isCaseSensitive,
			//IsbTempReport:me.IsbTempReport,
			listWidth:me.listWidth,
			//IsQueryRequest:me.IsQueryRequest
        });
        me.backupsBasic = Ext.create("Shell.class.historyAndBackups.backupsBasic.App", {
            title: '备份库',
            appType: me.appType,
            printCountSetting: me.printCountSetting,
			defaultWhere: me.defaultWhere,
			externalWhere:me.externalWhere,
			errorInfo:me.errorInfo,
			A4Type: me.A4Type,
		    printType:me.printType,
			checkFilter: me.checkFilter,
			checkUnprint: me.checkUnprint,
			defaultDates: me.defaultDates,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy: me.defaultOrderBy,
			headCollapsed: me.headCollapsed,
			hasPrint: me.hasPrint,
			requestParamsArr: me.requestParamsArr,
			hisRequestParamsArr:me.hisRequestParamsArr,
			autoSelect: me.autoSelect,
		    maxPrintTimes:me.maxPrintTimes,
			DateField: me.DateField,
			ForcedPagingField: me.ForcedPagingField,
			CheckOnly: me.CheckOnly,
		    hasReportPage: me.hasReportPage,
		    hasResultPage: me.hasResultPage,
		    defaultCheckedPage:me.defaultCheckedPage,
			checkDoublePrint: me.checkDoublePrint,
			hasPdfPrinter:me.hasPdfPrinter,
			pdfPrinterList: me.pdfPrinterList,
			isListHidden:me.isListHidden,
			isCaseSensitive:me.isCaseSensitive,
			//IsbTempReport:me.IsbTempReport,
			listWidth:me.listWidth,
			//IsQueryRequest:me.IsQueryRequest
        });
        return [me.doctor, me.historyBasic, me.backupsBasic];
    }
});