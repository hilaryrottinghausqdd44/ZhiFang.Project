<%@ Register TagPrefix="cc1" Namespace="com.unicafe.WebControls.Calendar" Assembly="Calendar" %>

<%@ Page Language="c#" CodeBehind="calendar.aspx.cs" AutoEventWireup="false" Inherits="OA.library.desktop.calendar" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>i_DateSelector</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta http-equiv="Content-Language" content="zh-cn">
    <link href="/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			function calendar_onclick(y,m,d)
			{
			}
			function calendar_navigate(y,m,d)
			{
				calendar_onclick (y,m,d);
				location = 'calendar.aspx?y=' + y + '&m=' + m + '&d=' + d;
			}
    </script>

</head>
<body topmargin="0" leftmargin="0" bgcolor="#dddddd">
    <table border="0" cellspacing="0" cellpadding="0" align="center" width="1%">
        <tr>
            <td align="middle">
                <cc1:calendar id="Calendar1" runat="server" BackColor="White" TitleColor="Lavender">
                </cc1:calendar>
            </td>
        </tr>
    </table>
</body>
</html>
