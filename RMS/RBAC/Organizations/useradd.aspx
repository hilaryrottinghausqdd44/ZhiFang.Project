<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.useradd" Codebehind="useradd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>用户添加</title>
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
                &nbsp;&nbsp;<font color="highlight" size="3"><b>添加新账号</b></font>
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
                &nbsp;操作描述：
            </td>
            <td style="height: 25px" width="25%">
                <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                <font face="宋体">账号不能重复，一个员工对应一个账号</font>
                <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
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
        <tr>
            <td style="height: 25px" nowrap align="right" width="10%">
                <font color="red">*</font>账号口令：
            </td>
            <td style="height: 25px" width="25%">
                <asp:TextBox ID="userpwd" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 22px" nowrap align="right" width="10%">
                <font color="red">*</font>口令确认：
            </td>
            <td style="height: 22px" width="25%">
                <asp:TextBox ID="userpwd1" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
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
                </font><font face="宋体"></font>
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
            <td>
                <font face="宋体">
                    <input id="ChkEnMPwd" type="checkbox" checked name="ChkEnMPwd">&nbsp;允许修改密码</font>
            </td>
        </tr>
        <tr>
            <td style="height: 21px" align="right">
            </td>
            <td style="height: 21px">
                <font face="宋体">
                    <input id="Checkbox1" type="checkbox" checked name="ChkPwdExprd"><font face="宋体">&nbsp;密码永不过期</font></font>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font face="宋体"></font>
            </td>
            <td>
                <input id="ChkAccExprd" onclick="javascript:ShowAccTime()" type="checkbox" checked
                    name="ChkAccExprd"><font face="宋体">&nbsp;账号永不过期
                        <div id="ChkPwdExprd1" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                            到期时间</div>
                        <input id="TxtAccExpTm" style="display: none; width: 88px; height: 22px" type="text"
                            size="9" name="TxtAccExpTm"></font>
            </td>
        </tr>
        <tr>
            <td style="height: 79px" align="right">
                <font face="宋体"></font>
            </td>
            <td style="height: 79px">
                <input id="CheckAccLock" onclick="ShowAccLock();" type="checkbox" name="CheckAccLock">
                <div style="display: inline; width: 88px; color: red; height: 18px">
                    帐号被锁定</div>
                <br>
                <font face="宋体">&nbsp;&nbsp; </font>
                <input id="ChkAuUnlocka" style="display: none" type="radio" value="0" name="ChkAuUnlock">
                <div id="ChkAuUnlock1" style="display: none; width: 232px; height: 14px">
                    永久锁定，直至系统管理员解锁</div>
                <br>
                <font face="宋体">&nbsp;&nbsp; </font>
                <input id="ChkAuUnlockb" style="display: none" type="radio" checked value="1" name="ChkAuUnlock">
                <div id="ChkAuUnlock2" style="display: none; width: 70px; height: 15px">
                    自动解锁</div>
                <select id="SelLockedPeriod" style="display: none; width: 72px" name="SelLockedPeriod">
                    <option value="1">一&nbsp;&nbsp;分钟</option>
                    <option value="5" selected>五&nbsp;&nbsp;分钟</option>
                    <option value="10">十&nbsp;&nbsp;分钟</option>
                    <option value="15">十五分钟</option>
                    <option value="30">三十分钟</option>
                    <option value="60">一&nbsp;&nbsp;小时</option>
                    <option value="360">六&nbsp;&nbsp;小时</option>
                    <option value="720">十二小时</option>
                    <option value="1440">一&nbsp;&nbsp;天</option>
                    <option value="9080">一&nbsp;&nbsp;周</option>
                    <option value="272400">一&nbsp;&nbsp;月</option>
                </select>
                <div id="ChkAuUnlock3" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                    后解开</div>
                <p>
                </p>
                <p>
                </p>
                <p style="color: #ff0000">
                    <font face="宋体"></font></FONT></p>
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
