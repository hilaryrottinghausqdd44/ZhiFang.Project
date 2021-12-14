<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.RunExec.QCReagent" Codebehind="QCReagent.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QCReagent</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/Style/cssnew.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="100%"
					cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
								<TR>
									<TD align="center"><B style="mso-bidi-font-weight: normal">试剂消耗品质控</B></TD>
								</TR>
								<TR>
									<TD align="center">
										<TABLE id="Table8" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="labTxt" style="WIDTH: 174px" align="right">批号</TD>
												<TD class="labTxt" style="WIDTH: 203px" align="left">
													<asp:TextBox id="txtID" runat="server"></asp:TextBox></TD>
												<TD class="labTxt" style="WIDTH: 91px" align="right">日期</TD>
												<TD class="labTxt" style="WIDTH: 329px" align="left">
													<asp:TextBox id="txtDate" runat="server"></asp:TextBox>-
													<asp:TextBox id="TextBox79" runat="server"></asp:TextBox></TD>
												<TD class="labTxt" align="left">
													<asp:Button id="btnSearch" runat="server" Text="查询"></asp:Button></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD valign="bottom" style="HEIGHT: 138px">
										<TABLE id="Table3" cellSpacing="1" width="50%" border="0" bgColor="black">
											<TR bgcolor="#ffffff">
												<TD align="right" class="labTxt">质控类型</TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox1" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD align="right" class="labTxt">目前批号</TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox2" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD align="right" class="labTxt">新进批号</TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox3" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD align="right" class="labTxt">所做例数</TD>
												<TD>
													<asp:TextBox id="TextBox4" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD align="right" class="labTxt">无菌检查</TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox5" runat="server">长菌/没长菌</asp:TextBox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="labTxt">支持细胞培养的测试</TD>
											</TR>
											<TR>
												<TD class="labTxt">
													<TABLE id="Table5" cellSpacing="1" cellPadding="0" width="100%" border="1" bgColor="black">
														<TR bgcolor="#ffffff">
															<TD class="labTxt">检验编号</TD>
															<TD class="labTxt">目前批号百分率（分裂像数/1000个细胞）</TD>
															<TD class="labTxt">新进批号百分率（分裂像数/1000个细胞）</TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox9" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox10" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox11" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox14" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox13" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox12" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox15" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox16" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox17" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox18" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox19" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox20" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox23" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox22" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox21" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox24" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox25" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox27" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox26" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox29" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox28" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox30" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox31" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox34" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox32" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox33" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox35" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">
																<asp:TextBox id="TextBox38" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox37" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox36" runat="server"></asp:TextBox></TD>
														</TR>
														<TR bgcolor="#ffffff">
															<TD class="labTxt">统计均值</TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox39" runat="server"></asp:TextBox></TD>
															<TD class="labTxt">
																<asp:TextBox id="TextBox40" runat="server"></asp:TextBox></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD style="WIDTH: 76px; HEIGHT: 22px" align="right" class="labTxt">结论</TD>
												<TD style="HEIGHT: 22px" class="labTxt">
													<asp:TextBox id="TextBox6" runat="server">合格/不合格</asp:TextBox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px; HEIGHT: 22px" align="right" class="labTxt">分析人员</TD>
												<TD style="HEIGHT: 22px" class="labTxt">
													<asp:TextBox id="TextBox7" runat="server"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD align="right" class="labTxt" style="WIDTH: 76px">日期</TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox8" runat="server"></asp:TextBox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table7" cellSpacing="1" cellPadding="0" width="100%" border="1" bgColor="red">
											<TR bgcolor="#ffffff">
												<TD colspan="4" class="labTxt">（另有一个试剂消耗品质控统计，如一个公司同一试剂不同批号的合格/不合格率）</TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt">公司</TD>
												<TD class="labTxt">试剂</TD>
												<TD class="labTxt">批号</TD>
												<TD class="labTxt">合格/不合格</TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt"><asp:TextBox id="TextBox41" runat="server">1</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox42" runat="server">1</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox43" runat="server">1</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox44" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt"><asp:TextBox id="TextBox48" runat="server"></asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox47" runat="server"></asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox46" runat="server">2</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox45" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt"><asp:TextBox id="TextBox49" runat="server"></asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox62" runat="server"></asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox63" runat="server">3</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox80" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt">
													<asp:TextBox id="TextBox50" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox61" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox64" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">总的合格情况</TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt"><asp:TextBox id="TextBox51" runat="server"></asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox60" runat="server">2</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox65" runat="server">1</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox78" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt"><asp:TextBox id="TextBox52" runat="server"></asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox55" runat="server"></asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox66" runat="server">2</asp:TextBox></TD>
												<TD class="labTxt"><asp:TextBox id="TextBox77" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt">
													<asp:TextBox id="TextBox53" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox56" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox67" runat="server">3</asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox76" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt">
													<asp:TextBox id="TextBox54" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox58" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox68" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox75" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt">
													<asp:TextBox id="TextBox57" runat="server">2</asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox59" runat="server">3</asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox69" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox74" runat="server"></asp:TextBox></TD>
											</TR>
											<TR bgcolor="#ffffff">
												<TD class="labTxt">
													<asp:TextBox id="TextBox72" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox71" runat="server">4</asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox70" runat="server"></asp:TextBox></TD>
												<TD class="labTxt">
													<asp:TextBox id="TextBox73" runat="server"></asp:TextBox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" class="labTxt">
									
									<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?">返回（主界面）</asp:HyperLink></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
