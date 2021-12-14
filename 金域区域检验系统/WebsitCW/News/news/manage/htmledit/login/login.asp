<!--#include file="../../inc/conn.asp"-->
<!--#include file="../../inc/config.asp"-->
<HTML>
	<HEAD>
		<TITLE>
			<%=rs_config("WebName")%>
			--后台管理登陆</TITLE>
		<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">
		<link rel="stylesheet" href="../Inc/styles.css" type="text/css">
			<style type="text/css"> <!-- .style1 {color: #66FFCC}
	--></style>
	</HEAD>
	<BODY BGCOLOR="#d6dbef" LEFTMARGIN="0" TOPMARGIN="0" MARGINWIDTH="0" MARGINHEIGHT="0">
		<form method="post" action="checklogin.asp" name="form1">
			<!-- ImageReady Slices (未标题-2.psd) -->
			<table border="0" cellpadding="0" style="BORDER-COLLAPSE: collapse" width="100%" id="table1"
				height="100%">
				<tr>
					<td align="center">
						<TABLE WIDTH="639" BORDER="0" CELLPADDING="0" CELLSPACING="0" id="table2">
							<TR>
								<TD COLSPAN="8">
									智方</TD>
							</TR>
							<TR>
								<TD COLSPAN="4">
									<IMG SRC="images/login_2.gif" WIDTH="320" HEIGHT="87" ALT=""></TD>
								<TD COLSPAN="3" width="315" height="87" bgcolor="#013467" valign="top">
									<table border="0" cellpadding="0" style="BORDER-COLLAPSE: collapse" width="100%" id="table4">
										<tr>
											<td width="270">
												<table border="0" cellpadding="0" style="BORDER-COLLAPSE: collapse" id="table5" align="right">
													<tr>
														<td><font size="2" color="#90a4dd">版本:</font><font size="2" color="#c0ccff"><%=rs_config("EditionNum")%></font></td>
														<td width="15" align="center">
															<font size="2" color="#c0ccff"><img border="0" src="images/1.gif" width="6" height="28" align="left"></font></td>
														<td><font size="2" color="#90a4dd">语言:</font><font size="2" color="#c0ccff"><%=rs_config("Language")%></font></td>
														<td width="15" align="center">
															<p align="center"><font size="2" color="#c0ccff"> <img border="0" src="images/1.gif" width="6" height="28">
																</font>
															</p>
														</td>
														<td><font size="2" color="#90a4dd">数据库:</font><font size="2" color="#c0ccff"><%=rs_config("DataBase")%></font></td>
													</tr>
												</table>
											</td>
											<td width="54" nowrap>
												<a href="../../../" class="style1">主页</a></td>
										</tr>
									</table>
								</TD>
								<TD ROWSPAN="8">
									<IMG SRC="images/login_4.gif" WIDTH="4" HEIGHT="418" ALT=""></TD>
							</TR>
							<TR>
								<TD ROWSPAN="7">
									<IMG SRC="images/login_5.gif" WIDTH="4" HEIGHT="331" ALT=""></TD>
								<TD ROWSPAN="6">
									<IMG SRC="images/login_6.jpg" WIDTH="279" HEIGHT="326" ALT=""></TD>
								<TD COLSPAN="5" width="352" height="48" background="images/login_7.gif">
									<b><font color="#ffffff" face="MingLiU">
											<%'=rs_config("EditionName")%>
										</font></b>
								</TD>
							</TR>
							<TR>
								<TD COLSPAN="4">
									<IMG SRC="images/login_8.gif" WIDTH="115" HEIGHT="18" ALT=""></TD>
								<TD ROWSPAN="3" width="237" height="125" valign="top" bgcolor="#003366">
									<table border="0" cellpadding="0" style="BORDER-COLLAPSE: collapse" width="100%" id="table3"
										height="107">
										<tr>
											<td height="29" valign="bottom" colspan="2">&nbsp;<span style="LETTER-SPACING: 2px"><font color="#90a4dd" size="2">输入登陆名:</font></span></td>
										</tr>
										<tr>
											<td height="26" valign="bottom" colspan="2">&nbsp;<input name="adminname" type="text" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
													value="test" size="27"></td>
										</tr>
										<tr>
											<td height="23" valign="bottom" colspan="2">&nbsp;<span style="LETTER-SPACING: 2px"><font color="#90a4dd" size="2">输入登陆密码:</font></span></td>
										</tr>
										<tr>
											<td width="52%">&nbsp;<input name="adminpassword" type="password" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
													value="test" size="14"></td>
											<td valign="bottom" width="48%">
												<input border="0" src="images/button.gif" name="I1" width="83" height="33" type="image"></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD ROWSPAN="2">
									<IMG SRC="images/login_10.gif" WIDTH="10" HEIGHT="107" ALT=""></TD>
								<TD COLSPAN="2" width="91" height="89">
									<img border="0" src="images/eMac-Front-OS-X.gif" width="91" height="89"></TD>
								<TD ROWSPAN="2">
									<IMG SRC="images/login_12.gif" WIDTH="14" HEIGHT="107" ALT=""></TD>
							</TR>
							<TR>
								<TD COLSPAN="2">
									<IMG SRC="images/login_13.gif" WIDTH="91" HEIGHT="18" ALT=""></TD>
							</TR>
							<TR>
								<TD COLSPAN="5">
									<IMG SRC="images/login_14.gif" WIDTH="352" HEIGHT="24" ALT=""></TD>
							</TR>
							<TR>
								<TD COLSPAN="5">
									<IMG SRC="images/login_15.gif" WIDTH="352" HEIGHT="129" ALT=""></TD>
							</TR>
							<TR>
								<TD COLSPAN="6">
									<IMG SRC="images/login_16.gif" WIDTH="631" HEIGHT="5" ALT=""></TD>
							</TR>
							<TR>
								<TD WIDTH="4" NOWRAP></TD>
								<TD WIDTH="279" NOWRAP></TD>
								<TD WIDTH="10" NOWRAP></TD>
								<TD WIDTH="27" NOWRAP></TD>
								<TD WIDTH="64" NOWRAP></TD>
								<TD WIDTH="14" NOWRAP></TD>
								<TD WIDTH="237" NOWRAP></TD>
								<TD WIDTH="4" NOWRAP></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<!-- End ImageReady Slices --></form>
		<%
Call configclose()
%>
	</BODY>
</HTML>
