<%@ Import Namespace="System.Web" %>

<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Browse.addComments" Codebehind="addComments.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>addComments</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../Main.css">

    <script id="clientEventHandlersJS" language="javascript">
<!--

function Submit_onclick() {
	if(Form1.content.value=="")//||Form1.content.value.trim=="")
	{
		window.alert("评论内容不能为空");
		return;
	}
	else
	{
		Form1.submit();
	}
}

function buttView_onclick() {
	window.open("Comments.aspx?id=<%=id%>","_black")
}

function window_onload() {
	if("<%=Request.ServerVariables["REQUEST_METHOD"].ToString()%>"=="POST")
	{
		top.window.location.reload();
	}
}

function buttSend_onclick() {
	var str=Form2.email.value;
	str=str.replace(" ","");
	str=str.replace("　","");
	if (str.indexOf("\@")<0||str.indexOf(".")<0||str.Length<6)
	{
		str="";
	}
	if(str=="")
	{
		window.alert("不是有效的电子邮件地");
		Form2.email.select();
		return;
	}
	Form2.submit();
	
}

//-->
    </script>

</head>
<body ms_positioning="GridLayout" language="javascript" bgcolor="#eefff9" onload="return window_onload()">
    <font face="宋体">
        <table class="" id="Table1" style="z-index: 101; left: 8px; position: absolute; top: 8px"
            width="640" align="center" border="0">
            <tr>
                <td>
                    <table id="Table2" width="95%" border="0">
                        <form id="Form1" method="post" runat="server">
                        <tbody>
                            <tr>
                                <td width="9%">
                                    评论人<input type="hidden" name="id" value="<%=id%>">
                                </td>
                                <td width="43%">
                                    <input type="text" size="41" value="匿名" name="username">
                                </td>
                                <td nowrap width="48%">
                                    可以不输入评论姓名，按匿名处理
                                </td>
                            </tr>
                            <tr>
                                <td nowrap>
                                    评论内容
                                </td>
                                <td>
                                    <font face="宋体">
                                        <textarea name="content" rows="5" cols="40"></textarea>
                                    </font>
                                </td>
                                <td valign="top">
                                    <p>
                                        * 评论内容,请不要发表现行法律禁止的内宾</p>
                                    <p>
                                        * 智方信息发布</p>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap>
                                    提交
                                </td>
                                <td>
                                    <input type="button" value="发表评论" name="Submit" language="javascript" onclick="return Submit_onclick()">&nbsp;&nbsp;
                                    已经存在评论数（<font color="#ff3366"><%=commentsCount%></font>）条<input type="button" name="buttView"
                                        value="查看" language="javascript" onclick="return buttView_onclick()">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </form>
                        <tr>
                            <td nowrap colspan="2" style="border-top: #e3e3d5 1px solid" valign="middle">
                                <br>
                                <form id="Form2" method="post" action="../Config/SendEmail.aspx?id=<%=id%>" target="_blank">
                                推荐给朋友&nbsp;Email:<input type="text" name="email" id="email">&nbsp;推荐人:<input style="width: 72px;
                                    height: 22px" type="text" size="6" name="commender">
                                </form>
                            </td>
                            <td valign="middle">
                                <input type="button" name="buttSend" id="buttSend" value="发送" language="javascript"
                                    onclick="return buttSend_onclick()">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </TBODY>
        </table>
    </font>
</body>
</html>
