<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPrint.aspx.cs" Inherits="ZhiFang.WebLis.ReportPrint.ReportPrint" %>
<html>
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../Css/Default.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%Response.Write(strTable); %>
    </div>
    </form>
</body>
</html>
