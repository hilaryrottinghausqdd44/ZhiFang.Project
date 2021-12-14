<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.TableButtonsSelection" Codebehind="TableButtonsSelection.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TableButtonsSelection</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css"> BODY { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	A { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	TABLE { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	DIV { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	SPAN { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	TD { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	TH { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	INPUT { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	SELECT { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	BODY { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px }
		</style>
	</HEAD>
	<body TOPMARGIN="0" LEFTMARGIN="0" RIGHTMARGIN="0" BOTTOMMARGIN="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="657" align="center"
				border="0" style="WIDTH: 657px; HEIGHT: 135px" bgcolor="#000099">
				<TR bgcolor="#ffffff">
					<TD style="HEIGHT: 42px" colSpan="10">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="0" bgcolor="#ffffff">
							<TR>
								<TD><IMG style="BORDER-RIGHT: #ccccff thin outset; BORDER-TOP: #ccccff thin outset; BORDER-LEFT: #ccccff thin outset; BORDER-BOTTOM: #ccccff thin outset"
										src="images/Buttons.jpg"></TD>
								<TD>
									<P>可以选择预定义的按钮组:也可以在上述页面中重新定义新的按钮组，根据不的业务功能可以选择合适的按钮</P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR align="center"  bgcolor="#ffffff">
					<TD width="80">名称(选)</TD>
					<TD width="80">批</TD>
					<TD width="80">增</TD>
					<TD width="80">删</TD>
					<TD width="80">复</TD>
					<TD width="80">改</TD>
					<TD width="80">存</TD>
					<TD width="80">弃</TD>
					<TD width="80">报表</TD>
					<TD width="80" bgcolor="skyblue">删除</TD>
					
				</TR>
				<%if(ButtonsList!=null)
				{
					foreach(XmlNode eachButton in ButtonsList)
					{
					%>
				<TR bgcolor="#ffffff"  >
					<TD nowrap onclick="parent.window.returnValue='<%=eachButton.InnerXml%>';parent.window.close();" style="FONT-WEIGHT: bold; CURSOR: hand" onmousemove="this.style.backgroundColor='#dddddd'" onmouseout="this.style.backgroundColor='white'"><%=eachButton.Attributes.GetNamedItem("Name").InnerXml%></TD>
					<%
						string[] AllButtons=eachButton.InnerXml.Split("|".ToCharArray());
						for(int i=0;i<8;i++){%>
					<TD><%if(AllButtons[i]!=""){
							%><img border="0" src="<%=AllButtons[i]%>">
						<%}else{%>
						&nbsp;<%}%></TD>
					<%}%>
					<TD><input type="button" value="删除" onclick="document.all['Action'].value='<%=eachButton.Attributes.GetNamedItem("Name").InnerXml%>';Form1.submit();"></TD>
					
				</TR>
				<%}}%>
			</TABLE>
			<P align="center"><INPUT type="button" value="关闭窗口" onclick="window.parent.close()"></P>
			<input type="hidden" name="Action">
			<input type="submit" value="aa" style="display:none">
		</form>
	</body>
</HTML>
