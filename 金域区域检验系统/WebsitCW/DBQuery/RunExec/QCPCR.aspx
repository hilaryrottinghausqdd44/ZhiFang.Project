<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.RunExec.QCPCR" Codebehind="QCPCR.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QCPCR</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
	<body MS_POSITIONING="GridLayout" language="javascript" onLoad="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" width="800" border="0">
				<TR>
					<TD align="center">染色体检查质控统计</TD>
				</TR>
				<TR>
					<TD class="labTxt">检查项目
						<asp:DropDownList id="DropDownList1" runat="server" Width="160px">
							<asp:ListItem Value="骨髓">骨髓</asp:ListItem>
							<asp:ListItem Value="淋巴结">淋巴结</asp:ListItem>
							<asp:ListItem Value="实体肿瘤">实体肿瘤</asp:ListItem>
							<asp:ListItem Value="羊水">羊水</asp:ListItem>
							<asp:ListItem Value="绒毛膜">绒毛膜</asp:ListItem>
							<asp:ListItem Value="其他">其他</asp:ListItem>
						</asp:DropDownList>日期
						<asp:TextBox id="sTime" runat="server"></asp:TextBox>至
						<asp:TextBox id="eTime" runat="server"></asp:TextBox>
						<asp:Button id="queryBtn" runat="server" Text="查 询"></asp:Button></TD>
				</TR>
				<TR>
					<TD class="labTxt">
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="TabInfo" cellSpacing="1" width="100%" border="0" height="100%" bgcolor="black"
							runat="server">
							<thead bgcolor="#cccccc">
								<TR height="10%" bgcolor="#ffffff">
									<TD class="labTxt">起始日期</TD>
									<TD class="labTxt">总数</TD>
									<TD class="labTxt">完成检查(%)</TD>
									<TD class="labTxt">未完成检查(%)</TD>
									<TD class="labTxt">失败(%)</TD>
									<TD class="labTxt">正常(%)</TD>
									<TD class="labTxt">异常(%)</TD>
									<TD class="labTxt">其他(%)</TD>
								</TR>
							</thead>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="saveBtn" runat="server" Text="保 存"></asp:Button></TD>
				</TR>
				<TR>
					<TD align="right">
						<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=226&amp;WindowSize=Max&amp;RBACModuleID=487">返回(主界面)</asp:HyperLink>
						<asp:HyperLink id="Hyperlink2" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=230&amp;RBACModuleID=492">返回实验室质控</asp:HyperLink>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
