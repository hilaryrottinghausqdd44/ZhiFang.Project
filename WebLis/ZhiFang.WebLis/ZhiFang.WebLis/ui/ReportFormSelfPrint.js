var pdfprint = {
    /**标题类型*/
    reportformtitle: "1" || "",

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
    /**计时数*/
    tnum: -1,
    /**计时器*/
    timer1: null,
    /**每一个文件容器回收时间,毫秒数*/
    clreaTimes: 30000,
    /**延时处理时间*/
    setTimeoutTimes: 100,
    /**开启打印次数累加功能*/
    openAddPrintTimes: false,
    /**当前存在的iframeId*/
    iframeIds: [],
    /**是否显示log信息*/
    openLog: true,

    /**
	 * pdf打印
	 * 每一个PDF创建一个隐藏的iframe，避免覆盖
	 * @author Jcall
	 * @version 2018-01-18
	 */
    printPDF: function (index) {
        if (pdfprint.list[index].url) {
            pdfprint.initIframeHtml(index);
        } else {
            pdfprint.getPDFUrl(pdfprint.list[index].ReportFormID, pdfprint.reportformtitle, function (data) {
                if (data.success) {
                    var list = Shell.util.JSON.decode(data.ResultDataValue) || [];
                    pdfprint.list[index].url = (list.length == 0 ? null : list[0]);
                } else {
                    pdfprint.list[index].url = null;
                    pdfprint.list[index].ErrorInfo = data.ErrorInfo;
                    pdfprint.tnum = 5;
                    pdfprint.timer1 = setInterval("Timer()", 1000);
                }
                if (pdfprint.list[index].url) {
                    pdfprint.initIframeHtml(index);
                } else {
                    var msg = '<a style="color:red;">' + '【' + (index + 1) + '】 ' +
						Shell.util.Date.toString(pdfprint.list[index].RECEIVEDATE, true) +
						' ' + pdfprint.list[index].CNAME + ' ' + pdfprint.list[index].SAMPLENO +
						'，加载失败</a>';
                    pdfprint.printInfo.push(msg);
                    pdfprint.showPrintInfo();
                    index++;
                    setTimeout(function () {
                        if (index < pdfprint.list.length) {
                            pdfprint.printPDF(index); //打开下一个PDF
                        } else {
                            pdfprint.printIsOver();
                        }
                    }, pdfprint.setTimeoutTimes);
                }
            });
        }

    },
    /**显示打印的信息*/
    showPrintInfo: function (config) {
        var content = pdfprint.printInfo.join("<br/>");
        setTimeout(function () {
            $('#ReportRormPrintResult').html("<div style='padding:10px;'>" + content + "</div>" );
        }, 100);
    },
    /**开始打印*/
    printBegin: function (reportformlist) {
        var list = pdfprint.iframeIds,
			len = list.length;
        //清空iframe元素
        for (var i = 0; i < len; i++) {
            var f = document.getElementById(list[i]);
            if (f) {
                f.parentNode.removeChild(f);
            }
        }
        pdfprint.iframeIds = [];

        pdfprint.list = reportformlist || [];

        if (pdfprint.list.length == 0) {
            pdfprint.showError({
                msg: "<b style='color:red'>没有需要打印的报告单！</b>"
            });
            return;
        }
        pdfprint.printInfo = [];
        pdfprint.printInfo.push("<b>共 " + pdfprint.list.length + " 份文件</b>");
        pdfprint.printInfo.push("<b>准备打印</b>");
        pdfprint.showPrintInfo();

        setTimeout(function () {
            pdfprint.printPDF(0);
        }, pdfprint.setTimeoutTimes);
    },
    /**正在打印中*/
    isPrinting: function (index, callback) {
        var row = pdfprint.list[index];

        pdfprint.printInfo.push('正在打印第  ' + (index + 1) + ' 份文件...');
        pdfprint.printInfo.push(row.RECEIVEDATE + ' ' + row.CNAME + ' ' + row.SAMPLENO);
        pdfprint.showPrintInfo();

        if (pdfprint.openAddPrintTimes) {
            pdfprint.addPrintTimes(row.ReportFormID, callback); //增加打印次数
        } else {
            callback();
        }
    },
    /**打印完成*/
    printIsOver: function () {
        var me = this;
        pdfprint.printInfo.push('<b>打印完成</b>');
        pdfprint.showPrintInfo();
        pdfprint.endPrintDPF();
        pdfprint.tnum = 5;
        pdfprint.timer1 = setInterval("Timer()", 1000);
    },
    /**停止打印*/
    stopPrintPDF: function () {
        if (pdfprint.printInfo.length == 0) return;
        pdfprint.printInfo.push("<b style='color:red;'>已停止打印</b>");
        pdfprint.showPrintInfo();
        pdfprint.endPrintDPF();
        pdfprint.tnum = 5;
        pdfprint.timer1 = setInterval("Timer()", 1000);
    },
    /**打印结束*/
    endPrintDPF: function () {
        var url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=";
        var iframeId = 'PDFWIN_IFRAME_END';
        pdfprint.iframeIds.push(iframeId);

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

        if (pdfprint.clreaTimes) {
            setTimeout(function () {
                var f = document.getElementById('PDFWIN_IFRAME_END');
                f.parentNode.removeChild(f);
               // pdfprint.showLog('iframeIsLoaded-removeChild：PDFWIN_IFRAME_END');
            }, pdfprint.clreaTimes);
        }
    },
    /**获取PDF文件路径*/
    getPDFUrl: function (reportformId, reportformtitle, callback) {
        var url = '';
        if (reportformId.indexOf(',') > -1) {
            url = pdfprint.ReportPrintUrl2 + "?ReportFormIDs=" + reportformId + "&Reportformtitle=" + reportformtitle;
        } else {
            url = pdfprint.ReportPrintUrl + "?reportformId=" + reportformId + "&reportformtitle=" + reportformtitle + "&reportformfiletype=JPG&printtype=1";
        }

        var value = null;

        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: url+"&" + new Date().getTime(),
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
        iframe.src = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=../" + url;
    },
    /**显示错误信息*/
    showError: function (config) {
        msg = config.msg;
        pdfprint.printInfo.push(msg);
        pdfprint.showPrintInfo();
    },
    /**在弹出面版中显示log信息*/
    showLog: function (msg) {
        if (!pdfprint.openLog) return;
        msg = '<a style="color:blue;">' + '【' + Shell.util.Date.toString(new Date().getTime(), false, true) + '】 ' + msg + '</a>';
        pdfprint.printInfo.push(msg);
        pdfprint.showPrintInfo();
    },
    /**初始化html内容*/
    initIframeHtml: function (index) {
        var me = this;
        var url = pdfprint.list[index].url;
        url = url.replace(/\.\.\//g, "");//排除相对路径符号，添加时间：2018-02-28
        url = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=../" + url;

        var fileName = url.split('/').slice(-1)[0].split('.')[0].replace(/-/g, '_').replace(/;/g, '_');
        var iframeId = 'PDFWIN_IFRAME_' + fileName;
        pdfprint.iframeIds.push(iframeId);

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
                pdfprint.iframeIsLoaded(index, iframeId);
            });
        } else {
            iframe.onload = function () {
                pdfprint.iframeIsLoaded(index, iframeId);
            };
        }
        //pdfprint.showLog('initIframeHtml-appendChild：' + iframeId);
        $("#iframe-div").append(iframe);
    },
    /**iframe加载*/
    iframeIsLoaded: function (index, iframeId) {
        var iframe = window.frames[iframeId];

        var pdf = iframe.document.getElementById('pdf');
        var row = pdfprint.list[index];
        if (pdf) {
           // pdfprint.showLog('iframeIsLoaded-true:' + row.CNAME + ';' + row.url + ';readyState=' + pdf.readyState);
            if (pdf.readyState == "4") {
                pdfprint.printFile(index, iframeId);
               // pdfprint.showLog('iframeIsLoaded-afterPrintFile');
                setTimeout(function () {
                    var f = document.getElementById(iframeId);
                    f.parentNode.removeChild(f);
                    //pdfprint.showLog('iframeIsLoaded-removeChild：' + iframeId);
                }, pdfprint.clreaTimes);
            } else {
                setTimeout(function () {
                    pdfprint.iframeIsLoaded(index, iframeId);
                }, pdfprint.setTimeoutTimes);
            }
        } else {
           // pdfprint.showLog('iframeIsLoaded-false:' + row.CNAME + ';' + row.url);
            setTimeout(function () {
                pdfprint.iframeIsLoaded(index, iframeId);
            }, pdfprint.setTimeoutTimes);
        }
    },
    /**打印文件*/
    printFile: function (index, iframeId) {
        var iframe = window.frames[iframeId];

        //pdfprint.showLog('printFile');

        iframe.PrintPdf();

        setTimeout(function () {
            pdfprint.isPrinting(index, function () {
                index++;
                if (index < pdfprint.list.length) {
                    pdfprint.printPDF(index); //打开下一个PDF
                } else {
                    pdfprint.printIsOver();
                }
            });
        }, pdfprint.setTimeoutTimes);
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
                url: Shell.util.Path.rootPath + '/ServiceWCF/PrintService.svc/UpdatePrintTimeByReportFormID' ,
                data: { ReportFormID: arryID[i] ,date:new Date().getTime()},
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
                    pdfprint.showLog('打印次数累计错误：reportID=' + reportID + ';错误信息：' + data.ErrorInfo);
                }
            });
        }
        callback();
    }
};
$(function () {
    $('#BarcodeTxt').textbox({
        prompt: '预置条码(必填)',
        required: true
    });
    $.extend($.fn.validatebox.defaults.rules, {
        minLength: {
            validator: function (value, param) {
                return value.length >= param[0];
            },
            message: 'Please enter at least {0} characters.'
        }
    });
    $('#Searchbtn').click(function () {
        if (pdfprint.tnum >= 0)
            return;
        Search();
    });
});
function Search() {

    if ($('#BarcodeTxt').val() != "" && $('#ClienNo').val() != "") {
        $('#BarcodeTxt').textbox({ editable: false });
        $('#Searchbtn').attr("readOnly", "true");
        var date = new Date()       
        var startday = Shell.util.Date.toString(Shell.util.Date.getNextDate(date, -90), true);
        var endday = Shell.util.Date.toString(date, true);
        //alert(startday);
        //alert(endday);
        //var startday = "2017-01-01";
        //var endday = "2017-01-03";
        $.ajax({
            ///ReportFromService.svc/SelectReportList2?Startdate=2017-01-01&Enddate=2017-01-03&CLIENTNO=20280676&statues=0&serialno=5273187861488982922&page=1&rows=100
            url: "ServiceWCF/ReportFromService.svc/SelectReportList2?Startdate=" + startday + "&Enddate=" + endday + "&CLIENTNO=" + $('#ClienNo').val() + "&statues=0&serialno=" + $('#BarcodeTxt').val() + "&page=1&rows=100&" + date.getTime(),
            async: false,
            success: function (data) {
                if (data.success) {
                    var jsona = $.parseJSON(data.ResultDataValue);
                    if (jsona.total && jsona.total > 0) {
                        pdfprint.tnum = 100;
                        ReportFormPrint(jsona);
                    }
                    else {
                        pdfprint.printInfo.push("未查找到报告单");
                        pdfprint.showPrintInfo();
                        pdfprint.tnum = 5;
                        pdfprint.timer1 = setInterval("Timer()", 1000);
                    }
                }
                else {
                    pdfprint.printInfo.push("未查找到报告单");
                    pdfprint.showPrintInfo();
                    pdfprint.tnum = 5;
                    pdfprint.timer1 = setInterval("Timer(10)", 1000);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //通常情况下textStatus和errorThrown只有其中一个包含信息
                //this;   //调用本次ajax请求时传递的options参数
                pdfprint.printInfo.push("未查找到报告单");
                pdfprint.showPrintInfo();
                pdfprint.tnum = 5;
                pdfprint.timer1 = setInterval("Timer(10)", 1000);
            }
        });
    }
    else {
        $('#BarcodeTxt').textbox('textbox').focus();
        return false;
    }
}
function ReportFormPrint(reportformlist) {
    //您有两份报告单。<br>正在打印第一份...<br>正在打印第二份...<br>打印完成。
    pdfprint.printBegin(reportformlist.rows);
}

function Timer() {
    pdfprint.tnum--;
    if (pdfprint.tnum >= 0) {
        $('#Searchbtn').val("查   询" + "(" + pdfprint.tnum + ")");
    }
    else {
        $('#BarcodeTxt').textbox("clear");
        $('#BarcodeTxt').textbox({ editable: true });
        $('#Searchbtn').val("查   询");
        $('#Searchbtn').attr("readOnly", "false");
        clearInterval(pdfprint.timer1);
        pdfprint.printInfo=[];
        pdfprint.showPrintInfo();
    }
}