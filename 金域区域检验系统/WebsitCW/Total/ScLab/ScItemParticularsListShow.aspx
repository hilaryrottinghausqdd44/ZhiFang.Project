<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScItemParticularsListShow.aspx.cs" Inherits="OA.Total.ScLab.ScItemParticularsListShow" %>

<%@ Register assembly="WebSiteOA" namespace="OA.ZCommon.CommonControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>显示组套项目下的子项目</title>
    <link href="../../Css/style.css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:InheritDataGrid ID="InheritDataGrid1" runat="server" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" 
            Mode="NumericPages" />
        <ItemStyle ForeColor="White" />
        <Columns>
            <asp:BoundColumn DataField="nfclientname" HeaderText="客户"></asp:BoundColumn>
            <asp:BoundColumn DataField="cname" HeaderText="姓名"></asp:BoundColumn>
            <asp:BoundColumn DataField="itemnamecw" HeaderText="项目"></asp:BoundColumn>
            <asp:BoundColumn DataField="itemcount" HeaderText="数量"></asp:BoundColumn>
            <asp:BoundColumn DataField="sumitemagioprice" HeaderText="价格"></asp:BoundColumn>
        </Columns>
        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="White" 
            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
            Font-Underline="False" />
    </cc1:InheritDataGrid>
			
        
    </form>
</body>
</html>
