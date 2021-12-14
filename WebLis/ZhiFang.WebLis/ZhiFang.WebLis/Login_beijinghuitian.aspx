<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_beijinghuitian.aspx.cs"
    Inherits="ZhiFang.WebLis.Login_beijinghuitian" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WebLis登陆</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="JS/Tools.js" type="text/javascript"></script>
</head>
<body class="full">
    <form id="form1" runat="server">
    <div>
        
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 100px;">
            <tr style="height: 10px; background-color: #eeeeee">
                <td>
                </td>
            </tr>
            <tr>
                <td height="310" align="center" bgcolor="#45948f">
                    <table width="70%" border="0" cellspacing="0" cellpadding="0" style="font: 12px">
                        <tr>
                            <td width="380">
                                <img src="images/beijinghuitian.png" width="380" height="310" />
                            </td>
                            <td valign="top">
                                <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
                                    <tr style="height: 80px">
                                        <td colspan="3" align="center">
                                            <img src="images/beijinghuitian1.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="border-bottom-color: White; border-bottom-style: dashed; border-bottom-width: 1px">
                                        </td>
                                    </tr>
                                    <tr  style="height: 35px">
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr style="height: 40px">
                                        <td width="150" align="right">
                                            <img src="images/beijinghuitian-username-icon.png" style="margin-right:20px" />
                                        </td>
                                        <td >
                                            <asp:TextBox ID="textUserid" runat="server" Width="200" Height="25"></asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red"
                                    Visible="False" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:Label>
                                        </td>
                                        <td rowspan="3" align="right" valign="bottom"> <img src="images/beijinghuitian2.png" /></td>
                                    </tr>
                                    <tr style="height: 40px">
                                        <td width="150" align="right">
                                            <img src="images/beijinghuitian-password-icon.png" style="margin-right:20px" />
                                        </td>
                                        <td >
                                            <asp:TextBox ID="textPassword" runat="server" Width="200" Height="25" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 100px">
                                        <td colspan="2" align="center" valign="middle">
                                            <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Italic="False" 
                                                Font-Names="Arial" ForeColor="#FF9900" Height="30px" onclick="Button1_Click" 
                                                Text="Login" Width="70px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 10px; background-color: #eeeeee">
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
