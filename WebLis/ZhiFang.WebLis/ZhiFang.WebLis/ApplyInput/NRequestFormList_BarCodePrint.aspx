<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NRequestFormList_BarCodePrint.aspx.cs"
    Inherits="ZhiFang.WebLis.ApplyInput.NRequestFormList_BarCodePrint" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>申请单列表</title>
    <link href="../ui/easyui/demo/demo.css" rel="stylesheet" type="text/css" />
    <link href="../ui/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../ui/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../ui/easyui/jquery-1.11.0-vsdoc.js" type="text/javascript"></script>
    <script src="../ui/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="../ui/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../ui/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../ui/util/util.js?v20200810-1" type="text/javascript"></script>
    <script src="../ui/print/Print.js" type="text/javascript"></script>
    <script src="../ui/entry_taihe/barcode.js" type="text/javascript"></script>
    <script language="javascript">
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
            $('#OperateStartDateTime').datebox('setValue', datetimestr);
            $('#OperateEndDateTime').datebox('setValue', datetimestr);

            $('#btnsearch').bind('click', function () {
                var ClientNo = $('#txtClientNo').combobox('getValue') ? $('#txtClientNo').combobox('getValue') : "\"\"";
                var WebLisFlag = $('#comboboxWebLisFlag').combobox('getValue') ? $('#comboboxWebLisFlag').combobox('getValue') : "-1";
                $('#NRequestFormList').datagrid({
                    url: '../ServiceWCF/NRequestFromService.svc/GetNRequestFromListByRBAC?guid=' + generateMixed(10),
                    queryParams: {
                        jsonentity: "{ClientNo:" + ClientNo
                            + ",OperDateStart:'" + $('#OperateStartDateTime').datebox('getValue')
                            + "',OperDateEnd:'" + $('#OperateEndDateTime').datebox('getValue')
                            + "',CollectDateStart:'" + $('#txtCollectStartDate').datebox('getValue')
                            + "',CollectDateEnd:'" + $('#txtCollectEndDate').datebox('getValue')
                            + "',DoctorName:'" + $('#SelectDoctor').combobox('getValue')
                            + "',CName:'" + $('#CName').textbox('getValue')
                            + "',PatNo:'" + $('#PatNo').textbox('getValue')
                            + "',SampleSendNo:'" + $('#SampleSendNo').textbox('getValue')
                            + "',WeblisflagList:'" + WebLisFlag
                            + "'}"
                    }
                });
            });
            $('#SelectDoctor').combobox({
                url: '../ServiceWCF/DictionaryService.svc/GetCenterDoctorAllList',
                method: 'GET',
                valueField: 'DoctorNo',
                textField: 'CName'
            });

            $('#btnaddnrequestform').bind('click', function () {
                var SN = Shell.util.Path.getRequestParams()["SN"];
                parent.OpenWindowFuc('新增申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), Shell.util.Path.rootPath + '/ui/entry_taihe/apply.html', SN);
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
                //        parent.OpenWindowFuc('新增申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry_taihe/apply.html', SN);
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
                        //var bclist = item.BarcodeList.split(',');
                        //var bccolornamelist = item.ColorName.split(',');
                        //var bcitemnamelist = item.ItemList.split(';');
                        //for (var i = 0; i < bclist.length; i++) {
                        //    Shell.taida.Print.barcode({
                        //        BarCode: bclist[i],
                        //        ClientName: item.WebLisSourceOrgName,
                        //        Name: item.CName,
                        //        Sex: item.GenderName,
                        //        Age: item.Age,
                        //        AgeUnit: item.AgeUnitName,
                        //        SickTypeName: item.SickTypeName,
                        //        PatNo: item.PatNo,
                        //        ColorName: bccolornamelist[i],
                        //        ItemList: bcitemnamelist[i],
                        //        CollectDate: item.CollectTime,
                        //        DeptNo: item.DeptName
                        //    });
                        //}

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
                                ColorName: BarCodeFormList[i].Color,
                                ItemList: BarCodeFormList[i].ItemName,
                                CollectDate: item.CollectTime,
                                DeptNo: item.DeptName
                            }
                            barcodeentitylist.push(tmp);
                        }
                    });
                    Shell.taida.Print.barcode(barcodeentitylist, true);
                }
            });
            var positionlist = Shell.util.Cookie.getCookie("ZhiFangUserPosition");
            if (positionlist.toUpperCase().indexOf('WEBLISADMIN') >= 0) {
                toolbar.push('-');
                toolbar.push({
                    iconCls: 'icon-help',
                    text: '条码设置',
                    handler: function () { Shell.taida.Print.designAndSave(); }
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
                        field: 'WebLisFlag', title: '样本状态', width: 90, align: 'center',
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
                url: '../ServiceWCF/DictionaryService.svc/GetClientListByRBAC?guid=' + generateMixed(10) + '&page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO&where=&sort=',
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
            parent.OpenWindowFuc('修改申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry_taihe/apply.html?Flag=1&NRequestFormNo=' + NRequestFormNo + '&BarCodeInputFlag=1&ReadOnly=1', SN);
            //var url = "ApplyInput_Weblis_dajia.aspx?Flag=1&NRequestFormNo=" + NRequestFormNo + "&BarCodeInputFlag=1";
            //window.open(url, "修改申请单", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));

        }
        function deleterow(NRequestFormNo) {
            $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
                if (r) {
                    $.ajax({
                        type: "GET",
                        url: "../ServiceWCF/NRequestFromService.svc/DeleteNRequestFromByNRequestFromNo?NRequestFromNo=" + NRequestFormNo,
                        dataType: "json",
                        success: function (data) {
                            if (data.BoolFlag) {
                                $.messager.alert('提示', '申请单删除成功！', 'info');
                                $('#NRequestFormList').datagrid('reload');
                            }
                            else {
                                if (data.ErrorInfo != null && data.ErrorInfo != "") {
                                    $.messager.alert('提示', '申请单删除失败！错误原因:' + data.ErrorInfo, 'error');
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
        function PrintNRequestFormList() {
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
        function DeliveryNRequestFormList() {
            var nrflist = $('#NRequestFormList').datagrid('getChecked');
            var barcodeentitylist = [];
            if (!nrflist || nrflist.length <= 0) {
                $.messager.alert('提示', '请选择要进行"物流收样"的申请单！', 'error');
                return;
            }
            $.each(nrflist, function (i, item) {
                var BarCodeFormList = item.BarCodeFormList;
                for (var i = 0; i < BarCodeFormList.length; i++) {
                    barcodeentitylist.push(BarCodeFormList[i].BarCode);
                }
            });
            //alert(barcodeentitylist.join(','));
            $.ajax({
                type: "GET",
                url: "../ServiceWCF/NRequestFromService.svc/DeliveryBarCodeFromByBarCodeList?BarCodeList=" + barcodeentitylist.join(',') + "&Flag=true",
                dataType: "json",
                success: function (data) {
                    if (data.BoolFlag) {
                        $.messager.alert('提示', '物流已收样！', 'info');

                    }
                    else {
                        if (data.ErrorInfo != null && data.ErrorInfo != "") {
                            $.messager.alert('提示', '物流收样失败！错误原因:' + data.ErrorInfo, 'error');
                        }
                    }
                }
            });
            $('#btnsearch').click();
        }
        function ReceiveNRequestFormList(Flag) {
            var nrflist = $('#NRequestFormList').datagrid('getChecked');
            if (!nrflist || nrflist.length <= 0) {
                if (Flag == "0") {
                    $.messager.alert('提示', '请选择要进行"拒收"的申请单！', 'error');
                }
                else {
                    $.messager.alert('提示', '请选择要进行"签收"的申请单！', 'error');
                }
                return;
            }
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
                            url: encodeURI("../ServiceWCF/NRequestFromService.svc/ReceiveBarCodeFromByBarCodeList?BarCodeList=" + barcodeentitylist.join(',') + "&Flag=" + strflag + "&Reason=" + Reason),
                            dataType: "json",
                            success: function (data) {
                                if (data.success) {
                                    $.messager.alert('提示', '样本已' + strreceive + '！', 'info');

                                }
                                else {
                                    if (data.ErrorInfo != null && data.ErrorInfo != "") {
                                        $.messager.alert('提示', '样本' + strreceive + '失败！错误原因:' + data.ErrorInfo, 'error');
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
                    url: encodeURI("../ServiceWCF/NRequestFromService.svc/ReceiveBarCodeFromByBarCodeList?BarCodeList=" + barcodeentitylist.join(',') + "&Flag=" + strflag + "&Reason=" + Reason),
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            $.messager.alert('提示', '样本已' + strreceive + '！', 'info');

                        }
                        else {
                            if (data.ErrorInfo != null && data.ErrorInfo != "") {
                                $.messager.alert('提示', '样本' + strreceive + '失败！错误原因:' + data.ErrorInfo, 'error');
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
                url: "../ServiceWCF/NRequestFromService.svc/CheckNRequestFromStatusByNRequestFromNo?NRequestFromNo=" + NRequestFormNo,
                dataType: "json",
                success: function (data) {
                    checkweblisflag = data.BoolFlag;
                    if (checkweblisflag) {
                        var SN = Shell.util.Path.getRequestParams()["SN"];
                        parent.OpenWindowFuc('修改申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry_taihe/apply.html?Flag=1&NRequestFormNo=' + NRequestFormNo + '&BarCodeInputFlag=1', SN);
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
    </script>
</head>
<body class="easyui-layout" data-options="fit:true,width:'auto'">
    <form id="form1" runat="server">
        <div id="mainpanel" class="easyui-layout" data-options="fit:true,border:false" style="width: 200px; height: 100%;">
            <div data-options="region:'north',iconCls:'icon-search'" title="新增|查询" style="height: 100px">
                <div id="btnaddnrequestformdiv" style="float: left; width: 129px; height: 100%; border-right: thin solid #0099CC">
                    <a id="btnaddnrequestform" href="#" style="margin-top: 10px; margin-bottom: 10px; margin-left: 10px;"
                        class="easyui-linkbutton" data-options="iconCls:'icon-add',size:'large'">新增申请单</a>
                </div>
                <div style="float: left">
                    <div style="padding: 5px;">
                        送检单位：<input id="txtClientNo" class="easyui-combobox" style="width: 218px" />
                        医&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;生：<input id="SelectDoctor" class="easyui-combobox"
                            style="width: 100px" />
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：<input id="CName" class="easyui-textbox" style="width: 100px" />
                        病&nbsp;&nbsp;历&nbsp;&nbsp;号：<input id="PatNo" class="easyui-textbox" style="width: 100px" />
                        申请号：<input id="SampleSendNo" class="easyui-textbox" style="width: 100px" />
                    </div>
                    <div style="padding: 5px;">
                        开单时间：<input id="OperateStartDateTime" class="easyui-datebox" editable='false' style="width: 100px" />
                        --
                    <input id="OperateEndDateTime" class="easyui-datebox" editable='false' style="width: 100px" />
                        采样时间：<input id="txtCollectStartDate" class="easyui-datebox" editable='false' style="width: 100px" />
                        --
                    <input id="txtCollectEndDate" class="easyui-datebox" editable='false' style="width: 100px" />
                        样本状态：
                        <select id="comboboxWebLisFlag" style="width: 155px;" class="easyui-combobox">
                            <option value="-1">全部</option>
                            <option value="0">待外送</option>
                            <option value="1">已打包</option>
                            <option value="2">已外送</option>
                            <option value="5">已签收</option>
                            <option value="6">已拒收</option>
                        </select>
                    </div>
                </div>
                <div style="float: right; vertical-align: middle; border: 1px; border-color: Black; height: 100%">
                    <a id="btnsearch" href="#" style="margin-top: 10px; margin-bottom: 10px;" class="easyui-linkbutton" data-options="iconCls:'icon-search',size:'large'">查询</a>
                    <a id="btnprint" href="#" style="margin-top: 10px; margin-bottom: 10px;" class="easyui-linkbutton" data-options="iconCls:'icon-print',size:'large'">打印清单</a>
                    <a id="btnDelivery" href="#" style="margin-top: 10px; margin-bottom: 10px;" class="easyui-linkbutton" data-options="iconCls:'icon-print',size:'large'">物流收样</a>
                    <a id="btnReceive" href="#" style="margin-top: 10px; margin-bottom: 10px;" class="easyui-linkbutton" data-options="iconCls:'icon-print',size:'large'">样本签收</a>
                    <a id="btnRejection" href="#" style="margin-top: 10px; margin-bottom: 10px;" class="easyui-linkbutton" data-options="iconCls:'icon-print',size:'large'">样本拒收</a>
                </div>
            </div>
            <div data-options="region:'center',title:'申请列表',iconCls:'icon-app-grid-16',tools:'#ToolsPanel'">
                <div id="NRequestFormList" class="easyui-datagrid" data-options="border:false,singleSelect:true,fit:true,fitColumns:true">
                </div>
            </div>
        </div>
    </form>
    <!-- 网页打印 -->
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0" pluginspage="resources/install_lodop32.exe"></embed>
    </object>
</body>
</html>
