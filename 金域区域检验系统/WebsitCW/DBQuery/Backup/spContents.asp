<%@ Language=VBScript %>
<%Response.Expires = 0%>

<!--#Include file="../../share/conn.asp" -->


<html>

<head>
<META name=VI60_defaultClientScript content=VBScript>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>报表内容</title>
<SCRIPT ID=clientEventHandlersVBS LANGUAGE=vbscript>
<!--

Sub window_onload
	if document.getElementById("objSpreadSheet") is nothing then exit sub
	objSpreadSheet.Rows(7).DeleteRows 10
	objSpreadSheet.Range("A6:Z1542").ParseText replace(Sheet.value,";",vbcrlf),","
	objSpreadSheet.Columns.AutoFitColumns
	objSpreadSheet.Range("A6:Z1542").Borders.linestyle=1
	
End Sub

-->
</SCRIPT>
</head>

<body rightmargin=0 topmargin=0 leftmargin=0 bottommargin=0>

<% if Request.ServerVariables("HTTP_METHOD")="POST" THEN%>
	<P>
	<object id="objSpreadSheet" style="LEFT: 0px; WIDTH: 80%; TOP: 0px; HEIGHT: 90%" height="100%" width="100%" border="2" classid="clsid:0002E559-0000-0000-C000-000000000046" name="objSpreadSheet" viewastext>
  <param name="HTMLURL" value>
  <param name="HTMLData" value="&lt;html xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;
xmlns=&quot;http://www.w3.org/TR/REC-html40&quot;&gt;

&lt;head&gt;
&lt;style type=&quot;text/css&quot;&gt;
&lt;!--tr
	{mso-height-source:auto;}
td
	{white-space:nowrap;}
.wc49FF633
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wcC5C9233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:none;
	border-bottom:solid 1.5pt black;
	mso-protection:locked;}
.wc0DC9233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:solid 1.5pt black;
	mso-protection:locked;}
.wc44D9233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:solid 1.5pt black;
	mso-protection:locked;}
.wc009B233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc41CB233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:left;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:none;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc88CB233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:left;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wcCFCB233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:left;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wcCBDB233
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:left;
	vertical-align:middle;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc8A40053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wcC900053
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wcCE57053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc8728053
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:none;
	border-bottom:solid 1.5pt black;
	mso-protection:locked;}
.wcCE28053
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:solid 1.5pt black;
	mso-protection:locked;}
.wc0638053
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:16pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:center;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:solid 1.5pt black;
	mso-protection:locked;}
.wc0248053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wcC3B8053
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:left;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc0BB8053
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:left;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc42C8053
	{white-space:nowrap;
	font-family:黑体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:left;
	vertical-align:middle;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc4EC8053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc4AD8053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc46E8053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:solid .5pt black;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:none;
	mso-protection:locked;}
.wc42F8053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:none;
	mso-protection:locked;}
.wc8C06053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:none;
	mso-protection:locked;}
.wc0716053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc0C65053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc8275053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:solid .5pt black;
	border-left:none;
	border-right:none;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc0975053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:solid .5pt black;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc0E14053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:none;
	mso-protection:locked;}
.wc8824053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:general;
	vertical-align:top;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:none;
	mso-protection:locked;}
.wc0CC4053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:solid .5pt black;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:none;
	mso-protection:locked;}
.wc82D4053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wcCDD4053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:general;
	vertical-align:top;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc4BE4053
	{white-space:nowrap;
	font-family:&quot;Times New Roman&quot;;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc0705053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc0315053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:none;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wcC189053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:none;
	mso-protection:locked;}
.wc4899053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:none;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc09A9053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:auto;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:solid .5pt black;
	border-left:none;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc8FB9053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:top;
	border-top:solid .5pt black;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wc40D9053
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:12pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wcCDD9053
	{white-space:nowrap;
	font-family:&quot;Times New Roman&quot;;
	mso-number-format:General;
	font-size:10pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:bottom;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
.wcCCE9053
	{white-space:nowrap;
	font-family:&quot;Times New Roman&quot;;
	mso-number-format:General;
	font-size:8pt;
	font-weight:400;
	font-style:normal;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	color:black;
	text-align:center;
	vertical-align:bottom;
	border-top:none;
	border-left:solid .5pt black;
	border-right:solid .5pt black;
	border-bottom:solid .5pt black;
	mso-protection:locked;}
--&gt;
&lt;/style&gt;
&lt;/head&gt;

&lt;body&gt;
&lt;!--[if gte mso 9]&gt;&lt;xml&gt;
 &lt;x:ExcelWorkbook&gt;
  &lt;x:ExcelWorksheets&gt;
   &lt;x:ExcelWorksheet&gt;
    &lt;x:OWCVersion&gt;9.0.0.3821&lt;/x:OWCVersion&gt;
    &lt;x:Label Style='border-top:solid .5pt silver;border-left:solid .5pt silver;
     border-right:solid .5pt silver;border-bottom:solid .5pt silver'&gt;
     &lt;x:Caption&gt;Microsoft Office Spreadsheet&lt;/x:Caption&gt;
    &lt;/x:Label&gt;
    &lt;x:Name&gt;Sheet1&lt;/x:Name&gt;
    &lt;x:WorksheetOptions&gt;
     &lt;x:Selected/&gt;
     &lt;x:Height&gt;15452&lt;/x:Height&gt;
     &lt;x:Width&gt;25638&lt;/x:Width&gt;
     &lt;x:TopRowVisible&gt;0&lt;/x:TopRowVisible&gt;
     &lt;x:LeftColumnVisible&gt;6&lt;/x:LeftColumnVisible&gt;
     &lt;x:ProtectContents&gt;False&lt;/x:ProtectContents&gt;
     &lt;x:AllowSort/&gt;
     &lt;x:AllowFilter/&gt;
     &lt;x:DefaultRowHeight&gt;210&lt;/x:DefaultRowHeight&gt;
     &lt;x:StandardWidth&gt;2389&lt;/x:StandardWidth&gt;
    &lt;/x:WorksheetOptions&gt;
   &lt;/x:ExcelWorksheet&gt;
  &lt;/x:ExcelWorksheets&gt;
  &lt;x:MaxHeight&gt;80%&lt;/x:MaxHeight&gt;
  &lt;x:MaxWidth&gt;80%&lt;/x:MaxWidth&gt;
 &lt;/x:ExcelWorkbook&gt;
&lt;/xml&gt;&lt;![endif]--&gt;

&lt;table class=wc49FF633 x:str&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;37&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;37&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;37&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;37&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;38&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;38&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;38&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;58&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;44&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;44&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;38&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;44&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;44&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;57&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col class=wc49FF633 width=&quot;56&quot;&gt;
 &lt;col width=&quot;56&quot;&gt;
 &lt;tr height=&quot;57&quot; style='mso-height-source:userset;mso-height-alt:855'&gt;
  &lt;td class=wc8728053 colspan=&quot;26&quot; style='border-right:solid .5pt black'&gt;塔 里 木 油 田 油 气 开 发 动 态 日 报&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;21&quot; style='mso-height-source:userset;mso-height-alt:315'&gt;
  &lt;td class=wcC3B8053 colspan=&quot;14&quot;&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4AD8053&gt;&lt;/td&gt;
  &lt;td class=wc4AD8053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wcC189053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;15&quot; style='mso-height-alt:225'&gt;
  &lt;td class=wc42F8053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc42F8053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc8C06053&gt;自喷&lt;/td&gt;
  &lt;td class=wc8C06053&gt;机抽&lt;/td&gt;
  &lt;td class=wc8C06053&gt;油嘴&lt;/td&gt;
  &lt;td class=wc8C06053&gt;油压&lt;/td&gt;
  &lt;td class=wc8C06053&gt;套压&lt;/td&gt;
  &lt;td class=wc8C06053&gt;回压&lt;/td&gt;
  &lt;td class=wc8C06053&gt;井口&lt;/td&gt;
  &lt;td class=wc8C06053&gt;分离器&lt;/td&gt;
  &lt;td class=wc8C06053&gt;原油&lt;/td&gt;
  &lt;td class=wc4899053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc0716053&gt;日&lt;/td&gt;
  &lt;td class=wc0716053&gt;产&lt;/td&gt;
  &lt;td class=wc0716053&gt;量&lt;/td&gt;
  &lt;td class=wc09A9053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc8C06053&gt;气油比&lt;/td&gt;
  &lt;td class=wc8C06053&gt;含水&lt;/td&gt;
  &lt;td class=wc8C06053&gt;含沙&lt;/td&gt;
  &lt;td class=wc8C06053&gt;冲程&lt;/td&gt;
  &lt;td class=wc8C06053&gt;冲次&lt;/td&gt;
  &lt;td class=wc8C06053&gt;泵径&lt;/td&gt;
  &lt;td class=wc8C06053&gt;泵深&lt;/td&gt;
  &lt;td class=wc8C06053&gt;日运&lt;/td&gt;
  &lt;td class=wc8C06053&gt;日存&lt;/td&gt;
  &lt;td class=wc42F8053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;18&quot; style='mso-height-alt:270'&gt;
  &lt;td class=wc0E14053&gt;油田名&lt;/td&gt;
  &lt;td class=wc0E14053&gt;井 名&lt;/td&gt;
  &lt;td class=wc0E14053&gt;时间&lt;/td&gt;
  &lt;td class=wc0E14053&gt;时间&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc0E14053&gt;温度&lt;/td&gt;
  &lt;td class=wc0E14053&gt;压力&lt;/td&gt;
  &lt;td class=wc0E14053&gt;密度&lt;/td&gt;
  &lt;td class=wc8FB9053&gt;总 液&lt;/td&gt;
  &lt;td class=wc8FB9053&gt;油&lt;/td&gt;
  &lt;td class=wc8FB9053&gt;油&lt;/td&gt;
  &lt;td class=wc8FB9053&gt;气&lt;/td&gt;
  &lt;td class=wc8FB9053&gt;水&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc0E14053&gt;率&lt;/td&gt;
  &lt;td class=wc0E14053&gt;量&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc0E14053&gt;出量&lt;/td&gt;
  &lt;td class=wc0E14053&gt;油量&lt;/td&gt;
  &lt;td class=wc0E14053&gt;备 注&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;18&quot; style='mso-height-alt:270'&gt;
  &lt;td class=wc82D4053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc40D9053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(h)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(h)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(mm)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(MPa)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(MPa)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(MPa)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(℃)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(MPa)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(g/cm)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m3)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m3)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(T)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m3)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m3)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m3/m3)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(%)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(%)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m)&lt;/td&gt;
  &lt;td class=wcCCE9053&gt;(n/min)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(mm)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m3)&lt;/td&gt;
  &lt;td class=wcCDD9053&gt;(m3)&lt;/td&gt;
  &lt;td class=wc40D9053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;14&quot;&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td class=wc8A40053&gt;&lt;/td&gt;
  &lt;td&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;57&quot; style='mso-height-source:userset;mso-height-alt:855'&gt;
  &lt;td class=wc8728053 colspan=&quot;23&quot; style='border-right:solid .5pt black'&gt;西 北 石
  油 局 油 气 开 发 动 态 月 报&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;21&quot; style='mso-height-source:userset;mso-height-alt:315'&gt;
  &lt;td class=wcC3B8053 colspan=&quot;14&quot;&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4EC8053&gt;&lt;/td&gt;
  &lt;td class=wc4AD8053&gt;&lt;/td&gt;
  &lt;td class=wc46E8053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;15&quot; style='mso-height-alt:225'&gt;
  &lt;td class=wc42F8053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc42F8053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc8C06053&gt;自喷&lt;/td&gt;
  &lt;td class=wc8C06053&gt;机抽&lt;/td&gt;
  &lt;td class=wc8C06053&gt;油咀&lt;/td&gt;
  &lt;td class=wc8C06053&gt;油压&lt;/td&gt;
  &lt;td class=wc8C06053&gt;套压&lt;/td&gt;
  &lt;td class=wc8C06053&gt;回压&lt;/td&gt;
  &lt;td class=wc0C65053 colspan=&quot;8&quot; style='border-right:solid .5pt black'&gt;油 产 量&lt;/td&gt;
  &lt;td class=wc0C65053 colspan=&quot;3&quot; style='border-right:solid .5pt black'&gt;气 产 量&lt;/td&gt;
  &lt;td class=wc0C65053 colspan=&quot;2&quot; style='border-right:solid .5pt black'&gt;水 产 量&lt;/td&gt;
  &lt;td class=wc42F8053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wc8C06053&gt;原油&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;18&quot; style='mso-height-alt:270'&gt;
  &lt;td class=wc0E14053&gt;油 气 田&lt;/td&gt;
  &lt;td class=wc0E14053&gt;油气井&lt;/td&gt;
  &lt;td class=wc0E14053&gt;时间&lt;/td&gt;
  &lt;td class=wc0E14053&gt;时间&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8824053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc8C06053&gt;年 计 划&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;年累计&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;年累计&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;为 年&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;月累计&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;月累计&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;日均产油&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;日均产油&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;年计划产&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;年累计&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;月累计&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;年累计&lt;/td&gt;
  &lt;td class=wc0CC4053&gt;月累计&lt;/td&gt;
  &lt;td class=wc0E14053&gt;气油比&lt;/td&gt;
  &lt;td class=wc0E14053&gt;含水&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;18&quot; style='mso-height-alt:270'&gt;
  &lt;td class=wc82D4053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;　 &lt;/td&gt;
  &lt;td class=wcCDD4053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc4BE4053&gt;(天)&lt;/td&gt;
  &lt;td class=wc4BE4053&gt;(天)&lt;/td&gt;
  &lt;td class=wc4BE4053&gt;(mm)&lt;/td&gt;
  &lt;td class=wc4BE4053&gt;(Mpa)&lt;/td&gt;
  &lt;td class=wc4BE4053&gt;(Mpa)&lt;/td&gt;
  &lt;td class=wc4BE4053&gt;(Mpa)&lt;/td&gt;
  &lt;td class=wc0705053&gt;产油(吨)&lt;/td&gt;
  &lt;td class=wc0315053&gt;方&lt;/td&gt;
  &lt;td class=wc0315053&gt;吨&lt;/td&gt;
  &lt;td class=wc0315053&gt;计划%&lt;/td&gt;
  &lt;td class=wc0315053&gt;方&lt;/td&gt;
  &lt;td class=wc0315053&gt;吨&lt;/td&gt;
  &lt;td class=wc0315053&gt;吨&lt;/td&gt;
  &lt;td class=wc0315053&gt;比上月增&lt;/td&gt;
  &lt;td class=wc0315053&gt;万方&lt;/td&gt;
  &lt;td class=wc0315053&gt;万方&lt;/td&gt;
  &lt;td class=wc0315053&gt;万方&lt;/td&gt;
  &lt;td class=wc0315053&gt;方&lt;/td&gt;
  &lt;td class=wc0315053&gt;方&lt;/td&gt;
  &lt;td class=wcCDD4053&gt;&lt;span style=&quot;mso-spacerun: yes&quot;&gt;&nbsp;&lt;/span&gt;&lt;/td&gt;
  &lt;td class=wc4BE4053&gt;%&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
  &lt;td class=wc0248053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;32&quot; style='mso-height-source:userset;mso-height-alt:480'&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcC900053&gt;&lt;/td&gt;
  &lt;td class=wcCE57053&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr style='mso-xlrowspan:1'&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;57&quot; style='mso-height-source:userset;mso-height-alt:855'&gt;
  &lt;td class=wcC5C9233 x:str=&quot;西北石油局油气开发动态半年报&quot; colspan=&quot;9&quot; style='border-right:
  solid .5pt black'&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td class=wc009B233&gt;&lt;/td&gt;
  &lt;td&gt;&lt;/td&gt;
 &lt;/tr&gt;
 &lt;tr height=&quot;21&quot; style='mso-height-source:userset;mso-height-alt:315'&gt;
  &lt;td class=wc41CB233 colspan=&quot;9&quot; style='border-right:solid .5pt black'&gt;&lt;/td&gt;
  &lt;td class=wcCBDB233&gt;&lt;/td&gt;
  &lt;td class=wcCBDB233&gt;&lt;/td&gt;
  &lt;td class=wcCBDB233&gt;&lt;/td&gt;
  &lt;td class=wcCBDB233&gt;&lt;/td&gt;
  &lt;td class=wcCBDB233&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td class=wc49FF633&gt;&lt;/td&gt;
  &lt;td&gt;&lt;/td&gt;
 &lt;/tr&gt;
&lt;/table&gt;

&lt;/body&gt;

&lt;/html&gt;
">
  <param name="DataType" value="HTMLDATA">
  <param name="AutoFit" value="0">
  <param name="DisplayColHeaders" value="-1">
  <param name="DisplayGridlines" value="0">
  <param name="DisplayHorizontalScrollBar" value="-1">
  <param name="DisplayRowHeaders" value="-1">
  <param name="DisplayTitleBar" value="-1">
  <param name="DisplayToolbar" value="-1">
  <param name="DisplayVerticalScrollBar" value="-1">
  <param name="EnableAutoCalculate" value="-1">
  <param name="EnableEvents" value="-1">
  <param name="MoveAfterReturn" value="-1">
  <param name="MoveAfterReturnDirection" value="0">
  <param name="RightToLeft" value="0">
  <param name="ViewableRange" value="1:65536">
</object>
</P>
	<%  if Request.ServerVariables("HTTP_METHOD")="POST" THEN
		sql=Request.Form("hiddenValue")
		'if len(year(cdate(Request.Form("timeControl"))))=2 then
			strYearsBegins=Request.Form("timeControl1")
			strYearsBegings=CSTR(FormatDateTime(cdate(strYearsBegins),2))
			
			strYearsEnds=Request.Form("timeControl2")
			strYearsEnds=CSTR(FormatDateTime(cdate(strYearsEnds),2))
			
			nDays=cdate(strYearsBegings)-cdate(strYearsEnds)
			
			
			Response.Write typename(nDays)
			Response.Write nDays
			
		select case Request.Form("reportType")
			case "1"
				sql=sql + " and RQ>=To_date('" + strYearsBegings + "','YYYY-MM-DD') and RQ<=To_date('" + strYearsEnds + "','YYYY-MM-DD') order by rq"
				'sql=sql + " and RQ<=To_date('" + strYearsBegings + "','YYYY-MM-DD')"
			case "2"
				
			case "3"
			
			case else
		end select
		
		Response.Write sql
		'on error resume next
		set rs=cn.execute(sql)
		Response.Write rs.recordcount
		'Response.Write rs(0)
		'esponse.Write err.description
		'Response.End
	end if
	
		Function iifNullRecord(byval nRecord)
			if isNull(nRecord) then
				iifNullRecord=""
			else
				iifNullRecord=cstr(nRecord)
			end if
		End Function
%>

	<%
		strValues=""
		do while not rs.eof
			strRow=""
			for i=1 to rs.fields.count-1
				strRow=strRow + "," + iifNullRecord(rs(i))
			next
			rs.movenext
			strRow=replace(strRow,",","",1,1)
			
			strValues=strValues + ";" + strRow
			
		loop
		strValues=replace(strValues,";","",1,1)
		
	%>
	<input type=hidden id="Sheet" name="Sheet" value="<%=strValues%>">
<%else%>
	这里显示的报表内容
<%end if%>

</body>

</html>
