<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.ProjectFolder" Codebehind="ProjectFolder.aspx.cs" %>

<%
    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function OpenProject(projectid,projectname)
	{
		window.open("/project/Project_Task_View.aspx?ProjectID="+ projectid +"&ProjectName="+ projectname,"","toolbar=no,location=no,directories=no,status=yes,menubar=no,width=750,height=550,scrollbars=0,top=50,left=50,resizable=yes");
	}
	function viewtask(TaskID)
	{
		window.open("/project/TaskModify.aspx?TaskID="+ TaskID,"newTask","width=450,height=360");
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>项目计划</b></font>
                    </td>
                    <td align="right" nowrap>
                        <a style="cursor: hand" onclick="imgOn_<%=ID%>.style.display = 'none';imgOff_<%=ID%>.style.display = '';trContent_<%=ID%>.style.display = 'none';">
                            <img id="imgOn_<%=ID%>" src="/images/hidden-on.gif" border="0" align="absbottom"></a>
                        <a style="cursor: hand" onclick="imgOn_<%=ID%>.style.display = '';imgOff_<%=ID%>.style.display = 'none';trContent_<%=ID%>.style.display = '';">
                            <img id="imgOff_<%=ID%>" src="/images/hidden-off.gif" border="0" align="absbottom"
                                style="display: none"></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="height: 1px" bgcolor="Teal">
        <td>
        </td>
    </tr>
    <tr style="height: 5px">
        <td>
        </td>
    </tr>
    <tr id="trContent_<%=ID%>">
        <td valign="top">
            <%
                DateTime date = DateTime.Today;

                com.unicafe.project.TaskActorMgr tam = new com.unicafe.project.TaskActorMgr();
                com.unicafe.project.TaskMgr tm = new com.unicafe.project.TaskMgr();
                com.unicafe.project.Task task = new com.unicafe.project.Task();
                com.unicafe.project.ProjectMgr pm = new com.unicafe.project.ProjectMgr();
                com.unicafe.project.Project proj = new com.unicafe.project.Project();

                ArrayList result = tam.FindTaskByActor(CurrentEmployee.EmplID, date);
                int TaskID;
                if (result.Count > 0)
                {
                    Response.Write("<table width=100% border=0>");
                    for (int i = 0; i < result.Count; i++)
                    {
                        TaskID = (int)result[i];
                        task = tm.GetTask(TaskID);

                        proj = pm.GetProject(task.ProjectID);

                        Response.Write("<tr><td>");
                        Response.Write("<a href=\"javascript:OpenProject(" + proj.ProjectID + ",'" + proj.ProjectName + "')\" class=ActLink><img src=/images/icons/0009_b.gif align=absbottom border=0>" + proj.ProjectName + "</a><br>");
                        Response.Write("&nbsp;&nbsp;&nbsp;<a href=\"javascript:viewtask(" + TaskID + ")\" class=ActLink><img src=/images/icons/0004_b.gif align=absbottom border=0>" + task.TaskName + "</a>(" + task.TaskProcedure + "%)<br>");
                        int n = ((TimeSpan)(task.TaskEnd - date)).Days;
                        if (n > 7) n = 7;
                        if (n < 1) n = 1;

                        Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt=\"截至" + task.TaskEnd.ToString("yyyy年M月d日") + "\" src=/project/images/TaskRest_7_" + n.ToString() + ".jpg align=absbottom><br>");
                        Response.Write("</td></tr>");
                    }
                    Response.Write("</table>");
                }
                else
                {
                    Response.Write("<font color=gray>[无]</font>");
                }
            %>
        </td>
    </tr>
</table>
