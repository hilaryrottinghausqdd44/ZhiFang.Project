<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.company_news" Codebehind="company_news.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<%
    com.unicafe.forum.NewsGroup ng;
    com.unicafe.forum.NewsMgr nm = new com.unicafe.forum.NewsMgr();
    ng = nm.GetNewsGroup(int.Parse(Request.QueryString["Param"].ToString()));

    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function ViewNews(nid)
	{		
		window.open ("/news/ViewNews.aspx?nid=" + nid, "_blank", "toolbar=no,location=no,directories=no,status=yes,menubar=no,width=650,height=550,scrollbars=1,resizable=1");
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>
                            <%=ng.GroupName%></b></font>
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
                ArrayList newslist;
                newslist = nm.ListNews(10, int.Parse(Request.QueryString["Param"].ToString()));

                if (newslist.Count == 0)
                {
                    Response.Write("<font color=gray>[ÎÞ]</font>");
                }
                else
                {
                    Response.Write("<table width=100% border=0 cellspacing=0>");
                    for (int j = 0; j < newslist.Count && j < 5; j++)
                    {
                        com.unicafe.forum.News n = (com.unicafe.forum.News)newslist[j];

                        Response.Write("<tr style=\"cursor:hand\" onmouseover=\"this.bgColor='#ffff66'\" onmouseout=\"this.bgColor=''\" onclick=\"javascript:ViewNews(" + n.NewsID + ")\">");
                        Response.Write("<td width=1><img src=\"/images/icons/0069_b.gif\" border=0 align=absbottom></td>");
                        Response.Write("<td width=1>");
                        if (nm.VerifyAttachmentByNewsID(n.NewsID))
                            Response.Write("<img src='/forum/images/attachment.gif' border=0 align='absbottom' WIDTH='9' HEIGHT='15'>&nbsp;");
                        Response.Write("</td>");
                        Response.Write("<td title=\"" + n.NewsTitle.Replace("\"", "&quot;") + "\">");
                        Response.Write("<div style=\"width:100%; height:14px; overflow:hidden\">");
                        if (n.CreateTime >= DateTime.Today)
                            Response.Write("<font color=red face=Arial style=\"background-color:yellow;font-size:8pt\"><b>New!</b></font>&nbsp;");
                        Response.Write(n.NewsTitle);
                        Response.Write("</div>");
                        Response.Write("</td>");
                        Response.Write("<td width=1% nowrap><font color=gray>");
                        Response.Write(n.CreateTime.ToString("yy-MM-dd"));
                        Response.Write("</font></td>");
                        Response.Write("</tr>");
                    }
                    Response.Write("</table>");
                }
            %>
            <div align="right">
                <a href="/info/default.aspx?Node=0.3&Url=../news/NewsList.aspx?gid=<%=ng.GroupID%>">
                    <img src="/images/more.gif" border="0" alt="¸ü¶à"></a></div>
        </td>
    </tr>
</table>
