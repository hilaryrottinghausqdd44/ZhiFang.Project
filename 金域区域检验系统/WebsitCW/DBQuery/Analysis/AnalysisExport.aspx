<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.AnalysisExport" Codebehind="AnalysisExport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AnalysisExport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		
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
			
			//=============�õ���ǵı������ֶ���=================
			function GetTableAndFieldArr(strTag)
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
					
					//======���ı���תӢ��===============
					str = FindENamByCName(str.toString());
					str = str.toString().split(/,/);
					//
					return str;
				}
				return void 0;//û��ƥ��ʱ����undefined
			}
			
			//=============�ѱ���滻������=========================
			function ReplaceTagByData()
			{
				var objSheet = Spreadsheet1.ActiveSheet;//sheet����
				//debugger;
				//=======�������к���=======
				var usedRow = objSheet.UsedRange.EntireRow.Count + 1; //��һ��Ҫ��
				var usedCol = objSheet.UsedRange.EntireColumn.Count;
				//=========
			
				//=========������������==========
				var strReplace;  //��Excel���е�����,�Ǳ�ǵĻ�����Ҫ�滻������
				var childInfo = new Array();
				//==============================
				
				for(var row=1; row<=usedRow; row++)
				{
					for(var col=1; col<=usedCol; col++)
					{
						//alert("Row:"+ row + " Col:" + col + "--" + objSheet.Cells(row, col).value);
						strReplace = GetTableAndFieldArr(objSheet.Cells(row, col).value);
						if(strReplace != undefined)
						//�Ǳ�ǣ� �������滻����
						{
							if( strReplace.length ==2 ) //��ʾ������ı��
							{
								//alert(GetMainTableData(strReplace));
								objSheet.Cells(row, col) = GetMainTableData(strReplace);
							}
							else
							//��ʾ���ӱ�ı��
							{
								BuildingChildInfo(strReplace, childInfo, row, col);
								
								objSheet.Cells(row, col).value = "";//�ѱ�Ǵ�Excel�������
							}
						}
					}
				}
				
				//debugger;
				//===============�����ݷ����ӱ�========================
				for(var childNum=0; childNum<childInfo.length; childNum++)
				{
					var nodeList;
					var xpath="";
					
					var childObj = childInfo[childNum];
					
					var tableNameArr = childObj.TableName.split("/");
					for(var n=0; n<tableNameArr.length; n++)
					{
						//xpath += "/Table[@TableCName='" + tableNameArr[n] + "']/tr"; //�ҵ�<tr>��㼯
						xpath += "/Table[@EName='" + tableNameArr[n] + "']/tr";
					}
					nodeList = xmlDoc.selectNodes(xpath);
					if(nodeList == null || nodeList.length==0)
						continue;
						
					//===========����һ��range============
					var objRange = objSheet.Range(objSheet.Cells(childObj.TagPosition[0].Row, childObj.TagPosition[0].Col), objSheet.Cells(childObj.TagPosition[childObj.TagPosition.length-1].Row, childObj.TagPosition[childObj.TagPosition.length-1].Col));
					//===============End===========
					
					var rowOffset = 0;
					var colOffset = 0;
					
					//=====����ҵ�Rangeվ�ĸ��ӣ�Ҫ�ж����һ��cells�Ƿ��кϲ��ĸ���=================
					var colSpan = objRange.Count + objRange.Cells(1,objRange.Count).MergeArea.Count - 1;
					//=====================
					
					//ѭ��tr���
					for(var trNodeNum=1; trNodeNum<nodeList.length; trNodeNum++)//�ӵڶ�����㿪ʼ����
					{
						var copiedRange;
						
						//��tr������������td
						for(var tdNodeNum=0; tdNodeNum<childObj.FieldName.length; tdNodeNum++)
						{
							var tdNodeValue="";
							var tdNode = nodeList[trNodeNum].selectSingleNode("td[@Column='" + childObj.FieldName[tdNodeNum] + "']");
							if(tdNode != null)
							{
								//===�жϽ������==
								if( GetFieldType(tableNameArr, childObj.FieldName[tdNodeNum]) == "0" || GetFieldType(tableNameArr, childObj.FieldName[tdNodeNum])==0 ) //�ַ���
								{
									tdNodeValue = "''" + tdNode.text;
								}
								else
								{
									tdNodeValue = "" + tdNode.text;
								}
							}
							//if(tableNameArr[1] == '�Լ�����')
							//	alert( tdNodeValue);
								
							objSheet.Cells(childObj.TagPosition[tdNodeNum].Row, childObj.TagPosition[tdNodeNum].Col).value = tdNodeValue;
						}
						
						//===============������Range==========
						while( (childObj.TagPosition[0].Col + colOffset) < UsedCols)
						{
							rowOffset++;
							if( (childObj.TagPosition[0].Row + rowOffset) < UsedRows)
							{
								copiedRange = objRange.Offset(rowOffset, colOffset);
								if( IsTheSame(objRange, copiedRange) )
								{
									//objRange.Copy(copiedRange);
									CopyRangeContent(objRange, copiedRange);
									break;
								}
								else
								{
									//colOffset++;
								
									colOffset += colSpan;
									rowOffset = -1;//��Ϊ-1�� ������0
								}
							}
							else
							{
								colOffset += colSpan;
								rowOffset = -1;
							}
						}
						
						//===============End=================
					}
					//===========�ָ���һ�е�����========
					for(var tdIndex=0; tdIndex<childObj.FieldName.length; tdIndex++)
					{
						var nodeValue="";
						var tdNodeLine = nodeList[0].selectSingleNode("td[@Column='" + childObj.FieldName[tdIndex] + "']");
						if(tdNodeLine != null)
						{
							if( GetFieldType(tableNameArr, childObj.FieldName[tdIndex]) == "0" )
								nodeValue = "'" + tdNodeLine.text;
							else
								nodeValue = tdNodeLine.text;
						}
			
						objSheet.Cells(childObj.TagPosition[tdIndex].Row, childObj.TagPosition[tdIndex].Col).value = nodeValue;
					}
					//=================End===============
				}
				
				//=======�ñ��ĵ�һ�б�ѡ��=======
				objSheet.Cells(1,1).MergeArea.Select();
				
			}
			
			//=================�õ����������==============
			function GetMainTableData(strTableField)
			{
				//var node = xmlDoc.selectSingleNode("/Table[@TableCName='" + strTableField[0] + "']/tr/td[@Column='" + strTableField[1] + "']");
				//alert("/Table[@EName='" + strTableField[0] + "']/tr/td[@Column='" + strTableField[1] + "']");
				var node = xmlDoc.selectSingleNode("/Table[@EName='" + strTableField[0] + "']/tr/td[@Column='" + strTableField[1] + "']");
				if( node == null)
				 return "";
				
				
				if( GetMainFieldType(strTableField[0], strTableField[1]) == "0" )
				{
					
					return "'" + node.text;
				}
				return node.text;
				//return xmlDoc.selectSingleNode("/Table[@TableCName='" + strTableField[0] + "']/tr/td[@Column='" + strTableField[1] + "']").text;
			}
			//======================End====================
			
			//==================�ҵ�����������Range����========
			
			function SearchRange(range, initRow, initCol)
			{
				var row = initRow;
				var col = initCol;
				
				var objSheet = Spreadsheet1.ActiveSheet;
				var objReturn;
				var rowOffset = 0;
				var colOffset = 0;
				
				
				while(col< UsedCols)
				{
					rowOffset = 0;
					row = initRow;
					
					while(row< UsedRows)
					//if(row<usedRow)
					{
						rowOffset++;
						row++;
						objReturn = range.Offset(rowOffset, colOffset);
						
						if( IsTheSame(range, objReturn) )
						{
							
							return objReturn;
						}
						
						//alert("EntirRow" + objSheet.UsedRange.EntireRow.Count + ":" + row);
					}
					colOffset++;
					col++;
				}
				return void 0;
			}
			
			//===========�ж�����Range�Ƿ���ͬ===============
			function IsTheSame(objSrc, objDest)
			{
				var dest = objDest;
				var src = objSrc;
				var rowNum = dest.Rows.Count;
				var colNum = dest.Columns.Count;
				
				//==�ж�Ŀ��Range�Ƿ�Ϊ��==
				for(var r=1; r<=rowNum; r++)
				{
					for(var c=1; c<=colNum; c++)
					{
						//�ж�Range�Ƿ�Ϊ��
						if(dest.Cells(r, c).value != "")
						{
							if(dest.Cells(r,c).value != undefined)
							{
								return false;
							}
						}
						//===========
						
						if(src.Cells(r, c).MergeArea.Count != dest.Cells(r, c).MergeArea.Count)
						{
							return false;
						}
						
						//if(src.Cells(r, c).MergeArea.Borders.Weight != dest.Cells(r, c).MergeArea.Borders.Weight)
							//return false;
					}
				}
				//=======End===========	
				return true;
			}
			//========================End====================
			
			//==================����Range�е�����============
			function CopyRangeContent(objSrc, objDest)
			{
				var dest = objDest;
				var src = objSrc;
				var rowNum = dest.Rows.Count;
				var colNum = dest.Columns.Count;
				
				for(var row=1; row<=rowNum; row++)
				{
					for(var col=1; col<=colNum; col++)
					{
						dest.Cells(row, col).value = src.Cells(row, col).value;
					}
				}
			}
			//=======================End====================
			
			//===============�ж��ֶε�����=================
			function GetFieldType(TableNameArray, FieldName)
			{
				var xpath = "";
				var node = null;
				
				for(var i=0; i<TableNameArray.length; i++)
				{
					//xpath += "Table[@TableCName='" + TableNameArray[i] + "']/"
					xpath += "Table[@EName='" + TableNameArray[i] + "']/";
				}
				//if(xpath.lastIndexOf('/') != -1)
				//{
					//xpath = xpath.substr(0, xpath.length-1);
				//}
				xpath += "td[@ColumnEName='" + FieldName + "']/@ColumnType";
				node = xmlTypeInfo.documentElement.selectSingleNode(xpath);
				//if(FieldName == "EName2")
				//{
					//alert(xpath + "::" + FieldName + "::" + node.nodeValue)
				//}
				//alert(xpath + "::" + FieldName); 
				if(node != null)
				{
					
					return node.nodeValue;
				}
			}
			function GetMainFieldType(TableName, FieldName)
			{
				var xpath = "";
				var node = null;
				
				//xpath = "Table[@TableCName='" + TableName + "']/td[@ColumnEName='" + FieldName + "']/@ColumnType";
				xpath = "Table[@EName='" + TableName + "']/td[@ColumnEName='" + FieldName + "']/@ColumnType";
				
				node = xmlTypeInfo.documentElement.selectSingleNode(xpath);
				if(node != null)
					return node.nodeValue;
			}
			//====================End===============
			
			//================�����ӱ�����Ϣ==============
			function BuildingChildInfo(strTableField, arrChildInfo, tagRow, tagCol)
			{
				var strTableName = "";//����
				var strFieldName = "";//�ֶ���
				
				for(var i=0; i<strTableField.length-1; i++)
					strTableName += strTableField[i] + "/";
				
				strTableName = strTableName.substr(0, strTableName.length-1);
				strFieldName = strTableField[strTableField.length-1];
				
				//�жϱ����Ƿ��Ѿ����˼���
				for(var index=0; index<arrChildInfo.length; index++)
				{
					if(arrChildInfo[index].TableName == strTableName)
					//��ʾ������ͬtr�µ��ֶ�
					{
						arrChildInfo[index].FieldName[arrChildInfo[index].FieldName.length] = strFieldName;
						
						arrChildInfo[index].TagPosition[arrChildInfo[index].TagPosition.length] = new Object();
						arrChildInfo[index].TagPosition[arrChildInfo[index].TagPosition.length-1].Row = tagRow;
						arrChildInfo[index].TagPosition[arrChildInfo[index].TagPosition.length-1].Col = tagCol;
						return;
					}
				}
				
				arrChildInfo[arrChildInfo.length] = new Object();
				
				arrChildInfo[arrChildInfo.length-1].TableName = strTableName;//�����Ѿ�����1�� ����Ҫ��������Ҫ��1
				
				arrChildInfo[arrChildInfo.length-1].FieldName = new Array();
				arrChildInfo[arrChildInfo.length-1].FieldName[0] = strFieldName;
				
				arrChildInfo[arrChildInfo.length-1].TagPosition = new Array();
				arrChildInfo[arrChildInfo.length-1].TagPosition[0] = new Object();
				arrChildInfo[arrChildInfo.length-1].TagPosition[0].Row = tagRow;
				arrChildInfo[arrChildInfo.length-1].TagPosition[0].Col = tagCol;
			}
			
		</script>
	</HEAD>
	<body>
		<OBJECT id="Spreadsheet1" height="544" width="889" classid="clsid:0002E559-0000-0000-C000-000000000046"
			VIEWASTEXT>
			<PARAM NAME="HTMLURL" VALUE="">
			<PARAM NAME="HTMLData" VALUE="<%=ExcelData%>">
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
		<form id="Form1" name="Form1" method="post" runat="server">
			<input type="hidden"  name="txtXmlData" value="<%=ResultXmlData %>"> <input type="hidden" name="txtDataType" value="<%=FieldTypeInfo %>">
			<input type="hidden" name="txtXmlConfig" value="<%=ResultXmlConfig%>" id="txtXmlConfig">
		</form>
		<script language="javascript">
			try
			{
			var xmlDoc = new ActiveXObject("Msxml2.DOMDocument.4.0");
			xmlDoc.resolveExternals = false;
			xmlDoc.loadXML(document.Form1.txtXmlData.value);
			
			var xmlTypeInfo = new ActiveXObject("Msxml2.DOMDocument.4.0");
			xmlTypeInfo.resolveExternals = false;
			xmlTypeInfo.loadXML(document.Form1.txtDataType.value);
			
			//===========Ϊ�����ı���תӢ��==============
			var xmlConfig = new ActiveXObject("Msxml2.DOMDocument.4.0");
			//xmlConfig.resolveExternals = false;
			xmlConfig.loadXML(document.getElementById("txtXmlConfig").value);
			//==========================================
			
			var UsedRows = Spreadsheet1.ActiveSheet.UsedRange.EntireRow.Count;
			var UsedCols = Spreadsheet1.ActiveSheet.UsedRange.EntireColumn.Count;
			
			//����
			//alert(Spreadsheet1.ActiveSheet.Cells(3,5).value);
			//alert(Spreadsheet1.ActiveSheet.UsedRange.EntireRow.Count);
			//alert(Spreadsheet1.ActiveSheet.UsedRange.EntireColumn.Count);
			//alert(xmlTypeInfo.xml);
			ReplaceTagByData();
			//alert(typeof(Spreadsheet1.ActiveSheet.Cells(68,1).value));
			//alert(document.Form1.all["txtDataType"].value);
		 //Spreadsheet1.ActiveSheet.Range("A45:A49").Copy("A50:A54");
		 //Spreadsheet1.ActiveSheet.Range(Spreadsheet1.ActiveSheet.Cells(45,1), Spreadsheet1.ActiveSheet.Cells(49,1)).Copy("A50:A54")
		// var o = Spreadsheet1.ActiveSheet.Range(Spreadsheet1.ActiveSheet.Cells(44,1), Spreadsheet1.ActiveSheet.Cells(44,3));
		 }
			catch(e)
			{
				alert('���ı��ػ���û�а�װ���µ�xml�������򣬰�ȷ��������');
				location.href="../../Includes/Activex/msxmlCHS.msi";
				//return;
			}
		</script>
	</body>
</HTML>
