/**
 * 打印窗口
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintPdfWin', {
    extend: 'Shell.ux.panel.Panel',

    title: '打印进度信息',

    /**循环间隔时间*/
    ftime: 7000,
    /**加载等待时间*/
    wtime: 1000,
    /**合并的数量*/
    mergePageCount: 2,
    /**iframe内容加载中*/
    iframeLoading: false,
    /**加载次数*/
    loadingCount: 0,
    /**最大加载次数*/
    maxLoadingCount: 15,

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

    /**合并文件服务A4*/
    DobuleA5MergeA4PDFFiles: "/ServiceWCF/ReportFormService.svc/DobuleA5MergeA4PDFFiles",
    Dobule32KMerge16KPDFFiles: "/ServiceWCF/ReportFormService.svc/Dobule32KMerge16KPDFFiles",
    /**增加打印次数服务路径*/
    addPrintTimesUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/ReportFormAddPrintTimes',

    initComponent: function () {
        var me = this;

        Shell.util.Function.printPDF = me.printPDF;

        me.layout = 'fit';
        me.initHtml();

        me.callParent(arguments);
    },
    /**初始化html内容*/
    initHtml: function () {
        this.html =
            '<iframe id="pdfWin_iframe" style="overflow:hidden;overflow-x:hidden;' +
            'overflow-y:hidden;height:100%;width:100%;position:absolute;' +
            'top:-1000px;left:-1000px;right:0px;bottom:0px"></iframe>';
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
            if (obj.PageName == "A4") {//A4纸独开一张
                me.printList.push({
                    indexList: [].concat(mergeIndexs)
                });

                mergeCount = 0;
                mergeIndexs = [];

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
        me.progressInfo.push("<b>共 " + me.printList.length + " 份打印文件</b>");
        me.progressInfo.push("<b>准备打印</b>");
        me.showProgresstInfo();

        setTimeout(function () { me.printPDF(0, true) }, 200);
    },
    /**显示打印的信息*/
    showProgresstInfo: function () {
        var me = this,
            html = '<div style="padding:10px;">' + me.progressInfo.join("</br>") + '</div>';

        me.progressInfoWin = me.progressInfoWin || Ext.create('Ext.window.Window', {
            title: '打印进度信息',
            constrainHeader: true,//true将窗口约束到可见区域，false不限制窗体头部位置
            closeAction: 'hidden',
            autoScroll:true,
            renderTo: me.ownerCt.getEl(),
            width: 250,
            height: 400,
            y: 0
        });
        me.progressInfoWin.update(html);
        me.progressInfoWin.show();

        var height = me.progressInfoWin.getEl().getHeight();
        me.progressInfoWin.body.scrollTo('top', height + 50, true);
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

        me.progressInfo.push("正在打印第  <b>" + index + "</b> 份文件...");

        for (var i = 0; i < len; i++) {
            var row = reportList[indexList[i]];
            ids.push(row.ReportFormID);
            arr.push("<a style='margin-left:20px;'>" + row.RECEIVEDATE + " " + row.CNAME +
                " " + row.PageName + " " + row.PageCount + "张</a>");
        }
        
        me.progressInfo.push(arr.join("</br>"));

        me.showProgresstInfo();

        //增加打印次数
        me.addPrintTimes(ids.join(","));
    },
    /**打印完成*/
    printIsOver: function () {
        var me = this;
        me.progressInfo.push("<b>打印完成</b>");
        me.showProgresstInfo();
        me.endPrintDPF();
        me.ownerCt.onSearch();
    },
    /**停止打印*/
    stopPrintPDF: function () {
        var me = this;
        me.progressInfo.push("<b style='color:red;'>打印中断</b>");
        me.showProgresstInfo();
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
    },
    /**重置信息*/
    resetInfo: function () {
        var me = this;
        //加载次数
        me.loadingCount = 0;
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
    printPDF: function (index, loaded) {
        var me = this;

        me.loadingCount++;
        if (me.loadingCount > me.maxLoadingCount) {
            me.progressInfo.push("正在打印的第  <b>" + (index + 1) + "</b> 份文件无法加载");
            me.stopPrintPDF();
            return;
        }

        var iframe = window.frames["pdfWin_iframe"];

        me.printList[index].url = me.printList[index].url ||
            me.fileMerge(me.printList[index].indexList);

        if (!me.printList[index].url) {
            me.printInfo.push("正在打印的第  <b>" + (index + 1) + "</b> 份文件未获取到文件路径");
            me.stopPrintPDF();
            return;
        }

        if (!me.iframeLoading && loaded && me.printList[index].url) {
            me.iframeLoading = true;
            me.changeFrameContent(me.printList[index].url);
        }

        if (me.iframeLoading && iframe && iframe.document && iframe.document.readyState == "complete") {
            me.iframeLoading = false;
            if (iframe.GetIsNoPdf()) {
                iframe.PrintPdf();
                index++;
                me.loadingCount = 0;
                me.isPrinting(index);

                if (index < me.printList.length) {
                    //选中下一个
                    setTimeout(function () { me.printPDF(index, true) }, me.ftime);
                } else {
                    me.printIsOver();
                }
            }
        } else {
            setTimeout(function () { me.printPDF(index, false) }, me.wtime);
        }
    },
    /**合并文件*/
    fileMerge: function (indexList) {
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

        var value = null;

        me.getToServer(url, function (v) {
            var result = Ext.JSON.decode(v);

            if (result.success) {
                value = result.ResultDataValue;
            } else {
                var errorInfo = "合并PDF文件失败！错误信息：" + result.ErrorInfo;
                errorInfo = '<b style="color:red">' + errorInfo + '</b>';
                me.progressInfo.push(errorInfo);
                me.showProgresstInfo();
            }
        }, false);

        return value;
    },
    /**更改显示内容*/
    changeFrameContent: function (url) {
        var iframe = document.getElementById("pdfWin_iframe");
        iframe.src = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=" + url;
    },
    /**增加打印次数*/
    addPrintTimes: function (ids) {
        var me = this,
			url = me.addPrintTimesUrl + "?reportformidstr=" + ids;

        me.getToServer(url, function (v) {
            Shell.util.Msg.showLog("【PrintList】打印次数累加返回的值=" + v);
            var result = Ext.JSON.decode(v);

            if (!result.success) {
                Shell.util.Msg.showLog("【PrintList】打印次数累加,ids=" + ids + "的打印次数累加错误!");
            } else {
                Shell.util.Msg.showLog("【PrintList】打印次数累加,ids=" + ids + "打印次数累加");
            }
        }, false);
    }
});