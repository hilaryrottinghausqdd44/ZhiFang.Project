<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZhiFang.WebLis.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebLis登陆</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="JS/Tools.js" type="text/javascript"></script>
</head>
<body class="full">
    <form id="form1" runat="server">
    <div>
    
        <table width="492" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 100px;
            border: #CCCCCC solid 1px; font: 12px">
            <tr>
                <td>
                    <img src="images/bg.jpg" width="492" height="156" />
                </td>
            </tr>
            <tr>
                <td height="35" align="center" bgcolor="f4ffff">
                    <table width="480" border="0" cellspacing="0" cellpadding="0" style="font: 12px">
                        <tr>
                            <td width="40">
                                <img src="images/admin.gif" width="25" height="26" />
                            </td>
                            <td width="100">
                                用户名：
                            </td>
                            <td width="120">
                                <asp:TextBox ID="textUserid" runat="server" Width="120"></asp:TextBox>
                            </td>
                            <td width="100">
                                密码：
                            </td>
                            <td width="120">
                                <asp:TextBox ID="textPassword" runat="server" Width="120" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1"  runat="server" ImageUrl="~/Images/dl.gif" OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10">
                                <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red"
                                    Visible="False" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
<script language="javascript">
        function f_IsFullScreen() {
            return (window.screenLeft == 0 && window.document.body.clientWidth == window.screen.width);
        }
        function WALL_web() {
            if (!f_IsFullScreen()) {
                if (window.screen.height <= 720 || window.screen.width <= 1280) {
                    var wsShell = new ActiveXObject('WScript.Shell');
                    wsShell.SendKeys('{F11}');
                    return false;
                }
            }
        }
        window.onload = WALL_web; 
</script>
