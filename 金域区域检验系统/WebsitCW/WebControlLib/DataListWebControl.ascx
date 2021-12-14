<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="OA.WebControlLib.DataListWebControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="DataListWebControl.ascx.cs" %>
<link href="CSS/WebControlDefault.css" rel="stylesheet" type="text/css" />   
<table id="Table2" width="100%" border="1">
    <tr>
        <td width="1%" bgcolor="#f1e3ff" nowrap>
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True"></asp:Label>
        </td>
        <td bgcolor="#f1e3ff" nowrap>
            <asp:LinkButton ID="btnNew" runat="server" CssClass="LinkButton" OnClick="btnNew_Click">新建</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:DataGrid ID="dataGridShowDataInfo" ShowHeader="True" BorderWidth="1" runat="server"
                SelectedItemStyle-CssClass="DataList" OnItemCommand="dataGridShowDataInfo_ItemCommand"
                OnSelectedIndexChanged="dataGridShowDataInfo_SelectedIndexChanged">
                <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#EEEEEE" CssClass="DataListInner"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084" CssClass="DataListColumn">
                </HeaderStyle>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:LinkButton ID="btnBatchDelete" runat="server" Enabled="False" Visible="False">批量删除</asp:LinkButton>
            <asp:LinkButton ID="btnLastPage" runat="server" CssClass="LinkButton" Enabled="False"
                Visible="True" OnClick="btnLastPage_Click">上一页</asp:LinkButton>
            <asp:LinkButton ID="btnNextPage" runat="server" CssClass="LinkButton" Visible="True"
                OnClick="btnNextPage_Click">下一页</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblDatabaseName" runat="server" CssClass="LabelTitle" Visible="False"></asp:Label>
            <asp:Label ID="lblTableName" runat="server" CssClass="LabelTitle" Visible="False"></asp:Label>
            <asp:Label ID="lblPageSize" runat="server" CssClass="LabelTitle" Visible="False">20</asp:Label>
            <asp:Label ID="lblPageStart" runat="server" CssClass="LabelTitle" Visible="False">0</asp:Label>
            <asp:Label ID="lblConditionSQL" runat="server" CssClass="LabelTitle" Visible="False"></asp:Label>
            <asp:Label ID="lblSelectFieldSQL" runat="server" CssClass="LabelTitle" Visible="False"></asp:Label>
            <asp:Label ID="lblSelectSQL" runat="server" CssClass="LabelTitle" Visible="False"></asp:Label>
            <asp:Label ID="lblSQL" runat="server" CssClass="LabelTitle" Visible="False"></asp:Label>
        </td>
    </tr>
</table>
