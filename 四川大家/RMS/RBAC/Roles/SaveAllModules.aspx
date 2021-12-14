<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.SaveAllModules" Codebehind="SaveAllModules.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>SaveAllModules</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

    <script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() {
	if('<%=Request.QueryString["txtRoleType"]%>'!='')
	{
		parent.document.all["buttSave"].disabled=false;
		parent.document.all["buttSave"].value="¼ÌÐø±£´æ";
		parent.document.all["Label1"].innerHTML='<%=Saved%>';
	}
}

//-->
    </script>

</head>
<body ms_positioning="GridLayout" language="javascript" onload="return window_onload()">
    <input type="text" value="" id="Saved">
</body>
</html>
