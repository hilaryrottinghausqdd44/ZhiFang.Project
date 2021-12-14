$(function () {
    var inputflag = 0;
    var IsDelivery = 0;
    var IsReceive = 0;
    var para = Shell.getRequestParams();
    if (para["IsInput"] && para["IsInput"] == "1") {
        inputflag = 1;
        $("#btnaddnrequestformdiv").css("display", "none");
    }

    if (para["IsDelivery"] && para["IsDelivery"] == "1") {
        IsDelivery = 1;
        $("#btnaddnrequestformdiv").css("display", "none");
        $("#btnprint").css("display", "none");
        $("#btnReceive").css("display", "none");
        $("#btnRejection").css("display", "none");
    }
    else {
        $("#btnDelivery").css("display", "none");
    }

    if (para["IsReceive"] && para["IsReceive"] == "1") {
        IsReceive = 1;
        $("#btnaddnrequestformdiv").css("display", "none");
        $("#btnprint").css("display", "none");
        $("#btnDelivery").css("display", "none");

        //$("comboboxWebLisFlag").combobox("clear");
        //var data = { }
        //$("comboboxWebLisFlag").combobox("loadData",);
    }
    else {
        $("#btnReceive").css("display", "none");
        $("#btnRejection").css("display", "none");
    }

    var datetimenow = new Date();
    var datetimestr = datetimenow.getFullYear() + "-" + (datetimenow.getMonth() + 1) + "-" + datetimenow.getDate();
    $('#OperateStartDateTime').datebox({
        onSelect: function (date) {
            if (date > datetimenow) {
                alert("所选日期不能大于当前系统日期！");
                $('#OperateStartDateTime').datebox('setValue', datetimestr);
                return;
            }
            var tmp = Shell.util.Date.getDate($('#OperateEndDateTime').datebox('getValue'));
            if (tmp && date > tmp ) {
                alert("开始日期不能大于结束日期！");
                $('#OperateStartDateTime').datebox('setValue', tmp.getFullYear() + "-" + (tmp.getMonth() + 1) + "-" + tmp.getDate());
                return;
            }
        }
    }).datebox('setValue', datetimestr);

    $('#OperateEndDateTime').datebox({
        onSelect: function (date) {
            if (date > datetimenow) {
                alert("所选日期不能大于当前系统日期！");
                $('#OperateEndDateTime').datebox('setValue', datetimestr);
                return;
            }
            var tmp = Shell.util.Date.getDate($('#OperateStartDateTime').datebox('getValue'));
            if (tmp && date < tmp) {
                alert("结束日期不能小于开始日期！");
                $('#OperateEndDateTime').datebox('setValue', tmp.getFullYear() + "-" + (tmp.getMonth() + 1) + "-" + tmp.getDate());
                return;
            }
        }
    }).datebox('setValue', datetimestr);

    $('#txtCollectStartDate').datebox({
        onSelect: function (date) {
            if (date > datetimenow) {
                alert("所选日期不能大于当前系统日期！");
                $('#txtCollectStartDate').datebox('setValue', datetimestr);
                return;
            }
            var tmp = Shell.util.Date.getDate($('#txtCollectEndDate').datebox('getValue'));
            if (tmp && date > tmp) {
                alert("开始日期不能大于结束日期！");
                $('#txtCollectStartDate').datebox('setValue', tmp.getFullYear() + "-" + (tmp.getMonth() + 1) + "-" + tmp.getDate());
                return;
            }
        }
    });

    $('#txtCollectEndDate').datebox({
        onSelect: function (date) {
            if (date > datetimenow) {
                alert("所选日期不能大于当前系统日期！");
                $('#txtCollectEndDate').datebox('setValue', datetimestr);
                return;
            }
            var tmp = Shell.util.Date.getDate($('#txtCollectStartDate').datebox('getValue'));
            if (tmp && date < tmp) {
                alert("结束日期不能小于开始日期！");
                $('#txtCollectEndDate').datebox('setValue', tmp.getFullYear() + "-" + (tmp.getMonth() + 1) + "-" + tmp.getDate());
                return;
            }
        }
    });

    $('#btnsearch').bind('click', function () {
        var ClientNo = $('#txtClientNo').combobox('getValue') ? $('#txtClientNo').combobox('getValue') : "\"\"";
        var WebLisFlag = $('#comboboxWebLisFlag').combobox('getValue') ? $('#comboboxWebLisFlag').combobox('getValue') : "-1";
        $('#NRequestFormList').datagrid({
            url: '../../ServiceWCF/NRequestFromService.svc/GetNRequestFromListByRBAC?guid=' + generateMixed(10),
            queryParams: {
                jsonentity: "{ClientNo:" + ClientNo
                    + ",OperDateStart:'" + $('#OperateStartDateTime').datebox('getValue')
                    + "',OperDateEnd:'" + $('#OperateEndDateTime').datebox('getValue')
                    + "',CollectDateStart:'" + $('#txtCollectStartDate').datebox('getValue')
                    + "',CollectDateEnd:'" + $('#txtCollectEndDate').datebox('getValue')
                    + "',DoctorName:'" + $('#SelectDoctor').combobox('getText')
                    + "',CName:'" + $('#CName').textbox('getValue')
                    + "',PatNo:'" + $('#PatNo').textbox('getValue')
                    + "',SampleSendNo:'" + $('#SampleSendNo').textbox('getValue')
                    + "',WeblisflagList:'" + WebLisFlag
                    + "'}"
            }
        });
    });
    $('#btnlodop').bind('click', function () {
        var LODOP = Lodop.getLodop();
        if (LODOP.VERSION) {
            if (LODOP.CVERSION)
                alert("当前有C-Lodop云打印可用!\n C-Lodop版本:" + LODOP.CVERSION + "(内含Lodop" + LODOP.VERSION + ")");
            else
                alert("本机已成功安装了Lodop控件！\n 版本号:" + LODOP.VERSION);

        };
    });

    $('#btnaddnrequestform').bind('click', function () {
        var SN = Shell.util.Path.getRequestParams()["SN"];
        parent.OpenWindowFuc('新增申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), Shell.util.Path.rootPath + '/ui/entry_Google/apply.html', SN);
        //var url = "../ui/entry/apply.html";
        //window.open(url, "新增申请单", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
    });
    $('#btnprint').bind('click', function () { PrintNRequestFormList(); });
    $('#btnDelivery').bind('click', function () { DeliveryNRequestFormList(); });
    $('#btnReceive').bind('click', function () { ReceiveNRequestFormList("1"); });
    $('#btnRejection').bind('click', function () { ReceiveNRequestFormList("0"); });

    var toolbar = [];
    if (!(inputflag == 1)) {

        //toolbar.push({
        //    iconCls: 'icon-add',
        //    text: '新增',
        //    handler: function () {
        //        var SN = Shell.util.Path.getRequestParams()["SN"];
        //        parent.OpenWindowFuc('新增申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry_Google/apply.html', SN);
        //    }
        //});
    }
    toolbar.push({
        iconCls: 'icon-barcode',
        text: '打印条码',
        handler: function () {
            var nrflist = $('#NRequestFormList').datagrid('getChecked');
            var barcodeentitylist = [];
            $.each(nrflist, function (i, item) {
                //alert(i);
                //alert(item.CName);
                var BarCodeFormList = item.BarCodeFormList;
                for (var i = 0; i < BarCodeFormList.length; i++) {
                    var tmp = {
                        BarCode: BarCodeFormList[i].BarCode,
                        ClientName: item.WebLisSourceOrgName,
                        Name: item.CName,
                        Sex: item.GenderName,
                        Age: item.Age,
                        AgeUnit: item.AgeUnitName,
                        SickTypeName: item.SickTypeName,
                        PatNo: item.PatNo,
                        TestTypeName: item.TestTypeName,
                        ColorName: BarCodeFormList[i].Color,
                        ItemList: BarCodeFormList[i].ItemName,
                        CollectDate: item.CollectTime,
                        DeptNo: item.DeptName
                    }
                    barcodeentitylist.push(tmp);
                }
            });
            Shell.barcode.Print.barcode(barcodeentitylist, true);
        }
    });/**/
    var positionlist = Shell.util.Cookie.getCookie("ZhiFangUserPosition");
    if (positionlist.toUpperCase().indexOf('WEBLISADMIN') >= 0) {
        toolbar.push('-');
        toolbar.push({
            iconCls: 'icon-help',
            text: '条码设置',
            handler: function () { Shell.barcode.Print.designAndSave(); }
        });
    }

    $('#NRequestFormList').datagrid({
        //url: '../ServiceWCF/NRequestFromService.svc/GetNRequestFromListByRBAC',
        method: 'GET',
        rownumbers: true,
        singleSelect: false,
        pagination: true,
        fitColumns: true,
        checkOnSelect: true,
        striped: true,
        columns: [[
            { field: 'cb', checkbox: 'true' },
            { field: 'BarcodeList', title: '条码号', width: 150 },
            { field: 'CName', title: '姓名', width: 50 },
            { field: 'GenderName', title: '性别', width: 30, align: 'center' },
            { field: 'Age', title: '年龄(岁)', width: 50 },
            { field: 'SampleTypeName', title: '样本', width: 50 },
            { field: 'ItemList', title: '项目', width: 150, align: 'center' },
            { field: 'DoctorName', title: '医生', width: 30 },
            { field: 'OperTime', title: '开单时间', width: 90, align: 'center' },
            { field: 'CollectTime', title: '采样时间', width: 90, align: 'center' },
            {
                        field: 'WebLisFlag', title: '状态', width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value == '') {
                                return row.WebLisFlag = '待外送';
                            }
                            if (value == '0') {
                                return row.WebLisFlag = '待外送';
                            }
                            if (value == '1') {
                                return row.WebLisFlag = '已打包';
                            }
                            if (value == '2') {
                                return row.WebLisFlag = '已外送';
                            }

                            if (value == '5') {
                                return row.WebLisFlag = '已签收';
                            }
                            if (value == '6') {
                                return row.WebLisFlag = '已拒收';
                            }
                            if (value == '10') {
                                return row.WebLisFlag = '已出报告';
                            }
                           
                        },
                        styler: function (value, row, index) {
                            if (value == '') {
                                return 'background-color:ffffff;';
                            }
                            if (value == '0') {
                                return 'background-color:ffffff;';
                            }
                            if (value == '1') {
                                return 'background-color:FFaaCC;';
                            }
                            if (value == '2') {
                                return 'background-color:FFaa00;';
                            }
                            if (value == '5') {
                                return 'background-color:FFF2CC;';
                            }
                            if (value == '6') {
                                return 'background-color:red;';
                            }
                            if (value == '10') {
                                return 'background-color:00cccc;';
                            }
                            return "";
                        }
                    },
            { field: 'WebLisSourceOrgName', title: '送检单位', width: 80, align: 'center' },
            { field: 'RejectionReason', title: '拒收原因', width: 80, align: 'center' },
            {
                field: 'action', title: '操作', width: 50, align: 'center',
                formatter: function (value, row, index) {
                    if (row.WebLisFlag == "已打包" || row.WebLisFlag == "已外送" || row.WebLisFlag == "已签收" || row.WebLisFlag == "已拒收") {
                        var a = '<a href="javascript:viewrow(\'' + row.NRequestFormNo + '\')" class="ope-save" >查看</a> ';
                        return a;
                    }
                    else {
                        var a = '<a href="javascript:editrow(\'' + row.NRequestFormNo + '\')" class="ope-save" >修改</a> ';
                        var b = '<a href="javascript:deleterow(\'' + row.NRequestFormNo + '\')"class="ope-save">删除</a>';
                        return a + b;
                    }
                }
            }
        ]],
        //                queryParams: {
        //                    jsonentity: "{ClientNo:1,OperDateStart:'2014-11-01',OperDateEnd:'2014-11-27'}"
        //                },
        onDblClickCell: function (index, field, value) {
            alert(index);
        },
        toolbar: toolbar
        //toolbar: $('#dlg-toolbar')
    });
    $('#txtClientNo').combobox({
        url: '../../ServiceWCF/DictionaryService.svc/GetClientListByRBAC?guid=' + generateMixed(10) + '&page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO&where=&sort=',
        method: 'GET',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter: function (data) {
            if (data.length > 0) {
                var flag = false;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].BusinessLogicClientControlFlag == 1) {
                        data[i].selected = true;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    data[0].selected = true;
            }
            return data;
        },
        onLoadSuccess: function () {
               $('#SelectDoctor').combobox({
                        url: '../../ServiceWCF/DictionaryService.svc/GetPubDict?tableName=B_Lab_Doctor&fields=LabDoctorNo,CName,ShortCode&labcode=' + $('#txtClientNo').combobox('getValue'),
                        method: 'GET',
                        valueField: 'LabDoctorNo',
                        textField: 'CName',
                        multiple: true,
                        loadFilter: function (data) {
                            if (data.success) {
                                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                                var list = obj.rows || [];
                                return list;
                            } else {
                                return [];
                            }
                        }
                    });
                    $('#SickTypeNo').combobox({
                        url: '../../ServiceWCF/DictionaryService.svc/GetCenterSickTypeListByLab_Area_Center?labcode=' + $('#txtClientNo').combobox('getValue') + '&page=0&rows=1000',
                        method: 'GET',
                        valueField: 'SickTypeNo',
                        textField: 'CName',
                        multiple: true,
                        loadFilter: function (data) {
                            if (data.success) {
                                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                                var list = obj.rows || [];
                                return list;
                            } else {
                                return [];
                            }
                        }
                    });
                    $('#btnsearch').click();
        },
                onChange: function (newValue, oldValue) {
                    //alert($('#txtClientNo').combobox('getValue'));
                    if ($('#txtClientNo').combobox('getValues').length != 1) {
                        $('#SelectDoctor').combobox('clear');
                        return;
                    }
                    $('#SelectDoctor').combobox({
                        url: '../../ServiceWCF/DictionaryService.svc/GetPubDict?tableName=B_Lab_Doctor&fields=LabDoctorNo,CName,ShortCode&labcode=' + newValue,
                        method: 'GET',
                        valueField: 'LabDoctorNo',
                        textField: 'CName',
                        loadFilter: function (data) {
                            if (data.success) {
                                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                                var list = obj.rows || [];
                                return list;
                            } else {
                                return [];
                            }
                        }
                    });
                    $('#SickTypeNo').combobox({
                        url: '../../ServiceWCF/DictionaryService.svc/GetCenterSickTypeListByLab_Area_Center?labcode=' + newValue +'&page=0&rows=1000',
                        method: 'GET',
                        valueField: 'SickTypeNo',
                        textField: 'CName',
                        multiple: true,
                        loadFilter: function (data) {
                            if (data.success) {
                                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                                var list = obj.rows || [];
                                return list;
                            } else {
                                return [];
                            }
                        }
                    });
                    $('#btnsearch').click();
                }
        //mode:'remote'//,
        //                onSelect: function (Key) {
        //                    var url = '../ServiceWCF/DictionaryService.svc/GetClientListByRBACAndInputKey?page=1&limit=10&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO&where=&sort=&inputkey=' + Key;
        //                    $('#txtClientNo').combobox('reload', url);
        //                }
    });
});
function editrow(target) {
    ModifyNRequestForm(target);
}
function viewrow(NRequestFormNo) {
    var SN = Shell.util.Path.getRequestParams()["SN"];
    parent.OpenWindowFuc('修改申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry_Google/apply.html?Flag=1&NRequestFormNo=' + NRequestFormNo + '&BarCodeInputFlag=1&ReadOnly=1', SN);
    //var url = "ApplyInput_Weblis_dajia.aspx?Flag=1&NRequestFormNo=" + NRequestFormNo + "&BarCodeInputFlag=1";
    //window.open(url, "修改申请单", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));

}
function deleterow(NRequestFormNo) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                type: "GET",
                url: "../../ServiceWCF/NRequestFromService.svc/DeleteNRequestFromByNRequestFromNo?NRequestFromNo=" + NRequestFormNo,
                dataType: "json",
                success: function (data) {
                    if (data.BoolFlag) {
                        $.messager.alert('提示', '申请单删除成功！', 'info');
                        $('#NRequestFormList').datagrid('reload');
                    }
                    else {
                        if (data.ErrorInfo != null && data.ErrorInfo != "") {
                            $.messager.alert('提示', '申请单删除失败！ErrorInfo:' + data.ErrorInfo, 'error');
                        }
                        else {
                            $.messager.alert('提示', '此申请单已被核收不能删除！', 'error');

                        }
                    }
                }
            });
        }
    });
}
function PrintNRequestFormList1() {
    var ClientNo = $('#txtClientNo').combobox('getValue');
    var txtStartDate = $('#OperateStartDateTime').datebox('getValue');
    var txtEndDate = $('#OperateEndDateTime').datebox('getValue');
    var txtCollectStartDate = $('#txtCollectStartDate').datebox('getValue');
    var txtCollectEndDate = $('#txtCollectEndDate').datebox('getValue');
    var SelectDoctor = $('#SelectDoctor').combobox('getValue');
    var txtPatientID = $('#PatNo').textbox('getValue');
    var txtPatientName = $('#CName').textbox('getValue');
    var WebLisFlag = $('#comboboxWebLisFlag').combobox('getValue') ? $('#comboboxWebLisFlag').combobox('getValue') : "\"\"";
    var url = "PrintNRequestFormList.aspx?guid=" + generateMixed(10) + "&IsUpDateWeblisFlag=1&ClientNo=" + ClientNo + "&txtStartDate=" + txtStartDate + "&txtEndDate=" + txtEndDate + "&txtCollectStartDate=" + txtCollectStartDate + "&txtCollectEndDate=" + txtCollectEndDate + "&SelectDoctor=" + SelectDoctor + "&txtPatientID=" + txtPatientID + "&txtPatientName=" + txtPatientName + "&WebLisFlagList=" + WebLisFlag;
    //alert(url);

    window.open(url, "申请单预览打印", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
}
function PrintNRequestFormList() {
    var nrflist = $('#NRequestFormList').datagrid('getChecked');
	var ClientNo = $('#txtClientNo').combobox('getValue');
	 var txtStartDate = $('#OperateStartDateTime').datebox('getValue');
    var txtEndDate = $('#OperateEndDateTime').datebox('getValue');
    if (!nrflist || nrflist.length <= 0) {
        $.messager.alert('提示', '请选择需要外送的样本！', 'info');
        return;
    }
    var NRequestFormNolist = [];
    $.each(nrflist, function (i, item) {
        var NRequestFormNo = item.NRequestFormNo;
        NRequestFormNolist.push(NRequestFormNo);
    });
    //alert(NRequestFormNolist.join(','));
    var WebLisFlag = $('#comboboxWebLisFlag').combobox('getValue') ? $('#comboboxWebLisFlag').combobox('getValue') : "\"\"";
    var url = "../PrintNRequestFormListByNRFNo.aspx?guid=" + generateMixed(10) + "&IsUpDateWeblisFlag=1&ClientNo=" + ClientNo + "&txtStartDate=" + txtStartDate + "&ParaNRFNO=" + NRequestFormNolist + "&WebLisFlagList=" + WebLisFlag;
    //alert(url);

    window.open(url, "申请单预览打印", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
}
function DeliveryNRequestFormList() {
    var nrflist = $('#NRequestFormList').datagrid('getChecked');
    var barcodeentitylist = [];
    $.each(nrflist, function (i, item) {
        var BarCodeFormList = item.BarCodeFormList;
        for (var i = 0; i < BarCodeFormList.length; i++) {
            barcodeentitylist.push(BarCodeFormList[i].BarCode);
        }
    });
    alert(barcodeentitylist.join(','));
    $.ajax({
        type: "GET",
        url: "../../ServiceWCF/NRequestFromService.svc/DeliveryBarCodeFromByBarCodeList?BarCodeList=" + barcodeentitylist.join(',') + "&Flag=true",
        dataType: "json",
        success: function (data) {
            if (data.BoolFlag) {
                $.messager.alert('提示', '物流已收样！', 'info');

            }
            else {
                if (data.ErrorInfo != null && data.ErrorInfo != "") {
                    $.messager.alert('提示', '物流收样失败！ErrorInfo:' + data.ErrorInfo, 'error');
                }
            }
        }
    });
    $('#btnsearch').click();
}
function ReceiveNRequestFormList(Flag) {
    var nrflist = $('#NRequestFormList').datagrid('getChecked');
    var barcodeentitylist = [];
    var strreceive = "签收";
    var strflag = "TRUE";
    var Reason = "";
    $.each(nrflist, function (i, item) {
        var BarCodeFormList = item.BarCodeFormList;
        for (var i = 0; i < BarCodeFormList.length; i++) {
            barcodeentitylist.push(BarCodeFormList[i].BarCode);
        }
    });

    if (Flag == "0") {
        strreceive = "拒收";
        strflag = "FALSE";
        $.messager.prompt('提示信息', '拒收原因：', function (r) {
            if (r) {
                Reason = r;

                $.ajax({
                    type: "GET",
                    url: encodeURI("../../ServiceWCF/NRequestFromService.svc/ReceiveBarCodeFromByBarCodeList?BarCodeList=" + barcodeentitylist.join(',') + "&Flag=" + strflag + "&Reason=" + Reason),
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            $.messager.alert('提示', '样本已' + strreceive + '！', 'info');

                        }
                        else {
                            if (data.ErrorInfo != null && data.ErrorInfo != "") {
                                $.messager.alert('提示', '样本' + strreceive + '失败！ErrorInfo:' + data.ErrorInfo, 'error');
                            }
                        }
                    }
                });
                $('#btnsearch').click();
            }
        });
    }
    else {

        $.ajax({
            type: "GET",
            url: encodeURI("../../ServiceWCF/NRequestFromService.svc/ReceiveBarCodeFromByBarCodeList?BarCodeList=" + barcodeentitylist.join(',') + "&Flag=" + strflag + "&Reason=" + Reason),
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    $.messager.alert('提示', '样本已' + strreceive + '！', 'info');

                }
                else {
                    if (data.ErrorInfo != null && data.ErrorInfo != "") {
                        $.messager.alert('提示', '样本' + strreceive + '失败！ErrorInfo:' + data.ErrorInfo, 'error');
                    }
                }
            }
        });
        $('#btnsearch').click();
    }

    //alert(barcodeentitylist.join(','));

}
function ModifyNRequestForm(NRequestFormNo) {
    $.ajax({
        type: "GET",
        url: "../../ServiceWCF/NRequestFromService.svc/CheckNRequestFromStatusByNRequestFromNo?NRequestFromNo=" + NRequestFormNo,
        dataType: "json",
        success: function (data) {
            checkweblisflag = data.BoolFlag;
            if (checkweblisflag) {
                var SN = Shell.util.Path.getRequestParams()["SN"];
                parent.OpenWindowFuc('修改申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry_Google/apply.html?Flag=1&NRequestFormNo=' + NRequestFormNo + '&BarCodeInputFlag=1', SN);
                //var url = "ApplyInput_Weblis_dajia.aspx?Flag=1&NRequestFormNo=" + NRequestFormNo + "&BarCodeInputFlag=1";
                //window.open(url, "修改申请单", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
            }
            else {
                $.messager.alert('提示', '此申请单已被核收不能修改！', 'error');
            }
        }
    });
}
var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

function generateMixed(n) {
    var res = "";
    for (var i = 0; i < n; i++) {
        var id = Math.ceil(Math.random() * 35);
        res += chars[id];
    }
    return res;
}

function ContentReLoad() {
    //$('#btnsearch').click();
    $('#NRequestFormList').datagrid('reload');
}