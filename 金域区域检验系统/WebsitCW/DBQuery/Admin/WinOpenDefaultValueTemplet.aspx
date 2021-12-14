<%@ Import Namespace="System.Xml" %>
<%@ Page validateRequest=false language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.WinOpenDefaultValueTemplet" Codebehind="WinOpenDefaultValueTemplet.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WinOpenDefaultValueTemplet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function ok(obj)
		{
			//parent.window.returnValue="$模板:" + obj;
			parent.window.returnValue= obj;
			parent.window.close();
		}
		function no(obj)
		{
			var confirm=window.confirm("真的要删除该模板吗？一旦删除不可能恢复\n如果有其他系统正在使用该模板，该模板将失效\n\n确定将执行删除\n\nYes to Delete Permanently");
			if(confirm)
				document.location.href="WinOpenDefaultValueTemplet.aspx?Delete=1&DefaultValue=" + escape(obj);
		}
		function ClearAll()
		{
			frames['eWebEditor1'].document.execCommand("SelectAll");
			frames['eWebEditor1'].document.execCommand("Cut");
		}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="1">
					<TR>
						<TD style="WIDTH: 158px" vAlign="top"><STRONG>模板选择<BR>
							<%if(allTemplet!=null){%>
									
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="1">
									<%
									foreach(XmlNode myNode in allTemplet){
									string myDefault=myNode.Attributes.GetNamedItem("tName").InnerXml;
									
									%>
									<TR>
										<TD><INPUT type="button" value="选择模板" onclick="ok('<%=myDefault%>');"></TD>
										<TD noWrap 
										<%if(myDefault==DefaultValue){%>bgcolor="skyblue"<%}%>
										<% else if(DefaultValue==""&&myNode.NextSibling==null){%>bgcolor="blue"<%}%>
										><a href="WinOpenDefaultValueTemplet.aspx?DefaultValue=<%=myDefault%>"><%=myDefault%></a></TD>
										<TD><IMG src="../images/icons/0014_b.gif" style="cursor:hand" onclick="no('<%=myDefault%>')"></TD>
									</TR>
									<%}%>
									
								</TABLE>
							<%}%><br>
							<IMG id="BCancel" onmouseover="this.style.border='#ccccff thin outset';" onclick="parent.window.close();"
											onmouseout="this.style.border='#ccccff 0px outset';" height="24" src="../image/middle/cancel.jpg" width="79" border="0">
							</STRONG>
						</TD>
						<TD vAlign="top">
							<table style="WIDTH: 100%; HEIGHT: 100%">
								<tr>
									<td colSpan="2"><IFRAME id="eWebEditor1" style="WIDTH: 100%; HEIGHT: 100%" src="../htmledit/ewebeditor.asp?id=content1&amp;style=standard_light"
											frameBorder="0" width="100%" scrolling="no" height="100%"></IFRAME><INPUT id=content1 type=hidden 
            value="<%=content%>" name=content1 
            >
									</td>
								</tr>
								<tr height="40">
									<td style="HEIGHT: 40px" vAlign="middle">&nbsp;<asp:button id="Button1" runat="server" Text="保存" onclick="Button1_Click"></asp:button><asp:textbox id="TextBox1" runat="server"></asp:textbox>
									<input type="button" value="清除" onclick="ClearAll();"</td>
									<td style="HEIGHT: 31px"></td>
								</tr>
								<TR height="40">
									<TD><asp:label id="lblMessage" runat="server" BorderColor="#8080FF" BorderWidth="1px" Width="430px" ForeColor="#9966ff" Font-Bold="true"></asp:label></TD>
									<TD></TD>
								</TR>
							</table>
							<P><INPUT type="hidden"></P>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
