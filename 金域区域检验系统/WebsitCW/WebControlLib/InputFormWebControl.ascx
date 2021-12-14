<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="OA.WebControlLib.InputFormWebControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="InputFormWebControl.ascx.cs" %>
<table width="100%" border="1" id="Table2">
    <tr>
        <td nowrap>
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle">±ÍÃ‚</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Table ID="tableInputFormControlList" runat="server" Width="100%" BorderWidth="1"
                GridLines="Both">
            </asp:Table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="±£¥Ê" OnClick="btnSave_Click"></asp:Button>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
</table>
