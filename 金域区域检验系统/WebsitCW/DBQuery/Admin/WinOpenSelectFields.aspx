<%@ Import Namespace="System.Xml" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.WinOpenSelectFields" Codebehind="WinOpenSelectFields.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ����ֶ�</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script id="clientEventHandlersJS" language="javascript">
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
		loopCheck(obj.id,objChecked);
		
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
				if(TableRows[i].cells[0].childNodes[0].checked&&TableRows[i].cells[0].childNodes[0].disabled==false)
					returnValue +=";" +TableRows[i].cells[0].childNodes[0].id;
			}
		}
		if(returnValue.length>0)
			parent.returnValue=returnValue.substr(1);
		window.close();
	}
	//-->
	
	//===================ȫѡ��ť���===============
	function SelectAll()
	{
		var rowCollection = document.getElementById("Table1").rows;
		var chkObj;
		for(var i=0, j=rowCollection.length; i<j; i++)
		{
			chkObj = rowCollection[i].cells[0].childNodes[0];
			if(chkObj.nodeType == "1")
			{
				if(chkObj.checked == false)
					chkObj.checked = true;
			}
		}
	}
	//======================End=====================
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" language="javascript" onsubmit="return Form1_onsubmit()">
			ϵͳ��<%=Request.QueryString["Name"]%> ������<%=Request.QueryString["TableName"]%>
			<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="300" bgColor="#3366cc" border="0">
				<TR style="COLOR: #ffffff">
					<TD nowrap>ѡ��</TD>
					<TD width="150">�ֶβο�����</TD>
					<TD>�ֶ�Ӣ������</TD>
				</TR>
				<%
				string ColumnEName;
				foreach(XmlNode eachSource in nodeListSource)
				{
					ColumnEName=eachSource.Attributes.GetNamedItem("ColumnEName").InnerXml;
				
				%>
				<TR style="BACKGROUND-COLOR: #ffffff;">
					<TD><input id="<%=ColumnEName%>" type="checkbox"  value="<%=ColumnEName%>"
					 <%if(TargetExit(eachSource)){%> checked disabled <%}%> onpropertychange="checkThisTable(this)" title="<%=eachSource.Attributes.GetNamedItem("ColumnCName").InnerXml%>"></TD>
					<TD nowrap><%=eachSource.Attributes.GetNamedItem("ColumnCName").InnerXml%></TD>
					<TD nowrap><%=eachSource.Attributes.GetNamedItem("ColumnEName").InnerXml%></TD>
				</TR>
				<%}%>
			</TABLE><br>
			<input type="button" value="ȫѡ" id="btnSelectAll" onclick="SelectAll()">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT type="button" value="ȷ��" id="buttConfig" onclick="returnValue()">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT type="button" value="ȡ��" id="buttCancel" onclick="javascript:parent.window.close()"><BR>
			<BR>
			<FONT size="2"><FONT size="3"><STRONG>ע�⣺</STRONG></FONT><BR>
				���û����������ֶΣ��뷵�ص��������д���</FONT><BR>
		</form>
	</body>
</HTML>
