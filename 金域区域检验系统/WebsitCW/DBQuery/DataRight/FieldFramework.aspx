<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.FieldFramework" Codebehind="FieldFramework.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>FieldFramework</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<frameset rows="110,*" border="0" frameborder="0" framespacing="0">
		<frame name="SelectRole" src="FieldPermissionSelect.aspx?<%=Request.ServerVariables["Query_String"]%>" noresize scrolling="auto">
		<frame name="FieldSetting" src="" >
	</frameset>
</HTML>