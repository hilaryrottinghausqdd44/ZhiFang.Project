<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.postsadd" Codebehind="postsadd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>���/�޸ĸ�λ��Ϣ</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Include/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		function CheckForm()
		{
			
			if(Form1.CName.value=='')
			{
				alert("���Ʋ���Ϊ�գ�");
				return false;
			}
			
		}
    </script>

</head>
<body bottommargin="0" leftmargin="0" background="../../Images/vdisk/images/vdisk-bg.gif"
    topmargin="0" rightmargin="0">
    <form id="Form1" name="Form1" onsubmit="return CheckForm()"
    method="post" runat="server">
    <table style="width: 268px; height: 291px" cellspacing="0" width="268" align="center"
        border="0">
        <tr height="50">
            <td style="width: 84px" nowrap width="84">
                <p align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;<img src="../../images/icons/0041_a.gif" align="absBottom">
                </p>
            </td>
            <td>
                <font color="highlight" size="2"><b>���/�޸ĸ�λ��Ϣ</b></font>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 84px" align="center" width="84">
                <p align="right">
                    <font color="#660000">��λ���</font></p>
            </td>
            <td width="80%">
                <asp:TextBox ID="SN" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 84px; height: 24px" align="center" width="84">
                <p align="right">
                    <font color="#660000"><font color="#660000">��λ</font>����</font></p>
            </td>
            <td style="height: 24px" width="80%">
                <p>
                    <asp:TextBox ID="CName" runat="server" Width="192px"></asp:TextBox></p>
            </td>
        </tr>
        <tr>
            <td style="width: 84px" align="center" width="84">
                <p align="right">
                    <font color="#660000"><font color="#660000">��λ</font>Ӣ����</font></p>
            </td>
            <td width="80%">
                <asp:TextBox ID="EName" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 84px" align="center" width="84">
                <p align="right">
                    <font face="����" color="#990033"><font color="#660000"><font color="#660000">��λ</font>���</font></font></p>
            </td>
            <td width="80%">
                <asp:TextBox ID="SName" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 84px; height: 55px" align="center">
                <p align="right">
                    <font color="#660000"><font color="#660000">��λ</font>˵��</font></p>
            </td>
            <td style="height: 55px">
                <asp:TextBox ID="Descr" runat="server" Width="192px" TextMode="MultiLine" Height="48px"></asp:TextBox>
            </td>
        </tr>
        <tr height="10">
            <td colspan="2">
                &nbsp;<font face="����">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font>
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr height="40">
            <td colspan="2" align="center" valign="middle">
                <asp:Button ID="Button1" runat="server" Text=" ȷ �� " OnClick="Button1_Click"></asp:Button>&nbsp;
                <input id="cancel" type="button" value=" ȡ �� " onclick="window.close();">
                <asp:CheckBox ID="UnClose" runat="server" Text="������ɺ��Զ��رմ���" Checked="True"></asp:CheckBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
