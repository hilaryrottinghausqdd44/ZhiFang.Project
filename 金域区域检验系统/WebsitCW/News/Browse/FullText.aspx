<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Browse.FullText" Codebehind="FullText.aspx.cs" %>

<%@ Import Namespace="OA" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>全文检索内容</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link rel="stylesheet" type="text/css" href="../Main.css">

    <script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() {

}
function AddHeight(iHeight){
	if (iHeight==null)
	{
		iHeight=50
	}
	var size=300;
	var obj1=frames["ifrmbody1"].frameElement;
	
	var height = parseInt(obj1.offsetHeight)
	obj1.height=height + iHeight;
}
function swapColor(str)
{
	var obj=document.getElementById(str)
	if(obj!=null)
	{
		obj.style.backgroundcolor='red';
	}
}
function selPage_onchange() {
	form1.submit();
}

function doPost(iNum)
{
	form1.selPage.options[iNum-1].selected=true;
	form1.submit();
	//window.alert(form1.selPage.selectedIndex);
	//form1.selPage.options[1].selected=true;
}
//-->
    </script>

    <style type="text/css">
        <!
        -- .hr
        {
            height: 1px;
            width: 664px;
        }
        -- ></style>
</head>
<body language="javascript" leftmargin="0" topmargin="0" onload="return window_onload()">
    <form id="form1" method="post">
    <%NewsFunction f = new NewsFunction();%>
    <table width="664" align="center" border="0" cellspacing="2">
        <tr>
            <td bgcolor="#c3d5df" height="85">
                <p align="left">
                    <img src="../Images/zfTitle.jpg" border="0"></p>
            </td>
        </tr>
    </table>
    <table id="Table2" style="width: 664px" width="664" align="center">
        <tr>
            <td>
                <table cellspacing="1" cellpadding="0" border="0">
                    <tr>
                        <td style="width: 101px" nowrap width="101">
                            信息全文检索
                        </td>
                        <td nowrap width="232">
                            <input type="text" size="14" name="fulltext" value="<%=f.disHtml(fulltext)%>">
                            <select name="selType">
                                <option value="title" <%if(selType=="title"){Response.Write("Selected");}%>>标题</option>
                                <option value="content" <%if(selType=="content"){Response.Write("Selected");}%>>全文</option>
                                <option value="dateandtime" <%if(selType=="dateandtime"){Response.Write("Selected");}%>>
                                    日期</option>
                                <option value="writer" <%if(selType=="writer"){Response.Write("Selected");}%>>作者</option>
                            </select>
                            <input type="submit" value="检索">
                        </td>
                        <td nowrap width="116">
                            <a href="/OA/news/">信息发布管理</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table1" style="width: 664px; height: 112px" height="116" cellspacing="1"
        cellpadding="1" width="664" align="center" border="0" class="tdBlack1px">
        <tr>
            <td height="16" colspan="5">
                <div align="right">
                    <%if (iCount > 0)
                      {
                          if (iRealBegins > 1)
                          {
                              Response.Write("<a href=\"javascript:doPost(" + (iRealBegins - 1).ToString() + ")\"> &lt;&lt;&lt;</a>");
                          }
                    %>
                    第
                    <select id="selPage" name="selPage" language="javascript" onchange="return selPage_onchange()">
                        <%
                            int i = 0;
                            for (i = 1; i <= Convert.ToInt32((iCount - 1) / iPageSize) + 1; i++)
                            {
                                Response.Write("<OPTION ");
                                if (i == iRealBegins)
                                { Response.Write("selected"); }
                                Response.Write(" value=\"" + i.ToString() + "\">" + i.ToString());
                                Response.Write("</OPTION>");
                            }
						
                        %>
                    </select>
                    页/共有<font color="red"><%=(Convert.ToInt32((iCount-1)/iPageSize)+1).ToString()%></font>页
                    <%
                        if (iRealBegins < Convert.ToInt32((iCount - 1) / iPageSize) + 1)
                        {
                            Response.Write("<a href=\"javascript:doPost(" + (iRealBegins + 1).ToString() + ")\"> &gt;&gt;&gt;</a>");

                        }
                }
                    %>
                </div>
            </td>
    </form>
    </tr>
    <tr bgcolor="#aadfaa">
        <td height="16" width="36" nowrap align="center">
            序号
        </td>
        <td width="343" height="16" align="center">
            标题
        </td>
        <td height="16" width="60" nowrap align="center">
            作者
        </td>
        <td height="16" width="121" nowrap align="center">
            日期
        </td>
        <td height="16" width="74" nowrap align="center">
            附件下载
        </td>
    </tr>
    <%
        if (iCount == 0)
        {
            Response.Write("<tr><td nowrap colspan=\"5\">检索结果如下:<br>");
            if (err == "")
            {
                err = "没有检索到\"" + f.disHtml(fulltext) + "\"";
            }
            err = f.addColor(err, "red");

            Response.Write(err + "</td></tr>");
            Response.End();
        }
    %>
    <%
        for (int i = 0; i < dt.Rows.Count; i++)
        {
    %>
    <tr id="tr<%=dt.Rows[i]["id"].ToString()%>" bgcolor="#d4dfd4" onmouseover="javascript:this.bgColor='#aadfaa'"
        onmouseout="javascript:this.bgColor='#d4dfd4'" onclick="javascript:window.open('eachnews.aspx?id=<%=dt.Rows[i]["id"].ToString()%>','_self')">
        <td height="22">
            <%=(iCount-(iRealBegins-1)*iPageSize-i).ToString()%>
        </td>
        <td height="22" nowrap>
            <a href="eachnews.aspx?id=<%=dt.Rows[i]["id"].ToString()%>" target="_top">
                <%=dt.Rows[i]["title"].ToString()%></a>
        </td>
        <td nowrap height="22">
            <%=dt.Rows[i]["writer"].ToString()%>
        </td>
        <td nowrap height="22">
            <%=dt.Rows[i]["dateandtime"].ToString()%>
        </td>
        <td nowrap height="22">
            <%=dt.Rows[i]["dateandtime"].ToString()%>
        </td>
    </tr>
    <%if (isFullText)
      {%>
    <tr>
        <td>
        </td>
        <td colspan="4" bgcolor="#ecf5eb">
            <div>
                <blockquote>
                    <%
                        //string myContent=f.disHtml(dt.Rows[i]["content"].ToString());
                        string myContent = dt.Rows[i]["content"].ToString();
                        myContent = f.FullTextDisplay(myContent, myFulls, "RED");
                        Response.Write(myContent);%></blockquote>
            </div>
        </td>
    </tr>
    <%}%>
    <% }%>
    </table>
</body>
</html>
