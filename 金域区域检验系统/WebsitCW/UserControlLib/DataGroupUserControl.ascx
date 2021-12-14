<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataGroupUserControl.ascx.cs"
    Inherits="ECDS.UserControlLib.DataGroupUserControl" %>
    
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

<table width="100%" border="0">
    <tr>
        <td>
            <asp:PlaceHolder ID="dataGroupControlList" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
    </tr>
</table>
