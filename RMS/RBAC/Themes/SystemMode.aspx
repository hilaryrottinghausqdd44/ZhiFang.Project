<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Themes.OptMode" Codebehind="SystemMode.aspx.cs" %>
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
					window.open('Mode.aspx?operateType='+<%=OperateType%>,'','width=350px,height=200px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
					break;
				
				
				case 'viewposition':
				if(SelEmpl=='')
					{
						alert('请选择职位！');
						return false
					}
					else
						
					//window.open('positionadd.aspx?id='+SelEmpl,'','width=370px,height=320px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );
					break;
				case 'delposition':
				if(SelEmpl=='')
					{
						alert('请选择职位！');
						return false
					}
					else		
					{
						Del(SelEmpl)
					}
					break;  				
				
			}
			
		</script>
		<script language="javascript">
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
			function Editposition(eid)
			{
				if(eid!='')
					window.open('positionadd.aspx?Id=' + eid,'','width=370px,height=320px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
			}
			function Del(id)
			{
				if (confirm('确实要删除吗！'))			
				{			
					FormDel.delID.value=id;
					FormDel.submit();
				}
			}
			function Modify(id)
			{
				window.open ('Mode.aspx?Id=' + id,'','width=350px,height=200px,resizable=yes,top=240,left=300');
			}			
		</script>
	</HEAD>
	<body topmargin="0" leftmargin="0" onselectstart="return false">
		<table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="COLOR: white;BORDER-COLLAPSE: collapse"
			bgcolor="steelblue">
			<tr height="30">
				<td width="1%" nowrap>
					&nbsp;<img src="../../images/icons/0019_a.gif" align="absBottom">&nbsp;
				</td>
				<td><%if(OperateType==2){%><b>系统访问策略</b><%}else{%><b>按钮操作方式</b><%}%>
				</td>
				<td align="right">
				</td>
			</tr>
		</table>
		<iewc:Toolbar id="Toolbar1" runat="server">
			<IEWC:TOOLBARBUTTON id="new" Enabled="true" Text="&amp;nbsp;添加" HoverStyle="border:solid 1px red;" DefaultStyle="display;border:solid 1px white;"
				ImageUrl="../../images/icons/0013_b.gif"></IEWC:TOOLBARBUTTON>
			<IEWC:TOOLBARBUTTON id="PrevPage" Text="上一页" HoverStyle="border:solid 1px red;" DefaultStyle="border:solid 1px white;"></IEWC:TOOLBARBUTTON>
			<IEWC:TOOLBARBUTTON id="NextPage" Text="下一页" HoverStyle="border:solid 1px red;" DefaultStyle="border:solid 1px white;"></IEWC:TOOLBARBUTTON>
		</iewc:Toolbar>
		<br>
		<script language="javascript">
		
		</script>
		<table border="0" width="95%" cellspacing="0" cellpadding="2" align="left" style="BORDER-COLLAPSE:collapse">
		</table>
		<table border="1" width="80%" cellspacing="0" cellpadding="2" align="left" style="BORDER-COLLAPSE:collapse">
			<tr bgcolor="#e0e0e0">
				<td align="center" width="15%" nowrap>标识编号</td>
				<td align="center" width="15%" nowrap>名称</td>
				<td align="left" width="15%" nowrap>颜色</td>
				<td align="left" width="40%" nowrap>描述</td>
				<td align="center" width="15%" nowrap>修改与删除</td>
			</tr>
			<%for(int k=0;k<Dt.Rows.Count;k++)
				{%>
					<tr id="NM<%=Dt.Rows[k]["ID"].ToString()%>" bgcolor="white" 
					onclick="SelectEmpl('<%=Dt.Rows[k]["ID"]%>')" 
					ondblclick="Modify('<%=Dt.Rows[k]["ID"]%>')"
					onmouseover="this.bgColor='LemonChiffon'" 
					onmouseout="this.bgColor=''">
						<td  nowrap><%=Dt.Rows[k]["SN"]%></td>
						<td nowrap><%=Dt.Rows[k]["CName"]%></td>
						<td align="right" style="PADDING-RIGHT: 10px; PADDING-LEFT: 13px; 
						FILTER: progid:DXImageTransform.Microsoft.Alpha( 
						style=1,opacity=0,finishOpacity=80,startX=0,finishX=100,startY=100,finishY=0); 
						WIDTH: 305px;  
						BACKGROUND-COLOR: <%=Dt.Rows[k]["OperateColor"]%>"><%=Dt.Rows[k]["OperateColor"]%></td>
						<td nowrap><%=Dt.Rows[k]["Descr"]%></td>
						<td nowrap><a href=javascript:Modify('<%=Dt.Rows[k]["ID"]%>')>修改</a>&nbsp;&nbsp;&nbsp;
						<a href="<%if(abilityRun){%>javascript:Del('<%=Dt.Rows[k]["ID"]%>')<%}else{Response.Write("#");}%>">删除</a></td></tr>					
			  <%}%>
		</table>
		<form id="FormDel" name="FormDel" method="post" runat="server">
			<input id="delID" name="delID" type="hidden">
		</form>
		<iframe name="datafrm" width="0" height="0" src="../library/blank.htm"></iframe>
		<br>
	</body>
</HTML>
