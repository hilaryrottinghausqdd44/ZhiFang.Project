<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataViewForm.aspx.cs" Inherits="OA.DataInput.DataViewForm" %>

<%@ Register src="../UserControlLib/DetailDataInputUserControl.ascx" tagname="DetailDataInputUserControl" tagprefix="uc1" %>
<%@ Register src="../UserControlLib/GridViewUserControl.ascx" tagname="GridViewUserControl" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据视图页面</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script src="../Util/CommonJS.js" type="text/javascript"></script>

</head>
<body class="ListBody" style="background-image:inherit" onload="resetWindowSizeForUseWindowOpen('lblErrorMessage')">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table id="tableDataView" runat="server" style="width:100%" border="1">
            <tr>
                <td style="text-align:center; width:auto">
                    <uc1:DetailDataInputUserControl ID="DetailDataInputUserControl1" 
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="PlaceHolder_DynamicUserControlContainer" runat="server" EnableViewState="true"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
