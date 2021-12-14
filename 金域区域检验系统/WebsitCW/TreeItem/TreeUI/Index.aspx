<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TreeItem.TreeUI.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
</head>
    <frame name="topFrame" scrolling="NO" noresize src="" frameborder="NO">
	<frameset cols="201,*" frameborder="YES" border="2" framespacing="2" rows="*" bordercolor="#ffffff">
		<frame name="leftFrame" scrolling="AUTO" src="<%=urlstring %>" frameborder="YES" bordercolor="#FFE6BF" borderColorDark="#ffffff" bgColor="#fff3e1" borderColorLight="#ffb766">
		<frame name="mainFrame" src="TreeBrowseNews.aspx" frameborder="YES" scrolling="AUTO" bordercolor="#ffffff">
	</frameset>
	<noframes>
		<body bgcolor="#FFFFFF" text="#000000">
		</body>
	</noframes>
</html>
