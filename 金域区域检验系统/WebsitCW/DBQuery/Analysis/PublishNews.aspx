<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.PublishNews" Codebehind="PublishNews.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>PublishNews</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
				<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="300" align="center" borderColorLight="darkgray"
						border="1" borderColor="#ff3333" borderColorDark="gainsboro">
						<TR height=25>
							<TD align="center" colspan="2">发布数据</TD>
						</TR>
						<TR height=25>
							<TD align="center" >选择新闻模板</TD>
							<td>
								<asp:DropDownList id="dropListTemplate" Runat="server"></asp:DropDownList></td>
						</TR>
						<TR height=25>
							<TD align="center" colspan="2"><asp:Button id="Button1"  runat="server"
					Text="发布新闻" Height="25px" onclick="Button1_Click"></asp:Button></TD>
						</TR>
					</TABLE>
			
				
		</form>
	</body>
</HTML>
