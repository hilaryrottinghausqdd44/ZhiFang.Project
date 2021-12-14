<%@ Page Language="c#" AutoEventWireup="True" Inherits="theNews.mNews.newsMain" Codebehind="newsMain.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>newsMain</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css.css" rel="stylesheet" type="text/css">
</head>
<body rightmargin="0" topmargin="0" leftmargin="0" bottommargin="0">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" style="z-index: 101; left: 8px; position: absolute; top: 8px"
        height="573" cellspacing="0" cellpadding="0" align="center" border="0">
        <tr>
            <td width="39">
                &nbsp;
            </td>
            <td valign="top" width="767">
                <table id="Table2" height="573" cellspacing="0" cellpadding="0" width="767" border="0">
                    <tr>
                        <td width="115" height="28">
                            <img height="28" src="image/index_2/top1.jpg" width="115">
                        </td>
                        <td width="436">
                            <img height="28" src="image/index_2/top2.jpg" width="436">
                        </td>
                        <td width="216">
                            <img height="28" src="image/index_2/top3.jpg" width="216">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="3">
                            <table id="Table3" height="545" cellspacing="0" cellpadding="0" width="767" border="0">
                                <tr>
                                    <td width="7">
                                        <img height="545" src="image/index_2/left.jpg" width="7">
                                    </td>
                                    <td>
                                        <table id="Table4" height="545" cellspacing="0" cellpadding="0" width="754" border="0">
                                            <tr>
                                                <td height="23" style="width: 220px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" colspan="3">
                                                    <table id="Table5" cellspacing="0" cellpadding="0" width="754" border="0">
                                                        <%
                                                            if (iCount == 0)
                                                            {
                                                                Response.Write("<tr><td nowrap colspan=\"5\">这类信息没有具体内容</td></tr>");
                                                                Response.End();
                                                            }
                                                        %>
                                                        <%
                                                            for (int i = 0; i < dt.Rows.Count; i++)
                                                            {
                                                        %>
                                                        <tr id="tr<%=dt.Rows[i]["id"].ToString()%>" onmouseover="javascript:this.bgColor='#aadfaa'"
                                                            onmouseout="javascript:this.bgColor='#ffffff'" ondblclick="javascript:window.open('../browse/eachnews.aspx?id=<%=dt.Rows[i]["id"].ToString()%>','_blank')">
                                                            <td width="33">
                                                                &nbsp;
                                                            </td>
                                                            <td width="4">
                                                                <img height="4" src="image/index_1/middle_point.jpg" width="4">
                                                            </td>
                                                            <td width="9">
                                                                &nbsp;
                                                            </td>
                                                            <td class="textm" nowrap>
                                                                <a class="textm" href="../browse/eachnews.aspx?id=<%=dt.Rows[i]["id"].ToString()%>"
                                                                    target="_blank">
                                                                    <%=dt.Rows[i]["title"].ToString()%></a>
                                                            </td>
                                                            <td width="25">
                                                                <img height="9" src="image/index_1/middle_new.jpg" width="25">
                                                            </td>
                                                            <td width="30">
                                                                &nbsp;
                                                            </td>
                                                            <td class="textm" nowrap>
                                                                <%=dt.Rows[i]["dateandtime"].ToString()%>
                                                            </td>
                                                            <td width="44">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="12" colspan="8">
                                                            </td>
                                                        </tr>
                                                        <% }%>
                                                        <tr>
                                                            <td height="6">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="22">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="220" height="11" style="width: 220px">
                                                    &nbsp;
                                                </td>
                                                <td class="index" nowrap width="148">
                                                    <a class="index" href="#">首页</a> <a class="index" href="#">尾页</a> <a class="index"
                                                        href="#">上一页</a> <a class="index" href="#">下一页</a>
                                                </td>
                                                <td width="46">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="32">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td background="image/index_2/bottom.jpg" colspan="3" height="1">
                                                    <img height="1" alt="" src="" width="1" name="">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="6">
                                        <img height="545" src="image/index_2/right.jpg" width="6">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
