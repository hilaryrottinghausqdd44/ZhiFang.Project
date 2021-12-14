<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.RunExec.QCFish" Codebehind="QCFish.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QCFish</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/Style/cssnew.css" type="text/css" rel="stylesheet">
		<script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() {

}
function ShowInfo()
{
	
}
//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" language="javascript" onload="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="labTxt">
							<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR height="40">
									<TD align="center">FISH质控</TD>
								</TR>
								<TR height="40">
									<TD align="left" class="labTxt">染色体号
										<asp:TextBox id="TextBox38" runat="server"></asp:TextBox>探针名称
										<asp:TextBox id="TextBox39" runat="server"></asp:TextBox>
										<asp:Button id="Button1" runat="server" Text="查询"></asp:Button></TD>
								</TR>
								<TR>
									<TD class="labTxt" vAlign="top" style="HEIGHT: 102px">
										<TABLE id="Table3" cellSpacing="1" width="100%" border="0" bgColor="black">
											<TBODY>
												<TR bgcolor="#ffffff">
													<TD class="labTxt" style="HEIGHT: 17px">染色体号数</TD>
													<TD class="labTxt" style="HEIGHT: 17px">探针名称</TD>
													<TD class="labTxt" style="HEIGHT: 17px">染色体号数</TD>
													<TD class="labTxt" style="HEIGHT: 17px">探针名称</TD>
												</TR>
												<TR bgcolor="#ffffff">
													<TD style="HEIGHT: 19px"></TD>
													<TD style="HEIGHT: 19px" onclick="ShowInfo()">
														<asp:LinkButton id="LinkButton1" runat="server" onclick="LinkButton1_Click">1p36/1q25</asp:LinkButton></TD>
													<TD style="HEIGHT: 19px"></TD>
													<TD style="HEIGHT: 19px">Cep12</TD>
												</TR>
												<TR bgcolor="#ffffff">
													<TD>1</TD>
													<TD>
														<asp:LinkButton id="LinkButton2" runat="server">Wcp1(so)</asp:LinkButton></TD>
													<TD>12</TD>
													<TD>Wcp12(so)</TD>
												</TR>
												<TR bgcolor="#ffffff">
													<TD></TD>
													<TD>
														<asp:LinkButton id="LinkButton3" runat="server">Wcp1(sg)</asp:LinkButton></TD>
									</TD>
									<TD></TD>
									<TD>Wcp12(sg)</TD>
								</TR>
							</TABLE>
							点击每个探针都会出现下列原始记录
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:Panel id="pnlInfo" runat="server" Visible="False">
								<TABLE id="Table4" cellSpacing="1" cellPadding="0" width="100%" bgColor="black" border="0">
									<TR bgColor="#ffffff">
										<TD class="labTxt">日期</TD>
										<TD class="labTxt">探针批号</TD>
										<TD class="labTxt">细胞总数</TD>
										<TD class="labTxt">Pattern1/细胞数</TD>
										<TD class="labTxt">Pattern2/细胞数</TD>
										<TD class="labTxt">Pattern3/细胞数</TD>
										<TD class="labTxt">Pattern4/细胞数</TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox5" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox9" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox13" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox17" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox21" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox25" runat="server"></asp:TextBox></TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD>
											<asp:TextBox id="TextBox2" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox6" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox10" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox14" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox18" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox22" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox26" runat="server"></asp:TextBox></TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD>
											<asp:TextBox id="TextBox3" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox7" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox11" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox15" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox19" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox23" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox27" runat="server"></asp:TextBox></TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD>
											<asp:TextBox id="TextBox4" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox8" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox12" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox16" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox20" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox24" runat="server"></asp:TextBox></TD>
										<TD>
											<asp:TextBox id="TextBox28" runat="server"></asp:TextBox></TD>
									</TR>
								</TABLE>
							</asp:Panel>
						</TD>
					</TR>
					<TR>
						<TD class="labTxt">
							<asp:Panel id="pnlStat" runat="server" Visible="False">
								<TABLE id="Table5" cellSpacing="1" width="100%" bgColor="black" border="0">
									<TR bgColor="#ffffff">
										<TD class="labTxt" colSpan="5">对一段时期的统计的样本量,自动统计次数</TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD class="labTxt" style="HEIGHT: 23px"></TD>
										<TD class="labTxt" style="HEIGHT: 23px">统计结果(如S)</TD>
										<TD class="labTxt" style="HEIGHT: 23px">统计结果(如X)</TD>
										<TD class="labTxt" style="HEIGHT: 23px">统计结果(如V)</TD>
										<TD class="labTxt" style="HEIGHT: 23px">统计结果</TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD class="labTxt">Pattern1</TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox29" runat="server"></asp:TextBox></TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox32" runat="server"></asp:TextBox></TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox35" runat="server"></asp:TextBox></TD>
										<TD class="labTxt"></TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD class="labTxt">Pattern2</TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox30" runat="server"></asp:TextBox></TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox33" runat="server"></asp:TextBox></TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox36" runat="server"></asp:TextBox></TD>
										<TD class="labTxt"></TD>
									</TR>
									<TR bgColor="#ffffff">
										<TD class="labTxt">Pattern3</TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox31" runat="server"></asp:TextBox></TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox34" runat="server"></asp:TextBox></TD>
										<TD class="labTxt">
											<asp:TextBox id="TextBox37" runat="server"></asp:TextBox></TD>
										<TD class="labTxt"></TD>
									</TR>
								</TABLE>
							</asp:Panel>
						</TD>
					</TR>
					<TR>
						<TD class="labTxt" align="right">
							<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=226&WindowSize=Max&RBACModuleID=487">返回(主界面)</asp:HyperLink>
							<asp:HyperLink id="HyperLink2" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/RBAC/MODULES/ModuleRun.aspx?ModuleID=492">返回(实验室质控)</asp:HyperLink></TD>
					</TR>
				</TABLE>
				</TD></TR></TBODY></TABLE> </FONT>
		</form>
	</body>
</HTML>
