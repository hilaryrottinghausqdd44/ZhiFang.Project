<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangPassWords_PatNo.aspx.cs" Inherits="ZhiFang.WebLis.Other.ChangPassWords_PatNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellpadding='0' cellspacing='1' border='0' 
            style="font-size: 12px">
    <tr><td style="text-align: right; " >原密码：</td><td>
        <asp:TextBox ID="TextBox1" runat="server"  TextMode="Password"></asp:TextBox>初始密码为“123456”。</td></tr>
    <tr><td style="text-align: right; font-size: 12px;">新密码：</td><td>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox></td></tr>
        <tr><td style="text-align: right; font-size: 12px;">再次输入密码：</td><td>
        <asp:TextBox ID="TextBox3" runat="server" TextMode="Password"></asp:TextBox></td></tr>
    <tr><td style="text-align: right; font-size: 12px;"><asp:Button ID="Button1" runat="server" Text="确定" 
            onclick="Button1_Click" /></td><td style="padding-left:12px">
        <input id="Reset1" type="reset" value="重置" /></td></tr>
        
    </table>
    </div>
    </form>
</body>
</html>
