<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleMonitoring.aspx.cs"
    Inherits="ZhiFang.WebLis.ApplyInput.SampleMonitoring" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>样本流程监控</title>
    <meta name="keywords" content="jquery,ui,easy,easyui,web" />
    <meta name="description" content="easyui help you build your web page easily!" />
    <script charset="UTF-8" src="../jquery-easyui-1.3/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.3/easyloader.js" type="text/javascript"></script>
    <script type="text/javascript">
        easyloader.locale = "zh_CN";
        easyloader.load('datagrid', function () {
            $('#SampleList').datagrid({
                url: '../Ashx/DropDownList.ashx?type=TypeList',
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                pageNumber: 1,
                pageSize: 13,
                pageList: [13, 20, 30],
                columns: [[
                { field: 'BarCode', title: '条码号', width: 100 },
                { field: 'CName', title: '姓名', width: 100 },
                { field: 'Sex', title: '性别', width: 100 },
                { field: 'TestItemCName', title: '项目名称', width: 200 },
                { field: 'DoctorName', title: '医生', width: 100 },
                { field: 'ClientName', title: '送检单位', width: 150 },
                { field: 'SampleName', title: '样本类型', width: 100 },
                 { field: 'WebLisSourceOrgName', title: '检验单位', width: 150 }
                ]], onClickRow: function ss(target) {
                    var t = $('#SampleList');
                    var row = t.datagrid('getSelected');
                    if (row != null && row != "") {
                        QueryTime(row.BarCode);
                    } else { QueryTime(0); }
                }, onLoadSuccess: function (target) {
                    $("#SampleList").datagrid("selectRow", 0);
                    var t = $('#SampleList');
                    var row = t.datagrid('getSelected');
                    if (row != null && row != "") {
                        QueryTime(row.BarCode);
                    } else { QueryTime(0); }
                }
            });
        });
 

    function Search() {
        $('#SampleList').datagrid('load', {
            txtClientNo: $('#txtClientNo').combobox('getValue'),
            txtBarCode: $('#txtBarCode').val(),
            txtPatNo: $('#txtPatNo').val(),
            txtCName: $('#txtCName').val(),
            txtStartDate: $('#txtStartDate').datebox('getValue'),
            txtEndDate: $('#txtEndDate').datebox('getValue')
        });


    }
    
    function QueryTime(code) {
        easyloader.load('datagrid', function () {
            $('#SampleTime').datagrid({
                url: '../Ashx/DropDownList.ashx?type=TypeTime',
                queryParams: {
                    BarCode: code
                },
                columns: [[
                { field: 'OperTime', title: '医嘱开单时间', width: 117 },
                { field: 'CollectTime', title: '采样时间', width: 117 },
                { field: 'incepttime', title: '签收时间', width: 117 },
                { field: 'TESTTIME', title: '上机检验时间', width: 117 },
                { field: 'CHECKDATE', title: '审核时间', width: 117 }
                ]]
            });
        });
    }
    </script>
    <style type="text/css">
        th
        {
            text-align: left;
        }
    </style>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div data-options="region:'north',split:false" style="height: 55px;">
        <table id="SampleTime" class="easyui-datagrid">
            <thead>
                <tr>
                    <th data-grinds="field:'OperTime',width:117">
                        医嘱开单时间
                    </th>
                    <th data-grinds="field:'CollectTime',width:117">
                        采样时间
                    </th>
                    <th data-grinds="field:'incepttime',width:117">
                        签收时间
                    </th>
                    <th data-grinds="field:'TESTTIME',width:117">
                        上机检验时间
                    </th>
                    <th data-grinds="field:'CHECKDATE',width:117">
                        审核时间
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div data-options="region:'center'">
        <table class="tablefrom datagrid-toolbar" width="100%" cellspacing="0" cellpadding="0"
            border="0">
            <tr>
                <th>
                    送检单位
                </th>
                <td>
                    <input id="txtClientNo" style="width: 155px;" class="easyui-combobox" data-options="valueField:'id',textField:'text',url: '../Ashx/DropDownList.ashx?type=TypeCli'" />
                </td>
                <th>
                    姓名
                </th>
                <td>
                    <input id="txtCName" />
                </td>
                <th>
                    条码号
                </th>
                <td>
                    <input type="text" id="txtBarCode" />
                </td>
            </tr>
            <tr>
                <th>
                    病历号
                </th>
                <td>
                    <input id="txtPatNo" />
                </td>
                <th>
                    开单时间
                </th>
                <td>
                    <input id="txtStartDate" style="width: 155px;" class="easyui-datebox" />
                </td>
                <th>
                    至
                </th>
                <td>
                    <input id="txtEndDate" style="width: 155px;" class="easyui-datebox" />
                </td>
                <td>
                    <a href="#" class="easyui-linkbutton" onclick="Search();">查询</a>
                </td>
            </tr>
        </table>
        <table id="SampleList">
        </table>
    </div>
    </form>
</body>
</html>
