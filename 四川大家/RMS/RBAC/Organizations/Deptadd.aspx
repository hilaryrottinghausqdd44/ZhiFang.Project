<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.Deptadd" Codebehind="Deptadd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>����»���</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta http-equiv="Content-Language" content="zh-cn">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		function IsValidate()
		{	
			if(AddDept.CName.value.length==0)
			{alert('����д���ƣ�');
			return false;
			}		
			//return true	
		}
		function ChkClose()
		{
			if(AddDept.UnClose.checked)
				AddDept.UnClose.value='yes';
			else
				AddDept.UnClose.value='no';
		}
    </script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="AddDept" name="AddDept" onsubmit="return IsValidate()"
    method="post" runat="server">
    <table cellspacing="0" width="100%" align="center" border="0">
        <tr bgcolor="lightgrey" height="50">
            <td colspan="4">
                &nbsp;&nbsp;<b>����»���</b>
            </td>
        </tr>
    </table>
    <table align="center" border="0" style="width: 450px">
        <tr bgcolor="white">
            <td width="87" align="right" bgcolor="#f0f0f0">
                <font color="red">*</font>�������ƣ�
            </td>
            <td width="115">
                <asp:TextBox ID="CName" Width="112px" runat="server"></asp:TextBox>
            </td>
            <td width="85" align="right" bgcolor="#f0f0f0">
                ������ƣ�
            </td>
            <td width="145">
                <asp:TextBox ID="SName" Width="104px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f0f0f0">
                Ӣ�����ƣ�
            </td>
            <td>
                <font face="����">
                    <asp:TextBox ID="EName" Width="112px" runat="server"></asp:TextBox></font>
            </td>
            <td bgcolor="#f0f0f0" nowrap>
                <p align="right">
                    ������ϵ�ˣ�</p>
            </td>
            <td align="right">
                <font face="����">
                    <p align="left">
                        <asp:TextBox ID="Contact" runat="server" Width="104px"></asp:TextBox></p>
                </font>
            </td>
        </tr>
        <tr bgcolor="white">
            <td align="right" bgcolor="#f0f0f0">
                ����������
            </td>
            <td colspan="3">
                <asp:TextBox ID="Descr" Width="312px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr bgcolor="white">
            <td align="right" bgcolor="#f0f0f0" style="width: 85px">
                ���ŵ绰��
            </td>
            <td>
                <asp:TextBox ID="Tel" Width="112px" runat="server"></asp:TextBox>
            </td>
            <td align="right" bgcolor="#f0f0f0">
                ���Ŵ��棺
            </td>
            <td>
                <asp:TextBox ID="Fax" Width="104px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr bgcolor="white">
            <td align="right" bgcolor="#f0f0f0" style="width: 85px">
                �������룺
            </td>
            <td>
                <asp:TextBox ID="Zip" Width="112px" runat="server"></asp:TextBox>
            </td>
            <td align="right" bgcolor="#f0f0f0">
                ���ŵ�ַ��
            </td>
            <td>
                <asp:TextBox ID="Addr" Width="104px" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table height="100" cellspacing="0" width="100%" border="0">
        <tr bgcolor="lightgrey" height="30">
            <td valign="top" align="center" colspan="4">
                <font face="����">&nbsp;</font><br>
                <asp:Button ID="Button1" runat="server" Text="ȷ  ��" OnClick="Button1_Click"></asp:Button>&nbsp;
                <input id="cancel" type="button" value="�رմ���" onclick="window.close();" style="width: 68px;
                    height: 22px">
                <asp:CheckBox ID="UnClose" runat="server" Text="������ɺ��Զ��رմ���" Checked="True"></asp:CheckBox>
                <p>
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></p>
            </td>
        </tr>
    </table>
    </form>

    <script language="javascript">
		
    </script>

</body>
</html>
