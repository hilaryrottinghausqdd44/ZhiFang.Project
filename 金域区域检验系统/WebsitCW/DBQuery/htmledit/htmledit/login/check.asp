<%
	
if session("adminname")="" or session("adminpower")="" then
	'response.write "<script>alert('�㻹û��½.���ߵ�½��ʱ.�����µ�½.');top.location.href='../default.asp';</script>"
	'response.end
end if
if cint(session("adminpower"))<pageadmin then
	'response.write "<script>location.href='../err.html';</script>"
	'response.end
end if
%>