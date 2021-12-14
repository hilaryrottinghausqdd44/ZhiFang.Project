<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Style.UploadStyle" Codebehind="UploadStyle.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UploadStyle</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" name="Form1" method="post" encType="multipart/form-data" runat="server">
			<table border="0">
				<tr>
					<td>
						<INPUT id="FileUpload" name="FileUpload" type="file" style="WIDTH: 307px; HEIGHT: 20px"
							size="32">
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblMessage" runat="server" Width="296px"  BorderColor="#8080FF"></asp:Label>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
