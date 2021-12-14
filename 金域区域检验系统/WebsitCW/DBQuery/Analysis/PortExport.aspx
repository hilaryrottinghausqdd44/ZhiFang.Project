<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.PortExport" CodeBehind="PortExport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PortExport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
			function ExportClick()
			{
				if(document.getElementById("dropListTemplate").selectedIndex == "0")
				{
					alert("��Ҫָ��ģ��!");
					return;
				}

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
				var hiddenSelectValue = document.getElementById("hiddenSelectValue");
				hiddenSelectValue.value = selectValue;

				
				
				
				var ModalClass = "";
				var RadioExportType = document.getElementsByName("RadioExportType");
				var ModalFlag = false;
				for(var i=0;i<RadioExportType.length;i++)
				{
				    if(RadioExportType[i].checked)
				    {
				        ModalFlag = true;
				        ModalClass = RadioExportType[i].id;
				    }
				}
				if(!ModalFlag)
				{
				    alert("��ѡ��ģ�����ͣ�");
				    return;
				}
				var fileTemplatePath = document.getElementById("dropListTemplate").value;//ģ���ļ�·����
				var hiddenFileExtensionName = document.getElementById("hiddenFileExtensionName");
				if(ModalClass.toUpperCase() == "CSV")
				{
				    fileTemplatePath = fileTemplatePath+".csv";
				    hiddenFileExtensionName.value=".csv";
				}
				else
				{
				    fileTemplatePath = fileTemplatePath+".xml";
				    hiddenFileExtensionName.value=".xml";
				}
				
				
				var url = "";
				var paraQueryCondition = "";
				var paraXPathCondition = "";
				switch(selectValue)
				{
				    case "SingleWithChild"://������¼���ӱ�
				        url="AnalysisExport.aspx?<%=Request.ServerVariables["Query_String"]%>&TemplateFilePath=" + fileTemplatePath+"&ModalClass="+ModalClass;
				    break;
				    case "AllWithChild": //����ȫ����¼
				        url="AnalysisExportAll.aspx?ModalClass="+ModalClass+"&<%=Request.ServerVariables["Query_String"]%>&TemplateFilePath=" + fileTemplatePath;
				    break;
				    case "ResultWithCondition"://������ѯ���
						//var fileTemplatePath = document.getElementById("dropListTemplate").value;//ģ���ļ�·����
						var queryCondition = window.opener.frames["ContentMain"].document.getElementById("hQueryCollection").value;
						queryCondition = queryCondition.replace(/\n/g, "��");
						
						var XPathCondition = window.opener.frames["ContentMain"].document.getElementById("hiddenXPath").value;
						paraQueryCondition = queryCondition;
						paraXPathCondition = XPathCondition
						url = "AnalysisExportAll.aspx?ModalClass="+ModalClass+"&<%=Request.ServerVariables["Query_String"]%>&TemplateFilePath=" + fileTemplatePath + "&QueryCondition=" + queryCondition + "&XPathCondition=" + XPathCondition;
				    break;
				}
				
				var hiddenParaQueryCondition = document.getElementById("hiddenParaQueryCondition");
				hiddenParaQueryCondition.value = paraQueryCondition;
				
				var hiddenParaXPathCondition = document.getElementById("hiddenParaXPathCondition");
				hiddenParaXPathCondition.value = paraXPathCondition;
				
				if(ModalClass.toUpperCase() == "CSV")
				{
				     return true;
				}
				else
				{
				    window.open(url, "_blank");
				}
				return false;
			}
		</script>
	</HEAD>
	<body bottomMargin="5" leftMargin="5" topMargin="5" rightMargin="5">
		<form id="Form1" name="Form1" method="post" runat="server">
			<P><FONT face="����">
					<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="300" align="center" borderColorLight="darkgray"
						border="1" borderColor="#ff3333" borderColorDark="gainsboro">
						<TR>
							<td colspan="2" align="center">��������</td>
						</TR>
						<TR>
							<TD align="center" colSpan="2">ѡ��ģ��&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:DropDownList id="dropListTemplate" Runat="server"></asp:DropDownList>
							</TD>
						</TR>
					</TABLE>
				</FONT>
			</P>
			<TABLE id="ExportTable" cellSpacing="0" cellPadding="0" width="300" align="center" borderColorLight="#a9a9a9"
				border="1" borderColorDark="#dcdcdc">
				<tr>
					<td width="100%" colspan="2" align="center">
						<fieldset id="fsetExportType" style="WIDTH: 208px; HEIGHT: 57px">
							<legend>
								<STRONG>��������</STRONG></legend>
							<table id="tableExportType" runat="server" border="0" width="184" style="WIDTH: 184px; HEIGHT: 26px">
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2"><asp:radiobuttonlist id="radioExport" Runat="server">
							<asp:ListItem Value="SingleWithChild">������¼</asp:ListItem>
							<asp:ListItem Value="ResultWithCondition" Selected="True">��ѯ���</asp:ListItem>
						</asp:radiobuttonlist></td>
				</tr>
				<tr>
					<td align="center">
					<asp:Button ID="ExportBtnID" Text="����" runat="server" onclick="ExportBtnID_Click" />
					<!--input onclick="ExportClick()" type="button" value="����" id="_ExportBtnID"-->
					</td>
					<td align="center"><input type="button" value="ȡ��" onclick="window.close();">
					</td>
				</tr>
			</TABLE>
			<p style="color:red">���Ҫʹ�����ݵ������ܣ���<a href="../../Includes/Office/Default.htm" target="_blank">������ؿؼ�</a></p>
			<input type="hidden" name="hiddImportRule" id="hiddImportRule"> <input type="hidden" name="IsTemplateEmpty" id="IsTemplateEmpty" value="<%=IsTemplateEmpty%>">
		
		
		<input type="hidden" id="hiddenModalClass" runat="server" />
		<input type="hidden" id="hiddenSelectValue" runat="server" />
		<input type="hidden" id="hiddenParaQueryCondition" runat="server" />
		<input type="hidden" id="hiddenParaXPathCondition" runat="server" />
		<input type="hidden" id="hiddenFileExtensionName" runat="server" />
		</form>
	</body>
</HTML>
