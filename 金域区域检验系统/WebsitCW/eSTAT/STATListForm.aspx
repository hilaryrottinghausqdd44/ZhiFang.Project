<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="STATListForm.aspx.cs" Inherits="OA.YHY.STATListForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>统计图列表</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:TreeView ID="tvStatSort" runat="server" CssClass="GridView">
        </asp:TreeView>
        <div style="text-align: center">
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
