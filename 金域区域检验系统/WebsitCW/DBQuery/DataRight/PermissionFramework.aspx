<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.PermissionFramework" Codebehind="PermissionFramework.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>PermissionFramework</TITLE>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<frameset cols="180,*" border="0" frameborder="0" framespacing="0">
		<frame name="permissType" src="PermissionTreeView.aspx?<%=Request.ServerVariables["Query_String"]%>">
		<frame name="mainContent" src="">
	</frameset>
</HTML>
