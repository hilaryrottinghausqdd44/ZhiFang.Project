<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.InputUploadFile" Codebehind="InputUploadFile.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InputUploadFile</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" name="Form1" method="post" encType="multipart/form-data" runat="server">
			<INPUT id="FileUpload" name="FileUpload" type="file" style="WIDTH: 307px; HEIGHT: 22px"
				size="32">
			<asp:Label id="lblMessage" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 24px" runat="server"
				Width="296px" BorderWidth="1px" BorderColor="#8080FF"></asp:Label>
		</form>
	</body>
</HTML>
