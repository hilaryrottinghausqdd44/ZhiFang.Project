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
			//parent.window.returnValue="$ģ��:" + obj;
			parent.window.returnValue= obj;
			parent.window.close();
		}
		function no(obj)
		{
			var confirm=window.confirm("���Ҫɾ����ģ����һ��ɾ�������ָܻ�\n���������ϵͳ����ʹ�ø�ģ�壬��ģ�彫ʧЧ\n\nȷ����ִ��ɾ��\n\nYes to Delete Permanently");
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
			<FONT face="����">
				<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="1">
					<TR>
						<TD style="WIDTH: 158px" vAlign="top"><STRONG>ģ��ѡ��<BR>
							<%if(allTemplet!=null){%>
									
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="1">
									<%
									foreach(XmlNode myNode in allTemplet){
									string myDefault=myNode.Attributes.GetNamedItem("tName").InnerXml;
									
									%>
									<TR>
										<TD><INPUT type="button" value="ѡ��ģ��" onclick="ok('<%=myDefault%>');"></TD>
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
									<td style="HEIGHT: 40px" vAlign="middle">&nbsp;<asp:button id="Button1" runat="server" Text="����" onclick="Button1_Click"></asp:button><asp:textbox id="TextBox1" runat="server"></asp:textbox>
									<input type="button" value="���" onclick="ClearAll();"</td>
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
