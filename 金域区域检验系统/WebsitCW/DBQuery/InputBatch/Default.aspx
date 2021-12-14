<%@ Import Namespace="System.Xml" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OA.DBQuery.InputBatch.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>批量操作数据</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%if(cssFile.Trim()==""){%>
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet" />
		<%}else{%>
		<LINK href="<%="../" + cssFile%>" type="text/css" rel="stylesheet" />
		<%;}%>
		<script language="javascript" src="../js/dialog.js"></script>
		<!--script language="javascript" src="../../Includes/js/calendar.js"></script-->
		<!--#include file="../../Util/Calendar.js"-->
		<script language="javascript">
		    function form1_onsubmit() {

		    }

		    function window_onload() {
		        if (parent != null && parent.document.getElementById("hLoaded") != null)
		            parent.document.all["hLoaded"].value = "1";
		    }

		    function LocateSubMeEditorMode(objSubMeTD, boolEdit) {
		        if (objSubMeTD.parentNode.lastChild.firstChild==null|| objSubMeTD.parentNode.lastChild.firstChild.disabled == true)
		            return;

		        if (Form1.hAction.value == "BAdd")
		            boolEdit = false;
		        if (boolEdit) {
		            if (objSubMeTD.parentNode.lastChild.firstChild.innerHTML != "取消新增") {
		                objSubMeTD.parentNode.lastChild.firstChild.innerHTML = "取消修改";
		            //    objSubMeTD.parentNode.style.textDecoration = 'none';
		            }
		        }
		        else
		            objSubMeTD.parentNode.lastChild.firstChild.innerHTML = "取消新增";
		    }
		    //子表操作======================================================================================================
		    //删除记录
		    function CancelSubMe(objSubMe, boolDelete) {
//		        if (objSubMe.disabled == true)
//		            return;
//		        if (objSubMe.innerHTML == "删除") {
//		            if (!boolDelete)
//		                return;
//		            objSubMe.innerHTML = "取消删除";
//		            objSubMe.parentNode.parentNode.style.textDecoration = 'line-through';
//		        }
//		        else if (objSubMe.innerHTML == "取消删除") {
//		            objSubMe.innerHTML = "删除";
//		            objSubMe.parentNode.parentNode.style.textDecoration = 'none';
//		        }
//		        else 
		        if (objSubMe.innerHTML == "取消修改") {
		            objSubMe.innerHTML = "";
		            objSubMe.parentNode.parentNode.style.backgroundColor = 'transparent';
		        }

//		        else if (objSubMe.innerHTML == "取消新增") {
//		            if (objSubMe.parentNode.parentNode.parentNode.parentNode.rows.length > 2)
//		                objSubMe.parentNode.parentNode.parentNode.parentNode.deleteRow(objSubMe.parentNode.parentNode.rowIndex); //.innerHTML="删除";
//		            else
//		                window.status = "不能删除";
//		        }
		    }
		</script>
</head>
<body onload="return window_onload()">
    <form id="Form1" name="Form1" method="post" target="frmDataRun" 
			action="../../test.aspx?<%=Request.ServerVariables["Query_String"]%>">
    
    <div id="divSubsMe">
		<%
            string hTableFields = "";
            //为了上下通过键盘移动,取ID数据组
            string strAllIds = "";
    
            if (NodeTdTitleList != null && NodeTdTitleList.Count > 0)
            {
               
                XmlNode NodeSubMeParent = NodeTdTitleList[0].ParentNode.ParentNode;
                //XmlNodeList tablesSubsMe = NodeSubMeParent.ParentNode.SelectNodes("Table");
                XmlNode EachSub = NodeSubMeParent;
                //foreach(XmlNode EachSub in tablesSubsMe)
                {
                    string tableCName = EachSub.Attributes.GetNamedItem("TableCName").InnerXml;
                    string tableEName = EachSub.Attributes.GetNamedItem("EName").InnerXml;
                    //string a = "a,b,c,d";
                    //string b = "key,printtimes,pdffiles";
                    
                    XmlNodeList tdSubsMe = EachSub.SelectNodes("tr/td[Batch/@DisplayOnQuery='Yes']");
                    if (tdSubsMe.Count <= 0)
                        Response.Write("没有设置批量操作的字段，请在功能设置-按钮处设置");
                    int intSubMeRows = 10;

                    //string strSubMeRows=(EachSub.Attributes.GetNamedItem("SubsMeIuputLines")!=null)?EachSub.Attributes.GetNamedItem("SubsMeIuputLines").InnerXml:"10";
                    intSubMeRows = hPageSize;// Int32.Parse(10);
                    if (intSubMeRows > iCount)
                        intSubMeRows = iCount;

                    //XmlNodeList NodeTrBodySubsMeList=null;
                    string SubMeDataLines = iCount.ToString();

                    //if(NodeTrBodyList!=null&&NodeTrBodyList.Count>0)
                    //{
                    //    NodeTrBodySubsMeList=NodeTrBodyList[0].SelectNodes("Table[@EName='"+tableEName+"']/tr");
                    //    if(NodeTrBodySubsMeList!=null)
                    //        SubMeDataLines=NodeTrBodySubsMeList.Count.ToString();
                    //}
                    if (Int32.Parse(SubMeDataLines) > intSubMeRows)
                        intSubMeRows = Int32.Parse(SubMeDataLines);
						
								
					%>
					<TABLE id="TableSubMeData" name="TableSubMeData" cellSpacing="0" cellPadding="1" border="0">
						<tr>
						<td nowrap align=left width="20%" title="<%=tableEName%>" style="FONT-WEIGHT: bold"><%=tableCName%></td>
						<td nowrap align=left width="20%" title="<%=tableEName%>" style="FONT-WEIGHT: bold;FONT-SIZE: 9pt">现第(<%=hPageBegins%>)页,(<%=hPageSize %>)记录, 共有<%=iRecordCount%>记录</td>
						
						<td nowrap align=right class="small" style="FONT-SIZE: 9pt" width="60%"><a href="#" disabled NoChange="No"  onclick="AddNewSubMe(this)"></a>&nbsp;</td>
						</tr>
						<tr>
							<td colspan=3>
								<TABLE id="TableSubMeData_<%=tableEName%>" name="TableSubMeData_<%=tableEName%>" cellSpacing="4" cellPadding="0" border="0" width="100%">
								<tr class="ListHeader" height="20" style="FONT-WEIGHT: bold">
									<%foreach (XmlNode EachSubTD in tdSubsMe)
                                   {
                                       string strColumnWidth = EachSubTD.SelectSingleNode("Batch/@DisplayLength").InnerXml;
                                       string strColumnCName= EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;
										int iColumnWidth=1;
										try
										{
											iColumnWidth=Int32.Parse(strColumnWidth);
										}
										catch
										{
											iColumnWidth=strColumnCName.Length;
										}//(<=iColumnWidth*15+7 >)
										//================允许空属性=================
                                        string strAllowNull = "No";
                                        try
                                        {
                                            strAllowNull = EachSubTD.Attributes.GetNamedItem("AllowNull").Value;
                                        }
                                        catch { }
                                        if (strAllowNull == "No")
                                            strAllowNull = "2";
                                        else
                                            strAllowNull = "";
                                        //====================End==================
                                    %>
									<td nowrap style="Width:<%=iColumnWidth*15+7%>px;" title="<%=EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml%>"><%=strColumnCName%><font face="Webdings" color="red"><%=strAllowNull %></font></td>
									<%}%>
									<td width="1%" nowrap>操作提示</td>
								</tr>
								<%for (int iRows = 0; iRows < intSubMeRows; iRows++)
                                {
                                 bool ExistRow = true;
								%>
								<tr>
									<%foreach (XmlNode EachSubTD in tdSubsMe)
                                   {
                                       string strColumnWidth = EachSubTD.SelectSingleNode("Batch/@DisplayLength").InnerXml;
                                       string strColumnCName = EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;
                                       int iColumnWidth = 1;
                                       try
                                       {
                                           iColumnWidth = Int32.Parse(strColumnWidth);
                                       }
                                       catch
                                       {
                                           iColumnWidth = strColumnCName.Length;
                                       }
                                       //iColumnWidth = iColumnWidth * 15+4;
                                       iColumnWidth = 98;
                                        %>
									<%//------------------------------------------------------------------------------------------------------
            //foreach(XmlNode EachSubTD in NodeTdTitleList)
            //{
            //字段类型;ColumnType="0" ColumnPrecision="20" 
            //表名 table1.field1 table1/table1/tablex.fieldx

            string strColumnType = EachSubTD.Attributes.GetNamedItem("ColumnType").InnerXml;
            string strColumnPrecision = EachSubTD.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
            string strTableName = RetrieveTableName(EachSubTD.ParentNode.ParentNode);
            //string strColumnCName = EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;
            string strColumnEName = EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml;

            string strKeyIndex = EachSubTD.Attributes.GetNamedItem("KeyIndex").InnerXml;

            //================只读属性=================
            string strNoChange = EachSubTD.Attributes.GetNamedItem("ReadOnly").Value;
            //====================End==================

            //================允许空属性=================
            string strAllowNull = "No";
            try
            {
                strAllowNull = EachSubTD.Attributes.GetNamedItem("AllowNull").Value;
            }
            catch { }
            //====================End==================

            string ValidateValue = " ";

            //<OPTION selected value="0">字符</OPTION>
            //<OPTION value="1">数字</OPTION>
            //<OPTION value="2">日期</OPTION>
            //<OPTION value="3">文件</OPTION>
            //<OPTION value="4">新闻信息</OPTION>
            //<OPTION value="5">下拉列表</OPTION> 
            //<OPTION value="6">登录者身份信息</OPTION> 
            switch (strColumnType)
            {
                case "0":
                    ValidateValue = " style=\"Width:" + iColumnWidth + "%;border:#666666 1px solid;\" onfocus=\"window.status='可以输入字符'\" ONKEYPRESS=\"window.status='可以输入字符';\" ";
                    break;

                case "1":
                    //ValidateValue=" onfocus=\"window.status='只能输入数字'\" ONKEYPRESS=\"event.returnValue=IsDigit();\" ";
                    ValidateValue = " style=\"Width:" + iColumnWidth + "%;border:#666666 1px solid;\" onfocus=\"window.status='只能输入数字'\" ONKEYPRESS=\"event.returnValue=IsValidateDigit(this);\" ";
                    break;
                case "2":
                    ValidateValue = " style=\"Width:" + iColumnWidth + "%;border:#666666 1px solid;\" onchange=\"IsDate(this);\" onfocus=\"setday(this);window.status='只能输入日期,格式yyyy-MM-DD';\"";
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    ValidateValue = " style=\"Width:" + iColumnWidth + "%\" ";
                    break;

            }

            //TableName.FieldName,TableName.FieldName;
            hTableFields = hTableFields + "," + EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;
            strAllIds = strAllIds + "," + EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml;

            XmlNode NodeData = null;
            string strTdData = "";
            //Response.Write(NodeTrBodySubsMeList.Count);  iRows <= (NodeTrBodySubsMeList.Count - 1)
            if (NodeTrBodySubsMeList != null && NodeTrBodySubsMeList.Count > 0)
            {
                NodeData = NodeTrBodySubsMeList[iRows].SelectSingleNode("td[@Column='" + strColumnEName + "']");
                if (NodeData != null)
                    strTdData = NodeData.InnerXml;
            }

            if (strKeyIndex == "Yes")
            {
                if (strTdData == "")
                    ExistRow = false;
                else
                    ExistRow = true;
            }
										
									%>
									
										<td onclick="LocateSubMeEditorMode(this,<%=ExistRow.ToString().ToLower()%>)" 
											ONKEYPRESS="LocateSubMeEditorMode(this,<%=ExistRow.ToString().ToLower()%>)" 
											nowrap class="small" style="FONT-SIZE: 9pt;CURSOR:hand" 
											title="<%=strColumnCName%>" align="center">
										<%
            //这里的代码是录入时的处理功能**********************************************width="=100/tdSubsMe.Count%"
            //FunctionRules[0]　功能按钮名
            //FunctionRules[1]　功能按钮风格
            //FunctionRules[2]　传入参数
            //FunctionRules[3]　是否定制功能
            //FunctionRules[4]　定制功能或核算功能规则
            //FunctionRules[5]　事件
            //FunctionRules[6]　传出参数

            XmlNode myFunction = EachSubTD.SelectSingleNode("Input/@FunctionOnInput");
            string[] FunctionRules;
            string FunctionName = "";
            string FunctinEvent = "";
            string FunctionButtonEvent = "";
            int intFunctionRulesLength = 1;
            string strOutPara = "";
            if (myFunction != null && myFunction.InnerXml.Trim() != "")
            {
                FunctionRules = myFunction.InnerXml.Split("|".ToCharArray());
                FunctionName = FunctionRules[0].Trim();
                intFunctionRulesLength = FunctionRules.Length;
                FunctionButtonEvent = "";
                if (FunctionRules[5].Trim() != "")
                {
                    FunctionButtonEvent = FunctionRules[5].Trim() + "=\"";
                    strOutPara = (FunctionRules[6].Trim() == "") ? strColumnCName : FunctionRules[6].Trim();
                    FunctionButtonEvent += "Javascript:Run" + strColumnEName
                        + "F(" + FunctionRules[3].Trim().ToLower() + ",'"
                        + FunctionRules[4].Trim() + "','"
                        + FunctionRules[2].Trim() + "','"
                        + strOutPara + "')" + "\"";
												%>
													<script language="javascript">
													function Run<%=strColumnEName%>F(bUserDefinedModul,ModulPath,InPara,OutPara)
													{
														if(bUserDefinedModul)
														{
															var strInPara="";
															strInPara=CollectInParaValues(InPara);
															var strDelimiter="?";
															if(ModulPath.indexOf("?")>0)
															{
																ModulPath=ReCollectInParaValues(ModulPath,strInPara);
																//alert(ModulPath);
																strDelimiter="&";
															}
															var strPath="SelectModalDialog.aspx?" 
																+ ModulPath + strDelimiter
																+ "FunctionString=" 
																+ strInPara;
															//alert(strPath);
															var DlgRtnValue = window.showModalDialog(strPath, 
																"", "status:yes;resizable:yes;dialogHeight:560px;dialogWidth:780px;center:yes");
																
															if(DlgRtnValue != void 0)
															{
																//alert(DlgRtnValue);
																setOutParaValues(DlgRtnValue,OutPara,'<%=strColumnCName%>');
															}
														}
														else
														{
															
														}
													}


                                                    </script>
												<%
            }

                if (FunctionName != "" && intFunctionRulesLength > 5)
                {
												%>
												<table cellpadding="0" cellspacing="0" border="0" width="100%">
												<tr>
													<td  width="90%">
												<%
            }
                else
                {
                    //此时由当前控件完成此功能
                    FunctinEvent = FunctionButtonEvent;
                }
            }
            //这里的代码是录入时的处理功能**********************************************
										%>
										<%
										
            int ColumnHeight = 1;
            //if(EachSubTD.SelectSingleNode("Input/@ColumnHeight").InnerXml!="")
            //{
            //    try
            //    {
            //        ColumnHeight=Int32.Parse(EachSubTD.SelectSingleNode("Input/@ColumnHeight").InnerXml);
            //    }
            //    catch
            //    {
            //        ColumnHeight=1;
            //    }
            //}
            //bool ExistRow=true;
            if (strKeyIndex == "Yes")
            {
											%>
												<input type=hidden title="<%=strColumnCName%>" id="<%=strTableName + "_" + strColumnEName%>" value="<%=strTdData%>">
											<%
            }


            switch (EachSubTD.Attributes.GetNamedItem("ColumnType").InnerXml)
            {
                case "0":
                case "1":
                case "2":
												%>
													<% if (ColumnHeight == 1)
                {%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													type="text" method="=" disabled AllowNull="<%=strAllowNull%>"
													id="<%=strColumnEName%>" <%=ValidateValue%>
													value="<%=strTdData%>" ColumnDefault="<%=RetrieveDefaultValue(EachSubTD,NodeTrBodyList)%>">
												<%
            }
                
                break;
                case "3"://文件
												%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													type="text" size="0" method="=" disabled style="WIDTH:0px;HEIGHT:20px"
													id="<%=strColumnEName%>" columnType="文件" AllowNull="<%=strAllowNull%>"
													value="<%=strTdData%>">
													<%if (strTdData != "")
               {%><a id="<%=strColumnEName%>0a0" href="DownLoadFile.aspx?File=<%=strTdData%>" onclick="return DownloadFile('<%=strColumnEName%>',this)" NoChange="<%=strNoChange%>">下载</a
													>&#20;<a id="<%=strColumnEName%>1b1" href="#" onclick="DeleteFile('<%=strColumnEName%>','DeleteFile.aspx?File=<%=strTdData%>','frmDataRun')" disabled NoChange="<%=strNoChange%>">删除</a
													>&#20;<%}%><a href="#" onclick="javascript:uploadFile('<%=strColumnEName%>','DeleteFile.aspx?File=<%=strTdData%>','frmDataRun',this)" disabled NoChange="<%=strNoChange%>">上传</a>
												<%
            break;
                case "4"://新闻
												%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>"  NoChange="<%=strNoChange%>"
													type="text" size="0" method="=" disabled style="WIDTH:0px;HEIGHT:20px" 
													id="<%=strColumnEName%>" columnType="新闻" AllowNull="<%=strAllowNull%>"
													value="<%=strTdData%>">
													<%if (strTdData != "")
               {%><a href="#" onclick="javascript:BrowseNews('<%=strColumnEName%>','<%=DataBase%>')" NoChange="<%=strNoChange%>">浏览信息</a
													>&#20;<%}%><a href="#" onclick="javascript:EditNews('<%=strColumnEName%>','<%=DataBase%>','<%=EachSubTD.Attributes.GetNamedItem("ColumnDefault").InnerXml%>')" disabled NoChange="<%=strNoChange%>">编辑信息</a>
												<%	
            if (ColumnHeight > 1)
            {
														%>	<br>
															<iframe name="frm<%=strColumnEName%>" id="frm<%=strColumnEName%>" width="98%" style="BORDER:skyblue 2px solid" scrolling="no"  
															height="<%=ColumnHeight*22%>" 
															<%if(Request.QueryString["btnid"]=="BAdd"){%>
															src="inputBrowseNews.aspx?FilePath=<%=DataBase%>&Template=<%=EachSubTD.Attributes.GetNamedItem("ColumnDefault").InnerXml%>"
															<%}else{%>
															src="inputBrowseNews.aspx?FilePath=<%=DataBase%>&FileName=<%=strTdData%>"
															<%}%>
															 frameborder=1></iframe>
														<%
            //iEachColsCountEnum =iEachColsCountEnum+ColumnHeight-1; style="width:100%" style="width:100%"
        }
            break;
                case "5"://列表
												%>
													<%
            switch (strColumnPrecision)//精度
            {
                case "1": //下拉单选
													%>
																<select title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
																size="1"  style="width:98%" method="=" readonly disabled AllowNull="<%=strAllowNull%>"
																id="<%=strColumnEName%>" ColumnDefault="<%=RetrieveDefaultValue(EachSubTD,NodeTrBodyList)%>">
																<option></option>
																<%=RetrieveDropDownList(EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml, EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml, strColumnPrecision, strTdData)%>
																</select>
																<%
            break;

                case "2"://下拉多选
																%>
																<SELECT title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
																style="width:98%" method="=" readonly disabled AllowNull="<%=strAllowNull%>"
																id="<%=strColumnEName%>"  multiple size="3">
																	<option></option>
																	<%=RetrieveDropDownList(EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml, EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml, strColumnPrecision, strTdData)%>
																</SELECT>
															<%	
            //iEachColsCountEnum =iEachColsCountEnum+1;
            break;

                case "3": //Radio 单选
															%><%=RetrieveRadioCheckList(strColumnEName, "Radio", EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml, EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml, strColumnPrecision, strTdData, strNoChange)%>
																<%
            break;
                case "4"://Check 多选
															%><%=RetrieveRadioCheckList(strColumnEName, "CheckBox", EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml, EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml, strColumnPrecision, strTdData, strNoChange)%>
															
														<%
            break;
            }
														 %>
													
													
												<%
            break;
                case "6":
												%>
													<select title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													size="1" method="=" readonly disabled AllowNull="<%=strAllowNull%>" <%=ValidateValue%>
													id="<%=strColumnEName%>" ColumnDefault="<%=RetrieveDefaultValue(EachSubTD,NodeTrBodyList)%>">
													<%=RetrieveUserInfo(EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml, EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml, strColumnPrecision, strTdData)%>
													</select>
												<%
            break;
                default:
            break;
            }
										%>
										
										<%
            if (myFunction != null && myFunction.InnerXml.Trim() != "")
            {
                if (FunctionName != "" && intFunctionRulesLength > 5)
                {
											%>
											</td><td width="10%" align="center">
												<input type="button" value="<%=FunctionName%>" style="width:<%=FunctionName.Length*15+5%>px" <%=FunctionButtonEvent%>></td>
											</tr>
											</table>
											<%}
            }//------------------------------------------------------------------------------------------------------
										%></td>
									
									<%}%>
									<!--td nowrap style="<%//=ExistRow?"FONT-WEIGHT: bold;":"FONT-WEIGHT: normal;"%>FONT-SIZE: 9pt"><a href="#" disabled NoChange="No"  onclick="AddNewSubMe(this,<%=ExistRow.ToString().ToLower()%>)"><%=ExistRow?"复制":"&nbsp;"%></a></td-->
									<td nowrap style="text-decoration:underline; <%//=ExistRow?"FONT-WEIGHT: bold;":"FONT-WEIGHT: normal;"%>FONT-SIZE: 9pt"><a href="#" disabled NoChange="No" onclick="CancelSubMe(this,<%=ExistRow.ToString().ToLower()%>)"><%=ExistRow ? "" : "&nbsp;"%></a></td>
								</tr>
								<%}%>
								</TABLE>
							</td>
						</tr>
					</TABLE>
					<%
            }
            }
            else
            {
                Response.Write("没有设置批量操作的字段，请在功能设置-按钮处设置");
            }		    
			
		%>
		
		
		</div>
		<input type="hidden" id="hAction" name="hAction" value="BModify" />
		<input type="hidden" id="hDataCollectionSubMes" name="hDataCollectionSubMes" value="">
		<input type="hidden" id="hQueryCollection" name="hQueryCollection" value="">
			
			<input type="hidden" id="txtBatches" name="txtBatches" value="">
			<input type="hidden" id="hSubTablesCopy" name="hSubTablesCopy" value="">
			<input type="hidden" id="hNotAllowNull" name="hNotAllowNull" value="">
			<input type="hidden" id="hAllIds" name="hAllIds" value="<%=strAllIds%>">
			
		<textarea id="hTxt" rows="5" cols="0" style="display:none"></textarea>
    </form>
</body>
</html>
