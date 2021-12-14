<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="STATQueryFormWebControl.ascx.cs" Inherits="ECDS.UserControlLib.STATQueryFormWebControl" %>
<%
    if (cssFile.Trim() == "")
    {
%>
<link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />
<%
    }
    else
    {
%>
<link href="<%=cssFile%>" type="text/css" rel="stylesheet" />
<%
    }
%>
<table>
    <tr>
        <td>
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True" Text="查询" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Table ID="tableQueryControlList" runat="server" Width="100%" BorderWidth="1" GridLines="Both">
            </asp:Table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
            <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
        </td>
    </tr>
    <div id="divSaveAS" runat="server">
        <tr>
            <td>
                <table border="1" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="text-align: right; vertical-align: middle; white-space: nowrap; width: 1%">
                            <asp:Label ID="lblSaveAs" runat="server" Text="要保存统计图分组名称为:" />
                        </td>
                        <td style="text-align: left; vertical-align: middle; width: 30%">
                            <asp:TextBox ID="txtSaveAs" runat="server" Width="98%" />
                        </td>
                        <td style="text-align: right; vertical-align: middle; white-space: nowrap; width: 1%">
                            <asp:Label ID="lblSaveAsMemo" runat="server" Text="备注:" />
                        </td>
                        <td style="text-align: left; vertical-align: middle; width: 30%">
                            <asp:TextBox ID="txtSaveAsMemo" Rows="4" runat="server" Height="76px" TextMode="MultiLine" Width="98%" Wrap="False" />
                        </td>
                        <td style="text-align: center; vertical-align: middle; white-space: nowrap; width: 1%">
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </div>
</table>
