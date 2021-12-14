<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ZhiFang.WebLis.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="textUserid" runat="server" Width="120"></asp:TextBox>
        <br />
        <asp:TextBox ID="textPassword" runat="server" TextMode="Password" Width="120"></asp:TextBox>
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/dl.gif" 
            OnClick="ImageButton1_Click" /><asp:ImageButton ID="ImageButton2" runat="server" />
        <asp:Button
                ID="Button1" runat="server" Text="Button" onclick="Button1_Click" CausesValidation=false />
    </div>
    </form>
</body>
</html>
