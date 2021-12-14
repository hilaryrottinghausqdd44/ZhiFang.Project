<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.PromptNewTable" Codebehind="PromptNewTable.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>新建/修改表</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			//定义两个全局变量，保存更改前的表名和编号
			var unchangeTCName;
			var unchangeTEName;
			
			function PageLoad()
			{
				unchangeTCName = TableCName.value;
				unchangeTEName = TableEName.value; 
				
			}
			function selectDB() {
			    var db = '<%=Request.QueryString["DBName"] %>';
			    var url = "../../DataInput/SelectTableNameFromDatabaseNameForm.aspx" +
                "?SelectDB=" + 'true' +
                "&dbName=" + db +
                "&tableName=" + '<%=Request.QueryString["TableEName"] %>';
			    //alert('<%=Request.ServerVariables["Query_string"] %>');
			    var r = window.showModalDialog(url, '_blank', 'width=800,height=700,resizable=yes,scrollbars=yes,titlebar=no');
			    if (r) {
			        TableEName.value = r;
			    }
			}
		</script>
		<script language="javascript" event="onclick" for="buttOK">
			if(	TableCName.value==""||
				TableEName.value==""||
				TableCName.value.indexOf(" ")>-1||
				TableEName.value.indexOf(" ")>-1||
				TableCName.value.indexOf(",")>-1||
				TableEName.value.indexOf(",")>-1)
			{
				alert("表名或编号不合法");
				return;
			}
			
			//window.returnValue=TableCName.value + "," + TableEName.value;
			var CNameChanged = true;
			var ENameChanged = true;
			if(TableCName.value == unchangeTCName)
			{
				CNameChanged = false;
			}
			
			if(TableEName.value == unchangeTEName)
			{
				ENameChanged = false;
			}
			//返回的是（中文字段名，英文名，中文名是否改变，英文名是否改变）
			window.returnValue = TableCName.value + "," + TableEName.value + "," + CNameChanged + "," + ENameChanged;
			window.close();
		</script>
	    <style type="text/css">
            #TableEName
            {
                width: 95px;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onload="PageLoad()">
		<TABLE id="table1" style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; POSITION: absolute; TOP: 8px; BACKGROUND-COLOR: white"
			cellSpacing="1" cellPadding="0" border="0">
			<TR style="BACKGROUND-COLOR: whitesmoke">
				<TD style="BACKGROUND-COLOR: gainsboro" noWrap align="left">表显示名&nbsp;
				</TD>
				<TH style="COLOR: dimgray" noWrap align="left">
					<INPUT id="TableCName" type="text" value="表1" runat="server"></TH></TR>
			<TR style="BACKGROUND-COLOR: whitesmoke">
				<TD style="BACKGROUND-COLOR: gainsboro" noWrap>表编号&nbsp;
				</TD>
				<TD noWrap align="left"><INPUT id="TableEName" type="text" value="table1" runat="server"> <input type="button" value="选择" onclick="selectDB()" /></TD>
			</TR>
			<TR style="BACKGROUND-COLOR: whitesmoke">
				<TD style="BACKGROUND-COLOR: gainsboro" noWrap colSpan="2" align="center"><INPUT type="button" value="确定" id="buttOK"></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
