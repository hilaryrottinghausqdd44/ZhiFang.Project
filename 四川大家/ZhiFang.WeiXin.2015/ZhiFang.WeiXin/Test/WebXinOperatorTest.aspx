<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebXinOperatorTest.aspx.cs" Inherits="ZhiFang.WeiXin.WebXinOperatorTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>
    
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button2_Click" Text="查询用户信息" />
        <div id="UserInfo" runat="server"></div>
    
    </div>
    
    </div>
    <div>
    
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" onclick="Button1_Click" Text="发送报告单" />
        <div id="SendResult" runat="server">123123123</div>
    
    </div>
    <asp:Button ID="Button3" runat="server" Text="获取用户列表" onclick="Button3_Click" />
    <div id="UserList" runat="server"></div>
    <br />
    <br />
    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="获取接口Token" />
    <asp:Button ID="Button5" runat="server" Text="获取用户列表" onclick="Button5_Click" />
    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" Text="上传图片" />
    <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox>
    <div id="Token" runat="server"></div>
    <br />
    <br />
    <br />
    <div id="UserNameList" runat="server">
        <asp:Button ID="Button7" runat="server" onclick="Button7_Click" Text="Button" />
    </div>
    </form>
</body>
</html>
