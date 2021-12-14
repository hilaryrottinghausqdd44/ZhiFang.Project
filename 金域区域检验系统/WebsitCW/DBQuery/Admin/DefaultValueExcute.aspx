<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.DefaultValueExcute" Codebehind="DefaultValueExcute.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DefaultValueExcute</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			function ClickCancel()
			{
				window.parent.close();
			}
			
			function ClickConfirm()
			{
				if(document.getElementById("txtName").value == "" || document.getElementById("txtContent").innerText == "")
				{
					alert("函数名称和函数内容不能为空！");
					return false;
				}
				return true;
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="1">
				<tr>
					<td align="center">函数名称</td>
					<td align="center"><asp:TextBox ID="txtName" Runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="center">函数内容</td>
					<td align="center"><asp:TextBox ID="txtContent" TextMode="MultiLine" Runat="server" Rows="4"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="center" colspan="2"><asp:Button ID="btnConfirm" Runat="server" Text="确定" onclick="btnConfirm_Click"></asp:Button>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" value="取消" onclick="ClickCancel()"></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;<asp:Label ID="lblMessage" Runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
