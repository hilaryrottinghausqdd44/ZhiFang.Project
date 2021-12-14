<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataAddAndUpdateForm.aspx.cs"
    Inherits="OA.DataInput.DataAddAndUpdateForm" ValidateRequest="false" %>

<%@ Register Src="../UserControlLib/DetailDataInputUserControl.ascx" TagName="DetailDataInputUserControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业信息</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script src="../Util/CommonJS.js" type="text/javascript"></script>




</head>
<body class="ListBody" style="background-image: inherit" onload="resetWindowSizeForUseWindowOpen('lblErrorMessage')">
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <uc1:DetailDataInputUserControl ID="DetailDataInputUserControl1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrorMessage" runat="server" Text="" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
