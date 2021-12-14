<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.WorkflowFolder" Codebehind="WorkflowFolder.aspx.cs" %>

<%@ Import Namespace="com.unicafe.workflow" %>
<%@ Import Namespace="com.unicafe.security" %>
<%@ Import Namespace="System.Xml" %>
<%
    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function OpenMessage(mid, nid)
	{
		window.open ('/workflow/run/OpenMessage.aspx?MessageID=' + mid + '&NodeKey=' + nid,'_blank','width=620,height=470,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2 );
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>待办事宜</b></font>
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
                if (NodeList1.Count == 0)
                    Response.Write("<font color=gray>[无]</font>");
                else
                {
                    Response.Write("<table width=100% border=0 cellspacing=0>");
                    for (int i = 0; i < NodeList1.Count && i < 5; i++)
                    {
                        Node node = (Node)NodeList1[i];
                        Message msg = wmgr.GetMessage(node.MessageID);
                        Employee empl = om.GetEmpl(msg.MessageIssuedBy);

                        Response.Write("<tr style=\"cursor:hand\" onmouseover=\"this.bgColor='#ffff66'\" onmouseout=\"this.bgColor=''\" onclick=\"OpenMessage('" + msg.MessageID + "','" + node.NodeKey + "')\">");
                        Response.Write("<td width=1><img src=\"/images/icons/0009_b.gif\" border=0 align=absbottom></td>");

                        Response.Write("<td title=\"" + msg.MessageTitle.Replace("\"", "&quot;") + "\">");
                        Response.Write("<div style=\"width:100%; height:14px; overflow:hidden\">");
                        Response.Write(msg.MessageTitle);
                        Response.Write("</div>");
                        Response.Write("</td>");

                        Response.Write("<td align=right><font color=gray>");
                        if (empl != null)
                            Response.Write(empl.EmplName);
                        Response.Write("</font></td>");

                        Response.Write("<td width=1% nowrap>&nbsp;<font color=gray>");
                        Response.Write(msg.MessageCreateTime.ToString("yy-MM-dd"));
                        Response.Write("</font></td>");

                        Response.Write("</tr>");
                    }
                    Response.Write("</table>");
                    if (More1) Response.Write("<div align=right><a href=\"javascript:location='/workflow'\"><img src=\"/images/more.gif\" border=0 alt=\"更多\"></a></div>");
                }
            %>
        </td>
    </tr>
</table>
