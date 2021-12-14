<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query._Default" Codebehind="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>欢迎使用</TITLE>
		<META NAME="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta http-equiv="Content-Language" content="zh-cn">
	</HEAD>
	<frameset rows="<%if(SysConfig&&Request.QueryString["DataOutQuery"]==null)Response.Write("");%>0,*" border="0" frameborder="0" framespacing="0" id=fset>
		<frame id="TopBar" name="TopBar" src="<%if(SysConfig){%>TopBar.aspx?<%=Request.ServerVariables["Query_String"]%><%}%>" scrolling="auto" noresize>
		<frame id="Content" name="Content" src="main.aspx?<%=Request.ServerVariables["Query_String"]%>" scrolling="<%if(Request.QueryString["DataOutQuery"]==null)Response.Write("no");else Response.Write("auto");%>">
	</frameset>
</HTML>
