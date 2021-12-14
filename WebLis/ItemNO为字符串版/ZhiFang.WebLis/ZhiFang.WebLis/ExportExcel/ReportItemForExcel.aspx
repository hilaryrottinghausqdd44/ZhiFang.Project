<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportItemForExcel.aspx.cs"
    Inherits="ZhiFang.WebLis.ExportExcel.ReportItemForExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weblis结果导出</title>
    <link type="text/css" rel="stylesheet" href="../Css/jyjgcxstyle.css" />
    <script type="text/javascript" src="../js/calendarb.js"></script>
    <script type="text/javascript" src="../js/PrinterOperatorInterface.js"></script>
    <script type="text/javascript" language="javascript">
        function ExportExcel() {
            var o = document.getElementById('ReportItemFullExcel');
            o.click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="topstyle">
        <div class="topstyler">
        </div>
        <div style="clear: both; height: 28px;">
        </div>
    </div>
    <div class="searchstyle">
        <!--#include file="../SearchTools/ExcelReportItem.htm"-->
    </div>
    <asp:LinkButton ID="linkGetAllFrom" Style="display: none" runat="server" OnClick="linkGetAllItem_Click"></asp:LinkButton>
    <asp:LinkButton ID="ReportItemFullExcel" Style="display: none" runat="server" OnClick="ReportItemFullExcel_Click"></asp:LinkButton>
    <div class="contentstyle">
        <iframe src="<%=strTable %>" height="100%" width="100%"></iframe>
    </div>
    </form>
</body>
</html>
