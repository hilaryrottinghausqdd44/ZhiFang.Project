<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.top_news" Codebehind="top_news.aspx.cs" %>

<%
    com.unicafe.forum.NewsGroup ng;
    com.unicafe.forum.NewsMgr nm = new com.unicafe.forum.NewsMgr();
    ng = nm.GetNewsGroup(int.Parse(Request.QueryString["Param"].ToString()));

    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>
<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>头条新闻</b></font>
                    </td>
                    <td align="right" nowrap>
                        <a style="cursor: hand" onclick="imgOn_<%=ID%>.style.display = 'none';imgOff_<%=ID%>.style.display = '';trContent_TopNews.style.display = 'none';">
                            <img id="imgOn_<%=ID%>" src="/images/hidden-on.gif" border="0" align="absbottom"></a>
                        <a style="cursor: hand" onclick="imgOn_<%=ID%>.style.display = '';imgOff_<%=ID%>.style.display = 'none';trContent_TopNews.style.display = '';">
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
                newslist = nm.ListNews(1, int.Parse(Request.QueryString["Param"].ToString()));

                if (newslist.Count == 0)
                {
                    Response.Write("<font color=gray>[无]</font></td></tr></table>");
                    Response.End();
                }
                com.unicafe.forum.News news = (com.unicafe.forum.News)newslist[0];

                Response.Write("<font size=3><b>" + news.NewsTitle + "</b></font>");
                Response.Write("<br>");
                Response.Write(news.CreateTime.ToString("yyyy-MM-dd HH:mm"));

                ArrayList result = nm.ListNewsAttachmentByNewsID(news.NewsID);
                if (result != null)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        com.unicafe.forum.NewsAttachment na = (com.unicafe.forum.NewsAttachment)result[i];

                        if (na.AttExtension == "gif" || na.AttExtension == "jpg" || na.AttExtension == "bmp")
                        {
                            Response.Write("<table border=0><tr><td><img src=\"/news/ShowPic.aspx?nid=" + news.NewsID + "&attid=" + na.AttID + "&filename=" + na.AttFileName + "\" align=right></td></tr></table><br>");
                        }
                    }
                }

                Response.Write(news.NewsContent);
            %>
        </td>
    </tr>
</table>
