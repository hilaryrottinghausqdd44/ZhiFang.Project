<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.TopBar" Codebehind="TopBar.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="admin.css" type="text/css" rel="stylesheet">
		<LINK href="css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function openWide()
			{
				var frm=parent.fset;
				if(frm.rows!="60,*")
				{
					frm.rows=frm.rows.replace("0,*","60,*");
				}
				else
				{
					frm.rows=frm.rows.replace("60,*","0,*");
				}
			}
		</script>
	</HEAD>
	<body>
		<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<!--BEGIN ONE LINE-->
				<td width="20">&nbsp;</td>
				<TD style="WIDTH: 32px" valign="top"><A href="Main.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content"><IMG src="images/icons/0041_a.gif" border="0"></A></TD>
				<td valign="top">
					<TABLE id="Table4" height="100%" width="40" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD colSpan="2" valign="top">&nbsp;<FONT face="黑体" size="3"><%//=Request.QueryString["Name"]%></FONT></TD>
						</TR>
						<TR>
							<TD colSpan="2" valign="top">&nbsp;</TD>
						</TR>
					</TABLE>
				</td>
				<td>
					<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD valign="top"><A href="admin/TablesConfig.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content"><IMG src="images/Table_Ico.gif" border="0"></A></TD>
							<TD valign="top"><IMG src="images/Query_Ico.gif"></TD>
						</TR>
						<TR>
							<TD colSpan="2"><A href="admin/TablesConfig.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content">功能管理</A></TD>
						</TR>
					</TABLE>
				</td>
				<td>
					<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD valign="top"><A href="admin/UIConfig.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content"><IMG src="images/icons/0031_b.gif" border="0"></A></TD>
							<TD valign="top"><IMG src="images/icons/0049_b.gif"></TD>
						</TR>
						<TR>
							<TD colSpan="2"><A href="admin/UIConfig.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content">界面管理</A></TD>
						</TR>
					</TABLE>
				</td>
				<td>
					<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD valign="top"><A href="Style/StyleAdmin.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content"><IMG src="images/icons/0004_b.gif" border="0"></A></TD>
							<TD valign="top"><IMG src="images/icons/0005_b.gif"></TD>
						</TR>
						<TR>
							<TD colSpan="2"><A href="Style/StyleAdmin.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content">风格应用</A></TD>
						</TR>
					</TABLE>
				</td>
				<td>
					<%--DataRightFramework.aspx--%>
					<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD valign="top"><A href="DataRight/RightXmlEdit.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content"><IMG src="images/icons/0075_b.gif" border="0"></a></TD>
							<TD valign="top"><A title="流程控制" href="DataRight/RightXmlEditFlow.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content"><IMG src="images/icons/0049_b.gif" border="0"></a></TD>
						</TR>
						<TR>
							<TD colSpan="2"><A href="DataRight/PermissionFramework.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content">系统设置</A></TD>
						</TR>
					</TABLE>
				</td>
				<td>
					<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD valign="top"><a href="Analysis/AnalysisMain.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content"><IMG src="images/icons/0022_b.gif" border="0"></a></TD>
							<TD valign="top"><a href="Analysis/AnalysisConfig.aspx?<%=Request.ServerVariables["Query_String"]%>" target="_blank"><IMG src="images/icons/0044_b.gif" border="0"></a></TD>
						</TR>
						<TR>
							<TD colSpan="2"><a href="Analysis/AnalysisMain.aspx?<%=Request.ServerVariables["Query_String"]%>" target="Content">数据统计</a></TD>
						</TR>
					</TABLE>
				</td>
				<!--
				<td>
					<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD valign="top"><IMG src="images/icons/0067_b.gif"></TD>
							<TD valign="top"><IMG src="images/icons/0060_b.gif"></TD>
						</TR>
						<TR>
							<TD colSpan="2">使用帮助</TD>
						</TR>
					</TABLE>
				</td>-->
				<!--END ONE LINE-->
				<td width="360" height="45"><IMG onclick="openWide()" id="img1" style="cursor:nw-resize;PADDING-RIGHT: 10px; PADDING-LEFT: 13px; FILTER: progid:DXImageTransform.Microsoft.Alpha( style=1,opacity=0,finishOpacity=80,startX=10,finishX=100,startY=100,finishY=0); FONT: bold 9pt/1.3 verdana; WIDTH: 360px; COLOR: darkred; HEIGHT: 59px; BACKGROUND-COLOR: skyblue"
						height="59" src="images/logo/logo.jpg" width="360"></td>
			</tr>
		</table>
		
	</body>
</HTML>
