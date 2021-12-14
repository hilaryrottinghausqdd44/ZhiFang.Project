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
    /**当前存在的iframeId*/
    iframeIds: [],
    /**是否显示log信息*/
    openLog: false,

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
            setTimeout(function(){
            	win.window('open').window("center");
            },100);
        } else {
        	setTimeout(function(){
            	win.window({ content: "<div style='padding:10px;'>" + content + "</div>" });
            },100);
        }
    },
    /**开始打印*/
    printBegin: function () {
        var list = pdfTimeoutInfo.iframeIds,
			len = list.length;
        //清空iframe元素
        for (var i = 0; i < len; i++) {
            var f = document.getElementById(list[i]);
            if (f) {
                f.parentNode.removeChild(f);
            }
        }
        pdfTimeoutInfo.iframeIds = [];

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
        pdfTimeoutInfo.endPrintDPF();
    },
    /**停止打印*/
    stopPrintPDF: function () {
        if (pdfTimeoutInfo.printInfo.length == 0) return;
        pdfTimeoutInfo.printInfo.push("<b style='color:red;'>已停止打印</b>");
        pdfTimeoutInfo.showPrintInfo();
        pdfTimeoutInfo.endPrintDPF();
    },
    /**打印结束*/
    endPrintDPF: function () {
        var url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=";
        var iframeId = 'PDFWIN_IFRAME_END';
        pdfTimeoutInfo.iframeIds.push(iframeId);

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

        $("#iframe-div").append(iframe);

        if (pdfTimeoutInfo.clreaTimes) {
            setTimeout(function () {
                var f = document.getElementById('PDFWIN_IFRAME_END');
                f.parentNode.removeChild(f);
                pdfTimeoutInfo.showLog('iframeIsLoaded-removeChild：PDFWIN_IFRAME_END');
            }, pdfTimeoutInfo.clreaTimes);
        }
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
        url = url.replace(/\.\.\//g,"");//排除相对路径符号，添加时间：2018-02-28
        url = encodeURIComponent(url);
        iframe.src = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=../" + url;
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
    /**初始化html内容*/
    initIframeHtml: function (index) {
        var me = this;
        var url = pdfTimeoutInfo.list[index].url;
        url = url.replace(/\.\.\//g,"");//排除相对路径符号，添加时间：2018-02-28
        url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=../" + url;

        var fileName = url.split('/').slice(-1)[0].split('.')[0].replace(/-/g, '_').replace(/;/g, '_');
        var iframeId = 'PDFWIN_IFRAME_' + fileName;
        pdfTimeoutInfo.iframeIds.push(iframeId);

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

        if (iframe.attachEvent) {
            iframe.attachEvent("onload", function () {
                pdfTimeoutInfo.iframeIsLoaded(index, iframeId);
            });
        } else {
            iframe.onload = function () {
                pdfTimeoutInfo.iframeIsLoaded(index, iframeId);
            };
        }
        pdfTimeoutInfo.showLog('initIframeHtml-appendChild：' + iframeId);
        $("#iframe-div").append(iframe);
    },
    /**iframe加载*/
    iframeIsLoaded: function (index, iframeId) {
        var iframe = window.frames[iframeId];

        var pdf = iframe.document.getElementById('pdf');
        var row = pdfTimeoutInfo.list[index];
        if (pdf) {
            pdfTimeoutInfo.showLog('iframeIsLoaded-true:' + row.CNAME + ';' + row.url + ';readyState=' + pdf.readyState);
            if (pdf.readyState == "4") {
                pdfTimeoutInfo.printFile(index, iframeId);
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
    printFile: function (index, iframeId) {
        var iframe = window.frames[iframeId];

        pdfTimeoutInfo.showLog('printFile');

        iframe.PrintPdf();

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
        toolbar: [{
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
});