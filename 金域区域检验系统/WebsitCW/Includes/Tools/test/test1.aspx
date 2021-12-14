<%@ Page Language="c#" AutoEventWireup="True" Inherits="theNews.Tools.test.test1" Codebehind="test1.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>test1</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout">
    asdf adsa fa sdf asdf asdf<br>
    <iframe src="test2.aspx" style="width: 504px; height: 344px"></iframe>
    <input id="myValue" type="hidden" style="display: " value="
		<%
		string stri="";
		for(int i=0;i<1000;i++)
		{	
			stri +=i.ToString();
		}
		Response.Write (stri);
		%>
		">
</body>
</html>
