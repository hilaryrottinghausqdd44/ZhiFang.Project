<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login1.aspx.cs" Inherits="ZhiFang.WebLis.login1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>佛山卫生区域信息平台</title>

<link href="CSS/login.css" rel="stylesheet" type="text/css" />
    <script src="JS/Tools.js" type="text/javascript"></script>
<script type="text/javascript">
    function Clear() {
        var UserID = document.getElementById('UserID');
        UserID.value = "";
        var Password = document.getElementById('Password');
        Password.value = "";

    }
    </script>

</head>
<body >
<form  id="form1" runat="server">


 <div class="bg01">
        <div class="name_logo">
            <span class="title">区域检验数据中心</span>
        </div>
        <div class="bg02">
        <asp:TextBox ID="textUserid" runat="server" Width="270" class="name"></asp:TextBox>
        <asp:TextBox ID="textPassword" runat="server" Width="270"  TextMode="Password"  class="mima"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server"  
                ImageUrl="images/login/banner01.png"  class="login" 
                onclick="ImageButton1_Click"   />
  <div style="text-align:center; width:325px; float:left; margin:0px 0px 0px 560px; padding:0px;"><span id="message" style="color:Red;font-size:X-Small;"><asp:label id="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red" Visible="False"
								BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:label></span></div>     
  <span class="banQuan">Copyright © 2009 佛山卫生,All Rights Reserved</span>
     </div>
       </div>
</form>
</body>
</html>
