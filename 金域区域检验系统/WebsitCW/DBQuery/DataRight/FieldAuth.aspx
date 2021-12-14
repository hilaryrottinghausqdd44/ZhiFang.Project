<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.FieldAuth" Codebehind="FieldAuth.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FieldAuth</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">TH { FONT-WEIGHT: 600; FONT-SIZE: 15px }
	TD { FONT-SIZE: 12px }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="divMain">
				<table style="BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; BORDER-COLLAPSE: collapse; BORDER-BOTTOM-STYLE: solid"
					borderColor="#000080" width="100%" bgColor="#ffffe0" border="2">
					<asp:repeater id="rptFieldSetting" Runat="server">
						<HeaderTemplate>
							<thead bgcolor="#000080" style="COLOR: #ffffff">
								<tr>
									<th align="center" width="120px" nowrap>
										字段名称</th>
									<th align="center">
										可见
									</th>
									<th align="center">
										不可见</th>
									<th align="center">
										不限制</th>
								</tr>
							</thead>
						</HeaderTemplate>
						<ItemTemplate>
							<tr bordercolor="#000080">
								<td align="center">
									<asp:Label ID="lblField" Runat="server"></asp:Label></td>
								<td align="center">
									<%--<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>--%>
									<asp:RadioButton ID="rdoVisible" Runat="server" GroupName="Field"></asp:RadioButton>
								</td>
								<td align="center">
									<asp:RadioButton ID="rdoInvisible" Runat="server" GroupName="Field"></asp:RadioButton>
								</td>
								<td align="center">
									<asp:RadioButton ID="rdoNoSetting" Runat="server" GroupName="Field"></asp:RadioButton>
								</td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr bordercolor="#000080" bgcolor="#ffffff">
								<td align="center">
									<asp:Label ID="lblField" Runat="server"></asp:Label></td>
								<td align="center">
									<%--<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>--%>
									<asp:RadioButton ID="rdoVisible" Runat="server" GroupName="Field"></asp:RadioButton>
								</td>
								<td align="center">
									<asp:RadioButton ID="rdoInvisible" Runat="server" GroupName="Field"></asp:RadioButton>
								</td>
								<td align="center">
									<asp:RadioButton ID="rdoNoSetting" Runat="server" GroupName="Field"></asp:RadioButton>
								</td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
						</FooterTemplate>
					</asp:repeater></table>
			</div>
			<br>
			<div style="TEXT-ALIGN: center"><asp:button id="btnSave" Runat="server" Text="保存" onclick="btnSave_Click"></asp:button>
				&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRepeal" Runat="server" Text="撤消当前角色配置" onclick="btnRepeal_Click"></asp:Button>
			</div>
		</form>
	</body>
</HTML>
