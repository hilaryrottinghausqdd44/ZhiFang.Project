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
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <table id="Table1" cellspacing="1" cellpadding="0" width="100%" bgcolor="#99cccc"
        border="0">
        <tr bgcolor="#eeeeea">
            <td align="center" width="30%">
                按钮名称
            </td>
            <td align="center" width="40%" colspan="2">
                参考名称
            </td>
            <td align="center" width="30%">
                按钮编号
            </td>
        </tr>
        <%for (int k = 0; k < ButDt.Rows.Count; k++)
          {%>
        <tr bgcolor="#ffffff">
            <td>
                <input id="butt<%=k%>" value="<%=ButDt.Rows[k]["OperateName"]%>" size="15" title="<%=ButDt.Rows[k]["OperateID"]%>">
            </td>
            <td align="center">
                <%=ButDt.Rows[k]["CName"]%>
            </td>
            <td align="right" style="padding-right: 10px; padding-left: 13px; filter: progid:DXImageTransform.Microsoft.Alpha( 
						style=1,opacity=0,finishOpacity=80,startX=0,finishX=100,startY=100,finishY=0);
                width: 25px; background-color: <%=ButDt.Rows[k]["OperateColor"]%>">
                &nbsp;
            </td>
            <td align="center">
                <%=ButDt.Rows[k]["SN"]%>
            </td>
        </tr>
        <%}%>
    </table>
    <input type="hidden" id="hButtons" value="<%=ButDt.Rows.Count%>">
</body>
</html>
