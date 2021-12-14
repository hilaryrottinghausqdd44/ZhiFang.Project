<%@ Import Namespace="System.Xml" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.SelectAllTableFields" Codebehind="SelectAllTableFields.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>SelectAllTableFields</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK rel="stylesheet" type="text/css" href="../css/ioffice.css">
  </head>
  <body MS_POSITIONING="GridLayout">
	<table border="0" cellspacing="1" bgcolor="skyblue">
	<%
		if(AllTables!=null)
		{
			foreach(XmlNode myNode in AllTables)
			{
			string TableCName= RetrieveTableName(myNode);
				%>
				<tr bgcolor="white" height="40">
					<td nowrap><b><%=myNode.Attributes.GetNamedItem("TableCName").Value%></b></td>
					<td nowrap><%=myNode.ChildNodes[0].ChildNodes.Count%></td>
					<td><%
					XmlNodeList myKids=myNode.SelectNodes("tr/td");
					foreach(XmlNode myKid in myKids)
					{
						if(Request.QueryString["myFlag"].ToString().ToUpper()=="TRUE"
							||myKid.Attributes.GetNamedItem("ColumnType")!=null
							&&myKid.Attributes.GetNamedItem("KeyIndex").Value!="Yes"
							&&myKid.Attributes.GetNamedItem("ColumnType").Value=="1")
						{
						%><a href="#" onclick="parent.returnValue='{<%=TableCName%>}.[<%=myKid.Attributes.GetNamedItem("ColumnCName").Value%>]';parent.close();"><b><%=myKid.Attributes.GetNamedItem("ColumnCName").Value%></b></a>&nbsp;&nbsp;&nbsp;<%
						}
						else
						{
						%><i><%=myKid.Attributes.GetNamedItem("ColumnCName").Value%></i>&nbsp;&nbsp;&nbsp;<%
					}}
					%></td>
				</tr>
				<%
			}
		}
	%>
	</table>
	
  </body>
</html>
