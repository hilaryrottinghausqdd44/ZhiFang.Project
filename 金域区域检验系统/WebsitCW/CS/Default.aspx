<%@ Import Namespace="System.Xml" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.CS._Default" Codebehind="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<OBJECT id="ThCrypt" codeBase="../Includes/Activx/ThCrypt.CAB" height="1" width="1" classid="clsid:0D17F212-7827-4588-B0E9-827E356CDF5A"
			name="ThCrypt" VIEWASTEXT visible="false">
			<PARAM NAME="_Version" VALUE="65536">
			<PARAM NAME="_ExtentX" VALUE="26">
			<PARAM NAME="_ExtentY" VALUE="26">
			<PARAM NAME="_StockProps" VALUE="0">
		</OBJECT>
		<TABLE id="Table1" style="FONT-SIZE: 10pt; Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
			cellSpacing="0" cellPadding="0" align="center" bgColor="#efefef" border="0">
			<TR>
				<TD vAlign="top" width="118" height="387" rowSpan="2"><IMG height="387" src="Images/1.gif" width="118"></TD>
				<TD vAlign="top" width="132" height="387" rowSpan="2"><IMG height="387" src="Images/2.gif" width="132"></TD>
				<TD vAlign="top" width="118" height="387" rowSpan="2"><IMG height="387" src="Images/3.gif" width="118"></TD>
				<TD vAlign="top" width="201" height="26"><A href="http://oa.digitlab.net.cn/" target="_blank"><IMG height="26" src="Images/5.gif" width="201" border="0"></A></TD>
			</TR>
			<TR>
				<TD vAlign="top" height="364"><IMG height="361" src="Images/4.gif" width="201"></TD>
			</TR>
			<TR>
				<TD vAlign="top" colSpan="2" height="25"><FONT face="宋体">运行状态：</FONT></TD>
				<TD align="right" colSpan="2"><IMG height="32" src="Images/logo.gif" width="118"></TD>
			</TR>
			<TR>
				<TD vAlign="top" colSpan="4" height="25"><div id="download" style="display:none"><a href="../Includes/Activex/setup.rar">下载</a></div></TD>
			</TR>
		</TABLE>
		<script language="javascript">
		try
		{
			<%if(thisProgramList!=null&&thisProgramList.Count>0){
			XmlNode NodePath=thisProgramList[0].SelectSingleNode("td[@Column='LocationStr']");
				if(NodePath!=null&&NodePath.InnerXml.Trim()!=""){
				%>
				ThCrypt.ExecCmd('<%=NodePath.InnerXml.Replace("\\","\\\\")%>');
			<%
				}
				else
				{%>
					window.alert('该程序配置安装路径有误');
				<%}
			}else
				{%>
					window.alert('该程序没有配置安装');
					document.all["download"].style.display="";
				<%}%>
		}
		catch(e)
		{
			alert('本机没有安装执行组件，请在界面上点击下载');
			document.all["download"].style.display="";
		}
		</script>
	</body>
</HTML>
