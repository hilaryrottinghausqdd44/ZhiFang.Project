<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.SourceConditionAuth" Codebehind="SourceConditionAuth.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SourceConditionAuth</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">TH { FONT-WEIGHT: 600; FONT-SIZE: 15px }
	TD { FONT-SIZE: 12px }
	.InputButton { BORDER-RIGHT: #666666 2px outset; BORDER-TOP: #666666 2px outset; BORDER-LEFT: #666666 2px outset; COLOR: white; BORDER-BOTTOM: #666666 2px outset; BACKGROUND-COLOR: #000080 }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="divCondition">
				<table style="BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; BORDER-COLLAPSE: collapse; BORDER-BOTTOM-STYLE: solid"
					borderColor="#000080" width="100%" bgColor="#ffffe0" border="2">
					<asp:repeater id="rptCondition" runat="server">
						<HeaderTemplate>
							<thead bgcolor="#000080" style="COLOR: #ffffff">
								<tr>
									<th align="center">
										ID号</th>
									<th align="center">
										描述</th>
									<th align="center">
										字段
									</th>
									<th align="center">
										关系</th>
									<th align="center">
									</th>
								</tr>
							</thead>
						</HeaderTemplate>
						<ItemTemplate>
							<tr bordercolor="#000080">
								<td align="center">
									<asp:Label ID="lblID" Runat="server"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lblMessage" Runat="server"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lblField" Runat="server"></asp:Label>
								<td>
									<asp:Label ID="lblRelation" Runat="server"></asp:Label></td>
								<td>
									<asp:LinkButton ID="btnDelete" text="删除" Runat="server"></asp:LinkButton>
									<a id="linkEdit" href="#" runat="server" target="_blank">编辑</a> <a id="linkAdd" href="#" runat="server">
										增加</a>
								</td>
							</tr>
						</ItemTemplate>
						<FooterTemplate>
							<tr bordercolor="#000080" bgcolor="#ffffff">
								<td align="center" colspan="5"><input type="button" value="新增条件" onclick="CreateDatabase()" class="InputButton"></td>
							</tr>
						</FooterTemplate>
					</asp:repeater></table>
			</div>
		</form>
	</body>
</HTML>
