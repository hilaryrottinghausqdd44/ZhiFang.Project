<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.RunExec.CaseManage" Codebehind="CaseManage.aspx.cs" %>
<%@ Import Namespace="System.Xml"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CaseManage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/Style/cssnew.css" type="text/css" rel="stylesheet">
		<!--#include file="../../Includes/JS/Calendar.js"-->
		<!--script language="javascript" src=../../Includes/JS/Calendar.js></script-->
		<script id="clientEventHandlersJS" language="javascript">
<!--

function window_onunload() {

}
function aLink()
{
	var formid = document.getElementById('txtValue').value;
	
	window.open('CaseInfo.aspx?id='+ formid,'Info','width=800,height=300');
}
function Finish()
{
	var formid = document.getElementById('txtFinish').value;
	
	window.open('CaseInfo.aspx?id='+ formid,'Info','width=800,height=300');
}
function NotFinish()
{
	var formid = document.getElementById('txtNoFinish').value;
	
	window.open('CaseInfo.aspx?id='+ formid,'Info','width=800,height=300');
}
//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" language="javascript" onunload="return window_onunload()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center">病例管理</TD>
							</TR>
							<TR>
								<TD class="labTxt">日期
									<asp:textbox id="txtDateStrat" runat="server" onfocus="setday(this)"></asp:textbox><asp:button id="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD>
									<TABLE id="Table3" cellSpacing="1" cellPadding="0" width="800" bgColor="black" border="0">
										<thead bgColor="#cccccc">
											<TR bgColor="#cccccc">
												<TD class="labTxt" width="94">检验小组</TD>
												<TD class="labTxt" width="120">检验类别</TD>
												<TD class="labTxt" width="114">应完成的病例数</TD>
												<TD class="labTxt" width="114">已完成的病例数</TD>
												<TD class="labTxt" width="114">待完成的病例数</TD>
											</TR>
										</thead>
										<tbody>
											<TR vAlign="bottom" align="center" bgColor="#ffffff">
												<TD class="labTxt" rowSpan="2">染色体检查</TD>
												<TD class="labTxt">遗传学染色体检查</TD>
												<TD class="labTxt"><a href="#" onclick="aLink()"><%=ycxYcount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="Finish()"><%=ycxCount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="NotFinish()"><%=ycxNcount%></a></TD>
											</TR>
											<TR vAlign="bottom" align="center" bgColor="#ffffff">
												<TD class="labTxt">肿瘤染色体检查</TD>
												<TD class="labTxt"><a href="#" onclick="aLink()"><%=zlYcount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="Finish()"><%=zlCount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="NotFinish()"><%=zlNcount%></a></TD>
											</TR>
											<TR vAlign="bottom" align="center" bgColor="#ffffff">
												<TD class="labTxt">FISH检查</TD>
												<TD class="labTxt">FISH检查</TD>
												<TD class="labTxt"><a href="#" onclick="aLink()"><%=FISHYcount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="Finish()"><%=FISHCount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="NotFinish()"><%=FISHNcount%></a></TD>
											</TR>
											<TR vAlign="bottom" align="center" bgColor="#ffffff">
												<TD class="labTxt" rowSpan="2">分子生物学检查</TD>
												<TD class="labTxt">分子病理学检查</TD>
												<TD class="labTxt"><a href="#" onclick="aLink()"><%=fzblYcount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="Finish()"><%=fzblCount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="NotFinish()"><%=fzblNcount%></a></TD>
											</TR>
											<TR vAlign="bottom" align="center" bgColor="#ffffff">
												<TD class="labTxt">分子遗传学检查</TD>
												<TD class="labTxt"><a href="#" onclick="aLink()"><%=fzycxYcount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="Finish()"><%=fzycxCount%></a></TD>
												<TD class="labTxt"><a href="#" onclick="NotFinish()"><%=fzycxNcount%></a></TD>
											</TR>
											<TR vAlign="bottom" align="center" bgColor="#ffffff">
												<TD class="labTxt"></TD>
												<TD class="labTxt">总数</TD>
												<TD class="labTxt"><%=YsumCount%></TD>
												<TD class="labTxt"><%=sumCount%></TD>
												<TD class="labTxt"><%=NsumCount%></TD>
											</TR>
										</tbody>
									</TABLE>
									<p><FONT face="宋体"></FONT>&nbsp;</p>
								</TD>
							</TR>
							<TR>
								<TD><FONT face="宋体"></FONT></TD>
							</TR>
							<TR>
								<TD></TD>
							</TR>
							<TR>
								<TD align="right"><asp:hyperlink id="HyperLink1" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=226&amp;WindowSize=Max&amp;RBACModuleID=487">返回(总界面)</asp:hyperlink></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="txtValue" type="hidden" name="Hidden1" runat="server"> <INPUT id="txtFinish" type="hidden" name="Finish" runat="server">
			<INPUT id="txtNoFinish" type="hidden" name="NotFinish" runat="server">
		</form>
	</body>
</HTML>
