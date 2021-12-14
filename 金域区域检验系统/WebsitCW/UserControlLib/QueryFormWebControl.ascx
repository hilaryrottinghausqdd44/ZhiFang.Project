<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryFormWebControl.ascx.cs" Inherits="ECDS.UserControlLib.QueryFormWebControl" %>
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

<script type="text/jscript" language="javascript">
</script>

<table id="tableParent" runat="server" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True" Text="查询" />
        </td>
    </tr>
    <tr class="Query11">
        <td>
            <asp:Table ID="tableQueryControlList" runat="server" GridLines="Both" CssClass="GridView" Width="100%" EnableViewState="false" />
        </td>
    </tr>
    <tr class="Button">
        <td align="left">
            <asp:Button ID="btnQuery" runat="server" Text="确定" OnClick="btnQuery_Click" />
            <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
            <asp:Image runat="server" ID="imageConvert" ImageUrl="~/App_Themes/zh-cn/Images/Arrow/转换.gif" />
            <asp:Button ID="btnQueryALL" ForeColor="Red" runat="server" Text="切换到高级查询" OnClick="btnQueryALL_Click" />
        </td>
    </tr>
</table>
