<%@ Register TagPrefix="uc1" TagName="NewsBrowserWebControl" Src="../../WebControlLib/NewsBrowserWebControl.ascx" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.News.Browse.BrowseNewsForm" Codebehind="BrowseNewsForm.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>新闻浏览</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../WebControlLib/CSS/WebControlDefault.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td>
                <asp:Label ID="lblTitile" runat="server" Visible="False">标题</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:NewsBrowserWebControl id="NewsBrowserWebControl1" runat="server">
                </uc1:NewsBrowserWebControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
