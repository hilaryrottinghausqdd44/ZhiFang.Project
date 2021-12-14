<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.News.Browse.BrowseApplicationSystemForm" Codebehind="BrowseApplicationSystemForm.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="DataListWebControl" Src="../../WebControlLib/DataListWebControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BrowseApplicationSystemForm</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../WebControlLib/CSS/WebControlDefault.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td>
                <uc1:DataListWebControl id="DataListWebControl1" runat="server">
                </uc1:DataListWebControl>
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
