<%@ Control Language="C#" AutoEventWireup="True" 
    Inherits="OA.DataUserControlLib.OAGridViewUserControl" Codebehind="OAGridViewUserControl.ascx.cs" %>

<script type="text/javascript">
    
        function getCurrentTime() 
        { 
            PageMethods.getCurrentTime();
        }
    
</script>

<link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

<table id="tableGridView" runat="server" width="100%" border="1">
    <tr>
        <td width="1%" bgcolor="#f1e3ff" nowrap colspan="2">
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True">控件的标题</asp:Label>
            <asp:LinkButton ID="btnNew" runat="server" CssClass="LinkButton" OnClick="btnNew_Click">新建</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td nowrap>
            <!-- 创建首页 -->
            <asp:LinkButton ID="btnFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                Enabled="false" OnClick="btnSelectLinkButtonClick">首页</asp:LinkButton>
            <!-- 创建上一页 -->
            <asp:LinkButton ID="btnPreviousPage" runat="server" CommandArgument="Prev" CommandName="Page"
                Enabled="false" OnClick="btnSelectLinkButtonClick">上一页</asp:LinkButton>
            <!-- 创建下一页 -->
            <asp:LinkButton ID="btnNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="false" OnClick="btnSelectLinkButtonClick">下一页</asp:LinkButton>
            <!-- 创建尾页 -->
            <asp:LinkButton ID="btnLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="false" OnClick="btnSelectLinkButtonClick">尾页</asp:LinkButton>
        </td>
        <td>
            <!-- 创建当前页 -->
            <asp:Label ID="Label1" runat="server" Text="第"></asp:Label>
            <asp:Label ID="lblCurrentPage" runat="server" Text="1"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="页/"></asp:Label>
            <!-- 创建总页数 -->
            <asp:Label ID="Label3" runat="server" Text="共"></asp:Label>
            <asp:Label ID="lblPageCount" runat="server" Text="0"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="页,"></asp:Label>
            <!-- 每页记录数 -->
            <asp:Label ID="Label7" runat="server" Text="每页"></asp:Label>
            <asp:Label ID="lblPageSize" runat="server" Text="20"></asp:Label>
            <asp:Label ID="Label8" runat="server" Text="条记录/"></asp:Label>
            <!-- 创建总记录数 -->
            <asp:Label ID="Label5" runat="server" Text="共"></asp:Label>
            <asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text="条记录!"></asp:Label>
            <!-- 选择每页的记录数 -->
            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="10" CommandName="PageSize"
                Enabled="true" OnClick="btnSelectLinkButtonClick">5</asp:LinkButton>
            <asp:LinkButton ID="btnPageSize10" runat="server" CommandArgument="10" CommandName="PageSize"
                Enabled="true" OnClick="btnSelectLinkButtonClick">10</asp:LinkButton>
            <asp:LinkButton ID="btnPageSize20" runat="server" CommandArgument="20" CommandName="PageSize"
                Enabled="true" OnClick="btnSelectLinkButtonClick">20</asp:LinkButton>
            <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="10" CommandName="PageSize"
                Enabled="true" OnClick="btnSelectLinkButtonClick">25</asp:LinkButton>
            <asp:LinkButton ID="btnPageSize30" runat="server" CommandArgument="30" CommandName="PageSize"
                Enabled="true" OnClick="btnSelectLinkButtonClick">30</asp:LinkButton>
            <asp:LinkButton ID="btnPageSize50" runat="server" CommandArgument="30" CommandName="PageSize"
                Enabled="true" OnClick="btnSelectLinkButtonClick">50</asp:LinkButton>
            <asp:LinkButton ID="btnPageSize100" runat="server" CommandArgument="30" CommandName="PageSize"
                Enabled="true" OnClick="btnSelectLinkButtonClick">100</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gridView1" runat="server" AutoGenerateColumns="false" BorderWidth="1px"
                OnRowDataBound="gridView1_RowDataBound" 
                onselectedindexchanged="gridView1_SelectedIndexChanged" 
                onrowdeleting="gridView1_RowDeleting" onrowediting="gridView1_RowEditing" >
            </asp:GridView>
        </td>
    </tr>
</table>
