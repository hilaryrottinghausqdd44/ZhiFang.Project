<%@ Page language="c#" AutoEventWireup="True" Inherits="Labweb.PopupSelectImageFile" Codebehind="PopupSelectImageFile.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择风格文件对话框</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<table width="100%" height="100%">
			<tr>
				<td>
					<iframe width="100%" height="100%" src="SelectImageFile.aspx?<%=Request.ServerVariables["Query_string"]%>"></iframe>
				</td>
			</tr>
		</table>
	</body>
</HTML>
