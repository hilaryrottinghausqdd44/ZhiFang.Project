/**
 * ListTabPanel
 * @author ghx
 * @version 2019-7-24
 * @author Guohx
 * @version 2020-01-08
 */
Ext.define('Shell.class.historyAndBackups.backupsBasic.ListTab', {
    extend: 'Ext.tab.Panel',
	height:300,
	printCountSetting:  '',//批量打印数量配置
	header: false,
	IsbTempReport: '',
	appType:  '',
	width: '',
	split:true,
	collapsible:true,
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
        me.Report = Ext.create("Shell.class.historyAndBackups.backupsBasic.ReportList", {
            title: '已出报告',
            itemId:'ReportList',
            printCountSetting:me.printCountSetting,
            header: false,
            IsbTempReport:me.IsbTempReport,
            appType:me.appType,
            //width:me.width,
            region:'west',
            split:true,
            collapsible:true,
            hasPrint:  me.hasPrint,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy: me.defaultOrderBy,
			defaultWhere: me.defaultWhere,
			externalWhere:me.externalWhere,
			defaultLoad: false,//me.externalWhere ? true : false,
			A4Type: me.A4Type,
			printType: me.printType,
			checkFilter:  me.checkFilter,
			checkUnprint: me.checkUnprint,
			autoSelect:  me.autoSelect,
			maxPrintTimes: me.maxPrintTimes,
			mergePageCount:  me.mergePageCount,
			clreaTimes:  me.clreaTimes,
			openAddPrintTimes:  me.openAddPrintTimes,
			//DateField:me.DateField,
			ForcedPagingField:  me.ForcedPagingField,
			checkDoublePrint: me.checkDoublePrint,
			CheckOnly:  me.CheckOnly,
			hasPdfPrinter: me.hasPdfPrinter,//是否需要选择打印机
			pdfPrinterList:  me.pdfPrinterList,//PDF文件打印机数组
            IsQueryRequest:me.IsQueryRequest//是否查询request表
			
        });
        me.Request = Ext.create("Shell.class.historyAndBackups.backupsBasic.RequestList", {
            title: '未出报告',
            itemId:'RequestList',
            printCountSetting:me.printCountSetting,
            header: false,
            IsbTempReport:me.IsbTempReport,
            appType:me.appType,
            //width:me.width,
            region:'west',
            split:true,
            collapsible:true,
            hasPrint:  me.hasPrint,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy: me.defaultOrderBy,
			defaultWhere: me.defaultWhere,
			externalWhere:me.externalWhere,
			defaultLoad: false,//me.externalWhere ? true : false,
			A4Type: me.A4Type,
			printType: me.printType,
			checkFilter:  me.checkFilter,
			checkUnprint: me.checkUnprint,
			autoSelect:  me.autoSelect,
			maxPrintTimes: me.maxPrintTimes,
			mergePageCount:  me.mergePageCount,
			clreaTimes:  me.clreaTimes,
			openAddPrintTimes:  me.openAddPrintTimes,
			//DateField:me.DateField,
			ForcedPagingField:  me.ForcedPagingField,
			checkDoublePrint: me.checkDoublePrint,
			CheckOnly:  me.CheckOnly,
			hasPdfPrinter: me.hasPdfPrinter,//是否需要选择打印机
			pdfPrinterList:  me.pdfPrinterList,//PDF文件打印机数组
            IsQueryRequest:me.IsQueryRequest//是否查询request表
        });
        
        return [me.Report, me.Request];
    }
});