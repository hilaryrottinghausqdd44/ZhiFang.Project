<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.userlist" Codebehind="userlist.aspx.cs" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PersonList</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript" for="Toolbar1" event="onbuttonclick">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'new':
					window.open('useradd.aspx','','width=550px,height=500px,resizable=yes,scrollbars=1,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
					break;
				case 'modify':
					if(SelEmpl=='')
					{
						alert('请选择人员！');
						return false
					}
					else		
							
						window.open('modifyuser.aspx?Id='+SelEmpl+'&RBACModuleID=<%=RBACModuleID%>','','width=550px,height=500px,status=yes,scrollbars=1,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
					break;
				case 'Remove':
					if (confirm('确定要把部门从目录中去除吗？'))
						location = 'PersonList.aspx?did=<%=Request.QueryString["id"] + ""%>&id=<%=Request.QueryString["id"] + ""%>';
					break;
				case 'newdept':
					window.open('Deptadd.aspx?','','width=800,height=550,top=200,left=300');
					break;
				case 'viewdept':
					window.open('Deptinfo.aspx?id=<%=Request.QueryString["id"] + ""%>','','width=800,height=500,top=200,scrollbars=1,left=300');
					break;
				case 'deldept':
					if (confirm('删除该部门，则其下所属部门都将会删除。您确认要删除吗？'))
						//window.open ('DelDept.aspx?deptdel=<%=Request.QueryString["id"] + ""%>','','width=200px,height=200px');
						document.all("datafrm").src= 'DelDept.aspx?deptdel=<%=Request.QueryString["id"] + ""%>';
					break;   
				case 'viewrole':
					if (SelEmpl == '')
					{
						alert ('请选择要查看角色的人员。');
					}  
					else
					{						
						window.open ('../security/UserEdit.aspx?emplid=' + SelEmpl,'','width=500px,height=430px,scrollbars=0,top=200,scrollbars=1,left=300');
					}
					break;
				case 'delempl':
					if (SelEmpl == '')
					{
						alert ('请选择要删除的人员。');
					}  
					else
					{
						if (SelEmpl == '100001')
						{
							alert('"系统管理员"不能被删除！');
						}
						else
						{
							if (confirm ('您确定要删除该人员吗？'))
							{
								<%
								if (Request.QueryString["KeyWord"] == null)
								{
								%>
									location = 'PersonList.aspx?del=' + SelEmpl + '&id=<%=Request.QueryString["id"] + ""%>';
								<%
								}
								else
								{
								%>
									location = 'PersonList.aspx?del=' + SelEmpl + '&id=<%=Request.QueryString["id"] + ""%>&KeyWord=<%=Request.QueryString["KeyWord"] + ""%>';
								<%
								}
								%>
							}
						}
					}
					break;
				case 'setrelation':
					if (SelEmpl == '')
					{
						alert ('请选择人员。');
					}  
					else
					{						
						window.open ('../security/SetLDAPUser.aspx?emplid=' + SelEmpl,'','width=300px,height=200px,scrollbars=0');
					}
					break;
				case 'viewpow':
					if (SelEmpl == '')
					{
						alert ('请选择人员。');
					} 
					else
					{
					window.open('PersonPower.aspx?emplid=' + SelEmpl,'','width='+screen.availWidth-40+',height='+screen.availHeight-10+',resizable=yes,scrollbars=1,left=10,top=20' );	
					}
					break;
				case 'export_E'://'导出':
				    break;
				case 'import_E'://'导入':
				    window.open('import_E.aspx');
				    break;
			}
		</script>
		<script language="javascript">
		
		function ModifyPassword(id)
		{
			window.open ('ModifyUserPassword.aspx?Id=' + id,'','width=300px,height=200px,resizable=yes,top=240,left=300');
		}
		function openempl(id)
		{
			window.open ('personadd.aspx?Id=' + id,'','width=680px,height=540px,resizable=yes,top=100,left=100');
		}
		function DelUser(id)
		{
			if (confirm('您真的要删除此用户吗？'))
			{
				
				FormDelUser.delID.value=id;
				FormDelUser.submit();
			}
		}
		function EditPerson(eid)
			{
				if(eid!='')
				    window.open('personadd.aspx?Id=' + eid, '', 'width=680px,height=540px,resizable=yes,scrollbars=1,left=' + (screen.availWidth - 740) / 2 + ',top=' + (screen.availHeight - 540) / 2);
			}
		var SelEmpl = '';
			
			function SelectEmpl(eid)
			{
			
				
				if (SelEmpl != '')
				{
					document.all['NM'+SelEmpl].style.backgroundColor = '';
					document.all['NM'+SelEmpl].style.color = '';
				}
				
				SelEmpl = eid;				
				document.all['NM'+eid].style.backgroundColor = 'gold';
				document.all['NM'+SelEmpl].style.color = 'black';
			}
		</script>
		<STYLE TYPE="text/css">
          .fixedHeader {
           position:relative ;
           table-layout:fixed;
           border-color:gray;
           top:expression(this.offsetParent.scrollTop);  
           z-index: 10;
          }

          .fixedHeader td{
           text-overflow:ellipsis;
           overflow:hidden;
           white-space: nowrap;
          }
         </STYLE>
	</HEAD>
	<body topmargin="0" leftmargin="0" onselectstart="return false">
		<form id="FormDelUser" name="FormDelUser" method="post" runat="server">
			<table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="COLOR: white;BORDER-COLLAPSE: collapse"
				bgcolor="steelblue">
				<tr height="30">
					<td width="1%" nowrap>
						&nbsp;<img src="../../images/icons/0019_a.gif" align="absBottom">&nbsp;
					</td>
					<td><b>账号设置</b>
					</td>
					<td align="right">
					</td>
				</tr>
			</table>
			<iewc:Toolbar id="Toolbar1" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Double"
				width="100%">
				<iewc:ToolbarButton id="new" Enabled="true" Text="&amp;nbsp;添加账号" HoverStyle="border:solid 1px red;"
					DefaultStyle="display;border:solid 1px white;" ImageUrl="../../images/icons/0013_b.gif"></iewc:ToolbarButton>
				<iewc:ToolbarButton id="modify" Enabled="true" Text="&amp;nbsp;修改账号" HoverStyle="border:solid 1px red;"
					DefaultStyle="display;border:solid 1px white;" ImageUrl="../../images/icons/0013_b.gif"></iewc:ToolbarButton>
				<iewc:ToolbarButton ImageUrl="../../images/icons/0060_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;查看员工权限" Id="viewpow" Enabled="true"></iewc:ToolbarButton>
				<iewc:ToolbarButton ImageUrl="../../images/icons/0055_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;导出机构与帐号" Id="export_E" Enabled="true"></iewc:ToolbarButton>
				<iewc:ToolbarButton ImageUrl="../../images/icons/0056_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;导入机构与帐号" Id="import_E" Enabled="true"></iewc:ToolbarButton>
				<iewc:ToolbarButton id="PrevPage" Text="上一页" HoverStyle="border:solid 1px red;" DefaultStyle="border:solid 1px white;"></iewc:ToolbarButton>
				<iewc:ToolbarButton id="NextPage" Text="下一页" HoverStyle="border:solid 1px red;" DefaultStyle="border:solid 1px white;"></iewc:ToolbarButton>
				
			</iewc:Toolbar>
			<br>
			<table border="0" width="100%" cellspacing="0" cellpadding="2" align="center" style="BORDER-COLLAPSE:collapse">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" Height="16px" Font-Size="Small" Font-Bold="True">查找用户：</asp:Label>
						<asp:TextBox id="searchuser" runat="server"></asp:TextBox><FONT face="宋体">&nbsp;&nbsp;&nbsp;
						</FONT>
						<asp:Button id="butsearch" runat="server" Text="查找" onclick="butsearch_Click"></asp:Button><FONT face="宋体">（请输入账号名或者员工名）
							<asp:Label id="Label2" runat="server"></asp:Label></FONT></td>
				</tr>
			</table>
			<table border="1" width="95%" cellspacing="0" cellpadding="2" align="center" style="BORDER-COLLAPSE:collapse">
				<tr bgcolor="#e0e0e0"  class="fixedHeader">
					<td align="center" nowrap>账号名</td>
					<td align="center" nowrap>密码</td>
					<td align="center" nowrap>对应员工姓名</td>
					<td align="center" nowrap>账号创建时间</td>
					<td align="center" nowrap>上次登录时间</td>
					<td align="center" nowrap>修改密码</td>
					<td align="center" nowrap>删除</td>
				</tr> 
				<%for(int k=0;k<Dt.Rows.Count;k++)
				{%>
					<tr id="NM<%=Dt.Rows[k]["Id"].ToString()%>" bgcolor="white" 
					onclick="SelectEmpl('<%=Dt.Rows[k]["Id"]%>')" 
					<%if(browse){					
					%>
					ondblclick="EditPerson('<%=Convert.IsDBNull(Dt.Rows[k]["EmpID"])?"":Dt.Rows[k]["EmpID"]%>')" 
					<%}%>
					onmouseover="this.bgColor='LemonChiffon'" 
					onmouseout="this.bgColor=''">
						<td><%=Dt.Rows[k]["Account"]%></td>
						<td><%=Dt.Rows[k]["password"]%></td>
						<td><a href=javascript:openempl('<%=Dt.Rows[k]["EmpID"]%>')><%=Dt.Rows[k]["NameL"].ToString()+Dt.Rows[k]["NameF"]%></a></td>
						<td><%=Dt.Rows[k]["AccCreateTime"]%></td>
						<%if(Convert.IsDBNull(Dt.Rows[k]["LoginTm"]))
							{%>
							<td>从未登录！</td>	
							<%}
							else
							{%>
								<td>
								<%
									TimeSpan t=DateTime.Now-Convert.ToDateTime(Dt.Rows[k]["LoginTm"]);
									string ss=((int)t.TotalDays==0)?"":(int)t.TotalDays + "天";
									Response.Write(ss);
									Response.Write(t.Hours + "小时前");
									
								%>
								</td>
							<%}%>					
						<td><a href="<%if(modifypassword){%>javascript:ModifyPassword('<%=Dt.Rows[k]["Id"]%>')<%}else Response.Write("#");%>">修改密码</a></td>
						
						<td><a href="<%if(delete){%>javascript:DelUser('<%=Dt.Rows[k]["Id"]%>')<%}else Response.Write("#");%>">删除</a></td>
						</tr>
					
			  <%}%>
			</table>
			<input id="delID" name="delID" type="hidden">
		</form>
		<iframe name="datafrm" width="0" height="0" src="../library/blank.htm"></iframe>
		<br>
	</body>
</HTML>
