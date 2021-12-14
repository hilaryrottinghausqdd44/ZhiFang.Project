<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.AnalysisImport" Codebehind="AnalysisImport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AnalysisImport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			//===================================����ģ����Ϣ����======================================
			function Point(rx, ry)//��������
			{
				this.x = rx;
				this.y = ry;
			}
			
			function TemplateInfo()
			{
				this.coordArray = new Array();  //cell�����ֶ�,�����ģ��
				this.tableFieldArray = new Array();//�������ֶ��������ֶ�
				this.innerArrayLength = 0;
			}
			TemplateInfo.prototype.addInfo = function(value, row, col)//�ռ���Ϣ
			{
				this.coordArray[this.coordArray.length] = new Point(row, col);
				this.tableFieldArray[this.tableFieldArray.length] = value; //�����ֶ�������
				this.innerArrayLength++;
			}
			TemplateInfo.prototype.getRangeCoord = function()//�õ���ǵķ�Χ
			{
				if(this.coordArray.length == 0)
					return void 0;
				return [this.coordArray[0], this.coordArray[this.coordArray.length-1]];
				
			}
			TemplateInfo.prototype.getRange = function()//�õ�ģ���Range
			{
				//tSheetObj.Range(iSheetObj.Cells(coord[0].x, coord[0].y), iSheetObj.Cells(coord[1].x, coord[1].y));
				if(this.coordArray.length == 0)
					return void 0;
				
				var returnRange = tSheetObj.Range(tSheetObj.Cells(this.coordArray[0].x, this.coordArray[0].y), tSheetObj.Cells(this.coordArray[this.innerArrayLength-1].x, this.coordArray[this.innerArrayLength-1].y));
				
				return returnRange;
			}
			//=========================================End===========================================
			
			//=======================���������������תӢ����====================
			//ע�������cName��ʽ�ǣ����ı���,���ı���,�ֶ���
			function FindENamByCName(cName)
			{
				//alert(cName);
				var XPath = "";
				var node = null;
				var TableField = cName.toString().split(/,/);
				
				var ReturnTableField = new Array();
				
				for(var i=0; i<TableField.length-1; i++)
				{
					XPath += "Table[@TableCName='" + TableField[i] + "']";
					node = xmlConfig.documentElement.selectSingleNode(XPath);
								
					//alert(node.attributes["1"].value);
					ReturnTableField[i] = node.attributes["1"].value;
					
					XPath += "/tr/";
				}
				ReturnTableField[TableField.length-1] = TableField[TableField.length-1];
				return ReturnTableField;
			}
			//===================================================================
			
			//==========������Ǻ�����=========
			function ParseTag(strTag)
			{
				if(typeof(strTag) == "undefined")
					return void 0;
					 
				if(typeof(strTag)=="number")
					return void 0;
					
				var result = new Array();
				result = strTag.match(/��.+��/g);
				
				if(result != null)
				{
					var str = result.toString();
					str = str.substr(1, str.length-2);
					
					str = str.split(/[\/\.]/);
					
					str = FindENamByCName(str);
					str = str.toString().split(/,/);
					return str;//���ر������ֶ�������
				}
				return void 0;//û��ƥ��ʱ����undefined
			}
			//====================================
			
			//=====================��������ļ�������Xml��Ϣ��=====================
			function DealWithInputFile(templateObj)//����ģ����Ϣ����
			{
				//==���ȶ�λ����һ�е�Range,
				//var o = Spreadsheet1.ActiveSheet.Range(Spreadsheet1.ActiveSheet.Cells(44,1), Spreadsheet1.ActiveSheet.Cells(44,3));
				var coord = templateObj.getRangeCoord();
				if(coord == void 0)
					return void 0;
					
				var infoRange = iSheetObj.Range(iSheetObj.Cells(coord[0].x, coord[0].y), iSheetObj.Cells(coord[1].x, coord[1].y));
				
				//alert(infoRange.Cells(1,2).value);
				BuildingXmlTree(infoRange, templateObj);
			}
			
			function BuildingXmlTree(rangeObj, tempObj)
			{
				var rowOffset = 0;
				var colOffset = 0;
				var rangeOffset = rangeObj;
				var tempRange = tempObj.getRange();
				
				//=========Xml����ı���==========
				var tableNode;
				var attributeNode;
				var trNode;
				var tdNode;
				var childTableNode;
				//=================================
				
				var firstRow = tempObj.coordArray[0].x;
				while(firstRow <= iUsedRows)
				{
					rangeOffset = rangeObj.Offset(rowOffset, colOffset);
					//�ж�rangeOffset�Ƿ��ģ��ƥ��
					if( !IsMatch(tempRange, rangeOffset) )
					{
						return;
					}
					//alert(rangeOffset.Cells(1,2).value);
					
					//==����Xml��===һ��Range��Ӧ������һ��TR
					tableNode = xmlDoc.documentElement.selectSingleNode("Table");
					if( null == tableNode )
					{
						tableNode = xmlDoc.createElement("Table");
						
						//attributeNode = xmlDoc.createAttribute("TableCName");
						attributeNode = xmlDoc.createAttribute("EName");
						
						attributeNode.value = tempObj.tableFieldArray[0][0];
						tableNode.setAttributeNode(attributeNode);
						xmlDoc.documentElement.appendChild(tableNode);
					}
					trNode = xmlDoc.createElement("tr");
					tableNode.appendChild(trNode);
					var cellFormat;
					for(var i=0; i< tempObj.innerArrayLength; i++)
					{
						//alert(rangeOffset.Cells(1, tempObj.coordArray[i].y).value);
						if(tempObj.tableFieldArray[i].length == 2) //����tempObj.tableFieldArray[0].length == 2
						{
						
							tdNode = xmlDoc.createElement("td");
							attributeNode = xmlDoc.createAttribute("Column");
							attributeNode.value = tempObj.tableFieldArray[i][1];//�ֶ���
							
							tdNode.setAttributeNode(attributeNode);
							
							//===============����Cells��ʱ�������==================
							//tdNode.text = rangeOffset.Cells(1, tempObj.coordArray[i].y).value;
							cellFormat = rangeOffset.Cells(1, tempObj.coordArray[i].y).NumberFormat;
							if(cellFormat != null)
							{
								if(cellFormat.indexOf("Date") != -1)
								{
									//alert("����");
									rangeOffset.Cells(1, tempObj.coordArray[i].y).NumberFormat = "General";
									//tdNode.text = date.getFullYear() + "-" + (date.getMonth()+1)  + "-" + date.getDate();
									var date = new Date((rangeOffset.Cells(1, tempObj.coordArray[i].y).value-25569)*24*60*60*1000);
									var month = date.getMonth()+1>9 ? date.getMonth()+1 : "0" + (date.getMonth()+1);
									var day = date.getDate()>9 ? date.getDate() : "0"+ date.getDate();
									tdNode.text = date.getFullYear() + "-" + month + "-" + day;
								}
								else
								{
									tdNode.text = rangeOffset.Cells(1, tempObj.coordArray[i].y).value;
								}
							}
							//=====================================================
							//��������ж���Ϊ�˷�ֹ�ռ�¼����
							if(rangeOffset.Cells(1, tempObj.coordArray[i].y).value != undefined && rangeOffset.Cells(1, tempObj.coordArray[i].y).value != "" && rangeOffset.Cells(1, tempObj.coordArray[i].y).value != "��")
							{
								childTableNode = trNode.selectSingleNode("Table");
								if(null != childTableNode)
								{
									trNode.insertBefore(tdNode, childTableNode);
								}
								else
								{	
									//alert(rangeOffset.Cells(1, tempObj.coordArray[i].y).value);
									trNode.appendChild(tdNode);
								}
							}
						}
						else
						{
							//TODO:�ڴ˴����ӱ�
							//tableNode = trNode.selectSingleNode("Table[@TableCName='" + tempObj.tableFieldArray[i][1] + "']");
							tableNode = trNode.selectSingleNode("Table[@EName='" + tempObj.tableFieldArray[i][1] + "']");
							if(rangeOffset.Cells(1, tempObj.coordArray[i].y).value != undefined && rangeOffset.Cells(1, tempObj.coordArray[i].y).value != "" && rangeOffset.Cells(1, tempObj.coordArray[i].y).value != "��")
							{
							if(null == tableNode)
							{
								tableNode = xmlDoc.createElement("Table");
								
								//attributeNode = xmlDoc.createAttribute("TableCName");
								attributeNode = xmlDoc.createAttribute("EName");
								
								attributeNode.value = tempObj.tableFieldArray[i][1];
								tableNode.setAttributeNode(attributeNode);
								trNode.appendChild(tableNode);//TR�м�һ���ӱ�
							}
							//=====�ӱ��е�tr=====
							var childTr = tableNode.firstChild;
							if(null == childTr)
							{
								childTr = xmlDoc.createElement("tr");
								tableNode.appendChild(childTr);
							}
							//
							tdNode = xmlDoc.createElement("td");
							attributeNode = xmlDoc.createAttribute("Column");
							attributeNode.value = tempObj.tableFieldArray[i][2];//�ӱ���ֶ���
							
							tdNode.setAttributeNode(attributeNode);
							
							//tdNode.text = rangeOffset.Cells(1, tempObj.coordArray[i].y).value;
							//===============����Cells��ʱ�������==================
							//tdNode.text = rangeOffset.Cells(1, tempObj.coordArray[i].y).value;
							cellFormat = rangeOffset.Cells(1, tempObj.coordArray[i].y).NumberFormat;
							if(cellFormat != null)
							{
								if(cellFormat.indexOf("Date") != -1)
								{
									rangeOffset.Cells(1, tempObj.coordArray[i].y).NumberFormat = "General";
									var date = new Date((rangeOffset.Cells(1, tempObj.coordArray[i].y).value-25569)*24*60*60*1000);
									tdNode.text = date.getFullYear() + "-" + (date.getMonth()+1)  + "-" + date.getDate();
								}
								else
								{
									tdNode.text = rangeOffset.Cells(1, tempObj.coordArray[i].y).value;
								}
							}
							//====================================================
							
							childTr.appendChild(tdNode);
							
							}
						}
					}
					//=============
					rowOffset++;
					firstRow++;
				}
			}
			//=============================End=======================================
			
			//========================�ж�����Range�Ƿ�ƥ��================
			function IsMatch(tRangeObj, dRangeObj)
			{
				if( tRangeObj==(void 0) || dRangeObj==(void 0) )
					return false;
				
				return true;
			}
			//=============================End============================
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<table width="100%" border="0">
			<tr>
				<td width="50%">
					<OBJECT id="importSheet" height="544" width="899" classid="clsid:0002E559-0000-0000-C000-000000000046"
						VIEWASTEXT>
						<PARAM NAME="HTMLURL" VALUE="../Configuration/ConfigurationXml/tempImportFile.htm">
						<PARAM NAME="DataType" VALUE="HTMLURL">
						<PARAM NAME="AutoFit" VALUE="0">
						<PARAM NAME="DisplayColHeaders" VALUE="-1">
						<PARAM NAME="DisplayGridlines" VALUE="-1">
						<PARAM NAME="DisplayHorizontalScrollBar" VALUE="-1">
						<PARAM NAME="DisplayRowHeaders" VALUE="-1">
						<PARAM NAME="DisplayTitleBar" VALUE="-1">
						<PARAM NAME="DisplayToolbar" VALUE="-1">
						<PARAM NAME="DisplayVerticalScrollBar" VALUE="-1">
						<PARAM NAME="EnableAutoCalculate" VALUE="0">
						<PARAM NAME="EnableEvents" VALUE="-1">
						<PARAM NAME="MoveAfterReturn" VALUE="-1">
						<PARAM NAME="MoveAfterReturnDirection" VALUE="0">
						<PARAM NAME="RightToLeft" VALUE="0">
						<PARAM NAME="ViewableRange" VALUE="1:65536">
					</OBJECT>
				</td>
				<td>
					<OBJECT id="templateSheet" height="544" width="899" classid="clsid:0002E559-0000-0000-C000-000000000046"
						VIEWASTEXT>
						<PARAM NAME="HTMLURL" VALUE="">
						<PARAM NAME="HTMLData" VALUE="<%=excelData%>">
						<PARAM NAME="DataType" VALUE="HTMLDATA">
						<PARAM NAME="AutoFit" VALUE="0">
						<PARAM NAME="DisplayColHeaders" VALUE="-1">
						<PARAM NAME="DisplayGridlines" VALUE="-1">
						<PARAM NAME="DisplayHorizontalScrollBar" VALUE="-1">
						<PARAM NAME="DisplayRowHeaders" VALUE="-1">
						<PARAM NAME="DisplayTitleBar" VALUE="-1">
						<PARAM NAME="DisplayToolbar" VALUE="-1">
						<PARAM NAME="DisplayVerticalScrollBar" VALUE="-1">
						<PARAM NAME="EnableAutoCalculate" VALUE="-1">
						<PARAM NAME="EnableEvents" VALUE="-1">
						<PARAM NAME="MoveAfterReturn" VALUE="-1">
						<PARAM NAME="MoveAfterReturnDirection" VALUE="0">
						<PARAM NAME="RightToLeft" VALUE="0">
						<PARAM NAME="ViewableRange" VALUE="1:65536">
					</OBJECT>
				</td>
			</tr>
		</table>
		<form id="Form1" name="Form1" method="post" action="ExecuteImport.aspx">
			<input type="hidden" id="hiddenXmlData" name="hiddenXmlData"> <input type="hidden" id="hiddenDatabase" name="hiddenDatabase" value="<%=dataBaseName%>">
			<input type="hidden" id="hiddenDatabaseEName" name="hiddenDatabaseEName" value="<%=dataBaseEName%>">
			<input type="hidden" id="hiddenRule" name="hiddenRule" value="<%=importRule%>">
			<input type="hidden" id="ResultConfig" name="ResultConfig" value="<%=ResultConfig%>">
		</form>
		<script language="javascript">
			//===����ȫ�ֱ���========
			var templateInfoObj = new TemplateInfo();
			
			var iSheetObj = importSheet.ActiveSheet;
			var tSheetObj = templateSheet.ActiveSheet;
			
			var tUsedRows = tSheetObj.UsedRange.Rows.Count;
			var tUsedCols = tSheetObj.UsedRange.Columns.Count;
			
			var iUsedRows = iSheetObj.UsedRange.Rows.Count;
			var iUsedCols = iSheetObj.UsedRange.Columns.Count;
			
			try{
			var xmlDoc = new ActiveXObject("Msxml2.DOMDocument.4.0");
			//=============Xml�����ļ���Ϊ��������תӢ����=====
			var xmlConfig = new ActiveXObject("Msxml2.DOMDocument.4.0");
			//========================End===============
			}
			catch(e)
			{
				alert('���ı��ػ���û�а�װ���µ�xml�������򣬰�ȷ��������');
				location.href="../../Includes/Activex/msxmlCHS.msi";
				//return;
			}
			xmlDoc.loadXML("<?xml version='1.0' encoding='GB2312'?><Tables/>");
			xmlConfig.loadXML(document.getElementById("ResultConfig").value);
			
			//======���������ӱ��Xml===================
			//var xmlChildDoc = new ActiveXObject("Msxml2.DOMDocument.4.0");
			//xmlChildDoc.loadXML("<?xml version='1.0' encoding='GB2312'?><Tables/>");
			//======================
			
			//alert(xmlConfig.xml);
			//============����ģ��,�ռ�ģ����Ϣ=============
			var cellValue;
			for(var tRow=1; tRow<=tUsedRows; tRow++)
			{
				for(var tCol=1; tCol<=tUsedCols; tCol++)
				{
					cellValue = ParseTag(tSheetObj.Cells(tRow, tCol).value);
					if( cellValue != void 0 )
					//�ҵ��˱��,cellValue =(�������������ֶ���)cellValue ������
					{
						//alert(cellValue);
						templateInfoObj.addInfo(cellValue, tRow, tCol);
					}
				}
			}
			//alert(templateInfoObj.innerArrayLength);
			//==============End=============================
			
			//=======================�����������=================
			function Validator()
			{
				//for(var myRow=1; myRow<=tUsedRows; myRow++)
				//{
				//	for(var myCol=1; myCol<=tUsedCols; myCol++)
				//	{
				//		myCellValue	= ParseTag(tSheetObj.Cells(myRow, myCol).value);
				//		if(myCellValue == void 0)
				//		{
				//			if(tSheetObj.Cells(myRow, myCol).value != iSheetObj.Cells(myRow, myCol).value)
				//			{
				//				alert("���ļ���ģ�岻ƥ��(�У�" + myRow + " �У�" + myCol + ")");
				//				return false;
				//			} 
				//		}
				//		else
				//		{
				//			return true;
				//		}
				//	}
				//   }
				var coord = templateInfoObj.getRangeCoord();
				if(coord == void 0)
					return false;
				//alert(coord[0].x);��
				//alert(coord[0].y);
				var myCellValue;
				var rowNum = coord[0].x-1;
				if(rowNum <= 0)
				{
					return true;
				}
				
				for(var myCol=1; myCol<=tUsedCols; myCol++)
				{
					myCellValue	= ParseTag(tSheetObj.Cells(rowNum, myCol).value);
					if(myCellValue == void 0)
					{
						if(tSheetObj.Cells(rowNum, myCol).value != iSheetObj.Cells(rowNum, myCol).value)
						{
							alert("���ļ���ģ�岻ƥ��(�У�" + rowNum + " �У�" + myCol + ")");
							return false;
						}
					}
				}
				return true;
			}
			//----------------------------End---------------------
			var isValidate = Validator();
			//===================��������ļ�=======================
			if(isValidate)
			{
				DealWithInputFile(templateInfoObj);
				//======================End==================================
				
				document.getElementById("hiddenXmlData").value = xmlDoc.xml;
				//alert(xmlDoc.xml);
				document.Form1.submit();
			}
		</script>
	</body>
</HTML>
