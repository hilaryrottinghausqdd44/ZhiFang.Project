<!--#include file="inc/function.asp" -->
<!--#include file="upload/upload_class.asp"-->


<%
Server.ScriptTimeOut = 999999
Dim sType, sStyleName
Dim sAllowExt, nAllowSize, sUploadDir

%>
<%
Dim up
Set up=new LBUpload
up.eWebList()
If Request.QueryString("action")="ewebsave" Then
	up.eWebAdd()
End if
Set up=Nothing
'call connclose

%>
<% 
' ����ͻ��˽ű�
Sub OutScript(str)
	Response.Write "<script language=javascript>" & str & ";history.back()</script>"
End Sub
%>