<%@ Page Language="c#" AutoEventWireup="True" Inherits="theNews.Config.SendEmail" Codebehind="SendEmail.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�����ѷ��͵����ʼ�</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Main.css" type="text/css" rel="stylesheet">
</head>
<body topmargin="1">
    <form id="Form1" method="post" runat="server">
    <table width="664" align="center" border="0" cellspacing="0">
        <tr>
            <td bgcolor="#c3d5df">
                <img src="../Images/zfTitle.jpg" border="1">
            </td>
        </tr>
    </table>
    <table class="tdBlack1px" id="Table2" width="333" align="center" border="0">
        <tr>
            <td style="height: 24px" height="24" nowrap>
                ����email
            </td>
            <td style="height: 24px">
                &nbsp;<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>&nbsp;����,
            </td>
            <td width="49" rowspan="3">
                <img src="images/commend.gif">
            </td>
        </tr>
        <tr>
            <td width="48" height="34">
                ����
            </td>
            <td width="249">
                &nbsp;<asp:TextBox ID="txtCommender" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap height="23">
                �����Ƽ�
            </td>
            <td>
                <a href="<%=sUrl%>"><font color="#0000ff">
                    <%=title%></font></a>
            </td>
        </tr>
        <tr>
            <td style="height: 83px" nowrap>
                ����˵��
            </td>
            <td style="height: 83px" colspan="2">
                <font face="����">&nbsp;<asp:TextBox ID="txtPS" runat="server" TextMode="MultiLine"
                    Height="71px" Width="256px"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td nowrap height="49">
                &nbsp;
            </td>
            <td colspan="2">
                &nbsp;
                <asp:Button ID="buttSend" runat="server" Text="�����Ƽ�����"></asp:Button>
                <img src="images/sendMail.gif">
                <a href="../">����</a>
            </td>
        </tr>
        <tr>
            <td valign="top" nowrap colspan="3" height="22" bgcolor="#ffe7e6">
                <font color="red">
                    <%=err%>
                </font>
            </td>
        </tr>
    </table>
    <p align="center">
        <a href="ConfigEmail.aspx">�ʼ�����������</a></p>
    </form>
</body>
</html>
