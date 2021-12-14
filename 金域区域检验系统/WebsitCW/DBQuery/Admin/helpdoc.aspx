<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.helpdoc" Codebehind="helpdoc.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>helpdoc</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<STYLE>TD { FONT-SIZE: 13px }
		</STYLE>
		<script language="javascript" for="chkEnable" event="onpropertychange">
		if(chkEnable.checked)
		{
			//document.getElementById("Button2").disabled=false;
			//document.getElementById("Button1").disabled=false;
		}
		else
		{
			//document.getElementById("Button1").disabled=true;
			//document.getElementById("Button2").disabled=true;
		/*
			var kids = form1.childNodes 
			for(var i=0;i<kids.length;i++)
			{
				if(kids[i].enabled==true)
					kids[i].enabled=false;
			}
			*/
			
		}
		</script>
		<script>
			
			function openWin(url,w,h)
			{
				var px=Math.round((window.screen.availWidth-w)/2);
				var py=Math.round((window.screen.availHeight-100-h)/2);
				var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes");
			}
		</script>
	</HEAD>
	<body>
		<form id="form1" runat="server">
			<table style="BORDER-COLLAPSE: collapse" borderColor="#93bee2" cellSpacing="0" cellPadding="0"
				width="400" align="center" border="1">
				<tr height="25">
					<td align="left" colSpan="2"><asp:checkbox id="chkEnable" runat="server" Text="启用"></asp:checkbox></td>
				</tr>
				<!--
				<tr height="25">
					<td>访问地址：</td>
					<td><asp:textbox id="TextBox1" runat="server" Enabled="False" Width="175px"></asp:textbox></td>
				</tr>
				-->
				<tr height="25">
					<td style="HEIGHT: 25px">窗口位置：</td>
					<td style="HEIGHT: 25px">
						<asp:RadioButton id="Rbs" runat="server" Text="本窗口" GroupName="winlocal"></asp:RadioButton>
						<asp:RadioButton id="Rbb" runat="server" Text="新窗口" GroupName="winlocal"  Checked="True"></asp:RadioButton></td>
				</tr>
				<tr height="25">
					<td>窗口大小：</td>
					<td>
					    PositionSize
						<asp:TextBox id="txt_PositionSize" runat="server" Width="64px" Text="中,中,80%,90%"></asp:TextBox></td>
				</tr>
				<tr height="25">
					<td>传递参数：</td>
					<td><asp:textbox id="txt_para" runat="server" Width="175px" Enabled="False"></asp:textbox><INPUT type="button" value="浏览" onclick="openWin('SelectPage.aspx','500','500')"></td>
				</tr>
				<tr height="25">
					<td colspan="2" align="center">
						<asp:Button id="Button1" runat="server" Text="修改" Width="60px" onclick="Button1_Click"></asp:Button>&nbsp;
						<asp:Button id="Button2" runat="server" Text="重置" Width="57px" onclick="Button2_Click"></asp:Button></td>
				</tr>
				<tr height="25">
					<td colspan="2" align="center"><FONT face="宋体">请用户自行创建“帮助新闻”模块！</FONT></td>
				</tr>
			</table>
			<input type="hidden" runat="server" id="hpara">
		</form>
	</body>
</HTML>
