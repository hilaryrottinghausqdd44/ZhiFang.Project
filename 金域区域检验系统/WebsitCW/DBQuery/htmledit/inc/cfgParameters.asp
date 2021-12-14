<%@ Language=VBScript %>
<HTML>
	<HEAD>
		<!--#Include file="SubConfig.asp" -->
		<% 
	dim strPath
	strPath=server.MapPath("config.xml")
	if not ExamConfig(strPath) then
		Response.Redirect "cfgBody.asp"
	end if 
	strErr="当前配置信息"
	
	SET xmlDoc =server.CreateObject("Msxml.DOMDocument")
	xmlDoc.async=false
	xmldoc.load server.MapPath("config.xml")
	if Request.ServerVariables("HTTP_METHOD")="POST" THEN
		
		set xmlAttrs=xmlDoc.documentElement.childNodes(0).attributes
		Set xmlAttr = xmlAttrs.getNamedItem("USERID")
		xmlAttr.nodevalue=Request.Form("txtUSERID")
		
		Set xmlAttr = xmlAttrs.getNamedItem("PASSWORD")
		xmlAttr.nodevalue=Request.Form("txtPASSWORD")
		
		Set xmlAttr = xmlAttrs.getNamedItem("DATASOURCE")
		xmlAttr.nodevalue=Request.Form("txtDATASOURCE")
		
		Set xmlAttr = xmlAttrs.getNamedItem("MACHINE")
		xmlAttr.nodevalue=Request.Form("txtMachine")
		Set xmlAttr = xmlAttrs.getNamedItem("PARAMETERTABLE")
		xmlAttr.nodevalue=Request.Form("txtParameterTable")
		
		Set xmlAttr = xmlAttrs.getNamedItem("JET")
		xmlAttr.nodevalue=Request.Form("SELECT1")
		
		set xmlAttrs=xmlDoc.documentElement.childNodes(1).attributes
		Set xmlAttr = xmlAttrs.getNamedItem("TABLENAME")
		xmlAttr.nodevalue=Request.Form("txtTABLENAME")
		
		call saveXML(xmldoc,strPath)
		strErr="配置保存成功"
		if Request.Form("Button1")="测试连接" then
			if Request.Form("SELECT1")="SQLSERVER" THEN
				sql="Driver={SQL Server};Server=" + Request.Form("txtMachine") + ";uid=" + Request.Form("txtUSERID") + _
					";pwd=" + Request.Form("txtPASSWORD") + ";Database=" + Request.Form("txtDATASOURCE") + ";"
			else
			
				strProvider="oraOLEDB.ORACLE"
				if Request.Form("SELECT1")="ACCESS" THEN
					strProvider="MICROSOFT.JET.OLEDB.4.0"
				END IF
				sql="Provider=" + strProvider + ";USER ID=" + Request.Form("txtUSERID") + _
					";PASSWORD=" + Request.Form("txtPASSWORD") + ";DATA SOURCE=" + Request.Form("txtDATASOURCE")
			end if
				DIM cn
				set cn=server.CreateObject("adodb.connection")
				cn.CursorLocation=3
				cn.ConnectionString=sql
				on error resume next
					cn.Open
				strErr="测试成功"
			
			bCanAccess=true
			if err.number<>0 then
				strErr=err.description
				strErr="<p>测试连接出错，请检查<p>"
				strErr=strErr + err.description
				bCanAccess=false
			else
				set rs=cn.Execute("select * from " + Request.Form("txtTABLENAME") + "")
				if err.number<>0 then
					strErr="读取参数表出错" + err.Description + sql
					bCanAccess=true
				end if
			end if
		end if
		if Request.Form("Button1")="登录" then
			Response.Redirect "inputData.asp"
		end if
	end if
	
	set xmlAttrs=xmlDoc.documentElement.childNodes(0).attributes
	Set xmlAttr = xmlAttrs.getNamedItem("USERID")
	strUSERID=xmlAttr.nodevalue
		
	Set xmlAttr = xmlAttrs.getNamedItem("PASSWORD")
	strPASSWORD=xmlAttr.nodevalue
		
	Set xmlAttr = xmlAttrs.getNamedItem("DATASOURCE")
	strDATASOURCE=xmlAttr.nodevalue
	
	Set xmlAttr = xmlAttrs.getNamedItem("MACHINE")
	strMACHINE=xmlAttr.nodevalue
	Set xmlAttr = xmlAttrs.getNamedItem("PARAMETERTABLE")
	strPARAMETERTABLE=xmlAttr.nodevalue
	
	Set xmlAttr = xmlAttrs.getNamedItem("JET")
	strJET=xmlAttr.nodevalue
	
	set xmlAttrs=xmlDoc.documentElement.childNodes(1).attributes
	Set xmlAttr = xmlAttrs.getNamedItem("TABLENAME")
	strTABLENAME=xmlAttr.nodevalue
		
		
%>
		<meta name="VI60_defaultClientScript" content="VBScript">
		<meta NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
		<link REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/THEME.CSS" VI6.0THEME="Sumi Painting">
			<link REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/GRAPH0.CSS" VI6.0THEME="Sumi Painting">
				<link REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/COLOR0.CSS" VI6.0THEME="Sumi Painting">
					<link REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/CUSTOM.CSS" VI6.0THEME="Sumi Painting">
						<script ID="clientEventHandlersVBS" LANGUAGE="vbscript">
<!--

Sub buttSAVE_onclick
	if form1.txtDATASOURCE.value<>"" then
		form1.submit
	else
		msgbox "数据库连接不能为空",vbinformation
	end if
End Sub

-->
						</script>
						<link href="css/tlm.css" rel="stylesheet" type="text/css">
							<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
	</HEAD>
	<body bgcolor="#ffffff">
		<p class="text2"><img src="biao.gif" width="28" height="28">数据库访问配置</p>
		<form id="form1" name="form1" method="post">
			<table width="75%" border="1" cellPadding="1" cellSpacing="1" bordercolor="#4db382">
				<tr bgcolor="#a0e2be">
					<td width="19%" height="25" align="center">项目</td>
					<td width="45%" height="25" align="center">配置&nbsp;&nbsp;&nbsp;
					</td>
					<td width="36%" height="25" align="center">说明</td>
				</tr>
				<tr>
					<td align="center" bgcolor="#ffffff">用户名</td>
					<td align="center" bgcolor="#ffffff"><input type="text" id="txtUSERID" name="txtUSERID" value="<%=strUSERID%>"></td>
					<td rowspan="5" bordercolor="#ffffff" bgcolor="#ffffff">
						<P>这是数据库配置程序</P>
						<P>数据的添加删除以及修改</P>
						<P>保存当前配置</P>
					</td>
				</tr>
				<tr>
					<td align="center" bgcolor="#eaeaea">密码</td>
					<td align="center" bgcolor="#eaeaea"><input id="txtPASSWORD" name="txtPASSWORD" type="password" value="<%=strpassword%>"></td>
				</tr>
				<tr>
					<td align="center" bgcolor="#ffffff">数据库</td>
					<td align="center" bgcolor="#ffffff"><input type="text" id="txtDATASOURCE" name="txtDATASOURCE" value="<%=strDATASOURCE%>"></td>
				</tr>
				<tr>
					<td align="center" bgcolor="#eaeaea">数据类型</td>
					<td align="center" bgcolor="#eaeaea">
						<P>
							<select id="select1" name="select1" style="WIDTH: 152px; HEIGHT: 22px">
								<option value="SQLSERVER" selected>SQLSERVER</option>
								<option value="ORACLE">ORACLE</option>
								<option value="ACCESS">ACCESS</option>
							</select><BR>
							机器名<INPUT id="txtMachine" type="text" size="14" name="txtMachine" value="<%=strMachine%>"></P>
					</td>
				</tr>
				<TR>
					<TD align="center" bgColor="#ffffff" height="22">要显示数据的表</TD>
					<TD align="center" bgColor="#ffffff"><input type="text" id="txtTABLENAME" name="txtTABLENAME" value="<%=strTABLENAME%>"></TD>
				</TR>
				<tr>
					<td height="22" align="center" bgcolor="#ffffff">中英文对照表</td>
					<td align="center" bgcolor="#ffffff"><INPUT id="txtParameterTable" type="text" value="<%=strParameterTable%>" name="txtParameterTable"></td>
				</tr>
			</table>
			<br>
			<P></P>
			&nbsp;&nbsp; <input id="buttSAVE" name="button1" type="button" src="images/save.gif" value="保存" WIDTH="38"
				HEIGHT="21">&nbsp;&nbsp; <input id="buttTESTCONNECTION" name="button1" type="submit" src="images/lianjie.gif" value="测试连接"
				WIDTH="66" HEIGHT="21"> <font color="red" size="3">
				<%=strErr%>
				----<%'=sql%>
			</font>
			<%if bCanAccess then%>
			<input id="buttLogin" name="button1" type="submit" src="images/denglu.gif" value="登录" WIDTH="38"
				HEIGHT="21">
			<%end if%>
		</form>
	</body>
</HTML>
