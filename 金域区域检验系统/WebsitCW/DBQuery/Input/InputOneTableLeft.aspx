<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Input.InputOneTableLeft" Codebehind="InputOneTableLeft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InputOneTableLeft</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../css/css.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="1" width="100%" border="1">
				<TBODY>
					<div id="ddd">
						<%Response.Write(ShowDoc.OuterXml.Replace(((char)160).ToString(),"&nbsp;"));%>
						
					</div>
				</TBODY>
			</TABLE>
		</form>
		<script language="javascript">
		function QueryString(name)
		{
			alert("缺少主键，建设中....");	
		}
		function Expound(Name)
		{			
			window.parent.frames["Right"].location="InputOneTableMain.aspx?Items="+Name;
		}
		function Add()
		{
			var TbStr=document.all["ddd"].innerHTML;
			
			TbStr=TbStr.substring(0,TbStr.length-8);
			TbStr=TbStr+"<TR><TD>3</TD><TD></TD><TD></TD><TD></TD></TR></TBODY>";
			alert(TbStr)
			document.all["ddd"].innerHTML=TbStr;
			alert(document.all["ddd"].length)
		}
		</script>
	</body>
</HTML>
