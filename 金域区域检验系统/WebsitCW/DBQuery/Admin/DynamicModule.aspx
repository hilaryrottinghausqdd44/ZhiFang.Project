<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicModule.aspx.cs" Inherits="OA.DBQuery.Admin.DynamicModule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改动态模板参数</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtModalRules" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonSave" runat="server" onclick="ButtonSave_Click" 
            Text="保存" />
&nbsp;配置说明：[字段名]
    </div>
    </form>
</body>
</html>
