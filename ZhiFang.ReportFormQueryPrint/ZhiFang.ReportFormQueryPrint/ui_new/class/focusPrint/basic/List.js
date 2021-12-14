/**
 * 报告列表
 * @author Jcall
 * @version 2018-09-03
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
 * @author GHX
 * @version 2019-10-16
 */
Ext.define('Shell.class.focusPrint.basic.List',{
    extend: 'Shell.ux.panel.Grid',

	title: '',
	width:0,
	height:0,
    /**最大打印数量*/
    maxPrintTimes:2,
    /**默认勾选双面打印*/
	checkDoublePrint: false,
	/**是否需要选择打印机*/
	hasPdfPrinter:false,
	/**PDF文件打印机数组*/
	pdfPrinterList: [],
	/**默认打印机*/
	_defaultPrinter:'',
	afterRender: function () {
	    var me = this;
	    me.callParent(arguments);
	    
	},
	initComponent:function(){
	    var me = this;
		me.addEvents('beforeSearch');
		me.callParent(arguments);
	},
	
    /**创建挂靠*/
	createDockedItems: function () {
	    var me = this;
	    var dockedItems = me.callParent(arguments);
	    
	    if(me.hasPdfPrinter){
	    	//新建一个WScript.Shell对象     
			me.Shell = new ActiveXObject("WScript.Shell");
			//默认打印机
			me._defaultPrinter = me.Shell.RegRead("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\Device");
			
			var defaultPrinter = me._defaultPrinter.split(',')[0],
				list = [],
				len = list.length,
				data = [];
			
			data.push([defaultPrinter,'【默认打印机】' + defaultPrinter]);//默认打印机
			//报告打印机
			for(var i=0;i<len;i++){
				data.push([list[i],'【报告打印机】' + list[i]]);
			}
			
	    	//打印机选择
		    dockedItems.unshift({
		    	dock:'top',
		    	xtype:'toolbar',
		    	itemId:'toptoolbar2',
		    	items:[{
		    		xtype:'combobox',itemId:'printer',
		            mode:'local',editable:false,
		            displayField:'text',valueField:'value',
		            width:'100%',fieldLabel:'打印机',labelWidth:50,
		            labelAlign:'right',value:defaultPrinter,
		            store:new Ext.data.SimpleStore({
		                fields: ['value','text'],
		                data: data
		            })
		    	}]
		    });
	    }

	    dockedItems.push(Ext.create('Shell.class.focusPrint.basic.PdfWinS', {
	        itemId: 'PdfWin',
	        header: false,
	        height: 0,
	        width: 0,
	        //mergePageCount: me.mergePageCount,
	        //clreaTimes: me.clreaTimes,
	        //openAddPrintTimes: me.openAddPrintTimes,
	        //DateField: me.DateField,
	        //ForcedPagingField: me.ForcedPagingField,
	        //hasPdfPrinter:me.hasPdfPrinter,//是否需要选择打印机
	       // defaultPrinter:me._defaultPrinter,//默认打印机
	        listeners:{
	        	printStart:function(p){
	        		//me.disableControl();
	        	},//禁用功能按钮
	        	printEnd:function(p,ids){
	        		//me.enableControl();
	        		//me.onPrintEnd(ids);
	        	}//启用功能按钮
	        }
	    }));
	    
	    return dockedItems;
	},
    /**直接打印处理*/
    PrintClick: function (record){
		var me = this;
	    /**
		 * 批量打印功能只支持IE8以上版本
		 * @JcallShell
		 * @version 2018-07-04
		 */
		var IEVersion = Shell.util.IE.getIEVersion();
		//非IE、IE6、IE7、Edge的浏览器需要下载IE8
	    if (IEVersion == -1 || IEVersion == 6 || IEVersion == 7 || IEVersion == 'edge') {
	        if (Ext.isChrome) {
	            me.getComponent("PdfWin").IsChrome = true;
	        } else {
	            Shell.util.Msg.showMsg({
	                title: '提示信息',
	                msg: '批量打印功能只支持IE8以上版本!需要下载IE8浏览器吗？',
	                icon: Ext.Msg.QUESTION,
	                buttons: Ext.Msg.OKCANCEL,
	                callback: function (btn) {
	                    if (btn != "ok") return;
	                    window.open(Shell.util.Adobe.IE8Url);
	                }
	            }, me);
	            return;
	        }
		}
		
		/**
		 * 打印必须先安装adobe组件，如果没有安装，弹出安装文件下载
		 * @JcallShell
		 * @version 2018-07-04
		 */
		var isAcrobatPluginInstall = Shell.util.Adobe.isInstall();
		if(!isAcrobatPluginInstall){
			window.open(Shell.util.Adobe.Url);
			return;
		}
		
        me.printAll(record);
       
	},
	/**打印勾选的报告*/
	printAll:function(records){
		var me = this,
			PdfWin = me.getComponent('PdfWin');
			
		if (PdfWin) {
			if(me.hasPdfPrinter){
				var printer = me.getComponent('toptoolbar2').getComponent('printer').getValue();
				PdfWin.print(me.getReportPrintInfo(records),null,printer);
			}else{
				PdfWin.print(me.getReportPrintInfo(records));
			}
		} else {
		    Shell.util.Msg.showError('打印组件加载失败！',me);
		}
    },
	/**获取勾选的数据*/
	getReportPrintInfo:function(reco){
	    var me = this,
			hasFilter =  null,
			isDoublePrint =  null,
			records = reco,
			len = records.length,
			data = [];
			
		/**
		 * 解决列表勾选不能按页面顺序打印的处理
		 * @JcallShell
		 * @version 2018-07-28
		 */
		var count = len,
			tempArr = [];
		for(var i=0;i<count;i++){
			tempArr.push(null);
		}
		for(var i=0;i<len;i++){
			var rec = records[i];
			tempArr[i] = rec;
		}
			
		for(var i=0;i<count;i++){
			if(tempArr[i] == null) continue;
			if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			
			var record = tempArr[i],
				ReportFormID = record.ReportFormID,
				DATE = Shell.util.Date.toString(record.RECEIVEDATE, true),
				fileName = DATE + "/" + ReportFormID + ".pdf";
				
			var obj = {
			    ReportFormID: ReportFormID,
			    DATE: DATE,
			    SectionNo:record.SECTIONNO,
			    SectionType:record.SectionType,
			    CNAME: record.CNAME,
			    SAMPLENO: record.SAMPLENO,
			    PageName: record.PageName,//纸张类型,A4/A5
			    PageCount: record.PageCount,//文件页量
			    url: Shell.util.Path.reportPath + "/" + fileName
			};
			if (me.ForcedPagingField) {
			    obj[me.ForcedPagingField.dataIndex] = record.get(me.ForcedPagingField.dataIndex);
			}
			data.push(obj);
		}
		
		return {
		    A4Type:1,
			strPageName:"双A5",
			isDoublePrint:isDoublePrint,
			data:data
		};
   },
    /**初始化html内容*/
	initIframeHtml: function(pdfurl) {
		var me = this;
		var url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=" + pdfurl;
		
		var fileName = url.split('/').slice(-1)[0].split('.')[0].replace(/-/g, '_').replace(/;/g, '_');
		var iframeId = 'PDFWIN_IFRAME_' + fileName;

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
				me.iframeIsLoaded(iframeId);
			});
		} else {
			iframe.onload = function() {
				me.iframeIsLoaded(iframeId);
			};
		}
		me.getEl().appendChild(iframe);
	},
	iframeIsLoaded: function(iframeId) {
		var me = this;
		var iframe = window.frames[iframeId];
		var pdf = iframe.document.getElementById('pdf');
		if(pdf) {
			if(pdf.readyState == "4") {
				me.printFile(iframeId);
				if(me.autoClear && me.clreaTimes) {
					setTimeout(function() {
						var f = document.getElementById(iframeId);
						f.parentNode.removeChild(f);
						me.showLog('iframeIsLoaded-removeChild：' + iframeId);
					}, me.clreaTimes);
				}
			} else {
				setTimeout(function() {
					me.iframeIsLoaded(iframeId);
				}, me.setTimeoutTimes);
			}
		} else {
			setTimeout(function() {
				me.iframeIsLoaded(iframeId);
			}, me.setTimeoutTimes);
		}
	},

	printFile: function(iframeId) {
		var me = this;
		var iframe = window.frames[iframeId];
		iframe.PrintPdf();
	},
	getCookie:function (name) {
		var cookies = document.cookie;
		var list = cookies.split("; ");    // 解析出名/值对列表
		      
		for(var i = 0; i < list.length; i++) {
			var arr = list[i].split("=");  // 解析出名和值
			if(arr[0] == name)
				return decodeURIComponent(arr[1]);  // 对cookie值解码
		} 
		return "";
	}
		
});