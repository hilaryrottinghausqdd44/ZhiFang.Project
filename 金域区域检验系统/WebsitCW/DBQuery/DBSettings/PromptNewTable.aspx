<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.PromptNewTable" Codebehind="PromptNewTable.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�½�/�޸ı�</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			//��������ȫ�ֱ������������ǰ�ı����ͱ��
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
				alert("�������Ų��Ϸ�");
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
			//���ص��ǣ������ֶ�����Ӣ�������������Ƿ�ı䣬Ӣ�����Ƿ�ı䣩
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
				<TD style="BACKGROUND-COLOR: gainsboro" noWrap align="left">����ʾ��&nbsp;
				</TD>
				<TH style="COLOR: dimgray" noWrap align="left">
					<INPUT id="TableCName" type="text" value="��1" runat="server"></TH></TR>
			<TR style="BACKGROUND-COLOR: whitesmoke">
				<TD style="BACKGROUND-COLOR: gainsboro" noWrap>����&nbsp;
				</TD>
				<TD noWrap align="left"><INPUT id="TableEName" type="text" value="table1" runat="server"> <input type="button" value="ѡ��" onclick="selectDB()" /></TD>
			</TR>
			<TR style="BACKGROUND-COLOR: whitesmoke">
				<TD style="BACKGROUND-COLOR: gainsboro" noWrap colSpan="2" align="center"><INPUT type="button" value="ȷ��" id="buttOK"></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
