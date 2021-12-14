<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportIntoReport.aspx.cs"
    Inherits="ZhiFang.WebLis.ExportExcel.ExportIntoReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报告结果数据导入</title>
    <script type="text/javascript" language="javascript">
        function IntoXml() {
            showFloat();
            var i = ZhiFang.WebLis.ExportExcel.ExportIntoReport.InXml();
        }
        function ShowNo() //隐藏两个层 
        {
            document.getElementById("doing").style.display = "none";
        }
        function $(id) {
            return (document.getElementById) ? document.getElementById(id) : document.all[id];
        }
        function showFloat() //根据屏幕的大小显示两个层 
        {
            var range = getRange();
            $('doing').style.width = range.width + "px";
            $('doing').style.height = range.height + "px";
            $('doing').style.display = "block";
        }
      
        function getRange() //得到屏幕的大小 
        {
            var top = document.body.scrollTop;
            var left = document.body.scrollLeft;
            var height = document.body.clientHeight;
            var width = document.body.clientWidth;

            if (top == 0 && left == 0 && height == 0 && width == 0) {
                top = document.documentElement.scrollTop;
                left = document.documentElement.scrollLeft;
                height = document.documentElement.clientHeight;
                width = document.documentElement.clientWidth;
            }
            return { top: top, left: left, height: height, width: width };
        } 
    </script>
    <link href="../Css/Default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="doing" style="filter: alpha(opacity=30); -moz-opacity: 0.3; opacity: 0.3;
        background-color: #000; width: 100%; height: 100%; z-index: 1000; position: absolute;
        left: 0; top: 0; display: none; overflow: hidden;">
        <center>
            <iframe runat="server" id="myframe" scrolling="no" style="width:300px; height:100px; position: relative; left: 50px; top: 150px;">
            </iframe>
        </center>
    </div>
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color: #0099cc">
        <tr style="background-color: #FFFFFF">
            <td align="center">
                <div style="font-size: 20px; font-weight: bold; margin: 10px">
                    报告结果数据导入</div>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <asp:Button ID="butReadXml" runat="server" Text="读取" OnClick="butReadXml_Click" />
    &nbsp;&nbsp;&nbsp;
    <%--<input id="butInXml" value="导入" type="button" onclick="IntoXml()" />--%>
    <asp:Button ID="butInXml" runat="server" Text="导入" OnClick="butInXml_Click" />
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color: #0099cc">
        <tr style="background-color: #FFFFFF">
            <td align="left" width="35%" valign="top">
                <asp:DataList ID="DataList1" runat="server" Width="100%" CellSpacing="0" CellPadding="0"
                    border="0">
                    <HeaderTemplate>
                        <table width="100%" class="tablehead" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td width="15%" align="center">
                                    文件名称
                                </td>
                                <td width="15%" align="center">
                                    是否带图
                                </td>
                                <td width="15%" align="center">
                                    图片张数
                                </td>
                                <td width="15%" align="center">
                                    导入状态
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%" height="25" bgcolor="#ffffff" cellpadding="0" cellspacing="0"
                            border="0" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onmouseout="this.bgColor='#ffffff';this.style.color='#000000'">
                            <tr>
                                <td width="15%">
                                    <asp:Label runat="server" ID="fileName" Text='<%# DataBinder.Eval(Container.DataItem, "fileName") %>' />
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="IsImage" Text='<%# DataBinder.Eval(Container.DataItem, "IsImage") %>' />
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="ImageCount" Text='<%# DataBinder.Eval(Container.DataItem, "ImageCount") %>' />
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="IsExport" Text='<%# DataBinder.Eval(Container.DataItem, "IsExport") %>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <table width="100%" height="25" bgcolor="#BFDBE1" cellpadding="0" cellspacing="0"
                            border="0" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onmouseout="this.bgColor='#BFDBE1';this.style.color='#000000'">
                            <tr>
                                <td width="15%">
                                    <asp:Label runat="server" ID="fileName" Text='<%# DataBinder.Eval(Container.DataItem, "fileName") %>' />
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="IsImage" Text='<%# DataBinder.Eval(Container.DataItem, "IsImage") %>' />
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="ImageCount" Text='<%# DataBinder.Eval(Container.DataItem, "ImageCount") %>' />
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="IsExport" Text='<%# DataBinder.Eval(Container.DataItem, "IsExport") %>' />
                                </td>
                            </tr>
                        </table>
                    </AlternatingItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
