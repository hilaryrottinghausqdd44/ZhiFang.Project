<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMain.aspx.cs" Inherits="ZhiFang.WebLis.LoginMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>WebLis登陆</title>
    <link rel="stylesheet" type="text/css" href="css/Login.css" />
    <script src="JS/Tools.js" type="text/javascript"></script>
    <script src="ui/util/base64.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Check() {
            if (document.getElementById('textPassword') && document.getElementById('textPassword').value && document.getElementById('textPassword').value != "") {
                var b = new Base64();
                var str = b.encode(document.getElementById('textPassword').value);
                document.getElementById('textPassword').value = str;
                return true;
            }
            return false;
        }
        function getExplorer() {
            var explorer = window.navigator.userAgent;
            //ie
            if (explorer.indexOf("MSIE") >= 0) {
                var browser = navigator.appName;
                var b_version = navigator.appVersion;
                var version = b_version.split(";");
                var trim_Version = version[1].replace(/[ ]/g, "");
                var number = "";
                for (i = 0; i < trim_Version.length; i++) {
                    if ("0123456789".indexOf(trim_Version.substr(i, 1)) > -1)
                        number += trim_Version.substr(i, 1);
                }
                window.location.href = "Login.aspx";
                if (number < "8.0") {
                    window.location.href = "Login.aspx";
                } else {
                    document.getElementById('tixing').style.display = 'none';
                }
            }
            //firefox
            else if (explorer.indexOf("Firefox") >= 0) {

                document.getElementById('tixing').style.display = 'none';
            }
            //Chrome
            else if (explorer.indexOf("Chrome") >= 0) {

                document.getElementById('tixing').style.display = 'none';
            }
            //Opera
            else if (explorer.indexOf("Opera") >= 0) {

                document.getElementById('tixing').style.display = 'none';
            }
            //Safari
            else if (explorer.indexOf("Safari") >= 0) {

                document.getElementById('tixing').style.display = 'none';
            }
        }

        window.onload = getExplorer;
    </script>
</head>
<body>
    <img class="bgone" src="Images/Login_bg.jpg" />
    <img class="pic" src="Images/Logo.png" />
    <form id="form1" runat="server" onsubmit="return Check();">
        <div class="table">
            <div class="wel">WebLis平台系统</div>
            <div class="wel1">WebLis PlatForm System</div>
            <div class="user">
                <div id="yonghu" style="">
                    <img src="Images/yhm.png" />
                </div>
                <asp:TextBox ID="textUserid" runat="server" placeholder="用户名"></asp:TextBox>
            </div>
            <div class="password">
                <div id="yonghu">
                    <img src="Images/mm.png" />
                </div>
                <asp:TextBox ID="textPassword" runat="server" TextMode="Password" placeholder="密码" name="密码"></asp:TextBox>
            </div>
            <asp:Button class="btn" ID="Button1" runat="server" name="登录" Text="登录" OnClick="ImageButton1_Click" />
            <div>
                <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red" Visible="False" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
