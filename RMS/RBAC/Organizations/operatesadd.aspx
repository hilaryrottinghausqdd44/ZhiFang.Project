<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.operatesadd" Codebehind="operatesadd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>���ְλ��Ϣ</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../Include/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		
    </script>

</head>
<body leftmargin="0" topmargin="0" background="../../Images/vdisk/images/vdisk-bg.gif"
    rightmargin="0" bottommargin="0">
    <form name="Form1" method="post" action="postsadd.aspx" id="Form1" onsubmit="return CheckForm()">
    &nbsp;
    <table border="0" width="100%" align="center" cellspacing="0">
        <tr height="50">
            <td width="1%" nowrap>
                &nbsp;&nbsp;&nbsp;&nbsp;<img src="../../images/icons/0041_a.gif" align="absBottom">
            </td>
            <td>
                <font color="highlight" size="2"><b>��Ӳ�����ʽ</b></font>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" width="20%">
                <font color="#660000">�������</font>
            </td>
            <td width="80%">
                <asp:TextBox ID="SN" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="20%" align="center" style="height: 24px">
                <font color="#660000"><font color="#660000">����</font>����</font>
            </td>
            <td width="80%" style="height: 24px">
                <p>
                    <asp:TextBox ID="CName" runat="server" Width="192px"></asp:TextBox></p>
            </td>
        </tr>
        <tr>
            <td align="center" width="20%" style="height: 23px">
                <font color="#660000">����Ӣ����</font>
            </td>
            <td width="80%" style="height: 23px">
                <asp:TextBox ID="EName" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" width="20%">
                <font face="����" color="#990033"><font color="#660000">�������</font></font>
            </td>
            <td width="80%">
                <asp:TextBox ID="SName" runat="server" Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" width="20%" style="height: 30px">
                <font face="����"><font face="����" color="#990033"><font color="#660000">��������</font></font></font>
            </td>
            <td width="80%" style="height: 30px">
                <asp:DropDownList ID="Type" runat="server">
                    <asp:ListItem Value="-1">ϵͳ����</asp:ListItem>
                    <asp:ListItem Value="0">ϵͳ���ܲ���</asp:ListItem>
                    <asp:ListItem Value="1">�Զ������</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <font color="#660000"><font color="#660000"><font face="����"><font face="����" color="#990033">
                    <font color="#660000">��������</font></font></font></font></font>
            </td>
            <td>
                <asp:TextBox ID="Descr" runat="server" Width="312px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr height="10">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr height="40">
            <td colspan="2" align="center" valign="middle">
                <asp:Button ID="Button1" runat="server" Text=" ȷ �� " OnClick="Button1_Click"></asp:Button>&nbsp;
                <asp:Button ID="Button2" runat="server" Text=" ȡ �� " OnClick="window.close();"></asp:Button>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
