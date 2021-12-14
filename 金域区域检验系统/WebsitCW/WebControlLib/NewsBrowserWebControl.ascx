<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="OA.WebControlLib.NewsBrowserWebControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="NewsBrowserWebControl.ascx.cs" %>
<link href="CSS/WebControlDefault.css" rel="stylesheet" type="text/css" />
<table id="table1" width="100%" border="0" runat="server">
    <tr>
        <td>
            <asp:Label ID="lblTitle" Visible="False" CssClass="LabelTitle" runat="server"></asp:Label>
            <asp:Label ID="lblPageSize" Visible="False" CssClass="LabelTitle" runat="server">20</asp:Label>
            <asp:Label ID="lblCatagory" Visible="False" CssClass="LabelTitle" runat="server"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid ID="dataGridShowDataInfo" runat="server" SelectedItemStyle-CssClass="DataList">
                <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#EEEEEE" CssClass="DataListInner"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084" CssClass="DataListColumn">
                </HeaderStyle>
            </asp:DataGrid>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
</table>
