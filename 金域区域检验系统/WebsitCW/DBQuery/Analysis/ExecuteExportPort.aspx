<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.ExecuteExportPort" Codebehind="ExecuteExportPort.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ExecuteExportPort</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<iframe name="exportFrame" id="exportFrame" src="AnalysisImport.aspx?TemplateName=<%=templateName%>&Database=<%=database%>&ImportRule=<%=importRule%>&DatabaseEName=<%=dataBaseEName%>" width="100%" height="100%">
			</iframe>
		</form>
	</body>
</HTML>
