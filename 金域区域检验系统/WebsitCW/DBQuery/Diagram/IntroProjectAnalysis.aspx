<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Text.RegularExpressions" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Diagram.IntroProjectAnalysis" Codebehind="IntroProjectAnalysis.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>单表</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%if(cssFile.Trim()==""){%>
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<%}else{%>
		<LINK href="../<%=cssFile%>" type=text/css rel=stylesheet >
		<%}%>
		<script language="javascript">
		var SelTD = null;
			
		///有bug,应该传递自身的AssesorXpath
		function SelectTd(eid,DataXPath)
		{
			eid.style.backgroundColor= 'skyblue';
			if(DataXPath!=null&&DataXPath!="")
					var a='a';//Expound(DataXPath);
			if (SelTD != null&&SelTD!=eid)
			{
				SelTD.style.backgroundColor = 'Transparent';
			}
			SelTD = eid;				
		}
		</script>
		<script language="javascript">
		function Expound(DataXPath)
		{	
			try
			{
				<%
				string strTemp=Request.ServerVariables["Query_String"];
				if(strTemp.IndexOf("&AssessorXpath")>-1)
					strTemp=strTemp.Remove(strTemp.IndexOf("&AssessorXpath"),strTemp.IndexOf("&",strTemp.IndexOf("&AssessorXpath")+1)-strTemp.IndexOf("&AssessorXpath"));
				%>
				var strTemp="<%=strTemp%>"
				parent.document.all["hParentXpath"].value=DataXPath;
				window.frames["Right"].location="InputOneTableMain.aspx"
					+"?"+strTemp+"&DataXPath="
					+ DataXPath +"&AssessorXpath=" +DataXPath;// + "&Items=北京垂杨柳医院";
				//alert(parent.document.all["hParentXpath"].value);
			}
			catch(e){}
		}
		function Add()
		{
			var TbStr=document.all["ddd"].innerHTML;
			
			TbStr=TbStr.substring(0,TbStr.length-8);
			TbStr=TbStr+"<TR><TD>3</TD><TD></TD><TD></TD><TD></TD></TR></TBODY>";
			alert(TbStr)
			document.all["ddd"].innerHTML=TbStr;
			alert(document.all["ddd"].length)
		}
		function TdOrderBy(ColumnIndex,orderByDirection)
		{
			var thisTable=document.all["TableOrderBy"];
			//thisTable.deleteRow(ColumnIndex);
			var thisTableTrs=thisTable.rows;
			var rowsCount=thisTableTrs.length;
			
			for(var i=1;i<rowsCount;i++)
			{
				for(var j=i+1;j<rowsCount;j++)
				{
					//alert(thisTableTrs[i].cells[ColumnIndex].innerHTML>thisTableTrs[j].cells[ColumnIndex].innerHTML);
					//return;
					if(thisTableTrs[i].cells[ColumnIndex].innerHTML>thisTableTrs[j].cells[ColumnIndex].innerHTML)
					{
						/*
						window.setTimeout("applyTransition()",330);
						bStart=true;
						while(bStart)
						{
							var x=1;
						}
						*/
						swapTr(thisTable,i,j);
					}	
				}
			}
		}
		
		var bStart=true;
		function applyTransition()
		{
			window.status="aaa";
			bStart=false;
		}
		function swapTr(thisTable,i,j)
		{
			thisTable.rows[i].swapNode(thisTable.rows[j]);
			return;
			//thisTable.insertRow(j);
			var newTR = thisTable.insertRow(j+1);
			//return;
			
			newTR.runtimeStyle.backgroundColor =thisTable.rows[i].bgColor;
			

			for (var iCells = 0; iCells < thisTable.rows[0].cells.length; iCells++) 
			{
		 		newTD = newTR.insertCell();
				newTD.innerHTML = thisTable.rows[i].cells[iCells].innerHTML;
			}
			
			if(i>j)
				thisTable.deleteRow(i+2);
			else
				thisTable.deleteRow(i);
		}
		
		function RunPage(StartPage)
		{
			Form1.hPageBegins.value=StartPage;
			Form1.submit();
		}
		</script>
	</HEAD>
	<body class="ListBody">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="ButtonAnalysis" runat="server" Text="用户测试量统计" onclick="ButtonAnalysis_Click"></asp:Button>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" 
				border="0">
				<TR>
					<TD style="WIDTH: <%if(NodeTdTitleList.Count==0)Response.Write("0%");else Response.Write("20%");%>; " valign="top"  align="center">
						<table height="100%" style="DISPLAY:<%if(NodeTdTitleList.Count==0)Response.Write("none");%>" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td valign="top">
									<TABLE class="ListTable" id="TableOrderBy" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr class="ListHeader" height="20">
											<td nowrap style="Cursor:url('../image/cursors/H_IBEAM.CUR');WIDTH:15px" align="center" onclick="TdOrderBy(0,true)">序</td>
											<%
											int iCount=0;
											
											int iPages=0;
											
											int iCalculator=0;
											
											string xpathFirstRow="";
											
											string strKeyIndex="";
											
											int iOrderByLocation=0;
											
											int BlankTableRows=iPageSize;
											
											foreach(XmlNode myNode in NodeTdTitleList){
												string strColumnCName=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
												string strColumnEName=myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
												
												string strColumnWidth=myNode.SelectSingleNode("Query/@DisplayLength").InnerXml;
												int iColumnWidth=1;
												try
												{
													iColumnWidth=Int32.Parse(strColumnWidth);
												}
												catch
												{
													iColumnWidth=strColumnCName.Length;
												}
											
												iOrderByLocation++;	
												string strKeyIndex1=myNode.Attributes.GetNamedItem("KeyIndex").InnerXml;
												if(strKeyIndex1=="Yes")
													strKeyIndex=strColumnEName;%>
												<td nowrap align="center"  style="Cursor:url('../image/cursors/H_IBEAM.CUR'); Width:<%=iColumnWidth*12+7%>px"
													onclick="TdOrderBy(<%=iOrderByLocation%>,true)"><%=strColumnCName%></td>
											<%
											}%>
										</tr>
										<%
										if(NodeTrBodyList!=null)
										{
										iCount=NodeTrBodyList.Count;
										iPages=iCount/iPageSize;
										iPages++;
										if((int)iCount/iPageSize==(double)iCount/iPageSize)
											iPages--;
										//if(iPages>0)
										//	iPageBegins=1;
										
										//补空表格行
										
											
										foreach(XmlNode myTrNode in NodeTrBodyList){
											//翻页
											if(iCalculator<(iPageBegins-1)*iPageSize||iCalculator>=(iPageBegins)*iPageSize)
											{
												iCalculator++;
												continue;
											}
												iCalculator++;
												
											BlankTableRows--;
											
											if(strKeyIndex=="")
												strKeyIndex=myTrNode.SelectSingleNode("td").Attributes.GetNamedItem("Column").InnerXml;
											XmlNode nodeKeyIndex=myTrNode.SelectSingleNode("td[@Column='"+strKeyIndex+"']");
											string strItem="";
											if(nodeKeyIndex!=null)
												strItem=nodeKeyIndex.InnerXml;
											
											
											%>
											<tr id="aa<%=iCalculator%>" class="" bgcolor="<%if(xpathFirstRow==""){%>skyblue<%}else{%>Transparent<%}%>" height="20" style="cursor:hand"
												
												onclick="SelectTd(this,'<%=CollectAssessorXPath(myTrNode).Replace("'","\\'")%>')" 
												
												onmouseover="this.style.color= 'red';" 
												onmouseout="this.style.color= 'black';">
												<td nowrap><%=iCalculator%></td>
												<%foreach(XmlNode myNode in NodeTdTitleList){
												string strColumnEName=myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
												string strColumnCName=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
												XmlNode ThisNode=myTrNode.SelectSingleNode("td[@Column='"+strColumnEName+"']");
												string strData="&nbsp;";
												if(ThisNode!=null)
													strData=ThisNode.InnerXml;
												
												string strColumnWidth=myNode.SelectSingleNode("Query/@DisplayLength").InnerXml;
												int iColumnWidth=1;
												try
												{
													iColumnWidth=Int32.Parse(strColumnWidth);
												}
												catch
												{
													iColumnWidth=strColumnCName.Length;
												}	
												System.Text.Encoding en=System.Text.Encoding.GetEncoding("GB2312");
												String str=strData;
												str=Regex.Replace(str,"(&)[Nn][Bb][Ss][Pp](;)","");
												
												
												bool bLong=false;
												while(en.GetByteCount(str)>iColumnWidth*2)
												{
													str=str.Substring(0,str.Length-1);
													bLong=true;
												}
												if (bLong)
												{
													//最后为两个字符ASCII
													if(en.GetByteCount(str.Substring(str.Length-2))==2)
														str=str.Substring(0,str.Length-2) + "..";
													
													//最后为两个汉字UNICODE
													else if (en.GetByteCount(str.Substring(str.Length-2))==4)
														str=str.Substring(0,str.Length-1) + "..";
													
													//最后为一个字符ASCII＋一个汉字UNICODE
													else if (en.GetByteCount(str.Substring(str.Length-2))==3)
													{
														if (en.GetByteCount(str.Substring(str.Length-1))==1)
															str=str.Substring(0,str.Length-1) + ".";
														else if (en.GetByteCount(str.Substring(str.Length-1))==2)
															str=str.Substring(0,str.Length-1) + "..";
														
													}
															
												}
												%>
												<td title="<%=strColumnCName + "=" + strData%>"><%Response.Write(str==""?"&nbsp;":str);%></td>
												<%}%>
											</tr>
											
											<%
											if(xpathFirstRow=="")
											{
											%>
												<!--script language="javascript">SelTD=document.all['aa<%=iCalculator%>'];</script-->
												<script language="javascript">SelTD=document.all['aa<%=iCalculator%>'];SelectTd(document.all['aa<%=iCalculator%>'],'<%=CollectAssessorXPath(myTrNode).Replace("'","\\'")%>')</script>
											<%
											}	
											if(xpathFirstRow=="")
												xpathFirstRow=CollectAssessorXPath(myTrNode);
											
											}
											
										}%>
										<%
											for(int myBlank=0;myBlank<BlankTableRows;myBlank++)
											{
											%>
												<tr height="20" bgcolor="Transparent" style="cursor:not-allowed">
												<%for(int myTD=0;myTD<NodeTdTitleList.Count+1;myTD++){%>
												<td>&nbsp;</td>
												<%}%>
												</tr>
											<%
											}
										%>
									</TABLE>
								</td>
							</tr>
							<tr height="25">
								<td align="right">
									<table style="width:105px" height="25" border="0" cellpadding="0" cellspacing="0" class="ListRecord">
										<tr>
											<td width="2">&nbsp;</td>
											<td valign="bottom" nowrap><%=iPageBegins%>/<%=iPages%>页(共<%=iCount%>记录)</td>
											<td width="15">&nbsp;</td>
											<td width="24" valign="bottom"><img src="../image/bottom/Previous.jpg" border="0" 
												<%if(iPageBegins>1){%>
													style="Cursor:url('../image/cursors/H_POINT.CUR')"
													title="第<%=iPageBegins-1%>页"
													onclick="RunPage('<%=iPageBegins-1%>')"
												<%}
												else
												{%>
													style="Cursor:url('../image/cursors/H_NODROP.CUR')"
												<%}%>
												></td>
											<td width="24" valign="bottom"><img src="../image/bottom/Next.jpg" border="0" 
												<%if(iPages>iPageBegins){%>
													style="Cursor:url('../image/cursors/H_POINT.CUR')"
													title="第<%=iPageBegins+1%>页"
													onclick="RunPage('<%=iPageBegins+1%>')"
												<%}
												else
												{%>
													style="Cursor:url('../image/cursors/H_NODROP.CUR')"
												<%}%>
											></td>
											<td width="2">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						
						
						<%if(NodeTdTitleList.Count==0){%>
						<img src="../image/bottom/Previous.jpg" border="0" 
							<%if(iPageBegins>1){%>
								style="Cursor:url('../image/cursors/H_POINT.CUR')"
								title="第<%=iPageBegins-1%>/<%=iCount%>页"
								onclick="RunPage('<%=iPageBegins-1%>')"
							<%}
							else
							{%>
								style="Cursor:url('../image/cursors/H_NODROP.CUR')"
							<%}%>
						>
						<img src="../image/bottom/Next.jpg" border="0" 
							<%if(iPages>iPageBegins){%>
								style="Cursor:url('../image/cursors/H_POINT.CUR')"
								title="第<%=iPageBegins+1%>/<%=iCount%>页"
								onclick="RunPage('<%=iPageBegins+1%>')"
							<%}
							else
							{%>
								style="Cursor:url('../image/cursors/H_NODROP.CUR')"
							<%}%>
						>
						<%}%>
						
							<%//Response.Write(ShowDoc.OuterXml.Replace(((char)160).ToString(),"&nbsp;"));%>
							<%
							string strTemp=Request.ServerVariables["Query_String"];
							if(strTemp.IndexOf("&AssessorXpath")>-1)
								strTemp=strTemp.Remove(strTemp.IndexOf("&AssessorXpath"),strTemp.IndexOf("&",strTemp.IndexOf("&AssessorXpath")+1)-strTemp.IndexOf("&AssessorXpath"));
							%>
				
					</TD>
				<%if(DisplayManner=="2"){%>
				</TR><TR height="99%">
				<%}%>
					<TD vAlign="top" rowspan="2">
					</TD>
				</TR>
			</TABLE>
			<input id="hTableFields" type="hidden" name="hTableFields" value=""> 
			<input type="hidden" id="hQueryCollection" name="hQueryCollection" value="<%=hQueryCollection%>">
			<input type="hidden" id="hPageBegins" name="hPageBegins" value="1">
			<input type="hidden" id="hiCalculator" name="hiCalculator" value="<%=iCalculator%>">
			<input type="hidden" id="hKeyIndex" name="hKeyIndex" value="<%=strKeyIndex%>">
			<%if(xpathFirstRow!=""){%>
			<script language="javascript">
				try{
				parent.document.all["hParentXpath"].value='<%=xpathFirstRow.Replace("'","\\'")%>';
				}
				catch(e){}
				//alert(parent.document.all["hParentXpath"].value);
			</script>
			<%}%>
		</form>
		<%//=Request.Form["hTableFields"]%>
	</body>
</HTML>
