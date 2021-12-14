<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.positionadd" Codebehind="positionadd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>添加/修改职位信息</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Include/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		
		function CheckForm()
		{
			if (Form1.CName.value == '')
			{
				alert ('请输入职位名称。');
				Form1.CName.focus();
				return false;
			}
			
			
		}
    </script>

</head>
<body bottommargin="0" leftmargin="0" background="../../Images/vdisk/images/vdisk-bg.gif"
    topmargin="0" rightmargin="0">
    <form id="Form1" name="Form1" onsubmit="return CheckForm()"
    method="post" runat="server">
    &nbsp;
    <table style="width: 383px; height: 304px" cellspacing="0" width="383" align="center"
        border="0">
        <tr height="50">
            <td style="width: 146px" nowrap width="146">
                &nbsp;&nbsp;&nbsp;&nbsp;<img src="../../images/icons/0041_a.gif" align="absBottom">
            </td>
            <td>
                <font color="highlight" size="2"><b>添加/修改职位信息</b></font>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 146px" align="center" width="146">
                <font color="#660000">职位编号</font>
            </td>
            <td width="80%">
                <asp:TextBox ID="SN" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 146px" align="center" width="146">
                <font color="#660000">职位名称</font>
            </td>
            <td width="80%">
                <p>
                    <asp:TextBox ID="CName" runat="server" Width="192px"></asp:TextBox></p>
            </td>
        </tr>
        <tr>
            <td style="width: 146px" align="center" width="146">
                <font color="#660000">职位英文名</font>
            </td>
            <td width="80%">
                <asp:TextBox ID="EName" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 146px; height: 21px" align="center" width="146">
                <font color="#660000">职位级别</font>
            </td>
            <td width="80%" style="height: 21px">
                <asp:DropDownList ID="Grade" runat="server" Width="96px">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 146px" align="center" width="146">
                <font face="宋体" color="#990033"><font color="#660000">职位简称</font></font>
            </td>
            <td width="80%">
                <asp:TextBox ID="SName" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 146px; height: 33px" align="center">
                <font color="#660000">说明</font>
            </td>
            <td style="height: 33px">
                <asp:TextBox ID="Descr" runat="server" Width="192px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr height="10">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr height="40">
            <td valign="middle" align="center" colspan="2">
                <p>
                    <asp:Button ID="Button1" runat="server" Text=" 确 定 " OnClick="Button1_Click"></asp:Button>&nbsp;
                    <input id="cancel" onclick="window.close();" type="button" value="关闭窗口">
                </p>
                <p>
                    <asp:CheckBox ID="UnClose" runat="server" Text="操作完成后，自动关闭窗口" Checked="True"></asp:CheckBox>
                    <asp:Label ID="Label1" runat="server"></asp:Label></p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
