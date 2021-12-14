/**PDF延时处理信息*/
var pdfTimeoutInfo = {
    /**标题类型*/
    reportformtitle: Shell.util.Path.getRequestParams().reportformtitle || "",

    /**获取打印信息服务地址*/
    ReportPrintUrl: Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/ReportPrint",
    /**获取打印信息服务地址2*/
    ReportPrintUrl2: Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/GetReportFullMerge",
    /**增加打印次数服务路径*/
    addPrintTimesUrl: Shell.util.Path.rootPath + '/ServiceWCF/PrintService.svc/UpdatePrintTimeByReportFormID',
    /**pdf地址列表*/
    list: [],
    /**打印信息*/
    printInfo: [],

    /**每一个文件容器回收时间,毫秒数*/
    clreaTimes: 30000,
    /**延时处理时间*/
    setTimeoutTimes: 100,
    /**开启打印次数累加功能*/
    openAddPrintTimes: true,
    /**是否显示log信息*/
    openLog: false,
    /**开始打印*/
    printBegin: function () {
        pdfTimeoutInfo.list = $('#pdfWin_grid').datagrid("getChecked") || [];

        if (pdfTimeoutInfo.list.length == 0) {
            pdfTimeoutInfo.showError({
                msg: "<b style='color:red'>请选择需要打印的数据！"
            });
            return;
        }
        pdfTimeoutInfo.printInfo = [];
        pdfTimeoutInfo.printInfo.push("<b>共 " + pdfTimeoutInfo.list.length + " 份文件</b>");
        pdfTimeoutInfo.printInfo.push("<b>准备打印</b>");
        pdfTimeoutInfo.showPrintInfo();

        setTimeout(function () {
            pdfTimeoutInfo.printPDF(0);
        }, pdfTimeoutInfo.setTimeoutTimes);
    },
    /**
     * pdf打印
     * 每一个PDF创建一个隐藏的iframe，避免覆盖
     * @author Jcall
     * @version 2018-01-18
     */
    printPDF: function (index) {
        if (pdfTimeoutInfo.list[index].url) {
            pdfTimeoutInfo.initIframeHtml(index);
        } else {
            pdfTimeoutInfo.getPDFUrl(pdfTimeoutInfo.list[index].ReportFormID, pdfTimeoutInfo.reportformtitle, function (data) {
                if (data.success) {
                    var list = Shell.util.JSON.decode(data.ResultDataValue) || [];
                    pdfTimeoutInfo.list[index].url = (list.length == 0 ? null : list[0]);
                } else {
                    pdfTimeoutInfo.list[index].url = null;
                    pdfTimeoutInfo.list[index].ErrorInfo = data.ErrorInfo;
                }
                if (pdfTimeoutInfo.list[index].url) {
                    pdfTimeoutInfo.initIframeHtml(index);
                } else {
                    var msg = '<a style="color:red;">' + '【' + (index + 1) + '】 ' +
                        Shell.util.Date.toString(pdfTimeoutInfo.list[index].RECEIVEDATE, true) +
                        ' ' + pdfTimeoutInfo.list[index].CNAME + ' ' + pdfTimeoutInfo.list[index].SAMPLENO +
                        '，加载失败</a>';
                    pdfTimeoutInfo.printInfo.push(msg);
                    pdfTimeoutInfo.showPrintInfo();
                    index++;
                    setTimeout(function () {
                        if (index < pdfTimeoutInfo.list.length) {
                            pdfTimeoutInfo.printPDF(index); //打开下一个PDF
                        } else {
                            pdfTimeoutInfo.printIsOver();
                        }
                    }, pdfTimeoutInfo.setTimeoutTimes);
                }
            });
        }

    },
    /**初始化html内容*/
    initIframeHtml: function (index) {
        var me = this;
        var url = pdfTimeoutInfo.list[index].url;
        url = url.replace(/\.\.\//g, "");//排除相对路径符号，添加时间：2018-02-28
        //url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=../" + url;
        url = Shell.util.Path.rootPath + '/' + Shell.util.String.encode(url);
        //pdfTimeoutInfo.printFile(index, "http://localhost/WebLis_2015/ui/BarPrintClass/PrintPDF/201910290001.pdf");
        pdfTimeoutInfo.printFile(index, pdfTimeoutInfo.list[index], url);
    },
    /**显示打印的信息*/
    showPrintInfo: function (config) {
        var win = pdfTimeoutInfo.infoWin,
            content = pdfTimeoutInfo.printInfo.join("<br/>");

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
            setTimeout(function () {
                win.window('open').window("center");
            }, 100);
        } else {
            setTimeout(function () {
                win.window({ content: "<div style='padding:10px;'>" + content + "</div>" });
            }, 100);
        }
    },

    /**正在打印中*/
    isPrinting: function (index, callback) {
        var row = pdfTimeoutInfo.list[index];

        pdfTimeoutInfo.printInfo.push('正在打印第  ' + (index + 1) + ' 份文件...');
        pdfTimeoutInfo.printInfo.push(row.RECEIVEDATE + ' ' + row.CNAME + ' ' + row.SAMPLENO);
        pdfTimeoutInfo.showPrintInfo();

        if (pdfTimeoutInfo.openAddPrintTimes) {
            pdfTimeoutInfo.addPrintTimes(row.ReportFormID, callback); //增加打印次数
        } else {
            callback();
        }
    },
    /**打印完成*/
    printIsOver: function () {
        var me = this;
        pdfTimeoutInfo.printInfo.push('<b>打印完成</b>');
        pdfTimeoutInfo.showPrintInfo();
    },
    /**停止打印*/
    stopPrintPDF: function () {
        if (pdfTimeoutInfo.printInfo.length == 0) return;
        pdfTimeoutInfo.printInfo.push("<b style='color:red;'>已停止打印</b>");
        pdfTimeoutInfo.showPrintInfo();
    },
    /**获取PDF文件路径*/
    getPDFUrl: function (reportformId, reportformtitle, callback) {
        var url = '';
        if (reportformId.indexOf(',') > -1) {
            url = pdfTimeoutInfo.ReportPrintUrl2 + "?ReportFormIDs=" + reportformId + "&Reportformtitle=" + reportformtitle;
        } else {
            url = pdfTimeoutInfo.ReportPrintUrl + "?reportformId=" + reportformId + "&reportformtitle=" + reportformtitle + "&reportformfiletype=JPG&printtype=1";
        }

        var value = null;

        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: url,
            async: false,
            success: function (result) {
                if (callback) {
                    callback(result);
                } else {
                    if (result.success) {
                        var list = Shell.util.JSON.decode(result.ResultDataValue) || [];
                        value = (list.length == 0 ? null : list[0]);
                    }
                }
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取PDF文件路径失败！错误信息：" + strError);
                if (callback) callback({ success: false, ErrorInfo: "获取PDF文件路径失败！错误信息：" + strError });
            }
        });

        if (!callback) return value;
    },
    /**更改显示内容*/
    changeFrameContent: function (url) {
        var iframe = document.getElementById("pdfWin_iframe");
        url = url.replace(/\.\.\//g, "");//排除相对路径符号，添加时间：2018-02-28
        url = encodeURIComponent(url);
        //iframe.src = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=../" + url;
        iframe.src = Shell.util.Path.rootPath + '/' + url;
    },
    /**显示错误信息*/
    showError: function (config) {
        var config = config || {},
            options = $("#pdfWin_grid").datagrid("options"),
            maxWidth = options.width - 20,
            maxHeight = options.height - 20,
            width = config.width || 250,
            height = config.height || 100;

        if (maxWidth < width) width = maxWidth;
        if (maxHeight < height) height = maxHeight;

        $.messager.show({
            title: config.title || "错误消息",
            timeout: config.timeout || 1000,
            width: width,
            height: height,
            msg: config.msg,
            showType: config.showType || 'show',
            style: config.style || { left: '10px', top: '2px' }
        });
    },
    /**在弹出面版中显示log信息*/
    showLog: function (msg) {
        if (!pdfTimeoutInfo.openLog) return;
        msg = '<a style="color:blue;">' + '【' + Shell.util.Date.toString(new Date().getTime(), false, true) + '】 ' + msg + '</a>';
        pdfTimeoutInfo.printInfo.push(msg);
        pdfTimeoutInfo.showPrintInfo();
    },

    /**iframe加载*/
    iframeIsLoaded: function (index, iframeId) {
        var iframe = window.frames[iframeId];

        var pdf = iframe.document.getElementById('pdf');
        var row = pdfTimeoutInfo.list[index];
        if (pdf) {
            pdfTimeoutInfo.showLog('iframeIsLoaded-true:' + row.CNAME + ';' + row.url + ';readyState=' + pdf.readyState);
            if (pdf.readyState == "4") {
                pdfTimeoutInfo.printFile(index, row, iframeId);
                pdfTimeoutInfo.showLog('iframeIsLoaded-afterPrintFile');
                setTimeout(function () {
                    var f = document.getElementById(iframeId);
                    f.parentNode.removeChild(f);
                    pdfTimeoutInfo.showLog('iframeIsLoaded-removeChild：' + iframeId);
                }, pdfTimeoutInfo.clreaTimes);
            } else {
                setTimeout(function () {
                    pdfTimeoutInfo.iframeIsLoaded(index, iframeId);
                }, pdfTimeoutInfo.setTimeoutTimes);
            }
        } else {
            pdfTimeoutInfo.showLog('iframeIsLoaded-false:' + row.CNAME + ';' + row.url);
            setTimeout(function () {
                pdfTimeoutInfo.iframeIsLoaded(index, iframeId);
            }, pdfTimeoutInfo.setTimeoutTimes);
        }
    },
    /**打印文件*/
    printFile: function (index, row, Url) {
        var lodop = Lodop.getLodopObj("打印PDF:" + row.CNAME);
        var printerindex = $("#PrinterList").combobox('getValue');
        if (printerindex) {
            LODOP.SET_PRINTER_INDEX(printerindex);
            var pagesize = $("#pagesizelist").combobox('getValue');
            var orientationType= $("#orientationType").combobox('getValue');
            LODOP.SET_PRINT_PAGESIZE(orientationType, "", "", pagesize);
        }

        LODOP.NEWPAGEA();
        LODOP.ADD_PRINT_PDF(0, 0, "100%", "100%", Url);
        //LODOP.PREVIEW();
        LODOP.PRINT();
        setTimeout(function () {
            pdfTimeoutInfo.isPrinting(index, function () {
                index++;
                if (index < pdfTimeoutInfo.list.length) {
                    pdfTimeoutInfo.printPDF(index); //打开下一个PDF
                } else {
                    pdfTimeoutInfo.printIsOver();
                }
            });
        }, pdfTimeoutInfo.setTimeoutTimes);
    },
    /**增加打印次数*/
    addPrintTimes: function (reportID, callback) {
        var arryID = [],
            length = 1;
        if (reportID.indexOf(",") > -1) {
            arryID = reportID.split(",");
            length = arryID.length;
        } else {
            arryID.push(reportID);
        }
        for (var i = 0; i < length; i++) {
            $.ajax({
                url: Shell.util.Path.rootPath + '/ServiceWCF/PrintService.svc/UpdatePrintTimeByReportFormID',
                data: { ReportFormID: arryID[i] },
                dataType: 'json',
                type: 'GET',
                timeout: 10000,
                async: true,
                contentType: 'application/json',//不加这个会出现错误
                success: function (data) {
                    if (data.success) {
                    }
                },
                error: function (data) {
                    //$.messager.alert('提示信息', data.ErrorInfo, 'error');
                    pdfTimeoutInfo.showLog('打印次数累计错误：reportID=' + reportID + ';错误信息：' + data.ErrorInfo);
                }
            });
        }
        callback();
    },
    GetClodopVersion: function () {
        var LODOP = Lodop.getLodop();
        if (LODOP.VERSION) {
            if (LODOP.CVERSION)
                alert("当前有C-Lodop云打印可用!\n C-Lodop版本:" + LODOP.CVERSION + "(内含Lodop" + LODOP.VERSION + ")");
            else
                alert("本机已成功安装了Lodop控件！\n 版本号:" + LODOP.VERSION);

        };
    },
    ShowPrintSet: function () {
        $("#printsetgroup").show();
        pdfTimeoutInfo.ShowPrinterList();
    },
    ShowPrinterList: function () {
        var LODOP = Lodop.getLodop();
        var iPrinterCount = LODOP.GET_PRINTER_COUNT();
        var printerlist = [];
        for (var i = 0; i < iPrinterCount; i++) {
            var printer = {
                name: LODOP.GET_PRINTER_NAME(i),
                value: i
            };
            printerlist.push(printer);
        };
        $("#PrinterList").combobox({
            valueField: 'value',
            textField: 'name',
            data: printerlist,
            onSelect: function () {
                pdfTimeoutInfo.ShowPrintPageSizeList();
            }
        });
        $("#PrinterList").combobox('select', 0);
    },
    ShowPrintPageSizeList: function () {
        var LODOP = Lodop.getLodop();
        var printerindex = $("#PrinterList").combobox('getValue');
        if (!printerindex) {
            return;
        }
        var strPageSizeList = LODOP.GET_PAGESIZES_LIST(printerindex, "\n");
        var Options = [];
        Options = strPageSizeList.split("\n");
        var PageSizelist = [];
        for (i in Options) {
            var PageSize = {
                name: Options[i],
                value: Options[i]
            };
            PageSizelist.push(PageSize);
        }
        $("#pagesizelist").combobox({
            valueField: 'value',
            textField: 'name',
            data: PageSizelist,
            onLoadSuccess: function () {
                $("#pagesizelist").combobox('select', Options[0]);
            }
        });
    }
};

$(function () {
    var params = Shell.util.Path.getRequestParams(),
        firstUrl = params.firstUrl,
        ids = params.ids,
        merge = params.merge;

    if (!ids) return;
    if (merge == 'CName' || merge == 'PatNo') {
        url = Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/ReportFormIdGroupByCnameOrPatno?reportformIDs=" + ids + "&GroupByCnameOrPatNo=" + merge;
    } else {
        ids = ids.replace(/,/g, "','");
        url = Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/SelectReportList?page=1&rows=1000&wherestr=ReportFormID in ('" + ids + "')";
    }
    $('#pdfWin_grid').datagrid({
        fit: true,
        border: false,
        fitColumns: true,
        rownumbers: true,
        loadMsg: '数据加载中...',
        method: 'get',
        idField: 'ReportFormID',
        sortName: params.sortName,
        sortOrder: params.sortOrder,
        //data:data,
        url: url,
        checkOnSelect: false,
        selectOnCheck: false,
        toolbar: '#report_grid_toolbar',
        columns: [[
            { field: 'ReportFormID', title: '主键', checkbox: true },
            {
                field: 'RECEIVEDATE', title: '核收日期', width: 100, formatter: function (value, index, row) {
                    if (!value) return "";
                    return value.slice(0, 10).replace(/\//g, "_");
                }, tooltip: function (value, index, row) {
                    if (!value) return "";
                    return "<b>" + value.slice(0, 10).replace(/\//g, "_") + "</b>";
                }
            },
            {
                field: 'CNAME', title: '名称', width: 100, tooltip: function (value, index, row) {
                    return "<b>" + value + "</b>";
                }
            },
            { field: 'PATNO', title: '病历号', width: 100 },
            { field: 'SAMPLENO', title: '样本号', width: 100 },
            { field: 'ZDY8', title: '编号', width: 100 },
            { field: 'SECTIONNO', title: '检验小组编号', hidden: true },
            { field: 'CLIENTNO', title: '送检单位编码', hidden: true },
            { field: 'SectionType', title: '小组类型', hidden: true }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                $('#pdfWin_grid').datagrid("clearChecked");
                return Shell.util.JSON.decode(data.ResultDataValue);
            } else {
                pdfTimeoutInfo.showError({
                    timeout: 4000,
                    msg: "<b style='color:red;'>" + data.ErrorInfo + "</b>"
                });
                return { "total": 0, "rows": [] };
            }
        },
        onLoadSuccess: function (data) {
            //默认选中第一行数据
            if (data.total > 0) {
                $('#pdfWin_grid').datagrid("selectRow", 0);
            }
        },
        onSelect: function (rowIndex, rowData) {
            if (rowData.url) {
                pdfTimeoutInfo.changeFrameContent(rowData.url);
            } else {
                //获取PDF文件路径
                pdfTimeoutInfo.getPDFUrl(rowData.ReportFormID, pdfTimeoutInfo.reportformtitle, function (result) {
                    if (result.success) {
                        var list = Shell.util.JSON.decode(result.ResultDataValue) || [];
                        rowData.url = list.length == 0 ? null : list[0];
                        pdfTimeoutInfo.changeFrameContent(rowData.url);
                    } else {
                        pdfTimeoutInfo.showError({
                            timeout: 4000,
                            width: 500,
                            height: 500,
                            msg: "<b style='color:red;'>" + result.ErrorInfo + "</b>"
                        });
                    }
                });
            }
        },
        onClickRow: function (rowIndex, rowData) {
            $('#pdfWin_grid').datagrid("clearSelections");
            $('#pdfWin_grid').datagrid("selectRow", rowIndex);
        }
    });
    $("#report_grid_toolbar-print").bind('click', function () { pdfTimeoutInfo.printBegin(); });
    $("#report_grid_toolbar-GetCLodopVersion").bind('click', function () { pdfTimeoutInfo.GetClodopVersion(); });
    $("#report_grid_toolbar-printset").bind('click', function () { pdfTimeoutInfo.ShowPrintSet(); });
});
/*
[{
            text: '打印',
            iconCls: 'button-print',
            handler: function () {
                //开始打印
                pdfTimeoutInfo.printBegin();
            }
            //		},{
            //			text:'停止',
            //			iconCls:'button-cancel',
            //			handler:function(){pdfTimeoutInfo.stopPrintPDF();}
        }],
*/