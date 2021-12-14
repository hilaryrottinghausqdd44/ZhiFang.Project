<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.EmployeeRole" Codebehind="EmployeeRole.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>查看修改人员岗位</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script id="clientEventHandlersJS" language="javascript">
<!--

function checkAll()
{
	
	var Added, Deleted;
	Added="";
	Deleted=""
	for(var m=0;m<<%=Dt.Rows.Count%>;m++)
	{
			if(document.all['checkbox' + m].checked)
			{
				document.all['checkbox' + m].value=document.all['checkbox' + m].title;
				Added +=document.all['checkbox' + m].title+',';
			}
			else
			{
				document.all['checkbox' + m].value="off";
				Deleted +=document.all['checkbox' + m].title+','
			}
		
	}
	if(Added=="")
	{
		alert('please select a role');
		return false;
	}
	Added=Added.substring(0,Added.length-1);
	if(Deleted!="")
	{
		Deleted=Deleted.substring(0,Deleted.length-1);
	}
	addRoles.added.value=Added;
	addRoles.deleted.value=Deleted;
}
//-->
    </script>

</head>
<body ms_positioning="GridLayout" language="javascript">
    <div style="left: 8px; width: 472px; position: absolute; top: 8px; height: 462px"
        ms_positioning="FlowLayout">
        <form id="addRoles" name="addRoles" onsubmit="javascript:checkAll()" method="post"
        runat="server">
        <font face="宋体"></font><font face="宋体"></font><font face="宋体"></font>
        <br>
        <table id="Table1" width="100%" align="center" border="0">
            <tr>
                <td nowrap width="1%">
                    &nbsp;<img src="../../images/icons/0018_a.gif" align="absBottom">&nbsp;
                </td>
                <td colspan="4">
                    &nbsp;&nbsp;<font color="highlight"><b>以下是<font color="#660000">系统管理员</font>所拥有的岗位</b></font>
                </td>
            </tr>
            <tr valign="top">
                <td valign="top" align="center" colspan="6">
                    <div style="overflow: auto; height: 260px; widht: 100%; valign: top">
                        <table id="Table2" width="100%" align="center" border="0">
                            <tr>
                                <%if (Dt.Rows.Count != 0)
                                  {
                                      int x = 0;
                                      for (int i = 0; i < Dt.Rows.Count; i++)
                                      {

                                          if (x == 4)
                                          {
                                %></tr>
                            <tr>
                                <%x = 0;
                                            }%>
                                <td>
                                    <input id="checkbox<%=i%>" type="checkbox" name="checkbox<%=i%>" title="<%=Dt.Rows[i]["ID"]%>"
                                        value="off"><label id="'Label'+<%=Dt.Rows[i]["ID"].ToString()%>"><%=Dt.Rows[i]["CName"]%></label>
                                </td>
                                <%x++;
                                        }
                                }
                                %>
                                <tr valign="top">
                                    <td valign="top" height="10%">
                                    </td>
                                </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr height="20">
                <td align="center" colspan="6">
                    <asp:Button ID="Button1" Text="确 定" runat="server" OnClick="Button1_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;<input
                        style="width: 60px" onclick="javascript:window.close()" type="button" value="关闭">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr height="10">
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>

        <script language="javascript">
				if('<%=PostId%>'!='')
				{
					var sPostIdarr='<%=EpostId%>';
					var PostIdarr=sPostIdarr.split(',');
					for(var m=0;m<<%=Dt.Rows.Count%>;m++)
					{
						for(var n=0;n<PostIdarr.length;n++)
						{
							if(document.all['checkbox' + m].title== PostIdarr[n])
							{
								document.all['checkbox' + m].checked=true;
								document.all['checkbox' + m].value=PostIdarr[n];
							}
						}
					}
				}
        </script>

        <input id="added" name="added" type="text" style="display: none">
        <input id="deleted" name="deleted" type="text" style="display: none">
        </form>
    </div>
</body>
</html>
