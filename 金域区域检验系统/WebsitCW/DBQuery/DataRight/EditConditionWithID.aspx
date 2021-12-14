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
							{	//PostModules.txtRoleTypeName.value='按部门分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								//txtName.value = rv[1];
								txtID.value = rv[0];
								//txtPattern.value = "部门";//
								
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
						
					case "btnPost"://岗位
						result = window.showModalDialog('../../RBAC/Roles/ChoosePost.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	
								//txtName.value = rv[1];
								txtID.value = rv[0];
								//txtPattern.value = "岗位";
								
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
					
					case "btnJob"://职位
						result = window.showModalDialog('../../RBAC/Roles/ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	
								//txtName.value = rv[1];
								txtID.value = rv[0];
								//txtPattern.value = "职位";
								
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
								//txtPattern.value = "员工";
								
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
							选择ID</th>
					</tr>
					<tr>
						<td>
							<input id="btnDepartment" type="button" value="按部门" onclick="ChoosePermissionPattern(this);">
						</td>
						<td>
							<input id="btnPost" type="button" value="按岗位" onclick="ChoosePermissionPattern(this);">
						</td>
						<td>
							<input id="btnJob" type="button" value="按职位" onclick="ChoosePermissionPattern(this);">
						</td>
						<td>
							<input id="btnEmployee" type="button" value="按人员" onclick="ChoosePermissionPattern(this);">
						</td>
					</tr>
				</table>
				<br>
				ID：<asp:TextBox ID="txtID" Runat="server" contentEditable="false"></asp:TextBox>
			</div>
			<div align="center">
				<asp:Button ID="btnSave" Runat="server" Text="保存" onclick="btnSave_Click"></asp:Button>
				&nbsp;&nbsp;&nbsp;&nbsp; <input type="button" value="取消" onclick="CloseWindow()">
			</div>
		</form>
	</body>
</HTML>
