<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.Deptlist" Codebehind="Deptlist.aspx.cs" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>PersonList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		
		<script language="javascript" for="Toolbar1" event="onbuttonclick">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'new':
					window.open('personadd.aspx?Id=0&RBACModuleID=<%=RBACModuleID%>&DeptId=<%=id%>','','width=700px,height=650px,scrollbars=1,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-650)/2 );	
					break;
				case 'newhr':
				   if (SelEmpl == '')
					{
						alert ('请选择人员。');
					}  
					else
					{		
					    window.open('personaddhr.aspx?Id=' + SelEmpl + '&RBACModuleID=<%=RBACModuleID%>&DeptId=<%=id%>','','width=700px,height=650px,scrollbars=1,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-650)/2 );	
					}
					break;
				case 'Remove':
					if (confirm('确定要把部门从目录中去除吗？'))
						location = 'PersonList.aspx?did=<%=Request.QueryString["id"] + ""%>&id=<%=Request.QueryString["id"] + ""%>';
					break;
				case 'newdept':
					window.open('Deptadd.aspx?Id=<%=id%>&RBACModuleID=<%=RBACModuleID%>','','width=450,height=260,top=200,scrollbars=1,left=200');
					break;
				case 'viewdept':
					window.open('Deptinfo.aspx?id=<%=Request.QueryString["id"] + ""%>&RBACModuleID=<%=RBACModuleID%>','','width=450,height=260,top=200,left=200,scrollbars=1');
					break;
				case 'deldept':
					if (confirm('当该部门有下级时，该部门不能删除！\n\n' +
						'当该部门内有员工时，该部门不能删除！\n\n' +
						'删除该部门，分配给该部门的模块功能将不在该部门的控制范围！\n\n\n' +
						'根据以上情况，您确认要删除该部门吗？'))
						//window.open ('DelDept.aspx?deptdel=<%=Request.QueryString["id"] + ""%>','','width=200px,height=200px');
						//document.all("datafrm").src= 'DelDept.aspx?deptdel=<%=Request.QueryString["id"] + ""%>';
						location.href="Deptlist.aspx?Id=<%=Request.QueryString["id"]%>&KeyWord=deldept";
					break;   
				case 'viewrole':
					if (SelEmpl == '')
					{
						alert ('请选择要查看角色的人员。');
					}  
					else
					{						
						window.open ('EmployeeRole.aspx?Id=' + SelEmpl,'','width=500px,height=430px,top=200,scrollbars=1,left=300');
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
								location = 'Deptlist.aspx?EmpId=' + SelEmpl + '&Id=<%=Request.QueryString["id"] + ""%>&KeyWord=delEmpl';
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
				
				case 'ToolbarButtonDesktop'://设置部门桌面
					window.open('DeptDesktopInfo.aspx?id=<%=Request.QueryString["id"] + ""%>&RBACModuleID=<%=RBACModuleID%>','','width=750,height=600,top=0,left=0,scrollbars=1');
					break;
				case 'ToolbarButtonRefresh'://设置部门桌面
					window.open(document.location.href + '&Refresh=','_self','width=450,height=260,top=200,left=200,scrollbars=1');
					break;
					


			}			
		</script>
		<script language="javascript">
		var SelEmpl = '';
		var account='';
			
			function SelectEmpl(useraccount,eid)
			{
				
				if (SelEmpl != '')
				{
					document.all['NM'+account].style.backgroundColor = '';
					document.all['NM'+account].style.color = '';
				}
				SelEmpl=eid;
				account=useraccount;
				
				document.all['NM'+useraccount].style.backgroundColor = 'gold';
				document.all['NM'+useraccount].style.color = 'black';
			}
			function EditPerson(eid)
			{
			<%if(browsedown)
			{%>
				if(eid!='')
					window.open('personadd.aspx?Id=' + eid + '&RBACModuleID=<%=RBACModuleID%>','','width=700px,height=650px,resizable=yes,scrollbars=1,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-650)/2 );
			<%}%>
			}
			function ShowTree()
			{
				var frm=parent.fset;
				if(frm.cols.indexOf(",0,")>-1)
				{
					frm.cols=frm.cols.replace(",0,",",170,");
					//ImgSwap.style.display="none";
					if(parent.frames["midFrame"].location.href.indexOf("about:blank")>-1)
					{
						parent.frames["midFrame"].location.href="DeptTree.aspx";
					}
					window.document.all["ImgDept"].src="../../images/icons/0029_b.gif";
					window.document.all["DeptTree"].innerHTML="隐藏组织机构图&nbsp;&nbsp;";
					
					window.document.all["Depttreeview"].value="yes";
				}
				else
				{
					frm.cols=frm.cols.replace(",170,",",0,");
					//ImgSwap.style.display="";
					window.document.all["ImgDept"].src="../../images/icons/0069_b.gif";
					window.document.all["DeptTree"].innerHTML="显示组织机构图&nbsp;&nbsp;";
					window.document.all["Depttreeview"].value="no";
				}
			}
		</script>
<script id=clientEventHandlersJS language=javascript>
<!--

function window_onload() {
	if('<%=errWindowOnLoad%>'!='')
		window.alert('<%=errWindowOnLoad%>');
}
function window_onunload() {
	var frm=parent.fset;
	
	//frm.cols=frm.cols.replace(",170,",",0,");
	
}

function ExpandNode(NodeData,bExpand)
{
	
	var tree = window.parent.frames["midFrame"].document.all["Treeview1"];
	var node = tree.getTreeNode(NodeData);
	window.parent.frames["midFrame"].document.all["nodedata"].value=NodeData;
		//alert(window.parent.frames["midFrame"].document.all["nodedata"].value)
	//tree.setAttribute('selectedNodeIndex',NodeData);
}
//-->
</script>
</HEAD>
	<body topmargin="0" leftmargin="0" onselectstart="return false" language=javascript onload="return window_onload()" onunload="return window_onunload()">
		<?XML:NAMESPACE PREFIX=TVNS />
		<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
		<table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="COLOR: white;BORDER-COLLAPSE: collapse"
			bgcolor="steelblue">
			<tr height="30">
				<td width="1%" nowrap>
					&nbsp;<img src="../../images/icons/0019_a.gif" align="absBottom">&nbsp;
				</td>
				<td>
						
					<b>
					 <a style="CURSOR:hand" onclick="Depts('0')"><u><%=Company%></u></a> 
					 
					<% 
					for(int k=0;k<z;k++)
					     {%> 
					     &gt; <a style="CURSOR:hand" onclick="Depts('<%=DId[k]%>')"><u><%=DName[k]%></u></a> 
					     <%
					     }%>
						
					</b>
				</td>
				<td align="right">
				<table style="COLOR: white;"><tr><td><img id="ImgDept" src="../../images/icons/0069_b.gif" onclick="ShowTree()" style="CURSOR:hand"></td><td><label id="DeptTree" onclick="ShowTree()" style="CURSOR:hand">显示组织机构图&nbsp;&nbsp;</label></td></tr></table>
					
				</td>
			</tr>
		</table>
		<%
			if (id == ""||id=="0")
			{				
				//Toolbar1.Items[2].DefaultStyle.Add("display","none");
				Toolbar1.Items[3].DefaultStyle.Add("display","none");
				
			}
		%>
		
		<iewc:Toolbar id="Toolbar1" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Double"
			width="100%">
			<iewc:ToolbarButton ImageUrl="../../images/icons/0013_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;添加员工" Id="new" Enabled="true"></iewc:ToolbarButton>			
			<iewc:ToolbarButton ImageUrl="../../images/icons/0013_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;添加下级部门" Id="newdept" Enabled="true"></iewc:ToolbarButton>
			<iewc:ToolbarButton ImageUrl="../../images/icons/0016_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;查看部门信息" Id="viewdept" Enabled="true"></iewc:ToolbarButton>
			<iewc:ToolbarButton ImageUrl="../../images/icons/0014_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;删除该部门" Id="deldept" Enabled="true"></iewc:ToolbarButton>
			<iewc:ToolbarButton ImageUrl="../../images/icons/0014_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;员工离职" Id="delempl" Enabled="true"></iewc:ToolbarButton>
			<iewc:ToolbarButton ImageUrl="../../images/icons/0016_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;人事修改" Id="newhr" Enabled="true"></iewc:ToolbarButton>
			
			<iewc:ToolbarButton ImageUrl="../../images/icons/0034_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;部门桌面设置" Id="ToolbarButtonDesktop" Enabled="true"></iewc:ToolbarButton>
			<iewc:ToolbarButton ImageUrl="../../images/icons/0023_b.gif" DefaultStyle="display;border:solid 1px white;" HoverStyle="border:solid 1px red;" Text="&amp;nbsp;刷新" Id="ToolbarButtonRefresh" Enabled="true"></iewc:ToolbarButton>
			
			
		</iewc:Toolbar>
		<br>
		<script language="javascript">
		<!--
			var item = Toolbar1.getItem(1);
			item.setAttribute('text','添加下级部门');
			item = Toolbar1.getItem(2);
			item.setAttribute('text','查看部门信息');			
			item = Toolbar1.getItem(3);
			item.setAttribute('text','删除该部门');			
		//-->
		function Depts(deptid)
		{
			var frm=parent.fset;
			if(frm.cols.indexOf(",0,")>-1)
			{
				if(parent.frames["midFrame"].location.href.indexOf("about:blank")>-1)
				{
					parent.frames["midFrame"].location.href="DeptTree.aspx";
				}
			}
			ExpandNode(deptid,true);
			location='Deptlist.aspx?id='+deptid;
			
		}
		</script>
		<%
		int iCount=0;
		if(SubDept!=null&&SubDept.Tables.Count>0)
			iCount=SubDept.Tables[0].Rows.Count;
		%>
		<table width="100%">
			<tr>
				<td>
					<span style="MARGIN-LEFT: 10px">&nbsp;&nbsp;<b><u><font color=red>下属部门</font></u></b>(<%=iCount%>)</span>
					<br><br>
					<table border="0" cellspacing="0" cellpadding="2" align="left" style="MARGIN-LEFT: 22px; BORDER-COLLAPSE: collapse" width=55>
						<tr><%
							int i=0;
							if(SubDept!=null&&SubDept.Tables.Count>0)
							{
								foreach(DataRow subDep in SubDept.Tables[0].Rows)
								{ 	
									%>
									<td width="120" onmouseover="this.bgColor='gold'" onmouseout="this.bgColor=''" 
									onclick="Depts('<%=subDep["Id"]%>')" style="CURSOR:hand"  
									nowrap><img src="../../images/icons/0059_b.gif" align=absBottom>&nbsp;<%=subDep["CNAME"]%></td>
									<%
									i++;
									if((double)i/5==(int)i/5)
									{
										Response.Write("</tr><tr>");
									}
								}
							}%>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
						<%
							iCount=EDt.Rows.Count;
						%>
						<br>
						<span style="MARGIN-LEFT: 10px">&nbsp;&nbsp;<b><u><font color=red>下属人员</font></u></b>(<%=iCount%>)</span>
						<br><br>
						<%Response.Write(newXml.InnerXml);%>
				
				</td>
			</tr>
			<tr>
				<td>
						<span style="MARGIN-LEFT: 10px">&nbsp;&nbsp;<b><u><font color=red>说明</font></u></b>(*号代表部门为本单位，未属于任何下级部门)</span>
						
				</td>
			</tr>
		</table>
		<form id="dept">
		<input type="text" name="Depttreeview" id="Depttreeview" style="WIDTH:0" width=0 height=0>
		</form>
		<script language="javascript">
		//window.document.all["Depttreeview"].value='<%=DeptTreeview%>';
		
		</script>
	<br>
		<iframe name="datafrm" width="0" height="0" src="../library/blank.htm"></iframe>
		<br>
		
	</body>
</HTML>
