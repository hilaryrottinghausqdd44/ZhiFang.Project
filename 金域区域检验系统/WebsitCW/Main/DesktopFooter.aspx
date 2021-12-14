<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesktopFooter.aspx.cs" Inherits="OA.Main.DesktopFooter" %>

<%@ Register src="../UserControlLib/LogonFooter.ascx" tagname="LogonFooter" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" style="width:60; text-align:center">
        <tr>
            <td align="center">
                <uc1:LogonFooter ID="LogonFooter1" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
