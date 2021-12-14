<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.forum_board" Codebehind="forum_board.aspx.cs" %>

<%
    com.unicafe.forum.Board board;
    com.unicafe.forum.ForumMgr fm = new com.unicafe.forum.ForumMgr();
    board = fm.GetBoard(int.Parse(Request.QueryString["Param"].ToString()));

    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function openview(id)
	{		
		window.open ("/forum/ViewArticle.aspx?objid=<%=Request.QueryString["Param"].ToString().Trim()%>&id=" + id,"_blank", "toolbar=no,location=no,directories=no,status=yes,menubar=no,width=620,height=450,resizable=yes,scrollbars=0,top=100,left=150");
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>
                            <%=board.BoardName%></b> - 在线讨论</font>
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
                ArrayList articlelist;
                articlelist = fm.ListArticle(int.Parse(Request.QueryString["Param"].ToString()), 8);
                if (articlelist.Count == 0)
                {
                    Response.Write("<font color=gray>[无]</font>");
                }
                else
                {
                    Response.Write("<table width=100% border=0 cellspacing=0>");
                    for (int j = 0; j < articlelist.Count && j < 5; j++)
                    {
                        com.unicafe.forum.Article a = (com.unicafe.forum.Article)articlelist[j];

                        Response.Write("<tr style=\"cursor:hand\" onmouseover=\"this.bgColor='#ffff66'\" onmouseout=\"this.bgColor=''\" onclick=\"openview(" + a.ArticleID + ")\">");
                        Response.Write("<td width=1><img src=\"/images/icons/0003_b.gif\" border=0 align=absbottom></td>");
                        Response.Write("<td width=1>");
                        if (fm.CountAttachment(a.ArticleID) > 0)
                            Response.Write("<img src='/forum/images/attachment.gif' border=0 align='absbottom' WIDTH='9' HEIGHT='15'>&nbsp;");
                        Response.Write("</td>");
                        Response.Write("<td title=\"" + a.ArticleTitle.Replace("\"", "&quot;") + "\">");
                        Response.Write("<div style=\"width:100%; height:14px; overflow:hidden\">");
                        if (a.CreateTime >= DateTime.Today)
                            Response.Write("<font color=red face=Arial style=\"background-color:yellow;font-size:8pt\"><b>New!</b></font>&nbsp;");
                        Response.Write(a.ArticleTitle);
                        Response.Write("</div>");
                        Response.Write("</td>");
                        Response.Write("<td width=1% nowrap><font color=gray>");
                        Response.Write(a.ArticleAuthor);
                        Response.Write("</font></td>");
                        Response.Write("<td width=1% nowrap>&nbsp;<font color=gray>");
                        Response.Write(a.CreateTime.ToString("yy-MM-dd"));
                        Response.Write("</font></td>");
                        Response.Write("</tr>");
                    }
                    Response.Write("</table>");
                }
            %>
            <div align="right">
                <a href="/collabration/default.aspx?Node=3&Url=../forum/Board.aspx?id=<%=board.BoardID + Server.UrlEncode("&") + "objid=" + board.BoardID%>">
                    <img src="/images/more.gif" border="0" alt="更多"></a></div>
        </td>
    </tr>
</table>
