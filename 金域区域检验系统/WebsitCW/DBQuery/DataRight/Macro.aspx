<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.Macro" Codebehind="Macro.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Macro</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
				
		
			function GetSelection()
			{	
				for (i=0;i<document.all.length;i++)
				{				
				if (document.all[i].checked)
				{
					alert(document.getElementById("RadioButton1").checked);
					
				}
				}

				//var checkBoxList = document.getElementById("CheckBoxList1");
				//for(i=0;i<checkBoxList.items.length;i++)
				//{
				//	if(checkBoxList[i].checked==true)
				//	alert(checkBoxList[i].value)
				//}
				return document.getElementById("RadioButton1").value;
				
			}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div align="center">&nbsp;
			</div>
			<div align="center">
				<input type="button" value=" 确定 " onclick="window.returnValue=GetSelection();window.close();">
				<input type="button" value=" 取消 " onclick="window.close();window.returnValue='';">
				<DIV align="center">&nbsp;</DIV>
				<DIV align="center">&nbsp;</DIV>
			</div>
			<asp:CheckBoxList id="CheckBoxList1" style="Z-INDEX: 101; LEFT: 392px; POSITION: absolute; TOP: 232px"
				runat="server" Width="160px">
				<asp:ListItem Value="EmployeeID()">EmployeeID()</asp:ListItem>
				<asp:ListItem Value="EmployeeName()">EmployeeName()</asp:ListItem>
				<asp:ListItem Value="DepartmentID()">DepartmentID()</asp:ListItem>
				<asp:ListItem Value="DepartmentName()">DepartmentName()</asp:ListItem>
				<asp:ListItem Value="DutyID()">DutyID()</asp:ListItem>
				<asp:ListItem Value="DutyName()">DutyName()</asp:ListItem>
				<asp:ListItem Value="PostID()">PostID()</asp:ListItem>
				<asp:ListItem Value="PostName()">PostName()</asp:ListItem>
				<asp:ListItem Value="CurrentDate()">CurrentDate()</asp:ListItem>
				<asp:ListItem Value="CurrentTime()">CurrentTime()</asp:ListItem>
			</asp:CheckBoxList>
			<DIV align="center">
			<asp:RadioButton id="RadioButton1" runat="server" Text="EmployeeID()"></asp:RadioButton>
			<DIV align="center">
			<asp:RadioButton id="RadioButton2" runat="server" Text="EmployeeID()"></asp:RadioButton></DIV>
		</DIV>
		</form>
		
	</body>
</HTML>
