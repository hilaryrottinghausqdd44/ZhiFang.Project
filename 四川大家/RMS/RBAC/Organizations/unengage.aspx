<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.unengage" Codebehind="unengage.aspx.cs" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>unengagedlist</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript" for="Toolbar1" event="onbuttonclick">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'new':
					window.open('postsadd.aspx','','width=400px,height=300px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
					break;
				
				case 'Remove':
					if (confirm('ȷ��Ҫ�Ѳ��Ŵ�Ŀ¼��ȥ����'))
						location = 'PersonList.aspx?did=<%=Request.QueryString["id"] + ""%>&id=<%=Request.QueryString["id"] + ""%>';
					break;				
				
			}
		</script>
		<script language="javascript">
		
		//function Modify(id)
		//{
		//	window.open ('postsadd.aspx?Id=' + id,'','width=400px,height=300px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );
		//}
		function openempl(id)
		{
			window.open ('personadd.aspx?Id=' + id,'','width=700px,height=480px,resizable=yes,top=100,left=100');
		}
		function Modify(id)
		{
			if (confirm('�����Ҫ�ָ���'))
			{
				//location = 'UserList.aspx?del=' + id;
				FormDelUser.delID.value=id;
				FormDelUser.submit();
			}
		}
		function Delete(id)
		{
			if (confirm('Ҫ����ɾ����ְԱ����������'))
			{
				location = 'unengage.aspx?keyword=del&EmpID=' + id;
				//FormDelUser.delID.value=id;
				//FormDelUser.submit();
			}
		}
		function EditPerson(eid)
			{
				if(eid!='')
					window.open('personadd.aspx?Id=' + eid,'','width=700px,height=540px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
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
			function Editposts(eid)
			{
				if(eid!='')
					window.open('postsadd.aspx?Id=' + eid,'','width=370px,height=320px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
			}
		</script>
	</HEAD>
	<body topmargin="0" leftmargin="0" onselectstart="return false">
		<table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="COLOR: white;BORDER-COLLAPSE: collapse"
			bgcolor="steelblue">
			<tr height="30">
				<td width="1%" nowrap>
					&nbsp;<img src="../../images/icons/0019_a.gif" align="absBottom">
				</td>
				<td><b>��ְ��Ա</b>
				</td>
				<td align="right">
				</td>
			</tr>
		</table>
		<iewc:Toolbar id="Toolbar1" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Double"
			width="100%">
			
			<iewc:ToolbarButton id="PrevPage" DefaultStyle="border:solid 1px white;" HoverStyle="border:solid 1px red;"
				Text="��һҳ"></iewc:ToolbarButton>
			<iewc:ToolbarButton id="NextPage" DefaultStyle="border:solid 1px white;" HoverStyle="border:solid 1px red;"
				Text="��һҳ"></iewc:ToolbarButton>
		</iewc:Toolbar>
		<br>
		<script language="javascript">
		
		</script>
		
		<table border="1" width="95%" cellspacing="0" cellpadding="2" align="center" style="BORDER-COLLAPSE:collapse">
			<tr bgcolor="#e0e0e0">
				
				<td align="center" nowrap>Ա����</td>
				<td align="left" nowrap>����</td>
				<td align="left" nowrap>�û���</td>
				<td align="left" nowrap>����(ְλ)</td>
				<td align="center" nowrap>�����ʼ�</td>
				<td align="center" nowrap>�ֻ�</td>
				<td align="center" nowrap>�ָ� ����ɾ��</td>
			</tr>
			<%				
				for(int k=0;k<EDt.Rows.Count;k++)
				{%>
					<tr id="NM<%=k+1%>" bgcolor="white" 
					onclick="SelectEmpl('<%=k+1%>','<%=k+1%>')" 
					<%if(Browsedown)
					{%>
					ondblclick="EditPerson('<%=EDt.Rows[k]["EmpID"]%>')" 
					<%}
					else
					{%>
					ondblclick="javascript:window.alert('��û��Ȩ�޲鿴��Ա��֯�����е�Ա����Ϣ��')" 
					<%}%>
					onmouseover="this.bgColor='LemonChiffon'"
					onmouseout="this.bgColor=''">
						<td><%=EDt.Rows[k]["EmpSN"]%></td>
						<td><%=EDt.Rows[k]["EmpNameL"].ToString()+EDt.Rows[k]["EmpNameF"].ToString()%></td>
						<td><%=getEmpAccount(Int32.Parse(EDt.Rows[k]["EmpID"].ToString()))%></td>
						<td><%=getEmpDeptName(Int32.Parse(EDt.Rows[k]["EmpID"].ToString()))%></td>
						<td><%=EDt.Rows[k]["Email"]%></td>
						<td><%=EDt.Rows[k]["Mobile"]%></td>
						<td align="center"><%if(Modify)
							{%>
							<a href=javascript:Modify('<%=EDt.Rows[k]["EmpID"]%>')>�ָ�</a>
							<%}
							else
							{%>�ָ�<%}%>
							&nbsp;
							<%if(Remove)
							{%>
							<a href=javascript:Delete('<%=EDt.Rows[k]["EmpID"]%>')>����ɾ��</a>
							<%}
							else
							{%>����ɾ��<%}%>
							</td>
						</tr>
			 <%}%>
		</table>
		
		<form id="FormDelUser" name="FormDelUser" method="post" runat="server">
			<input id=delID name=delID type=hidden value="">
		
		</form>
		<br>
	</body>
</HTML>
