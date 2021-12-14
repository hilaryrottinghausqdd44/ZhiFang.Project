<%
'dim strPath
'strPath="c:\config.xml"
dim xmlDoc
SET xmlDoc =server.CreateObject("MSXML.DOMDocument")
xmlDoc.async=false
'strPath=server.MapPath("config.xml")
'*******************************************
'配置环境检测 ExamConfig()
'*******************************************
'dim cn
'set cn=server.CreateObject("adodb.connection")
'cn.CursorLocation=3

Function ExamConfig(byval strPath)
	
	if xmlDoc.load(strPath) then
		if xmlDoc.childNodes.length>0 and xmlDoc.childNodes(0).childNodes.length>1 then
			set xmlNode=xmldoc.childNodes(0).selectSingleNode("CONNECTIONSTRING")
			set xmlTable=xmldoc.childNodes(0).selectSingleNode("SELECTABLE")
			
			if (not xmlNode is nothing) and  (not xmlTable is nothing) then
			    if xmlNode.attributes.length=6 then
					if xmlNode.attributes(0).nodeName="JET"	and _
					   xmlNode.attributes(1).nodeName="USERID" AND _
						xmlNode.attributes(2).nodeName="PASSWORD" AND _
						xmlNode.attributes(3).nodeName="DATASOURCE" AND _
						xmlNode.attributes(4).nodeName="MACHINE" AND _
						xmlNode.attributes(5).nodeName="PARAMETERTABLE" THEN
							ExamConfig=True
					END IF
				end if
			end if
		end if
	end if
End Function

Sub SaveXml(xmlDoc,byval strPath)
	xmlDoc.save strPath
End Sub

Function ReadCNString(byval strXMLPath)
	'ReadCNString="driver={SQL Server};server=localhost;uid=sa;pwd=sa;Database=newsdb;" 
	'exit function
	'End Function

	'Function Test(byval strXMLPath)	
	if xmlDoc.load(strXMLPath) then
		dim xmlAttrs
		set xmlAttrs=xmlDoc.documentElement.childNodes(0).attributes
		dim xmlAttr
		Set xmlAttr = xmlAttrs.getNamedItem("JET")
		dim strJet
		strJet=xmlAttr.nodevalue
		
			Set xmlAttr = xmlAttrs.getNamedItem("USERID")
			dim strUserId 
			strUserId =xmlAttr.nodevalue
			
			Set xmlAttr = xmlAttrs.getNamedItem("PASSWORD")
			dim strPassword
			strPassword=xmlAttr.nodevalue
			
			Set xmlAttr = xmlAttrs.getNamedItem("DATASOURCE")
			dim strDatasource
			strDatasource=xmlAttr.nodevalue
		
		dim strSql	
		if strJet="SQLSERVER" THEN
			Set xmlAttr = xmlAttrs.getNamedItem("MACHINE")
			dim strMachine
			strMachine=xmlAttr.nodevalue
			
			strSql="Driver={SQL Server};Server=" + strMachine + _
				";uid=" + strUserId + _
				";pwd=" + strPassword + _
				";Database=" + strDataSource + ";"
		else
			dim strProvider
			if strJet="ACCESS" THEN
				strProvider="MICROSOFT.JET.OLEDB.4.0"
			elseif strJet="ORACLE" THEN
				strProvider="oraOLEDB.ORACLE"
			ELSE
				strProvider="Unknown"
			end if
			strSql="Provider=" + strProvider + _
				";User id=" + strUserId + _
				";Password=" + strPassword + _
				";Data Source=" + strDataSource
		end if
	end if
		ReadCNString=strSql
End Function

Function ReadTableName(strXMLPath,strPara)
	if xmlDoc.load(strXMLPath) then
		set xmlAttrs=xmlDoc.documentElement.childNodes(1).attributes
		Set xmlAttr = xmlAttrs.getNamedItem(strPara)
		strTableName =xmlAttr.nodevalue
	end if
	ReadTableName=strTableName
End Function

Function SaveTableName(strPara)
	if xmlDoc.load(strPath) then
		set xmlAttrs=xmlDoc.documentElement.childNodes(1).attributes
		Set xmlAttr = xmlAttrs.getNamedItem("TABLENAME")
		xmlAttr.nodevalue=strPara
		xmlDoc.save strPath
	end if
	SaveTableName=strTableName
End Function



Function ReadParameter(strXMLPath,strPara)
	if xmlDoc.load(strXMLPath) then
		set xmlAttrs=xmlDoc.documentElement.childNodes(0).attributes
		Set xmlAttr = xmlAttrs.getNamedItem(strPara)
		strPara=xmlAttr.nodevalue
	end if
	ReadParameter=strPara
End Function

Function iifEmptyToNBSP(byval strIIFEmpty)
	if IsEmpty(strIIFEmpty) then
		strIIFEmpty="&nbsp;"
	end if
	iifEmptyToNBSP=strIIFEmpty
End Function


Function iifNullRecordShowNULL(byval nRecord)
	if isNull(nRecord) then
		iifNullRecordShowNULL="Null"
	else
		set iifNullRecordShowNULL=nRecord
	end if
End Function

Function iifNullRecord(byval nRecord)
	if isNull(nRecord) then
		iifNullRecord=""
	else
		set iifNullRecord=nRecord
	end if
End Function

Function CollectValues(byval nCount,byval strName,byval strSeperator)
	for i=1 to nCount
		strCollect=strCollect + strSeperator + replace(Request.Form(strName + cstr(i)),"←",vbcrlf)
	next
		strCollect=right(strCollect,len(strCollect)-len(strSeperator))
		CollectValues=strCollect
end Function

Function CollectTypes(cn,byval strTableName,byval strFEFIELDNAMES,Byval strType)
	set rsTemp=cn.Execute("select " + strFEFIELDNAMES + " from " +strTableName + " where rownum=0")
	n=rsTemp.recordcount
	n=rsTemp.fields.count
	for each field in rsTemp.fields
		strCollectTypes=strCollectTypes +strType  + cstr(field.type)
	next
	strCollectTypes=right(strCollectTypes,len(strCollectTypes)-len(strType))
	CollectTypes=strCollectTypes
End Function

Function UpdateValues(byval strValues,byval strFieldTypes,byval strType,byval strFind,byval strReplace)
	arrayValues=split(strValues,strType)
	arrayFieldTypes=split(strFieldTypes,strType)
	if UBound(arrayValues)<>uBound(arrayFieldTypes) then
		Response.Write "输入记录中可能包含{$,}号,请检查"
		Response.End
	end if
	i=0
	for each arrayValue in arrayValues
		if arrayFieldTypes(i)=cstr(5) or arrayFieldTypes(i)=cstr(139) then
			if IsNumeric(arrayValue) then
				strUpdatedValues=strUpdatedValues +strType + arrayValue
			elseif arrayValue="" then
				strUpdatedValues=strUpdatedValues +strType + "NULL"
			else
				Response.Write "第" + cstr(i+1) + "个记录中有非法字符，这个记录只能是数字"
				Response.End
			end if
		else'if arrayFieldTypes(i)=cstr(200) then
			if arrayValue="" then
				arrayValue="Null"
			else
				arrayValue=replace(arrayValue,strFind,strReplace)
				arrayValue="'" + arrayValue + "'"
			end if
			strUpdatedValues=strUpdatedValues +strType + arrayValue
		end if
		i=i+1
	next
	strUpdatedValues=right(strUpdatedValues,len(strUpdatedValues)-len(strType))
	UpdateValues=strUpdatedValues
End Function

Function CollectWheres(byval strFEFIELDNAMES,byval strUpdatedValues,Byval strSeperator,byval strConnector,byval strType)
	arrFEFIELDNAMES=split(strFEFIELDNAMES,",")
	arrUpdatedValues=split(strUpdatedValues,strSeperator)
	arrayTypes=split(strFieldTypes,strSeperator)
	i=0
	strCollectWheres=""
	for each arrFEFIELDNAME in arrFEFIELDNAMES
		'IF arrUpdatedValues(i)<>"" AND arrUpdatedValues(i)<>" " AND _
		'	arrUpdatedValues(i)<>"''" AND arrUpdatedValues(i)<>"' '" THEN
		if Ucase(arrUpdatedValues(i))="NULL" THEN
			if arrayTypes(i)=cstr(5) or arrayTypes(i)=cstr(139) then
				strCollectWheres=strCollectWheres + " " + strConnector + " " + arrFEFIELDNAME + " IS " +arrUpdatedValues(i) + ""
			else
				strCollectWheres=strCollectWheres + " " + strConnector + " (" + arrFEFIELDNAME + " IS " +arrUpdatedValues(i) + " OR " + arrFEFIELDNAME + "='')"
			end if
		ELSE
			strCollectWheres=strCollectWheres + " " + strConnector + " " + arrFEFIELDNAME + "=" +arrUpdatedValues(i)
		END IF
		i=i+1
	next
	'if len(strCollectWheres)>len(strConnector)+4 then
		strCollectWheres=right(strCollectWheres,len(strCollectWheres)-len(strConnector)-1)
	'end if
	CollectWheres=strCollectWheres
	 
End Function

Function CollectUpdates(byval strFEFIELDNAMES,byval strUpdatedValues,Byval strSeperator,byval strConnector,byval strType)
	arrFEFIELDNAMES=split(strFEFIELDNAMES,",")
	arrUpdatedValues=split(strUpdatedValues,strSeperator)
	arrayTypes=split(strFieldTypes,strSeperator)
	i=0
	strCollectWheres=""
	for each arrFEFIELDNAME in arrFEFIELDNAMES
		if Ucase(arrUpdatedValues(i))="NULL" THEN
			if arrayTypes(i)=cstr(5) or arrayTypes(i)=cstr(139) then
				strCollectWheres=strCollectWheres + " " + strConnector + " " + arrFEFIELDNAME + "=" +arrUpdatedValues(i) + ""
			else
				strCollectWheres=strCollectWheres + " " + strConnector + " " + arrFEFIELDNAME + "=''"
			end if
		ELSE
			strCollectWheres=strCollectWheres + " " + strConnector + " " + arrFEFIELDNAME + "=" +arrUpdatedValues(i)
		END IF
		i=i+1
	next
	strCollectWheres=right(strCollectWheres,len(strCollectWheres)-len(strConnector)-1)
	CollectUpdates=strCollectWheres
	 
End Function

Function InterpretCharacter(byval strChar)
	strChar=replace(strChar,"<","&lt;")
	strChar=replace(strChar,">","&gt;")
	'strChar=replace(strChar,"&","&amp;")
	'strChar=replace(strChar,"'","&apos;")
	'strChar=replace(strChar,"""","&quot;")
	InterpretCharacter=strChar
End Function
%>
