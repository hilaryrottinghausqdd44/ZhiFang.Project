<!-- #include file="../login/check.asp" -->
<!--#include file="../inc/conn.asp" -->
<!--#include file="../inc/function.asp" -->
<!--#include file="upload_class.asp" -->
<%
response.expires=-1
%>
<head>
<link rel="stylesheet" href="../inc/lbstyle.css" type="text/css">
<title>ио╢╚ЁлпР</title>
<base target="_self">
</head>
<body oncontextmenu="return false" onselectstart="return false" ondragstart="return false" >
<%
Dim action
action=Request.QueryString("action")
Dim up
Set up=new LBUpload
Select Case action
  Case "add"
	If LCase(Request.ServerVariables("request_method"))="post" Then
		up.Add()
	Else
		up.PageHtmlForm()
	End If
  Case "delete"
	up.Delete()
  Case Else
	up.PageUplist(10)
End Select
Set up=Nothing
call connclose

%>
</body>
