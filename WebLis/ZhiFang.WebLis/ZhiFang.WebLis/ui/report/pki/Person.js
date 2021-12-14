$(function () {
    /**错误信息*/
    var errorInfo = [],
    /**结果类型*/
        reportType = "",
    /**结果信息条件*/
        reportParams = null,
    /**结果信息*/
        reportInfo = {},
    /**结果图窗口*/
        imageWin = null,
    /**开启结果图显示功能*/
        showReportImg = true,
    //送检单位简码
        clientShortCodeAry = [],
    /**文件类型*/
        fileType = (Shell.util.Cookie.getCookie("ReportFormFileType") || "").toLocaleUpperCase(),
    /**服务地址*/
        serverUrl = {
            /**获取报告列表服务地址*/
            SelectReportListUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/SelectReportListByPerson_Barcode_Name",
            /**获取报告服务地址*/
            GetPreviewReportByIdUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportById",
            /**获取结果服务地址*/
            GetPreviewReportResultByIdUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportResultById",
            /**获取打印信息服务地址*/
            ReportPrintUrl: Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/ReportPrint",
            /**获取结果图*/
            GETImagesUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportImageById",
            /**获取导出Excel路径*/
            GETDownloadReportExcelUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/DownloadReportExcelByPara"
        };

    //$.ajax()请求服务 gwh add 2015-3-4
    function askService(serviceType, entity, where, rowIndex) {
        var serviceParam = {}, //请求服务参数
            async = serviceType == 'delete' ? false : true; //删除操作，同步执行$.ajax方法

        if (serviceType == 'load') {
            serviceParam.data = entity;
        }
        serviceParam = setService(serviceType, serviceParam, where); //配置服务参数
        $.ajax({
            url: encodeURI(Shell.util.Path.rootPath + '/ServiceWCF/' + serviceParam.serviceName),
            data: serviceParam.data,
            dataType: 'json',
            type: serviceParam.type,
            timeout: 100000,
            async: async,
            contentType: 'application/json', //不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var result = eval('(' + data.ResultDataValue + ')');
                    switch (serviceType) {
                        case 'delete': $('#report_grid').datagrid('deleteRow', rowIndex);
                            break;
                        case 'load': $('#report_grid').datagrid('loadData', data);
                            break;
                        case 'downLoad': $('#downLoadReportForm').attr('href', Shell.util.Path.rootPath + '/' + result);
                            var r = $('#clickContent').click();
                            onSearch();
                            break;
                    }
                } else {
                    if (serviceType == 'downLoad') {
                        $.messager.alert('提示信息', "获取报告失败！", 'error');
                    }
                }
            },
            error: function (data) {
                $.messager.alert('提示信息', data.ErrorInfo, 'error');
            }
        });

    }

    //配置服务参数 gwh add 2015-3-4
    function setService(serviceType, serviceParam, where) {

        //数据请求方式（GET,POST）
        switch (serviceType) {
            case 'delete':
                serviceParam.serviceName = 'ReportFromService.svc/DeleteReportForm';
                serviceParam.type = 'GET';
                serviceParam.data = where;
                break;
            case 'print':
                serviceParam.serviceName = 'PrintService.svc/UpdatePrintTimeByReportFormID';
                serviceParam.type = 'GET';
                serviceParam.data = where;
                break;
            case 'load':
                serviceParam.serviceName = 'ReportFromService.svc/SelectReportList2'; //SelectReportList2
                serviceParam.type = 'POST';
                break;
            case 'downLoad':
                serviceParam.serviceName = 'PrintService.svc/DownLoadReport';
                serviceParam.type = 'POST';
                serviceParam.data = where;
                break;
        }

        return serviceParam;
    }

    /**查询*/
    function onSearch() {
        //alert("查询");
        var where = getWhere();
        if (!where) {
            $.messager.alert("错误提示", "必须输入开始日期,且查询时间不能超过90天!", "error");
            return;
        }
    }
    /**分组按钮处理*/
    function onButtonClick(type) {
        switch (type) {
            case "today": disabledCOLLECTDATE(true); onSearch(); break;
            case "3days": disabledCOLLECTDATE(true); onSearch(); break;
            case "7days": disabledCOLLECTDATE(true); onSearch(); break;
            case "all": disabledCOLLECTDATE(false); break;
        }
    }
    /**禁用采样日期框*/
    function disabledCOLLECTDATE(bo) {
        var action = bo ? "disable" : "enable";
        $("#search-text-COLLECTDATE-S").datebox(action);
        $("#search-text-COLLECTDATE-E").datebox(action);
    }

    /**获取查询条件*/
    function getWhere() {
        var Barcode = getCookie('BarcodeTxt'),
            Name = getCookie('NameTxt'),
            param = {};

        param.Barcode = Barcode;
        param.Name = Name;

        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: serverUrl.SelectReportListUrl + "?Barcode=" + Barcode + "&Name=" + Name + "&guid=" + generateMixed(10),
            data: null,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.success == true) {
                    var jsona = $.parseJSON(data.ResultDataValue);
                    if (jsona.total > 0) {
                        if ($('#report_grid').datagrid("options").url) {
                            $('#report_grid').datagrid('load', null);
                        }
                        else {
                            $('#report_grid').datagrid("options").url = serverUrl.SelectReportListUrl + "?Barcode=" + Barcode + "&Name=" + Name + "&guid=" + generateMixed(10);
                        }
                    }
                    else {
                        var tmpname = Name.replace(/\\/g, "%");
                        $.messager.show({
                            title: '提示消息',
                            msg: '未查找到预制条码号：“' + Barcode + '”，姓名：“' + unescape(tmpname) + '”的报告单。',
                            showType: 'slide',
                            width: '500',
                            height: '150',
                            timeout:1500,
                            style: {
                                right: '',
                                top: document.body.scrollTop + document.documentElement.scrollTop,
                                bottom: ''
                            }
                        });
                    }
                } else {
                    $.messager.alert('提示', '查询信息异常:' + data.msg);
                }
            }
        });
        return true;
    }

    /**报告列表点击事件处理*/
    function onReportGridSelect(rowIndex, rowData) {
        Shell.util.Msg.showLog(rowData.CNAME);

        changeContentInfo({
            reportformId: rowData.ReportFormID,
            sectionNo: rowData.SECTIONNO,
            sectionType: rowData.SectionType
        });
    }
    /**报告/结果内容更改*/
    function changeContentInfo(params) {
        //报告编号、小组编号、小组类别
        var reportformId = params.reportformId,
            sectionNo = params.sectionNo,
            sectionType = params.sectionType,
            error = [];

        if (!reportformId) { error.push("<b style='color:red;'>报告编号reportformId<b>"); }
        if (!sectionNo) { error.push("<b style='color:red;'>小组编号sectionNo<b>"); }
        if (!sectionType) { error.push("<b style='color:red;'>小组类别sectionType<b>"); }

        if (error.length > 0) {
            var text = error.join(" ") + "<b style='color:black;'> 缺失,请传递这些参数!<b>"
            Shell.easyuiUtil.Msg.showError(text);
            return;
        }
        //延时处理
        Shell.util.Action.delay(function () {
            reportInfo = {}; //清空结果
            reportParams = params; //条件重新赋值
            loadReportInfo(params);
        });
    }
    /**加载报告结果数据*/
    function loadReportInfo(params) {
        //加载结果数据
        GetClientNoData(params, function (result, type) {
            var content = document.getElementById("content"),
                html = "";
            if (result.success) {
                html = Shell.util.JSON.decode(result.ResultDataValue) || "";
            } else {
                html = result.ErrorInfo;
            }
            reportInfo["L" + type] = html;
            content.innerHTML = html;
        });
        //加载结果图
        if (showReportImg) {
            GetImageList(params.reportformId, function (result) {
                if (result.success) {
                    var list = Shell.util.JSON.decode(result.ResultDataValue) || [];
                    showImageWin(list);
                } else {
                    showImageWin([]);
                }
            });
        }
    }
    /**显示结果图窗口*/
    function showImageWin(list) {
        var len = list.length,
            html = [];

        if (!imageWin) { initImageWin(); }

        if (len == 0) {
            imageWin.window("minimize");
            return;
        }

        for (var i = 0; i < len; i++) {
            html.push("<img width='100%' height='100%' src='" + Shell.util.Path.rootPath + "/" + list[i].replace(/\\/g, "\/") + "'></img>");
        }
        imageWin.window({ content: html.join("") });
        imageWin.window("open");

    }
    /**初始化结果图窗口*/
    function initImageWin() {
        imageWin = $("#imageWin").window({
            title: "报告结果图",
            width: 400,
            height: 400,
            minimizable: false, //不可最小化
            collapsible: false, //不可折叠
            onBeforeClose: function () {
                $("#imageWin").window("minimize");
                return false;
            }
        }).window("minimize");
    }

    /**结果类型变化处理*/
    function onReportTypeChange() {
        var type = getRadioValueByName("reportType");
        if (reportInfo["L" + type]) {
            var content = document.getElementById("content");
            content.innerHTML = reportInfo["L" + type];
        } else {
            if (reportParams.reportformId && reportParams.sectionNo && reportParams.sectionType) {
                loadReportInfo(reportParams);
            }
        }
    }

    /**清空结果信息*/
    function clearContent() {
        reportParams = null;
        reportInfo = {};
        var content = document.getElementById("content");
        content.innerHTML = "";
    }

    /**打印报告*/
    function printReport(preview) {
        var list = $('#report_grid').datagrid("getChecked") || [],
            len = list.length,
            printConfigInfo = getPrintConfigInfo(),
            reportformtitle = printConfigInfo.titleType,
            urls = [],
            ReportFormIDArr = [],
            error = [];

        if (len == 0) {
            $.messager.alert("提示信息", "请勾选需要打印的数据!", "error");
            return;
        }
        winPdfPrint();
    }

    /**Lodop页面打印*/
    function winLodopPrint(list, ReportFormIDArr, preview) {
        var lodop = Shell.util.Print.getLodopObj("报告单打印"),
            printConfigInfo = getPrintConfigInfo(),
            intOrient = parseInt(printConfigInfo.orientationType),
            strPageName = printConfigInfo.paperType;

        lodop.SET_PRINT_PAGESIZE(intOrient, 0, 0, strPageName); //方向 1:纵;2:横

        for (var i = 0; i < list.length; i++) {
            lodop.NEWPAGE();
            //lodop.ADD_PRINT_IMAGE(0,0,"100%","100%","<img border='0' src='" + Shell.util.Path.rootPath + "/" + list[i] + "'width='100%'/>");
            lodop.ADD_PRINT_IMAGE(0, 0, "100%", "100%", "URL:" + Shell.util.Path.rootPath + "/" + list[i]);
            lodop.SET_PRINT_STYLEA(0, "Stretch", 2); //按原图比例(不变形)缩放模式
        }

        //预览打印/直接打印
        if (preview) {
            if (lodop.PREVIEWB() > 0) {//返回来的数值，表示打印的次数
                //                for (var i = 0; i < ReportFormIDArr.length; i++)
                //                //记录打印次数
                //                    askService('print', null, { ReportFormID: ReportFormIDArr[i] });
            }
        } else {
            var print = lodop.PRINT(); //返回true,表示打印成功
            if (print) {
                //                for (var i = 0; i < ReportFormIDArr.length; i++)
                //                //记录打印次数
                //                    askService('print', null, { ReportFormID: ReportFormIDArr[i] });
            }
        }
        onSearch();
    }

    /**pdf页面打印*/
    function winPdfPrint() {
        var printConfigInfo = getPrintConfigInfo(),
            reportformtitle = printConfigInfo.titleType;

        var url = Shell.util.Path.uiPath + "/report/pki/printPDF_Person.html";

        url += "?reportformtitle=" + reportformtitle;
        OpenWindowFuc("PDF打印", "96%", "96%", url);
    }

    /**获取打印类型设置*/
    function getPrintConfigInfo() {
        var 
            paperType = $("#paperType").combobox("getValue"),
            orientationType = $("#orientationType").combobox("getValue");

        return {
            titleType: 'CENTER',
            paperType: paperType,
            orientationType: orientationType
        };
    }

    /**获取单选项的值*/
    function getRadioValueByName(name) {
        if (!name) return null;
        var list = document.getElementsByName(name) || [],
            len = list.length;
        for (var i = 0; i < len; i++) {
            if (list[i].checked) {
                return list[i].value;
            }
        }
        return null;
    }

    /**获取报告结果*/
    function GetClientNoData(params, callback) {
        var type = getRadioValueByName("reportType"),
            url = "";

        switch (type) {
            case "1": url = serverUrl.GetPreviewReportByIdUrl; break;
            case "2": url = serverUrl.GetPreviewReportResultByIdUrl; break;
        }

        if (url) url += "?reportformId=" + params.reportformId + "&sectionNo=" + params.sectionNo + "&sectionType=" + params.sectionType;
        url = encodeURI(url);

        $.ajax({
            dataType: 'json',
            async: false, //同步请求
            contentType: 'application/json',
            url: url,
            success: function (result) {
                callback(result, type);
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取报告结果失败！错误信息：" + strError);
                callback({ success: false, ErrorInfo: "获取报告结果失败！错误信息：" + strError }, type);
            }
        });
    }
    /**获取打印信息列表*/
    function GetReportPrintList(reportformId, reportformtitle, callback) {
        var url = serverUrl.ReportPrintUrl + "?reportformId=" + reportformId + "&reportformtitle=" + reportformtitle + "&reportformfiletype=JPG&printtype=1";
        url = encodeURI(url);
        $.ajax({
            dataType: 'json',
            async: false, //同步请求
            contentType: 'application/json',
            url: url,
            success: function (result) {
                callback(reportformId, result);
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取打印信息列表失败！错误信息：" + strError);
                callback({ success: false, ErrorInfo: "获取打印信息列表失败！错误信息：" + strError });
            }
        });
    }
    /**获取结果图*/
    function GetImageList(reportformId, callback) {
        var url = serverUrl.GETImagesUrl + "?reportformId=" + reportformId;
        url = encodeURI(url);
        $.ajax({
            dataType: 'json',
            async: false, //同步请求
            contentType: 'application/json',
            url: url,
            success: function (result) {
                callback(result);
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取结果图失败！错误信息：" + strError);
                callback({ success: false, ErrorInfo: "获取结果图失败！错误信息：" + strError });
            }
        });
    }

    function logout()
    {
        location.href = "../../../PersonSearch_AiPuYi.aspx";
    }
    var date = new Date(),
        date_s = Shell.util.Date.toString(date, true),
        date_e = Shell.util.Date.toString(Shell.util.Date.getNextDate(date), true);

    //列表-刷新
    $("#report_grid_toolbar-refresh").bind('click', function () { onSearch(); });
    //列表-直接打印
    $("#report_grid_toolbar-print").bind('click', function () { printReport(); });
    //列表-预览打印
    $("#report_grid_toolbar-preview").bind('click', function () { printReport(true); });
    //列表-批量下载
    $("#report_grid_toolbar-downLoadMost").bind('click', function () { downLoadMost(); });
    //列表-退出
    $("#report_grid_toolbar-logout").bind('click', function () { logout(); });
    //项目类型列表
    $('#report_grid').datagrid({
        fit: true,
        border: false,
        fitColumns: true,
        rownumbers: true,
        loadMsg: '数据加载中...',
        method: 'GET',
        idField: 'ReportFormID',
        pagination: true,
        pageSize: 10,
        pageList: [10, 20, 50, 100, 200, 500],
        checkOnSelect: false,
        selectOnCheck: false,
        toolbar: '#report_grid_toolbar',
        //multiSort:true,
        remoteSort: false,
        columns: [[
            { field: 'ReportFormID', title: '主键', checkbox: true },
            {
                field: 'noperdate', title: '接收时间', width: 100, sortable: true, formatter: function (value, index, row) {
                    if (!value) return "";
                    return value.slice(0, 10).replace(/\//g, "-");
                }, tooltip: function (value, index, row) {
                    if (!value) return "";
                    return "<b>" + value.slice(0, 10).replace(/\//g, "-") + "</b>";
                }
            },
            {
                field: 'CNAME', title: '名称', width: 70, sortable: true, tooltip: function (value, index, row) {
                    return "<b>" + value + "</b>";
                }
            },
            { field: 'SERIALNO', title: '样本预制条码', sortable: true, width: 150 },
            { field: 'SAMPLENO', title: '样本号', sortable: true, width: 100 },
            { field: 'clientename', title: '送检项目', sortable: true, width: 100 },
            {
                field: 'AGE', title: '年龄', width: 50, formatter: function (value, row, index) {
                    return value + row.AGEUNITNAME;
                }
            },
            { field: 'GENDERNAME', title: '性别', width: 50 },
            { field: 'DEPTNAME', title: '科室', width: 100 },
            { field: 'ZDY10', title: '纸张类型', width: 100 },
            {
                field: 'printStatus', title: '打印状态', width: 100, formatter: function (value, row, index) {
                    if (row.PRINTTIMES == 0) {
                        return row.printStatus = "未打印"
                    } else if (row.PRINTTIMES >= 1) {
                        return row.printStatus = "已打印"
                    }
                }
            },

            {
                field: 'PRINTDATETIME', title: '打印时间', sortable: true, width: 100,
                formatter: function (value, row, index) {
                    if (value.indexOf('0001/01/01') > -1) {
                        return null;
                    }
                    return value;
                }
            },
            { field: 'PRINTTIMES', title: '打印次数', width: 100 },
            { field: 'SECTIONNO', title: '检验小组编号', hidden: true },
            { field: 'CLIENTNO', title: '送检单位编码', hidden: true },
            { field: 'SectionType', title: '小组类型', hidden: true },
            {
                field: 'Isdown', title: '下载', sortable: true, width: 100,
                formatter: function (value, row, index) {
                    var result = value == 1 ? '已下载' : '未下载';
                    return result;
                }
            },
            {
                field: 'opt', title: '操作', align: 'right', width: 100, align: 'center',
                formatter: function (value, row, index) {
                    var downLoad = '<a href="javascript:void(0)"  data-options="iconCls:icon-edit" style="margin-right: 10px" onclick=" $.DownLoad(' + index + ')">下载</a>';
                    return downLoad;
                }
            }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                $('#report_grid').datagrid("clearChecked");
                return Shell.util.JSON.decode(data.ResultDataValue);
            } else {
                if (data.ErrorInfo)
                    Shell.easyuiUtil.Msg.show({
                        title: "错误信息",
                        msg: "<b style='color:red;'>" + data.ErrorInfo + "</b>"
                    });
                return { "total": 0, "rows": [] };
            }
        },
        onBeforeLoad: function (params) {
            if (params.page == 0) return false;
        },
        onLoadSuccess: function (data) {
            //默认选中第一行数据
            if (data.total == 0) {
                //  clearContent();
            } else {
                $('#report_grid').datagrid("selectRow", 0);
            }
        },
        onSelect: function (rowIndex, rowData) {
            onReportGridSelect(rowIndex, rowData);
        },
        onClickRow: function (rowIndex, rowData) {
            $('#report_grid').datagrid("clearSelections");
            $('#report_grid').datagrid("selectRow", rowIndex);
        },
        onSortColumn: function (sort, order) {
            $('#report_grid').datagrid('clearSelections');
            onSearch();
        },
        onDblClickRow: function (rowIndex, rowData) {
            $('#report_grid').datagrid("clearChecked");
            $('#report_grid').datagrid("checkRow", rowIndex);
            printReport(true);
        }
    });


    //报告下载
    $.DownLoad = function (index) {
        var rows = $('#report_grid').datagrid('getRows') || [],
            ReportFormID = rows[index].ReportFormID;
        //CENTER
        askService('downLoad', null, '{"jsonentity":{"reportformIds":"' + ReportFormID + '","reportformtitle":"CENTER"}}');
        //askService('downLoad', null, { reportformIds: ReportFormID, reportformtitle: 'CENTER' });
    }
    //批量下载
    function downLoadMost() {
        var rows = $('#report_grid').datagrid('getChecked') || [],
            length = rows.length,
            ReportFormIDs = '',
            param = '{"jsonentity":{';
        if (length == 0) {
            $.messager.alert('信息提示', '请勾选要下载的记录', 'info');
            return;
        }
        for (var i = 0; i < length; i++) {
            ReportFormIDs += rows[i]['ReportFormID'] + ';';
        }
        param += '"reportformtitle":"CENTER",';
        param += '"reportformIds":"' + ReportFormIDs + '"}}';

        askService('downLoad', null, param);
    }


    //采样日期
    var date = Shell.util.Date.toString(new Date(), true);
    // Shell.util.Date.getNextDate(date_s, 90).getTime(),
    var data_3 = Shell.util.Date.toString((new Date()) - 1000 * 60 * 60 * 72);



    //查询-状态
    $("#search-text-statues").combobox({
        height: 22,
        width: 70,
        valueField: 'statues',
        textField: 'text',
        editable: false,
        panelHeight: 80,
        data: [
            { statues: 0, text: '未打印' },
            { statues: 1, text: '已打印' },
            { statues: 2, text: '全部' }
        ],
        onLoadSuccess: function () {
            $(this).combobox('select', 0);
        }

    });

    //查询-状态
    $("#search-text-zdy10").combobox({
        height: 22,
        width: 50,
        valueField: 'zdy10',
        textField: 'text',
        editable: false,
        panelHeight: 50,
        data: [
            { zdy10: 0, text: 'A4' },
            { zdy10: 1, text: 'A5' }
        ],
        onLoadSuccess: function () {
            $(this).combobox('select', 0);
        }

    });

    //查询-结果异常
    $("#search-text-abnormalresult").combobox({
        height: 22,
        width: 70,
        valueField: 'abnormalresult',
        textField: 'text',
        editable: false,
        panelHeight: 80,
        data: [
            { abnormalresult: '', text: '全部' },
            { abnormalresult: '0', text: '正常结果' },
            { abnormalresult: '1', text: '异常结果' }
        ],
        onLoadSuccess: function () {
        }

    });

    //报告/结果切换
    $("#reportType2").bind('onchange', onReportTypeChange);

    //初始化结果图窗口
    initImageWin();

    //PDF方式没有设置打印选项
    if (fileType == "PDF") {
        $("#printConfig").hide();
    }
    onSearch();
});
var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

function generateMixed(n) {
    var res = "";
    for (var i = 0; i < n; i++) {
        var id = Math.ceil(Math.random() * 35);
        res += chars[id];
    }
    return res;
}
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return arr[2]; return null;
}
/**设置cookie属性*/
function setCookie(name, value) {
    var days = 30,
			exp = new Date();

    exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
function OpenWindowFuc(ptitle, pwidth, pheight, purl, sn) {
    $('#win').window({
        title: ptitle,
        width: pwidth,
        height: pheight,
        content: "<iframe src='" + purl + "' width='100%' height='100%' frameborder=0></iframe>",
        modal: true,
        onClose: function () {
            if (sn != null && sn != "") {
                if (document.getElementById(sn) != null) {
                    document.getElementById(sn).contentWindow.ContentReLoad();
                }
            }
        }
    }).window('open').window('center');

};
function getReportCheckedData() {
    //alert('aaa');
    var data = $('#report_grid').datagrid("getChecked") || [];
    return data;
}
