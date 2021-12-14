<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.DataRightMain" Codebehind="DataRightMain.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DataRightMain</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
			TH { FONT-WEIGHT: 600; FONT-SIZE: 15px }
			TD { FONT-SIZE: 12px }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="divMain">
				<table width="100%" border="2" bordercolor="#000080" bgcolor="#ffffe0" style="BORDER-TOP-STYLE:solid;BORDER-RIGHT-STYLE:solid;BORDER-LEFT-STYLE:solid;BORDER-COLLAPSE:collapse;BORDER-BOTTOM-STYLE:solid">
					<asp:Repeater ID="rptFieldSetting" Runat="server">
						<HeaderTemplate>
							<thead bgcolor="#000080" style="COLOR: #ffffff">
								<tr>
									<th align="center" width="120px" nowrap>
										类型</th>
									<th align="center">
										名称</th>
									<th align="center">
										ID
									</th>
									<th align="center">
										数据条件</th>
									<th align="center">
										编辑
									</th>
									<th align="center">
										删除
									</th>
								</tr>
							</thead>
						</HeaderTemplate>
						<ItemTemplate>
							<tr bordercolor="#000080">
								<td>
									<asp:Label ID="lblField" Runat="server"></asp:Label></td>
								<td></td>
								<td></td>
								<td>
									<asp:TextBox ID="txtCondition" TextMode="MultiLine" Runat="server" Width="200px" Rows="5"/></td>
								<td></td>
								<td></td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr bordercolor="#000080" bgcolor="#ffffff">
								<td>
									<asp:Label ID="lblField" Runat="server"></asp:Label></td>
								<td></td>
								<td></td>
								<td>
									<asp:TextBox ID="txtCondition" TextMode="MultiLine" Runat="server" Width="200px" Rows="5" /></td>
								<td></td>
								<td></td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
						</FooterTemplate>
					</asp:Repeater>
				</table>
			</div>
		</form>
	</body>
</HTML>
