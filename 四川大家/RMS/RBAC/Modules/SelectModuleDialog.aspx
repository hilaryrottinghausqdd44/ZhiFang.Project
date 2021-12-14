<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.RBAC.Modules.SelectModuleDialog" Codebehind="SelectModuleDialog.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>选择风格文件对话框</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <table width="100%" height="100%">
        <tr>
            <td>
                <iframe width="100%" height="100%" src="SelectModule.aspx?<%=Request.ServerVariables["Query_string"]%>">
                </iframe>
            </td>
        </tr>
    </table>
</body>
</html>
