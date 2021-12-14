<!-- #include file="../../inc/const.asp" -->
<!-- #include file="../../inc/conn.asp" -->
<!-- #include file="../../inc/function.asp" -->
<!-- #include file="../../inc/config.asp" -->
<!-- #include file="../../inc/md5.asp" -->
<%dim adminname,adminpassword
adminname=DisHtml(request.form("adminname"))
adminpassword=DisHtml(request.form("adminpassword"))
if adminname="" or adminpassword="" then
	call showerror("用户名和密码不能为空")
end if
adminpassword=md5(adminpassword)
set rs=conn.execute("select * from [admin] where adminname='"&adminname&"' and adminpassword='"&adminpassword&"'")
if rs.eof then
	rs.close
	set rs=nothing
	call connclose
	call showerror("用户名或密码不存在")
else
	session("adminname")=adminname
	session("AdminPower")=rs("AdminPower")
	session("ct")=rs("ct")

	session("ct")=""
	
	rs.close
	set rs=nothing
	call connclose
	response.write "<script>top.location.href='../menu/main.asp';</script>"
end if


%>