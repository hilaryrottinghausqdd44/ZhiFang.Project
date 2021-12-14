<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.AnalysisConfigMain" CodeBehind="AnalysisConfigMain.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AnalysisConfigMain</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../css/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		
			var lastTable = null;
			function LinkTableNameMenu(obj)//单击页面下方的表明触发
			{
				obj.style.backgroundColor = "skyblue";
				
				if(lastTable!=null && lastTable!=obj)
				{
					lastTable.style.backgroundColor = "white";
				}
				lastTable = obj;
				/*
				EFieldName 英文字段名
				TableName 表名(/xx/xxx)
				*/
				BuildFieldTable(obj.title, obj.EFieldName);
				
				//alert(obj.EFieldName);
				//alert(obj.TableName);
				
				//=========把表名加入隐藏字段中去===
				var tempTableName = "";
				if(obj.TableName.indexOf("/") == 0)
				{
					tempTableName = obj.TableName.substr(1);
				}
				document.Form1.all["hiddenTableName"].value = tempTableName;
				//============End======================
			}
								
			//============构建字段名表==========
			function BuildFieldTable(fields, eFields)//中文字段和英文字段
			{
				var startTableTag = "<Table width=\"100%\" border='0' id='builtedTable' cellspacing='0' cellpadding='0'>";
				var endTableTag = "</table>";
				var strItem = "";
				var arrLength;
				var arrField = new Array();
				var arrEField = new Array();//英文字段名
				
				if(fields != "")
				{
					arrField = fields.split(',');
					arrEField = eFields.split(',');
					
					arrLength = arrField.length;
					
					//==========
					var startTr = "";
					var endTr = "";
					//=========
					var tempFieldName;
					for(var i =0; i<arrLength; i++)
					{
						if(arrField[i].length > 4)
						{
							tempFieldName = arrField[i].substr(0, 3) + "..";
						}
						else
						{
							tempFieldName = arrField[i];
						}
						if( (i+1)%2 != 0)
						{
							startTr = "<tr>";
							endTr = "";	
						}
						else
						{
							startTr = "";
							endTr = "</tr>";
						}
						
						strItem += startTr //"<tr>"
								+"<td width='10'><input type='checkbox' index='" + i  + "' id='chkBox" + i + "'"
								+"></td>"
								+"<td nowrap OnMouseOver='MouseOverField(this)' style='cursor:pointer; cursor:hand'"
								+ "TableFieldEName='" + arrEField[i] + "' "
								+"OnClick='MouseClickField(this)' "
								+ "title='" + arrField[i] + "' "
								+"OnMouseLeave='MouseLeaveField(this)'>" 
								+ tempFieldName	//arrField[i] 
								+ "</td>"//"</td></tr>";
								+ endTr;
					}
					
					document.Form1.all["fieldDiv"].innerHTML = startTableTag + strItem + endTableTag;	
					document.Form1.all["buttonTable"].style.display = "";
							
				}
				else
				{
					document.Form1.all["fieldDiv"].innerHTML = startTableTag + endTableTag;
					document.Form1.all["buttonTable"].style.display = "none";
				}
				//alert(strItem);
			}
			
			//================================
			function MouseOverField(obj)
			{
				//obj.style.border='#ccccff 2px outset';
				obj.style.backgroundColor = 'gold';
			}
			function MouseLeaveField(obj)
			{
				//obj.style.border='#ccccff 0px outset';
				obj.style.backgroundColor = '';
			}
			//单击出入字段
			function MouseClickField(obj)
			{
				var resultName = "";
				resultName ="【" + document.Form1.all["hiddenTableName"].value + "." + obj.TableFieldEName + "】";
				window.frames[0].Spreadsheet1.Selection.value = obj.title;
				window.frames[0].Spreadsheet1.Selection.Rows(2).value = resultName;
			}
			
			//================全选、全部清空功能按钮============
			function IsSelectAll(isCheck)
			{
				var tableObj = document.Form1.all["builtedTable"];
				var rowCount = tableObj.rows.length * 2;//2列，所以乘2
				
				for(var row=0; row<rowCount-1; row++)
				{
					document.Form1.all["chkBox" + row].checked = isCheck;
				}
				
				if(typeof(document.Form1.all["chkBox" + (rowCount-1)]) != "undefined")
				{
					document.Form1.all["chkBox" + (rowCount-1)].checked = isCheck;
				}
			}
			
			function ConfirmSelected()//确定按钮
			{
				var tableObj = document.Form1.all["builtedTable"];
				var rowCount = tableObj.rows.length;
				var index = 0;
				var arrayField = new Array();
				
				var arrayFieldC=new Array();
				
				//=============收集选择的字段信息=================
				/*
				for(var row=0; row<rowCount; row++)
				{
					if(document.Form1.all["chkBox" + row].checked)//选中的字段
					{
						arrayField[index] = "【" + document.Form1.all["hiddenTableName"].value + "." + tableObj.rows(row).cells(1).TableFieldEName + "】";
						index++;
					}
				}
				*/

				var row;
				for(var num=0; num<rowCount*2-1; num++)
				{
					if(document.Form1.all["chkBox" + num].checked)
					{
						row = Math.floor(num/2);
						if(num%2 == 0)
						{
							arrayField[index] = "【" + document.Form1.all["hiddenTableName"].value + "." + tableObj.rows(row).cells(1).TableFieldEName + "】";
							arrayFieldC[index] = tableObj.rows(row).cells(1).title;
						}
						else
						{
							arrayField[index] = "【" + document.Form1.all["hiddenTableName"].value + "." + tableObj.rows(row).cells(3).TableFieldEName + "】";
							arrayFieldC[index] = tableObj.rows(row).cells(3).title;
						}
						index++;
					}
				}
				if(typeof(document.Form1.all["chkBox" + num]) != "undefined")
				{
					if(document.Form1.all["chkBox" + num].checked)
					{
						arrayField[index] = "【" + document.Form1.all["hiddenTableName"].value + "." + tableObj.rows(row).cells(3).TableFieldEName + "】";
					}
				}
				//===============End===================
				
				var rangeObj = window.frames[0].Spreadsheet1.Selection;
				var rowNum = rangeObj.Rows.Count;
				var colNum = rangeObj.Columns.Count;
				
				var  colSpan = 0;
				
				var writeRow = 0; //要写入的行
				var writeCol = -1;//要写入的列
				for(var r=1; r<=arrayField.length; r++)
				{
					if( r<= rowNum*colNum )
					{
						writeCol++;
						writeCol = writeCol%colNum;
						if(writeCol==0)
						{ 
							writeRow++;
						}
						if(document.getElementById("chktitle").checked==true)
						{
						rangeObj.Cells(writeRow, writeCol+1).value=arrayFieldC[r-1];
						rangeObj.Cells(writeRow+1, writeCol+1).value = arrayField[r-1] ;
						}
						else
						{
						rangeObj.Cells(writeRow, writeCol+1).value = arrayField[r-1] ;
						}
						
			
					}
					else
					{
						var temp = r - rowNum*colNum; //超过了选定的数量
						if(document.getElementById("chktitle").checked==true)
						{
						rangeObj.Cells(rowNum, colNum+temp).value = arrayFieldC[r-1];
						rangeObj.Cells(rowNum+1, colNum+temp).value = arrayField[r-1];
						}
						else
						{
						rangeObj.Cells(rowNum, colNum+temp).value = arrayField[r-1];
						}
						
					}
				}
			}
			
			function SaveClick()
			{
				var obj = window.frames[0].Spreadsheet1;
				//obj.CSVData = "1,1,2,3,5,Nirvana,13";
				//alert(obj.HTMLData);
				window.frames[0].document.Form1["hiddenExcelData"].value = obj.HTMLData;
				window.frames[0].document.Form1["hiddenTag"].value = "Save";   //加入了存储标记(Save)
				window.frames[0].document.Form1.submit();
			}
			
			function SaveAsClick(token)
			{
				var txtObj;
				if(token == "Input") //导入保存
				{
					txtObj = document.getElementById("inTemplateName");
					if(txtObj.value == "")
					{
						alert("没有输入导入模板的保存文件名");
						return;
					}	
					window.frames[0].document.Form1["hiddenTag"].value = "SaveAsInput";   //加入了存储标记(SaveAs)
				}
				else
				{
					txtObj = document.getElementById("outTemplateName");
					if(txtObj.value == "")
					{
						alert("没有输入导出模板的保存文件名");
						return;
					}
					window.frames[0].document.Form1["hiddenTag"].value = "SaveAsOutput";   //加入了存储标记(SaveAs)
				}
				var hiddenModalClass = document.getElementById("hiddenModalClass");
				window.frames[0].document.Form1["hiddenFileName"].value = txtObj.value;             //保存的模板名
				window.frames[0].document.Form1["hiddenModalClass"].value = hiddenModalClass.value; //保存的模板类型
				
				
				if(hiddenModalClass.value.toUpperCase() == "CSV")
				{
				    var objActiveSheet = window.frames[0].Spreadsheet1.ActiveSheet;
				    var tUsedRows = objActiveSheet.UsedRange.Rows.Count;//使用的总行数
			        var tUsedCols = objActiveSheet.UsedRange.Columns.Count;
                    var csvStr = "";
			        for(var row = 1;row<=tUsedRows;row++)
			        {
			            for(var col = 1;col<=tUsedCols;col++)
			            {
			                var cellValue = objActiveSheet.Cells(row,col).value;
			                if(cellValue == undefined)
			                {
			                    cellValue = "";
			                }
			                var flag = false;//值为true时需要将字符串整体括上""
			                var patt1=/\"|,|\n/gm;
			                if(patt1.test(cellValue))
			                {
			                    flag = true;
			                    cellValue = cellValue.replace(/\"/g,"\"\""); //将字符串中的"转换为""
			                }
			                if(cellValue.indexOf(',')>-1)
			                if(flag)
			                {
			                    cellValue = "\""+cellValue+"\"";
			                    alert(cellValue);
			                }
			                csvStr += cellValue;
			                csvStr += ",";
			            }
			            csvStr += "\n";//换行
			        }
			        window.frames[0].document.Form1["hiddenExcelData"].value = csvStr;
				}
				if(hiddenModalClass.value.toUpperCase() == "PLUG")
				{
				    var obj = window.frames[0].Spreadsheet1;
				    window.frames[0].document.Form1["hiddenExcelData"].value = obj.HTMLData;
				}
				window.frames[0].document.Form1.submit();
			}

			function OutputClick(token) 
			{

			    //debugger;
				var dropObj;
				if(token == "Output") //导出
				{
					dropObj = document.getElementById("dlOutputTemplate");
					if(dropObj.selectedIndex == 0)
					{
						alert("请选择一个导出模板");
						return;
					}
					document.getElementById("outTemplateName").value = dropObj.options[dropObj.selectedIndex].text;
					window.frames[0].document.Form1["hiddenTag"].value = "Output";
//					var hiddenModalClass = document.getElementById("hiddenModalClass");
//					window.frames[0].document.Form1["hiddenModalClass"].value = hiddenModalClass.value; //保存的模板类型
				}
				else
				{
					dropObj = document.getElementById("dlInputTemplate");
					if(dropObj.selectedIndex == 0)
					{
						alert("请选择一个导入模板");
						return;
					}
					document.getElementById("inTemplateName").value = dropObj.options[dropObj.selectedIndex].text;
					window.frames[0].document.Form1["hiddenTag"].value = "Input";
				}
				window.frames[0].document.Form1["hiddenFileName"].value = dropObj.options[dropObj.options.selectedIndex].text;
				window.frames[0].document.Form1.submit();
			}
			
			function DeleteClick(token)
			{
				var dropObj;
				
				var confirm = window.confirm("确定要删除本模块吗？");
				if(!confirm)
				{
				    return;
				}
				//需要添加CSV格式文件的删除
				
				if(token == "Output") //导出的删除
				{
					dropObj = document.getElementById("dlOutputTemplate");
					if(dropObj.selectedIndex == 0)
					{
						alert("选择要删除的导出模板");
						return;
					}
					window.frames[0].document.Form1["hiddenTag"].value = "DeleteOutTemplate";
				}
				else
				{
					dropObj = document.getElementById("dlInputTemplate");
					if(dropObj.selectedIndex == 0)
					{
						alert("选择要删除的导入模板");
						return;
					}
					window.frames[0].document.Form1["hiddenTag"].value = "DeleteInTemplate";
				}
				
				//var dropObj = document.Form1.all["dropListTemplate"];
				window.frames[0].document.Form1["hiddenFileName"].value = dropObj.options[dropObj.options.selectedIndex].text;
				window.frames[0].document.Form1.submit();
				dropObj.options[dropObj.options.selectedIndex] = null;
				dropObj.selectedIndex = 0;
			}
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="1">
        <tr>
            <td width="100%">
                <iframe src="AnalysisConfigBody.aspx?<%=Request.ServerVariables["Query_String"]%>" width="100%" height="100%"></iframe>
            </td>
            <td valign="top" nowrap align="right" width="140">
                <table width="100%">
                    <tr>
                        <td>
                            <div id="fieldDiv">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="buttonTable" width="100%" style="display: none">
                                <tr>
                                    <td>
                                        <input type="button" value="确定" onclick="ConfirmSelected()">
                                    </td>
                                    <td>
                                        <input type="button" value="全选" onclick="IsSelectAll(true)">
                                    </td>
                                    <td>
                                        <input type="button" value="清空" onclick="IsSelectAll(false)">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <input type="checkbox" id="chktitle" value="带标题">带标题
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="40">
            <td align="center" colspan="2" width="100%">
                <%--
						<table width="100%" border="0">
							<tr>
								<td align="center"><input name="btnSave" type="button" value="保存为导入导出模板" onclick="SaveClick()"></td>
								<td align="center"><input name="btnSaveAs" type="button" value="保存为其他报表" onclick="SaveAsClick()"><input type="text" name="txtFileName" size="10"></td>
								<td align="left"><asp:DropDownList ID="dropListTemplate" Runat="server">
									<asp:ListItem Value="-1">--选择模板--</asp:ListItem>
								</asp:DropDownList>
									<input type="button" value="调出" name="btnOutput" onclick="OutputClick()">&nbsp;<input type="button" value="删除" name="btnDelete" onclick="DeleteClick()">
								</td>
							</tr>
						</table>
						--%>
                <table width="100%" cols="2" align="center" bgcolor="#ECECEC">
                    <tr>
                        <td align="center" width="50%">
                            <u style="color: #ff0000">导入</u>模板名称:<input id="inTemplateName" type="text" style="width: 95px; height: 18px; border: 1px solid #cccccc;">
                        </td>
                        <td align="center" width="50%" nowrap="nowrap">
                            <table border="0">
                                <tr>
                                    <td colspan="2">
                                        <u style="color: #ff0000">导出</u>模板名称:<input id="outTemplateName" type="text" style="width: 95px; height: 18px; border: 1px solid #cccccc;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        保存类型:
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="hiddenModalClass.value='PLUG'" id="PLUG" name="radModalList" /><label for="PLUG">Excel控件</label>
                                        <input type="checkbox" onclick="hiddenModalClass.value='CSV'" id="CSV" name="radModalList" /><label for="CSV">CSV</label>
                                    </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DropDownList ID="dlInputTemplate" runat="server" Style="width: 95px; height: 18px; border: 1px solid #cccccc;">
                        <asp:ListItem Value="-1">--选择模板--</asp:ListItem>
                    </asp:DropDownList>
                    <input type="button" value="保存导入模板" onclick="SaveAsClick('Input')">&nbsp;<input type="button" value="调出" onclick="OutputClick('Input')">&nbsp;<input type="button" value="删除" onclick="DeleteClick('Input')">
                </td>
                <td align="center">
                    <asp:DropDownList ID="dlOutputTemplate" runat="server" Style="width: 95px; height: 18px; border: 1px solid #cccccc;">
                        <asp:ListItem Value="-1">--选择模板--</asp:ListItem>
                    </asp:DropDownList>
                    <input type="button" value="保存导出模板" onclick="SaveAsClick('Output')">&nbsp;<input type="button" value="调出" onclick="OutputClick('Output')">&nbsp;<input type="button" value="删除" onclick="DeleteClick('Output')">
                </td>
            </tr>
            </table> </td>
        </tr>
        <tr height="40">
            <td align="center" colspan="2">
                <!--显示的表名-->
                <table id="tableName" style="border-right: #0099cc 1px solid; border-top: #0099cc 1px solid; border-left: #0099cc 1px solid; border-bottom: #0099cc 1px solid" runat="server">
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" name="hiddenTableName">
    <input type="hidden" id="hiddenModalClass" value="PLUG" name="hiddenModalClass">
    </form>
</body>
</html>
