<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.useradd" Codebehind="useradd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�û����</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Include/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		function IsValidate()
		{
			var MSG_NEED_username = "����д�����û�����";
			var MSG_NEED_userpwd = "�����������û����";
			var MSG_NEED_userpwd1 = "��������������벻һ�£����������롣";
			var MSG_NEED_person = "��ѡ���Ӧ��Ա����š�";
			
			if (newuser.username.value.length == 0)
				{
					alert (MSG_NEED_username);
					newuser.username.focus();
					return false;
				}
			if (newuser.userpwd)
			{
				if (newuser.userpwd.value.length == 0)
				{
					alert (MSG_NEED_userpwd);
					newuser.userpwd.focus();
					return false;
				}
				if (newuser.userpwd.value != newuser.userpwd1.value)
				{
					alert (MSG_NEED_userpwd1);
					newuser.userpwd1.focus();
					return false;
				}
			}
			if (newuser.username.value == "")
			{
				alert (MSG_NEED_person);
				newuser.person.focus();
				return false;
			}
			if(newuser.ChkAccExprd.checked==false)
			{
				if(newuser.TxtAccExpTm=="")
				{
					alert('����д����ʱ�䣡');
					return false;
				}
			}
		}
		function ShowAccTime()
		{
			if(document.all("TxtAccExpTm").style.display=="none")
				{
					
					document.all("TxtAccExpTm").style.display="";
					document.all("ChkPwdExprd1").style.display="inline";
				}
			else
				{
					document.all("TxtAccExpTm").style.display="none";
					document.all("ChkPwdExprd1").style.display="none";
				}
			
		}
		function ShowAccLock()
		{
			if(newuser.ChkAuUnlocka.style.display=="none")
			{
				newuser.ChkAuUnlocka.style.display="";
				newuser.ChkAuUnlockb.style.display="";				
				document.all("ChkAuUnlock1").style.display="inline"
				document.all("ChkAuUnlock2").style.display="inline"			
				document.all("SelLockedPeriod").style.display="inline"
				document.all("ChkAuUnlock3").style.display="inline"
				
			}
			else
			{
				newuser.ChkAuUnlocka.style.display="none";
				newuser.ChkAuUnlockb.style.display="none";
				document.all("ChkAuUnlock1").style.display="none"
				document.all("ChkAuUnlock2").style.display="none"			
				document.all("SelLockedPeriod").style.display="none"
				document.all("ChkAuUnlock3").style.display="none"
			}
		}
    </script>

</head>
<body bottommargin="0" leftmargin="0" background="../../Images/vdisk/images/vdisk-bg.gif"
    topmargin="0" rightmargin="0">
    <form id="newuser" name="newuser" onsubmit="return IsValidate()" method="post" runat="server">
    <table cellspacing="0" width="100%" align="center" border="0">
        <tr height="50">
            <td nowrap width="1%" colspan="2">
                &nbsp;&nbsp;&nbsp;<img src="../../Images/icons/0019_a.gif" align="absBottom">&nbsp;
                &nbsp;&nbsp;<font color="highlight" size="3"><b>������˺�</b></font>
                <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr height="30">
            <td colspan="2">
                <font face="����"></font>
            </td>
        </tr>
        <tr>
            <td style="height: 25px" nowrap align="right" width="8%">
                &nbsp;����������
            </td>
            <td style="height: 25px" width="25%">
                <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                <font face="����">�˺Ų����ظ���һ��Ա����Ӧһ���˺�</font>
                <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="10%">
                <font color="red">*</font>�˺�����
            </td>
            <td width="25%">
                <asp:TextBox ID="username" runat="server" Width="272px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 25px" nowrap align="right" width="10%">
                <font color="red">*</font>�˺ſ��
            </td>
            <td style="height: 25px" width="25%">
                <asp:TextBox ID="userpwd" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 22px" nowrap align="right" width="10%">
                <font color="red">*</font>����ȷ�ϣ�
            </td>
            <td style="height: 22px" width="25%">
                <asp:TextBox ID="userpwd1" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="10%">
                <font color="red">*</font>��ӦԱ����
            </td>
            <td width="25%">
                <input id="test1" style="width: 112px; height: 22px" disabled type="text" size="13"><a
                    href="javascript:ChooseEmpl()">ѡ��Ա��</a>
                <input id="userid" style="width: 24px; height: 22px" type="hidden" size="1" name="userid">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="10%">
                <font id="FONT1" face="����" runat="server">Ա��������</font>
            </td>
            <td width="25%">
                <asp:TextBox ID="UserDesc" runat="server" Width="272px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 44px" align="center" width="5%" colspan="2">
                <font face="����"></font><font face="����"></font><font face="����"></font><font face="����">
                </font><font face="����"></font><font face="����"></font><font face="����"></font><font
                    face="����"></font><font face="����"></font><font face="����"></font><font face="����">
                </font><font face="����"></font><font face="����"></font><font face="����"></font><font
                    face="����"></font><font face="����"></font><font face="����"></font><font face="����">
                </font><font face="����"></font><font face="����"></font><font face="����"></font><font
                    face="����"></font><font face="����"></font><font face="����"></font><font face="����">
                </font><font face="����"></font>
                <br>
                &nbsp;
                <asp:Button ID="addnew" runat="server" Text=" ȷ  �� " OnClick="addnew_Click"></asp:Button>
                <asp:CheckBox ID="UnClose" runat="server" Text="������ɺ��Զ��رմ���" Checked="True"></asp:CheckBox>
            </td>
        </tr>
        <tr height="10">
            <td style="height: 23px" align="right">
                <font face="����"></font>
            </td>
            <td style="height: 23px">
                <font face="����"></font>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font face="����"></font>
            </td>
            <td>
                <font face="����">
                    <input id="ChkEnMPwd" type="checkbox" checked name="ChkEnMPwd">&nbsp;�����޸�����</font>
            </td>
        </tr>
        <tr>
            <td style="height: 21px" align="right">
            </td>
            <td style="height: 21px">
                <font face="����">
                    <input id="Checkbox1" type="checkbox" checked name="ChkPwdExprd"><font face="����">&nbsp;������������</font></font>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font face="����"></font>
            </td>
            <td>
                <input id="ChkAccExprd" onclick="javascript:ShowAccTime()" type="checkbox" checked
                    name="ChkAccExprd"><font face="����">&nbsp;�˺���������
                        <div id="ChkPwdExprd1" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                            ����ʱ��</div>
                        <input id="TxtAccExpTm" style="display: none; width: 88px; height: 22px" type="text"
                            size="9" name="TxtAccExpTm"></font>
            </td>
        </tr>
        <tr>
            <td style="height: 79px" align="right">
                <font face="����"></font>
            </td>
            <td style="height: 79px">
                <input id="CheckAccLock" onclick="ShowAccLock();" type="checkbox" name="CheckAccLock">
                <div style="display: inline; width: 88px; color: red; height: 18px">
                    �ʺű�����</div>
                <br>
                <font face="����">&nbsp;&nbsp; </font>
                <input id="ChkAuUnlocka" style="display: none" type="radio" value="0" name="ChkAuUnlock">
                <div id="ChkAuUnlock1" style="display: none; width: 232px; height: 14px">
                    ����������ֱ��ϵͳ����Ա����</div>
                <br>
                <font face="����">&nbsp;&nbsp; </font>
                <input id="ChkAuUnlockb" style="display: none" type="radio" checked value="1" name="ChkAuUnlock">
                <div id="ChkAuUnlock2" style="display: none; width: 70px; height: 15px">
                    �Զ�����</div>
                <select id="SelLockedPeriod" style="display: none; width: 72px" name="SelLockedPeriod">
                    <option value="1">һ&nbsp;&nbsp;����</option>
                    <option value="5" selected>��&nbsp;&nbsp;����</option>
                    <option value="10">ʮ&nbsp;&nbsp;����</option>
                    <option value="15">ʮ�����</option>
                    <option value="30">��ʮ����</option>
                    <option value="60">һ&nbsp;&nbsp;Сʱ</option>
                    <option value="360">��&nbsp;&nbsp;Сʱ</option>
                    <option value="720">ʮ��Сʱ</option>
                    <option value="1440">һ&nbsp;&nbsp;��</option>
                    <option value="9080">һ&nbsp;&nbsp;��</option>
                    <option value="272400">һ&nbsp;&nbsp;��</option>
                </select>
                <div id="ChkAuUnlock3" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                    ��⿪</div>
                <p>
                </p>
                <p>
                </p>
                <p style="color: #ff0000">
                    <font face="����"></font></FONT></p>
            </td>
        </tr>
    </table>

    <script language="javascript">
				function ChooseEmpl()
				{
					var result;
					result = window.showModalDialog('searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
					if (result != 'undefined' && typeof(result)!='undefined')
					{
						var rv = result.split("|");
						if (rv.length == 2);
						{	
							newuser.test1.value = rv[1];
							newuser.userid.value = rv[0];
						}
					}
				}
				
    </script>

    </form>
</body>
</html>
