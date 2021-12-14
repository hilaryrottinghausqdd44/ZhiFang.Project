<%@ Import Namespace="System" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.News.Body" Codebehind="Body.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Main</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link rel="stylesheet" type="text/css" href="Main.css">

    <script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() {

}

//-->
    </script>

    <script language="javascript1.2">
function showsubmenu(sid)
{
whichEl = eval("submenu" + sid);
if (whichEl.style.display == "none")
{
eval("submenu" + sid + ".style.display=\"\";");
}
else
{
eval("submenu" + sid + ".style.display=\"none\";");
}
}
    </script>

</head>
<body leftmargin="0" topmargin="0" language="javascript" onload="return window_onload()">
    <table width="671" border="0" align="center" cellspacing="0">
        <tr>
            <td width="669" height="97">
                <table id="table4" bordercolor="#c0c0c0" cellpadding="0" width="99%" border="0">
                    <tbody>
                        <tr>
                            <td width="50%">
                                <% 
                                    if (dtCa == null)
                                    {
                                        Response.Write("出错");
                                        Response.End();
                                    }
                                    for (int i = 0; i < dtCa.Rows.Count; i++)
                                    {
                                        if (dtCa.Rows[i]["CategoryId"].ToString().Length <= 5)
                                        {
                                            string caID = dtCa.Rows[i]["Categoryid"].ToString();
									
                                %>
                                <table id="table8" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td id="menuTitle1" onclick="showsubmenu(<%=i.ToString()%>)" width="50%" background="image/menu_bg.gif"
                                                height="25" class="tdblack1px" style="border-bottom: 0px">
                                                &nbsp; <a href="browse/CategoryNews.aspx?categoryid=<%=caID%>" target="_parent">
                                                    <img height="11" src="image/h_1.gif" width="9" border="0">
                                                    <img height="11" src="image/h_1.gif" width="9" border="0">
                                                    <font size="2">&nbsp;<b><font color="#003063"><%=dtCa.Rows[i]["CategoryName"].ToString()%></font></b></font>
                                                    全部.. </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id='submenu<%=i.ToString()%>' style="border: 1px solid #53876D; display: ;" height="20"
                                                class="tdBlack1px">
                                                <%
                                                    int displayCount = 0;
                                                    for (int j = 0; j < dt.Rows.Count; j++)
                                                    {
                                                        //Response.Write(i.ToString());
                                                        //caID==dt.Rows[j]["CategoryID"].ToString()||
                                                        if (caID == dt.Rows[j]["Categoryid"].ToString().Substring(0, 5))
                                                        {
                                                            if (j > 0 && dt.Rows[j]["CategoryID"].ToString() != dt.Rows[j - 1]["CategoryID"].ToString() && dt.Rows[j]["CategoryID"].ToString().Length > 5)
                                                            {
                                                                Response.Write("<br>");
                                                                displayCount = 0;
                                                                if (dt.Rows[j]["CategoryID"].ToString().Length > 10)
                                                                {
                                                                    Response.Write("&nbsp;└---");
                                                                }
                                                                Response.Write("&nbsp;&nbsp;<b><a href=\"Browse/CategoryNews.aspx?categoryid=" + dt.Rows[j]["Categoryid"].ToString() + "\" target=_parent><Font color=\"#003063\">" + dt.Rows[j]["CategoryName"].ToString() + "</Font></a></b>");
                                                                //Response.Write("<br>&nbsp;");
                                                            }
                                                            if (displayCount < Convert.ToInt32(dtCa.Rows[i]["newsdisplay"]))
                                                            {
                                                                //主标题下的信息
                                                                displayCount += 1;
                                                                //Response.Write (displayCount);

                                                                if (displayCount >= Convert.ToInt32(dtCa.Rows[i]["newsdisplay"]))
                                                                {

                                                                }
                                                %>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr class="trbottomDot">
                                                        <%
                                                            System.Text.Encoding en = System.Text.Encoding.GetEncoding("GB2312");
                                                            String strTitle = dt.Rows[j]["Title"].ToString();
                                                            String str = strTitle;

                                                            int nCut = 0;
                                                            if (!Convert.IsDBNull(dt.Rows[j]["attribute"]))
                                                            {
                                                                if (dt.Rows[j]["attribute"].ToString().IndexOf("top") > -1)
                                                                { nCut += 6; }
                                                            }
                                                            if ((System.DateTime.Now - Convert.ToDateTime(dt.Rows[j]["dateandtime"])).TotalDays < 7)
                                                                nCut += 8;
                                                            //Response.Write((System.DateTime.Now-Convert.ToDateTime(dt.Rows[j]["dateandtime"])).TotalDays>7);
                                                            if (dt.Rows[j]["Expr1"].ToString() != "0")
                                                                nCut += 4;

                                                            bool bLong = false;
                                                            while (en.GetByteCount(str) > 42 - nCut)
                                                            {
                                                                str = str.Substring(0, str.Length - 1);
                                                                bLong = true;
                                                            }
                                                            if (bLong)
                                                            {
                                                                //最后为两个字符ASCII
                                                                if (en.GetByteCount(str.Substring(str.Length - 2)) == 2)
                                                                    str = str.Substring(0, str.Length - 2) + "..";

                                                                //最后为两个汉字UNICODE
                                                                else if (en.GetByteCount(str.Substring(str.Length - 2)) == 4)
                                                                    str = str.Substring(0, str.Length - 1) + "..";

                                                                //最后为一个字符ASCII＋一个汉字UNICODE
                                                                else if (en.GetByteCount(str.Substring(str.Length - 2)) == 3)
                                                                {
                                                                    if (en.GetByteCount(str.Substring(str.Length - 1)) == 1)
                                                                        str = str.Substring(0, str.Length - 1) + ".";
                                                                    else if (en.GetByteCount(str.Substring(str.Length - 1)) == 2)
                                                                        str = str.Substring(0, str.Length - 1) + "..";

                                                                }

                                                            }
				
                                                        %>
                                                        <!--小图片-->
                                                        <td width="10" align="center" height="20">
                                                            <img height="11" src="image/h_1.gif" width="9" border="0">
                                                        </td>
                                                        <!--图文新闻？,推荐？-->
                                                        <td width="0" nowrap align="right">
                                                            <%=(Convert.IsDBNull(dt.Rows[j]["attribute"]))?"":((dt.Rows[j]["attribute"].ToString().IndexOf("top")>-1)?"[<font color=red>图</font>]":"")%>
                                                        </td>
                                                        <!--链接-->
                                                        <td>
                                                            <a href="#" onclick="javascript:window.open('browse/eachnews.aspx?id=<%=dt.Rows[j]["id"].ToString()%>','_blank','width=800,height=600,scrollbars=1,top=0,left=200')"
                                                                title="<%=strTitle%>">
                                                                <%=str%>
                                                                <%//=en.GetByteCount(str)%>
                                                            </a>
                                                            <%if ((System.DateTime.Now - Convert.ToDateTime(dt.Rows[j]["dateandtime"])).TotalDays < 7)
                                                              {%><img src="image/8.gif">
                                                            <%}%>
                                                        </td>
                                                        <!--评论-->
                                                        <td width="1" nowrap align="right">
                                                            <%=(dt.Rows[j]["Expr1"].ToString()=="0")?"":"[<a href=\"#\" onclick=\"javascript:window.open(\'browse/Comments.aspx?id=" + dt.Rows[j]["id"].ToString() + "\',\'_blank\',\'width=800,height=600,scrollbars=1,top=0,left=200\')\"><font color=darkred>评</font></a>]"%>
                                                        </td>
                                                        <!--新闻时间??-->
                                                        <td nowrap align="right" width="60px">
                                                            <%=Convert.IsDBNull(dt.Rows[j]["dateandtime"])?"":"["+Convert.ToDateTime(dt.Rows[j]["dateandtime"]).Month.ToString()+ "." + (Convert.ToDateTime(dt.Rows[j]["dateandtime"]).Day.ToString().Length==1?"0":"") + Convert.ToDateTime(dt.Rows[j]["dateandtime"]).Day.ToString() + ":" + (Convert.ToDateTime(dt.Rows[j]["dateandtime"]).Hour.ToString().Length==1?"0":"") + Convert.ToDateTime(dt.Rows[j]["dateandtime"]).Hour.ToString() + "]"%>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%
                                                    }
            else if (j == 0)//||dt.Rows.Count==0)
            {
                //主标题无信息
                //displayCount=0;
                Response.Write("<br><b><Font color=\"#003063\">" + dt.Rows[j]["CategoryName"].ToString() + "<b></Font>");
            }

        }
    }
	
                                                %>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <%
                                    }
                                    }
                                %>
                            </td>
                            <td width="15">
                            </td>
                            <td width="50%">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <p>
        <font face=""></font>&nbsp;</p>
</body>
</html>
