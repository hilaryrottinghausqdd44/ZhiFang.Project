<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.modifyusers" Codebehind="modifyuser.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�޸��û�</title>
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
			//if (newuser.userpwd)
			//{
			//	if (newuser.userpwd.value.length == 0)
			//	{
			///		alert (MSG_NEED_userpwd);
			//		newuser.userpwd.focus();
			//		return false;
			//	}
			//	if (newuser.userpwd.value != newuser.userpwd1.value)
			//	{
			//		alert (MSG_NEED_userpwd1);
			//		newuser.userpwd1.focus();
			//		return false;
			//	}
			//}
			if (newuser.test1.value == "")
			{
				alert (MSG_NEED_person);
				newuser.test1.focus();
				return false;
			}
			if(newuser.ChkAccExprd.checked==false)
			{
				if(newuser.TxtAccExpTm.value=="")
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
				newuser.ChkAuUnlockb.checked=true;			
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

    <script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() {
	newuser.test1.value="<%=Dt.Rows[0]["NameL"].ToString()+Dt.Rows[0]["NameF"].ToString()%>";
		newuser.userid.value="<%=Dt.Rows[0]["EmpID"]%>";
	
		if("<%=Dt.Rows[0]["EnMPwd"]%>"=="True")
			newuser.ChkEnMPwd.checked=true;
		else
			newuser.ChkEnMPwd.checked=false;
		if("<%=Dt.Rows[0]["PwdExprd"]%>"=="True")
			newuser.ChkPwdExprd.checked=true;
		else
			newuser.ChkPwdExprd.checked=false;
		if("<%=Dt.Rows[0]["AccExprd"]%>"=="True")
			newuser.ChkAccExprd.checked=true;
		else
		{
			newuser.ChkAccExprd.checked=false;
			document.all("ChkPwdExprd1").style.display="";
			newuser.TxtAccExpTm.style.display="";
			newuser.TxtAccExpTm.value="<%=Dt.Rows[0]["AccExpTm"]%>";
		}
		if("<%=Dt.Rows[0]["AccLock"]%>"=="True")
		{
			newuser.CheckAccLock.checked=true;
			document.all("ChkAuUnlock1").style.display="";
			document.all("ChkAuUnlock2").style.display="";
			document.all("ChkAuUnlock3").style.display="";
			newuser.ChkAuUnlocka.style.display="";
			newuser.ChkAuUnlockb.style.display="";
			newuser.SelLockedPeriod.style.display="";
			if(<%=Dt.Rows[0]["AuUnlock"].ToString()%>=="1")
			{
			newuser.ChkAuUnlocka.checked=true;
			var selectedValue="<%=Dt.Rows[0]["LockedPeriod"].ToString()%>";
			
			switch (selectedValue)
			{
			case '1':
			newuser.SelLockedPeriod.options["a" +selectedValue,0].selected=true;
			break;
			case '5':
			newuser.SelLockedPeriod.options["a" +selectedValue,1].selected=true;
			break;
			case '10':
			newuser.SelLockedPeriod.options["a" +selectedValue,2].selected=true;
			break;
			case '15':
			newuser.SelLockedPeriod.options["a" +selectedValue,3].selected=true;	
			break;
			case '30':
			newuser.SelLockedPeriod.options["a" +selectedValue,4].selected=true;	
			break;
			case '60':
			newuser.SelLockedPeriod.options["a" +selectedValue,5].selected=true;			
			break;
			case '360':
			newuser.SelLockedPeriod.options["a" +selectedValue,7].selected=true;	
			break;
			case '720':
			newuser.SelLockedPeriod.options["a" +selectedValue,8].selected=true;	
			break;
			case '1440':
			newuser.SelLockedPeriod.options["a" +selectedValue,9].selected=true;	
			break;
			case '9080':
			newuser.SelLockedPeriod.options["a" +selectedValue,10].selected=true;
			break;
			case '272400':
			newuser.SelLockedPeriod.options["a" +selectedValue,11].selected=true;
			break;
			}
			}
			else
			{
				newuser.ChkAuUnlockb.checked=true;
			}
		}
		else
		{
			newuser.CheckAccLock.checked=false;
			
		}		
}

//-->
    </script>

</head>
<body language="javascript" bottommargin="0" leftmargin="0" background="../../Images/vdisk/images/vdisk-bg.gif"
    topmargin="0" onload="return window_onload()" rightmargin="0">
    <form id="newuser" name="newuser" onsubmit="return IsValidate()" method="post" runat="server">
    <table cellspacing="0" width="100%" align="center" border="0">
        <tr height="50">
            <td nowrap width="1%" colspan="2">
                &nbsp;&nbsp;&nbsp;<img src="../../Images/icons/0019_a.gif" align="absBottom">
                &nbsp;&nbsp;&nbsp;&nbsp;<font color="highlight" size="3"><b>�޸��˺�</b></font>
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
                ����������
            </td>
            <td style="height: 25px" width="25%">
                <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                <font face="����">�˺Ų����ظ���һ��Ա����Ӧһ���˺�</font>
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
        <tr style=" display:none">
            <td style="height: 25px" nowrap align="right" width="10%">
                <font color="red">*</font>�˺ſ��
            </td>
            <td style="height: 25px" width="25%">
                <asp:TextBox ID="userpwd" runat="server" Width="272px" contentEditable="false" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr style=" display:none">
            <td style="height: 22px" nowrap align="right" width="10%">
                <font color="red">*</font>����ȷ�ϣ�
            </td>
            <td style="height: 22px" width="25%">
                <asp:TextBox ID="userpwd1" runat="server" Width="272px" contentEditable="false" TextMode="Password"></asp:TextBox>
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
                </font><font face="����"></font><font face="����"></font><font face="����"></font><font
                    face="����"></font><font face="����"></font>
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
            <td align="left">
                <font face="����">
                    <input id="ChkEnMPwd" type="checkbox" checked name="ChkEnMPwd">&nbsp;�����޸�����</font>
            </td>
        </tr>
        <tr>
            <td style="height: 20px" align="left">
            </td>
            <td style="height: 20px">
                <font face="����">
                    <input id="ChkPwdExprd" type="checkbox" checked name="ChkPwdExprd"><font face="����">&nbsp;������������</font></font>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font face="����"></font>
            </td>
            <td>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td align="left">
                            <input id="ChkAccExprd" onclick="javascript:ShowAccTime()" type="checkbox" checked
                                name="ChkAccExprd">&nbsp;�˺���������
                        </td>
                        <td>
                            <div id="ChkPwdExprd1" style="display: none" ms_positioning="FlowLayout">
                                &nbsp;&nbsp;����ʱ��</div>
                        </td>
                        <td>
                            <input id="TxtAccExpTm" style="display: none; width: 88px" type="text" size="9" name="TxtAccExpTm">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font face="����"></font>
            </td>
            <td nowrap align="left">
                <input id="CheckAccLock" onclick="ShowAccLock();" type="checkbox" name="CheckAccLock">
                <div style="display: inline; width: 88px; color: red; height: 18px">
                    �ʺű�����</div>
                <table width="100%" border="0">
                    <tr>
                        <td align="right">
                            <input id="ChkAuUnlocka" style="display: none" type="radio" value="0" name="ChkAuUnlock">
                        </td>
                        <td>
                            <div id="ChkAuUnlock1" style="display: none; width: 232px; height: 14px">
                                ����������ֱ��ϵͳ����Ա����</div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <input id="ChkAuUnlockb" style="display: none" type="radio" value="1" name="ChkAuUnlock">
                        </td>
                        <td nowrap>
                            <table>
                                <tr>
                                    <td>
                                        <div id="ChkAuUnlock2" style="display: none; width: 70px; height: 15px">
                                            �Զ�����</div>
                                    </td>
                                    <td>
                                        <select id="SelLockedPeriod" style="display: none; width: 72px" name="SelLockedPeriod">
                                            <option value="1">һ&nbsp;&nbsp;����</option>
                                            <option id="a5" value="5" selected>��&nbsp;&nbsp;����</option>
                                            <option id="a10" value="10">ʮ&nbsp;&nbsp;����</option>
                                            <option id="a15" value="15">ʮ�����</option>
                                            <option id="a30" value="30">��ʮ����</option>
                                            <option id="a60" value="60">һ&nbsp;&nbsp;Сʱ</option>
                                            <option id="a360" value="360">��&nbsp;&nbsp;Сʱ</option>
                                            <option id="a720" value="720">ʮ��Сʱ</option>
                                            <option id="a1440" value="1440">һ&nbsp;&nbsp;��</option>
                                            <option id="a9080" value="9080">һ&nbsp;&nbsp;��</option>
                                            <option id="a272400" value="272400">һ&nbsp;&nbsp;��</option>
                                        </select>
                                    </td>
                                    <td>
                                        <div id="ChkAuUnlock3" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                                            ��⿪</div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
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
