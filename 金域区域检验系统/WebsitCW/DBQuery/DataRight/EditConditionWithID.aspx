<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.EditConditionWithID" Codebehind="EditConditionWithID.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EditConditionWithID</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/javascript">
		function CloseWindow()
		{
			window.close();
		}
		
		function ChoosePermissionPattern(obj)
			{
				var result;
				//var txtName = document.getElementById("txtName");
				var txtID = document.getElementById("txtID");
				//var txtPattern = document.getElementById("txtPattern");
				
				switch(obj.id)
				{
					case "btnDepartment":
						result = window.showModalDialog('../../RBAC/Roles/ChooseDepartment.aspx','','dialogWidth=30;dialogHeight=32;status=no;scroll=no');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	//PostModules.txtRoleTypeName.value='�����ŷ���';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								//txtName.value = rv[1];
								txtID.value = rv[0];
								//txtPattern.value = "����";//
								
								//window.parent.frames["mainContent"].location = "PermissionTreeView.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=department/"+txtName.value;
								return true;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					break;
						
					case "btnPost"://��λ
						result = window.showModalDialog('../../RBAC/Roles/ChoosePost.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	
								//txtName.value = rv[1];
								txtID.value = rv[0];
								//txtPattern.value = "��λ";
								
								//window.parent.frames["mainContent"].location = "PermissionTreeView.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=post/"+txtName.value;
								return true;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					break;
					
					case "btnJob"://ְλ
						result = window.showModalDialog('../../RBAC/Roles/ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	
								//txtName.value = rv[1];
								txtID.value = rv[0];
								//txtPattern.value = "ְλ";
								
								//window.parent.frames["mainContent"].location = "PermissionTreeView.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=duty/"+txtName.value;
								return true;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					break;
					
					case "btnEmployee":
						result = window.showModalDialog('../../RBAC/Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	
								//txtName.value = rv[1];
								txtID.value = rv[0];
								//txtPattern.value = "Ա��";
								
								//window.parent.frames["mainContent"].location = "PermissionTreeView.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=employee/"+txtName.value;
								return true;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					break;
				}
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div>
				<table>
					<tr>
						<th colspan="4" align="center">
							ѡ��ID</th>
					</tr>
					<tr>
						<td>
							<input id="btnDepartment" type="button" value="������" onclick="ChoosePermissionPattern(this);">
						</td>
						<td>
							<input id="btnPost" type="button" value="����λ" onclick="ChoosePermissionPattern(this);">
						</td>
						<td>
							<input id="btnJob" type="button" value="��ְλ" onclick="ChoosePermissionPattern(this);">
						</td>
						<td>
							<input id="btnEmployee" type="button" value="����Ա" onclick="ChoosePermissionPattern(this);">
						</td>
					</tr>
				</table>
				<br>
				ID��<asp:TextBox ID="txtID" Runat="server" contentEditable="false"></asp:TextBox>
			</div>
			<div align="center">
				<asp:Button ID="btnSave" Runat="server" Text="����" onclick="btnSave_Click"></asp:Button>
				&nbsp;&nbsp;&nbsp;&nbsp; <input type="button" value="ȡ��" onclick="CloseWindow()">
			</div>
		</form>
	</body>
</HTML>
