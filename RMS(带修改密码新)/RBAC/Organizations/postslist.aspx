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
					window.open('postsadd.aspx?RBACModuleID=<%=RBACModuleID%>','','width=450px,height=350px,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );	
					break;
				
				case 'GroupConfig':
					window.open('../RoleConfig/RoleGroupConfig.aspx','','width=700px,height=540px,scrollbars=1,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );			
				
			}
    </script>

    <script language="javascript">

        function Modify(id) {
            window.open('postsadd.aspx?Id=' + id + '&RBACModuleID=<%=RBACModuleID%>', '', 'width=450px,height=350px,resizable=yes,left=' + (screen.availWidth - 620) / 2 + ',top=' + (screen.availHeight - 470) / 2);
        }
        
		function openempl(id) {
		    window.open('personadd.aspx?Id=' + id, '', 'width=700px,height=480px,resizable=yes,top=100,left=100');
		}

		function Del(id) {
		    if (confirm('您真的要删除此岗位吗？')) {
		        //location = 'UserList.aspx?del=' + id;
		        FormDelUser.delID.value = id;
		        FormDelUser.submit();
		    }
		}
		
		function EditPerson(eid) {
		    if (eid != '')
		        window.open('personadd.aspx?Id=' + eid, '', 'width=700px,height=540px,resizable=yes,left=' + (screen.availWidth - 740) / 2 + ',top=' + (screen.availHeight - 540) / 2);
		}
		var SelEmpl = '';

		function SelectEmpl(eid) {
		    if (SelEmpl != '') {
		        document.all['NM' + SelEmpl].style.backgroundColor = '';
		        document.all['NM' + SelEmpl].style.color = '';
		    }

		    SelEmpl = eid;
		    document.all['NM' + eid].style.backgroundColor = 'gold';
		    document.all['NM' + SelEmpl].style.color = 'black';
		}
		
		function Editposts(eid) {
		    if (eid != '')
		        window.open('postsadd.aspx?Id=' + eid + '&RBACModuleID=<%=RBACModuleID%>', '', 'width=450px,height=350px,resizable=yes,left=' + (screen.availWidth - 740) / 2 + ',top=' + (screen.availHeight - 540) / 2);
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
                <b>岗位设置(权限中的角色管理)</b>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <iewc:Toolbar ID="Toolbar1" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Double"
        Width="100%">
        <iewc:ToolbarButton ID="new" ImageUrl="../../images/icons/0013_b.gif" DefaultStyle="display;border:solid 1px white;"
            HoverStyle="border:solid 1px red;" Text="&amp;nbsp;添加岗位" Enabled="true"></iewc:ToolbarButton>
        <iewc:ToolbarButton ID="GroupConfig" ImageUrl="../../images/icons/06.gif" DefaultStyle="border:solid 1px white;" HoverStyle="border:solid 1px red;"
            Text="角色分类管理"></iewc:ToolbarButton>
        
    </iewc:Toolbar>
    <br>

    <script language="javascript">
		
    </script>

    <table border="0" width="95%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
    </table>
    <table border="1" width="90%" cellspacing="0" cellpadding="2" align="center" style="border-collapse: collapse">
        <tr bgcolor="#e0e0e0">
            <td align="left" nowrap colspan="4">
                <B>角色分类</B>
            </td>
        </tr>
        <tr bgcolor="#e0e0e0">
            <td align="right" nowrap>
                角色编号
            </td>
            <td align="center" nowrap>
                角色名称
            </td>
            <td align="center" nowrap>
                角色描述
            </td>
            <td align="center" nowrap>
                操作
            </td>
        </tr>
        <%
            for (int iGroup = 0; iGroup < dtGroups.Rows.Count; iGroup++)
            {
                %>
                <tr  bgcolor="#f0f0f0">
                    <td colspan="4" style="font-weight:bold"><%=iGroup + 1 %>,<%=dtGroups.Rows[iGroup]["RoleGroupName"]%>: 
                    <%=dtGroups.Rows[iGroup]["RoleGroupDesc"].ToString().Replace("\r\n","<br/>").Replace(" ","&nbsp;")%></td>
                </tr>
                <%
                System.Data.DataRow[] drs = Dt.Select("GroupName='" + dtGroups.Rows[iGroup]["RoleGroupName"] + "'", "GroupOrder");
                for (int k = 0; k < drs.Length; k++)
                {%>
                <tr id="NM<%=drs[k]["Id"].ToString()%>" bgcolor="white" onmouseover="this.bgColor='LemonChiffon'"
                    ondblclick="Editposts('<%=Convert.IsDBNull(drs[k]["Id"])?"":drs[k]["Id"]%>')"
                    onmouseout="this.bgColor=''"  style="cursor:hand">
                    <td align=right>
                        <%=drs[k]["SN"]%>
                    </td>
                    <td>
                        <%=drs[k]["CName"]%>
                    </td>
                    <td>
                        <%=drs[k]["Descr"]%>
                    </td>
                    <td>
                        <a href="<%if(modify){%>javascript:Modify('<%=drs[k]["Id"]%>')<%}else Response.Write("#");%>">
                            修改</a>&nbsp;&nbsp; <a href="<%if(delete){%>javascript:Del('<%=drs[k]["Id"]%>')<%}else Response.Write("#");%>">
                                删除</a>
                    </td>
                </tr>
                <%
                Dt.Rows.Remove(drs[k]);
                }
            }%>
            
            <%
            if(Dt.Rows.Count>0)
            {
                %>
                <tr  bgcolor="#f0f0f0">
                    <td colspan="4" style="font-weight:bold;color:Red">xx,未知分类: 没有指定分类的角色</td>
                </tr>
                <%
                System.Data.DataRow[] drs = Dt.Select("", "GroupOrder");
                for (int k = 0; k < drs.Length; k++)
                {%>
                <tr id="Tr1" bgcolor="white" onmouseover="this.bgColor='LemonChiffon'"
                    ondblclick="Editposts('<%=Convert.IsDBNull(drs[k]["Id"])?"":drs[k]["Id"]%>')"
                    onmouseout="this.bgColor=''"  style="cursor:hand">
                    <td align="right">
                        <%=drs[k]["SN"]%>
                    </td>
                    <td>
                        <%=drs[k]["CName"]%>
                    </td>
                    <td>
                        <%=drs[k]["Descr"]%>
                    </td>
                    <td>
                        <a href="<%if(modify){%>javascript:Modify('<%=drs[k]["Id"]%>')<%}else Response.Write("#");%>">
                            修改</a>&nbsp;&nbsp; <a href="<%if(delete){%>javascript:Del('<%=drs[k]["Id"]%>')<%}else Response.Write("#");%>">
                                删除</a>
                    </td>
                </tr>
                <%
                Dt.Rows.Remove(drs[k]);
                }
            }%>
            
    </table>
    <form id="FormDelUser" name="FormDelUser" method="post" runat="server">
    <input id="delID" name="delID" type="hidden" value="">
    </form>
    <br>
</body>
</html>
