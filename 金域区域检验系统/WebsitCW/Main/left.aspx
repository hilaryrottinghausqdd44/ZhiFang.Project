<%@ Page Language="c#" AutoEventWireup="True" Inherits="theNews.main.left" Codebehind="left.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css.css" rel="stylesheet" type="text/css">

    <script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() 
{

}
function shrink(frmName)
{
	var frm=parent.fset;
	frm.cols="0,*";
	var top=parent.frames["topFrame"]
	var enlarge=top.document.all["enlarge"];
	
	enlarge.style.display="";

}

//-->
    </script>

    <script language="javascript1.2">
function showsubmenu(sid)
{
	try
	{
		whichEl = eval("submenu" + sid);
		if(whichEl ==null) return;
		if (whichEl.style.display == "none")
		{

			eval("submenu" + sid + ".style.display=\"\";");
			eval("submenuB" + sid + ".style.display=\"\";");
			eval("img" + sid + ".src=\"image/anniu3.jpg\";");

		}
		else
		{
			eval("submenu" + sid + ".style.display=\"none\";");
			eval("submenuB" + sid + ".style.display=\"none\";");
			eval("img" + sid + ".src=\"image/anniu2.jpg\";");
		}
		window.status="完成";
	}
	catch(e)
	{
		window.status="没有下级分类,请选择他类";
	}
}
    </script>

</head>
<body bgcolor="#999999" topmargin="0" leftmargin="0" background="image/index_1/leftbg.jpg"
    language="javascript" onload="return window_onload()">
    <table id="Table1" style="width: 151px" cellspacing="0" cellpadding="0" width="151"
        background="image/index_1/leftbg.jpg" border="0">
        <tr>
            <td height="27">
                <img height="27" src="image/shrink.gif" width="149" style="cursor: hand;" onclick="shrink('left')">
            </td>
        </tr>
        <tr>
            <td height="5">
                <img height="1" alt="" src="" width="1" name="">
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table id="Table2" cellspacing="0" cellpadding="0" width="149" border="0">
                    <%
                        /*if(Session["userid"]==null||Session["userid"]=="")
						{
							Response.Write("<tr><td>");
							Response.Write("登录时间已经失效,<br><a href=\"../\" target=\"_top\">请重新登录</a>");
							Response.Write("</td></tr></table></td></tr></table></body></html>");
							Response.End();
							
						}
						*/

                        for (int i = 0; i < dtCa.Rows.Count; i++)
                        {
                    %>
                    <tr>
                        <td class="left" valign="middle" background="image/anniu1.jpg" height="25" ondblclick="showsubmenu(<%=i.ToString()%>)">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="20">
                                    </td>
                                    <td>
                                        <div align="center">
                                            <a class="left" href="<%=Convert.IsDBNull(dtCa.Rows[i]["RedirectUrl"])||dtCa.Rows[i]["RedirectUrl"].ToString().Trim()==""?"#":dtCa.Rows[i]["RedirectUrl"].ToString()%>"
                                                target="<%=Convert.IsDBNull(dtCa.Rows[i]["RedirectUrl"])||dtCa.Rows[i]["RedirectUrl"].ToString().Trim()==""?"_self":dtCa.Rows[i]["RedirectTarget"].ToString()%>">
                                                <%=dtCa.Rows[i]["name"]%>
                                            </a>
                                        </div>
                                    </td>
                                    <td width="20" align="center">
                                        <img id="img<%=i.ToString()%>" onclick="showsubmenu(<%=i.ToString()%>)" src="image/anniu2.jpg"
                                            width="15" height="15" style="cursor: hand">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%
                        bool hasChild = false;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["ParentId"].ToString() == dtCa.Rows[i]["id"].ToString())
                            {
                                hasChild = true;
                                break;
                            }
                        }
                        if (hasChild)
                        {
                    %>
                    <tr id="submenuB<%=i.ToString()%>" style="display: none;">
                        <td height="2">
                            <img height="1" alt="" src="" width="1" name="">
                        </td>
                    </tr>
                    <tr id="submenu<%=i.ToString()%>" style="display: none;">
                        <td>
                            <table id="Table3" cellspacing="0" cellpadding="0" width="149" background="image/index_1/leftbg.jpg"
                                border="0">
                                <tr>
                                    <td width="21">
                                    </td>
                                    <td>
                                        <table id="Table4" cellspacing="0" cellpadding="0" width="102" border="0">
                                            <%for (int j = 0; j < dt.Rows.Count; j++)
                                              {
                                                  if (dt.Rows[j]["ParentId"].ToString() == dtCa.Rows[i]["id"].ToString())
                                                  {
                                            %>
                                            <tr>
                                                <td height="17" width="102" background="image/index_1/left3.jpg">
                                                    <div id="Div1" style="width: 101px; height: 14px">
                                                        <div align="center">
                                                            <a class="left1" href="<%=Convert.IsDBNull(dt.Rows[j]["RedirectUrl"])?"#":dt.Rows[j]["RedirectUrl"].ToString()%>"
                                                                target="main">
                                                                <%=dt.Rows[j]["name"]%></a></div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <%
                                                }
                        }
                                            %>
                                        </table>
                                    </td>
                                    <td width="26" background="image/index_1/leftbg.jpg">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%}%>
                    <tr>
                        <td height="2">
                            <img height="1" alt="" src="" width="1" name="">
                        </td>
                    </tr>
                    <%}%>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
