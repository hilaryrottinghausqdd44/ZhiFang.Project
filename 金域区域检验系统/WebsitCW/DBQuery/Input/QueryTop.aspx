<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Input.QueryTop" Codebehind="QueryTop.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Globalization" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QueryTop</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%if(cssFile.Trim()==""){%>
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<%}else{%>
		<LINK href="../<%=cssFile%>" type="text/css" rel="stylesheet">
		<%}%>
		<!--script language="javascript" src="../../Includes/JS/Calendar.js"></script-->
		<!--#include file="../../Includes/JS/Calendar.js"-->
		<script language="javascript" src="../../Includes/JS/Dialog.js"></script>
		<script language="javascript" src="../../Includes/JS/GB2312.js"></script>
		<script language="javascript">
		String.prototype.trim= function()  
		{  
		// 用正则表达式将前后空格  
		// 用空字符串替代。  
		 return this.replace(/(^\s*)|(\s*$)/g, "");  
		}

		function trans(){
			result.innerHTML="结果如下："
			result.innerHTML+='<br>UrlEncode(source.value):<br>'+UrlEncode(source.value)
			result.innerHTML+='<br>getSpell(source.value):<br>'+getSpell(source.value)
			result.innerHTML+='<br>getSpell(source.value,"\'"):<br>'+getSpell(source.value,"'")
		}

		function spellSort(a,b){
			var p=strGB.indexOf(a.substr(0,1))
			var q=strGB.indexOf(b.substr(0,1))
			document.write(p,"-",q,"=",p-q,"<br>");
			return p-q;
		}



		function spellSort(a,b){
			/***********(qiushuiwuhen 2002-9-17)************/
			var i,p,q,l=strGB.length;
			p=0;for(i=a.length;i>0;i--)p=p/l+strGB.indexOf(a.substr(i-1,1))
			q=0;for(i=b.length;i>0;i--)q=q/l+strGB.indexOf(b.substr(i-1,1))
			return p-q;
		}
		function sort(str){
			var arr=str.split(","),arr2=[];
			arr.sort(spellSort);
			alert(arr)
		}
		</script>
		<script>
		var sel="",timer=null;
		function spellList(){
		/********(qiushuiwuhen 2002-9-20)***********/
			with(window.event){
				with(srcElement){
					if(keyCode<48)return;
					if(keyCode>95)keyCode-=48
					sel+=String.fromCharCode(keyCode)
					window.status=sel
					for(i=0;i<length;i++){
              			if(!options[i].sp){
              				var tmp="",arr=getSpell(options[i].text,"'").split("'")
              				for(var j=0;j<arr.length;j++)tmp+=arr[j].substr(0,1).toUpperCase();
              				options[i].sp=tmp;
              			}
              			if(options[i].sp.indexOf(sel)==0){selectedIndex=i;break;}
					}
				}
				returnValue=false;
				clearTimeout(timer)
				timer=setTimeout("sel=''",500);
			}
		}
		</script>
		<script language="javascript">
		var hWhereClause="";
			
		function FireQuery()
		{
			Form1.acceptCharset="gb2312";
			Form1.action=parent.frames['ContentMain'].location;//"InputOneTable.aspx";
			Form1.target='ContentMain';
			hWhereClause="";
			CollectWhereClause(TableData.childNodes);
			if(hWhereClause.length>0)
				hWhereClause=hWhereClause.substr(1);
			Form1.hQueryCollection.value=hWhereClause;
			//alert(hWhereClause);
			
			Form1.submit();
			
		}
		function FireReset()
		{
			ResetAll(TableData.childNodes);
			
		}
		function ResetAll(kids)
		{
			for(var i=0;i<kids.length;i++)
			{
				if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
				{
					if(kids[i].value!="")
					kids[i].value="";
				}
				if(kids[i].nodeName.toUpperCase()=='SELECT')
				{
					if(kids[i].options[kids[i].selectedIndex].text!="")
						kids[i].options[0].selected=true;
				}
				if(kids[i].hasChildNodes)
					ResetAll(kids[i].childNodes);
			}
		}
		
		function CollectWhereClause(kids)
		{
			for(var i=0;i<kids.length;i++)
			{
				if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
				{
					if(kids[i].value!="")
					hWhereClause +="\n" +  kids[i].id + kids[i].method + kids[i].value;
				}
				if(kids[i].nodeName.toUpperCase()=='SELECT')
				{
					if(kids[i].options[kids[i].selectedIndex].text!="")
						hWhereClause +="\n" +  kids[i].id + kids[i].method + kids[i].options[kids[i].selectedIndex].text;
				}
				if(kids[i].hasChildNodes)
					CollectWhereClause(kids[i].childNodes);
			}
		}
		
		function SelectParentObjValue(obj,selectedValue)
		{
			//alert(typeof(obj)=="undefined");
			if(typeof(obj)=="undefined")
				return;
			if(obj.nodeName.toUpperCase()=='SELECT')
			{
				for(var eachOption=0; eachOption<obj.options.length;eachOption++)
				{
					if(obj.options[eachOption].text==selectedValue)
						obj.options[eachOption].selected=true;
				}
			}
		}
		
		function setTody(obj,strDate)
		{
			if(obj.value=="")
				obj.value=strDate;
		}
		function SetSortSearch(sortdata,sqlType)
		{
			Form1.acceptCharset="gb2312";
			Form1.action=parent.frames['ContentMain'].location;//"InputOneTable.aspx";
			Form1.target='ContentMain';
			hWhereClause="";
			CollectWhereClause(TableData.childNodes);
			if(hWhereClause.length>0)
				hWhereClause=hWhereClause.substr(1);
				
			//分割"and",暂时不能处理"or"  ,分割后的数组要处理掉空格
			var arraydata="";	
			if(sqlType=="XML")
			{
				if(sortdata.indexOf(" and ")<0||sortdata.indexOf(" or ")<0)
				{
					arraydata = sortdata;
				}
				else
				{
					sortdata=sortdata.replace(/ or /g," and ");
					var datacomb = sortdata.split(" and ")
					for(var i=0;i<datacomb.length;i++)
					{
						arraydata +='<%=TableEName%>'+"."+datacomb[i].trim()  + "\n";
					}
					//arraydata.substr(1);
					//alert(arraydata);
					//arraydata.trim();
				}
			}
			else
				arraydata="$$SQL=" + sortdata
				
			hWhereClause +=  "\n" + arraydata;
			Form1.hQueryCollection.value=hWhereClause;
			//alert(hWhereClause);

			Form1.submit();
		}
		
		function findNextFocus()
		{
				if ((window.event.altKey)&&
				((window.event.keyCode==37)|| //屏蔽 Alt+ 方向键 ←
				(window.event.keyCode==39))){ //屏蔽 Alt+ 方向键 →

						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}

				if ((event.keyCode == 8) && 
				(event.srcElement.type != "text" && 
				event.srcElement.type != "textarea" && 
				event.srcElement.type != "password") || //屏蔽退格删除键 
				(event.keyCode==116)|| //屏蔽 F5 刷新键
				(event.ctrlKey && event.keyCode==82)){ //Ctrl + R
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if ((event.ctrlKey)&&(event.keyCode==78)) //屏蔽 Ctrl+n
				{
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if ((event.shiftKey)&&(event.keyCode==121)) //屏蔽 shift+F10
				{
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if (window.event.srcElement.tagName == "A" && window.event.shiftKey) 
				{
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if ((window.event.altKey)&&(window.event.keyCode==115))
				{ //屏蔽Alt+F4
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false;
				}
			

			//if(event.srcElement.tagName == "TEXTAREA"
			//||event.srcElement.tagName == "SELECT")
			//alert(event.keyCode);
			var up,down,left,right; 
			up = 38; 
			down = 40; 
			left = 37; 
			right = 39; 
			enter =13;
			
			switch(event.keyCode) 
			{ 
				case left:{ 
					if(event.srcElement.tagName == "SELECT")
					{
						var objFocus=event.srcElement;
						
						if(objFocus.id!=null&&objFocus.id!="")
						{
							var thisID=objFocus.id;
							var allIds=document.all["hAllIds"].value;
							if(allIds.indexOf(thisID)>0)
							{
								//var arrayIDs=allIds.split(",");
								allIds=allIds.substr(0,allIds.indexOf(thisID)-1);
								
								var previousID="";
								while(document.getElementById(previousID)==null)
								{
									if(allIds.length<=1)
										break;
									previousID=allIds.substr(allIds.lastIndexOf(",")+1);
									//alert(allIds);
									
									var preObject=document.getElementById(previousID);
									if(preObject!=null)
									{
										preObject.focus();
										break;
									}
									allIds=allIds.substr(0,allIds.lastIndexOf(","));
									
								}
							}
						}
					}
					break; 
					} 
				case up:{ 
					if(event.srcElement.tagName == "SELECT"
					||event.srcElement.tagName == "TEXTAREA")
						return;
					var objFocus=event.srcElement;
					
					if(objFocus.id!=null&&objFocus.id!="")
					{
						var thisID=objFocus.id;
						var allIds=document.all["hAllIds"].value;
						if(allIds.indexOf(thisID)>0)
						{
							//var arrayIDs=allIds.split(",");
							allIds=allIds.substr(0,allIds.indexOf(thisID)-1);
							
							var previousID="";
							while(document.getElementById(previousID)==null)
							{
								if(allIds.length<=1)
									break;
								previousID=allIds.substr(allIds.lastIndexOf(",")+1);
								//alert(allIds);
								
								var preObject=document.getElementById(previousID);
								if(preObject!=null)
								{
									preObject.focus();
									break;
								}
								allIds=allIds.substr(0,allIds.lastIndexOf(","));
								
							}
						}
					}
					
					//while(objFocus.previousSibling==null)
					//{
					//	objFocus=objFocus.parentNode;
					//	objFocus.focus();
					//}
					
					break; 
					} 
				case right:{
					if(event.srcElement.tagName == "SELECT")
						event.keyCode=9;
					break; 
					} 
				case down:{ 
					if(event.srcElement.tagName != "SELECT"&&event.srcElement.tagName != "TEXTAREA")
						event.keyCode=9;
					break; 
					} 
				case enter:{ 
					if(event.srcElement.tagName != "TEXTAREA")
						event.keyCode=9;
					break; 
					} 
			} 
		}

		function window_onload() {
		    //FireRequeryQuery();
		}
		function FireRequeryQuery() {
		    if (parent.window.frames["ContentMain"] != null
		        && parent.window.frames["ContentMain"].frames["Right"] != null
		        && parent.window.frames["ContentMain"].frames["Right"].document.readyState == "complete"
		        && parent.document.readyState=="complete"
                ) {
		        FireQuery();
		    }
		    else {
		        window.setTimeout(FireRequeryQuery, 200);
		    }
		}
		</script>
	</HEAD>
	<body class="QueryBody"  onkeydown="findNextFocus();" onload="return window_onload()">
		<%//Response.Write(QueryDoc.InnerXml);%>
		<%//Response.Write("<br>时间：" + DateTime.Now + ":" + DateTime.Now.Millisecond);%>
		<table id="TableTopQuery" border="0" align="center" cellpadding="0" cellspacing="0" width="98%">
			<%
			if(thisQueries!=null&&UserDefinedGroupQuery)
				{
			%>
			<tr height="30px">
				<td colspan=3>
				<div id="SortSearch" style="hand:CURSOR">
					<%
						foreach(XmlNode myNode in thisQueries)
						{
							string strColumnValue = myNode.InnerXml;
							string strQuseryData = myNode.Attributes.GetNamedItem("WhereClause").InnerXml;
							//string strTableName=RetrieveTableName(myNode.ParentNode.ParentNode);
							//string strColumnName = strColumnValue
							
							if(myNode.Attributes.GetNamedItem("SQL")==null||myNode.Attributes.GetNamedItem("SQL").InnerXml.Trim()=="")
							{
								if(strQuseryData.IndexOf(" and ")<0||strQuseryData.IndexOf(" or ")<0)
								{
									%> 
									<a class="QueryGroup" href="javascript:SetSortSearch('<%=TableEName%>.<%=strQuseryData%>','XML')"><%=strColumnValue%></a>
									<%
												
											}else{
											

									%>
									<a class="QueryGroup" href="javascript:SetSortSearch('<%=strQuseryData%>','XML')"><%=strColumnValue%></a>
									<%
								}
							}
							else
							{
								%>
								<a class="QueryGroup" href="javascript:SetSortSearch('<%=myNode.Attributes.GetNamedItem("SQL").InnerXml.Replace("\\","\\\\").Replace("\n"," ").Replace("\r"," ").Replace("'","\\'")%>','<%=XmlDBType%>')"><%=strColumnValue%></a>
								<%
							}
						}
					%>
					
				</div>
				</td>
			</tr>
			<%}
			%>
			<tr  style="Display:<%if(tdNodeList==null||tdNodeList.Count==0) Response.Write("none");%>">
				<td align="right">
					<table id="TableData" border="0" align="center" cellpadding="0" cellspacing="0" width="100%">
						<tr class="Query">
							<%
								//键盘操作用id数组
								string strAllIds="";
								
								int i=0;
								if(iCols==0)iCols=1;
								string hTableFields="";
								if(tdNodeList!=null)
								{
									foreach(XmlNode myNode in tdNodeList)
									{
										if(!myOwnNode(myNode))
											continue;
										
										//字段类型;ColumnType="0" ColumnPrecision="20" 
										//表名 table1.field1 table1/table1/tablex.fieldx
										
										string strColumnType=myNode.Attributes.GetNamedItem("ColumnType").InnerXml;
										string strColumnPrecision="";
										if(myNode.Attributes.GetNamedItem("ColumnPrecision")!=null)
											strColumnPrecision=myNode.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
											
										string strTableName=RetrieveTableName(myNode.ParentNode.ParentNode);
										string strColumnCName=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
										string strColumnEName=myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
										
										//===============查询默认值配置==============
										string strQueryDefault = string.Empty;
										XmlNode inputQueryNode = myNode.SelectSingleNode("InputQuery");
										if(inputQueryNode != null)
										{
											if(inputQueryNode.Attributes.GetNamedItem("QueryDefault") != null)
											{
												strQueryDefault = inputQueryNode.Attributes.GetNamedItem("QueryDefault").Value;
											}
										}
								
										//=================
										
										string ValidateValue=" ";
										
										//<OPTION selected value="0">字符</OPTION>
										//<OPTION value="1">数字</OPTION>
										//<OPTION value="2">日期</OPTION>
										//<OPTION value="3">文件</OPTION>
										//<OPTION value="4">新闻信息</OPTION>
										//<OPTION value="5">下拉列表</OPTION> 
										//<OPTION value="6">登录者身份信息</OPTION> 
										
										string SelectTimeDialog="";
										switch(strColumnType)
										{
											case "1":
												break;
											case "2":
												ValidateValue=" ONKEYPRESS=\"event.returnValue=IsDigit();\" ";
												string DateString=System.DateTime.Now.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
												SelectTimeDialog=" onfocus=\"setTody(this,'" + DateString +"'); this.select();\"' ";
												break;
											case "3":
												break;
											case "4":
												break;
											case "5":
												break;
											case "6":
												break;
												
										}
										
										//TableName.FieldName,TableName.FieldName;
										hTableFields=hTableFields + "," + myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
										strAllIds=strAllIds + "," + strTableName + "." +strColumnCName;
									%>
							<td width="12px">&nbsp;</td>
							<td nowrap align="right"><%=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml%>&nbsp;</td>
							<td nowrap width="<%=100/(iCols+1)-5%>%">
								<%
										switch(myNode.SelectSingleNode("InputQuery").Attributes.GetNamedItem("DisplayMethod").InnerXml)
										{
											case "":
											case "文本框":
												%>
								<input title="<%=strColumnCName%>" 
													type="text" style="width:100%" method="!!"
													id="<%=strTableName%>.<%=strColumnCName%>"
													value="<%=ParentObjValue(strTableName,strColumnEName, strQueryDefault)%>">
								<%
												break;
											case "区间":
												%>
								<input title="<%=strColumnCName%>" 
													type="text" style="width:48%" method=">="
													id="<%=strTableName%>.<%=strColumnCName%>"
													value="<%=ParentObjValue(strTableName,strColumnEName, strQueryDefault)%>" <%=SelectTimeDialog%>>-<input title="<%=strColumnCName%>" 
													type="text" style="width:48%" method="<="
													id="<%=strTableName%>.<%=strColumnCName%>"
													value="<%=ParentObjValue(strTableName,strColumnEName, strQueryDefault)%>" <%=SelectTimeDialog%>>
								<%
												break;
											case "下拉列表":
												%>
								<select title="<%=strColumnCName%>" onkeydown=spellList()
													size="1" style="WIDTH:100%" method="="
													id="<%=strTableName%>.<%=strColumnCName%>">
									<%if(strColumnType!="6"){%>
									<option selected></option>
									<%}%>
									<%
													string SelectOptions="";
													string DropDownListSource="";
													if(myNode.SelectSingleNode("Dictionary/@DataSource")!=null)
														DropDownListSource=myNode.SelectSingleNode("Dictionary/@DataSource").InnerXml;
													
													switch(strColumnType)
													{
														case "5":
															if(DropDownListSource=="Local")
																SelectOptions=RetrieveLocalDropDownList(myNode.SelectSingleNode("Dictionary/@DataSourceName"), strQueryDefault);
															else if(DropDownListSource=="Fixed")
																SelectOptions=RetrieveFixedDropDownList(myNode.SelectSingleNode("Dictionary/@DataSourceName").InnerXml, strQueryDefault);
															else if(DropDownListSource=="Remote")
																SelectOptions="<option>Remote...</option>";//动态未知
															else
																SelectOptions="<option>Error</option>";//未知
															break;
														case "6":
															SelectOptions=RetrieveUserInfo(myNode.SelectSingleNode("Dictionary/@DataSource").InnerXml,myNode.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,"");
															//SelectOptions="<option>登录信息</option>";
															break;
														default:
															SelectOptions=RetrieveDistinctValueList(strTableName,strColumnEName);
															break;
													}%>
									<%=SelectOptions%>
								</select>
								<script language="javascript">
									SelectParentObjValue(document.all["<%=strTableName%>.<%=strColumnCName%>"],"<%=ParentObjValue(strTableName,strColumnEName, strQueryDefault)%>");
								</script>
								<%//=DropDownListSource%>
								<%
												break;
											default:
												break;
										}
										%>
							</td>
							<%
									i++;
									if((int)i/iCols==(double)i/iCols&&i!=tdNodeList.Count)
										Response.Write("</tr><tr class=\"Query\">");
									}
								}%>
						</tr>
					</table>
				</td>
				<td width="10%" align="center"><div align="center"><img src="../image/middle/search.jpg" width="63" height="22" border="0" style="CURSOR: hand"
							onmouseover="this.style.border='#ccccff thin outset'" onmouseout="this.style.border='#ccccff 0px outset'" onmousedown="this.style.border='#ccccff thin inset'"
							onmouseup="this.style.border='#ccccff thin outset'" onclick="FireQuery()"></div>
				</td>
				<TD align="center" width="10%"><img src="../image/middle/Reset.jpg" width="63" height="22" border="0" style="CURSOR: hand"
							onmouseover="this.style.border='#ccccff thin outset'" onmouseout="this.style.border='#ccccff 0px outset'" onmousedown="this.style.border='#ccccff thin inset'"
							onmouseup="this.style.border='#ccccff thin outset'" onclick="FireReset()"></TD>
			</tr>
		</table>
		<input type="hidden" id="hAllIds" name="hAllIds" value="<%=strAllIds%>">
		<%
		int meFrameHeight=23;
		if(tdNodeList!=null){
		meFrameHeight=i/iCols*23;
		if(meFrameHeight<(double)i/iCols*23)
			meFrameHeight=(i/iCols+1)*23;
		if(thisQueries!=null&&thisQueries.Count>0)
			meFrameHeight +=35;
		string TopVisible="true";
		if((thisQueries==null||thisQueries.Count==0||!UserDefinedGroupQuery)&&tdNodeList.Count==0)
			TopVisible="false";
		
		%>
		<script>
			parent.frames["Top"].frameElement.height=TableTopQuery.clientHeight+5;//'<%=meFrameHeight%>';            //分类查询的高度暂时设置为30 TableTopQuery.style.height//
			//alert(document.all["TableTopQuery"].clientHeight);
			
			if(!<%=TopVisible%>)
				parent.frames["Top"].frameElement.height=0;//'<%=meFrameHeight%>';            //分类查询的高度暂时设置为30 TableTopQuery.style.height//
			


        </script>
		<%}%>
		<!--input type="text" id="text1" onfocus="setday(this)" name="text1" style="WIDTH: 0px; HEIGHT:0px;	LEFT: 10px; POSITION: relative; TOP: -194px" size="78"></input日期控件-->
		<form id="Form1" method="post" action="">
			<input type="hidden" id="hTableFields" name="hTableFields" value="<%=hTableFields%>">
			<input type="hidden" id="hQueryCollection" name="hQueryCollection">
		</form>
		<%//Response.Write("<br>时间：" + DateTime.Now + ":" + DateTime.Now.Millisecond);%>
	</body>
</HTML>
