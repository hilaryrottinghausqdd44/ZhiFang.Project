/**
 * @author guohaixiang
 * @version 2020-05-15
 */
Ext.define('Shell.class.basic.sampleStateList.sampleListconent', {
    extend: 'Shell.ux.panel.Panel',
	//height:300,
	printCountSetting:  '',//批量打印数量配置
	header: false,
	IsbTempReport: '',
	layout:'border',
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
        me.Report = Ext.create("Shell.class.basic.List", {
            title: '',
            itemId:'ReportListcenter',
            printCountSetting: me.printCountSetting,//批量打印数量配置
			header: false,
			IsbTempReport:me.IsbTempReport,//是否查询部分审核报告
			appType: me.appType,
			width:me.width,
			region:'west',
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
			IsQueryRequest:me.IsQueryRequest//是否查询request表
			
        });
        me.sampleState = Ext.create("Shell.class.basic.sampleStateList.sampleStateGrid", {
            title: '样本状态',
            itemId:'sampleState',
			height:250,region:'south',collapsed:true,
			split:true,collapsible:true
        });
        
        return [me.Report,me.sampleState];
    }
});