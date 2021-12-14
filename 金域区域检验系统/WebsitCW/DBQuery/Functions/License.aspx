<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Functions.License1" Codebehind="License.aspx.cs" %>
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<style type="text/css">BODY {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
A {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
TABLE {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
DIV {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
SPAN {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
TD {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
TH {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
INPUT {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
}
SELECT {
	FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif
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
							<legend>检验之星主程序注册号生成模块</legend>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>网卡号:</td>
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
									<TD>授权码:</TD>
									<TD width="5"></TD>
									<TD width="216"><asp:textbox id="TextBoxLicenseNo" runat="server" Width="234px"></asp:textbox></TD>
									<TD width="40" colSpan="4"><asp:button id="ButtonCreate" runat="server" Text="生成授权号" onclick="ButtonCreate_Click"></asp:button></TD>
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
							<legend>授权有效规则</legend>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>授权类型:</td>
									<td width="5"></td>
									<td width="111"><select id="drpLicenseType" style="WIDTH: 104px" runat="server">
											<option value="商业" selected>商业</option>
											<option value="临时">临时</option>
										</select>
									</td>
									<td width="40"></td>
									<td>程序模块:</td>
									<td width="5"></td>
									<td><SELECT id="drpModule" style="WIDTH: 112px" disabled name="Select1" runat="server">
											<OPTION value="2000" selected>主程序</OPTION>
										</SELECT></td>
									<td width="7"></td>
								</tr>
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>有效期始于:</td>
									<td width="5"></td>
									<td width="111"><asp:textbox id="txtDateBegins" runat="server" Width="101px"></asp:textbox></td>
									<td width="40"></td>
									<td>有效期至于:</td>
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
					<td align="center" height="5"><input id="Ok" onclick="ok();" type="button" value="  确定  ">&nbsp;<input onclick="window.close();" type="button" value="  取消  "></td>
				</tr>
				<tr>
					<td>&nbsp;
					</td>
				</tr>
				<tr>
					<td height="5">
						<FIELDSET>
							<LEGEND>生成序列号功能参数说明:</LEGEND>
							<TABLE id="Table1" height="79" cellSpacing="5" cellPadding="1" border="0">
								<TR>
									<TD></TD>
									<TD><STRONG>授权类型</STRONG></TD>
									<TD>商业，临时</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>网卡号</STRONG></TD>
									<TD>可选择</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>主程序开始日&nbsp;</STRONG></TD>
									<TD>可选择传入</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>主程序到期日&nbsp;</STRONG></TD>
									<TD>可选择传入</TD>
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
