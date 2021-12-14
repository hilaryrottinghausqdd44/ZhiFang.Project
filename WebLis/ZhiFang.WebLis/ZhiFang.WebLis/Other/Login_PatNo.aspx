<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_PatNo.aspx.cs" Inherits="ZhiFang.WebLis.Other.Login_PatNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellpadding='0' cellspacing='1' border='0' style="font-size: 12px">
    <tr><td style="text-align: right">病例号：</td><td>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td></tr>
    <tr><td style="text-align: right">密码：</td><td>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox></td></tr>
    <tr><td style="text-align: right"><asp:Button ID="Button1" runat="server" Text="登录" 
            onclick="Button1_Click" /></td><td style="padding-left:12px">
        <input id="Reset1" type="reset" value="重置" /></td></tr>
        
    </table>
    </div>
    </form>
</body>
</html>
