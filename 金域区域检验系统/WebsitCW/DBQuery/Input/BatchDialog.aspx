<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.BatchDialog" Codebehind="BatchDialog.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>批量数据选择</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
		<iframe id="frmBatch" src="BatchDataReselect.aspx?<%=Request.ServerVariables["Query_String"]%>" width="100%" height="100%" scrolling="auto"></iframe>
   </body>
</html>
