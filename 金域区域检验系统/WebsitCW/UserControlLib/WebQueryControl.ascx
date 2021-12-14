<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="WebQueryControl.ascx.cs" Inherits="ECDS.UserControlLib.WebQueryControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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

    <tr>
        <td>
            <asp:Table ID="tableQueryControlList" runat="server" Width="100%" BorderWidth="1" GridLines="Both">
            </asp:Table>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td align="left">
            <asp:Button ID="btnQuery" runat="server" Text="²éÑ¯" OnClick="btnQuery_Click"></asp:Button>
            <asp:Button ID="btnReset" runat="server" Text="ÖØÖÃ" OnClick="btnReset_Click"></asp:Button>
            <asp:Button ID="btnSave" runat="server" Text="±£´æ" OnClick="btnSave_Click"></asp:Button>
        </td>
    </tr>
</table>
