<%@ Import Namespace="System.Xml" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.SelectSubTablesForCopy" Codebehind="SelectSubTablesForCopy.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SelectSubTablesForCopy</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../css/ioffice.css">
	<script language="javascript">
	var r = "";
		
	function returnConfirm()
	{
		r="";
		var obj = document.all["Table1"].childNodes;
		
		CollectCheckBox(obj);
		
		if(r.length>0)
		{
			if(r.charAt(r.length-1) == ",")
			{
				r = r.substring(0, r.length-1);
			}
		}
		window.parent.returnValue = r;
		window.parent.close();
	}
	
	function CollectCheckBox(childNodes)
	{
		for(var i=0; i<childNodes.length; i++)
		{
			if(childNodes[i].nodeName.toUpperCase()=="INPUT"&&childNodes[i].type.toUpperCase()=="CHECKBOX"&&childNodes[i].checked)
				r += childNodes[i].value + ","; 
				
			if(childNodes[i].childNodes.length>0)
				CollectCheckBox(childNodes[i].childNodes);
		}
	}
	</script>
	</HEAD>
	
	<body MS_POSITIONING="GridLayout" bottomMargin="5" leftMargin="5" topMargin="5" rightMargin="5">
		
		是否复制子表？
		<%//=Request.ServerVariables["Query_String"]%>
		<INPUT style="Z-INDEX: 101" type="button" value="确定" id="buttConfirm" onclick="returnConfirm()">
		<br><br>
		<TABLE id="Table1" cellSpacing="1"
			cellPadding="0" width="300" border="0" bgcolor="#6699ff">
			<%
			if(xmlNodeSubTables!=null)
			{
				foreach(XmlNode eachSubTable in xmlNodeSubTables)
				{
				%>
				<TR bgcolor="#ffffff">
					<TD><INPUT type="checkbox" checked value="<%=eachSubTable.Attributes.GetNamedItem("EName").Value%>"></TD>
					<TD title="<%=RetrieveTableName(eachSubTable)%>"><%=RetrieveTableCName(RetrieveTableName(eachSubTable))%></TD>
				</TR>
				<%	}
			}
			else
			{
			%>
			<script language=javascript>window.parent.close();</script> 
			<%}%>
			
		</TABLE>
	</body>
</HTML>
