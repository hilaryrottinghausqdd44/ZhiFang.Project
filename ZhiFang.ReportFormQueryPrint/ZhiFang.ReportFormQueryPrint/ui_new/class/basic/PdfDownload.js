/**
 * 下载功能
 * @author GHX
 * @version 2020-10-12
 */
Ext.define('Shell.class.basic.PdfDownload', {
	extend: 'Shell.ux.panel.Panel',

	title: '下载进度信息',

	/**合并的数量*/
	mergePageCount: 2,
	/**是否自动销毁iframe,false时再次调用下载方法时销毁*/
	autoClear: true,
	/**每一个文件容器回收时间,毫秒数*/
	clreaTimes: 3000,
	/**延时处理时间*/
	setTimeoutTimes: 100,
	/**开启下载次数累加功能*/
	openAddPrintTimes: true,
	/**下载完成后刷新列表*/
	refreshListAfterPrintIsOver: true,
	/**显示进度窗口*/
	showProgressInfoWin: true,
	/**是否显示log信息*/
	openLog: false,

	/**报告时间字段*/
	DateField: 'RECEIVEDATE',
	/**强制分页字段*/
	ForcedPagingField: '',
	/**是否双面下载*/
	isDoublePrint: false,
	/**A4纸张类型，1(A4) 2(16开)*/
	A4Type: null,
	/**报告下载类型*/
	strPageName: null,
	/**报告列表*/
	reportList: [],
	/**下载文件列表(可能多份报告合并成一个文件)*/
	printList: [],

	/**成功累加的报告ID*/
	SuccessIds: [],
	/*是否谷歌浏览器*/
	IsChrome: false,
	/**谷歌混合合并*/
	BlendPDF: "/ServiceWCF/ReportFormService.svc/BlendPDF",
	/**A5文件合并服务*/
	DobuleA5MergeA4PDFFiles: "/ServiceWCF/ReportFormService.svc/DobuleA5MergeA4PDFFilesPost",
	/**32K文件合并服务*/
	Dobule32KMerge16KPDFFiles: "/ServiceWCF/ReportFormService.svc/Dobule32KMerge16KPDFFilesPost",
	/**A4文件合并服务*/
	MergeA4PDFFiles: "/ServiceWCF/ReportFormService.svc/MergeA4PDFFiles",
	/**16K文件合并服务*/
	Merge16KPDFFiles: "/ServiceWCF/ReportFormService.svc/Merge16KPDFFiles",
	/**增加下载次数服务路径*/
	addPrintTimesUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/ReportFormAddUpdatePrintTimes',
	//生成报告服务
	createReportUrl: '/ServiceWCF/ReportFormService.svc/GetReportFormPDFByReportFormID',
	//合并下载生成报告服务
	MergeCreateReportUrl: '/ServiceWCF/ReportFormService.svc/GetMergReportFromByReportFormIdList',
	//是否和并下载
	isMerg: false,
	/**是否浏览下载*/
	isView: false,
	/**当前存在的iframeId*/
	iframeIds: [],
	/**是否需要选择下载机*/
	hasPdfPrinter: false,
	/**默认下载机*/
	defaultPrinter: '',
	/**PDF文件下载机*/
	pdfPrinter: '',

	//当前往下载机里面发送的文件总数
	printNum: 0,
	//每次生成的数量
	createCount: 5,
	//是否壳下载
	IsDelphi: false,
	//下载功能中移动成功的文件数量
	moveSuccessCount: 0,
	//文件移动是否全部成功
	moveOneSuccess: false,

	initComponent: function () {
		var me = this;
		me.layout = 'fit';
		me.addEvents('printStart', 'printEnd');
		me.mergePageCount = me.mergePageCount || 2;
		me.clreaTimes = me.clreaTimes || 0;
		if (me.clreaTimes < 0) me.clreaTimes = 0;
		me.openAddPrintTimes = me.openAddPrintTimes === false ? false : true;

		if (me.hasPdfPrinter && !me.IsDelphi) {
			//新建一个WScript.Shell对象     
			me.Shell = new ActiveXObject("WScript.Shell");
		}

		me.callParent(arguments);
	},
	isPrintEnd: false,//是否下载结束
	i: 0,//循环体
	mapkey: 1,//当前下载的数据的map key值
	printmaps: {},//传入参数数据集合
	/**
	 * @public 下载
	 * @example
	 * 	config
	 * 	   isDoublePrint 是否双面下载
	 *     strPageName 下载报告类型//A4/A5/双A5 
	 *     A4Type A4报告的类型//A4/16开
	 *     data 下载的数据列表
	 *	       数组内的对象 {
	 *		    ReportFormID: ReportFormID,
	 *		    DATE: DATE,
	 * 			SectionNo:record.get('SECTIONNO'),
	 *		    SectionType:record.get('SectionType'),
	 *		    CNAME: record.get('CNAME'),
	 *		    SAMPLENO: record.get('SAMPLENO'),
	 *		    PageName: record.get('PageName'),//纸张类型,A4/A5
	 *		    PageCount: record.get('PageCount'),//文件页量
	 *		    url: Shell.util.Path.reportPath + "/" + fileName
	 *		};
	 */
	/**@public 下载*/
	print: function (config, isView, pdfPrinter) {
		var me = this;

		me.i = me.i + 1;
		me.printmaps[me.i] = [{ config: config, isView: isView, pdfPrinter: pdfPrinter, isMerge: false }];

		if (me.i == 1) {
			me.printLogic(1);
		} else if (me.printmaps[me.i - 1] == null && me.isPrintEnd && me.printmaps[me.i] != null) {
			me.isPrintEnd = false;
			me.printLogic(me.i);
		}
	},
	printLogic: function (num) {
		var me = this;
		var allParameter = me.printmaps[num][0];
		var isView = allParameter.isView;
		var config = allParameter.config;
		var pdfPrinter = allParameter.pdfPrinter;
		me.mapkey = num;

		me.isView = (isView === true ? true : false);
		me.pdfPrinter = pdfPrinter;

		me.isMerge = false;
		//重置信息
		me.resetInfo();

		//清空iframe元素
		for (var i in me.iframeIds) {
			var f = document.getElementById(me.iframeIds[i]);
			if (f) {
				f.parentNode.removeChild(f);
			}
		}
		me.iframeIds = [];

		me.isDoublePrint = config.isDoublePrint;
		me.A4Type = config.A4Type;
		me.strPageName = config.strPageName;
		me.reportList = config.data;

		//先生成报告==>移动到指定位置==》再打包下载
		me.onServerCreateReport(function () {
			//me.fireEvent('printStart', me);	
			var reports = me.reportList;
			var len = reports.length;
			var info = "共生成" + len + "份报告  ";
			me.showProgresstInfo(info, false, false);
			//移动到指定位置
			me.showProgresstInfo('【开始复制报告】', true);
			//开始构造要打包的文件夹名称dateformat，格式为当前日期+guid
			var datetime = new Date();
			var year = datetime.getFullYear();//获取完整的年份(4位,1970)
			var month = datetime.getMonth() + 1;//获取月份(0-11,0代表1月,用的时候记得加上1)
			if (month <= 9) {
				month = "0" + month;
			}
			var date = datetime.getDate();//获取日(1-31)
			if (date <= 9) {
				date = "0" + date;
			}
			var dateformat = year + "-" + month + "-" + date;
			var guid = me.guid();
			var folderName = dateformat + guid;
			//名称构造结束
			//循环调用移动文件的方法
			for (var i = 0; i < len; i++) {
				me.moveThePDFByReportFormID(reports[i], folderName, len);
			}
			//只要有一个移动成功，就可以下载
			if (me.moveOneSuccess) {
				//开始打包下载
				me.zipAndDownload(folderName);
			} else {
				me.showProgresstInfo('文件移动全部失败,不能下载', false, true);
				me.endPrintDPF(false);
            }
		});
	},
	//生成guid
	guid:function () {
		function S4() {
			return(((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
		}
		return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
	},
	//打包下载
	zipAndDownload: function (folderPath) {
		var me = this;
		me.showProgresstInfo('【开始文件下载，请耐心等待...】', true);
		var me = this;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			method: 'GET',
			async: false,
			url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/ZipAndDowanloadThePDFByReportFormID?FolderPath=' + folderPath,
			success: function (response) {
				var response = Ext.decode(response.responseText);
				var ResultDataValue = JSON.parse(response.ResultDataValue);
				if (response.success) {
					window.location.href = Shell.util.Path.rootPath + "/" + ResultDataValue;
				} else {
					me.showProgresstInfo('文件下载失败！', false, true);

					Shell.util.Msg.showInfo(response.ErrorInfo);
				}
				//清空提示框的内容
				me.endPrintDPF(false);
			},
			error: function (response) {
				Shell.util.Msg.showInfo("下载失败！");
			}
		});
	},
	//移动报告单到指定路径Folderfield--要移动的目标文件夹的名字，Reportfields--要移动的文件信息
	moveThePDFByReportFormID: function (Reportfield, Folderfield, count) {

		var me = this;
		var ReportFormID = Reportfield.ReportFormID;
		var reportfields = [];
		reportfields[0] = Reportfield.url;
		var data = { Reportfields: reportfields, ReportFormID: ReportFormID, Folderfield: Folderfield };

		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			method: 'POST',
			async: false,
			url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/MoveThePDFByReportFormID',
			params: Ext.JSON.encode(data),
			success: function (response) {
				var response = Ext.decode(response.responseText);
				//var ResultDataValue = JSON.parse(response.ResultDataValue);
				if (response.success) {
					//只要有一个移动成功就可以进行后续的下载
					me.moveOneSuccess = true;
					//Shell.util.Msg.showInfo('连接成功');
					me.moveSuccessCount++;
					me.onPrintProgressbarChange(me.moveSuccessCount, count);//进度条
					me.showProgresstInfo(Reportfield.DATE + '' + Reportfield.CNAME + ' 移动成功');
					if (me.moveSuccessCount == count) {
						me.moveSuccessCount = 0;
                    }
				} else {
					me.showProgresstInfo(Reportfield.DATE + '' + Reportfield.CNAME + ' 移动失败');
					me.onPrintProgressbarChange(me.moveSuccessCount, count);
				}
			},
			error: function (response) {
				Shell.util.Msg.showInfo("下载失败！");
			}
		});
	},
	//打包并下载报告单
	//ZipAndDowanloadThePDF: function () {
	//	var FolderPath = '文件夹名称';
	//	Ext.Ajax.defaultPOSTHander = "application/json";
	//	Ext.Ajax.request({
	//		url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/ZipAndDowanloadThePDFByReportFormID?FolderPath=' + FolderPath,
	//		async: false,
	//		method: 'get',
	//		success: function (response) {

	//			var response = Ext.decode(response.responseText);
	//			var ResultDataValue = JSON.parse(response.ResultDataValue);
	//			if (response.success) {
	//				Shell.util.Msg.showInfo('链接成功' + ResultDataValue);
	//			} else {
	//				Shell.util.Msg.showInfo(response.ErrorInfo);
	//			}
	//			me.endPrintDPF(false);
	//		},
	//		error: function (response) {
	//			Shell.util.Msg.showInfo("下载失败！");
	//		}
	//	});
	//},
	/**显示下载的信息*/
	showProgresstInfo: function(info, isBold, isError, style) {
		var me = this;
		if(!me.showProgressInfoWin) return;
		
		me["progressInfoWin"+me.mapkey]= me["progressInfoWin"+me.mapkey] || Ext.create('Ext.window.Window', {
			title: '下载进度信息',
			//bodyStyle:'background-color:#ffffff;',
			dockedItems: [{
				xtype: 'toolbar',dock: 'top',itemId:'toolbar',
				items: [{xtype:'progressbar',width:'100%',itemId:'createProgressbar'}]
			},{
				xtype: 'toolbar',dock: 'top',itemId:'toolbar2',
				items: [{xtype:'progressbar',width:'100%',itemId:'printProgressbar'}]
			}],
			constrainHeader: true, //true将窗口约束到可见区域，false不限制窗体头部位置
			closeAction: 'hidden',
			autoScroll: true,
			modal:false,//设置是否添加遮罩
			//renderTo: me.ownerCt.getEl(),
			width: 300,
			height: 400,
			x: 100,
			y: 100
		});
		if(me.printmaps[me.mapkey][0].isMerge){
			me["progressInfoWin"+me.mapkey].modal = true;
		};		
		me["progressInfoWin"+me.mapkey].show();
		var dom = me["progressInfoWin"+me.mapkey].body.dom;

		var divStyle = {};
		if(isBold) divStyle['font-weight'] = 'bold';
		if(isError) divStyle['color'] = 'red';

		for(var i in style) {
			divStyle[i] = style[i];
		}

		var div = document.createElement('div');
		div.style['margin'] = '5px 10px';

		for(var i in divStyle) {
			div.style[i] = divStyle[i];
		}

		var node = document.createTextNode(info);
		div.appendChild(node);

		dom.appendChild(div);
	},

	/**重置信息*/
	resetInfo: function() {
		var me = this;
		//是否双面下载
		me.isDoublePrint = false;
		//A4报告的类型//A4/16开
		me.A4Type = null;
		//报告下载类型
		me.strPageName = null;
		//报告列表
		me.reportList = [];
		//下载文件列表(可能多份报告合并成一个文件)
		me.printList = [];
		//成功累加的报告ID
		me.SuccessIds = [];
		
		me.printNum = 0;
		if (me["progressInfoWin"+me.mapkey]) {
			me["progressInfoWin"+me.mapkey].update('');
			//生成报告进度变化
			me.onCreateProgressbarChange(0,'');
			//下载报告进度变化
			me.onPrintProgressbarChange(0,'');
		}
	},
	//后台生成报告
	onServerCreateReport:function(callback){
		var me = this,
			list = me.reportList;
			len = list.length;
			
		me.resultCount = 0;
		me.showProgresstInfo('【开始生成报告】',true);
		
		me.onCallback(0,callback);
	},
	onCallback:function(index,callback){
		var me = this,
			list = me.reportList;
			len = list.length,
			max = me.createCount;
		
		me.onServerCreateReportFive(index,function(){
			if(me.resultCount == len){
				//console.log(new Date() + '回调下载callback');
				callback();
			}else{
				me.onCallback(index + max,callback);
			}
		});
	},
	//五次循环
	onServerCreateReportFive:function(index,callback){
		var me = this,
			allList = me.reportList,
			allLen = allList.length,
			len = me.createCount;
			
		var max = index + len;
		for(var i=0;i<len;i++){
			if(index + i >=allLen) break;
			me.onServerCreateReportOne(index + i,function(){
				if(me.resultCount == max || me.resultCount >= allLen){
					//console.log(new Date() + '回调onServerCreateReportFive-callback-' + index);
					callback();
				}
			});
		}
	},
	//生成报告
	onServerCreateReportOne:function(index,callback){
		var me = this,
			url = Shell.util.Path.rootPath + me.createReportUrl,
			list = me.reportList,
			len = list.length,
            data = list[index];
        //根据判断是否合并下载       --2019-01-02 jing
        url = url + '?ReportFormID=' + data.ReportFormID + '&SectionNo=' + data.SectionNo + '&SectionType=' + data.SectionType;
        if (data.IsMerge) {
            url = Shell.util.Path.rootPath + me.MergeCreateReportUrl;
            url += "?ReportFormIdList=" + data.ReportFormID + "&SectionType=" + data.SectionType;
        }
		//console.log(index + ',ReportFormID=' + data.ReportFormID);
        me.getToServer(url, function(v) {
			me.resultCount++;
			var result = {};
			try{
				result = Ext.JSON.decode(v);
			}catch(err){
				result.success = false;
			}
			//生成报告进度变化
			me.onCreateProgressbarChange(me.resultCount,len);
			
			var info = '[' + (index + 1) + '] ' + data.DATE + ' ' + data.CNAME;
			if(result.success) {
				var value = Ext.JSON.decode(result.ResultDataValue);
				data.PageName = value.PageName;
				data.PageCount = value.PageCount;
				data.url = value.PDFPath;
				info += ' ' +  data.PageCount + '张 生成成功';
				me.showProgresstInfo(info,false,false);
			}else{
				info += ' 生成失败';
				me.showProgresstInfo(info,false,true);
			}
			callback();
//				if(me.resultCount == len){
//					callback();
//				}
		});
	},
	/**打印结束*/
	endPrintDPF: function(isClose) {
		var me = this;
		//清空选择框
		me.fireEvent('printEnd', me);
		if(isClose && me.printmaps[me.mapkey][0].isMerge == false){
			//延时操作，打印完毕以后隐藏打印状态框
			Shell.util.Action.delay(function () {
	                               //me["progressInfoWin"+me.mapkey].close();
	                            }, null, 1000);
		};
		me.isPrintEnd=true;
		var arr = 0;
		for(var property in me.printmaps){
	        if(Object.prototype.hasOwnProperty.call(me.printmaps,property)){
	            arr++;
	        };
		};
		me.printmaps[me.mapkey]=null;
		
		if(me.isPrintEnd && me.mapkey<arr && me.printmaps[me.mapkey+1] != null  && me.printmaps[me.mapkey+1][0].isMerge != false){
			printLogic(me.mapkey+1);
		};
		
		
	},
	//生成报告进度变化
	onCreateProgressbarChange: function (num, count) {
	    var me = this;
	    if (!me.showProgressInfoWin) {
	        return;
	    }
	    
        var createProgressbar = me["progressInfoWin"+me.mapkey].getComponent('toolbar').getComponent('createProgressbar');
			
		var value = num / count;
		var text = '报告生成' + num + '/' + count;
		
		createProgressbar.updateProgress(value,text,true);
	},
	//移动复制报告进度变化
	onPrintProgressbarChange: function (num, count) {
	    var me = this;
	    if (!me.showProgressInfoWin) {
	        return;
	    }
	    var printProgressbar = me["progressInfoWin"+me.mapkey].getComponent('toolbar2').getComponent('printProgressbar');
			
		var value = num / count;
		var text = '报告复制' + num + '/' + count;
		
		printProgressbar.updateProgress(value,text,true);
	}
});