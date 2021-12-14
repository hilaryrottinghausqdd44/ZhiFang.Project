<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.SelectModalDialog" Codebehind="SelectModalDialog.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Ñ¡Ôñ¶Ô»°¿ò</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script language=javascript>
		var strStatus='<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>';
		window.status=strStatus;
    </script>
  </head>
  <body MS_POSITIONING="GridLayout" bottommargin=0 leftmargin=0 rightmargin=0 topmargin=0>
		<iframe id="frmBody" src="<%=Request.ServerVariables["Query_String"]%>" width="100%" height="100%" scrolling="auto"></iframe>
   </body>
</html>

