<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.ExcelPage.ImportSetting" Codebehind="ImportSetting.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ImportSetting</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">BODY {
	FONT-SIZE: 12px; MARGIN: 0px; TEXT-ALIGN: center
}
#Container {
	BORDER-RIGHT: red 1px solid; BORDER-TOP: red 1px solid; BORDER-LEFT: red 1px solid; WIDTH: 500px; BORDER-BOTTOM: red 1px solid; HEIGHT: 500px; TEXT-ALIGN: center
}
#header {
	MARGIN-TOP: 20px; MARGIN-BOTTOM: 20px
}
#selectTemplate {
	MARGIN-TOP: 40px
}
#divImportRule {
	MARGIN-TOP: 50px
}
#functionButton {
	MARGIN-TOP: 200px
}
		</style>
		
		<script type="text/javascript">
			var xmlHttp, xmlHttp2;
			var processId;
			var isExcute = false;
			
			function createXMLHttpRequest()
			{
				if(window.ActiveXObject)
				{
					xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
				}
				else if(window.XMLHttpRequest)
				{
					xmlHttp = new XMLHttpRequest();
				}
			}
			
			function StartImport()
			{
				createXMLHttpRequest();
				SettingDisable(true);
				SettingVisibility(true);
				
				//=====����ѡ����ļ����ƺ͵������====
				var dropObj = document.getElementById("dropListTemplate");
				fileName = dropObj.options[dropObj.selectedIndex].value; //����·��������
				
				var ImportRules = document.getElementsByName("importRule");
				var ImportRule;
				for(var i=0; i<ImportRules.length; i++)
				{
					if(ImportRules[i].checked)
					{
						ImportRule = ImportRules[i].value;
						break;
					}
				}
				//============End============
				var url = 'ImportAnsyExecute.aspx?<%=Request.ServerVariables["Query_String"]%>&FileName='+fileName + '&ImportRule='+ImportRule;
				xmlHttp.open("GET", url, true);
				xmlHttp.onreadystatechange = Callback;
				xmlHttp.send(null);
			}
			
			function SettingDisable(isDisable)
			{
				//parameters.length
				if(isDisable == true)
				{
					document.getElementById("functionButton").disabled = true;
				}
				else
					document.getElementById("functionButton").disabled = false;
			}
			
			function SettingVisibility(isVisible)
			{
				if(isVisible == true)
				{
					document.getElementById("ProgressBar").style.visibility = "visible";
				}
				else
					document.getElementById("ProgressBar").style.visibility = "hidden";
			}
			
			function Callback()
			{
				if(xmlHttp.readyState == 4)
				{
					if(xmlHttp.status == 200)
					{
						//document.getElementById("ProgressBar").innerText= "���";
						document.getElementById("ProgressBar").innerText= xmlHttp.responseText;
						setTimeout(SettingDisable, 1000);
						setTimeout(SettingVisibility, 1000);
						//SettingDisable(false);
						//SettingVisibility(false);
						
					}
				}
				/*
				if(xmlHttp.readyState == 1 && isExcute==false)
				{
					isExcute = true;
					processId = setInterval(pollServer, 2000);
					
				}
				*/
			}
			
			/*
			function pollServer()
			{
				//createXMLHttpRequest();
				if(window.ActiveXObject)
				{
					xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
				}
				else if(window.XMLHttpRequest)
				{
					xmlHttp = new XMLHttpRequest();
				}
				
				var url = "ImportAnsyExecute.aspx?action=poll";
				xmlHttp.open("GET", url, true);
				xmlHttp.onreadystatechange = PollCallback;
				xmlHttp.send(null);
			}
			
			function PollCallback()
			{
				if(xmlHttp.readyState == 4)
				{
					if(xmlHttp.status == 200)
					{
						var objBar = document.getElementById("ProgressBar");
						objBar.innerText = xmlHttp.responseText;
					}
				}
			}
			*/
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="Container">
				<div id="header"><span><b>��ӭʹ��Excel�ļ�������(2/2)</b></span>
					<br>
					<span>ִ��Excel����</span>
				</div>
				<div id="selectTemplate"><span>ѡ��ģ�壺<asp:dropdownlist id="dropListTemplate" Runat="server"></asp:dropdownlist></span>
				</div>
				<div id="divImportRule">
					<fieldset style="WIDTH: 235px; HEIGHT: 57px">
						<legend>
							<b>�������</b></legend>
						<table border="0" style="FONT-SIZE: 12px; WIDTH: 233px; HEIGHT: 26px">
							<tr>
								<td align="center" nowrap><input type="radio" name="importRule" value="override" checked>����</td>
								<td align="center" nowrap><input type="radio" name="importRule" value="ignore">�����ظ�</td>
								<td align="center" nowrap><input type="radio" name="importRule" value="cancel">�����ظ�ʱ����</td>
							</tr>
						</table>
					</fieldset>
				</div>
				<div id="ProgressBar" style="VISIBILITY: hidden; BACKGROUND-COLOR: #ffff99">���ڵ���...</div>
				<div id="functionButton"><input type="button" value="��ʼ����" onclick="StartImport();"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<input type="button" value="ȡ��">
				</div>
			</div>
		</form>
	</body>
</HTML>
