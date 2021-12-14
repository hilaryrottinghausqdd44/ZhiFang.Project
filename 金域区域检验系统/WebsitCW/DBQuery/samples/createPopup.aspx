<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.samples.createPopup" Codebehind="createPopup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>Popup Example</TITLE>
		<SCRIPT LANGUAGE="JScript">
var oPopup = window.createPopup();
function ButtonClick()
{
    var oPopBody = oPopup.document.body;
    oPopBody.style.backgroundColor = "lightyellow";
    oPopBody.style.border = "solid black 1px";
    oPopBody.innerHTML = divMenu.innerHTML;//"Click outside <B>popup</B> to close.";
    oPopup.show(100, 100, 170, 345, document.body);
}
		</SCRIPT>
	</HEAD>
	<BODY>
		<div id="divMenu" style="LEFT:0px; WIDTH:17.55%; POSITION:absolute; TOP:0px; HEIGHT:100%" onselectstart="return false"
			oncontextmenu="return false">
			<table id="tbMenu" name="tbMenu" border="0" bgcolor="#e0e0e0" cellspacing="0" cellpadding="0"
				style="BORDER-RIGHT:buttonshadow 1px solid;BORDER-TOP:window 1px solid;FONT-SIZE:9pt;BORDER-LEFT:window 1px solid;CURSOR:default;BORDER-BOTTOM:buttonshadow 1px solid"
				width="170" height="100%">
				<tr>
					<td align="center" valign="bottom" width="25" bgcolor="green" id="noneitem" style="FILTER:progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=black, EndColorStr=green)">
						<img src="../images/menutitle.gif"><br>
						<img src="../images/smalllogo.gif">
					</td>
					<td noWrap>
						<table border="0" cellpadding="5" cellspacing="0" style="FONT-SIZE:9pt;CURSOR:default"
							width="100%">
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/main.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0031_b.gif" align="absBottom">&nbsp;桌面
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/Organization/Default.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0019_b.gif" align="absBottom">&nbsp;人员机构
								</td>
							</tr>
							<tr>
								<td height="2">
									<TABLE border="0" cellpadding="0" cellspacing="0" width="128" height="2">
										<tr>
											<td height="1" bgcolor="buttonshadow"></td>
										</tr>
										<tr>
											<td height="1" bgcolor="buttonhighlight"></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/email/Default.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0056_b.gif" align="absBottom">&nbsp;通讯中心
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/collabration/Default.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0002_b.gif" align="absBottom">&nbsp;协同工作
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/info/Default.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0004_b.gif" align="absBottom">&nbsp;新闻和信息
								</td>
							</tr>
							<tr>
								<td height="2">
									<TABLE border="0" cellpadding="0" cellspacing="0" width="128" height="2">
										<tr>
											<td height="1" bgcolor="buttonshadow"></td>
										</tr>
										<tr>
											<td height="1" bgcolor="buttonhighlight"></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/workflow/Default.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0063_b.gif" align="absBottom">&nbsp;工作流程管理
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/DataCenter/Default.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0064_b.gif" align="absBottom">&nbsp;数据中心
								</td>
							</tr>
							<tr>
								<td height="2">
									<TABLE border="0" cellpadding="0" cellspacing="0" width="128" height="2">
										<tr>
											<td height="1" bgcolor="buttonshadow"></td>
										</tr>
										<tr>
											<td height="1" bgcolor="buttonhighlight"></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.frames['Content'].location = '/Desktop/Default.aspx';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0075_b.gif" align="absBottom">&nbsp;系统设置
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.open('/Help/index.html');window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0006_b.gif" align="absBottom">&nbsp;帮助和支持
								</td>
							</tr>
							<tr>
								<td height="2">
									<TABLE border="0" cellpadding="0" cellspacing="0" width="128" height="2">
										<tr>
											<td height="1" bgcolor="buttonshadow"></td>
										</tr>
										<tr>
											<td height="1" bgcolor="buttonhighlight"></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td noWrap onclick="window.parent.parent.location = '/Default.aspx?logout=yes';window.parent.WinMenu.hide()"
									style="CURSOR:hand" onmouseover="this.bgColor='darkgreen';this.style.color='white'"
									onmouseout="this.bgColor='';this.style.color=''">
									<img src="../images/icons/0040_b.gif" align="absBottom">&nbsp;注销
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<BUTTON onclick="ButtonClick()" type="button">Click Me!</BUTTON>
		</div>
	</BODY>
</HTML>
