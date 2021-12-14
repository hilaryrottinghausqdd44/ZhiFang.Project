<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.RBAC.Themes.ChooseButtContainer" Codebehind="ChooseButtContainer.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>供选择的全部按钮</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout" bottommargin="0" topmargin="0" rightmargin="0"
    leftmargin="0">
    <iframe id="frmButtons" src="ChooseButton.aspx?TemName=<%=Request.QueryString["TemName"]%>"
        width="100%" height="100%" frameborder="0"></iframe>
</body>
</html>
