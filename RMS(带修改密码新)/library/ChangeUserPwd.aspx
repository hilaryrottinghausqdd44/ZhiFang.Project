<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.library.ChangeUserPwd" Codebehind="ChangeUserPwd.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�޸�����</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function ValidForm()
		{	
			
			if(ChangeUserPwd.curpwd.value!='<%=oldpassword%>')
			{
				alert("������ľ�����������������룡");
				ChangeUserPwd.curpwd.focus();
				return false;
			}
			if (ChangeUserPwd.userpwd.value != ChangeUserPwd.userpwd1.value)
			{
				alert ('��������������벻һ�£����������롣');
				ChangeUserPwd.userpwd1.focus();
				return false;
			}
			ChangeUserPwd.modify.value="yes";
			return true;
		}
		</script>
	</HEAD>
	<body bgcolor="gainsboro" topmargin="0" leftmargin="0">
		<table width="100%" border="0" cellspacing="0" bgcolor="darkgray" height="40">
			<tr>
				<td align="left">
					<font color="white" size="3"><b>&nbsp;�������� </b></font>
				</td>
			</tr>
		</table>
		<form id="ChangeUserPwd" name="ChangeUserPwd" method="post" runat="server" onsubmit="return ValidForm()">
			<br>
			<table border="0" cellspacing="1" width="200" bgcolor="lightgrey" align="center">
				<tr bgcolor="white" height="25">
					<td nowrap width="1%" bgcolor="#f0f0f0">
						&nbsp;��ǰ���룺
					</td>
					<td>
						<input type="password" style="WIDTH:100%" id="curpwd" name="curpwd">
					</td>
				</tr>
				<tr bgcolor="white" height="25">
					<td nowrap width="1%" bgcolor="#f0f0f0">
						&nbsp;�����룺
					</td>
					<td>
						<input type="password" style="WIDTH:100%" id="userpwd" name="userpwd">
					</td>
				</tr>
				<tr bgcolor="white" height="25">
					<td nowrap width="1%" bgcolor="#f0f0f0">
						&nbsp;�ظ������룺
					</td>
					<td>
						<input type="password" style="WIDTH:100%" id="userpwd1" name="userpwd1">
					</td>
				</tr>
			</table>
			<INPUT id="modify"  HEIGHT: 22px" type="text" size="10" name="modify" style="WIDTH:0"
				width="0" height="0">
			<P align="center">
				<input type="submit" value=" ���� "> <input type="button" value=" ȡ�� " onclick="window.close()">
			</P>
			<P align="center">
				<asp:Label id="Label1" runat="server"></asp:Label></P>
		</form>
		<script language="javascript">
		ChangeUserPwd.curpwd.focus();
		</script>
	</body>
</HTML>
