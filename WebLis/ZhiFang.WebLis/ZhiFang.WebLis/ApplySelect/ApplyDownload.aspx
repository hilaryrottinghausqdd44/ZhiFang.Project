<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="ApplyDownload.aspx.cs"
    Inherits="ZhiFang.WebLis.ApplySelect.ApplyDownload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>申请下载</title>
    <script src="../jquery-easyui-1.3/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.3/easyloader.js" type="text/javascript"></script>
    <style type="text/css">
        a {font-size:12px} 

        a:link {color: blue; text-decoration:none;} 

        a:active:{color: red; } 

        a:visited {color:purple;text-decoration:none;}

        a:hover {color: red; text-decoration:underline;} 

        .listTable
        {
            width: 100%;
            border: 1px solid #add9c0;
            border-width: 1px 0px 0px 1px;
        }
        .conditions
        {
            width: 100%;
        }
        .listTable.td
        {
            border: solid #add9c0;
            border-width: 0px 1px 1px 0px;
            padding: 10px 0px;
        }
        #SendUnit
        {
            margin-bottom: 0px;
        }
    </style>
    <script type="text/javascript">
        easyloader.locale = "zh_CN";
        $(function () {
           // easyloader.load('combotree', function () {
//                $('#ItemNo').combotree({
//                    url: '../Ashx/ApplyDownload.ashx',
//                    editable: false,
//                    checkbox:true,
//                    onClick: function (node) {
//                        JJ.Prm.GetDepartmentUser(node.id, 'selUserFrom');
//                    }, //全部折叠
//                    onLoadSuccess: function (node, data) {
//                        $('#ItemNo').combotree('tree').tree("collapseAll");
//                    }
//                });
           // });
        });

        function onSel() {
            var strWhere = "";
            var adminid = $("#item").combotree("getValues");
            $("#txt").attr("value",adminid);
            var star = $('#star').datebox('getValue');
            var stop = $('#stop').datebox('getValue');
            var SendUnit = $('#SendUnit').val();
            var PickUnit = $('#PickUnit').val();
            var ParItemNo = $('#txt').val();
            var strObj = "";
            easyloader.load('datagrid', function () {
                $('#dg').datagrid({
                    valueField: 'ItemNo',
                    textField: 'Cname',
                    singleSelect: true,
                    rownumbers: true,
                    fit: true,
                    url: '../Ashx/ApplyDownload.ashx',
                    queryParams: {
                        SendUnit: SendUnit,
                        PickUnit: PickUnit,
                        star: star,
                        stop: stop,
                        ParItemNo: ParItemNo,
                        strObj:1
                    },
                    pagination: true,
                    //                    idField: 'itemid',
                    columns: [[
                    //         { field: 'checked', formatter: function (value, row, index) {
                    //             if (row.checked) {
                    //                 return '<input type="checkbox" name="DataGridCheckbox" checked="checked">';
                    //             }
                    //             else {
                    //                 return '<input type="checkbox" name="DataGridCheckbox">';
                    //             }
                    //         }
                    //         },
                    //        { field:'ck',checkbox:true},
        {field: 'ClientName', title: '送检单位', width: 150, align: 'center' },
        { field: 'SerialNo', title: '预制条码', width: 150, align: 'center' },
        { field: 'OldSerialNo', title: '医院条码', width: 150, align: 'center' },
        { field: 'CName', title: '姓名', width: 150, align: 'center' },
        { field: 'GenderName', title: '性别', width: 150, align: 'center' },
        { field: 'Age', title: '年龄', width: 150, align: 'center' },
                    //        { field: 'SampleTypeNo', title: '样本类型' },
        {field: 'ParItemName', title: '项目名称', width: 150, align: 'center' },
        { field: 'OperDate', title: '操作日期', width: 150, align: 'center' }
    ]],
                    pageList: [15, 20, 30],
                    pageSize: 15
                });
            });

        }
        function onDown() {
//            $('#ItemNo').combotree('setValues', [1, 3, 21]);
           var o = document.getElementById('dowload').click();
        }
    </script>
</head>
<body style="margin: 0px 0px 0px 0px; overflow: hidden">
    <form id="form1" runat="server">
    <div>
        <div id="cc" class="easyui-layout" style="width: 100%; height: 680px;">
            <div data-options="region:'north',title:'查询条件',split:true" style="height: 100px;">
                <table cellpadding="0" cellspacing="0" class="conditions">
                    <tr>
                        <td>
                            送检单位:
                        </td>
                        <td>
                            <asp:DropDownList ID="SendUnit"  Width="180px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            接检单位:
                        </td>
                        <td>
                            <asp:DropDownList ID="PickUnit" Width="180px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            化验项目:
                        </td>
                        <td>
                            <select id="item" class="easyui-combotree" data-options="url:'../Ashx/ApplyDownload.ashx',method:'get'" multiple style="width:200px;"></select>
                        </td>
                        <td>
                         
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            操作起止日期:
                        </td>
                        <td>
                            <input id="star" type="text" runat="server" class="easyui-datebox"/>
                        </td>
                        <td>
                            操作截止日期:
                        </td>
                        <td>
                            <input id="stop" type="text" runat="server" class="easyui-datebox"/>
                        </td>
                        <td>
                           
                        </td>
                        <td>
                            &nbsp;
                            <a id="btnSel" onclick="onSel()" class="easyui-linkbutton" href="#" >查询</a>
                            <asp:LinkButton ID="dowload" class="easyui-linkbutton" runat="server" Visible="true" OnClick="dowload_Click">下载</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                &nbsp;
                <br />
            </div>
            <div data-options="region:'center',title:'申请列表',split:true" style="overflow: hidden;">
                <table id="dg" border="false" cellspacing="1" class="listTable">
                </table>
            </div>
        </div>
        <input id="txt"  runat="server" type="text" />
    </form>
</body>
</html>
