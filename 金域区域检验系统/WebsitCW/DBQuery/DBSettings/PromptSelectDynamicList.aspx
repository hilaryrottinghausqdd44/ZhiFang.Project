<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.PromptSelectDynamicList" Codebehind="PromptSelectDynamicList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PromptSelectDynamicList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			function btnConfirm()
			{
				var strValue = document.Form1.all["txtInput"].value;
				window.returnValue = strValue;
				window.close();
			}
			function btnCancel()
			{
				window.close();
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td colspan="2" align="center">���봦����</td>
				</tr>
				<tr>
					<td colspan="2"><input type="text" name="txtInput"></td>
				</tr>
				<tr>
					<td align="center">
						<input type="button" value="ȷ��" onclick="btnConfirm()">
					</td>
					<td align="center">
						<input type="button" value="ȡ��" onclick="btnCancel()">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
