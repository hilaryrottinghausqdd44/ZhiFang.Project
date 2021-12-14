<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.AnalysisConfigLeft" Codebehind="AnalysisConfigLeft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AnalysisConfigLeft</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
			var lastTable = null;
			function LinkTableNameMenu(obj)
			{
				obj.style.backgroundColor = "skyblue";
				
				if(lastTable!=null && lastTable!=obj)
				{
					lastTable.style.backgroundColor = "white";
				}
				lastTable = obj;
				var str = obj.innerText;//中文表名
				//传过去中文表名
				window.parent.frames["AnalysisContent"].document.location.href = "AnalysisConfigMain.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + str;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tableName" style="BORDER-RIGHT: #0099cc 1px solid; BORDER-TOP: #0099cc 1px solid; BORDER-LEFT: #0099cc 1px solid; BORDER-BOTTOM: #0099cc 1px solid"
				runat="server">
			</TABLE>
		</form>
	</body>
</HTML>
