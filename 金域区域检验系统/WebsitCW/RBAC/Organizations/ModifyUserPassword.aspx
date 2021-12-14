<%@ Page Language="c#" AutoEventWireup="True"
    Inherits="OA.RBAC.Organizations.ModifyPwd" Codebehind="ModifyUserPassword.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>修改密码</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		function ValidForm()
		{			
			if (ModifyUserPassword.userpwd.value != ModifyUserPassword.userpwd1.value)
			{
				alert ('您两次输入的密码不一致，请重新输入。');
				ModifyUserPassword.userpwd1.focus();
				return false;
			}
			return true;
		}
    </script>

</head>
<body ms_positioning="GridLayout" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0">
    <form id="ModifyUserPassword" name="ModifyUserPassword" method="post" runat="server"
    onsubmit="return ValidForm()">
    <font face="宋体"></font>
    <br>
    <br>
    <br>
    <table border="0" cellspacing="1" width="200" bgcolor="lightgrey" align="center"
        style="z-index: 101; left: 8px; position: absolute; top: 8px">
        <tr bgcolor="white" height="25">
            <td nowrap width="1%" bgcolor="#f0f0f0">
                &nbsp;新密码：
            </td>
            <td>
                <asp:TextBox ID="userpwd" runat="server" Width="142px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr bgcolor="white" height="25">
            <td nowrap width="1%" bgcolor="#f0f0f0">
                &nbsp;重复新密码：
            </td>
            <td>
                <asp:TextBox ID="userpwd1" runat="server" Width="142px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr bgcolor="white" height="50">
            <td colspan="2" align="center">
                <p>
                    &nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 更 改 " Width="50px" OnClick="Button1_Click">
                    </asp:Button>&nbsp;<input type="button" value="关 闭 " onclick="window.close();" style="width: 41px;
                        height: 24px">&nbsp;
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
