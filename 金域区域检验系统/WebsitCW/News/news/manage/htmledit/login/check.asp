<%
	
if session("adminname")="" or session("adminpower")="" then
	'response.write "<script>alert('你还没登陆.或者登陆超时.请重新登陆.');top.location.href='../default.asp';</script>"
	'response.end
end if
if cint(session("adminpower"))<pageadmin then
	'response.write "<script>location.href='../err.html';</script>"
	'response.end
end if
%>