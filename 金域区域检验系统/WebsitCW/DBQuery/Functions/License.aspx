<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Functions.License1" Codebehind="License.aspx.cs" %>
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<style type="text/css">BODY {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
A {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
TABLE {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
DIV {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
SPAN {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
TD {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
TH {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
INPUT {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
SELECT {
	FONT: 9pt "����", Verdana, Arial, Helvetica, sans-serif
}
BODY {
	PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px
}
</style>
		<script language="JavaScript" src="dialog.js"></script>
		<script language="javascript">
			//window.returnValue = null;
			//window.close();
			function ok()
			{
				parent.window.returnValue = Form2.TextBoxLicenseNo.value;
				parent.window.close();
			}
		</script>
	</HEAD>
	<body oncontextmenu="return false" onselectstart="return false" ondragstart="return false"
		bgColor="menu">
		<form id="Form2" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<td>
						<fieldset>
							<legend>����֮��������ע�������ģ��</legend>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>������:</td>
									<td width="5"></td>
									<td width="216" colSpan="5"><asp:textbox id="TextBoxNetworkCardNo" runat="server" Width="341px"></asp:textbox></td>
									<td width="7"></td>
								</tr>
								<TR>
									<TD width="7"></TD>
									<TD></TD>
									<TD width="5"></TD>
									<TD width="216"></TD>
									<TD width="40"></TD>
									<TD></TD>
									<TD width="5"></TD>
									<TD></TD>
									<TD width="7"></TD>
								</TR>
								<TR>
									<TD width="7"></TD>
									<TD>��Ȩ��:</TD>
									<TD width="5"></TD>
									<TD width="216"><asp:textbox id="TextBoxLicenseNo" runat="server" Width="234px"></asp:textbox></TD>
									<TD width="40" colSpan="4"><asp:button id="ButtonCreate" runat="server" Text="������Ȩ��" onclick="ButtonCreate_Click"></asp:button></TD>
									<TD width="7"></TD>
								</TR>
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td height="5"></td>
				</tr>
				<tr>
					<td>
						<fieldset>
							<legend>��Ȩ��Ч����</legend>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>��Ȩ����:</td>
									<td width="5"></td>
									<td width="111"><select id="drpLicenseType" style="WIDTH: 104px" runat="server">
											<option value="��ҵ" selected>��ҵ</option>
											<option value="��ʱ">��ʱ</option>
										</select>
									</td>
									<td width="40"></td>
									<td>����ģ��:</td>
									<td width="5"></td>
									<td><SELECT id="drpModule" style="WIDTH: 112px" disabled name="Select1" runat="server">
											<OPTION value="2000" selected>������</OPTION>
										</SELECT></td>
									<td width="7"></td>
								</tr>
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>��Ч��ʼ��:</td>
									<td width="5"></td>
									<td width="111"><asp:textbox id="txtDateBegins" runat="server" Width="101px"></asp:textbox></td>
									<td width="40"></td>
									<td>��Ч������:</td>
									<td width="5"></td>
									<td><asp:textbox id="txtDateEnds" runat="server" Width="113px"></asp:textbox></td>
									<td width="7"></td>
								</tr>
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td align="center" height="5"><input id="Ok" onclick="ok();" type="button" value="  ȷ��  ">&nbsp;<input onclick="window.close();" type="button" value="  ȡ��  "></td>
				</tr>
				<tr>
					<td>&nbsp;
					</td>
				</tr>
				<tr>
					<td height="5">
						<FIELDSET>
							<LEGEND>�������кŹ��ܲ���˵��:</LEGEND>
							<TABLE id="Table1" height="79" cellSpacing="5" cellPadding="1" border="0">
								<TR>
									<TD></TD>
									<TD><STRONG>��Ȩ����</STRONG></TD>
									<TD>��ҵ����ʱ</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>������</STRONG></TD>
									<TD>��ѡ��</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>������ʼ��&nbsp;</STRONG></TD>
									<TD>��ѡ����</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>����������&nbsp;</STRONG></TD>
									<TD>��ѡ����</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG></STRONG></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</FIELDSET></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
