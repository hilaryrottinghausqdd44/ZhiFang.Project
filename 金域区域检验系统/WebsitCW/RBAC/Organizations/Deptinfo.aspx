<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.Deptinfo" Codebehind="Deptinfo.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�鿴������Ϣ</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">

    <script language="javascript">
		function IsValidate()
		{	
			if(AddDept.CName.value.length==0)
			{alert('����д���ƣ�');
			return false;
			}		
			//return true	
		}
		
    </script>

    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="AddDept" name="AddDept" onsubmit="return IsValidate()"
    method="post" runat="server">
    <table cellspacing="0" width="100%" align="center" border="0">
        <tr bgcolor="lightgrey" height="50">
            <td colspan="4">
                &nbsp;&nbsp;<strong>������Ϣ</strong>
            </td>
        </tr>
    </table>
    <table width="418" border="0" align="center">
        <tr bgcolor="white">
            <td width="93" align="right" nowrap bgcolor="#f0f0f0">
                <font color="red">*</font>�������ƣ�
            </td>
            <td>
                <asp:TextBox ID="CName" Width="104px" runat="server"></asp:TextBox>
            </td>
            <td align="right" nowrap bgcolor="#f0f0f0">
                ������ƣ�
            </td>
            <td width="114">
                <asp:TextBox ID="SName" Width="112px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap bgcolor="#f0f0f0">
                Ӣ�����ƣ�
            </td>
            <td>
                <font face="����">
                    <asp:TextBox ID="EName" Width="104px" runat="server"></asp:TextBox></font>
            </td>
            <td nowrap bgcolor="#f0f0f0">
                <font face="����">&nbsp;</font>
            </td>
            <td align="right">
                <font face="����">
                    <p align="left">
                        <asp:TextBox ID="Contact" runat="server" Width="112px"></asp:TextBox>
                </font></P>
            </td>
        </tr>
        <tr bgcolor="white">
            <td align="right" nowrap bgcolor="#f0f0f0">
                ����������
            </td>
            <td colspan="3">
                <asp:TextBox ID="Descr" Width="320px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr bgcolor="white">
            <td align="right" nowrap bgcolor="#f0f0f0">
                ���ŵ绰��
            </td>
            <td>
                <asp:TextBox ID="Tel" Width="104px" runat="server"></asp:TextBox>
            </td>
            <td width="95" align="right" nowrap bgcolor="#f0f0f0">
                ���Ŵ��棺
            </td>
            <td>
                <asp:TextBox ID="Fax" Width="112px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr bgcolor="white">
            <td align="right" nowrap bgcolor="#f0f0f0">
                �������룺
            </td>
            <td width="98">
                <asp:TextBox ID="Zip" Width="104px" runat="server"></asp:TextBox>
            </td>
            <td align="right" nowrap bgcolor="#f0f0f0">
                ���ŵ�ַ��
            </td>
            <td>
                <asp:TextBox ID="Address" Width="112px" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table height="100" cellspacing="0" width="100%" border="0">
        <tr bgcolor="lightgrey" height="30">
            <td valign="top" align="center" colspan="4">
                <font face="����">&nbsp;</font><br>
                <asp:Button ID="Button1" runat="server" Text=" �� �� " OnClick="Button1_Click"></asp:Button>&nbsp;
                <input id="cancel" type="button" value="�رմ���" onclick="window.close();" style="width: 74px;
                    height: 22px">
                <asp:CheckBox ID="UnClose" runat="server" Text="������ɺ��Զ��رմ���" Checked="True"></asp:CheckBox>
                <p>
                    <asp:Label ID="Label1" runat="server"></asp:Label></p>
            </td>
        </tr>
    </table>
    </form>

    <script language="javascript">
		
    </script>

</body>
</html>
