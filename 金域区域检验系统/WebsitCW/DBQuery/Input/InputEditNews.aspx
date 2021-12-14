<%@ Page validateRequest="false" enableEventValidation="false" language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.InputEditNews" Codebehind="InputEditNews.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>±‡º≠–≈œ¢</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		function ok(obj)
		{
			obj.style.display="none";
			Form1.submit();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" rightmargin="0" leftmargin="0" bottommargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server"> 
			<table height="100%" width="100%">
				<tr>
					<td colspan="2"><IFRAME id="eWebEditor1" src="../htmledit/ewebeditor.asp?id=content1&amp;style=standard_light"
							frameBorder="0" width="100%" scrolling="no" height="100%"></IFRAME><INPUT id=content1 type=hidden value="<%=Server.HtmlEncode(content)%>" name=content1>
					</td>
				</tr>
				<tr height="40">
					<td><img id="BSave" src="../image/middle/save.jpg" width="79" height="24" border="0"
							onmouseover="this.style.border='#ccccff thin outset';" 
							onmouseout="this.style.border='#ccccff 0px outset';" 
							onclick="ok(this)">	
					<img id="BCancel" src="../image/middle/cancel.jpg" width="79" height="24" border="0"
							onmouseover="this.style.border='#ccccff thin outset';" 
							onmouseout="this.style.border='#ccccff 0px outset';" 
							onclick="parent.window.close();">	</td>
					<td><asp:Label id="lblMessage" style="Z-INDEX: 101; " runat="server"
					Width="296px" BorderWidth="1px" BorderColor="#8080FF"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
