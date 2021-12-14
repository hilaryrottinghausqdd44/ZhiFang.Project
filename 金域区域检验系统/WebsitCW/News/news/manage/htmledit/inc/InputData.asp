<%@ Language=VBScript %>

<!--#Include file="SubConfig.asp" -->



<%
	strErr=""
	strErr1=""
	dim cn
	set cn=server.CreateObject("ADODB.CONNECTION")
	cn.CursorLocation=3
	'on error resume next
	strPATH=server.MapPath("config.xml")
	strConnectionstring=ReadCNString(strPATH)
	cn.ConnectionString=strConnectionstring
	
	cn.Open' ,,,adAsyncConnect 
	
	
	strParameterTable=Ucase(ReadPARAMETER(strPath,"PARAMETERTABLE"))
	if Request("TABLENAME")="" THEN
		strTableName=Ucase(ReadTableName(strPath,"TABLENAME"))
	else
		strTableName=Request("TABLENAME")
		set rs=cn.Execute("select * from " + strTableName + " where 1>2")
		if err.number<>0 then
			Response.Write err.Description
			Response.Write "select * from " + strTableName + " where 1>2"
			Response.End
		end if
		SaveTableName strTableName
	END IF
	
	'=============================
	'上面的代码有错，以后要改,李子佳
	'=============================
	
	nColumns=0
	SQL="select FEFIELDNAME,FCFIELDNAME from " + strParameterTable + " where FETABLENAME='" + strTableName + "'ORDER BY FORDER"
	set rs=cn.Execute(SQL)
	'set rs=server.CreateObject("ADODB.RECORDSET")
	'RS.Open SQL,CN,1,3 'cn.Execute(SQL,4,adOpenAsync )
	
	
	strFEFIELDNAMES=""
	strFCFIELDNAME=""
	if err.number=0 then
		nColumns=rs.recordcount
	else
		Response.Write err.description
	end if

	
	if nColumns=0 then
		Response.Write "<p>本操作没有找到表字段对照关系"
		dim rsTemp
		set rsTemp=cn.Execute("select * from " + strTableName)
		if rsTemp.fields.count=0 then
			Response.Write "没有找到字段"
			Response.End
		end if
		nColumns=rsTemp.fields.count
		dim field
		for each field in rsTemp.Fields
			strFEFIELDNAMES=strFEFIELDNAMES + "," + field.Name
			'rsTemp.movenext
		next
		strFCFIELDNAMES=strFEFIELDNAMES
		Response.Write err.description
		
	else
		do while not rs.EOF 
			strFEFIELDNAMES=strFEFIELDNAMES + "," + rs("FEFIELDNAME")
			strFCFIELDNAMES=strFCFIELDNAMES + "," + rs("FCFIELDNAME")
			rs.movenext
		LOOP
	end if
	
	
		IF strFEFIELDNAMES<>"" THEN
			strFEFIELDNAMES=right(strFEFIELDNAMES,len(strFEFIELDNAMES)-1)
			strFCFIELDNAMES=right(strFCFIELDNAMES,len(strFCFIELDNAMES)-1)
		end if
													
		arrayFeFieldNames=split(strFEFIELDNAMES,",")
		arrayFCFIELDNAMES=split(strFCFIELDNAMES,",")
	
	'给文本框复值
	dim Values()
	Redim values(nColumns)
	
	SQL="SELECT " + strFEFIELDNAMES + " FROM " + strTableName
	
'=============================================================================================
	if Request.ServerVariables("HTTP_METHOD")="POST" THEN
		strFieldTypes=CollectTypes(cn,strTableName,strFEFIELDNAMES,"$,")
		strValues=CollectValues(nColumns,"text","$,")
		strUpdatedValues=UpdateValues(strValues,strFieldTypes,"$,","'","''")
		
		strWheres=CollectWheres(strFEFIELDNAMES,strUpdatedValues,"$,","AND",strFieldTypes)
		
		strInsertValues=replace(strUpdatedValues,"$,",",")
		
		IF Request.Form("Button1")="" then
			strSql="select " + strFEFIELDNAMES + " from " + strTableName 
			strSql= strSql + " where " + strWheres
			'on error resume next
				set rsData=cn.Execute(strSql)
				if rsData.recordcount=0 then
					set rsData=cn.Execute("insert into " + strTableName + " (" +strFEFIELDNAMES+ ") Values(" + strInsertValues + ")")
					strAddedSql=" or (" + replace(CollectWheres(strFEFIELDNAMES,strUpdatedValues,"$,","AND",strFieldTypes),"$,",",") + ")"
				else
					strErr="{记录已经存在，不能重复添加}"
				end if
			if err.number<>0 then
				strErr=Err.description
			end if
				
			
		'elseif Request.Form("Button1") ="修改" then
		'strSql="update table set"
		'Response.Write Request.ServerVariables("QUERY_STRING")
		elseif Request.Form("Button1") ="删除" or Request.Form("Button1") ="修改" then
			strWheres=""
			if Request.ServerVariables("QUERY_STRING")<>"" then
				if instr(1,Request.ServerVariables("QUERY_STRING"),"==")>0 THEN
					
					arrayWheres=split(Request.ServerVariables("QUERY_STRING"),"&")
					for intCount=0 to UBound(arrayWheres)
						arrayWhere=split(arrayWheres(intCount),"==")
						arrayWhere(1)=replace(replace(arrayWhere(1),"←",vbcrlf),"$sharp","#")
						if arrayWhere(1)="" then
							arrayWhere(1)="NULL"
							strWheres= strWheres + " AND (" + arrayWhere(0) + " is " + arrayWhere(1) + " OR " + arrayWhere(0) + "=" + arrayWhere(1) + ")"
						else
							strWheres= strWheres + " AND " + arrayWhere(0) + "=" + arrayWhere(1) + ""
						END IF
						'strWheres= strWheres + " AND " + arrayWhere(0) + "=" + arrayWhere(1) + ""
					next
					strWheres=right(strWheres,len(strWheres)-4)	
					set rsDelete=cn.Execute(SQL + " where " + strWheres)
					if rsDelete.recordcount=1 then
						if Request.Form("Button1") ="删除" then
							cn.Execute("delete from " + strTableName + " where " + strWheres)
							strErr="删除成功"
						elseif Request.Form("Button1") ="修改" then
							strUpdateString=CollectUpdates(strFEFIELDNAMES,strUpdatedValues,"$,",",",strFieldTypes)
							cn.Execute("Update " +strTableName + " set " + strUpdateString + " where " + strWheres)
							strErr="修改成功"
						end if 
					else
						strErr="删除或修改时出错，不是一条记录？请检查"
					end if
				end if
			end if
		end if
	

	END IF
'=============================================================================================
	
	

	
'-----------------查询过滤-------------------------------------------------------------------
	if Request.ServerVariables("QUERY_STRING")<>"" then
	
		'选定一条记录或过滤****************************************
		if instr(1,Request.ServerVariables("QUERY_STRING"),"==")>0 THEN
			strWheres=""
			'if instr(1,Request.ServerVariables("QUERY_STRING"),"&")>0 then
				arrayWheres=split(Request.ServerVariables("QUERY_STRING"),"&")
			'else
				'arrayWheres(0)=Request.ServerVariables("QUERY_STRING")
			'end if
			for intCount=0 to UBound(arrayWheres)
				arrayWhere=split(arrayWheres(intCount),"==")
				arrayWhere(1)=replace(replace(arrayWhere(1),"←",vbcrlf),"$sharp","#")
				if arrayWhere(1)="" then
					arrayWhere(1)="NULL"
					strWheres= strWheres + " AND (" + arrayWhere(0) + " is " + arrayWhere(1) + " OR " + arrayWhere(0) + "=" + arrayWhere(1) + ")"
				else
					strWheres= strWheres + " AND " + arrayWhere(0) + "=" + arrayWhere(1) + ""
				END IF
				'strWheres= strWheres + " AND " + arrayWhere(0) + "=" + arrayWhere(1) + ""
			next
			strWheres=right(strWheres,len(strWheres)-4)
			SQL=SQL + " where " + strWheres +" " + strAddedSql
			SQL=replace(sql,"%20"," ")
			set rsTEXT=cn.Execute(SQL)
			if rsTEXT.recordcount=1 then
				for row=1 to nColumns
					Values(row)=replace(iifNullRecord(rsTEXT(row-1)),vbcrlf,"←")
				next
				bCanModifyAndDelete="1"
				strErr=strErr + "{现在可以对记录进行删除修改操作}"
			else
				strErr=strErr + "{记录不唯一，请检查？! 现在不能修改记录}"
			end if
		elseif instr(1,Request.ServerVariables("QUERY_STRING"),"↓")>0 then
			'排序 A->Z ********************************************************
			strOderBy=Request.ServerVariables("QUERY_STRING")
			strOderBy=replace(strOderBy,"↓","")
			SQL=SQL + " ORDER BY " + strOderBy
			strErr=strErr + "{排序完成,A->Z}"
		elseif instr(1,Request.ServerVariables("QUERY_STRING"),"↑")>0 then
			'排序 Z->A ********************************************************
			strOderBy=Request.ServerVariables("QUERY_STRING")
			strOderBy=replace(strOderBy,"↑","")
			SQL=SQL + " ORDER BY " + strOderBy + " DESC"
			strErr=strErr + "{排序完成,Z->A}"
		end if
		
	end if
	
	set rs=cn.Execute(SQL)
	strErr="目前有"+cstr(rs.recordcount) + "条记录&nbsp;&nbsp;&nbsp;&nbsp;" + strErr
	'Response.Write Request.ServerVariables("QUERY_STRING")
'-----------------查询过滤-------------------------------------------------------------------
	
%>
<HTML>
<HEAD>
<META name=VI60_defaultClientScript content=VBScript>
<META NAME="GENERATOR" Content="Microsoft FrontPage 4.0">
<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">
<LINK REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/THEME.CSS" VI6.0THEME="Sumi Painting">
<LINK REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/GRAPH0.CSS" VI6.0THEME="Sumi Painting">
<LINK REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/COLOR0.CSS" VI6.0THEME="Sumi Painting">
<LINK REL="stylesheet" TYPE="text/css" HREF="../_Themes/sumipntg/CUSTOM.CSS" VI6.0THEME="Sumi Painting"><SCRIPT ID=clientEventHandlersVBS LANGUAGE=vbscript>
<!--

Sub window_onload
	if bCanModifyAndDelete.value<>"" then
		form1.ButtModify.disabled=false
		form1.ButtDelete.disabled=false
	end if
End Sub

Sub ButtAddNew_onclick
	if form1.text1.value="" then
		msgbox "第一个输入内容不能为空"
	else
		form1.submit
	end if
End Sub

-->
</SCRIPT>
<LINK rel="stylesheet" type="text/css" href="css/tlm.css">
<title></title>
</HEAD>
<BODY topmargin="5">
<form id=form1 name=form1 method=post>
<P align=left>{表名}<%=strTableName%> <input type="hidden" id="TABLENAME" name="TABLENAME" value="<%=strTableName%>">
&nbsp;<input type=Button name=Button1 id=ButtAddNew value=添加>   
&nbsp;<input type=submit name=Button1 id=ButtModify value=修改  disabled>   
&nbsp;<input type=submit name=Button1 id=ButtDelete value=删除 disabled>   

</P>

<TABLE WIDTH=75% border=1 cellspacing=0 cellpadding=0 class=mptable bgcolor="#A0EBC6" bordercolordark="#FFFFFF" ID="Table1">
	<TR>
		<%for row=1 to nColumns%> 
			<TD  align="center" bgcolor="#31D583"  nowrap><input style="width:100%" type=text name=text<%=cstr(row)%> id=text<%=cstr(row)%> value="<%=iifEmptyToNBSP(InterpretCharacter(Values(row)))%>"></TD>
		<%next%>
	</TR>
	</form>
	<TR class=mptd0>
		
			<TD colspan="<%=nColumns%>" bgcolor="#64DFA2" height="30" nowrap>&nbsp<%=strErr%></TD>
		
	</TR>
	<TR class=mptd3>
		<%for each eField in arrayFeFieldNames%> 
			<TD align="center" bgcolor="#A0EBC6" height="25" nowrap><%=efield%><a href="inputData.asp?<%=efield%>↓" >↓</a></TD>
		<%next%>
	</TR>
	<TR class=mptable>
		<%	intCount=0%>
		<%  for each cField in arrayFcFieldNames%>
		<%  intCount=intCount+1	%>
			<TD NOWRAP align="center" bgcolor="#CEF9E0" height="25"><%=cfield%> <a href="inputData.asp?<%=arrayFeFieldNames(intCount-1)%>↑">↑</a></TD>
		<%next%>
	</TR>
	<%do while not rs.eof%>
	<%intTr=intTr+1%>
	<TR class=mptd<%=cstr(intTr MOD 2)%>>
		<%for i=0 to nColumns-1 %> 
		
				<%	if i=0 then
						if nColumns<3 then
							intQrys=nColumns
						elseif nColumns<10 then
							intQrys=3
						else
							intQrys=5
						end if
						strQueryString=""
						for intQry=0 to intQrys
							if not isNull(rs(intQry)) then
								if rs(intQry).type=200 or rs(intQry).type=202 then
									strQueryString=strQueryString + "&" + rs(intQry).name + "=='" + replace(replace(Replace(rs(intQry).value,"'","''"),vbcrlf,"replace"),"#","$sharp") + "'"
								end if
								if rs(intQry).type=5 or rs(intQry).type=139 then
									strQueryString=strQueryString + "&" + rs(intQry).name + "==" + cstr(rs(intQry).value)
								end if
							end if
						next
						 
						if strQueryString<>"" then
							strQueryString=Replace(strQueryString,"&","?",1,1)
						end if
						if isNull(rs(0)) then
							strQueryString="?" + rs(0).Name + "=="
						end if
								%>
							<TD NOWRAP class=NormalLink bgcolor="#E4FCEF" height="25"><a href="InputData.asp<%=strQueryString%>"><%=iifEmptyToNBSP(InterpretCharacter(replace(iifNullRecordShowNULL(RS(I)),vbcrlf,"←")))%></a></TD>
					<%else
						strQueryString=""
						if not isNull(RS(i)) then
							
							if rs(i).type=200 or rs(i).type=202 then
								strQueryString=strQueryString + "?" + rs(i).name + "=='" + replace(replace(replace(rs(i).value,"'","''"),vbcrlf,"←"),"#","$sharp") + "'"
							end if
							if rs(i).type=5 or rs(i).type=139  then
								strQueryString=strQueryString + "?" + rs(i).name + "==" + cstr(rs(i).value)
							end if
						end if %>
						<TD NOWRAP bgcolor="#F5FEF9">&nbsp;<a href="InputData.asp<%=strQueryString%>"><%=iifEmptyToNBSP(InterpretCharacter(replace(iifNullRecord(RS(i)),vbcrlf,"←")))%></a></TD>
					<%end if%>
		<%next%>
	</TR>
	<%rs.movenext%>
	<%loop%>
</TABLE>

<input type=hidden id=bCanModifyAndDelete value="<%=bCanModifyAndDelete%>" NAME="bCanModifyAndDelete">

</BODY>
</HTML>

<%
	set rs=nothing
	set cn=nothing
%>