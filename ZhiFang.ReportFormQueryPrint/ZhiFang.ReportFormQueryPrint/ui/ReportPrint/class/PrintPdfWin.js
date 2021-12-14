/**
 * 打印窗口
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintPdfWin', {
    extend: 'Shell.ux.panel.Panel',

    title: '打印进度信息',
    width: 600,
    height:400,

    /**A4报告的类型//A4/16开*/
    A4Type: null,
    /**报告列表*/
    reportList: [],
    /**打印文件列表(可能多份报告合并成一个文件)*/
    printList: [],
    /**进度信息*/
    progressInfo: [],

    /**合并的数量*/
    mergePageCount: 10,

    initComponent: function () {
        var me = this;

        me.layout = 'fit';

        me.initHtml();


        

        me.callParent(arguments);
    },
    /**初始化html内容*/
    initHtml: function () {
        this.html =
            '<iframe id="pdfWin_iframe" style="overflow:hidden;overflow-x:hidden;' +
            'overflow-y:hidden;height:100%;width:100%;position:absolute;' +
            'top:0px;left:0px;right:0px;bottom:0px"></iframe>';
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

        me.A4Type = config.A4Type,
        me.reportList = config.reportList;
        me.printList = [];
        me.progressInfo = [];

        //开始准备打印
        if (config.strPageName == "双A5") {
            me.printDA5();
        } else {
            me.printA4A5();
        }
    },
    /**双A5打印*/
    printDA5: function () {
        var me = this,
            reportList = me.reportList,
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

        setTimeout("pdfTimeoutInfo.printPDF(0,true)", 1000);
    },
    /**显示打印的信息*/
    showPrintInfo: function (config) {
        var me = this,
            win = me.progressInfoWin,
			content = me.progressInfo.join("<br/>");

        if (!win) {
            var config = config || {},
				maxWidth = document.body.clientWidth - 20,
				maxHeight = document.body.clientHeight - 20,
				width = config.width || 280,
				height = config.height || 500;
            win = $("#messager").window({
                title: "打印进度信息",
                content: "<div style='padding:10px;'>" + content + "</div>",
                width: (maxWidth > width ? width : maxWidth),
                height: (maxHeight > height ? height : maxHeight),
                inline: true,//显示在父容器中
                minimizable: false,//不可最小化
                maximizable: false,//不可最大化
                collapsible: false,//不可折叠
                draggable: false,//不可拖拽
                resizable: false,//不可改变大小
                modal: true//模态
            });
            win.window('open').window("center");
        } else {
            win.window({ content: "<div style='padding:10px;'>" + content + "</div>" });
        }
    },

});