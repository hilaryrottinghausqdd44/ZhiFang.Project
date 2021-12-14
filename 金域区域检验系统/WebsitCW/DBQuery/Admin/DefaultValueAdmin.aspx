<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.DefaultValueAdmin" Codebehind="DefaultValueAdmin.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DefaultValueAdmin</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">
			th{font-size:15px; font-weight:600;}
			td{font-size:12px;}
			.InputButton{border-style:outset; border-width:2px; background-color:#000080; color:white;border-color:#666666;}
		</style>
		<script type="text/javascript">
			function ClickEdit(obj)
			{
				var ret = window.showModalDialog("PopDefaultValueWindow.aspx?DefaultValueName=" + obj, "", "dialogHeight:300px; dialogWidth:400px;status:no;toolbar:no");
				if(ret != void 0)
				{
					document.location = document.location;
				}
			}
			
			function ClickDel(obj)
			{
				var confirmDel = window.confirm("是否真的要删除？");
				return confirmDel;
			}
			
			function NewCreateDefault()
			{
				var ret=window.showModalDialog("PopDefaultValueWindow.aspx", "", "dialogHeight:300px; dialogWidth:400px;status:no;toolbar:no");
				if(ret != void 0)
				{
					document.location = document.location;
				}
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="divConfig">
				<table style="BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; BORDER-COLLAPSE: collapse; BORDER-BOTTOM-STYLE: solid"
					borderColor="#000080" width="100%" bgColor="#ffffe0" border="2">
					<asp:Repeater ID="rptConfig" Runat="server">
						<HeaderTemplate>
							<tr bgcolor="#000080" style="COLOR: #ffffff">
								<th align="center">
									函数名称
								</th>
								<th align="center">
									函数内容
								</th>
								<th>
								</th>
							</tr>
						</HeaderTemplate>
						<ItemTemplate>
							<tr bordercolor="#000080">
								<td align="center" width="180px">
									<asp:Label Runat="server" ID="lblName"></asp:Label></td>
								<td align="center" width="180px">
									<asp:Label Runat="server" ID="lblValue"></asp:Label></td>
								<td align="left"><a runat="server" id="linkEdit">编辑</a>&nbsp;&nbsp;<asp:LinkButton ID="linkDel" Runat="server">删除</asp:LinkButton></td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr bordercolor="#000080" bgcolor="#ffffff">
								<td align="center" width="180px">
									<asp:Label Runat="server" ID="lblName"></asp:Label></td>
								<td align="center" width="180px">
									<asp:Label Runat="server" ID="lblValue"></asp:Label></td>
								<td align="left"><a runat="server" id="linkEdit">编辑</a>&nbsp;&nbsp;<asp:LinkButton ID="linkDel" Runat="server">删除</asp:LinkButton></td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
							<tr bordercolor="#000080" bgcolor="#ffffff">
								<td colspan="3" align="center">
									<input type="button" value="新建默认值" onclick="NewCreateDefault()" class="InputButton">
								</td>
							</tr>
						</FooterTemplate>
					</asp:Repeater></table>
			</div>
		</form>
	</body>
</HTML>
