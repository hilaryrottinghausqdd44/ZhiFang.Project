<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.samples.test" Codebehind="test.aspx.cs" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>test</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<SELECT style="Z-INDEX: 101; LEFT: 448px; WIDTH: 152px; POSITION: absolute; TOP: 216px; HEIGHT: 70px"
				multiple>
				<OPTION value="" selected>a</OPTION>
				<OPTION value="">b</OPTION>
				<OPTION value="">c</OPTION>
				<OPTION value="">d</OPTION>
				<OPTION value=""></OPTION>
			</SELECT>
			<iewc:TreeView id="TreeView1" style="Z-INDEX: 102; LEFT: 160px; POSITION: absolute; TOP: 184px"
				runat="server"></iewc:TreeView>
		</form>
	</body>
</HTML>
