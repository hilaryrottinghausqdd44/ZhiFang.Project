<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.PromptNewDictTable" Codebehind="PromptNewDictTable.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>[新建/修改]字典表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			
			window.returnValue=TableCName.value + "," + TableEName.value;
			window.close();
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
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
					<TD noWrap align="left"><INPUT id="TableEName" type="text" value="table1" runat="server"></TD>
				</TR>
				<TR style="BACKGROUND-COLOR: whitesmoke">
					<TD style="BACKGROUND-COLOR: gainsboro" noWrap align="center" colSpan="2"><INPUT id="buttOK" type="button" value="确定"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
