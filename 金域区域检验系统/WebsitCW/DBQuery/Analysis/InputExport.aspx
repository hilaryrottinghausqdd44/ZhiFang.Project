<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.InputExport" Codebehind="InputExport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>数据导出/导入</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function InitUI()
			{
				document.getElementById("ImportTable").style.display = "none";
				document.getElementById("ExportTable").style.display = "";
			}
			function ImportExport(obj)
			{
				if(obj.value == "import")//导入
				{
					document.getElementById("ImportTable").style.display = "";
					document.getElementById("ExportTable").style.display = "none";
				}
				else
				{
					document.getElementById("ImportTable").style.display = "none";
					document.getElementById("ExportTable").style.display = "";
				}
			}
			
			//事件处理
			function ExportClick()
			{
				if(document.getElementById("dropListTemplate").selectedIndex == "0")
				{
					alert("需要指定模板!");
					return;
				}
				
				//var radioObj = document.getElementById("radioExport");
				var radioObj = document.Form1.all["radioExport"];
				var selectValue;
				for(var i=1; i<=radioObj.length; i++)
				{
					if(radioObj[i].checked)
					{
						selectValue = radioObj[i].value;
						break;
					}
				}
				
				switch(selectValue)
				{
					case "SingleWithChild"://单条记录带子表
						window.open("AnalysisExport.aspx?<%=Request.ServerVariables["Query_String"]%>","_blank");
						window.close();
					break;
					
					case "AllWithChild": //导出全部记录
						var fileTemplatePath = document.getElementById("dropListTemplate").value;//模板文件路径名
						window.open("AnalysisExportAll.aspx?<%=Request.ServerVariables["Query_String"]%>&TemplateFilePath=" + fileTemplatePath, "_blank");
						window.close();
					break;
					
					case "ResultWithCondition"://导出查询结果
						var fileTemplatePath = document.getElementById("dropListTemplate").value;//模板文件路径名
						var queryCondition = window.opener.frames["ContentMain"].document.getElementById("hQueryCollection").value;
						queryCondition = queryCondition.replace(/\n/g, "∧");
						
						//alert(queryCondition);
						
						window.open("AnalysisExportAll.aspx?<%=Request.ServerVariables["Query_String"]%>&TemplateFilePath=" + fileTemplatePath + "&QueryCondition=" + queryCondition, "_blank");
						window.close();
					break;
					
				}
			}
			
			//=============点击导入按钮======
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
	<body onload="InitUI()" bottomMargin="5" leftMargin="5" topMargin="5" rightMargin="5">
		<form id="Form1" name="Form1" method="post" runat="server" enctype="multipart/form-data">
			<P><FONT face="宋体">
					<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="300" align="center" borderColorLight="darkgray"
						border="1" borderColor="#ff3333" borderColorDark="gainsboro">
						<TR>
							<TD align="center"><INPUT onclick="ImportExport(this)" type="radio" CHECKED value="export" name="radioInOut">导出</TD>
							<TD align="center"><INPUT onclick="ImportExport(this)" type="radio" value="import" name="radioInOut">导入</TD>
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
			<TABLE id="ExportTable" cellSpacing="0" cellPadding="0" width="300" align="center" borderColorLight="#a9a9a9"
				border="1" borderColorDark="#dcdcdc">
				<tr>
					<td width="100%" colspan="2" align="center">
						<fieldset id="fsetExportType" style="WIDTH: 208px; HEIGHT: 57px">
							<legend>
								<STRONG>导出类型</STRONG></legend>
							<table id="tableExportType" border="0" width="184" style="WIDTH: 184px; HEIGHT: 26px">
								<tr>
									<td align="center"><input type="radio" name="RadioExportType" checked>Excel
									</td>
									<td align="center"><input type="radio" name="RadioExportType" disabled>PDF
									</td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2"><asp:radiobuttonlist id="radioExport" Runat="server">
							<asp:ListItem Value="SingleWithChild" Selected="True">单条记录</asp:ListItem>
							<asp:ListItem Value="ResultWithCondition">查询结果</asp:ListItem>
							<%--<asp:ListItem Value="AllWithChild">全部</asp:ListItem>--%>
						</asp:radiobuttonlist></td>
				</tr>
				<tr>
					<td align="center"><input onclick="ExportClick()" type="button" value="导出" id="ExportBtnID">
					</td>
					<td align="center"><input type="button" value="取消" onclick="window.close();">
					</td>
				</tr>
			</TABLE>
			<input type="hidden" name="hiddImportRule" id="hiddImportRule"> <input type="hidden" name="IsTemplateEmpty" id="IsTemplateEmpty" value="<%=IsTemplateEmpty%>">
		</form>
		<script language="javascript">
			if(document.getElementById("IsTemplateEmpty").value == "Yes")
			{
				document.getElementById("ExportBtnID").disabled = "disabled";
				document.getElementById("ImportBtnID").disabled = "disabled";
			}
		</script>
	</body>
</HTML>
