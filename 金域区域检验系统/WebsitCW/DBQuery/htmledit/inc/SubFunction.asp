<%'OPTION EXPLICIT
set xmlDoc=server.CreateObject("Msxml2.DOMDocument.4.0")
xmldoc.async=false


dim selectType,arrayType,txtType
	selectType=replace(Request.Form("selectType")," ","")
	txtType=replace(Request.Form("txtType")," ","")
dim selectBranches,arrayBranches,txtBranches
	txtBranches=replace(Request.Form("txtBranches")," ","")
	selectBranches=replace(Request.Form("selectBranches")," ","")
	selectShowFeathersField=Request.Form("selectShowFeathersField")
dim selectShowFeathersField, textareaShowFeathers
	selectShowFeathersField=Request.Form("selectShowFeathersField")
	'textareaShowFeathers=replace(Request.Form("textareaShowFeathers"),vbcrlf,"||")
	textareaShowFeathers=Request.Form("textareaShowFeathers")
dim txtSpecificType,chkDefault
	txtSpecificType=replace(Request("txtSpecificType")," ","")
	chkDefault=Request.Form("chkDefault")

dim bDefault
dim txtURL, txtFieldName, txtRelationShip,txtValue,txtDisplayColor,txtDisplayColorAlias
	txtURL=Request.Form("txtURL")
	txtFieldName=Request.Form("txtFieldName")
	txtRelationShip=Request.Form("txtRelationShip")
	txtValue=Request.Form("txtValue")
	txtDisplayColor=Request.Form("txtDisplayColor")
	txtDisplayColorAlias=Request.Form("txtDisplayColorAlias")
	
	
dim strOppErr,xmlNode

if not xmlDoc.load(strPath) then
	Response.Write "配置文件未找到"
	Response.End
end if

set	xmlNode=xmlDoc.childNodes(0).selectSingleNode("FUNCTION")
if xmlNode is nothing then
	set xmlTempNode=xmlDoc.createNode(1,"FUNCTION","")
	xmldoc.childNodes(0).appendChild(xmlTempNode)
	xmlDoc.save(strPath)
	set	xmlNode=xmlDoc.childNodes(0).selectSingleNode("FUNCTION")

end if

Sub AddType() '要是用一个递归是不是好一点
	strTxtType=replace(txtType," ","")
	strTxtBraches=replace(txtBranches," ","")
	if strTxtType<>"" then
		set xmlTempNode=xmlNode.selectSingleNode(strTxtType)
		if xmlTempNode is nothing then
			set xmlTempNode=xmldoc.createNode(1,strTxtType,"")
			xmlNode.appendChild(xmlTempNode)
			selectType=strTxtType
		end if
		if strTxtBraches<>"" then
			set xmlBranchesNode=xmlTempNode.selectSingleNode(strTxtBraches)
			if xmlBranchesNode is nothing then
				set xmlBranchesNode=xmldoc.createNode(1,strTxtBraches,"")
				xmlTempNode.appendChild(xmlBranchesNode)
				selectBranches=strTxtBraches
			else
				strOppErr="这个子项已经存在"
			end if
		else
			strOppErr="未指定子项，类别已经加入"
		end if
		xmlDoc.save(strPath)
	else
		strOppErr="类别不能为空"
	end if	

End Sub

Sub DeleteType()
	if txtType=selectType and txtType<>"" then
			set xmlTempNode=xmlNode.selectSingleNode(txtType)
			if not xmlTempNode is nothing then
				if selectBranches<> "" then
					set xmlTempNodeBranch=xmlTempNode.selectSingleNode(selectBranches)
					if not xmlTempNodeBranch is nothing then
						xmlTempNode.removeChild(xmlTempNodeBranch)
					end if
					
				end if				
				if xmlTempNode.childNodes.length=0 then
					xmlNode.removeChild(xmlTempNode)
					selectBranches=""
				else
					selectBranches=xmlTempNode.childNodes(0).NodeName
				end if
			end if
			xmldoc.save(strPATH)
			
	else
		strOppErr="你要删除哪个项目?,下拉列表框和文本框的必须一致"
	end if
End Sub
	
Sub AddConfiguration()
	if selectBranches<>"" and selectType<>"" then
		set xmlTypeNode=xmlNode.selectSingleNode(selectType)
		set xmlBranchesNode=xmlTypeNode.selectSingleNode(selectBranches)
		
		'创建实体显示字段
		Set xmlAttrs = xmlBranchesNode.Attributes
		Set xmlAttr = xmlAttrs.getNamedItem("FIELD")
    	if selectShowFeathersField<>"" and xmlAttr is nothing then
    		Set xmlAttr = xmlDoc.createAttribute("FIELD")
    		xmlAttr.nodeValue=selectShowFeathersField
			xmlAttrs.setNamedItem xmlAttr
		end if
		
		'创建显示实体类别
		Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    	if selectShowFeathersField<>"" and xmlAttr is nothing then
    		Set xmlAttr = xmlDoc.createAttribute("VALUES")
    		xmlAttr.nodeValue=textareaShowFeathers
			xmlAttrs.setNamedItem xmlAttr
		end if
		
		'创建具体条件
		if chkDefault<>"" then
			Set xmlAttr = xmlAttrs.getNamedItem("DEFAULT")
    		if txtSpecificType<>"" and xmlAttr is nothing then
    			Set xmlAttr = xmlDoc.createAttribute("DEFAULT")
    			xmlAttr.nodeValue=txtSpecificType
				xmlAttrs.setNamedItem xmlAttr
			end if
		end if
		
		'创建一个条件
		if txtSpecificType<>"" then
			set xmlSpecificNode=xmlBranchesNode.selectSingleNode(txtSpecificType)
			if not xmlSpecificNode is nothing then 
				strOPPerr="这个具体类别已经存在，请用其它字串"
				exit sub
			end if
			set xmlSpecificNode=xmlDoc.createNode(1,txtSpecificType,"")
			xmlBranchesNode.appendChild(xmlSpecificNode)
			
				Set xmlAttrs = xmlSpecificNode.Attributes
				Set xmlAttr = xmlAttrs.getNamedItem("URL")
    			if xmlAttr is nothing then
    				Set xmlAttr = xmlDoc.createAttribute("URL")
    				xmlAttr.nodeValue=txtURL
					xmlAttrs.setNamedItem xmlAttr
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("FIELDNAME")
    			if xmlAttr is nothing then
    				Set xmlAttr = xmlDoc.createAttribute("FIELDNAME")
    				xmlAttr.nodeValue=txtFieldName 
					xmlAttrs.setNamedItem xmlAttr
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("RELATIONSHIP")
    			if xmlAttr is nothing then
    				Set xmlAttr = xmlDoc.createAttribute("RELATIONSHIP")
    				xmlAttr.nodeValue=txtRelationShip
					xmlAttrs.setNamedItem xmlAttr
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    			if xmlAttr is nothing then
    				Set xmlAttr = xmlDoc.createAttribute("VALUES")
    				xmlAttr.nodeValue=txtValue
					xmlAttrs.setNamedItem xmlAttr
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("COLORS")
    			if xmlAttr is nothing then
    				Set xmlAttr = xmlDoc.createAttribute("COLORS")
    				xmlAttr.nodeValue=txtDisplayColor
					xmlAttrs.setNamedItem xmlAttr
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("COLORSALAS")
    			if xmlAttr is nothing then
    				Set xmlAttr = xmlDoc.createAttribute("COLORSALAS")
    				xmlAttr.nodeValue=txtDisplayColorAlias
					xmlAttrs.setNamedItem xmlAttr
				end if
		else
			strOPPerr="具体内容不能为空"
		end if
		
		xmlDoc.save(strPATH)
		'txtSpecificType
	else
		strOppErr="请选定主类别和子项，如果还没有这些项目，请先添加创建"
	end if
End Sub

Sub DeleteConfiguration()
	if selectBranches<>"" and selectType<>"" then
		set xmlTypeNode=xmlNode.selectSingleNode(selectType)
		set xmlBranchesNode=xmlTypeNode.selectSingleNode(selectBranches)
		
		'保存实体显示字段
		Set xmlAttrs = xmlBranchesNode.Attributes
		Set xmlAttr = xmlAttrs.getNamedItem("FIELD")
    	if selectShowFeathersField<>"" and not xmlAttr is nothing then
    		xmlAttrs.removeNamedItem "FIELD"
    		xmlDoc.save(strPATH)
		end if
		
		'保存显示实体类别
		Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    	if selectShowFeathersField<>"" and not xmlAttr is nothing then
    		xmlAttrs.removeNamedItem "VALUES"
    		xmlDoc.save(strPATH)
		end if
		
		'保存具体条件
		
		
		'创建一个条件
		if txtSpecificType<>"" then
			set xmlSpecificNode=xmlBranchesNode.selectSingleNode(txtSpecificType)
			if xmlSpecificNode is nothing then 
				strOPPerr="没有此类，无法删除"
				exit sub
			else
				xmlBranchesNode.removeChild xmlSpecificNode
				call ClearSpecifig()
				xmlDoc.save(strPATH)
				
			end if
			
			
			
			if xmlBranchesNode.childNodes.length>0 then
				set xmlSpecificNode=xmlBranchesNode.childNodes(0)
				txtSpecificType=xmlSpecificNode.nodeName
				
				Set xmlAttrs = xmlSpecificNode.Attributes
					Set xmlAttr = xmlAttrs.getNamedItem("DEFAULT")
    					if not xmlAttr is nothing then
   							xmlAttr.nodeValue=txtSpecificType
   							bDefault=true
						end if
						xmlDoc.save(strPATH)
				
				Set xmlAttr = xmlAttrs.getNamedItem("URL")
    			if not xmlAttr is nothing then
    				txtURL=xmlAttr.nodeValue
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("FIELDNAME")
    			if not xmlAttr is nothing then
    				txtFieldName=xmlAttr.nodeValue
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("RELATIONSHIP")
    			if not xmlAttr is nothing then
    				txtRelationShip=xmlAttr.nodeValue
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    			if not xmlAttr is nothing then
    				txtValue=xmlAttr.nodeValue
				end if
				
				
				Set xmlAttr = xmlAttrs.getNamedItem("COLORS")
    			if not xmlAttr is nothing then
    				txtDisplayColor=xmlAttr.nodeValue
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("COLORSALAS")
    			if not xmlAttr is nothing then
    				txtDisplayColorAlias=xmlAttr.nodeValue
				end if
			else
				call ClearSpecifig()
			end if
			
		else
			strOPPerr="没有删除具体内容"
		end if
		
		xmlDoc.save(strPATH)
	else
		strOppErr="请选定主类别和子项，无法确定要删除的具体内容"
	end if
End Sub

Sub SaveConfiguration()
	if selectBranches<>"" and selectType<>"" then
		set xmlTypeNode=xmlNode.selectSingleNode(selectType)
		set xmlBranchesNode=xmlTypeNode.selectSingleNode(selectBranches)
		
		'保存实体显示字段
		Set xmlAttrs = xmlBranchesNode.Attributes
		Set xmlAttr = xmlAttrs.getNamedItem("FIELD")
    		if xmlAttr is nothing then
    			set xmlAttr=xmldoc.createAttribute("FIELD")
    			xmlAttr.nodeValue=selectShowFeathersField
    			xmlAttrs.setNamedItem xmlAttr
    		else
    			xmlAttr.nodeValue=selectShowFeathersField
    		end if
				xmlDoc.save(strPATH)
		
		'保存显示实体类别
		Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    		if xmlAttr is nothing then
    			set xmlAttr=xmldoc.createAttribute("VALUES")
    			xmlAttr.nodeValue=textareaShowFeathers
    			xmlAttrs.setNamedItem xmlAttr
    		else
    			xmlAttr.nodeValue=textareaShowFeathers
    		end if
    		xmlDoc.save(strPATH)
		
		'保存具体条件
		
		
		'创建一个条件
		if txtSpecificType<>"" then
			set xmlSpecificNode=xmlBranchesNode.selectSingleNode(txtSpecificType)
			if xmlSpecificNode is nothing then 
				strOPPerr="这个具体类不存在，请先添加"
				exit sub
			end if
			
			if chkDefault<>"" then
				Set xmlAttr = xmlAttrs.getNamedItem("DEFAULT")
    			if txtSpecificType<>"" then
    				if xmlAttr is nothing then
    					Set xmlAttr = xmlDoc.createAttribute("DEFAULT")
    					xmlAttr.nodeValue=txtSpecificType
						xmlAttrs.setNamedItem xmlAttr
					else
						xmlAttr.nodeValue=txtSpecificType
					end if
					xmlDoc.save(strPATH)
				end if
			end if
				Set xmlAttrs = xmlSpecificNode.Attributes
				Set xmlAttr = xmlAttrs.getNamedItem("URL")
    			if not xmlAttr is nothing then
    				xmlAttr.nodeValue=txtURL
					xmlDoc.save(strPATH)
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("FIELDNAME")
    			if not xmlAttr is nothing then
    				xmlAttr.nodeValue=txtFieldName 
					xmlDoc.save(strPATH)
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("RELATIONSHIP")
    			if not xmlAttr is nothing then
    				xmlAttr.nodeValue=txtRelationShip
					xmlDoc.save(strPATH)
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    			if not xmlAttr is nothing then
    				xmlAttr.nodeValue=txtValue
					xmlDoc.save(strPATH)
				end if
				
				
				Set xmlAttr = xmlAttrs.getNamedItem("COLORS")
    			if not xmlAttr is nothing then
    				xmlAttr.nodeValue=txtDisplayColor
					xmlDoc.save(strPATH)
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("COLORSALAS")
    			if not xmlAttr is nothing then
    				xmlAttr.nodeValue=txtDisplayColorAlias
					xmlDoc.save(strPATH)
				end if
			
		else
			strOPPerr="具体内容不能为空"
		end if
		
		xmlDoc.save(strPATH)
	else
		strOppErr="请选定主类别和子项，如果还没有这些项目，请先添加创建"
	end if
End Sub



Sub ReadingParameters()
	'是否有功能 设置选项
	'set	xmlNode=xmlDoc.childNodes(0).selectSingleNode("FUNCTION")
	if not xmlNode is nothing then
		'是否有类别设置------------------------------------------------------------
		if txtType<>"" then
			set xmlTempNode=xmlNode.selectSingleNode(txtType)
			if xmlTempNode is nothing then
				set xmlTempNode=xmlNode.selectSingleNode(selectType)
			end if
			if xmlTempNode is nothing then
				txtType=""
				selectType=""
				if xmlNode.childnodes.length>0 then
					txtType=xmlNode.childnodes(0).nodeName
					selectType=txtType
				end if
			end if
		elseif selectType<>"" then
			set xmlTempNode=xmlNode.selectSingleNode(selectType)
			if xmlTempNode is nothing then
				txtType=""
				selectType=""
				if xmlNode.childnodes.length>0 then
					txtType=xmlNode.childnodes(0).nodeName
					selectType=txtType
				end if
			end if
		elseif txtType="" and selectType="" then
			
		end if
		'读取类别下拉列表框.....................................
		if xmlNode.childNodes.length>0 then
			for each xmlSelectTypeNode in xmlNode.childNodes
				strSelectType=strSelectType + "," + xmlSelectTypeNode.NodeName
			next
			if strSelectType<>"" then
				arrayType=split(replace(strSelectType,",","",1,1),",")
				if SelectType="" then
					SelectType=arrayType(0)
				end if
			else
				SelectType=""
				arrayType=split(",",",")
			end if
				
		end if
		
			txtType=SelectType
		'是否有类别设置------------------------------------------------------------
		
		'是否有子项设置================================================================
		if SelectType<>"" then
			set xmlTempNode=xmlNode.selectSingleNode(SelectType)
			if not xmlTempNode is nothing then
				for each xmlSelectBranchesNode in xmlTempNode.childNodes
					strSelectBranches=strSelectBranches + "," + xmlSelectBranchesNode.NodeName
				next
				if strSelectBranches<>"" then
					arrayBranches=split(replace(strSelectBranches,",","",1,1),",")
					if selectBranches="" then
						selectBranches=arrayBranches(0)
					else
						set xmlSelectBranchesNode=xmlTempNode.selectSingleNode(selectBranches)
						if xmlSelectBranchesNode is nothing then
							if xmlTempNode.childNodes.length<>0 then
								selectBranches=xmlTempNode.childNodes(0).nodeName
							else
								selectBranches=""
							end if
						end if
							
					end if
				else
					selectBranches=""
					arrayBranches=split(",",",")
				end if
			end if
			txtBranches=selectBranches
		end if
		'==========================================================================
		
		'具体条件设置 
		if SelectType<>"" and selectBranches<>"" then
			set xmlTypeNode=xmlNode.selectSingleNode(selectType)
				if xmlTypeNode is nothing then exit sub
			set xmlBranchesNode=xmlTypeNode.selectSingleNode(selectBranches)
				if xmlBranchesNode is nothing then exit sub
			
			Set xmlAttrs = xmlBranchesNode.Attributes
				Set xmlAttr = xmlAttrs.getNamedItem("FIELD")
    			if not xmlAttr is nothing then
    				'selectShowFeathersField=xmlAttr.nodeValue
    			else
    				'selectShowFeathersField=""
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    			if not xmlAttr is nothing then
    				textareaShowFeathers=replace(xmlAttr.nodeValue,"||",vbcrlf)
    			else
    				textareaShowFeathers=""
				end if
				
				Set xmlAttr = xmlAttrs.getNamedItem("DEFAULT")
    			if not xmlAttr is nothing then
    				strDEFAULT=xmlAttr.nodeValue
    			else
    				strDEFAULT=""
				end if
				
					
				if txtSpecificType = "" then
						if xmlBranchesNode.childNodes.length>0 then
							set xmlSpecificNode=xmlBranchesNode.childNodes(0)
							txtSpecificType=xmlBranchesNode.childNodes(0).nodeName
						else
							call ClearSpecifig()
							exit Sub
						end if
				else
					set xmlSpecificNode=xmlBranchesNode.selectSingleNode(txtSpecificType)
					if xmlSpecificNode is nothing then
						if xmlBranchesNode.childNodes.length>0 then
							set xmlSpecificNode=xmlBranchesNode.childNodes(0)
							txtSpecificType=xmlBranchesNode.childNodes(0).nodeName
						else
							call ClearSpecifig()
							exit Sub
						end if
					end if
					
				end if
				
				
					
	
				if txtSpecificType=strDEFAULT and txtSpecificType<>"" then
					bDefault=true
				end if
					Set xmlAttrs = xmlSpecificNode.Attributes
					Set xmlAttr = xmlAttrs.getNamedItem("URL")
    				if not xmlAttr is nothing then
    					txtURL=xmlAttr.nodeValue
    				else
    					txtURL=""
					end if
					
					Set xmlAttr = xmlAttrs.getNamedItem("FIELDNAME")
    				if not xmlAttr is nothing then
    					txtFieldName=xmlAttr.nodeValue
    				else
    					txtFieldName=""
					end if
					
					Set xmlAttr = xmlAttrs.getNamedItem("RELATIONSHIP")
    				if not xmlAttr is nothing then
    					txtRelationShip=xmlAttr.nodeValue
    				else
    					txtRelationShip=""
					end if
					
					Set xmlAttr = xmlAttrs.getNamedItem("VALUES")
    				if not xmlAttr is nothing then
    					txtValue=xmlAttr.nodeValue
    				else
    					txtValue=""
					end if
					
					Set xmlAttr = xmlAttrs.getNamedItem("COLORS")
    				if not xmlAttr is nothing then
    					txtDisplayColor=xmlAttr.nodeValue
    				else
    					txtDisplayColor=""
					end if
					
					Set xmlAttr = xmlAttrs.getNamedItem("COLORSALAS")
    				if not xmlAttr is nothing then
    					txtDisplayColorAlias=xmlAttr.nodeValue
    				else
    					txtDisplayColorAlias=""
					end if
		end if
		
	end if
End SuB

Sub ClearSpecifig()
	txtSpecificType=""
	txtURL="" 
	txtFieldName="" 
	txtRelationShip="" 
	txtValue="" 
	txtDisplayColor="" 
	txtDisplayColorAlias=""
End Sub
%>