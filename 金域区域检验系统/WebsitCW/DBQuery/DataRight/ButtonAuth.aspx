<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.ButtonAuth" Codebehind="ButtonAuth.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ButtonAuth</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script type="text/javascript">
			function SaveClick()
			{
				var flags="";
				var chkObj;
				//for(var i=0; i<8; i++)
				for(var i=0; i<9; i++)  //������һ������
				{
					chkObj = document.getElementById("chk" + i);
					if(chkObj.checked == true)
					{
						flags += "1";
					}
					else
					{
						flags += "0";
					}
				}
				//alert(flags);
				document.getElementById("hiddenTag").value = flags;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div align="center">
				<table id="tableButton" runat="server">
					<tr>
						<td style="FONT-WEIGHT: bold; COLOR: #ffffff; BACKGROUND-COLOR: #000080" align="center"
							colSpan="4">���ܰ�ť����</td>
					</tr>
				</table>
				<div align="center">
					<asp:Button ID="btnSave" Runat="server" Text="����" style="BORDER-RIGHT: #666666 2px outset; BORDER-TOP: #666666 2px outset; BORDER-LEFT: #666666 2px outset; COLOR: white; BORDER-BOTTOM: #666666 2px outset; BACKGROUND-COLOR: #000080" onclick="btnSave_Click"></asp:Button>
					&nbsp; &nbsp; &nbsp;
					<asp:Button ID="btnCancel" Runat="server" Text="������ǰ��ɫȫ������" style="BORDER-RIGHT: #666666 2px outset; BORDER-TOP: #666666 2px outset; BORDER-LEFT: #666666 2px outset; COLOR: white; BORDER-BOTTOM: #666666 2px outset; BACKGROUND-COLOR: #000080" onclick="btnCancel_Click"></asp:Button>
				</div>
			</div>
			<input type="hidden" runat="server" id="hiddenTag">
		</form>
	</body>
</HTML>
