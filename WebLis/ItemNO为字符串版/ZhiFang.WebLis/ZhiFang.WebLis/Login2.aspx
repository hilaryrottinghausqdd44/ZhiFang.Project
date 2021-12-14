<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Inherits="ZhiFang.WebLis.Login2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>登录</title>
   <style type="text/css">
        #main-right
        {
            position: absolute;
            top: 45px;
            left: 240px;
        }
        #Image1
        {
            position: absolute;
            top: 100px;
        }
        
        #table aa
        {
            padding: 290px 40px;
        }
        #main
        {
            width: 600px;
            height: 400px;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-top: -200px;
            margin-left: -300px;
            border: 1px solid #008800;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="main" style="border: 0px solid #F60; width: 700px; height: 400px;">
            <asp:Image ID="Image1" align="center" runat="server" ImageUrl="~/Images/mainlogo.jpg" />
            <table id="main-right" cellpadding="6" cellspacing="6">
                <tr>
                    <td colspan="2">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logintext.jpg" />
                    </td>
                </tr>
                <tr id="aa">
                    <td>
                        用户名称:
                    </td>
                    <td>
                        <asp:TextBox ID="textUserid" runat="server" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                       用户密码:
                    </td>
                    <td>
                        <asp:TextBox ID="textPassword" TextMode="Password" runat="server" Height="19px" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:label id="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red" Visible="False"
								BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/dl.gif" 
                                    onclick="ImageButton1_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:Image ID="tel" runat="server" ImageUrl="~/Images/tel.jpg" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
