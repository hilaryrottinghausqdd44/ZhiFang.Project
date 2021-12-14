<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.News.Main" Codebehind="Main.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>
        -主页面</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link rel="stylesheet" type="text/css" href="Main.css">

    <script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() {
	var obj1=frames["ifrmbody1"].frameElement;
	var obj2=frames["ifrmbody2"].frameElement;
	var tableBody=document.getElementById("tablebody");
	obj1.height=400;
	obj2.height=400;
	
}
function AddHeight(iHeight){
	if (iHeight==null)
	{
		iHeight=50
	}
	var size=300;
	var obj1=frames["ifrmbody1"].frameElement;
	var obj2=frames["ifrmbody2"].frameElement;
	var tableBody=document.getElementById("tablebody");
	
	
	var height = parseInt(obj1.offsetHeight)
	obj1.height=height + iHeight;
	obj2.height=height + iHeight;
	tableBody.height=height + iHeight;
	
}
//-->
    </script>

</head>
<body language="javascript" leftmargin="0" topmargin="15" onload="return window_onload()">
    <font face=""></font>
    <table width="664" align="center" border="0" cellspacing="2">
    </table>
    <table id="Table2" style="width: 664px" align="center" border="0" bordercolor="#ff6666"
        cellpadding="0" cellspacing="0">
        <tr>
            <td valign="bottom">
                <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                    <form id="form1" method="post" action="browse/FullText.aspx" target="_blank">
                    <tbody>
                        <tr>
                            <td style="width: 90px" nowrap>
                                &nbsp;信息全文检索
                            </td>
                            <td nowrap width="232">
                                <input type="text" size="14" name="fulltext">
                                <select name="selType">
                                    <option value="title" selected>标题</option>
                                    <option value="content">全文</option>
                                    <option value="dateandtime">日期</option>
                                    <option value="writer">作者</option>
                                    <option value="keyword">关键字</option>
                                </select>
                                <input type="submit" value="检索">
                            </td>
                            <td nowrap width="86" align="center">
                                <a href="news/">信息发布管理</a>
                            </td>
                            <td nowrap width="86" align="center">
                                <a href="Config/SendEmail.aspx">推荐本页</a>
                            </td>
                            <td nowrap width="86" align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </form>
                </table>
            </td>
        </tr>
        </TBODY></table>
    <table id="tableBody" cellspacing="0" width="664" align="center" border="0" style="width: 664px">
        <tr>
            <td width="332">
                <iframe id="ifrmbody1" src="body.aspx?col=1" frameborder="0" width="100%" scrolling="no"
                    height="1500"></iframe>
            </td>
            <td width="332">
                <iframe id="ifrmbody2" src="body.aspx?col=2" frameborder="0" width="100%" scrolling="no"
                    height="1500"></iframe>
            </td>
        </tr>
        <tr height="30">
            <td valign="middle" align="right" bgcolor="#edfefb">
                增加版面&gt;&gt;
                <img style="cursor: hand" onclick="javascript:AddHeight(100)" src="Images/SizePlus.gif">
            </td>
            <td align="left" bgcolor="#edfefb">
                <img style="cursor: hand" onclick="javascript:AddHeight(-50)" src="Images/SizeMinus.gif">&lt;&lt;减少版面
            </td>
        </tr>
    </table>
</body>
</html>
