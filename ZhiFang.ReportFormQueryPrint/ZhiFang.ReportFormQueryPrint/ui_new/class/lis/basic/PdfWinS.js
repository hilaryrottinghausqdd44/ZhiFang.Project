/**
 * 采用BS生成报告方式
 * @author Jcall
 * @version 2018-09-03
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
 */
Ext.define('Shell.class.lis.basic.PdfWinS', {
	extend: 'Shell.ux.panel.Panel',

	title: '打印进度信息',

	/**合并的数量*/
	mergePageCount: 2,
	/**是否自动销毁iframe,false时再次调用打印方法时销毁*/
	autoClear: false,
	/**每一个文件容器回收时间,毫秒数*/
	clreaTimes: 30000,
	/**延时处理时间*/
	setTimeoutTimes: 100,
	/**开启打印次数累加功能*/
	openAddPrintTimes: true,
	/**打印完成后刷新列表*/
	refreshListAfterPrintIsOver: true,
	/**显示进度窗口*/
	showProgressInfoWin: true,
	/**是否显示log信息*/
	openLog: false,

	/**报告时间字段*/
	DateField: 'RECEIVEDATE',
	/**强制分页字段*/
	ForcedPagingField: '',
	/**是否双面打印*/
	isDoublePrint: false,
	/**A4纸张类型，1(A4) 2(16开)*/
	A4Type: null,
	/**报告打印类型*/
	strPageName: null,
	/**报告列表*/
	reportList: [],
	/**打印文件列表(可能多份报告合并成一个文件)*/
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
	/**增加打印次数服务路径*/
	addPrintTimesUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/ReportFormAddUpdatePrintTimes',
	//生成报告服务
	createReportUrl:'/ServiceWCF/ReportFormService.svc/GetReportFormPDFByReportFormID',

	/**是否浏览打印*/
	isView: false,
	/**当前存在的iframeId*/
	iframeIds: [],
	
	/**是否需要选择打印机*/
	hasPdfPrinter:false,
	/**默认打印机*/
	defaultPrinter:'',
	/**PDF文件打印机*/
	pdfPrinter: '',
	
	//当前往打印机里面发送的文件总数
	printNum:0,
	//每次生成的数量
	createCount:5,
	//查询类型
    QueryType:'',
	//clodop预览打印标志
	clodopPreviewPrint:false,
	initComponent: function() {
		var me = this;
		//clodop打印相关关
		if (me.IsUseClodopPrint + "" == "true") {
			Ext.create('Shell.ux.print.Print').init(function (print) {
				me.clodopPrint = print;
			});
		}
		me.layout = 'fit';
		me.addEvents('printStart', 'printEnd');
		me.mergePageCount = me.mergePageCount || 2;
		me.clreaTimes = me.clreaTimes || 0;
		if(me.clreaTimes < 0) me.clreaTimes = 0;
		me.openAddPrintTimes = me.openAddPrintTimes === false ? false : true;
		
		if(me.hasPdfPrinter){
			//新建一个WScript.Shell对象     
			me.Shell = new ActiveXObject("WScript.Shell");
		}

		me.callParent(arguments);
	},

	/**
	 * @public 打印
	 * @example
	 * 	config
	 * 	   isDoublePrint 是否双面打印
	 *     strPageName 打印报告类型//A4/A5/双A5 
	 *     A4Type A4报告的类型//A4/16开
	 *     data 打印的数据列表
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
	/**@public 打印*/
	print: function(config, isView, pdfPrinter) {
		var me = this;
		me.isView = (isView === true ? true : false);
		me.pdfPrinter = pdfPrinter;
		//重置信息
		me.resetInfo();
		
		//清空iframe元素
		for(var i in me.iframeIds) {
			var f = document.getElementById(me.iframeIds[i]);
			f.parentNode.removeChild(f);
		}
		me.iframeIds = [];

		me.isDoublePrint = config.isDoublePrint;
		me.A4Type = config.A4Type;
		me.strPageName = config.strPageName;
		me.reportList = config.data;
		
		if(config .isurl){
			if (me.IsChrome) {
				    if (me.isDoublePrint) { //双面打印
				        me.onDoublePrint();
				    } else { //单面打印
				        me.ChromePrint(me.strPageName);
				    }
				    return;
				}
	
				if (me.isDoublePrint) { //双面打印
					me.onDoublePrint();
				} else { //单面打印
					if(me.strPageName == "双A5") {
						me.printDA5();
					} else {
						me.printA4A5();
					}
				}
		}else {
			//先生成报告再打印
			me.onServerCreateReport(function () {
			    var reports = me.reportList;
			    var len = reports.length;
			    
			    var info = "共生成" + len + "份报告  ";
			    me.showProgresstInfo(info, false, false);
			    
				me.showProgresstInfo('【开始打印报告】',true);
				me.showProgresstInfo('本次打印报告类型：' + me.strPageName);
	
				if (me.IsChrome && me.IsUseClodopPrint=='false') {
				    if (me.isDoublePrint) { //双面打印
				        me.onDoublePrint();
				    } else { //单面打印
				        me.ChromePrint(me.strPageName);
				    }
				    return;
				}
	
				if (me.isDoublePrint) { //双面打印
					me.onDoublePrint();
				} else { //单面打印
					if(me.strPageName == "双A5") {
						me.printDA5();
					} else {
						me.printA4A5();
					}
				}
			});
		}
			
		
	},

	ChromePrint: function(pageName) {
	    var me = this,
                reportList = me.reportList || [],
                len = reportList.length,
                mergeCount = 0,
                mergeIndexs = [];
	    var delReportList = [];
	    for (var i = 0; i < len; i++) {
	        var obj = reportList[i];

	        if (pageName == "A4") {
	            if (obj.PageName != "A4") {
	                delReportList.push(i);
	                continue;
	            }
	        }
	        if (pageName == "A5") {
	            if (obj.PageName != "A5") {
	                delReportList.push(i);
	                continue;
	            }
	        }
	        mergeIndexs.push(i);
	        mergeCount += parseInt(obj.PageCount || "1");
	        if (mergeCount >= me.mergePageCount) {
	            me.printList.push({
	                indexList: [].concat(mergeIndexs)
	            });

	            mergeCount = 0;
	            mergeIndexs = [];
	        }

	    }

	    if (mergeIndexs.length > 0) {
	        me.printList.push({
	            indexList: [].concat(mergeIndexs)
	        });
	    }

	    me.printBegin();
	},

	/**双A5打印*/
	printDA5: function() {
		var me = this,
			reportList = me.reportList || [],
			len = reportList.length,
			mergeCount = 0,
			mergeIndexs = [];

		for(var i = 0; i < len; i++) {
			var obj = reportList[i];
			//需要强制分页
			if(me.ForcedPagingField && i > 0) {
				var v = obj[me.ForcedPagingField.dataIndex];
				var oV = reportList[i - 1][me.ForcedPagingField.dataIndex];
				if(v != oV) {
					if(mergeIndexs.length > 0) {
						me.printList.push({
							indexList: [].concat(mergeIndexs)
						});
						mergeCount = 0;
						mergeIndexs = [];
					}
				}
			}

			if(obj.PageName == "A4") { //A4纸独开一张
				if(mergeIndexs.length > 0) {
					me.printList.push({
						indexList: [].concat(mergeIndexs)
					});
					mergeCount = 0;
					mergeIndexs = [];
				}
				
				me.printList.push({
					url: obj.url,
					indexList: [i]
				});
			} else { //连续的A5报告合并
				mergeIndexs.push(i);
				mergeCount += parseInt(obj.PageCount || "1");

				if(mergeCount >= me.mergePageCount) {
					me.printList.push({
						indexList: [].concat(mergeIndexs)
					});

					mergeCount = 0;
					mergeIndexs = [];
				}
			}
		}

		if(mergeIndexs.length > 0) {
			me.printList.push({
				indexList: [].concat(mergeIndexs)
			});
		}

		me.printBegin();
	},
	/**A4/A5打印*/
	printA4A5: function() {
		var me = this,
			reportList = me.reportList,
			len = reportList.length;

		for (var i = 0; i < len; i++) {
		    if (reportList[i].PageName != me.strPageName) {
		        continue;
		    }
		    me.printList.push({
				url: reportList[i].url,
				indexList: [i]
			});
		}

		me.printBegin();
	},

	/**打印开始*/
	printBegin: function() {
		var me = this;
		
		if(me.hasPdfPrinter){
			//往注册表中写入值=PDF打印机
			me.Shell.RegWrite("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\Device",
				me.pdfPrinter || me.defaultPrinter, "REG_SZ");
		}
		
		me.fireEvent('printStart', me);
		
		if(me.printList.length > 0){
			me.showProgresstInfo('共 ' + me.printList.length + ' 份打印文件需要打印');
			setTimeout(function() {
				me.printPDF(0);
			}, me.setTimeoutTimes);
		}else{
			me.showProgresstInfo('没有需要打印的报告！',true,true);
			me.endPrintDPF();
		}
	},
	/**正在打印中*/
	isPrinting: function(index, callback) {
		var me = this,
			reportList = me.reportList,
			obj = me.printList[index - 1],
			indexList = obj.indexList,
			len = indexList.length,
			ids = [],
			arr = [];

		me.showProgresstInfo('正在打印第  ' + index + ' 份文件...');
		me.printNum += len;
		if (me.IsChrome && me.IsUseClodopPrint == 'false') {
		    me.onPrintProgressbarChange(me.printNum, len);//打印报告进度变化
		} else {
		    me.onPrintProgressbarChange(me.printNum, me.reportList.length);//打印报告进度变化
		}
		

		for(var i = 0; i < len; i++) {
			var row = reportList[indexList[i]];
			ids.push(row.ReportFormID);

			var info = row.DATE + ' ' + row.CNAME + ' ' + row.PageName + ' ' + row.PageCount + '张';
			me.showProgresstInfo(info, false, false, {
				marginLeft: '20px'
			});
		}

		if(me.openAddPrintTimes) {
			me.addPrintTimes(ids.join(","), callback); //增加打印次数
		} else {
			callback();
		}
	},
	/**打印完成*/
	printIsOver: function() {
		var me = this;
		me.showProgresstInfo('打印完成', true);
		me.endPrintDPF();
	},
	/**停止打印*/
	stopPrintPDF: function() {
		var me = this;
		me.showProgresstInfo('打印中断', true, true);
		me.endPrintDPF();
	},
	/**打印结束*/
	endPrintDPF: function() {
		var me = this;
		
		if(me.pdfPrinter){
			setTimeout(function(){
				//往注册表中写入值=默认打印机
				me.Shell.RegWrite("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\Device",
					me.defaultPrinter, "REG_SZ");
			},100);
		}
		
		var ids = Ext.clone(me.SuccessIds);
		//重置信息
		//me.resetInfo();

		var url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=";
		var iframeId = 'PDFWIN_IFRAME_END';
		me.iframeIds.push(iframeId);

		var iframe = document.createElement('iframe');
		iframe.id = iframeId;
		iframe.src = url;

		iframe.style['overflow'] = 'hidden';
		iframe.style['overflow-x'] = 'hidden';
		iframe.style['overflow-y'] = 'hidden';
		iframe.style['height'] = '100%';
		iframe.style['width'] = '100%';
		iframe.style['position'] = 'absolute';
		iframe.style['top'] = '-10000px';
		iframe.style['left'] = '-10000px';
		iframe.style['right'] = '-10000px';
		iframe.style['bottom'] = '-10000px';

		me.getEl().appendChild(iframe);

		if(me.autoClear && me.clreaTimes) {
			setTimeout(function() {
				var f = document.getElementById('PDFWIN_IFRAME_END');
				f.parentNode.removeChild(f);
				me.showLog('iframeIsLoaded-removeChild：PDFWIN_IFRAME_END');
			}, me.clreaTimes);
		}
		me.fireEvent('printEnd', me, ids);
		
		//延时操作，打印完毕以后隐藏打印状态框
		/*Shell.util.Action.delay(function () {
                                me.progressInfoWin.close();
                            }, null, 1000);*/
	},
	/**显示打印的信息*/
	showProgresstInfo: function(info, isBold, isError, style) {
		var me = this;
		if(!me.showProgressInfoWin) return;
		var modal1=true;
		if(me.NewWindowLoadIframeToPrint=='true'){
			modal1=false;
		}
		me.progressInfoWin = me.progressInfoWin || Ext.create('Ext.window.Window', {
			title: '打印进度信息',
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
			modal:modal1,//设置是否添加遮罩
			//renderTo: me.ownerCt.getEl(),
			width: 300,
			height: 400,
			x: 10,
			y: 10
		});

		me.progressInfoWin.show();
		var dom = me.progressInfoWin.body.dom;

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
		//是否双面打印
		me.isDoublePrint = false;
		//A4报告的类型//A4/16开
		me.A4Type = null;
		//报告打印类型
		me.strPageName = null;
		//报告列表
		me.reportList = [];
		//打印文件列表(可能多份报告合并成一个文件)
		me.printList = [];
		//成功累加的报告ID
		me.SuccessIds = [];
		
		me.printNum = 0;
		if (me.progressInfoWin) {
			me.progressInfoWin.update('');
			//生成报告进度变化
			me.onCreateProgressbarChange(0,'');
			//打印报告进度变化
			me.onPrintProgressbarChange(0,'');
		}
	},

	/**文件打印*/
	printPDF: function(index) {
		var me = this;
		//clodop修改：谷歌浏览器如果选择用clodop打印则不合并，否则按原逻辑
		if (me.IsChrome && me.IsUseClodopPrint=='false') {
		    if (me.isDoublePrint) { //双面打印
		        me.onMergeFile(me.printList[index].urlList, function (url) {
		            me.printList[index].url = url;
		            me.GetPdf(index);
		        });
		    } else {
		        me.fileMergeAll(me.printList[index].indexList, function (url) {
		            me.printList[index].url = url;
		            me.GetPdf(index);
		        });
		    }
		    return;
		}
		if(me.isDoublePrint) { //双面打印
			me.onMergeFile(me.printList[index].urlList, function(url) {
				me.printList[index].url = url;
				if(me.IsUseClodopPrint == 'true'){
					me.ClodopPrint(index);
				}else{
					me.initIframeHtml(index);
				}
			});
		} else { //单面打印
		    if (me.strPageName == "双A5") {
				me.fileMerge(me.printList[index].indexList, function(url) {
					me.printList[index].url = url;
					if(me.IsUseClodopPrint == 'true'){
						me.ClodopPrint(index);
					}else{
						me.initIframeHtml(index);
					}
				});
			} else {
				if(me.IsUseClodopPrint == 'true'){
					me.ClodopPrint(index);
				}else{
					me.initIframeHtml(index);
				}
			}
		}
	},
	//clodop打印方式
	ClodopPrint:function(index){
		//初始化clodop打印组件
		ZFPrint.init();
		var me = this;
		
		var isPreviewPrint = false;//是否预览打印
		if(me.clodopPreviewPrint){
			var urlListPreview = [];
			for(var i =0 ;i < me.printList.length;i++){
				//处理url为clodop可用的url
				var urlPreview = me.printList[i].url;
				var pdfurlPreview = Shell.util.Path.rootPath + "/" + urlPreview;
				var finalUrlPreview = encodeURI(pdfurlPreview);//地址中有中文，必须转义clodop才能获取到pdf
				urlListPreview.push(finalUrlPreview);
			}
			//预览打印
			ZFPrint.pdf.preview(urlListPreview, '正在预览打印',function() {
				var reportList = me.reportList,
				ids = [];
				for(var i = 0; i < reportList.length; i++) {
					var row = reportList[i];
					ids.push(row.ReportFormID);
		
				}
				//增加打印次数
				if(me.openAddPrintTimes) {
					me.addPrintTimes(ids.join(","),function(){
						me.printIsOver();
					}); 
				}
				//预览打印时通过页面按钮点击打印，打印完后要把预览标志重置为false
				me.clodopPreviewPrint=false;
			});
			
		}else{//直接打印
			var urlList=[];
			//处理url为clodop可用的url
			var url = me.printList[index].url;
			var pdfurl= Shell.util.Path.rootPath + "/" + url;
			var finalUrl = encodeURI(pdfurl);//地址中有中文，必须转义clodop才能获取到pdf
			urlList.push(finalUrl);
			//直接打印
			index++;
			ZFPrint.pdf.print(urlList,'正在打印',function() {
				me.isPrinting(index, function() {
					if(index < me.printList.length) {
						me.printPDF(index); //打开下一个PDF
					} else {
						me.printIsOver();
					}
				});
			});
			
		}
	},
    /**获得pdf*/
	GetPdf: function (index) {
	    var me = this;
	    var url = Shell.util.Path.rootPath + "/" + me.printList[index].url;
		$.ajax({url:url,success:function(result){
		}}); 
		if(me.NewWindowLoadIframeToPrint=='true'){
			Ext.onReady(function() {
			
				var win = new Ext.Window({
					id:'win'
					,width:1000
					,height:720
					,closable:true
					,resizable:true
					,minimizable:true
					,maximizable:true,
					x: 550,
       				layout : 'absolute'
					,html: '<iframe id="oh_no" src="' + url +
					'" height="100%" width="100%" frameborder="0" ' +
					'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;' +
					'position:absolute;top:0px;left:0px;right:0px;bottom:0px;" ' +
				'></iframe>'
					
				});
			
				win.show();
			
			});
		}else{
			var iframe = document.createElement('iframe');
			iframe.appendChild(document.createElement('body'));
			me.getEl().appendChild(iframe);
			var doc = iframe.contentWindow.document;
			doc.body.innerHTML = '<iframe id="oh_no" src="' + url +
							'" height="100%" width="100%" frameborder="0" ' +
							'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;' +
							'position:absolute;top:0px;left:0px;right:0px;bottom:0px;" ' +
							/*'onload="if(this.contentDocument.head){' +
								'this.contentDocument.write(\'<div style=\\\'margin:20px;text-align:center;\\\'><b>报告文件还未生成，请耐心等待！</b></div>\');' +
							'};"' +*/
						'></iframe>';
			var p = doc.getElementById("oh_no");
			p.contentWindow.print();
		}
	    index++;
	    setTimeout(function () {
	        me.isPrinting(index, function () {
	            if (index < me.printList.indexList) {
	                me.printPDF(index); //打开下一个PDF
	            } else {
	                me.printIsOver();
	            }
	        });
	    }, me.setTimeoutTimes);
	},

	/**初始化html内容*/
	initIframeHtml: function(index) {
		var me = this;
		var url = me.printList[index].url;
		var pdfurl = Shell.util.Path.rootPath + "/" + url;
		$.ajax({url:pdfurl,success:function(result){
	    }}); 
		url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=" + url;
		
		var fileName = url.split('/').slice(-1)[0].split('.')[0].replace(/-/g, '_').replace(/;/g, '_');
		var iframeId = 'PDFWIN_IFRAME_' + fileName;
		me.iframeIds.push(iframeId);

		var iframe = document.createElement('iframe');
		iframe.id = iframeId;
		iframe.name = 'PDFWIN_IFRAME';
		iframe.src = url;

		iframe.style['overflow'] = 'hidden';
		iframe.style['overflow-x'] = 'hidden';
		iframe.style['overflow-y'] = 'hidden';
		iframe.style['height'] = '100%';
		iframe.style['width'] = '100%';
		iframe.style['position'] = 'absolute';
		iframe.style['top'] = '-10000px';
		iframe.style['left'] = '-10000px';
		iframe.style['right'] = '-10000px';
		iframe.style['bottom'] = '-10000px';

		if(iframe.attachEvent) {
			iframe.attachEvent("onload", function() {
				me.showLog('iframe.attachEvent-onload');
				me.iframeIsLoaded(index, iframeId);
			});
		} else {
			iframe.onload = function() {
				me.iframeIsLoaded(index, iframeId);
			};
		}
		me.showLog('initIframeHtml-appendChild：' + iframeId);
		me.getEl().appendChild(iframe);
	},
	iframeIsLoaded: function(index, iframeId) {
		var me = this;
		var iframe = window.frames[iframeId];

		//if (!Ext.isIE) iframe = iframe.contentWindow;
		var pdf = iframe.document.getElementById('pdf');

		var row = me.reportList[index];
		if(pdf) {
			me.showLog('iframeIsLoaded-true:' + row.CNAME + ';' + row.url + ';readyState=' + pdf.readyState);
			if(pdf.readyState == "4") {
				me.printFile(index, iframeId);
				me.showLog('iframeIsLoaded-afterPrintFile');
				if(me.autoClear && me.clreaTimes) {
					setTimeout(function() {
						var f = document.getElementById(iframeId);
						f.parentNode.removeChild(f);
						me.showLog('iframeIsLoaded-removeChild：' + iframeId);
					}, me.clreaTimes);
				}
			} else {
				setTimeout(function() {
					me.iframeIsLoaded(index, iframeId);
				}, me.setTimeoutTimes);
			}
		} else {
			me.showLog('iframeIsLoaded-false:' + row.CNAME + ';' + row.url);
			setTimeout(function() {
				me.iframeIsLoaded(index, iframeId);
			}, me.setTimeoutTimes);
		}

	},

	printFile: function(index, iframeId) {
		var me = this;
		var iframe = window.frames[iframeId];
		me.showLog('printFile');

		if(me.isView) {
			iframe.PrintPreview();
		} else {
		    //iframe.Print();
			iframe.PrintPdf();
		}

		index++;
		setTimeout(function() {
			me.isPrinting(index, function() {
				if(index < me.printList.length) {
					me.printPDF(index); //打开下一个PDF
				} else {
					me.printIsOver();
				}
			});
		}, me.setTimeoutTimes);
	},

    /**合并文件所有*/
	fileMergeAll: function (indexList, callback) {
	    var me = this,
			reportList = me.reportList,
			A4Type = me.A4Type,
			urls = [],
			    pageType = [];
	    for (var i = 0; i < indexList.length; i++) {
	        urls.push(reportList[indexList[i]].url);
	        pageType.push(reportList[indexList[i]].PageName);
	    }

	    var url = null;
	    var fileFlag = 0;
	    url = Shell.util.Path.rootPath + me.BlendPDF;
	    me.postToServer(url, Ext.JSON.encode({
	        fileList: urls.join(","), pageType: pageType.join(",")
	    }), function (v) {
	        var result = Ext.JSON.decode(v);
	        if (result.success) {
	            callback(result.ResultDataValue);
	        } else {
	            var info = "合并PDF文件失败！错误信息：" + result.ErrorInfo;
	            me.showProgresstInfo(info, true, true);
	            me.stopPrintPDF();
	        }
	    }, false, 120000);
	},

	/**合并文件*/
	fileMerge: function(indexList, callback) {
		var me = this,
			reportList = me.reportList,
			A4Type = me.A4Type,
			urls = [];
		//console.log(A4Type);
		//console.log(reportList);
		for (var i = 0; i < indexList.length; i++) {
            //A4报告不合并
		    if (reportList[indexList[i]].PageName == "A4") {
		        callback(reportList[indexList[i]].url);
		        return;
		    }
		    urls.push(reportList[indexList[i]].url);
		    //if (reportList[indexList[i]].PageName != 'A5') {
		    //    A4Type = 3;
		    //}
		}
        
		var url = null;
		switch(A4Type) {
			case 1:
				url = me.DobuleA5MergeA4PDFFiles;
				break;
			case 2:
				url = me.Dobule32KMerge16KPDFFiles;
				break;
		}
		url = Shell.util.Path.rootPath + url;

		me.postToServer(url, Ext.JSON.encode({
			fileList: urls.join(",")
		}), function(v) {
			var result = Ext.JSON.decode(v);
			if (result.success) {
				callback(result.ResultDataValue);
			} else {
				var info = "合并PDF文件失败！错误信息：" + result.ErrorInfo;
				me.showProgresstInfo(info, true, true);
				me.stopPrintPDF();
			}
		}, false, 120000);
	},
	/**增加打印次数*/
	addPrintTimes: function(ids, callback) {
	    var me = this,
			url = me.addPrintTimesUrl + "?reportformidstr=" + ids;
		if(me.QueryType=='REQUEST'){
			url = url.replace("ReportFormAddUpdatePrintTimes","RequestFormAddUpdatePrintTimes");
		}else{
			url = url.replace("RequestFormAddUpdatePrintTimes","ReportFormAddUpdatePrintTimes");
		};
		me.getToServer(url, function(v) {
			var result = Ext.JSON.decode(v);

			if(result.success) {
				me.SuccessIds = me.SuccessIds.concat(ids.split(","));
			}
			callback();
		});
	},
	/**在弹出面版中显示log信息*/
	showLog: function(msg) {
		var me = this;
		if(!me.openLog) return;
		me.showProgresstInfo('【' + Shell.util.Date.toString(new Date().getTime(), false, true) + '】 ' + msg);
	},

	//==================支持双面打印=====================
	/**双面打印*/
	onDoublePrint: function() {
		var me = this;
		me.printList = me.getPrintList();
		me.printBegin();
	},
	/**获取打印报告列表*/
	getPrintList: function() {
		var me = this,
			reportList = me.reportList || [],
			len = reportList.length,
			list = [];

		if(!me.ForcedPagingField) { //不强制切分
			list.push({
				urlList: [],
				indexList: []
			});
			for(var i = 0; i < len; i++) {
				var obj = reportList[i];
				list.urlList.push(reportList[i].url);
				list.indexList.push(i);
			}
		} else { //强制切分
			var patient = {};
			for(var i = 0; i < len; i++) {
				var obj = reportList[i];
				patient[obj[me.ForcedPagingField.dataIndex]] =
					patient[obj[me.ForcedPagingField.dataIndex]] || {
						urlList: [],
						indexList: []
					};
				patient[obj[me.ForcedPagingField.dataIndex]].urlList.push(reportList[i].url);
				patient[obj[me.ForcedPagingField.dataIndex]].indexList.push(i);
			}

			for(var i in patient) {
				list.push(patient[i]);
			}
		}

		return list;
	},
	/**合并报告*/
	onMergeFile: function(urls, callback) {
		var me = this,
			url = null;

		if(me.A4Type == 1) { //A4
			if(me.strPageName == "A4") {
				url = me.MergeA4PDFFiles;
			} else {
				url = me.DobuleA5MergeA4PDFFiles
			}
		} else if(me.A4Type == 2) { //16K
			if(me.strPageName == "A4") {
				url = me.Merge16KPDFFiles;
			} else {
				url = me.Dobule32KMerge16KPDFFiles
			}
		}
		url = Shell.util.Path.rootPath + url;

		me.postToServer(url, Ext.JSON.encode({
			fileList: urls.join(",")
		}), function(v) {
			var result = Ext.JSON.decode(v);
			if(result.success) {
				callback(result.ResultDataValue);
			} else {
				var info = "合并PDF文件失败！错误信息：" + result.ErrorInfo;
				me.showProgresstInfo(info, true, true);
				me.stopPrintPDF();
			}
		}, false, 120000);
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
				//console.log(new Date() + '回调打印callback');
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
			if(me.QueryType=='REQUEST'){
				url = url.replace("GetReportFormPDFByReportFormID","GetRequestFormPDFByReportFormID");
			}else{
				url = url.replace("GetRequestFormPDFByReportFormID","GetReportFormPDFByReportFormID");
			};
		//console.log(index + ',ReportFormID=' + data.ReportFormID);
		url = url + '?ReportFormID=' + data.ReportFormID + '&SectionNo=' + data.SectionNo + '&SectionType=' + data.SectionType;
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
				info += ' ' + data.PageName + ' ' + data.PageCount + '张 生成成功';
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
	//生成报告进度变化
	onCreateProgressbarChange: function (num, count) {
	    var me = this;
	    if (!me.showProgressInfoWin) {
	        return;
	    }
	    
        var createProgressbar = me.progressInfoWin.getComponent('toolbar').getComponent('createProgressbar');
			
		var value = num / count;
		var text = '报告生成' + num + '/' + count;
		
		createProgressbar.updateProgress(value,text,true);
	},
	//打印报告进度变化
	onPrintProgressbarChange: function (num, count) {
	    var me = this;
	    if (!me.showProgressInfoWin) {
	        return;
	    }
	    var printProgressbar = me.progressInfoWin.getComponent('toolbar2').getComponent('printProgressbar');
			
		var value = num / count;
		var text = '已送往打印机' + num + '/' + count;
		
		printProgressbar.updateProgress(value,text,true);
	}
});