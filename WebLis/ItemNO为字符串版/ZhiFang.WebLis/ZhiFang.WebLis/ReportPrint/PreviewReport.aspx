<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewReport.aspx.cs"
    Inherits="ZhiFang.WebLis.ReportPrint.PreviewReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报告查询预览</title>
    <meta name="keywords" content="jquery,ui,easy,easyui,web">
    <meta name="description" content="easyui help you build your web page easily!">
    <script src="../jquery-easyui-1.3/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.3/easyloader.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/WindowLocationSize.js"></script>
    <script src="../JS/Tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        function londingImg(rowUrl,Cno) {
            $('#ReportImg').attr("src", rowUrl);
            var Lyuan = ZhiFang.WebLis.Ashx.ReportShare.Lyuan(Cno);
            $("#Lyuan").html();
            $("#Lyuan").html(Lyuan.value);
        }
        function onSel() {
            //var file_url = ZhiFang.WebLis.Ashx.ReportShare.File_url();
            $('#ReportImg').attr("src", "");
            var strWhere = "";
//          var PAT_ID = $('#iptPat_id').val();
            var Card_ID = $('#iptCard_ID').val();
            var Report_TimeStar = $('#star').datebox('getValue');
            var Report_TimeStop = $('#stop').datebox('getValue');
            var Name = $('#Name').val();
            var Age = $('#Age').val();
            var Sex = $('#Sex').find("option:selected").text();
            var ChangeStatus = 0;
            easyloader.load('datagrid', function () {
                $('#dg').datagrid({
                    singleSelect: true,
                 
                    rownumbers: true,
                    fit: true,
                    url: '../Ashx/ReportShare.ashx?type=1',
                    queryParams: {
//                        PAT_ID: PAT_ID,
                        Card_ID: Card_ID,
                        Report_TimeStar: Report_TimeStar,
                        Report_TimeStop: Report_TimeStop,
                        Name: Name,
                        Age: Age,
                        Sex: Sex,
                        ChangeStatus: ChangeStatus
                    },
                    pagination: true,
                    columns: [[
        { field: 'Report_Time', title: '报告审核时间' },
        { field: 'Name', title: '姓名' },
        { field: 'Card_ID', title: '身份证号' },
        { field: 'Age', title: '年龄' },
        { field: 'Sex', title: '性别' }
    ]], onClickRow: function ss(target) {
        var t = $('#dg');
        var row = t.datagrid('getSelected');
        londingImg(row.File_Url,row.Medical_Institution_Code);
    },
                    pageList: [15, 20, 30],
                    pageSize: 15
                });
            });

        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        a{
            
            text-decoration: none;
        }
        a:link{
        color : Black;
        }
        a:visited{
            color : #A4A5AA;
        }
        a:active{
            background-color: yellow;
        }
        
      
    </style>
</head>
<body style="margin: 0px 0px 0px 0px; overflow: hidden">
    <form id="form1" runat="server">
    <%--调用医院ID=123&报告医院ID=123&被检验者万达的PAT_ID =123&加密验证字符串=1234567890&报告开始时间=2013-12-01&报告结束时间=2013-12-01--%>
    <div id="cc" class="easyui-layout" style="width: 100%; height: 680px;">
        <div data-options="region:'north',title:'查询条件',split:true" style="height: 100px;">
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <%--<td>
                        万达PAT_ID:
                    </td>
                    <td>
                        <input id="iptPat_id" type="text" value="" />
                    </td>--%>
                    <td>
                        身份证号:
                    </td>
                    <td>
                        <input id="iptCard_ID" type="text" value="" />
                    </td>
                    <td>
                        报告审核开始时间:
                    </td>
                    <td>
                        <input id="star" type="text" class="easyui-datebox" required="required" />
                    </td>
                    <td>
                        报告审核结束时间:
                    </td>
                    <td>
                        <input id="stop" type="text" class="easyui-datebox" required="required" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                       &nbsp;
                    </td>
                    <td>
                       <a href="../Login.aspx"><b>返回门户</b></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        姓名:
                    </td>
                    <td>
                        <input id="Name" type="text" />
                    </td>
                    <td>
                        年龄:
                    </td>
                    <td>
                        <input id="Age" type="text" />
                    </td>
                    <td>
                        性别:
                    </td>
                    <td>
                        <select id="Sex">
                            <option>全部</option>
                            <option>男</option>
                            <option>女</option>
                        </select>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <input id="btnSel" type="button" onclick="onSel()" value="查询" onclick="return btnSel_onclick()" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            &nbsp;
            <br />
        </div>
        <%--<div data-options="region:'south',title:'South Title',split:true" style="height:100px;"></div>  --%>
        <%--<div data-options="region:'east',iconCls:'icon-reload',title:'East',split:true" style="width:100px;"></div> --%>
        <%--被检验者万达的PAT_ID;身份证号;门诊（就诊卡号）/住院（住院号）/体检（体检编号）;病人姓名;年龄;性别;手机号;报告时间;就诊医疗机构编码--%>
        <div data-options="region:'west',title:'报告列表',split:true" style="width: 320px; overflow:hidden;">
            <table id="dg" border="false">
                <thead>
                    <tr>
                        <th data-options="field:'Report_Time',width:60">
                            报告审核时间
                        </th>
                        <th data-options="field:'Name',width:50">
                            姓名
                        </th>
                        <th data-options="field:'Name',width:80">
                            身份证号
                        </th>
                        <th data-options="field:'Name',width:50">
                            年龄
                        </th>
                        <th data-options="field:'Name',width:50">
                            性别
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="pp" style="background: #efefef; border: 1px solid #ccc;">
            </div>
        </div>
        <div data-options="region:'center',title:'报告预览'" style="padding: 5px; background: #eee;">
        <div style=" position:fixed; background-color:#F2F8F2;"><b style=" font-size:24;">注：本报告来源于"<span id="Lyuan"></span>"。下载报告单，请右键另存为! </b></div>
            <img id="ReportImg" alt="" src="" /><br/>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //    var time = new Date;
    //    var month = time.getMonth() + 1
    //    time = time.getFullYear() + '-' + month + '-' + time.getDate();
    //    $('#star').val(time);
    //    $('#stop').val(time);
    easyloader.locale = "zh_CN";
    easyloader.load('datagrid', function () {
        $('#dg').datagrid({
            //            url: '../Ashx/ReportShare.ashx',
            fitColumns: true,
            columns: [[
               { field: 'Report_Time', title: '报告审核时间' },
        { field: 'Name', title: '姓名' },
        { field: 'Card_ID', title: '身份证号' },
        { field: 'Age', title: '年龄' },
        { field: 'Sex', title: '性别' }
    ]]
            //    , onClickRow: function ss(target) {
            //        var t = $('#dg');
            //        var row = t.datagrid('getSelected');
            //        //alert(row.Name);
            //        console.log(row.File_Url);
            //        londingImg(row.File_Url);
            //    }
        });
    });
    function btnSel_onclick() {

    }

</script>
