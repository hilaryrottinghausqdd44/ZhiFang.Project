<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="OA.DataInput.DataQueryForm" Codebehind="DataQueryForm.aspx.cs" %>


<%@ Register Src="../DataUserControlLib/OADetailDataInputUserControl.ascx" TagName="OADetailDataInputUserControl"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <div>
        <uc1:OADetailDataInputUserControl ID="OADetailDataInputUserControl1" runat="server" />
    </div>
    <table border="1">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label111111111111">1111111111</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
