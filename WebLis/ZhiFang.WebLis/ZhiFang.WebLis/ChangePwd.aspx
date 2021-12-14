<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="ZhiFang.WebLis.ChangePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="新密码:"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" ></asp:TextBox>
        </div>
        <div style="margin-top:5px;">
            <asp:Label ID="Label2" runat="server" Text="确认密码:"></asp:Label>
            <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
    <div style="padding-left:170px;padding-top:20px;">
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
    </div>
    
    </form>
</body>
</html>
