/**
 * 全局设置
 * @author 王耀宗
 * @version 2021-6-17
 */


layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'element'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		element = layui.element,
		$ = layui.jquery;
	var app = {};
	app.dataList = [];//查询出的全局设置
	app.dataListDefult = [];//默认的全局配置
	app.url = {
		//获取全局设置
		selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetAllPublicSetting',
		//更新全局设置
		AddUrl: uxutil.path.ROOT +"/ServiceWCF/DictionaryService.svc/UpdatePublicSetting"
	};
	//get参数
	app.paramsObj = {
		ModuleID: '123',//模块id
		module: ''//模块
	};
	//各站点拥有的全局配置项
	app.pageTypeHaveSetting = {
		//所有配置
		public: {
			'defaultWhere': '', 'requestParamsArr': '', 'hisRequestParamsArr': '', 'defaultDates': '1', 'DateField': '',
			'defaultPageSize': '50', 'hasPrint': 'true', 'A4Type': '1', 'printType': 'A4', 'maxPrintTimes': '100', 'mergePageCount': '100',
			'ForcedPagingField': '', 'openAddPrintTimes': 'true', 'checkUnprint': 'false', 'checkFilter': 'false', 'headCollapsed': 'false', 'autoSelect': 'false',
			'CheckOnly': 'true', 'hasReportPage': 'true', 'hasResultPage': 'true', 'defaultCheckedPage': '1', 'printCountSetting': '100', 'pdfPrinterList': 'false',
			'hasPdfPrinter': 'false', 'isListHidden': 'false', 'isCaseSensitive': 'false', 'listWidth': '550', 'isviewportHeader': 'true', 'IsbTempReport': 'false',
			'IsQueryRequest': 'false', 'IsSampleState': 'false', 'HistoryCompareDateField': 'CHECKDATE', 'HistoryCompareDefaultDates': '90', 'HistoryDefaultCollapsed': 'true',
			'sortFields': '', 'queryDateRange': '180', 'NewWindowLoadIframeToPrint': 'false', 'IsUseClodopPrint': 'false', 'IsHistory': '', 'IsBackups': '', 'MaxDownLoadNum': ''
		},
		//医生站单独设置
		doctor: {
			'defaultWhere': '', 'requestParamsArr': '', 'hisRequestParamsArr': '', 'defaultDates': '1', 'DateField': '',
			'defaultPageSize': '50', 'hasPrint': 'true', 'A4Type': '1', 'printType': 'A4', 'maxPrintTimes': '100', 'mergePageCount': '100',
			'ForcedPagingField': '', 'openAddPrintTimes': 'true', 'checkUnprint': 'false', 'checkFilter': 'false', 'headCollapsed': 'false', 'autoSelect': 'false',
			'CheckOnly': 'true', 'hasReportPage': 'true', 'hasResultPage': 'true', 'defaultCheckedPage': '1', 'printCountSetting': '100', 'pdfPrinterList': 'false',
			'hasPdfPrinter': 'false', 'isListHidden': 'false', 'isCaseSensitive': 'false', 'listWidth': '550', 'isviewportHeader': 'true', 'IsbTempReport': 'false',
			'IsQueryRequest': 'false', 'IsSampleState': 'false', 'HistoryCompareDateField': 'CHECKDATE', 'HistoryCompareDefaultDates': '90', 'HistoryDefaultCollapsed': 'true',
			'sortFields': '', 'queryDateRange': '180', 'NewWindowLoadIframeToPrint': 'false', 'IsUseClodopPrint': 'false'
		},
		//doctor: {
		//	'defaultWhere': '', 'requestParamsArr', 'hisRequestParamsArr', 'defaultDates', 'DateField',
		//	'defaultPageSize', 'hasPrint', 'A4Type', 'printType', 'maxPrintTimes', 'mergePageCount',
		//	'ForcedPagingField', 'openAddPrintTimes', 'checkUnprint', 'checkFilter', 'headCollapsed', 'autoSelect',
		//	'CheckOnly', 'hasReportPage', 'hasResultPage', 'defaultCheckedPage', 'printCountSetting', 'pdfPrinterList',
		//	'hasPdfPrinter', 'isListHidden', 'isCaseSensitive', 'listWidth', 'isviewportHeader', 'IsbTempReport',
		//	'IsQueryRequest', 'IsSampleState', 'HistoryCompareDateField', 'HistoryCompareDefaultDates', 'HistoryDefaultCollapsed',
		//	'sortFields', 'queryDateRange', 'NewWindowLoadIframeToPrint', 'IsUseClodopPrint'
		//},
		//护士站nurse，门诊查询台odp，站点查询siteQuery
		nurse: {'defaultWhere': '', 'defaultDates': '', 'defaultPageSize': '', 'hasPrint': '', 'A4Type': '', 'printType': '', 'maxPrintTimes': '',
			'mergePageCount': '', 'ForcedPagingField': '', 'openAddPrintTimes': '', 'checkUnprint': '', 'checkFilter': '', 'headCollapsed': '',
			'autoSelect':'', 'CheckOnly': '', 'hasReportPage': '', 'hasResultPage': '', 'defaultCheckedPage': '', 'printCountSetting': '',
			'pdfPrinterList': '', 'hasPdfPrinter': '', 'isListHidden': '', 'isCaseSensitive': '', 'listWidth': '', 'isviewportHeader': '',
			'IsQueryRequest': '', 'MaxDownLoadNum': '', 'HistoryCompareDateField': '', 'HistoryCompareDefaultDates': '',
			'HistoryDefaultCollapsed': '', 'sortFields': '', 'queryDateRange': '', 'NewWindowLoadIframeToPrint': '', 'IsUseClodopPrint': ''},
		//检验前后查询
		CheckReportRequest: {
			'defaultWhere': '', 'requestParamsArr': '', 'hisRequestParamsArr': '', 'defaultDates': '', 'DateField': '',
			'defaultPageSize': '', 'hasPrint': '', 'A4Type': '', 'printType': '', 'maxPrintTimes': '', 'mergePageCount': '',
			'ForcedPagingField': '', 'openAddPrintTimes': '', 'checkUnprint': '', 'checkFilter': '', 'headCollapsed': '', 'autoSelect': '',
			'CheckOnly': '', 'hasReportPage': '', 'hasResultPage': '', 'defaultCheckedPage': '', 'printCountSetting': '', 'pdfPrinterList': '',
			'hasPdfPrinter': '', 'isListHidden': '', 'isCaseSensitive': '', 'listWidth': '', 'isviewportHeader': '', 'IsUseClodopPrint': ''},
		//分库查询
		historyAndBackups: {
			'defaultWhere': '', 'requestParamsArr': '', 'hisRequestParamsArr': '', 'defaultDates': '', 'DateField': '',
			'defaultPageSize': '', 'hasPrint': '', 'A4Type': '', 'printType': '', 'maxPrintTimes': '', 'mergePageCount': '', 'ForcedPagingField': '',
			'openAddPrintTimes': '', 'checkUnprint': '', 'checkFilter': '', 'headCollapsed': '', 'autoSelect': '', 'CheckOnly': '', 'hasReportPage': '',
			'hasResultPage': '', 'defaultCheckedPage': '', 'printCountSetting': '', 'pdfPrinterList': '', 'hasPdfPrinter': '', 'isListHidden': '',
			'isCaseSensitive': '', 'listWidth': '', 'isviewportHeader': '', 'IsbTempReport': '', 'IsHistory': '', 'IsBackups': '', 'IsUseClodopPrint': ''},
		//lis
		lis: {
			'defaultWhere': '', 'requestParamsArr': '', 'hisRequestParamsArr': '', 'defaultDates': '', 'DateField': '', 'defaultPageSize': '',
			'hasPrint': '', 'A4Type': '', 'printType': '', 'maxPrintTimes': '', 'mergePageCount': '', 'ForcedPagingField': '', 'openAddPrintTimes': '',
			'checkUnprint': '', 'checkFilter': '', 'headCollapsed': '', 'autoSelect': '', 'CheckOnly': '', 'hasReportPage': '', 'hasResultPage': '',
			'defaultCheckedPage': '', 'printCountSetting': '', 'pdfPrinterList': '', 'hasPdfPrinter': '', 'isListHidden': '', 'isCaseSensitive': '',
			'listWidth': '', 'isviewportHeader': '', 'HistoryCompareDateField': '', 'HistoryCompareDefaultDates': '', 'HistoryDefaultCollapsed': '',
			'sortFields': '', 'NewWindowLoadIframeToPrint': '', 'IsUseClodopPrint': ''
		}

    }	

	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		//获取模块id
		if (params.MODULEID) {
			me.paramsObj.ModuleID = params.MODULEID;
		}
		if (params.MODULE) {
			me.paramsObj.module = params.MODULE;
		}

	};
	//初始化  
	app.init = function () {
		var me = this;
		
		me.getParams();
		me.listeners();
		me.getGlobalSetting(me.paramsObj.module,"dataList");
		me.getGlobalSetting("allPageType", "dataListDefult");//需要获取默认全局配置，获取ParaDesc
		me.initHtml();
	};

	//监听事件
	app.listeners = function () {
		var me = this;
		//保存按钮
		layui.$('#saveSetting').on('click', function () { me.savePublicSetting() });
		
	}
	/**
	 * 获取对应站点的全局配置
	 * @param {string} pageType
	 * @param {string} dataListName
	 */
	app.getGlobalSetting = function (pageType,dataListName) {
		var me = this;
		var url = me.url.selectUrl + '?pageType=' + pageType;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (data) {
				
			if (data) {
				if (!data.success) {
					layer.msg(data.ErrorInfo);
					return;
				}
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
						me[dataListName] = value.list;
					} else {
						value = value + "";
					}
				}
				if (!value) return;				
			} else {
				layer.msg(data.msg);
			}
		});
	}
	/**初始化页面元素 */
	app.initHtml = function () {
		var me = this;
		var pageType = me.paramsObj.module;
		
		if (me.paramsObj.module == "odp" || me.paramsObj.module == "siteQuery" ) {
			pageType = "nurse";
		}
		
		var settings = this.pageTypeHaveSetting[pageType];//获取对应的拥有的setting
		var dataList = me.dataList;//查询出来的设置
		
		for (var key in settings) {
			$("#" + key).parent().parent().parent().removeAttr("style");//页面拥有的配置显示
			if (key =="defaultCheckedPage") {
				$("#" + key).parent().parent().removeAttr("style");
            }
			for (var j = 0; j < dataList.length; j++) {
				if (key == dataList[j].ParaNo) {
					if (dataList[j].ParaValue + "" == "false") {//checkbox类型的处理
						settings[key] = false;
					} else {
						settings[key] = dataList[j].ParaValue;
					}
					
					break;
                }
			}
		}
		//给表单赋值
		//{ //formTest 即 class="layui-form" 所在元素属性 lay-filter="" 对应的值
		//	"username": "贤心" // "name": "value"
		//		, "sex": "女"
		//			, "auth": 3
		//				, "check[write]": true
		//					, "open": false
		//						, "desc": "我爱layui"
		//}
		
		form.val("settingForm", settings);
		
			
	}
	/** 处理不同类型元素，赋值或改变状态*/
	app.handleElement = function (elementName,elementValue) {

	}
	/** 保存设置*/
	app.savePublicSetting = function () {
		var me = this;
		var pageType = me.paramsObj.module;
		if (me.paramsObj.module == "odp" || me.paramsObj.module == "siteQuery") {
			pageType = "nurse";
		}
		//1.获取对应页面配置项
		var settings = this.pageTypeHaveSetting[pageType];//获取对应的拥有的setting
		//2.获取页面表单的对象
		var settingForm=form.val("settingForm");//是一个对象{name:value,name:value...}
		//3.获取查询出来的设置
		var dataList = me.dataList;
		//4.处理构造入参
		var list = [];
		for (var key in settings) {
			var formValue = settingForm[key];//获取对应对象值
			if (typeof (formValue) == "undefined") {
				formValue = 'false';//checkbox组件如果不选中则获取不到对应值
            }
			var hash = {};
			hash["ParaValue"] = formValue;
			hash["ParaNo"] = key;
			hash["SName"] = me.paramsObj.module;
			hash["Name"] = "查询打印页面配置";
			hash["ParaType"] = "config";
			for (var j = 0; j < me.dataListDefult.length; j++) {
				if (key == me.dataListDefult[j].ParaNo) {
					hash["ParaDesc"] = me.dataListDefult[j].ParaDesc;
					break;
				}
				
			}
			list.push(hash);
		}
		
		//5.保存设置
		uxutil.server.ajax({
			url: me.url.AddUrl,
			async: false,
			type: "post",
			data: JSON.stringify({ "models": list })
		}, function (data) {
			if (data) {
				if (data.success) {
					layer.msg("保存成功");
					me.init();
				} else {
					layer.msg("保存失败");
				}
				
			} else {
				layer.msg("保存失败");
			}
		});
	}
	app.init();
});