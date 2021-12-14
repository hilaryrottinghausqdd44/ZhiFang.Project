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
		    SelectReportListUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/SelectReportList2",
		    /**获取报告服务地址*/
		    GetPreviewReportByIdUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportById",
		    /**获取结果服务地址*/
		    GetPreviewReportResultByIdUrl: Shell.util.Path.rootPath +
"/ServiceWCF/ReportFromService.svc/GetPreviewReportResultById",
		    /**获取打印信息服务地址*/
		    ReportPrintUrl: Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/ReportPrint",
		    /**获取结果图*/
		    GETImagesUrl: Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportImageById"
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
            url: Shell.util.Path.rootPath + '/ServiceWCF/' + serviceParam.serviceName,
            data: serviceParam.data,
            dataType: 'json',
            type: serviceParam.type,
            timeout: 10000,
            async: async,
            contentType: 'application/json', //不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    switch (serviceType) {
                        case 'delete': $('#report_grid').datagrid('deleteRow', rowIndex);
                            break;
                        case 'load': $('#report_grid').datagrid('loadData', data);
                            break;
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
        }

        return serviceParam;
    }

    //获取cookie值 gwh add 2015-3-3
    function getCookieValue(name) {
        var key = name + '=',
			cookies = document.cookie;

        var startSet = cookies.indexOf(key);
        if (cookies.indexOf(key) > -1) {
            var startSet = cookies.indexOf(key),
				endSet = cookies.indexOf(';', startSet),
				keyValue = '';
            if (endSet == -1) {
                keyValue = cookies.substring(startSet);
            } else {
                keyValue = cookies.substring(startSet, endSet);
            }
            if (keyValue.indexOf('WebLisAdmin') > -1) {
                return 'admin';
            }
        }
        return 'NoAdmin';
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
    /**获取查询条件*/
    function getWhere() {
        var date_s = $("#search-text-RECEIVEDATE-S").datebox("getValue"),
			date_e = $("#search-text-RECEIVEDATE-E").datebox("getValue"),
			ClientNo = $("#search-text-ClientNo").combobox("getValue"),
			CNAME = $("#search-text-CNAME").searchbox("getValue"),
			SectionNo = $("#search-text-PGroup").combobox("getValues"),
			SickTypeNo = $("#search-text-SickType").combobox("getValue"),
			param = {};

        //开始日期必须存在,且结束日期-开始日期<=90
        if (!date_s) return null;
        var s = Shell.util.Date.getNextDate(date_s, 90).getTime(),
			e = (date_e ? Shell.util.Date.getDate(date_e) : new Date()).getTime();
        if (s < e) return null;

        param.Startdate = date_s;
        param.Enddate = date_e;
        if (ClientNo)
            param.CLIENTNO = ClientNo;
        if (CNAME)
            param.CNAME = CNAME;
        if (SectionNo && SectionNo != '0' && SectionNo.length>0)
            param.SECTIONNO = SectionNo.join(',');
        //alert(param.SECTIONNO);
        if (SickTypeNo && SickTypeNo != '0')
            param.SICKTYPENO = SickTypeNo;
        if ($('#report_grid').datagrid("options").url)
            $('#report_grid').datagrid('load', param);
        else//初始化列表的情况
        {
            $('#report_grid').datagrid("options").url = serverUrl.SelectReportListUrl;
            $('#report_grid').datagrid('load', param);
            //$('#report_grid').datagrid("options").queryParams=param;
        }


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
            html.push("<img width='100%' height='100%' src='" + Shell.util.Path.rootPath + "/" + list[i].replace(/\\/g, "\/") +
"'></img>");
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

        if (fileType == "PDF") {
            winPdfPrint();
        } else if (fileType == "JPG" || fileType == "JPEG") {
            for (var i = 0; i < len; i++) {
                GetReportPrintList(list[i].ReportFormID, reportformtitle, function (result) {
                    if (result.success) {
                        ReportFormIDArr.push(list[i].ReportFormID);
                        var url = Shell.util.JSON.decode(result.ResultDataValue) || [];
                        if (url.length == 1) { urls.push(url[0]); }
                    } else {
                        error.push("<b style='color:red;'>" + result.ErrorInfo + "</b>");
                    }
                });
            }
            if (error.length > 0) {
                Shell.easyuiUtil.Msg.show({
                    title: "错误信息",
                    msg: "<b style='color:red;'>" + error.join(" ") + "</b>"
                });
            } else {
                winLodopPrint(urls, ReportFormIDArr, preview);
            }
        } else {
            $.messager.alert("错误提示", "打印方式没有配置或配置错误！", "error");
        }
    }

    //删除报告单
    function deleteReport() {
        var rows = $('#report_grid').datagrid('getChecked') || [],
			length = rows.length || 0;

        if (length == 0) {
            $.messager.alert('提示信息', '请选择要删除的记录', 'info');
        } else {
            $.messager.confirm('确认', '确认删除所勾选的数据吗？', function (btn) {
                if (btn) {
                    for (var i = length - 1; i >= 0; i--) {
                        var index = $('#report_grid').datagrid('getRowIndex', rows[i]);
                        askService('delete', null, { ReportFormID: rows[i]['ReportFormID'] }, index);
                    }
                }
            });
        }
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
                for (var i = 0; i < ReportFormIDArr.length; i++)
                //记录打印次数
                    askService('print', null, { ReportFormID: ReportFormIDArr[i] });
            }
        } else {
            var print = lodop.PRINT(); //返回true,表示打印成功
            if (print) {
                for (var i = 0; i < ReportFormIDArr.length; i++)
                //记录打印次数
                    askService('print', null, { ReportFormID: ReportFormIDArr[i] });
            }
        }
        onSearch();
    }
    /**pdf页面打印*/
    function winPdfPrint() {
        var printConfigInfo = getPrintConfigInfo(),
			reportformtitle = printConfigInfo.titleType;

        var url = Shell.util.Path.uiPath + "/report/printTest/printPDF.html",
			data = $('#report_grid').datagrid("getChecked") || [],
			len = data.length,
			d = [],
			ids = [],
			merge = $('#mergeReport').combobox('getValue');

        for (var i = 0; i < len; i++) {
            var sectionno = data[i].SECTIONNO;
            if (merge != "empty" && sectionno != "2") {
                $.messager.alert("提示信息", "报告单合并操作只支持生化组检验组的报告单!所选报告单包含生化组以外的报告单，请重新选择!",
"error");
                return;
            }
            ids.push(data[i].ReportFormID);
        }
        url += "?ids=" + ids.join(",") + "&merge=" + merge + "&reportformtitle=" + reportformtitle;

        //默认排序条件
        var options = $('#report_grid').datagrid("options");
        url += "&sortName=" + options.sortName + "&sortOrder=" + options.sortOrder;

        //var SN = Shell.util.Path.getRequestParams()["SN"];
        parent.OpenWindowFuc("PDF打印", "96%", "96%", url);
    }

    /**获取打印类型设置*/
    function getPrintConfigInfo() {
        var titleType = $("#titleType").combobox("getValue"),
			paperType = $("#paperType").combobox("getValue"),
			orientationType = $("#orientationType").combobox("getValue");

        return {
            titleType: titleType,
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

        if (url) url += "?reportformId=" + params.reportformId + "&sectionNo=" + params.sectionNo + "&sectionType=" +
params.sectionType;
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
        var url = serverUrl.ReportPrintUrl + "?reportformId=" + reportformId + "&reportformtitle=" + reportformtitle +
"&reportformfiletype=JPG&printtype=1";
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

    var date = new Date(),
		date_s = Shell.util.Date.toString(date, true),
		date_e = Shell.util.Date.toString(Shell.util.Date.getNextDate(date), true);

    //列表-刷新
    $("#report_grid_toolbar-refresh").bind('click', function () { onSearch(); });
    //列表-直接打印
    $("#report_grid_toolbar-print").bind('click', function () { printReport(); });
    //列表-预览打印
    $("#report_grid_toolbar-preview").bind('click', function () { printReport(true); });
    //删除报告单
    if (getCookieValue('ZhiFangUserPosition') == 'admin') {
        $('#report_grid_toolbar-delete').show();
    }
    $('#report_grid_toolbar-delete').bind('click', function () { deleteReport(); });

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
            { field: 'RECEIVEDATE', title: '核收日期', sortable: true, width: 100, formatter: function (value, index, row) {
                if (!value) return "";
                return value.slice(0, 10).replace(/\//g, "-");
            }, tooltip: function (value, index, row) {
                if (!value) return "";
                return "<b>" + value.slice(0, 10).replace(/\//g, "-") + "</b>";
            }, sorter: function (a, b) {
                return;
            }
            },
            { field: 'CNAME', title: '名称', width: 70, sortable: true, tooltip: function (value, index, row) {
                return "<b>" + value + "</b>";
            }, sorter: function (a, b) {
                return;
            }
            },
            { field: 'SAMPLENO', title: '样本号', sortable: true, width: 100, sorter: function (a, b) {
                return;
            } 
            },
			{ field: 'PRINTTIMES', title: '打印次数', sortable: true, width: 100, sorter: function (a, b) {
			    return;
			} 
			},
			{ field: 'ZDY8', title: '编号', width: 70, sortable: true, sorter: function (a, b) {
			    return;
			} 
			},
            { field: 'SECTIONNO', title: '检验小组编号', hidden: true },
            { field: 'CLIENTNO', title: '送检单位编码', hidden: true },
            { field: 'SectionType', title: '小组类型', hidden: true }
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
                clearContent();
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
        }
    });

    //判断时间
    $('#search-text-RECEIVEDATE-E').datebox({
        onSelect: function (date) {
            var startDate = $('#search-text-RECEIVEDATE-S').datebox('getValue'),
				endDate = $('#search-text-RECEIVEDATE-E').datebox('getValue');
            if (endDate < startDate) {
                $('#search-text-RECEIVEDATE-E').datebox('setValue', '');
                $.messager.alert('提示', '不能小于开始时间', 'info');
            }
        }
    });
    //核收日期
    var date = Shell.util.Date.toString(new Date(), true);
    $("#search-text-RECEIVEDATE-S").datebox({ disabled: false, value: date });
    $("#search-text-RECEIVEDATE-E").datebox({ disabled: false, value: date });
    $("#search-text-RECEIVEDATE-S").next('.combo').find('input').bind('keydown', function (e) {
        if (e.keyCode == 13) { onSearch(); }
    });
    $("#search-text-RECEIVEDATE-E").next('.combo').find('input').bind('keydown', function (e) {
        if (e.keyCode == 13) { onSearch(); }
    });

    //送检单位
    $('#search-text-ClientNo').combobox({
        height: 22, width: 130,
        valueField: "ClIENTNO",
        textField: "CNAME",
        editable: true,
        method: 'GET',
        url: Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO",
        loadFilter: function (data) {
            data = data || [];
            if (data.length > 0) {
                if (getCookieValue('ZhiFangUserPosition') == 'admin')
                    $(this).combobox('setValue', '');
                else
                    data[0].selected = true;
                clientShortCodeAry = data;

            }
            return data;
        },
        onLoadSuccess: function () {
            onSearch();
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options'),
			     shortCode = row['SHORTCODE'] || "",
				 CName = row[opts.textField] || "";

            if (CName.indexOf(q) > -1) {
                return true;
            }
            q = q.toUpperCase();
            if (shortCode.indexOf(q) > -1) {
                return true;
            }
            return false;
        }
    });
    //查询-姓名
    $("#search-text-CNAME").searchbox({ height: 22, width: 100, prompt: '请输入姓名', searcher: onSearch });
    //查询-检验小组
    $("#search-text-PGroup").combobox({
        height: 22, width: 200,
        valueField: "SectionNo",
        textField: "CName",
        editable: false,
        panelMaxHeight: 130,
        method: 'GET',
        multiple: true,
        url: Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=PGroup&fields=SectionNo,CName",
        loadFilter: function (data) {
            if (data.success) {
                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                var list = obj.rows || [];
                //list.unshift({ "SectionNo": 0, "CName": "全部", "selected": true });
                return list;
            } else {
                //return [{ "SectionNo": 0, "CName": "全部", "selected": true}];
            }
        }
    });
    //查询-就诊类型
    $("#search-text-SickType").combobox({
        height: 22, width: 100,
        valueField: "SickTypeNo",
        textField: "CName",
        editable: false,
        panelMaxHeight: 130,
        method: 'GET',
        url: Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=SickType&fields=SickTypeNo,CName",
        loadFilter: function (data) {
            if (data.success) {
                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                var list = obj.rows || [];
                list.unshift({ "SickTypeNo": 0, "CName": "全部", "selected": true });
                return list;
            } else {
                return [{ "SickTypeNo": 0, "CName": "全部", "selected": true}];
            }
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

});
