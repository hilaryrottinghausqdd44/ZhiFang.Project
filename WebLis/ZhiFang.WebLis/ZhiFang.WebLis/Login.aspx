<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZhiFang.WebLis.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebLis登陆</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="JS/Tools.js" type="text/javascript"></script>
    <script src="ui/util/base64.js" type="text/javascript"></script>
    <script>
        function Check() {
            if (document.getElementById('textPassword') && document.getElementById('textPassword').value && document.getElementById('textPassword').value != "") {
                var b = new Base64();
                var str = b.encode(document.getElementById('textPassword').value);
                document.getElementById('textPassword').value = str;
                return true;
            }
            return false;
        }
    </script>
     <style type="text/css">
        #main {
            position: absolute;
            top: 25%;
            left: 30%
        }

        input {
            height: 30px;
            outline: none;
            background: #eef6fd;
            border-bottom: 2px #d6e7fa solid;
        }
    </style>
</head>
<body id="bg" style="background-image: url('Images/Login_bg.jpg')">
    <form id="form1" runat="server" onsubmit="return Check();">
        <div>
            <div id="main" style="border: 0px solid #F60; background-color: white">
                <table id="main-left" width="100%" cellpadding="6" cellspacing="0" border="0" style="background-color: white; width: 1000px;">
                    <tr>
                        <td rowspan="6" style="border-right: solid; border-right-color: cornflowerblue; border-right-width: 1px">
                            <img src="Images/Logo.png" height="400px" />
                        </td>
                        <td colspan="2" style="width: 500px; border-bottom: solid; border-bottom-color: cornflowerblue; border-bottom-width: 0px" align="center">
                            <span style="font-weight: bold; font-size: 30px; color: Highlight">Weblis 平台系统</span>
                            <br />
                            <span style="font-weight: bold; font-size: 15px">Weblis PlatForm System</span>
                        </td>
                    </tr>
                    <tr id="aa">
                        <td align="right">
                            <img src="Images/yhm.png" style="width: 25px" />
                        </td>
                        <td>
                            <asp:TextBox ID="textUserid" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <img src="Images/mm.png" style="width: 25px" />
                        </td>
                        <td>
                            <asp:TextBox ID="textPassword" TextMode="Password" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="border-top: solid; border-top-color: cornflowerblue; border-top-width: 0px">
                            <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red" Visible="False"
                                BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/LoginButton.png" OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <%-- <asp:Image ID="tel" runat="server" ImageUrl="~/Images/tel.jpg" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
