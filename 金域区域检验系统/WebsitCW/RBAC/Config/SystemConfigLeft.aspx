<%@ Page language="c#" AutoEventWireup="True" Inherits="theNews.Config.SystemConfigLeft" Codebehind="SystemConfigLeft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>-系统管理
		</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../main/css.css" rel="stylesheet" type="text/css">
		</HEAD>
	<body bgcolor="#999999" topmargin="0" leftmargin="0" background="../main/image/index_1/leftbg.jpg">
		<TABLE id="Table1" style="WIDTH: 151px" cellSpacing="0" cellPadding="0" width="151" background="../main/image/index_1/leftbg.jpg"
			border="0">
			<TR>
				<TD height="27"><IMG height="27" src="../main/image/index_1/left1.jpg" width="149"></TD>
			</TR>
			<TR>
				<TD height="5"><IMG height="1" alt="" src="" width="1" name=""></TD>
			</TR>
			<TR>
				<TD vAlign="top">
					<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="149" border="0">
						<TR>
							<TD class="left" vAlign="middle" background="../main/image/index_1/left2.jpg" height="25">
								<DIV align="center"><A class="left" href="../mNews/newsMain.aspx?categoryid=00006" target="main">综合新闻</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD height="2"><IMG height="1" alt="" src="" width="1" name=""></TD>
						</TR>
						<TR>
							<TD>
								<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="149" background="../main/image/index_1/leftbg.jpg"
									border="0">
									<TR>
										<TD width="21">&nbsp;</TD>
										<TD>
											<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="102" border="0">
												<TR>
													<TD height="17" width="102" background="../main/image/index_1/left3.jpg">
														<DIV id="Div1" style="WIDTH: 101px; HEIGHT: 14px">
															<DIV align="center"><A class="left1" href="../mNews/newsMain.aspx?categoryid=00001" target="main">国内新闻</A></DIV>
														</DIV>
													</TD>
												</TR>
												<TR>
													<TD height="16" background="../main/image/index_1/left4.jpg">
														<DIV id="Div2" style="WIDTH: 101px; HEIGHT: 14px">
															<DIV align="center"><A class="left1" href="../mNews/newsMain.aspx?categoryid=0000000000" target="main">国际新闻</A></DIV>
														</DIV>
													</TD>
												</TR>
												<TR>
													<TD height="16" background="../main/image/index_1/left5.jpg">
														<DIV id="Layer1" style="WIDTH: 101px; HEIGHT: 14px">
															<DIV align="center"><A class="left1" href="../mNews/newsMain.aspx?categoryid=00003" target="main">综合新闻</A></DIV>
														</DIV>
													</TD>
												</TR>
											</TABLE>
											<P>&nbsp;</P>
										</TD>
										<TD width="26" background="../main/image/index_1/leftbg.jpg">&nbsp;</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD height="2"><IMG height="1" alt="" src="" width="1" name=""></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD>
					<TABLE id="Table5" height="121" cellSpacing="0" cellPadding="0" width="149" border="0">
						<TR>
							<TD background="../main/image/index_1/left6.jpg" height="24">
								<DIV class="left" align="center"><A class="left" href="#">公司动态</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="left" background="../main/image/index_1/left7.jpg" height="24">
								<DIV align="center"><A class="left" href="#">待办提示</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="left" background="../main/image/index_1/left8.jpg" height="24">
								<DIV class="left" align="center"><A class="left" href="#">待办业务</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="left" background="../main/image/index_1/left9.jpg" height="24">
								<DIV align="center"><A class="left" href="#">最新邮件</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="left" background="../main/image/index_1/left10.jpg" height="25">
								<DIV align="center"><A class="left" href="#">公共服务</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="left" background="../main/image/index_1/left10.jpg" height="25">
								<DIV align="center"><A class="left" href="../Config/SystemConfig.aspx" target="main">系统管理</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="left" background="../main/image/index_1/left10.jpg" height="25">
								<DIV align="center"><A href="/webdir" target="_blank">文档管理</A></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="left" background="../main/image/index_1/left10.jpg" height="25">
								<DIV align="center"><A href="../documents/公用文档/default.htm" target="_blank">文档管理1</A></DIV>
							</TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>

