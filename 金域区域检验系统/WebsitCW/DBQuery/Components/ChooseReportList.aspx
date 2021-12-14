<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Components.ChooseReportList" Codebehind="ChooseReportList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ChooseReportList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function returnMyChoice()
		{
			parent.window.returnValue=Form1.TextBox1.value + "---" + Form1.TextBox2.value;
			parent.window.close();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="TextBox1" style="Z-INDEX: 101; LEFT: 32px; POSITION: absolute; TOP: 64px" runat="server"
				Height="104px"></asp:textbox>
			<DIV style="DISPLAY: inline; Z-INDEX: 104; LEFT: 208px; WIDTH: 70px; POSITION: absolute; TOP: 32px; HEIGHT: 15px"
				ms_positioning="FlowLayout">Label</DIV>
			<INPUT id="buttReturnReport" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 200px"
				onclick="returnMyChoice()" type="button" value="Button">
			<DIV style="DISPLAY: inline; Z-INDEX: 103; LEFT: 40px; WIDTH: 70px; POSITION: absolute; TOP: 32px; HEIGHT: 15px"
				ms_positioning="FlowLayout">Label</DIV>
			<asp:TextBox id="TextBox2" style="Z-INDEX: 105; LEFT: 208px; POSITION: absolute; TOP: 72px" runat="server"
				Height="96px"></asp:TextBox></form>
	</body>
</HTML>
