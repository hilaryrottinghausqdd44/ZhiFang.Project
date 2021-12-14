<%@ Page language="c#" Codebehind="main.aspx.cs" AutoEventWireup="false" Inherits="theNews.main.main" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN">
<HTML>
	<HEAD>
		<TITLE>
			<%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>
		</TITLE>
		<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<frameset rows="97,*" frameborder="NO" border="0" framespacing="0">
		<frame src="top.aspx" name="topFrame" scrolling="no" noresize>
		<FRAMESET id="fset" border="0" frameSpacing="0" frameBorder="0" cols="151,*">
			<frame name="leftFrame" scrolling="no" noresize src="left.aspx">
			<frame name="main" src="mainBody.aspx" scrolling=auto>
		</FRAMESET>
	</frameset>

</HTML>
