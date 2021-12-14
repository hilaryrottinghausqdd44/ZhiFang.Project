<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.personal_memo" Codebehind="personal_memo.aspx.cs" %>

<%
    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function OpenPersonalTask(TaskID)
	{
		window.open('/project/PersonalTaskView.aspx?TaskID='+TaskID,'PersonalTaskView','Width=485,Height=325,scroll=no,status=no');
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>个人任务</b></font>
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
                com.unicafe.project.PersonalTaskMgr ptm = new com.unicafe.project.PersonalTaskMgr();
                com.unicafe.project.PersonalTask pt = new com.unicafe.project.PersonalTask();
                ArrayList result = ptm.FindPersonalTask(CurrentEmployee.EmplID);

                if (result.Count == 0)
                {
                    Response.Write("<font color=gray>[无]</font>");
                }
                else
                {
                    Response.Write("<table width=100% border=0 cellspacing=0>");
                    for (int i = 0; i < result.Count && i < 10; i++)
                    {
                        pt = (com.unicafe.project.PersonalTask)result[i];

                        if (pt.Status == "已完成")
                            continue;

                        Response.Write("<tr style=\"cursor:hand\" onmouseover=\"this.bgColor='#ffff66'\" onmouseout=\"this.bgColor=''\" onclick=\"OpenPersonalTask(" + pt.PTID + ")\">");
                        Response.Write("<td width=1 nowrap><img src=\"/images/icons/0004_b.gif\" border=0 align=absbottom>&nbsp;</td>");

                        Response.Write("<td>");
                        Response.Write("<div style=\"width:100%; height:14px; overflow:hidden\">");
                        if (pt.Enddate < DateTime.Now)
                            Response.Write("<font color=red>");
                        else
                            Response.Write("<font color=black>");
                        Response.Write(pt.Task);
                        Response.Write("</font>");
                        Response.Write("</div>");
                        Response.Write("</td>");

                        Response.Write("<td align=right nowrap><font color=gray>(");
                        Response.Write(pt.Begindate.ToString("yy-MM-dd"));
                        Response.Write(")-(");
                        Response.Write(pt.Enddate.ToString("yy-MM-dd"));
                        Response.Write(")</font></td>");

                        Response.Write("</tr>");
                    }
                    Response.Write("</table>");
                }			
            %>
        </td>
    </tr>
</table>
