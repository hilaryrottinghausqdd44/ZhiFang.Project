<%@ Page Language="c#" AutoEventWireup="True" Inherits="test.WebForm1" Codebehind="WebForm1.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>WebForm1</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <font face="ËÎÌו">
        <asp:Button ID="Button1" Style="z-index: 100; left: 200px; position: absolute; top: 112px"
            runat="server" Text="Button"></asp:Button>
        <asp:TextBox ID="TextBox3" Style="z-index: 106; left: 344px; position: absolute;
            top: 192px" onclick="javascript:window.dateField &#9;= TextBox1 &#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;var calendar &#9;= window.open('calendar.asp','cal1','width=250,height=200,top=300,left=440')&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar.focus()&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar= ''"
            runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" Style="z-index: 105; left: 344px; position: absolute;
            top: 152px" onclick="javascript:window.dateField &#9;= TextBox1 &#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;var calendar &#9;= window.open('calendar.asp','cal1','width=250,height=200,top=300,left=440')&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar.focus()&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar= ''"
            runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox1" Style="z-index: 104; left: 344px; position: absolute;
            top: 112px" onclick="javascript:window.dateField &#9;= TextBox1 &#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;var calendar &#9;= window.open('calendar.asp','cal1','width=250,height=200,top=300,left=440')&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar.focus()&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;calendar= ''"
            runat="server"></asp:TextBox>
        <asp:Button ID="Button2" Style="z-index: 102; left: 392px; position: absolute; top: 344px"
            runat="server" Text="Button" OnClick="Button2_Click"></asp:Button>
        <input id="File1" style="z-index: 101; left: 344px; position: absolute; top: 280px"
            type="file" name="File1" runat="server">
    </font>
    </form>
</body>
</html>
