<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.email" Codebehind="email.aspx.cs" %>

<%
    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function EditEmail(id)
	{		
		window.open('/email/MailMessage.aspx?EM_ID='+id,'','width=600,height=480,scrollbars=no,resizable=yes,left=' + (screen.availWidth-620)/2 + ',top=' + (screen.availHeight-470)/2);
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>新邮件</b></font>
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
                com.unicafe.email.EmailMgr eMgr = new com.unicafe.email.EmailMgr();
                ArrayList result = eMgr.FindEmailMessage(this.CurrentEmployee.EmplID, 10, "");

                if (result.Count == 0)
                {
                    Response.Write("<font color=gray>[无]</font>");
                }
                else
                {
                    Response.Write("<table width=100% border=0 cellspacing=0>");
                    for (int i = 0; i < result.Count && i < 5; i++)
                    {
                        com.unicafe.email.EmailMessage em = (com.unicafe.email.EmailMessage)result[i];

                        Response.Write("<tr style=\"cursor:hand\" onmouseover=\"this.bgColor='#ffff66'\" onmouseout=\"this.bgColor=''\" onclick=\"EditEmail('" + em.EM_ID + "')\">");
                        Response.Write("<td width=1><img src=\"/images/icons/0079_b.gif\" border=0 align=absbottom></td>");

                        Response.Write("<td width=1>");
                        if (eMgr.totalEmailAttachment(em.EM_ID) > 0)
                            Response.Write("<img src='/email/images/attachment.gif' border=0 align='absbottom' WIDTH='9' HEIGHT='15'>&nbsp;");
                        Response.Write("</td>");

                        string EmailSubject = em.EM_Subject.Trim();
                        if (EmailSubject == "")
                            EmailSubject = "(无主题)";

                        Response.Write("<td title=\"" + EmailSubject.Replace("\"", "&quot;") + "\">");
                        Response.Write("<div style=\"width:100%; height:14px; overflow:hidden\">");
                        Response.Write(EmailSubject);
                        Response.Write("</div>");
                        Response.Write("</td>");

                        string EmailFrom = em.EM_From.Replace("<", "&lt;").Replace(">", "&gt;");
                        Response.Write("<td width=100 title=\"" + EmailFrom + "\">");
                        Response.Write("<div style=\"width:100px; height:14px; overflow:hidden\">");
                        Response.Write("<font color=gray>" + EmailFrom + "</font>");
                        Response.Write("</div>");
                        Response.Write("</td>");

                        Response.Write("<td width=1% nowrap><font color=gray>");
                        Response.Write(em.EM_DateSend.ToString("yy-MM-dd"));
                        Response.Write("</font></td>");
                        Response.Write("</tr>");
                    }
                    Response.Write("</table>");
                }			
            %>
            <div align="right">
                <a href="/email/default.aspx?Node=0.0&Url=../email/ReceiveList.aspx?ReceiveType=0">
                    <img src="/images/more.gif" border="0" alt="更多"></a></div>
        </td>
    </tr>
</table>
