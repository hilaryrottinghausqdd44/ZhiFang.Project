<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.ModulePublish" Codebehind="ModulePublish.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ModulePublish</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <style>
        .text
        {
            font-size: 13px;
            color: #000000;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <table style="border-collapse: collapse" bordercolor="#93bee2" cellspacing="0" cellpadding="0"
            width="75%" border="1" align="center" class="text">
            <tr>
                <td width="15%">
                    id号
                </td>
                <td width="28%">
                    模块名称
                </td>
                <td>
                    链接地址
                </td>
            </tr>
            <%for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
              {%>
            <tr>
                <td>
                    <%=ds.Tables[0].Rows[i]["id"].ToString()%>
                </td>
                <td>
                    <%=ds.Tables[0].Rows[i]["CName"].ToString()%>
                </td>
                <td>
                    <%=ds.Tables[0].Rows[i]["URL"].ToString()%>
                </td>
            </tr>
            <%}%>
        </table>
    </font>
    </form>
</body>
</html>
