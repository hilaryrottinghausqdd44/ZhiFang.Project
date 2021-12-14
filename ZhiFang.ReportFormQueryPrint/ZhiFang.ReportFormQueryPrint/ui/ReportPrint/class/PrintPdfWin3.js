/**
 * 打印窗口
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintPdfWin3', {
    extend: 'Shell.ux.panel.Panel',

    title: '打印进度信息',

    /**合并的数量*/
    mergePageCount: 2,
    /**每一个文件容器回收时间,毫秒数*/
    clreaTimes: 30000,
    /**开启打印次数累加功能*/
    openAddPrintTimes: true,

    /**打印完成后刷新列表*/
    refreshListAfterPrintIsOver: true,
    /**显示进度窗口*/
    showProgressInfoWin:true,
    

    /**A4报告的类型//A4/16开*/
    A4Type: null,
    /**报告打印类型*/
    strPageName:null,
    /**报告列表*/
    reportList: [],
    /**打印文件列表(可能多份报告合并成一个文件)*/
    printList: [],
    /**进度信息*/
    progressInfo: [],
    /**报告时间字段*/
    DateField: 'RECEIVEDATE',
    /**强制分页字段*/
    ForcedPagingField: '',

    /**合并文件服务A4*/
    DobuleA5MergeA4PDFFiles: "/ServiceWCF/ReportFormService.svc/DobuleA5MergeA4PDFFiles",
    Dobule32KMerge16KPDFFiles: "/ServiceWCF/ReportFormService.svc/Dobule32KMerge16KPDFFiles",
    /**增加打印次数服务路径*/
    addPrintTimesUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/ReportFormAddPrintTimes',

    initComponent: function () {
        var me = this;
        me.layout = 'fit';
        me.addEvents('printStart','printEnd');
        me.callParent(arguments);
    },
    pdfIsLoded: function (index,iframeId) {
        var me = this;
        var iframe = window.frames[iframeId];
        var pdf = iframe.document.getElementById('pdf');
        //var iframe = document.getElementById(iframeId);
        //var pdf = iframe.contentDocument.getElementById('pdf');
        
        if (pdf) {
            Shell.util.Msg.showLog("【加载状态】" + iframeId + " 时间:" + Shell.util.Date.toString(new Date()) + " STATE状态：" + pdf.readyState);
            Shell.util.Msg.showLog("【文件地址】:" + pdf.src);
            if (pdf.readyState == "4") {
                Shell.util.Msg.showLog("【加载成功】" + iframeId + " 时间：" + Shell.util.Date.toString(new Date()));
                me.printFile(index, iframeId);
                setTimeout(function () {
                    Shell.util.Msg.showLog("【回收容器】" + iframeId + " 时间：" + Shell.util.Date.toString(new Date()));
                    var f = document.getElementById(iframeId);
                    f.parentNode.removeChild(f);
                }, me.clreaTimes);
            } else {
                setTimeout(function () {
                    me.pdfIsLoded(index, iframeId);
                }, 100);
            }
        } else {
            setTimeout(function () {
                Shell.util.Msg.showLog("【加载PDF】" + iframeId + " 时间：" + Shell.util.Date.toString(new Date()));
                me.pdfIsLoded(index, iframeId);
            }, 100);
        }
        
    },
    /**初始化html内容*/
    initHtml: function (index) {
        var me = this;
        var url = me.printList[index].url;
        url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=" + url;

        var fileName = url.split('/').slice(-1)[0].split('.')[0].replace(/-/g, '_').replace(/;/g, '_');
        var iframeId = 'PDFWIN_IFRAME_' + fileName;

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
        me.pdfIsLoded(index, iframeId);
    },
    /**
     * 打印
     * config
     *     strPageName 打印报告类型//A4/A5/双A5 
     *     A4Type A4报告的类型//A4/16开
     *     list 打印的数据列表
     */
    print: function (config) {
        var me = this;
        //重置信息
        me.resetInfo();

        me.A4Type = config.A4Type;
        me.strPageName = config.strPageName;
        me.reportList = config.data;
        

        //开始准备打印
        if (me.strPageName == "双A5") {
            me.printDA5();
        } else {
            me.printA4A5();
        }
    },
    /**双A5打印*/
    printDA5: function () {
        var me = this,
            reportList = me.reportList || [],
            len = reportList.length,
            mergeCount = 0,
            mergeIndexs = [];

        for (var i = 0; i < len; i++) {
            var obj = reportList[i];

            //需要强制分页
            if (me.ForcedPagingField && i > 0) {
                var v = obj[me.ForcedPagingField.dataIndex];
                var oV = reportList[i - 1][me.ForcedPagingField.dataIndex];
                if (v != oV) {
                    if (mergeIndexs.length > 0) {
                        me.printList.push({
                            indexList: [].concat(mergeIndexs)
                        });
                        mergeCount = 0;
                        mergeIndexs = [];
                    }
                }
            }

            if (obj.PageName == "A4") {//A4纸独开一张
                if (mergeIndexs.length > 0) {
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
            } else {//连续的A5报告合并
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
        }

        if (mergeIndexs.length > 0) {
            me.printList.push({
                indexList: [].concat(mergeIndexs)
            });
        }

        me.printBegin();
    },
    /**A4/A5打印*/
    printA4A5: function () {
        var me = this,
            reportList = me.reportList,
            len = reportList.length;

        for (var i = 0; i < len; i++) {
            me.printList.push({
                url: reportList[i].url,
                indexList: [i]
            });
        }

        me.printBegin();
    },
    /**打印开始*/
    printBegin: function () {
        var me = this;
        me.fireEvent('printStart',me);
        //显示进度窗口
        me.showInfoWin();
        me.progressInfoWin.show();
        //准备打印
        me.onPrintReady();
		//打印
        me.printPDF(0);
    },
    /**显示一行打印信息*/
    showRowInfo:function(info,style){
    	var me = this;
    	
    	//显示进度窗口
    	if(!me.showProgressInfoWin) return;
    	
    	var div = document.createElement("div");
        div.style['margin'] = '5px';
        
        if(style){for(var i in style){div.style[i] = style[i];}}
        
        var node = document.createTextNode(info);
        div.appendChild(node);
        me.progressInfoWin.body.dom.appendChild(div);
    },
    /**显示进度窗口*/
    showInfoWin:function(){
	var me = this;
    	me.progressInfoWin = me.progressInfoWin || Ext.create('Ext.window.Window', {
            title: '打印进度信息',
            constrainHeader: true,//true将窗口约束到可见区域，false不限制窗体头部位置
            closeAction: 'hidden',
            autoScroll:true,
            width: 250,
            height: 400,
            y: 0
        });
	me.progressInfoWin.update('');
    },
    /**准备打印*/
	onPrintReady:function(){
		var me = this;
		me.showRowInfo('共 ' + me.printList.length + ' 份打印文件',{fontWeight:'bold'});
		me.showRowInfo('准备打印',{fontWeight:'bold'});
	},
    /**正在打印中*/
    isPrinting: function (index) {
        var me = this,
            reportList = me.reportList,
            obj = me.printList[index - 1],
            indexList = obj.indexList,
            len = indexList.length,
            ids = [],
            arr = [];
		
        me.showRowInfo('正在打印第  ' + index + ' 份文件...');
        
        for (var i = 0; i < len; i++) {
            var row = reportList[indexList[i]];
            ids.push(row.ReportFormID);
            me.showRowInfo(row.DATE + ' ' + row.CNAME + ' ' + row.PageName + ' ' + 
            	row.PageCount + '张',{marginLeft:'20px'});
        }
        
        if (me.openAddPrintTimes) {
            me.addPrintTimes(ids.join(',')); //增加打印次数
        }
    },
    /**打印完成*/
    printIsOver: function () {
        var me = this;
        me.showRowInfo('打印完成',{fontWeight:'bold'});
        me.endPrintDPF();
        if (me.refreshListAfterPrintIsOver) {
            me.ownerCt.onSearch();
        }
    },
    /**停止打印*/
    stopPrintPDF: function () {
        var me = this;
        me.showRowInfo('打印中断',{fontWeight:'bold',color:'red'});
        me.endPrintDPF();
    },
    /**打印结束*/
    endPrintDPF: function () {
        var me = this;
        if (me.stime != null) {
            clearTimeout(me.stime);
        }
        //重置信息
        me.resetInfo();
        me.fireEvent('printEnd',me);
    },
    /**重置信息*/
    resetInfo: function () {
        var me = this;
        //A4报告的类型//A4/16开
        me.A4Type = null;
        //报告打印类型
        me.strPageName = null;
        //报告列表
        me.reportList = [];
        //打印文件列表(可能多份报告合并成一个文件)
        me.printList = [];
        //进度信息
        me.progressInfo = [];
    },
    /**文件打印*/
    printPDF: function (index) {
        var me = this;

        if (me.strPageName == "双A5") {
            me.fileMerge(me.printList[index].indexList, function (url) {
                me.printList[index].url = url;
                me.initHtml(index);
            });
        } else {
            me.initHtml(index);
        }
    },
    printFile: function (index,iframeId) {
        var me = this;
        var iframe = window.frames[iframeId];
        
        iframe.PrintPdf();
        index++;
        
        setTimeout(function () {
            me.isPrinting(index);
            if (index < me.printList.length) {
                me.printPDF(index);//打开下一个PDF
            } else {
                me.printIsOver();
            }
        }, 100);
    },
    /**合并文件*/
    fileMerge: function (indexList,callback) {
        var me = this,
            reportList = me.reportList,
            A4Type = me.A4Type,
            urls = [];

        for (var i = 0; i < indexList.length; i++) {
            urls.push(reportList[indexList[i]].url);
        }

        var url = null;
        switch (A4Type) {
            case 1: url = me.DobuleA5MergeA4PDFFiles; break;
            case 2: url = me.Dobule32KMerge16KPDFFiles; break;
        }
        url = Shell.util.Path.rootPath + url + "?fileList=" + urls.join(",");
        url = encodeURI(url);

        me.getToServer(url, function (v) {
            var result = Ext.JSON.decode(v);

            if (result.success) {
                callback(result.ResultDataValue);
            } else {
                var errorInfo = "合并PDF文件失败！错误信息：" + result.ErrorInfo;
                me.showRowInfo(errorInfo,{fontWeight:'bold',color:'red'});
                me.stopPrintPDF();
            }
        });

        return value;
    },
    /**增加打印次数*/
    addPrintTimes: function (ids) {
        var me = this,
			url = me.addPrintTimesUrl + "?reportformidstr=" + ids;

        me.getToServer(url, function (v) {
            Shell.util.Msg.showLog("【打印累计】返回值=" + v);
            var result = Ext.JSON.decode(v);

            if (!result.success) {
                Shell.util.Msg.showLog("【打印累计错误】ids=" + ids);
            } else {
                Shell.util.Msg.showLog("【打印累计成功】ids=" + ids);
            }
        });
    }
});