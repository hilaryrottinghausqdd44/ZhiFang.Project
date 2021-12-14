<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.ExcelPage.ImportPort" Codebehind="ImportPort.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ImportPort</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">BODY { FONT-SIZE: 12px; MARGIN: 0px; TEXT-ALIGN: center }
	#Container { BORDER-RIGHT: red 1px solid; BORDER-TOP: red 1px solid; BORDER-LEFT: red 1px solid; WIDTH: 500px; BORDER-BOTTOM: red 1px solid; HEIGHT: 500px; TEXT-ALIGN: center }
	#header { MARGIN-TOP: 20px; MARGIN-BOTTOM: 20px }
	#fileUpload { MARGIN-TOP: 50px }
	#functionButton { MARGIN-TOP: 300px }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" enctype="multipart/form-data">
			<div id="Container">
				<div id="header"><span><b>欢迎使用Excel文件导入向导(1/3)</b></span>
					<br>
					<span>Excel文件上传</span>
				</div>
				<div id="fileUpload"><input id="importFile" type="file" name="importFile">
				</div>
				<div id="waiter" style="VISIBILITY: hidden; BACKGROUND-COLOR: #ffffcc">
					<span>文件导入中...请等待..</span>
				</div>
				<div id="functionButton"><asp:button id="btnNextStep" Runat="server" Text="下一步" onclick="btnNextStep_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<input type="button" value="取消">
				</div>
			</div>
			<script language='javascript'>
				//var obj = document.getElementById('waiter');
				//obj.style.visibility = 'visible';
			</script>
		</form>
	</body>
</HTML>
