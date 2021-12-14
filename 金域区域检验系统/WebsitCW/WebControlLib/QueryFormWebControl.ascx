<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="OA.WebControlLib.QueryFormWebControl" Codebehind="QueryFormWebControl.ascx.cs" %>
<table width="100%" border="1" id="Table2">
    <tr>
        <td>
            <asp:Table ID="tableQueryControlList" runat="server" Width="100%" BorderWidth="1"
                GridLines="Both">
            </asp:Table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Button ID="btnQuery" runat="server" Text="È·¶¨" OnClick="btnQuery_Click"></asp:Button>
            <asp:Button ID="btnReset" runat="server" Text="ÖØÖÃ" OnClick="btnReset_Click"></asp:Button>
        </td>
    </tr>
</table>
