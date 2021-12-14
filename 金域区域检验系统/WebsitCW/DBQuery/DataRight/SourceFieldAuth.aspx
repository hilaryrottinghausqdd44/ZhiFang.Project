<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.SourceFieldAuth" Codebehind="SourceFieldAuth.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SourceFieldAuth</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
			.ButtonStyle { WIDTH: 200px; BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; BORDER-BOTTOM-STYLE: solid; border-size: 1px; backgroundColor: #000080 }
		</style>
		<script language="javascript">
			function ChoosePermissionPattern(obj)
			{
				var result;
				var txtName = document.getElementById("txtName");
				var txtID = document.getElementById("txtID");
				var txtPattern = document.getElementById("txtPattern");
				
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
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "����";//
								
								window.frames["buttonContent"].location = "FieldAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=department/"+txtName.value;
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
							{	//PostModules.txtRoleTypeName.value='����λ����';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "��λ";
								
								window.frames["buttonContent"].location = "FieldAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=post/"+txtName.value;
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
							{	//PostModules.txtRoleTypeName.value='��ְλ����';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "ְλ";
								
								window.frames["buttonContent"].location = "FieldAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=duty/"+txtName.value;
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
							{	//PostModules.txtRoleTypeName.value='��Ա������';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "Ա��";
								
								window.frames["buttonContent"].location = "FieldAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=employee/"+txtName.value;
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
			<table borderColor="#000080" height="100%" width="100%" align="center" border="2">
				<tr>
					<td vAlign="top" width="200">
						<table width="100%">
							<tr>
								<td style="FONT-WEIGHT: bold; COLOR: #ffffff; BACKGROUND-COLOR: #000080" align="center">��Ȩģʽ</td>
							</tr>
							<tr>
								<td align="center"><input id="btnDepartment" type="button" value="������" class="ButtonStyle" onclick="ChoosePermissionPattern(this)"></td>
							</tr>
							<tr>
								<td align="center"><input id="btnPost" type="button" value="����λ" class="ButtonStyle" onclick="ChoosePermissionPattern(this)"></td>
							</tr>
							<tr>
								<td align="center"><input id="btnJob" type="button" value="��ְλ" class="ButtonStyle" onclick="ChoosePermissionPattern(this)"></td>
							</tr>
							<tr>
								<td align="center"><input id="btnEmployee" type="button" value="��Ա��" class="ButtonStyle" onclick="ChoosePermissionPattern(this)"></td>
							</tr>
							<tr>
								<td>
									<table>
										<tr>
											<td nowrap>��</td>
											<td><asp:TextBox ID="txtPattern" Runat="server" contentEditable="false"></asp:TextBox></td>
										</tr>
										<tr>
											<td noWrap>����
											</td>
											<td><asp:textbox id="txtName" Runat="server" contentEditable="false"></asp:textbox></td>
										</tr>
										<tr>
											<td noWrap>ID</td>
											<td><asp:textbox id="txtID" Runat="server" contentEditable="false"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td vAlign="top">
						<table width="100%" height="100%">
							<tr>
								<td width="100%">
									<iframe width="100%" height="100%" name="buttonContent"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
