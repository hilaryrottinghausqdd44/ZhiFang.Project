<%@ Page language="c#" validateRequest="false" enableEventValidation="false" AutoEventWireup="True" Inherits="FTP.xmlEdit" Codebehind="xmlEdit.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>xmlEdit</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="styles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function ConfirmDelete(){
				if(confirm("真的要删除sitemap.xml文件吗？后果是很严重的呀!"))
				{
	   	 			document.location="../tools/XPathAnalyzer/ContentPane.aspx?Delete=1&Type=File&Name=sitemape.xml&Path=<%=Server.MapPath("../xml/").Replace("\\","\\\\")%>";
                }
			}			
		</script>
	</HEAD>
	<BODY bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form id="edit" method="post" runat="server">
			<P align="center">
				<asp:Button id="SaveBtn" runat="server" Text="保存" onclick="SaveBtn_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
				<asp:Button id="SaveAs" runat="server" Width="59px" Text="另存为" Enabled="False" onclick="SaveAs_Click"></asp:Button>
				<asp:TextBox id="SaveAsTxt" runat="server" Width="74px"></asp:TextBox>
				<asp:Label id="Header1" runat="server" Width="254px" Height="20px" ForeColor="Navy" BackColor="#C0C0FF"></asp:Label><BR>
				<BR>
				<asp:TextBox id="CodeText" runat="server" Width="95%" Height="500px" 
                    TextMode="MultiLine" Wrap="False" ontextchanged="CodeText_TextChanged"></asp:TextBox>
				<br>
			</P>
		</form>
	</BODY>
</HTML>
