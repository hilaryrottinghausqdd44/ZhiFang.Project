<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.WinOpenSelectTable" Codebehind="WinOpenSelectTable.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WinOpenSelectTable</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<script id=clientEventHandlersJS language=javascript>
	<!--

	function Form1_onsubmit() {

	}

	var init=false;
	function checkThisTable(obj)
	{
		if(init)
			return false;
		inti=true;
		var objChecked=obj.checked;
		//loopCheck(obj.id,objChecked);
		
	}
	function loopCheck(strId,bChecked)
	{
		var TableRows=document.Form1.all["Table1"].rows;
		for(var i=1;i<=TableRows.length-1;i++)
		{
			if(strId==TableRows[i].cells[0].childNodes[0].id)
				continue;
			//alert(TableRows[i].cells[0].childNodes[0].tagName);
			if(TableRows[i].cells[0].childNodes[0].tagName=="INPUT")
			{
				if(bChecked)
				{
					if(strId.indexOf(TableRows[i].cells[0].childNodes[0].id)==0)
						TableRows[i].cells[0].childNodes[0].checked=true;
				}
				else
				{
					if(TableRows[i].cells[0].childNodes[0].id.indexOf(strId)==0)
						TableRows[i].cells[0].childNodes[0].checked=false;
				}
			}
		}
	}
	function returnValue()
	{
		var returnValue="";
		var TableRows=document.Form1.all["Table1"].rows;
		for(var i=1;i<=TableRows.length-1;i++)
		{
			if(TableRows[i].cells[0].childNodes[0].tagName=="INPUT")
			{
				if(TableRows[i].cells[0].childNodes[0].checked)
					returnValue +=";" +TableRows[i].cells[0].childNodes[0].title + ","+ TableRows[i].cells[0].childNodes[0].id;
			}
		}
		if(returnValue.length>0)
			parent.returnValue=returnValue.substr(1);
		window.close();
	}
	//-->
	</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" language=javascript onsubmit="return Form1_onsubmit()">
			<%=Request.QueryString["Name"]%>
			<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="300" bgColor="#3366cc" border="0">
				<TR style="COLOR: #ffffff">
					<TD nowrap>选择</TD>
					<TD width="150">表参考名称</TD>
					<TD>表英文名称</TD>
				</TR>
				<%
				string TableEName;
				foreach(XmlNode eachSource in nodeListSource)
				{
					TableEName=GetTableEName(eachSource);
				
				%>
				<TR style="BACKGROUND-COLOR: #ffffff;">
					<TD><input id="<%=TableEName%>" type="checkbox"  value="<%=TableEName%>"
					 <%if(TargetExit(eachSource)){%> checked disabled <%}%> onpropertychange="checkThisTable(this)" title="<%=eachSource.Attributes.GetNamedItem("TableCName").InnerXml%>"></TD>
					<TD><%=eachSource.Attributes.GetNamedItem("TableCName").InnerXml%></TD>
					<TD><%=eachSource.Attributes.GetNamedItem("EName").InnerXml%></TD>
				</TR>
				<%}%>
			</TABLE>
			<br>
			<INPUT type="button" value="确定" id=buttConfig onclick="returnValue()">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT type="button" value="取消" id=buttCancel onclick="javascript:parent.window.close()">
			<BR>
			<FONT size="2"><FONT size="3"><STRONG>注意：</STRONG></FONT><BR>
				如果没有您所需的表，请返回单表数据中创建</FONT><BR>
		</form>
	</body>
</HTML>
