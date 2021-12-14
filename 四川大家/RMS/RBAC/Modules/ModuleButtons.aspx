<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.ModuleButtons" Codebehind="ModuleButtons.aspx.cs" %>

<%%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>模块的按钮</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onload() {
            if(parent)
                parent.document.all("tdFrmButtons").style.height = document.body.scrollHeight + 20; 
        }
        function deleteButt(buttID,TemName) {
            if(confirm("确定要删除按钮吗？"))
                document.location.href ="ModuleButtons.aspx?buttTempName=" + TemName + "&buttDelete=" + buttID;
        }
// ]]>
    </script>
    <style type="text/css">
        .style1
        {
            cursor:hand;
        }
        
    </style>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0"  bgcolor="#f0f0f0" rightmargin="0" onload="return window_onload()">
    <table id="Table1" cellspacing="1" cellpadding="0" width="100%" bgcolor="#99cccc"
        border="0">
        <tr bgcolor="#eeeeea">
            <td align="center" width="20%">
                按钮名称
            </td>
            <td align="center" width="20%">
                按钮编号
            </td>
            <td align="center" width="80%">
                说明
            </td>
        </tr>
        <%for (int k = 0; k < ButDt.Rows.Count; k++)
          {%>
        <tr bgcolor="#ffffff">
            <td nowrap="nowrap">
                 <img src="../../Images/icons/0014_b.gif" style="cursor:hand" onclick="Javascript:deleteButt(<%=ButDt.Rows[k]["ID"]%>,'<%=buttTempName%>')" />
                 <input id="h<%=k %>" type=hidden value="<%=ButDt.Rows[k]["ID"]%>"/>
                <input id="butt<%=k%>" value="<%=ButDt.Rows[k]["OperateName"]%>" size="12" title="<%=ButDt.Rows[k]["OperateID"]%>">
            </td>
            
            <td align="center" nowrap="nowrap">
                <input id="buttCode<%=k%>" value="<%=ButDt.Rows[k]["OperateCode"]%>" size="13">
            </td>
            <td align="center" nowrap="nowrap">
                <input id="buttDesc<%=k%>" value="<%=ButDt.Rows[k]["OperateDesc"]%>" size="25">
            </td>
        </tr>
        <%}%>
    </table>
    <input type="hidden" id="hButtons" value="<%=ButDt.Rows.Count%>">
</body>
</html>
