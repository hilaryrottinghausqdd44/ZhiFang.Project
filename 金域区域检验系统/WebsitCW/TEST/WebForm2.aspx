<%@ Page Language="c#" AutoEventWireup="True" Inherits="test.WebForm2" Codebehind="WebForm2.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>WebForm2</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <td width="214" bgcolor="#faf1f3">
        <asp:TextBox ID="Birthday" runat="server" BackColor="Silver" contentEditable="false"></asp:TextBox>
        <img id="imgBirthday" onclick="javascript:window.dateField &#9;= Birthday &#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;var calendar &#9;= window.open('calendar.asp','cal1','width=250,height=200,top=300,left=440')&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar.focus()&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar= ''"
            src="../images/date.gif" runat="server"><font color="red">*</font>
    </td>
    <td class="zi2" align="center" width="106" bgcolor="#f3e2e8">
        ×¢²áºÅ
    </td>
    <input type="button" value="È·¶¨" onclick="add()">
    </form>

    <script>
		function add()
		{
			alert('ok');
			alert(test.WebForm2.ServerSideAdd(3,5).value);
		}
    </script>

</body>
</html>
