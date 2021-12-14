<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.invitations" Codebehind="invitations.aspx.cs" %>

<%@ Import Namespace="com.unicafe.calendar" %>
<%
    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function OpenEvent(id)
	{
		window.open ('/calendar/EditEvent.aspx?id=' + id, '', 'width=600,height=450,status=no,toolbar=no,scrollbars=no');
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>日程邀请</b></font>
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
                EventList = mgr.FindPendingEvents(CurrentEmployee.EmplID);
                if (EventList.Count == 0)
                    Response.Write("<font color=gray>[无]</font>");
                for (int i = 0; i < EventList.Count; i++)
                {
                    Event e = (Event)EventList[i];
                    EventType et = mgr.GetEventType(e.EventTypeID);

                    Response.Write("<table border=1 style=\"border-collapse:collapse; cursor:hand\" onclick=\"OpenEvent('" + e.EventID + "')\" cellspacing=0 bordercolor=white>");
                    Response.Write("<tr><td bgcolor=" + et.Color + ">");

                    Response.Write("<img src=/calendar/images/men.gif width=16 height=16 border=0 align=absbottom>&nbsp;");

                    Response.Write(e.EventBegin.ToString("H:mm") + "&nbsp;" + e.EventEnd.ToString("H:mm") + "&nbsp;");
                    Response.Write(e.EventTitle);
                    if (e.UserID != CurrentEmployee.EmplID)
                    {
                        com.unicafe.security.OrgMgr omgr = new com.unicafe.security.OrgMgr();

                        Response.Write("&nbsp;[邀请人：" + omgr.GetEmpl(e.UserID).EmplName + "]");
                    }
                    Response.Write("</td></tr></table>");

                }
            %>
        </td>
    </tr>
</table>
