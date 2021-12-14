<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.AddCondition" Codebehind="AddCondition.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>AddCondition</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<script language=javascript>
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
							{	//PostModules.txtRoleTypeName.value='��Ա������';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								txtName.value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "Ա��";
								
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
							{	//PostModules.txtRoleTypeName.value='�����ŷ���';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								document.getElementById("txtName").value = rv[1];
								//alert(txtName.value);
								txtID.value = rv[0];
								txtPattern.value = "����";//
								
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
						
					case "post"://��λ
						result = window.showModalDialog('../../RBAC/Roles/ChoosePost.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	//PostModules.txtRoleTypeName.value='����λ����';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
							document.getElementById("txtName").value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "��λ";
								
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
					
					case "duty"://ְλ
						result = window.showModalDialog('../../RBAC/Roles/ChoosePosition.aspx','','dialogWidth=30;dialogHeight=20;status=no;scroll=yes');
						if (result != 'undefined' && typeof(result)!='undefined')
						{
							var rv = result.split("|");
							if (rv.length == 2)
							{	//PostModules.txtRoleTypeName.value='��ְλ����';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								document.getElementById("txtName").value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "ְλ";
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
							{	//PostModules.txtRoleTypeName.value='��Ա������';
								//PostModules.txtRoleName.value = rv[1];
								//PostModules.txtRoleID.value = rv[0];
								document.getElementById("txtName").value = rv[1];
								txtID.value = rv[0];
								txtPattern.value = "Ա��";
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
				if(document.getElementById("dropListSV").value=="����")
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
<form id=Form1 method=post runat="server">
<div><FONT face=����></FONT>&nbsp;</DIV><br>
<div></DIV>
<TABLE id=Table4 
style="Z-INDEX: 108; LEFT: 8px; WIDTH: 736px; POSITION: absolute; TOP: 8px; HEIGHT: 328px" 
cellSpacing=1 cellPadding=1 width=736 border=1>
  <TR>
    <TD colSpan=3><FONT face=����><asp:label id=Label3 runat="server" Width="80px" Font-Bold="True">���ƣ�</asp:label><FONT 
      face=����><asp:textbox id=tbName runat="server" Width="408px"></asp:TextBox></FONT>
      <DIV><FONT face=���� 
      ></FONT>&nbsp;</DIV></FONT></TD></TR>
  <TR>
    <TD colSpan=3><asp:label id=Label2 runat="server" Width="208px" Font-Bold="True">��ɫ����Ȩ�ޣ�</asp:label></TD></TR>
  <TR>
    <TD colSpan=3><FONT face=���� 
      ></FONT>
      <TABLE>
        <TR>
          <TD noWrap>���</TD>
          <TD><asp:dropdownlist id=dropListSort runat="server" Width="88px">
										<asp:ListItem Value="department" Selected="True">����</asp:ListItem>
										<asp:ListItem Value="post">��λ</asp:ListItem>
										<asp:ListItem Value="duty">ְλ</asp:ListItem>
										<asp:ListItem Value="employee">��Ա</asp:ListItem>
									</asp:dropdownlist><asp:textbox id=txtPattern Width="0px" Height="0px" contentEditable="false" Runat="server"></asp:textbox></TD>
          <TD noWrap><FONT face=���� 
            >��ϵ</FONT> </TD>
          <TD><asp:dropdownlist id=DropDownList1 runat="server"></asp:dropdownlist></TD>
          <TD noWrap><FONT face=���� 
            ><FONT face=���� 
            >����</FONT></FONT></TD>
          <TD><asp:textbox id=txtName onclick=ChoosePermissionPattern(); contentEditable="false" Runat="server"></asp:textbox></TD>
          <TD><FONT face=����>ID <asp:textbox id=txtID contentEditable="false" Runat="server"></asp:textbox></FONT></TD>
          <TD><asp:button id=btnAdd1 runat="server" Height="20px" Text="����" onclick="btnAdd1_Click"></asp:button></TD>
          <TD></TD></TR></TABLE></TD></TR>
  <TR>
    <TD colSpan=3><asp:table id=Table2 runat="server" Width="648px" BorderColor="#E0E0E0" BorderStyle="Solid" CellPadding="0" CellSpacing="0" BorderWidth="1px" GridLines="Both">
							<asp:TableRow BackColor="GhostWhite">
								<asp:TableCell Text="��ɫ"></asp:TableCell>
								<asp:TableCell Text="��ϵ"></asp:TableCell>
								<asp:TableCell Text="ֵ"></asp:TableCell>
							</asp:TableRow>
						</asp:table></TD></TR>
  <TR>
    <TD colSpan=3><asp:label id=Label1 runat="server" Width="232px" Font-Bold="True">���ֶβ���Ȩ�ޣ�</asp:label></TD></TR>
  <TR>
    <TD colSpan=3>
      <TABLE id=Table1>
        <TR>
          <TD noWrap>�ֶ�</TD>
          <TD><FONT face=����><asp:dropdownlist id=dropListField runat="server" Width="120px"></asp:dropdownlist></FONT></TD>
          <TD style="WIDTH: 48px"><FONT face=���� 
            >��ϵ</FONT></TD>
          <TD style="WIDTH: 69px"><FONT face=���� 
            ><asp:dropdownlist id=DropDownList2 runat="server"></asp:dropdownlist></FONT></TD>
          <TD style="WIDTH: 79px" noWrap><FONT face=���� 
            >ֵ����</FONT></TD>
          <TD><FONT face=����><asp:dropdownlist id=dropListSV runat="server" Width="60px" AutoPostBack="True" onselectedindexchanged="dropListSV_SelectedIndexChanged">
											<asp:ListItem Value="����" Selected="True">����</asp:ListItem>
											<asp:ListItem Value="��">��</asp:ListItem>
										</asp:dropdownlist></FONT></TD>
          <TD style="WIDTH: 42px"><FONT face=���� 
            >��ֵ</FONT></TD>
          <TD><asp:textbox id=txtValue onclick=Sort(); Width="104px" Runat="server"></asp:textbox><asp:dropdownlist id=dropListValue runat="server" Width="112px" Visible="False"></asp:dropdownlist></TD>
          <TD><asp:button id=btnAdd2 runat="server" Height="20px" Text="����" onclick="btnAdd2_Click"></asp:button></TD></TR></TABLE></TD></TR>
  <TR>
    <TD colSpan=3><asp:table id=Table3 runat="server" Width="696px" BorderColor="#E0E0E0" BorderStyle="Solid" CellPadding="0" CellSpacing="0" BorderWidth="1px" GridLines="Both">
							<asp:TableRow BackColor="GhostWhite">
								<asp:TableCell Text="�ֶ�"></asp:TableCell>
								<asp:TableCell Text="��ϵ"></asp:TableCell>
								<asp:TableCell Text="ֵ"></asp:TableCell>
							</asp:TableRow>
						</asp:table></TD></TR>
  <TR>
    <TD align=center colSpan=3><asp:button id=btnReturn runat="server" Width="64px" Height="24px" Text="����" onclick="btnReturn_Click"></asp:button></TD></TR></TABLE></FORM>
	</body>
</HTML>
