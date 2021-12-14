<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.DBSetting.TablesConfig" Codebehind="TablesConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>欢迎使用</TITLE>
		<META NAME="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta http-equiv="Content-Language" content="zh-cn">
	</HEAD>
	<frameset cols="155,84%" border="0" frameborder="0" framespacing="0">
		<frame id="Left" name="TopBar" src="TableConfigLeft.aspx?<%=Request.ServerVariables["Query_String"]%>" scrolling="auto" noresize>
		<frame id="Right" name="Content" src="" >	
	</frameset>
</HTML>
