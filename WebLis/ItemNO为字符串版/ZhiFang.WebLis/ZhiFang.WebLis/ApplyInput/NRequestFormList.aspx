<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NRequestFormList.aspx.cs"
    Inherits="ZhiFang.WebLis.ApplyInput.NRequestFormList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>申请单列表</title>
    <link href="../ui/easyui/demo/demo.css" rel="stylesheet" type="text/css" />
    <link href="../ui/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../ui/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../EasyUI/jquery-easyui-1.4/jquery-1.11.0-vsdoc.js" type="text/javascript"></script>
    <script src="../ui/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="../ui/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../ui/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../ui/util/util.js" type="text/javascript"></script>
    <script src="../ui/util/LodopFuncs.js"></script>
    <script language="javascript">
        $(function () {
            var datetimenow = new Date();
            var datetimestr = datetimenow.getFullYear() + "-" + (datetimenow.getMonth() + 1) + "-" + datetimenow.getDate();

            $('#OperateStartDateTime').datebox('setValue', datetimestr);
            $('#OperateEndDateTime').datebox('setValue', datetimestr);
            $('#btnsearch').bind('click', function () {
                $('#NRequestFormList').datagrid({
                    url: '../ServiceWCF/NRequestFromService.svc/GetNRequestFromListByRBAC?guid=' + generateMixed(10),
                    queryParams: {
                        jsonentity: "{ClientNo:" + $('#txtClientNo').combobox('getValue')
                        + ",OperDateStart:'" + $('#OperateStartDateTime').datebox('getValue')
                        + "',OperDateEnd:'" + $('#OperateEndDateTime').datebox('getValue')
                        + "',CollectDateStart:'" + $('#txtCollectStartDate').datebox('getValue')
                        + "',CollectDateEnd:'" + $('#txtCollectEndDate').datebox('getValue')
                        + "',DoctorName:'" + $('#SelectDoctor').combobox('getValue')
                        + "',CName:'" + $('#CName').textbox('getValue')
                        + "',PatNo:'" + $('#PatNo').textbox('getValue')
                        + "'}"
                    }

                });
            });
            $('#btnaddnrequestform').bind('click', function () {
                var SN = Shell.util.Path.getRequestParams()["SN"];
                parent.OpenWindowFuc('新增申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry/apply.html?v=20170531', SN);
                //var url = "../ui/entry/apply.html";
                //window.open(url, "新增申请单", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
            });
            $('#btnprint').bind('click', function () { PrintNRequestFormList(); });
            $('#btnprintbarcode').bind('click', function () { alert('条码打印！'); });
            $('#NRequestFormList').datagrid({
                //url: '../ServiceWCF/NRequestFromService.svc/GetNRequestFromListByRBAC',
                method: 'GET',
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                fitColumns: true,
                checkOnSelect: false,
                striped: true,
                columns: [[
                //{ field: 'checkbox', title: 'checkbox', width: 150, checkbox: true },
                            {field: 'BarcodeList', title: '条码号', width: 150 },
                            { field: 'CName', title: '姓名', width: 50 },
                            { field: 'GenderName', title: '性别', width: 30, align: 'center' },
                            { field: 'Age', title: '年龄(岁)', width: 50,
                                formatter: function (value, row, index) {
                                    if (value == "200" || value == 200) {
                                        return "成人";
                                    }
                                    else {
                                        return value;
                                    }
                                }
                            },
                            { field: 'SampleTypeName', title: '样本', width: 50 },
                            { field: 'ItemList', title: '项目', width: 150, align: 'center' },
                            { field: 'DoctorName', title: '医生', width: 30 },
                            { field: 'OperTime', title: '开单时间', width: 90, align: 'center' },
                            { field: 'CollectTime', title: '采样时间', width: 90, align: 'center' },
                            { field: 'WebLisSourceOrgName', title: '送检单位', width: 80, align: 'center' },
                            { field: 'action', title: '操作', width: 50, align: 'center',
                                formatter: function (value, row, index) {
                                    var a = '<a href="javascript:editrow(\'' + row.NRequestFormNo + '\')" class="ope-save" >修改</a> ';
                                    var b = '<a href="javascript:deleterow(\'' + row.NRequestFormNo + '\')"class="ope-save">删除</a>';
                                    return a + b;
                                }
                            }
                         ]]//,
                //                queryParams: {
                //                    jsonentity: "{ClientNo:1,OperDateStart:'2014-11-01',OperDateEnd:'2014-11-27'}"
                //                },
                //                                toolbar: ['-',{
                //                                    iconCls: 'icon-edit',
                //                                    text: '修改',
                //                                    handler: function () { alert('编辑按钮') }
                //                                }, {
                //                                    iconCls: 'icon-remove',
                //                                    text: '删除',
                //                                    handler: function () { alert('编辑按钮') }
                //                                }, '-', {
                //                                    iconCls: 'icon-help',
                //                                    handler: function () { alert('帮助按钮') }
                //                                }]
                //toolbar: $('#dlg-toolbar')
            });
            $('#txtClientNo').combobox({
                url: '../ServiceWCF/DictionaryService.svc/GetClientListByRBAC?guid=' + generateMixed(10) + '&page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO&where=&sort=',
                method: 'GET',
                valueField: 'ClIENTNO',
                textField: 'CNAME',
                loadFilter: function (data) {
                    if (data.length > 0) {
                        data[0].selected = true;
                    }
                    return data;
                },
                onLoadSuccess: function () {
                    //alert($('#txtClientNo').combobox('getValue'));
                    $('#SelectDoctor').combobox({
                        url: '../ServiceWCF/DictionaryService.svc/GetPubDict?tableName=B_Lab_Doctor&fields=LabDoctorNo,CName,ShortCode&labcode=' + $('#txtClientNo').combobox('getValue'),
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
                    $('#btnsearch').click();
                },
                onChange: function (newValue, oldValue) {
                    //alert($('#txtClientNo').combobox('getValue'));
                    $('#SelectDoctor').combobox({
                        url: '../ServiceWCF/DictionaryService.svc/GetPubDict?tableName=B_Lab_Doctor&fields=LabDoctorNo,CName,ShortCode&labcode=' + newValue,
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
        function PrintNRequestFormList() {
            var ClientNo = $('#txtClientNo').combobox('getValue');
            var txtStartDate = $('#OperateStartDateTime').datebox('getValue');
            var txtEndDate = $('#OperateEndDateTime').datebox('getValue');
            var txtCollectStartDate = $('#txtCollectStartDate').datebox('getValue');
            var txtCollectEndDate = $('#txtCollectEndDate').datebox('getValue');
            var SelectDoctor = $('#SelectDoctor').combobox('getValue');
            var txtPatientID = $('#PatNo').textbox('getValue');
            var txtPatientName = $('#CName').textbox('getValue');
            var url = "PrintNRequestFormList.aspx?ClientNo=" + ClientNo + "&txtStartDate=" + txtStartDate + "&txtEndDate=" + txtEndDate + "&txtCollectStartDate=" + txtCollectStartDate + "&txtCollectEndDate=" + txtCollectEndDate + "&SelectDoctor=" + SelectDoctor + "&txtPatientID=" + txtPatientID + "&txtPatientName=" + txtPatientName;
          
            window.open(url, "申请单预览打印", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
      
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
                        parent.OpenWindowFuc('修改申请单', Math.floor(window.screen.width * 0.9), Math.floor(window.screen.height * 0.7), '../ui/entry/apply.html?Flag=1&NRequestFormNo=' + NRequestFormNo + '&BarCodeInputFlag=1', SN);
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
    <div id="mainpanel" class="easyui-layout" data-options="fit:true,border:false" style="width: 200px;
        height: 100%;">
        <div data-options="region:'north',iconCls:'icon-search'" title="新增|查询" style="height: 100px">
            <div style="float: left; width: 129px; height: 100%; border-right: thin solid #0099CC">
                <a id="btnaddnrequestform" href="#" style="margin-top: 10px; margin-bottom: 10px;
                    margin-left: 10px;" class="easyui-linkbutton" data-options="iconCls:'icon-add',size:'large'">
                    新增申请单</a>
            </div>
            <div style="float: left">
                <div style="padding: 5px;">
                    送检单位：<input id="txtClientNo" class="easyui-combobox" style="width: 200px" />
                    医 生：<input id="SelectDoctor" class="easyui-combobox" style="width: 100px" />
                    姓 名：<input id="CName" class="easyui-textbox" style="width: 100px" />
                    病 历 号：<input id="PatNo" class="easyui-textbox" style="width: 100px" /></div>
                <div style="padding: 5px;">
                    开单时间：<input id="OperateStartDateTime" class="easyui-datebox" editable='false' style="width: 100px" />
                    --
                    <input id="OperateEndDateTime" class="easyui-datebox" editable='false' style="width: 100px" />
                    采样时间：<input id="txtCollectStartDate" class="easyui-datebox" editable='false' style="width: 100px" />
                    --
                    <input id="txtCollectEndDate" class="easyui-datebox" editable='false' style="width: 100px" />
                </div>
            </div>
            <div style="float: right; vertical-align: middle; border: 1px; border-color: Black;
                height: 100%">
                <a id="btnsearch" href="#" style="margin-top: 10px; margin-bottom: 10px;" class="easyui-linkbutton"
                    data-options="iconCls:'icon-search',size:'large'">查询</a> <a id="btnprint" href="#"
                        style="margin-top: 10px; margin-bottom: 10px;" class="easyui-linkbutton" data-options="iconCls:'icon-print',size:'large'">
                        打印清单</a> <a id="btnprintbarcode" href="#" style="margin-top: 10px; margin-bottom: 10px;
                            margin-right: 10px; display: none" class="easyui-linkbutton" data-options="iconCls:'icon-barcode',size:'large'">
                            打印条码</a>
            </div>
        </div>
        <div data-options="region:'center',title:'申请列表',iconCls:'icon-app-grid-16',tools:'#ToolsPanel'">
            <div id="NRequestFormList" class="easyui-datagrid" data-options="border:false,singleSelect:true,fit:true,fitColumns:true">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
