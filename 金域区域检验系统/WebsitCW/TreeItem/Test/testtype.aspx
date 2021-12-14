<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testtype.aspx.cs" Inherits="TreeItem.Test.testtype" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    编号:<asp:TextBox ID="txtid" runat="server"></asp:TextBox>
    <br />
    名称:<asp:TextBox ID="txtname" runat="server"></asp:TextBox>
    <br />
    描述:<asp:TextBox ID="txtdes" runat="server"></asp:TextBox>
    <br />
    备注:<asp:TextBox ID="txtmemo" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="typelist" />
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="typeadd" />
    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="typeupdate" />
    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
        Text="typedelete" />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    </div>
    </form>
</body>
</html>
