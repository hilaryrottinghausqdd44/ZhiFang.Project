<%@ language="vbscript"%>

<%
'Response.Write err.Description + "<br>"
'Response.Write conn.ConnectionString
'Response.Write "<br>xml load="  + cstr(bLoad)
'Response.Write err.Description

'	Dim SoapClient
	'on error resume next
'	set SoapClient = Server.CreateObject("MSSOAP.SoapClient30")
'	if err.number<>0 then
'		Response.Write err.Description + "<br>"
'		Response.Write "本服务器没有安装SOAP SDK,请安装"
'		Response.End
'	end if
	
	  
'	Dim RetVal
'	SoapClient.ClientProperty("ServerHTTPRequest") = True
'	
'	dim xmlDom
'	set xmlDom=Server.CreateObject("MSXML2.DOMDocument.4.0")
	
'	dim bLoad
	'bLoad=xmlDom.loadXML(Server.MapPath("../../Web.Config"))
	'Response.Write Server.MapPath("../../Web.Config")
	
	'call SoapClient.MSSoapInit (Application("WebServicesUrl") + "?wsdl","","abc","")
	
dim conn
set conn=Server.CreateObject("ADODB.CONNECTION")
on error resume next
conn.Open "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\inetpub\wwwroot\theNews\news\manage\htmledit\db\test1.mdb"	

'set rs=conn.Execute("select * from eWebEditor_Button")

Response.Write err.Description
response.Write err.number


%>