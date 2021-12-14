<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelManager.aspx.cs" Inherits="ZhiFang.WebLis.ReportPrint.ModelManager" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板管理</title>
    <link href="../Css/Default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/Tools.js"></script>
    <script language="javascript">
        function DelModel(id) {
            if (confirm("确定删除？")) {
                document.getElementById('tmpId').value = id;
                var o = document.getElementById('Button1');
                o.click();
            }
            else {
                return;
            }
        }
        function EditModel(id) {
            WOpenMid('ModelAdd.aspx?Id=' + id, 'ModelEdit');
        }
    </script>
</head>
<body style="width: 100%; padding: 0px; margin: 0px">
    <form id="form1" runat="server">
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color: #0099cc">
        <tr style="background-color: #FFFFFF">
            <td align="center">
                <div style="font-size: 20px; font-weight: bold; margin: 10px">
                    模板管理</div>
            </td>
            <td align="center" width="10%">
                <input type="button" value="添加" onclick="WOpenMid('ModelAdd.aspx','ModelAdd');" />
            </td>
        </tr>
        <tr style="background-color: #FFFFFF">
            <td align="center" colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Style="display: none" /><asp:TextBox
                            ID="tmpId" runat="server" Style="display: none"></asp:TextBox>
                        <asp:DataList ID="DataList1" runat="server" Width="100%" CellSpacing="1" CellPadding="0"
                            border="0">
                            <HeaderTemplate>
                                <table width="100%" class="tablehead" cellspacing="1" cellpadding="0" border="0">
                                    <tr>
                                        <td width="60" align="right">
                                            模板名称
                                        </td>
                                        <td width="160">
                                            模板存放地址
                                        </td>
                                        <td width="60">
                                            模板文件名
                                        </td>
                                        <td width="60">
                                            项目或图片数
                                        </td>
                                        <td width="60">
                                            纸张大小
                                        </td>
                                        <td width="160">
                                            模板描述
                                        </td>
                                        <td width="50">
                                            套打标志
                                        </td>
                                        <td width="50">
                                            操作
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table width="100%" height="25" bgcolor="#ffffff" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'"
                                    onmouseout="this.bgColor='#ffffff';this.style.color='#000000'" cellspacing="1"
                                    cellpadding="0" border="0">
                                    <tr title="<%# DataBinder.Eval(Container.DataItem, "PrintFormatDesc")%>">
                                        <td width="60" align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "PrintFormatName")%>
                                        </td>
                                        <td width="160">
                                            <%# DataBinder.Eval(Container.DataItem, "PintFormatAddress")%>
                                        </td>
                                        <td width="60">
                                            <%# DataBinder.Eval(Container.DataItem, "PintFormatFileName")%>
                                        </td>
                                        <td width="60">
                                            <%# DataBinder.Eval(Container.DataItem, "ItemParaLineNum")%>
                                        </td>
                                        <td width="60">
                                            <%# DataBinder.Eval(Container.DataItem, "PaperSize")%>
                                        </td>
                                        <td width="160">
                                            <%# DataBinder.Eval(Container.DataItem, "PrintFormatDesc")%>
                                        </td>
                                        <td width="50">
                                            <%# DataBinder.Eval(Container.DataItem, "BatchPrint").ToString() == "1" ? "是" : "否"%>
                                        </td>
                                        <td width="50">
                                            <a href="javascript:EditModel('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">修改</a>&nbsp;&nbsp;<a
                                                href="javascript:DelModel('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">删除</a>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table width="100%" height="25" bgcolor="#BFDBE1" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'"
                                    onmouseout="this.bgColor='#BFDBE1';this.style.color='#000000'" cellspacing="1"
                                    cellpadding="0" border="0">
                                    <tr title="<%# DataBinder.Eval(Container.DataItem, "PrintFormatDesc")%>">
                                        <td width="60" align="right">
                                            <%# DataBinder.Eval(Container.DataItem, "PrintFormatName")%>
                                        </td>
                                        <td width="160">
                                            <%# DataBinder.Eval(Container.DataItem, "PintFormatAddress")%>
                                        </td>
                                        <td width="60">
                                            <%# DataBinder.Eval(Container.DataItem, "PintFormatFileName")%>
                                        </td>
                                        <td width="60">
                                            <%# DataBinder.Eval(Container.DataItem, "ItemParaLineNum")%>
                                        </td>
                                        <td width="60">
                                            <%# DataBinder.Eval(Container.DataItem, "PaperSize")%>
                                        </td>
                                        <td width="160">
                                            <%# DataBinder.Eval(Container.DataItem, "PrintFormatDesc")%>
                                        </td>
                                        <td width="50">
                                            <%# DataBinder.Eval(Container.DataItem, "BatchPrint").ToString() == "1" ? "是" : "否"%>
                                        </td>
                                        <td width="50">
                                            <a href="javascript:EditModel('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">修改</a>&nbsp;&nbsp;<a
                                                href="javascript:DelModel('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">删除</a>
                                        </td>
                                    </tr>
                                </table>
                            </AlternatingItemTemplate>
                        </asp:DataList></ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
