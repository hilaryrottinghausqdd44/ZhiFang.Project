<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="GridViewUserControl.ascx.cs" Inherits="ECDS.UserControlLib.GridViewUserControl" %>

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
        <link href="<%=cssFile%>" type="text/css" rel="stylesheet"/>
    <%
    }
    %>

<script src="../Util/CommonJS.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
</script>


<table id="tableGridView" runat="server" border="0" class="ListTableContainer" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:1%;  background-color:#f1e3ff; white-space:nowrap" colspan="3">
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True">控件的标题</asp:Label>
            <asp:LinkButton ID="btnNew" Text="新建" SkinID="btnNew" runat="server" CssClass="LinkButton" OnClick="btnNew_Click"/>
            <asp:LinkButton ID="btnExport" runat="server" CssClass="LinkButton" OnClick="btnExport_Click" Text="导出数据" Visible="false" />
        </td>
    </tr>
    <tr>
        <td  style="white-space:nowrap;">
            <asp:LinkButton ID="btnDeleteBatch" Text="批量删除" runat="server"  CssClass="LinkButton" onclick="btnDeleteBatch_Click"/>
        </td>
        <td  style="white-space:nowrap;" id="tdBrowsePageButton">
            <!-- 创建首页 -->
            <asp:ImageButton ID="btnFirstPage" runat="server" CommandArgument="First" CommandName="Page" Enabled="false" OnClick="btnSelectImageButtonClick" ImageUrl="~/App_Themes/zh-cn/Images/Button/btnFirstd.gif" ToolTip="第一页"/>
            <!-- 创建上一页 -->
            <asp:ImageButton ID="btnPrevPage" runat="server" CommandArgument="Prev" CommandName="Page" Enabled="false" OnClick="btnSelectImageButtonClick" ImageUrl="~/App_Themes/zh-cn/Images/Button/btnPrevd.gif" ToolTip="上一页"/>
            <!-- 创建下一页 -->
            <asp:ImageButton ID="btnNextPage" runat="server" CommandArgument="Next" CommandName="Page" Enabled="false" OnClick="btnSelectImageButtonClick"  ImageUrl="~/App_Themes/zh-cn/Images/Button/btnNextd.gif" ToolTip="下一页"/>
            <!-- 创建尾页 -->
            <asp:ImageButton ID="btnLastPage" runat="server" CommandArgument="Last" CommandName="Page" Enabled="false" OnClick="btnSelectImageButtonClick"  ImageUrl="~/App_Themes/zh-cn/Images/Button/btnLastd.gif" ToolTip="最后一页"/>
        </td>
        <td  style="white-space:nowrap;" id="tdBrowsePageNumber">
            <!-- 创建当前页 -->
            <asp:Label ID="Label1" runat="server" Text="页数"/>
            <asp:Label ID="lblCurrentPage" runat="server" Text="1"  ToolTip="当前页码"/>
            <asp:Label ID="Label2" runat="server" Text="/"/>
            <!-- 创建总页数 -->
            <asp:Label ID="lblPageCount" runat="server" Text="0" ToolTip="总页数" />
            <asp:Label ID="Label3" runat="server" Text="每页"/>
            <!-- 每页记录数 -->
            <asp:Label ID="lblPageSize" runat="server" Text="20" Visible="false"/>
            <!-- 选择每页的记录数 -->
            <asp:LinkButton ID="btnPageSize10"  Text="10" ToolTip="每页１０条记录" runat="server" CommandName="PageSize" OnClick="btnSelectLinkButtonClick"/>
            <asp:LinkButton ID="btnPageSize20"  Text="20" ToolTip="每页２０条记录" runat="server" CommandName="PageSize" OnClick="btnSelectLinkButtonClick"/>
            <asp:LinkButton ID="btnPageSize30"  Text="30" ToolTip="每页３０条记录" runat="server" CommandName="PageSize" OnClick="btnSelectLinkButtonClick"/>
            <!-- 创建总记录数 -->
            <asp:Label ID="Label5" runat="server" Text="共"/>
            <asp:Label ID="lblRecordCount" runat="server" Text="0" ToolTip="总记录数"/>
            <asp:Label ID="Label4" runat="server" Text="条"/>
        </td>
    </tr>
    <tr>
        <td colspan="3" enableviewstate="false">
            <asp:GridView ID="gridView1" runat="server" AutoGenerateColumns="false" BorderWidth="1px"
                OnRowDataBound="gridView1_RowDataBound" 
                onselectedindexchanged="gridView1_SelectedIndexChanged" 
                onrowediting="gridView1_RowEditing" 
                onrowcommand="gridView1_RowCommand" onrowcreated="gridView1_RowCreated" 
                AllowSorting="True" onsorting="gridView1_Sorting" ShowFooter="True" 
                onrowdeleting="gridView1_RowDeleting" ondatabound="gridView1_DataBound">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="全选">
                        <ItemStyle Wrap="False" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectALL" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectALL_OnCheckedChanged"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelectOne" runat="server" AutoPostBack="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td id="tdShowErrorMessage" align="center">
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
