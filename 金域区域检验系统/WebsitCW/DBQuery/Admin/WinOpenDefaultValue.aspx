<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.WinOpenDefaultValue" Codebehind="WinOpenDefaultValue.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WinOpenDefaultValue</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">BODY {
	TEXT-ALIGN: center
}
TD {
	TEXT-ALIGN: center
}
#divDefaultValue {
	WIDTH: 180px; HEIGHT: 250px; TEXT-ALIGN: center
}
#divButton {
	WIDTH: 200px; TEXT-ALIGN: center
}
.InputButton {
	BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; FONT-WEIGHT: 500; BORDER-LEFT: 1px solid; COLOR: #ffffff; BORDER-BOTTOM: 1px solid; BACKGROUND-COLOR: #0033ff
}
		</style>
		<script type="text/javascript">
			function ConfirmClick()
			{
				var selectObj = document.getElementById("listDefaultValue");
				if(selectObj.selectedIndex != -1)
				{
				window.parent.returnValue = selectObj.options[selectObj.selectedIndex].value;
				window.parent.close();
				}
				else
				{
					alert("还没选择！");
				}
			}
			
			function CancelClick()
			{
				window.parent.close();
			}
		</script>
		<script language="javascript" for="listDefaultValue" event="ondblclick">
			//alert(this.selectedIndex);
			return ConfirmClick();
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="Container">
				<div id="divDefaultValue"><asp:listbox id="listDefaultValue" Runat="server" Width="100%" Height="100%"></asp:listbox></div>
				<div id="divButton">
					<table width="100%">
						<tr>
							<td><input class="InputButton" id="btnConfirm" onclick="ConfirmClick()" type="button" value="确定">
							</td>
							<td><input type="button" id="btnCancel" value="取消" onclick="CancelClick()" class="InputButton"></td>
						</tr>
					</table>
				</div>
			</div>
		</form>
	</body>
</HTML>
