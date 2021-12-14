<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.DictionariesConfigMain" Codebehind="DictionariesConfigMain.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DictionariesConfigMain</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			function IsSameName()
			{
				var rowsNum = document.Form1.all["tableField"].rows.length;
				if(rowsNum <= 2)
					return true;
			
				for(var i=1; i<rowsNum; i++)
				{					
					sc1 = document.Form1.all["dictFieldID" + i].value;
					
					for(var j=i+1; j<rowsNum; j++)
					{
						sc2 = document.Form1.all["dictFieldID" + j].value;
						
						if((sc1 == sc2))
						{
							alert("字段名不能相同");
							return false;
						}
					}
				}
				return true;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="10">
				<tr>
					<td><asp:button id="btnNewField" Text="新增记录" Runat="server" onclick="btnNewField_Click"></asp:button></td>
					<td><asp:button id="btnSave" Text="保存" Runat="server" onclick="btnSave_Click"></asp:button></td>
				</tr>
			</table>
			<table id="tableField" width="100%" align="center" border="1" runat="server">
			</table>
		</form>
	</body>
</HTML>
