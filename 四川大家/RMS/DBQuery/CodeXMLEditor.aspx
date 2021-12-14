<%@ Page validateRequest=false language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.CodeXMLEditor" Codebehind="CodeXMLEditor.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EditStyle</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="Button1" runat="server" Text="保存代码" onclick="Button1_Click"></asp:Button>
			<asp:Label id="Label1" runat="server" Width="440px" BorderWidth="1px" BorderColor="#FF8080">提示:</asp:Label>
			<asp:TextBox id="TextBox1" runat="server" TextMode="MultiLine" Height="552px" Width="100%"></asp:TextBox>
		</form>
	</body>
</HTML>
