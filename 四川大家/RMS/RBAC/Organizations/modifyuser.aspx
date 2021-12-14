<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.modifyusers" Codebehind="modifyuser.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>修改用户</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Include/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript">	
			
		function IsValidate()
		{
			var MSG_NEED_username = "请填写您的用户名。";
			var MSG_NEED_userpwd = "请设置您的用户口令。";
			var MSG_NEED_userpwd1 = "您两次输入的密码不一致，请重新输入。";
			var MSG_NEED_person = "请选择对应的员工编号。";
			
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
					alert('请填写到期时间！');
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
                &nbsp;&nbsp;&nbsp;&nbsp;<font color="highlight" size="3"><b>修改账号</b></font>
                <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr height="30">
            <td colspan="2">
                <font face="宋体"></font>
            </td>
        </tr>
        <tr>
            <td style="height: 25px" nowrap align="right" width="8%">
                操作描述：
            </td>
            <td style="height: 25px" width="25%">
                <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                <font face="宋体">账号不能重复，一个员工对应一个账号</font>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="10%">
                <font color="red">*</font>账号名：
            </td>
            <td width="25%">
                <asp:TextBox ID="username" runat="server" Width="272px"></asp:TextBox>
            </td>
        </tr>
        <tr style=" display:none">
            <td style="height: 25px" nowrap align="right" width="10%">
                <font color="red">*</font>账号口令：
            </td>
            <td style="height: 25px" width="25%">
                <asp:TextBox ID="userpwd" runat="server" Width="272px" contentEditable="false" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr style=" display:none">
            <td style="height: 22px" nowrap align="right" width="10%">
                <font color="red">*</font>口令确认：
            </td>
            <td style="height: 22px" width="25%">
                <asp:TextBox ID="userpwd1" runat="server" Width="272px" contentEditable="false" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="10%">
                <font color="red">*</font>对应员工：
            </td>
            <td width="25%">
                <input id="test1" style="width: 112px; height: 22px" disabled type="text" size="13"><a
                    href="javascript:ChooseEmpl()">选择员工</a>
                <input id="userid" style="width: 24px; height: 22px" type="hidden" size="1" name="userid">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" width="10%">
                <font id="FONT1" face="宋体" runat="server">员工描述：</font>
            </td>
            <td width="25%">
                <asp:TextBox ID="UserDesc" runat="server" Width="272px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 44px" align="center" width="5%" colspan="2">
                <font face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font face="宋体">
                </font><font face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font
                    face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font face="宋体">
                </font><font face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font
                    face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font face="宋体">
                </font><font face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font
                    face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font face="宋体">
                </font><font face="宋体"></font><font face="宋体"></font><font face="宋体"></font><font
                    face="宋体"></font><font face="宋体"></font>
                <br>
                &nbsp;
                <asp:Button ID="addnew" runat="server" Text=" 确  定 " OnClick="addnew_Click"></asp:Button>
                <asp:CheckBox ID="UnClose" runat="server" Text="操作完成后，自动关闭窗口" Checked="True"></asp:CheckBox>
            </td>
        </tr>
        <tr height="10">
            <td style="height: 23px" align="right">
                <font face="宋体"></font>
            </td>
            <td style="height: 23px">
                <font face="宋体"></font>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font face="宋体"></font>
            </td>
            <td align="left">
                <font face="宋体">
                    <input id="ChkEnMPwd" type="checkbox" checked name="ChkEnMPwd">&nbsp;允许修改密码</font>
            </td>
        </tr>
        <tr>
            <td style="height: 20px" align="left">
            </td>
            <td style="height: 20px">
                <font face="宋体">
                    <input id="ChkPwdExprd" type="checkbox" checked name="ChkPwdExprd"><font face="宋体">&nbsp;密码永不过期</font></font>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font face="宋体"></font>
            </td>
            <td>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td align="left">
                            <input id="ChkAccExprd" onclick="javascript:ShowAccTime()" type="checkbox" checked
                                name="ChkAccExprd">&nbsp;账号永不过期
                        </td>
                        <td>
                            <div id="ChkPwdExprd1" style="display: none" ms_positioning="FlowLayout">
                                &nbsp;&nbsp;到期时间</div>
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
                <font face="宋体"></font>
            </td>
            <td nowrap align="left">
                <input id="CheckAccLock" onclick="ShowAccLock();" type="checkbox" name="CheckAccLock">
                <div style="display: inline; width: 88px; color: red; height: 18px">
                    帐号被锁定</div>
                <table width="100%" border="0">
                    <tr>
                        <td align="right">
                            <input id="ChkAuUnlocka" style="display: none" type="radio" value="0" name="ChkAuUnlock">
                        </td>
                        <td>
                            <div id="ChkAuUnlock1" style="display: none; width: 232px; height: 14px">
                                永久锁定，直至系统管理员解锁</div>
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
                                            自动解锁</div>
                                    </td>
                                    <td>
                                        <select id="SelLockedPeriod" style="display: none; width: 72px" name="SelLockedPeriod">
                                            <option value="1">一&nbsp;&nbsp;分钟</option>
                                            <option id="a5" value="5" selected>五&nbsp;&nbsp;分钟</option>
                                            <option id="a10" value="10">十&nbsp;&nbsp;分钟</option>
                                            <option id="a15" value="15">十五分钟</option>
                                            <option id="a30" value="30">三十分钟</option>
                                            <option id="a60" value="60">一&nbsp;&nbsp;小时</option>
                                            <option id="a360" value="360">六&nbsp;&nbsp;小时</option>
                                            <option id="a720" value="720">十二小时</option>
                                            <option id="a1440" value="1440">一&nbsp;&nbsp;天</option>
                                            <option id="a9080" value="9080">一&nbsp;&nbsp;周</option>
                                            <option id="a272400" value="272400">一&nbsp;&nbsp;月</option>
                                        </select>
                                    </td>
                                    <td>
                                        <div id="ChkAuUnlock3" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                                            后解开</div>
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
