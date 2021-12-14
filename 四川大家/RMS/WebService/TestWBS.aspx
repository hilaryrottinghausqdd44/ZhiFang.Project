<%@ Page Language="C#" AutoEventWireup="true" validateRequest=false CodeBehind="TestWBS.aspx.cs" Inherits="OA.WebService.TestWBS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="返回人员信息" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="返回组织机构代码" />
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Height="364px" TextMode="MultiLine" 
        Width="543px"></asp:TextBox>
    </form>
</body>
</html>
