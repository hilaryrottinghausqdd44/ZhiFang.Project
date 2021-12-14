<%@ Page language="c#" AutoEventWireup="True" Debug="true" Inherits="OA.DBQuery.Analysis.AnalysisExportAll" Codebehind="AnalysisExportAll.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AnalysisExportAll</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			
			//===================================定义模板信息对象======================================
			function Point(rx, ry)//坐标点对象
			{
				this.x = rx;
				this.y = ry;
			}
			
			function TemplateInfo()
			{
				this.coordArray = new Array();  //cell坐标字段,相对于模板
				this.tableFieldArray = new Array();//表名，字段名数组字段
				this.innerArrayLength = 0;
			}
			TemplateInfo.prototype.addInfo = function(value, row, col)//收集信息
			{
				this.coordArray[this.coordArray.length] = new Point(row, col);
				this.tableFieldArray[this.tableFieldArray.length] = value; //表名字段名数组
				this.innerArrayLength++;
			}
			TemplateInfo.prototype.getRangeCoord = function()//得到标记的范围
			{
				if(this.coordArray.length == 0)
					return void 0;
				return [this.coordArray[0], this.coordArray[this.coordArray.length-1]];
			}
			TemplateInfo.prototype.getRange = function()//得到模板的Range
			{
				//tSheetObj.Range(iSheetObj.Cells(coord[0].x, coord[0].y), iSheetObj.Cells(coord[1].x, coord[1].y));
				if(this.coordArray.length == 0)
					return void 0;
				
				//=====tSheetObj.Cells(this.coordArray[0].x, 1)中选择1表示从第一列开始，防止第一列没有插入数据
				//var returnRange = tSheetObj.Range(tSheetObj.Cells(this.coordArray[0].x, this.coordArray[0].y), tSheetObj.Cells(this.coordArray[this.innerArrayLength-1].x, this.coordArray[this.innerArrayLength-1].y));
				var returnRange = tSheetObj.Range(tSheetObj.Cells(this.coordArray[0].x, 1), tSheetObj.Cells(this.coordArray[this.innerArrayLength-1].x, this.coordArray[this.innerArrayLength-1].y));
				
				return returnRange;
			}
			//=========================================End===========================================
			
			//==========解析标记和数据=========
			function ParseTag(strTag)
			{
				if(typeof(strTag) == "undefined")
					return void 0;
					 
				if(typeof(strTag)=="number")
					return void 0;
					
				var result = new Array();
				result = strTag.match(/【.+】/g);
						
				if(result != null)
				{
					var str = result.toString();
					str = str.substr(1, str.length-2);
					
					str = str.split(/[\/\.]/);
					return str;//返回表名和字段名数组
				}
				return void 0;//没有匹配时返回undefined
			}
			//====================================
			
			//====================导出所有的数据============
			
			function ExportAllData(exportXml)
			{
				//===有关Xml变量申名===
				var mainTrNode;
				var childTable;
				var childTrNodeList;
				//=================
				
				//===========
				var rowOffset = -1;
				var copiedRange;
				var fieldValue;//数据文件中的数据值
				var maxTrNum;
				//======
				var range = templateInfoObj.getRange();//第一行的Range对象
				
				//mainTrNode = exportXml.documentElement.selectNodes("Table/tr");	
		
				//mainTrNode = exportXml.documentElement.selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[0][0] + "']/tr");
				mainTrNode = exportXml.documentElement.selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[0][0] + "']/tr");
				
				//alert(mainTrNode.length);
				//主循环，对主表的Tr
				for(var mainTrNum=0; mainTrNum<mainTrNode.length; mainTrNum++)
				{
					maxTrNum = 1;//取1可以保证没有子表时也可以正常
					//======查看子表中最大的行数(tr)=======
					for(var tagNum=0; tagNum<templateInfoObj.innerArrayLength; tagNum++)
					{
						if(templateInfoObj.tableFieldArray[tagNum].length > 2) //子表标记
						{
							//var num = mainTrNode[mainTrNum].selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[tagNum][1] + "']/tr").length;
							var num = mainTrNode[mainTrNum].selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[tagNum][1] + "']/tr").length;
							if(num != null)
							{
								num = parseInt(num);
								if(maxTrNum < num)
									maxTrNum = num;
							}
						}
					}
					
					//alert(maxTrNum);
					//=================================要保证没有子表时也可以进入循环===========
					
					for(var m=0; m<maxTrNum; m++)
					{
						//======模板中Range循环======
						for(var rCount=0; rCount<templateInfoObj.innerArrayLength; rCount++)
						{
							
							if(templateInfoObj.tableFieldArray[rCount].length == 2)//主表
							{
								if(null != mainTrNode[mainTrNum].selectSingleNode("td[@Column='" +templateInfoObj.tableFieldArray[rCount][1] + "']") )
								{
								//判断是否为空，是为了防止有的主表没这个记录有的主表有
									//alert(templateInfoObj.coordArray[rCount].y);
									fieldValue = mainTrNode[mainTrNum].selectSingleNode("td[@Column='" +templateInfoObj.tableFieldArray[rCount][1] + "']").text;
									//alert(fieldValue);
									//alert(range.Cells(1, templateInfoObj.coordArray[rCount].y).value);
									//alert(Spreadsheet1.ActiveSheet.Cells(4, 2).value);
									range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "'" + fieldValue;
								}
								else
								{
									range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "";
								}
							}
							else
							{
								//childTrNodeList = mainTrNode[mainTrNum].selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[rCount][1] + "']/tr");
								childTrNodeList = mainTrNode[mainTrNum].selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[rCount][1] + "']/tr");
								
								//alert(childTrNodeList[m].xml);
								
								if(childTrNodeList.length <= m)
								{
									range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "";
								}
								else
								{
									
									//alert(childTrNodeList[m].xml);
									//alert(childTrNodeList[m].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[rCount][2] + "']").text);
									//alert(childTrNodeList[m]);
									//alert(m);
									if( childTrNodeList[m].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[rCount][2] + "']") != null )
									{
										fieldValue = childTrNodeList[m].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[rCount][2] + "']").text;
										range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "'" + fieldValue;
									}
									else
									{
										range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "";
									}
								}
								
							}
						}
						
					
						//===========================
						rowOffset++;
						if(rowOffset > 0)//保证第一次不copy
						{
							//copiedRange = range.Offset(rowOffset, 0);
							//range.Copy(copiedRange);

						    copiedRange = range.Offset(rowOffset, 0);
						    copiedRange.EntireRow.Insert();
						    //office2000 webcomponent控件 copiedRange.InsertRows();
						    //office 2003 copiedRange.EntireRow.Insert()
						    copiedRange = copiedRange.Offset(-1, 0);
						    range.Copy(copiedRange);
							
						}
					}
				}
				//=======恢复第一行数据===========
				for(var firstRow=0; firstRow<templateInfoObj.innerArrayLength; firstRow++)
				{
					
					if(templateInfoObj.tableFieldArray[firstRow].length == 2)//主表
					{
						//alert(templateInfoObj.tableFieldArray[firstRow][1]);
					    //===========================查询的字段在数据文件中可能没有这个配置节=======================
					    if (mainTrNode == null)
					        continue;
					        
						if(mainTrNode[0].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[firstRow][1]+ "']") != null)
						{
							fieldValue = mainTrNode[0].selectSingleNode("td[@Column='" +templateInfoObj.tableFieldArray[firstRow][1] + "']").text;
						}
						else
						{
							fieldValue = "";
						}
						
						range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = "'" + fieldValue;
					}
					else
					{
						//var firstTrNode = mainTrNode[0].selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[firstRow][1] + "']/tr")[0];
					    if (mainTrNode == null)
					        continue;
					    var firstTrNode = mainTrNode[0].selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[firstRow][1] + "']/tr")[0];
						
						if(firstTrNode != null)
						{
							if(firstTrNode.selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[firstRow][2] + "']") != null)
							{
								//fieldValue = firstTrNode.text;
								//range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = fieldValue;
								range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = "'" + firstTrNode.selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[firstRow][2] + "']").text;
							}
							else
							{
								range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = "";
							}
						}
					}
				}
				//===========End=================
				
			}
			//======================End====================
		</script>
	</HEAD>
	<body onload="return window_onload()">
	    <table id="tableProcess" border=0 bgcolor="skyblue" cellspacing="1" width="100%">
	        <tr bgcolor="white"><td id="tdprocessStatus" width="1%" nowrap></td><td id="tdPage" nowrap width="2%"></td><td id="tdProcess" nowrap width="97%"><div id="divPercent" style="background-color:Blue; font-weight: bold; color: #FFFFFF; text-align:center"></div></td></tr>
	    </table>
	    <br />
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
		<form id="Form1" method="post" runat="server">
			<input type="hidden"  name="txtXmlData" value="<%=ResultXmlData %>" id="txtXmlData">
			<!--为了根据中文表名找到英文名，加入了XmlConfig文件-->
			<input type="hidden" name="txtXmlConfig" id="txtXmlConfig" value="<%=ResultXmlConfig%>">
		</form>
		<script language="javascript">
		    //===定义全局变量========
		    //debugger;
			var xmlConfig;
			var templateInfoObj = new TemplateInfo();
			
			var tSheetObj = Spreadsheet1.ActiveSheet;
        			
			function loadXMLDBdata()
			{
			    
			    //var tUsedRows = tSheetObj.UsedRange.Rows.Count;
			    var tUsedRows = tSheetObj.UsedRange.Rows.Count;
			    var tUsedCols = tSheetObj.UsedRange.Columns.Count;
			    //alert(tUsedRows + ":" + tUsedCols + ":rowCount" + tSheetObj.UsedRange.Rows.Count);
			    var xmlDoc = new ActiveXObject("Msxml2.DOMDocument.4.0");
			    xmlDoc.loadXML(document.getElementById("txtXmlData").value);
			    //=================================
    			
			    //==================载入的配置文件============
			    xmlConfig = new ActiveXObject("Msxml2.DOMDocument.4.0");
			    xmlConfig.loadXML(document.getElementById("txtXmlConfig").value);
			    //===========================================
    			
			    //alert(xmlDoc.xml);
    			
			    //============遍历模板,收集模板信息=============
			    var cellValue;
			    //var colStart = 0;
			    //debugger;
			    //alert(tUsedRows);
			    //alert(tUsedCols);
			    tUsedRows++; //不一定要加
			    for(var tRow=1; tRow<=tUsedRows; tRow++)
			    {
				    for(var tCol=1; tCol<=tUsedCols; tCol++)
				    {
					    cellValue = ParseTag(tSheetObj.Cells(tRow, tCol).value);
					    if( cellValue != void 0 )
					    //找到了标记,cellValue =(表名，表名，字段名)cellValue 是数组
					    {
						    //alert("没变换的:" + cellValue);
						    //alert(FindENamByCName(cellValue));
						    //alert(cellValue);
    						
						    //====转换中文表名到英文=======
						    //templateInfoObj.addInfo(cellValue, tRow, tCol);
						    templateInfoObj.addInfo(FindENamByCName(cellValue), tRow, tCol);
					    }
				    }
			    }
			    //alert(templateInfoObj.innerArrayLength);
			    //==============End=============================
			    //alert(xmlDoc.xml);
			    //alert(xmlConfig.xml);
			    ExportAllData(xmlDoc);
    			
			    //=======让表格的第一行被选定=======
			    tSheetObj.Cells(1,1).MergeArea.Select();
			}
			
			//=========中文表名转英文名称================
			//注：这里的cName格式是：中文表名,中文表名,字段名
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
			//==========================================
			
			var xmlQuerySqlData="<%=xmlQueryStr.Replace("\"","'") %>";
			var database="<%=dataBase %>";
			
			function window_onload() {
//			    var retXML = OA.DBQuery.Analysis.AnalysisExportAll.LoadSqlDataPage('',);
//			    alert(retXML.value);
                tdprocessStatus.innerHTML = "<img src='../../images/loading.gif'/>";
                if(xmlQuerySqlData=="*")
                {
                    loadXMLDBdata();
                }
                else
                {
                    
			        LoadSqlData();
			    }
			}
			
			var iStartPage=1;
			var TimeIntervalID;
			var TimeInterval=100;
			
			function LoadSqlData()
			{
			    window.setTimeout(loopLoadSqlData,TimeInterval);
			}

            var iPageCount=1;
            var iPageSizeMinor=20;
			var iRetRecordCount=0;
			       
            function loopLoadSqlData()
			{
			    if(iStartPage>iPageCount)
			    {
			        //window.clearInterval(TimeIntervalID);
			        window.status ="全部调用" +iPageCount + "页完成";
			        tdprocessStatus.innerHTML = "完成";
			        if(range!=null)
			            range.EntireRow.Delete();
			        return;
			    }
			    window.status ="正在调用第" + iStartPage + "页...";
			    
			    var retXML = OA.DBQuery.Analysis.AnalysisExportAll.LoadSqlDataPage(xmlQuerySqlData,iStartPage,iPageSizeMinor,database);
			    var strRetXML=retXML.value;
			    //alert(strRetXML);
			    if (strRetXML.length<2)
			    {
			        //window.clearInterval(TimeIntervalID);
			        tdprocessStatus.innerHTML = "完成";
			        window.status ="全部调用" +iPageCount + "页完成,未数据";
			        return;
			    }
			    try{
			        //debugger;
			        iRetRecordCount=strRetXML.substr(0,strRetXML.indexOf(","));
			        iPageCount=parseInt(iRetRecordCount/iPageSizeMinor);
			        if((iRetRecordCount % iPageSizeMinor)>0)
			        {
			            iPageCount=iPageCount+1;
			        }
			        tdPage.innerHTML = "共" + iRetRecordCount +"记录,分" + iPageSizeMinor + "条/" + iPageCount + "次读取";
			        //alert(tdPage.innerHTML);
			        divPercent.innerHTML="" + parseInt(iStartPage/iPageCount * 100) + "%";
			        divPercent.style.width=parseInt(iStartPage/iPageCount * 100) + "%";
			        
			        
			        var xmlStr=strRetXML.substr(strRetXML.indexOf(",") + 1);
			        
                   //alert(xmlStr);
			        //var tUsedRows = tSheetObj.UsedRange.Rows.Count;
			        var tUsedRows = tSheetObj.UsedRange.Rows.Count;
			        var tUsedCols = tSheetObj.UsedRange.Columns.Count;
			        //alert(tUsedRows + ":" + tUsedCols + ":rowCount" + tSheetObj.UsedRange.Rows.Count);
			        var xmlDoc = new ActiveXObject("Msxml2.DOMDocument.4.0");
			        xmlDoc.loadXML(xmlStr);
			        //=================================
        			
			        //==================载入的配置文件============
			        xmlConfig = new ActiveXObject("Msxml2.DOMDocument.4.0");
			        xmlConfig.loadXML(document.getElementById("txtXmlConfig").value);
			        //===========================================
        			
			        //alert(xmlDoc.xml);
        			
			        //============遍历模板,收集模板信息=============
			        var cellValue;
			        //var colStart = 0;
			        //debugger;
			        //alert(tUsedRows);
			        //alert(tUsedCols);
			        tUsedRows++; //不一定要加
			        for(var tRow=1; tRow<=tUsedRows; tRow++)
			        {
				        for(var tCol=1; tCol<=tUsedCols; tCol++)
				        {
					        cellValue = ParseTag(tSheetObj.Cells(tRow, tCol).value);
					        if( cellValue != void 0 )
					        //找到了标记,cellValue =(表名，表名，字段名)cellValue 是数组
					        {
						        //alert("没变换的:" + cellValue);
						        //alert(FindENamByCName(cellValue));
						        //alert(cellValue);
        						
						        //====转换中文表名到英文=======
						        //templateInfoObj.addInfo(cellValue, tRow, tCol);
						        templateInfoObj.addInfo(FindENamByCName(cellValue), tRow, tCol);
					        }
				        }
			        }
			        //alert(templateInfoObj.innerArrayLength);
			        //==============End=============================
			        //alert(xmlDoc.xml);
			        //alert(xmlConfig.xml);
			        
			        ExportAllXMLData(xmlDoc,iStartPage,iPageSizeMinor);
        			
			        //=======让表格的第一行被选定=======
			        //tSheetObj.Cells(1,1).MergeArea.Select();
			    }
			    catch(e)
			    {
			         window.status +="调用" +iStartPage + "页出错：" + e;
			         return;
			    }
//			    alert(retXML.value);
			    
			    ///在这里调用AJAX分页取数据;
			    TimeInterval=TimeInterval+ 100;
			    window.setTimeout(loopLoadSqlData,TimeInterval);
			    iStartPage=iStartPage+1;
			}
			
			//====================导出所有的数据============
			var rowOffset = -1;
			var range;
				
			function ExportAllXMLData(exportXml,iStartPage,iPageSize)
			{
				//===有关Xml变量申名===
				var mainTrNode;
				var childTable;
				var childTrNodeList;
				//=================
				
				//===========
				var copiedRange;
				var fieldValue;//数据文件中的数据值
				var maxTrNum;
				//======
				if(range==null)
				    range = templateInfoObj.getRange();//第一行的Range对象
				
				//mainTrNode = exportXml.documentElement.selectNodes("Table/tr");	
		
				//mainTrNode = exportXml.documentElement.selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[0][0] + "']/tr");
				mainTrNode = exportXml.documentElement.selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[0][0] + "']/tr");
				
				//alert(mainTrNode.length);
				//主循环，对主表的Tr
				for(var mainTrNum=0; mainTrNum<mainTrNode.length; mainTrNum++)
				{
					maxTrNum = 1;//取1可以保证没有子表时也可以正常
					//======查看子表中最大的行数(tr)=======
					for(var tagNum=0; tagNum<templateInfoObj.innerArrayLength; tagNum++)
					{
						if(templateInfoObj.tableFieldArray[tagNum].length > 2) //子表标记
						{
							//var num = mainTrNode[mainTrNum].selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[tagNum][1] + "']/tr").length;
							var num = mainTrNode[mainTrNum].selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[tagNum][1] + "']/tr").length;
							if(num != null)
							{
								num = parseInt(num);
								if(maxTrNum < num)
									maxTrNum = num;
							}
						}
					}
					
					//alert(maxTrNum);
					//=================================要保证没有子表时也可以进入循环===========
					
					for(var m=0; m<maxTrNum; m++)
					{
						//======模板中Range循环======
						for(var rCount=0; rCount<templateInfoObj.innerArrayLength; rCount++)
						{
							
							if(templateInfoObj.tableFieldArray[rCount].length == 2)//主表
							{
								if(null != mainTrNode[mainTrNum].selectSingleNode("td[@Column='" +templateInfoObj.tableFieldArray[rCount][1] + "']") )
								{
								//判断是否为空，是为了防止有的主表没这个记录有的主表有
									//alert(templateInfoObj.coordArray[rCount].y);
									fieldValue = mainTrNode[mainTrNum].selectSingleNode("td[@Column='" +templateInfoObj.tableFieldArray[rCount][1] + "']").text;
									//alert(fieldValue);
									//alert(range.Cells(1, templateInfoObj.coordArray[rCount].y).value);
									//alert(Spreadsheet1.ActiveSheet.Cells(4, 2).value);
									range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "'" + fieldValue;
								}
								else
								{
									range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "";
								}
							}
							else
							{
								//childTrNodeList = mainTrNode[mainTrNum].selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[rCount][1] + "']/tr");
								childTrNodeList = mainTrNode[mainTrNum].selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[rCount][1] + "']/tr");
								
								//alert(childTrNodeList[m].xml);
								
								if(childTrNodeList.length <= m)
								{
									range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "";
								}
								else
								{
									
									//alert(childTrNodeList[m].xml);
									//alert(childTrNodeList[m].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[rCount][2] + "']").text);
									//alert(childTrNodeList[m]);
									//alert(m);
									if( childTrNodeList[m].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[rCount][2] + "']") != null )
									{
										fieldValue = childTrNodeList[m].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[rCount][2] + "']").text;
										range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "'" + fieldValue;
									}
									else
									{
										range.Cells(1, templateInfoObj.coordArray[rCount].y).value = "";
									}
								}
								
							}
						}
						
					
						//===========================
						rowOffset++;
//						if(rowOffset > 0)//保证第一次不copy
//						{
//							//copiedRange = range.Offset(rowOffset, 0);
//							//range.Copy(copiedRange);

//						    copiedRange = range.Offset(rowOffset, 0);
//						    debugger;
//						    copiedRange.EntireRow.Insert();
//						    if(copiedRange.Cells(1,1).value=="" || copiedRange.Cells(1,1).value=="'")
//						        alert("第"+rowOffset+"行数据没有插入");
//						    window.status=copiedRange.Cells(1,1).value;
//						    //office2000 webcomponent控件 copiedRange.InsertRows();
//						    //office 2003 copiedRange.EntireRow.Insert()
//						    copiedRange = copiedRange.Offset(-1, 0);
//						    range.Copy(copiedRange);
//							
//						}
                        //rowOffset>0保证第一次不copy
                        if(rowOffset>-1) //range!=null多一行
						{
//						    copiedRange = copiedRange.Offset(-1, 0);
//						    range.Copy(copiedRange);
						    range=range.Offset(1,0);
							
						}
					}
				}
				//=======恢复第一行数据===========
				for(var firstRow=0; firstRow<templateInfoObj.innerArrayLength; firstRow++)
				{
					
					if(templateInfoObj.tableFieldArray[firstRow].length == 2)//主表
					{
						//alert(templateInfoObj.tableFieldArray[firstRow][1]);
					    //===========================查询的字段在数据文件中可能没有这个配置节=======================
					    if (mainTrNode == null)
					        continue;
					        
						if(mainTrNode[0].selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[firstRow][1]+ "']") != null)
						{
							fieldValue = mainTrNode[0].selectSingleNode("td[@Column='" +templateInfoObj.tableFieldArray[firstRow][1] + "']").text;
						}
						else
						{
							fieldValue = "";
						}
						
						range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = "'" + fieldValue;
					}
					else
					{
						//var firstTrNode = mainTrNode[0].selectNodes("Table[@TableCName='" + templateInfoObj.tableFieldArray[firstRow][1] + "']/tr")[0];
					    if (mainTrNode == null)
					        continue;
					    var firstTrNode = mainTrNode[0].selectNodes("Table[@EName='" + templateInfoObj.tableFieldArray[firstRow][1] + "']/tr")[0];
						
						if(firstTrNode != null)
						{
							if(firstTrNode.selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[firstRow][2] + "']") != null)
							{
								//fieldValue = firstTrNode.text;
								//range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = fieldValue;
								range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = "'" + firstTrNode.selectSingleNode("td[@Column='" + templateInfoObj.tableFieldArray[firstRow][2] + "']").text;
							}
							else
							{
								range.Cells(1, templateInfoObj.coordArray[firstRow].y).value = "";
							}
						}
					}
				}
				//===========End=================
				
			}
			//======================End====================
        </script>
	</body>
</HTML>
