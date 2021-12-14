<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.postslist" Codebehind="postslist.aspx.cs" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PersonList</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" for="Toolbar1" event="onbuttonclick">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'new':
					window.open('postsadd.aspx?RBACModuleID=<%=RBACModuleID%>','','width=400px,height=300px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
					break;
				
				case 'Remove':
					if (confirm('确定要把部门从目录中去除吗？'))
						location = 'PersonList.aspx?did=<%=Request.QueryString["id"] + ""%>&id=<%=Request.QueryString["id"] + ""%>';
					break;				
				
			}
    </script>

    <script language="javascript">
		
		function Modify(id)
		{
			window.open('postsadd.aspx?Id=' + id+'&RBACModuleID=<%=RBACModuleID%>','','width=400px,height=300px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );
		}
		function openempl(id)
		{
			window.open ('personadd.aspx?Id=' + id,'','width=700px,height=480px,resizable=yes,top=100,left=100');
		}
		function Del(id)
		{
			if (confirm('您真的要删除此岗位吗？'))
			{
				//location = 'UserList.aspx?del=' + id;
				FormDelUser.delID.value=id;
				FormDelUser.submit();
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
					window.open('postsadd.aspx?Id=' + eid+'&RBACModuleID=<%=RBACModuleID%>','','width=370px,height=320px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
			}
    </script>

</head>
<body topmargin="0" leftmargin="0" onselectstart="return false">
    <table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="color: white;
        border-collapse: collapse" bgcolor="steelblue">
        <tr height="30">
            <td width="1%" nowrap>
                &nbsp;<img src="../../images/icons/0019_a.gif" align="absBottom">
            </td>
            <td>
                <b>岗位设置</b>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <iewc:Toolbar ID="Toolbar1" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Double"
        Width="100%">
        <iewc:ToolbarButton ID="new" ImageUrl="../../images/icons/0013_b.gif" DefaultStyle="display;border:solid 1px white;"
            HoverStyle="border:solid 1px red;" Text="&amp;nbsp;添加岗位" Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton ID="PrevPage" DefaultStyle="border:solid 1px white;" HoverStyle="border:solid 1px red;"
            Text="上一页"></iewc:ToolbarButton>
        <iewc:ToolbarButton ID="NextPage" DefaultStyle="border:solid 1px white;" HoverStyle="border:solid 1px red;"
            Text="下一页"></iewc:ToolbarButton>
    </iewc:Toolbar>
    <br>

    <script language="javascript">
		
    </script>

    <table border="0" width="95%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
    </table>
    <table border="1" width="90%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
        <tr bgcolor="#e0e0e0">
            <td align="left" nowrap>
                岗位名称
            </td>
            <td align="left" nowrap>
                岗位描述
            </td>
            <td align="left" nowrap>
                操作
            </td>
        </tr>
        <%for (int k = 0; k < Dt.Rows.Count; k++)
          {%>
        <tr id="NM<%=Dt.Rows[k]["Id"].ToString()%>" bgcolor="white" onmouseover="this.bgColor='LemonChiffon'"
            ondblclick="Editposts('<%=Convert.IsDBNull(Dt.Rows[k]["Id"])?"":Dt.Rows[k]["Id"]%>')"
            onmouseout="this.bgColor=''">
            <td>
                <%=Dt.Rows[k]["CName"]%>
            </td>
            <td>
                <%=Dt.Rows[k]["Descr"]%>
            </td>
            <td>
                <a href="<%if(modify){%>javascript:Modify('<%=Dt.Rows[k]["Id"]%>')<%}else Response.Write("#");%>">
                    修改</a>&nbsp;&nbsp; <a href="<%if(delete){%>javascript:Del('<%=Dt.Rows[k]["Id"]%>')<%}else Response.Write("#");%>">
                        删除</a>
            </td>
        </tr>
        <%}%>
    </table>
    <form id="FormDelUser" name="FormDelUser" method="post" runat="server">
    <input id="delID" name="delID" type="hidden" value="">
    </form>
    <br>
</body>
</html>
