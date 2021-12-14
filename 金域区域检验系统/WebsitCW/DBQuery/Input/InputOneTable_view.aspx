<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.InputOneTable_view" Codebehind="InputOneTable_view.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<html>
	<HEAD>
		<title>选择表字段</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%if(cssFile.Trim()==""){%>
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<%}else{%>
		<LINK href="../<%=cssFile%>" type="text/css" rel="stylesheet">
		<%}%>

		<script>
		function window_onload()
		{
			CollectWhereClause(document.getElementById("userdesin").childNodes);
		}
				function CollectWhereClause(kids)
			{
				for(var i=0;i<kids.length;i++)
				{
					//alert('ok');
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
								kids[i].disabled=true;
					}
					
					//==================TextArea=====================
					if(kids[i].nodeName.toUpperCase() =='TEXTAREA')
					{
								kids[i].disabled=true;
					}
					if(kids[i].nodeName.toUpperCase()=='SELECT')
					{
						kids[i].disabled=true;
					}
					if(kids[i].nodeName.toUpperCase()=='A')
					{
						kids[i].disabled=true;
					}
					if(kids[i].hasChildNodes)
						CollectWhereClause(kids[i].childNodes);
				}
			}
		</script>
</head>
<body onload="window_onload()" topmargin=0 leftmargin=0 rightmargin=0 bottommargin=0>
<form id="Form1" name="Form1" method="post" target="frmDataRun" action="DataRun.aspx?<%=Request.ServerVariables["Query_String"]%>" onsubmit="CollectDataRun()">
	<div id="userdesin">
		<%=strContent%>
		
	</div>

	
	

				

			<input type="hidden" id="hQueryCollection" name="hQueryCollection" value="">
			<input type="hidden" id="hAction" name="hAction" value="Browse">
			<input type="hidden" id="txtBatches" name="txtBatches" value="">
			<input type="hidden" id="hSubTablesCopy" name="hSubTablesCopy" value="">
			<input type="hidden" id="hNotAllowNull" name="hNotAllowNull" value="">
			
		
			<br>
			<div class="biaoti" id="txtKeyIndexBatches" style="display:none"></div>
</form>
</body>
</HTML>
