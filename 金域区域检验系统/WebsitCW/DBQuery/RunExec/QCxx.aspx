<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.RunExec.QCxx" Codebehind="QCxx.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QCxx</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/Style/cssnew.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="100%"
				cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
							<TR height="50">
								<TD align="center"><FONT face="宋体">分析检查质控统计</FONT></TD>
							</TR>
							<TR height="40">
								<TD class="labTxt"><FONT face="宋体">起始日期
										<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>至
										<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
										<asp:Button id="Button2" runat="server" Text="查询"></asp:Button></FONT></TD>
							</TR>
							<TR>
								<TD class="labTxt" style="HEIGHT: 136px">
									<TABLE id="Table3" cellSpacing="1" width="100%" border="0" height="100%" bgcolor="black">
										<thead bgcolor="#cccccc">
											<TR height="10%">
												<TD class="labTxt"><FONT face="宋体">检验项目</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">标本类型</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">检验类别</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">总数</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">完成检查(%)</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">未完成检查(%)</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">失败(%)</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">正常(%)</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">异常(%)</FONT></TD>
												<TD class="labTxt"><FONT face="宋体">其他(%)</FONT></TD>
											</TR>
										</thead>
										<tbody bgcolor="#ffffff" valign="bottom">
											<TR>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox14" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox15" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox16" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox17" runat="server" Width="50px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox18" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox19" runat="server" Width="100px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox20" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox21" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox22" runat="server" Width="80px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox23" runat="server" Width="80px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox3" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox4" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox5" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox6" runat="server" Width="50px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox7" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox9" runat="server" Width="100px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox11" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox13" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox10" runat="server" Width="80px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="TextBox8" runat="server" Width="80px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox24" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox25" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox26" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox27" runat="server" Width="50px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox28" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox29" runat="server" Width="100px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox30" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox31" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox32" runat="server" Width="80px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox33" runat="server" Width="80px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox34" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox35" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox36" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox37" runat="server" Width="50px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox38" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox39" runat="server" Width="100px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox40" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox41" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox42" runat="server" Width="80px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox43" runat="server" Width="80px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox44" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox45" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox46" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox47" runat="server" Width="50px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox48" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox49" runat="server" Width="100px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox50" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox51" runat="server" Width="90px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox52" runat="server" Width="80px"></asp:TextBox></TD>
												<TD class="labTxt" style="HEIGHT: 30px">
													<asp:TextBox id="Textbox53" runat="server" Width="80px"></asp:TextBox></TD>
											</TR>
										</tbody>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 52px">
									<TABLE id="Table5" width="100%" border="0">
										<TR>
											<TD>月报表
											</TD>
										</TR>
										<TR>
											<TD>
												<TABLE id="Table3" cellSpacing="1" width="100%" border="0" height="100%" bgcolor="black">
													<thead bgcolor="#cccccc">
														<TR height="10%">
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">检验项目</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">标本类型</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">检验类别</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">总数</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">完成检查(%)</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">未完成检查(%)</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">失败(%)</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">正常(%)</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">异常(%)</FONT></TD>
															<TD class="labTxt" style="HEIGHT: 3.99%"><FONT face="宋体">其他(%)</FONT></TD>
														</TR>
													</thead>
													<tbody bgcolor="#ffffff">
														<TR>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox12" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox54" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox55" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox56" runat="server" Width="50px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox57" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox58" runat="server" Width="100px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox59" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox60" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox61" runat="server" Width="80px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox62" runat="server" Width="80px"></asp:TextBox></TD>
														</TR>
														<TR>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox73" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox74" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox75" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox76" runat="server" Width="50px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox77" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox78" runat="server" Width="100px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox79" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox80" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox81" runat="server" Width="80px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox82" runat="server" Width="80px"></asp:TextBox></TD>
														</TR>
													</tbody>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD>&nbsp;
									<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD>年报表</TD>
										</TR>
										<TR>
											<TD>
												<TABLE id="Table3" cellSpacing="1" width="100%" border="0" height="100%" bgcolor="black">
													<thead bgcolor="#cccccc">
														<TR height="10%">
															<TD class="labTxt"><FONT face="宋体">检验项目</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">标本类型</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">检验类别</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">总数</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">完成检查(%)</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">未完成检查(%)</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">失败(%)</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">正常(%)</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">异常(%)</FONT></TD>
															<TD class="labTxt"><FONT face="宋体">其他(%)</FONT></TD>
														</TR>
													</thead>
													<tbody bgcolor="#ffffff">
														<TR>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox63" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox64" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox65" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox66" runat="server" Width="50px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox67" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox68" runat="server" Width="100px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox69" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox70" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox71" runat="server" Width="80px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox72" runat="server" Width="80px"></asp:TextBox></TD>
														</TR>
														<TR>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox83" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox84" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox85" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox86" runat="server" Width="50px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox87" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox88" runat="server" Width="100px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox89" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox90" runat="server" Width="90px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox91" runat="server" Width="80px"></asp:TextBox></TD>
															<TD class="labTxt" style="HEIGHT: 15px">
																<asp:TextBox id="Textbox92" runat="server" Width="80px"></asp:TextBox></TD>
														</TR>
													</tbody>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD align="center">
									<asp:Button id="Button1" runat="server" Text="保存"></asp:Button></TD>
							</TR>
							<TR>
								<TD align="right"><FONT face="宋体">
										<asp:HyperLink id="Hyperlink2" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=226&amp;WindowSize=Max&amp;RBACModuleID=487">返回(主界面)</asp:HyperLink>
										<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=230&amp;RBACModuleID=492">返回实验室质控</asp:HyperLink></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
