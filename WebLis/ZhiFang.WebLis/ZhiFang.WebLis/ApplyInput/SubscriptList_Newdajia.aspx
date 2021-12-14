<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptList_Newdajia.aspx.cs"
    Inherits="ZhiFang.WebLis.ApplyInput.SubscriptList_Newdajia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>申请单列表</title>
    <link href="../CSS/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <meta name="keywords" content="jquery,ui,easy,easyui,web" />
    <meta name="description" content="easyui help you build your web page easily!" />
    <script src="../jquery-easyui-1.3/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.3/easyloader.js" type="text/javascript"></script>
    <script src="../JS/jquery.autocomplete.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //转到编辑页面 (可整理)
        function OpenInsertPage(flag) {
            //flag == '0' 表示录入新病历单
            //flag == '1' 表示修改原病历单
            var url = " ApplyInput_Weblis_Newdajia.aspx?flag=0&BarCodeInputFlag=" + getQueryString("BarCodeInputFlag");
            if (flag == 1) {
                
            }

            window.open(url, "申请单维护", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
        }
        function edit(e) {
            url = "ApplyInput_Weblis_Newdajia.aspx?Flag=1&BarCodeNo=" + e + "&BarCodeInputFlag=" + getQueryString("BarCodeInputFlag");
            window.open(url, "申请单维护", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
        }
        function getQueryString(name) {
            var urlall = location.href;
            var urla = urlall.split('?');
            var paramat;
            if (urla.length > 1) {
                var para = urla[1].split('&');
                for (var i = 0; i < para.length; i++) {
                    paramat = para[i].split('=');
                    if (paramat[0] == name) {
                        return paramat[1];
                    }
                }
            }
        }
        //输入单位名称过滤送检单位（后期可提取）
        $(document).ready(function () {
            document.getElementById("txtClientNo").focus();
            $(document).keydown(function (event) {
                var which = event.which;
                if (event.altKey && event.which == 73) {
                    OpenInsertPage(0);
                    //alert('按键是：' + which);
                }
            });
            easyloader.locale = "zh_CN";
            $("#txtClientNo").autocomplete('SubscriptList_Newdajia.aspx?k=1', {
                multiple: true,
                parse: function (data) {
                    return $.map(eval(data), function (row) {
                        return {
                            data: row,
                            value: row.ClIENTNO,
                            result: row.CNAME
                        }
                    });
                },
                formatItem: function (item) {
                    return item.CNAME;
                }
            }).result(function (event, data, formatted) {
                $("#pro").val(data.CNAME + '@' + data.ClIENTNO);
            });
        });



        //删除申请单（后期可提取）
        function del(aa) {
            if (confirm("删除是不可恢复的，你确认要删除吗？")) {
                $.ajax({
                    url: '../Ashx/ApplyNewInput.ashx?row=' + aa + '&type=delete',
                    //加了个type，作用是以后不管什么删除，都可以转到这个ashx中处理  
                    success: function (i) {
                        if (i == "err") {
                            alert("该申请单信息已被签收，不能删除！");
                            //$('#dg').datagrid('eload');
                        }
                        else {
                            alert("删除成功");
                            onSel();
                        }
                    }
                });
            }
        }
        //查询功能
        function onSel() {
            var txtClientNo = $('#pro').val();
            var txtPatientName = $('#txtPatientName').val();
            var SelectDoctor = $('#SelectDoctor').val();
            var txtPatientID = $('#txtPatientID').val();
            var txtStartDate = $('#txtStartDate').datebox('getValue');
            var txtEndDate = $('#txtEndDate').datebox('getValue');
            var txtCollectStartDate = $('#txtCollectStartDate').datebox('getValue');
            var txtCollectEndDate = $('#txtCollectEndDate').datebox('getValue')
            var chkOnlyNoPrintBarCode = $('#chkOnlyNoPrintBarCode').val();
            easyloader.load('datagrid', function () {
                $('#dg').datagrid({
                    singleSelect: true,
                    rownumbers: true,
                    fit: true,
                    url: '../Ashx/ApplyNewInput.ashx',
                    queryParams: {
                        txtClientNo: txtClientNo,
                        txtPatientName: txtPatientName,
                        SelectDoctor: SelectDoctor,
                        txtPatientID: txtPatientID,
                        txtStartDate: txtStartDate,
                        txtEndDate: txtEndDate,
                        txtCollectStartDate: txtCollectStartDate,
                        txtCollectEndDate: txtCollectEndDate,
                        chkOnlyNoPrintBarCode: chkOnlyNoPrintBarCode
                    },
                    pagination: true,
                    columns: [[
        { field: 'BarCode', title: '条码号' },
        { field: 'CName', title: '姓名' },
        { field: 'Sex', title: '性别' },
        { field: 'Age', title: '年龄' },
        { field: 'SampleName', title: '样本' },
        { field: 'ItemName', title: '项目', width: 200 },
        { field: 'DoctorName', title: '医生' },
        { field: 'OperTime', title: '开单时间' },
        { field: 'CollectTime', title: '采样时间' },
        { field: 'WebLisSourceOrgName', title: '送检单位', width: 120 },
        { field: 'ClientName', title: '站点名称', width: 130 },
        { field: 'Diag', title: '诊断' },
        { field: 'opt', title: '操作', width: 90, align: 'center',
            formatter: function (value, rec, index) {
                var e = '<a href="#" mce_href="#" onclick="edit(\'' + rec.BarCode + '\')">修改</a> ';
                var d = '<a href="#" mce_href="#" onclick="del(\'' + rec.BarCode + '\')">删除</a> ';
                return e + d;
            }
        }

    ]], onClickRow: function ss(target) {
        var t = $('#dg');
        var row = t.datagrid('getSelected');
    },
                    pageList: [15, 20, 30],
                    pageSize: 15,
                    nowrap: false
                });
            });

        }
    </script>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: 100%;
        }
        #txtClientNo
        {
            width: 349px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="cc" class="easyui-layout" fit="true" style="height: 500px;">
        <div data-options="region:'north',title:'查询条件',split:true" style="height: 130px;
            overflow: hidden;">
            <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 5px;">
                       <tr>
            <td>
             <a onclick="OpenInsertPage(0);" class="easyui-linkbutton" id="buton">录入[I]</a>
            <%--<input type="button" name="Input" accesskey="I" onclick="OpenInsertPage(0);" value="录入[I]" />--%>
            </td>
            </tr>
                <tr>
                    <td>
                        送检单位:
                    </td>
                    <td colspan="3">
                        <input id="txtClientNo" runat="server" tabindex="1" type="text" />
                        <input runat="server" id="pro" name="pro" style="display: none;" />
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
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        开单时间:
                    </td>
                    <td>
                        <input id="txtStartDate" editable=false type="text" tabindex="5" class="easyui-datebox" />
                    </td>
                    <td>
                        —
                    </td>
                    <td>
                        <input id="txtEndDate" editable=false type="text" tabindex="6" class="easyui-datebox" />
                    </td>
                    <td>
                        采样时间:
                    </td>
                    <td>
                        <input id="txtCollectStartDate" editable=false class="easyui-datebox" tabindex="7" type="text" />
                    </td>
                    <td>
                        —
                    </td>
                    <td>
                        <input id="txtCollectEndDate" editable=false class="easyui-datebox" tabindex="8" type="text" />
                    </td>
                    <td><a onclick="onSel();" class="easyui-linkbutton">查询</a>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        姓名:
                    </td>
                    <td>
                        <input id="txtPatientName" tabindex="2" type="text" />
                    </td>
                    <td>
                        医生:
                    </td>
                    <td>
                        <input id="SelectDoctor" tabindex="3" type="text" />
                    </td>
                    <td>
                        病历号:
                    </td>
                    <td>
                        <input id="txtPatientID" tabindex="4" type="text" />
                    </td>
                    <td colspan="3">
                        <input id="chkOnlyNoPrintBarCode" type="checkbox" />仅未打印条码 &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div data-options="region:'center',title:'申请单信息'" style="width: 100%;">
            <table class="easyui-datagrid" rownumbers="true" id="dg" border="false">
                <thead>
                    <tr>
                        <th data-options="field:'Report_Time',width:60">
                            条码号
                        </th>
                        <th data-options="field:'Name',width:60">
                            姓名
                        </th>
                        <th data-options="field:'Name',width:60">
                            性别
                        </th>
                        <th data-options="field:'Name',width:50">
                            年龄
                        </th>
                        <th data-options="field:'Name',width:50">
                            样本
                        </th>
                        <th data-options="field:'Name',width:200">
                            项目
                        </th>
                        <th data-options="field:'Name',width:60">
                            医生
                        </th>
                        <th data-options="field:'Name',width:60">
                            开单时间
                        </th>
                        <th data-options="field:'Name',width:60">
                            采样时间
                        </th>
                        <th data-options="field:'Name',width:60">
                            站点名称
                        </th>
                        <th data-options="field:'Name',width:60">
                            诊断
                        </th>
                        <th data-options="field:'Name',width:50">
                            修改
                        </th>
                        <th data-options="field:'Name',width:50">
                            删除
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
