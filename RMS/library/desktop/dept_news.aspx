<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.dept_news" Codebehind="dept_news.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<%
    com.unicafe.forum.NewsMgr nm = new com.unicafe.forum.NewsMgr();

    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function ViewDeptNews(nid)
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
                            <%=CurrentEmployee.DeptName%></b> -
                            <%if (Request.QueryString["type"] + "" == "org") Response.Write("机构"); else Response.Write("部门");%>新闻</font>
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
                ArrayList priNews;
                if (Request.QueryString["type"] + "" == "org")
                    priNews = nm.ListNewsByDeptID(10, ((com.unicafe.security.Department)(CurrentEmployee.MyOrgList[0])).DeptID);
                else
                    priNews = nm.ListNewsByDeptID(10, ((com.unicafe.security.Department)(CurrentEmployee.MyDeptList[0])).DeptID);

                if (priNews.Count == 0)
                {
                    Response.Write("<font color=gray>[无]</font>");
                }
                else
                {
                    Response.Write("<table width=100% border=0 cellspacing=0>");
                    for (int i = 0; i < priNews.Count && i < 5; i++)
                    {
                        com.unicafe.forum.News n = (com.unicafe.forum.News)priNews[i];

                        Response.Write("<tr style=\"cursor:hand\" onmouseover=\"this.bgColor='#ffff66'\" onmouseout=\"this.bgColor=''\" onclick=\"javascript:ViewDeptNews(" + n.NewsID + ")\">");
                        Response.Write("<td width=1><img src=\"/images/icons/0069_b.gif\" border=0 align=absbottom></td>");
                        Response.Write("<td width=1>");
                        if (nm.VerifyAttachmentByNewsID(n.NewsID))
                            Response.Write("<img src='/forum/images/attachment.gif' border=0 align='absbottom' WIDTH='9' HEIGHT='15'>&nbsp;");
                        Response.Write("</td>");
                        Response.Write("<td title=\"" + n.NewsTitle + "\">");
                        Response.Write("<div style=\"width:100%; height:14px; overflow:hidden\">");
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
                if (Request.QueryString["type"] + "" == "org")
                {
            %>
            <div align="right">
                <a href="/info/default.aspx?Node=0.2&Url=/news/NewsList.aspx?type=org">
                    <img src="/images/more.gif" border="0" alt="更多"></a></div>
            <%
                }
            else
            {
            %>
            <div align="right">
                <a href="/info/default.aspx?Node=0.3&Url=/news/NewsList.aspx">
                    <img src="/images/more.gif" border="0" alt="更多"></a></div>
            <%
                }
            %>
        </td>
    </tr>
</table>
