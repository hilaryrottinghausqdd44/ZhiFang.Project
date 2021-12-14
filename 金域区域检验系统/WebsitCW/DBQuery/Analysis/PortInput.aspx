<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.PortInput" Codebehind="PortInput.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PortInput</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
			function ImportClick()
			{
				
				if(document.getElementById("dropListTemplate").selectedIndex == "0")
				{
					alert("需要指定模板!");
					return;
				}
				
				var fileTemplatePath = document.getElementById("dropListTemplate").value;//模板文件名
				var ruleObj = document.getElementsByName("importRule");
				var selectedRule="";
				
				if(document.getElementById("importFile").value == "")
				{
					alert("还没有选择导入文件");
					return;
				}
				//==收集导入规则
				
				for(var i=0; i<ruleObj.length; i++)
				{
					if(ruleObj[i].checked)
					{
						selectedRule = ruleObj[i].value;
						break;
					}
					
				}
				//把导入规则放入隐藏域，在Form中提交
				document.getElementById("hiddImportRule").value = selectedRule;
				document.Form1.submit();
			}
		</script>
	</HEAD>
	<body bottomMargin="5" leftMargin="5" topMargin="5" rightMargin="5">
		<form id="Form1" name="Form1" method="post" runat="server" enctype="multipart/form-data">
			<P><FONT face="宋体">
					<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="300" align="center" borderColorLight="darkgray"
						border="1" borderColor="#ff3333" borderColorDark="gainsboro">
						<TR>
							<TD align="center" colspan="2">导入数据</TD>
						</TR>
						<TR>
							<TD align="center" colSpan="2">选择模板&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:DropDownList id="dropListTemplate" Runat="server"></asp:DropDownList></TD>
						</TR>
					</TABLE>
				</FONT>
			</P>
			<p></p>
			<TABLE id="ImportTable" cellSpacing="0" cellPadding="0" width="300" align="center" borderColorLight="darkgray"
				border="1" borderColorDark="gainsboro">
				<tr>
					<td align="center" colspan="2">
						<fieldset style="WIDTH: 235px; HEIGHT: 57px">
							<legend>
								<b>导入规则</b></legend>
							<table border="0" width="233" style="WIDTH: 233px; HEIGHT: 26px">
								<tr>
									<td align="center"><input type="radio" name="importRule" value="override" checked>覆盖</td>
									<td align="center"><input type="radio" name="importRule" value="ignore">跳过重复</td>
									<td align="center"><input type="radio" name="importRule" value="cancel">主键重复时放弃</td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2">选择导入文件：<BR>
						<input type="file" id="importFile" name="importFile">
					</td>
				</tr>
				<tr>
					<td align="center"><input type="button" value="导入" onclick="ImportClick()" id="ImportBtnID"></td>
					<td align="center"><input type="button" value="取消" onclick="window.close();"></td>
				</tr>
			</TABLE>
			<input type="hidden" name="hiddImportRule" id="hiddImportRule">
		</form>
	</body>
</HTML>
