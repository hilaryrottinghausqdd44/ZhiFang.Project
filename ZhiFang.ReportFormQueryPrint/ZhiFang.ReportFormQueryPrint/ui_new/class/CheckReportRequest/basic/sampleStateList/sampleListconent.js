/**
 * @author guohaixiang
 * @version 2020-05-15
 */
Ext.define('Shell.class.CheckReportRequest.basic.sampleStateList.sampleListconent', {
    extend: 'Shell.ux.panel.Panel',
	//height:300,
	printCountSetting:  '',//批量打印数量配置
	header: false,
	IsbTempReport: '',
	layout: {
	     type: 'border',
	     regionWeights: {
	         west: 20
	     }
	},
	appType:  '',
	width: '',
	//split:true,
	//collapsible:true,
	hasPrint:  '',
	defaultPageSize:  '',
	defaultOrderBy: '',
	defaultWhere: '',
	externalWhere: '',
	defaultLoad: false,//me.externalWhere ? true : false,
	A4Type:  '',
	printType: '',
	checkFilter:  '',
	checkUnprint:  '',
	autoSelect:  '',
	maxPrintTimes: '',
	mergePageCount:  '',
	clreaTimes:  '',
	openAddPrintTimes:  '',
	//DateField:me.DateField,
	ForcedPagingField:  '',
	checkDoublePrint: '',
	CheckOnly:  '',
	hasPdfPrinter: '',//是否需要选择打印机
	pdfPrinterList:  '',//PDF文件打印机数组
	IsQueryRequest:false,//是否查询request表
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },
    createItems:function () {
        var me = this;
        me.patientState = Ext.create("Shell.class.CheckReportRequest.basic.sampleStateList.sampleStateGrid", {
            title: '病人信息',
            itemId:'patientMessage',width: '35%',
			region:'west',collapsed:false,
			split:true,collapsible:true,
			printCountSetting: me.printCountSetting,//批量打印数量配置
			header: false,
			IsbTempReport:me.IsbTempReport,//是否查询部分审核报告
			appType: me.appType,
			hasPrint: me.hasPrint,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy:me.defaultOrderBy,
			defaultWhere:me.defaultWhere,
			externalWhere:me.externalWhere,
			defaultLoad: false,//me.externalWhere ? true : false, 
			A4Type: me.A4Type,
			printType:me.printType,
			checkFilter: me.checkFilter,
			checkUnprint: me.checkUnprint,
			//autoSelect: me.autoSelect,
			maxPrintTimes:me.maxPrintTimes,
			mergePageCount: me.mergePageCount,
			clreaTimes: me.clreaTimes,
			openAddPrintTimes: me.openAddPrintTimes,
			//DateField:me.DateField,
			ForcedPagingField: me.ForcedPagingField,
			checkDoublePrint:me.checkDoublePrint,
			CheckOnly: me.CheckOnly,
			hasPdfPrinter:me.hasPdfPrinter,//是否需要选择打印机
			pdfPrinterList: me.pdfPrinterList,//PDF文件打印机数组
			IsQueryRequest:me.IsQueryRequest//是否查询request表
        });
        me.Report = Ext.create("Shell.class.CheckReportRequest.basic.List", {
            title: '检测报告',
            itemId:'ReportListcenter',
            printCountSetting: me.printCountSetting,//批量打印数量配置
			header: false,
			IsbTempReport:me.IsbTempReport,//是否查询部分审核报告
			appType: me.appType,
			width:'',
			region:'center',
			split:true,collapsible:true,
			hasPrint: me.hasPrint,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy:me.defaultOrderBy,
			defaultWhere:me.defaultWhere,
			externalWhere:me.externalWhere,
			defaultLoad: false,//me.externalWhere ? true : false, 
			A4Type: me.A4Type,
			printType:me.printType,
			checkFilter: me.checkFilter,
			checkUnprint: me.checkUnprint,
			//autoSelect: me.autoSelect,
			maxPrintTimes:me.maxPrintTimes,
			mergePageCount: me.mergePageCount,
			clreaTimes: me.clreaTimes,
			openAddPrintTimes: me.openAddPrintTimes,
			//DateField:me.DateField,
			ForcedPagingField: me.ForcedPagingField,
			checkDoublePrint:me.checkDoublePrint,
			CheckOnly: me.CheckOnly,
			hasPdfPrinter:me.hasPdfPrinter,//是否需要选择打印机
			pdfPrinterList: me.pdfPrinterList,//PDF文件打印机数组
			IsQueryRequest:me.IsQueryRequest,//是否查询request表
			IsUseClodopPrint:me.IsUseClodopPrint
			
        });
        
        
        return [me.patientState,me.Report];
    }
});