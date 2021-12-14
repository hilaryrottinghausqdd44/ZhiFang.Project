<%@ Page language="c#" validateRequest="false" enableEventValidation="false" Codebehind="RightXmlEditFlow.aspx.cs" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.RightXmlEditFlow" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RightXmlEdit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div align="center">
				<asp:Button id="SaveBtn" runat="server" Text="±£´æ" onclick="SaveBtn_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
				<asp:Label id="Header" runat="server" Width="254px" Height="20px" ForeColor="Navy" BackColor="#C0C0FF"></asp:Label><BR>
				<BR>
				<asp:TextBox id="CodeText" runat="server" Width="95%" Height="500px" TextMode="MultiLine" Wrap="False"></asp:TextBox>
				<br>
			</div>
		</form>
	</body>
</HTML>
