<!--#Include file="SubConfig.asp" -->
<%response.charset="GB2312"
	dim conn
	dim connstr
	dim db
	'db="../../DataBase/db.mdb"
	Set conn = Server.CreateObject("ADODB.Connection")
	connstr=ReadCNString(server.MapPath("inc/config.xml"))
	'Response.Write server.MapPath("../inc/config.xml")
	if conn.State=0 then
		'on error resume next
		conn.ConnectionTimeout=5
		'Response.Write connstr
		'response.End()
		conn.Open connstr
		if err.number<>0 then
			Response.Redirect("inc/cfgParameters.asp")
		end if
	end if
		'释放Rs对象
	function rsclose
		set rs=nothing
	end function
	
	'关闭并释放数据库连接
	function connclose
		conn.close
		set conn=nothing
	end function
Dim userid
userid="aa"
%>
