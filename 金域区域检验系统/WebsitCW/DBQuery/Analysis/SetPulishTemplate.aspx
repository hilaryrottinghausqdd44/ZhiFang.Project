<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.SetPulishTemplate" validateRequest="false" enableEventValidation="false" Codebehind="SetPulishTemplate.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>SetPulishTemplate</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style>.text { FONT-SIZE: 13px; COLOR: #000000; TEXT-DECORATION: none }
	</style>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table class="text" style="BORDER-COLLAPSE: collapse" borderColor="#93bee2" cellSpacing="0"
				cellPadding="0" width="75%" align="center" border="1">
				<tr height="25">
					<td align="center" colSpan="2"><b>单表系统发布功能配置</b>
					</td>
				</tr>
				<tr height="25">
					<td width="20%">选择新闻系模块</td>
					<td><asp:dropdownlist id="DropDownList1" runat="server"></asp:dropdownlist>&nbsp;
						<asp:button id="Button1" runat="server" Text="应用" Width="54px" onclick="Button1_Click"></asp:button></td>
				</tr>
			</table>
			<p></p>
			<FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT><FONT face="宋体"></FONT><FONT face=宋体></FONT><FONT face=宋体></FONT>
			<table class="text" style="BORDER-COLLAPSE: collapse" borderColor="#93bee2" cellSpacing="0"
				cellPadding="0" width="75%" align="center" border="1">
				<tr height="25">
					<td width="20%">选择发布模板</td>
					<td><asp:dropdownlist id="dropListTemplate" runat="server"></asp:dropdownlist>
						<asp:Button id="Button2" runat="server" Text="调出主题" onclick="Button2_Click"></asp:Button>
						<asp:button id="SaveBtn" runat="server" Text="保存" onclick="SaveBtn_Click"></asp:button>
						<asp:Button id="Button3" runat="server" Text="另存为" onclick="Button3_Click"></asp:Button>
<asp:TextBox id=TextBox2 runat="server" Width="104px"></asp:TextBox></td>
				</tr>
				<tr>
					<TD vAlign="top" colspan="2">
						<asp:TextBox id="TextBox1" runat="server" Height="432px" Width="100%" TextMode="MultiLine"></asp:TextBox>
					</TD>
				</tr>
			</table>
		</form>
	</body>
</HTML>
