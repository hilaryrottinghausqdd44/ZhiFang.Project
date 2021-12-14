<%
Set rs_config= Server.CreateObject("ADODB.Recordset")
strSql_config="select * from [config]"
rs_config.open strSql_config,Conn,1,1



Function configclose
rs_config.close
Set rs_config = nothing
End Function
%>