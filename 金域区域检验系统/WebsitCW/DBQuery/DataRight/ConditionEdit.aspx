<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.ConditionEdit" Codebehind="ConditionEdit.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ConditionEdit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">TH { FONT-WEIGHT: 600; FONT-SIZE: 15px }
	TD { FONT-SIZE: 12px }
	.InputButton { BORDER-RIGHT: #666666 2px outset; BORDER-TOP: #666666 2px outset; BORDER-LEFT: #666666 2px outset; COLOR: white; BORDER-BOTTOM: #666666 2px outset; BACKGROUND-COLOR: #000080 }
	.tableWid input 
	{
		overflow-x:visible;
		width:389px;
	}
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
							{	//PostModules.txtRoleTypeName.value='按部门分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "部门";//
								
								//window.parent.frames["mainContent"].location = "PermissionTreeView.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=department/"+txtName.value;
								window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=department/"+txtName.value;
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
							{	//PostModules.txtRoleTypeName.value='按岗位分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "岗位";
								
								window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=post/"+txtName.value;
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
							{	//PostModules.txtRoleTypeName.value='按职位分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "职位";
								
								window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=duty/"+txtName.value;
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
							{	//PostModules.txtRoleTypeName.value='按员工分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "员工";
								
								window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=employee/"+txtName.value;
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
			
			function ChoosePermissionPattern()
			{
				var result;
				var selVal = document.getElementById("dropListSort").value;
				var txtID = document.getElementById("txtID");
				var txtPattern = document.getElementById("txtPattern");
				//alert('a');
				switch(selVal)
				{
					case "department":
						result = window.showModalDialog('../../RBAC/Roles/ChooseDepartment.aspx','','dialogWidth=30;dialogHeight=32;status=no;scroll=no');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
						
							var rv = result.split("|");
							if (rv.length == 2)
							{	//PostModules.txtRoleTypeName.value='按部门分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								document.getElementById("TextBox1").value = rv[1];
								//alert(txtName.value);
								txtID.value = rv[0];
								txtPattern.value = "部门";//
								
								//window.parent.frames["mainContent"].location = "PermissionTreeView.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=department/"+txtName.value;
								//window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=department/"+txtName.value;
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
						
					case "post"://岗位
						result = window.showModalDialog('../../RBAC/Roles/ChoosePost.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	//PostModules.txtRoleTypeName.value='按岗位分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
							document.getElementById("TextBox1").value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "岗位";
								
								//window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=post/"+txtName.value;
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
					
					case "duty"://职位
						result = window.showModalDialog('../../RBAC/Roles/ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	//PostModules.txtRoleTypeName.value='按职位分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								document.getElementById("TextBox1").value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "职位";
								//window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=duty/"+txtName.value;
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
					
					case "employee":
						result = window.showModalDialog('../../RBAC/Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	//PostModules.txtRoleTypeName.value='按员工分配';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								document.getElementById("TextBox1").value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "员工";
								//window.parent.frames["ButtonSetting"].location = "ButtonAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&RoleID=" + txtID.value + "&RoleType=employee/"+txtName.value;
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
			function Sort()
			{
				if(document.getElementById("dropListSV").value=="常量")
				{
					document.getElementById("txtValue").Visible=true;
					//document.getElementById("dropListValue").visible=false;
					document.getElementById("txtValue").readOnly=false;
				}
				else
				{
					//document.getElementById("txtValue").visible=false;
					//document.getElementById("dropListValue").Visible=true;
					result = window.showModalDialog('./Macro.aspx','','dialogWidth=20;dialogHeight=30;status=no;scroll=no');
					document.getElementById("txtValue").value=result;
					
				}
			}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="rolCondition1"><asp:textbox id="tbpermissionFile" runat="server" Height="0px" Width="0px"></asp:textbox><asp:textbox id="tableEName" runat="server" Height="0px" Width="0px"></asp:textbox>
			</div>
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 704px; POSITION: absolute; TOP: 16px; HEIGHT: 224px"
				cellSpacing="1" cellPadding="1" width="704" border="1">
				<TR>
					<TD colSpan="3"><asp:button id="btnReturn" runat="server" Height="24px" Width="64px" Text="返回" onclick="btnReturn_Click"></asp:button><asp:button id="btnSave" runat="server" Height="24px" Width="64px" Text="保存" onclick="btnSave_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<asp:Label id="lblName" runat="server" Width="264px" Font-Bold="True">数据过滤规则性描述：</asp:Label>
						<asp:TextBox id="txtName" runat="server" Width="640px" ForeColor="Blue"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD colSpan="3"><FONT face="宋体">
							<asp:label id="Label2" runat="server" Width="200px" Font-Bold="True">角色操作权限：</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<TABLE id="Table3">
							<TR>
								<TD noWrap>类别</TD>
								<TD>
									<asp:dropdownlist id="dropListSort" runat="server" Width="88px">
										<asp:ListItem Value="department" Selected="True">部门</asp:ListItem>
										<asp:ListItem Value="post">岗位</asp:ListItem>
										<asp:ListItem Value="duty">职位</asp:ListItem>
										<asp:ListItem Value="employee">人员</asp:ListItem>
									</asp:dropdownlist>
									<asp:textbox id="txtPattern" Height="0px" Width="0px" Runat="server" contentEditable="false"></asp:textbox></TD>
								<TD noWrap><FONT face="宋体">关系</FONT>
								</TD>
								<TD>
									<asp:dropdownlist id="DropDownList1" runat="server"></asp:dropdownlist></TD>
								<TD noWrap><FONT face="宋体">名称
										<asp:textbox id="Textbox1" onclick="ChoosePermissionPattern();" Runat="server" contentEditable="false"></asp:textbox></FONT></TD>
								<TD>
									ID</TD>
								<TD><FONT face="宋体">
										<asp:textbox id="txtID" Runat="server" contentEditable="false"></asp:textbox></FONT></TD>
								<TD>
									<asp:button id="btnAdd1" runat="server" Height="20px" Text="增加" onclick="btnAdd1_Click"></asp:button></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<asp:label id="Label1" runat="server" Width="232px" Font-Bold="True">表字段操作权限：</asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<TABLE id="Table4">
							<TR>
								<TD noWrap>字段</TD>
								<TD><FONT face="宋体">
										<asp:dropdownlist id="dropListField" runat="server" Width="120px"></asp:dropdownlist></FONT></TD>
								<TD style="WIDTH: 35px"><FONT face="宋体">关系</FONT></TD>
								<TD style="WIDTH: 63px">
									<asp:dropdownlist id="DropDownList2" runat="server"></asp:dropdownlist>
								</TD>
								<TD><FONT face="宋体">值类型</FONT></TD>
								<TD><FONT face="宋体">
										<asp:dropdownlist id="dropListSV" runat="server" Width="60px" AutoPostBack="True" onselectedindexchanged="dropListSV_SelectedIndexChanged">
											<asp:ListItem Value="常量" Selected="True">常量</asp:ListItem>
											<asp:ListItem Value="宏">宏</asp:ListItem>
										</asp:dropdownlist></FONT></TD>
								<TD><FONT face="宋体">数值&nbsp;</FONT></TD>
								<TD>
									<asp:textbox id="txtValue" onclick="Sort();" Width="177px" Runat="server"></asp:textbox><FONT face="宋体">
										<asp:dropdownlist id="dropListValue" runat="server" Width="177px" Visible="False"></asp:dropdownlist></FONT></TD>
								<TD>
									<asp:button id="btnAdd2" runat="server" Height="20px" Text="增加" onclick="btnAdd2_Click"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<asp:table id="Table1" runat="server" GridLines="Both" BorderColor="Silver" BorderStyle="Solid"
							BorderWidth="1px" CellSpacing="0" CellPadding="1" CssClass="tableWid"></asp:table></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
