<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.PromptSelectDynamicList" Codebehind="PromptSelectDynamicList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态获取外面数据源</title>
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
					<td colspan="2" align="center">动态获取外部数据源</td>
				</tr>
				<tr>
					<td colspan="2"><input type="text" name="txtInput"></td>
				</tr>
				<tr>
					<td align="center">
						<input type="button" value="确定" onclick="btnConfirm()">
					</td>
					<td align="center">
						<input type="button" value="取消" onclick="btnCancel()">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
