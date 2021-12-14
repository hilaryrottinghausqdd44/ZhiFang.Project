<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.News.publish.AtatchFiles" Codebehind="AtatchFiles.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AtatchFiles</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="StyleSheet.css" rel="stylesheet">

    <script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() {

}

function msgbox(str)
{
	window.alert(str);
}

function checkFile()
{
	var file=document.all["FindFile"];
	if(file!=null)
	{
		var but=document.all["AddFile"];
			
		if(file.value=="")
			but.disabled=true;
		else
			but.disabled=false;
	}
}
//-->
    </script>

</head>
<body language="javascript" text="#ff6633" onload="return window_onload()" bottommargin="0"
    leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Attachme" method="post" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="300" border="0">
        <tr>
            <td height="34">
                <input class="bluebutton" id="FindFile" title="FindFile" style="width: 288px; height: 22px"
                    type="file" size="28" name="FindFile" runat="server" onpropertychange="javascript:checkFile()">
            </td>
            <td align="center">
                提示信息:<br>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" CssClass="txtbox" Height="104" Width="288px"
                    Font-Size="XX-Small" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged">
                </asp:ListBox>
            </td>
            <td rowspan="3" valign="top">
                <asp:Label ID="Label1" runat="server" Height="96px" Width="224px" BorderColor="#FF8080"
                    BorderStyle="Solid" BorderWidth="1px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="AddFile" runat="server" CssClass="bluebutton" Height="23px" Width="75"
                    Text="添加" Enabled="False" OnClick="AddFile_Click"></asp:Button>&nbsp;&nbsp;
                <asp:Button ID="RemvFile" runat="server" CssClass="bluebutton" Height="23px" Width="75"
                    Text="删除" Enabled="False" OnClick="RemvFile_Click"></asp:Button>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
