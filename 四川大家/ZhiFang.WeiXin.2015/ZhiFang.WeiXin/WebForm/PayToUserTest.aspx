<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayToUserTest.aspx.cs" Inherits="ZhiFang.WeiXin.WebForm.PayToUserTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div><asp:Label ID="Label1" runat="server" Text="OID"></asp:Label>
        <asp:TextBox ID="TextOpenId" runat="server"></asp:TextBox></div>
         <div><asp:Label ID="Label2" runat="server" Text="amount"></asp:Label>
        <asp:TextBox ID="TextAmount" runat="server"></asp:TextBox></div>
         <div><asp:Label ID="Label3" runat="server" Text="UserName"></asp:Label>
        <asp:TextBox ID="TextName" runat="server"></asp:TextBox></div>
         <div><asp:Label ID="Label4" runat="server" Text="desc"></asp:Label>
        <asp:TextBox ID="TextDesc" runat="server"></asp:TextBox></div>
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    
    </div>
    </form>
</body>
</html>
