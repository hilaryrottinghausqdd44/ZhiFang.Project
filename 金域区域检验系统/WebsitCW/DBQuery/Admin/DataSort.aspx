<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.DataSort" Codebehind="DataSort.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DataSort</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<iframe id="executeSort" src="exeDataSort.aspx?<%=Request.ServerVariables["Query_String"]%>" width="100%" height="100%" scrolling="auto"></iframe>
		</form>
	</body>
</HTML>
