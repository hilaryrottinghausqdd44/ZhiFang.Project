<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataGroupShowDetailInfoUserControl.ascx.cs" Inherits="ECDS.UserControlLib.DataGroupShowDetailInfoUserControl" %>

    <%if (cssFile.Trim() == "")
      {%>
        <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />
    <%}
      else
      {%>
        <link href="<%=cssFile%>" type="text/css" rel="stylesheet">
    <%}%>

<table id="tableShowDataGroup" runat="server" style="width:auto" border="1">
    <tr>
        <td style="width:1%;  background-color:#f1e3ff; white-space:nowrap" colspan="3">
            <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True">控件的标题</asp:Label>
            <asp:LinkButton ID="btnNew" Text="新建" SkinID="btnNew" runat="server" CssClass="LinkButton" Visible="false"/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder ID="PlaceHolder_DynamicUserControlContainer" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>

