<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="OA.Main._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>
			-主页</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">hand { CURSOR: hand }
		</style>
		<script language="javascript">
		
		function checked()
		
		{
			if(window.document.all["textUserid"].value=="")
			{
				alert("请输入用户名！")
				return false;
			}
			else
			{
				Form1.submit();
			}
		}
		</script>
	</HEAD>
	<body bgColor="#999999" onload="applyTransition()">
		<OBJECT id="RemoveIEToolbar" codeBase="../Includes/Activex/nskey.dll" height="1" width="1"
			classid="CLSID:2646205B-878C-11d1-B07C-0000C040BCDB" VIEWASTEXT>
			<PARAM NAME="ToolBar" VALUE="1">
		</OBJECT>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="center" bgColor="#ffffff" border="0">
				<tr>
					<td bgColor="#ffffff" height="99" style="HEIGHT: 99px">&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="237">
						<DIV align="center">
							<table cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<tr>
									<td height="64">&nbsp;</td>
									<td><IMG height="64" src="image/index/left1.jpg" width="223"></td>
									<td><IMG height="64" src="image/index/right1.jpg" width="217"></td>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td width="172" height="108"><img src="image/index/left.jpg" width="172" height="108"></td>
									<td colspan="2" background="image/index/index.jpg"><table width="100%" height="40" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td class="zhifang_index">
													<H2 align="center"><FONT face="黑体" color="#ffffff"><EM>企业内部办公系统</EM></FONT></H2>
												</td>
											</tr>
										</table>
									</td>
									<td width="188"><img src="image/index/right.jpg" width="188" height="108"></td>
								</tr>
								<tr>
									<td height="30">&nbsp;</td>
									<td><IMG height="30" src="image/index/left3.jpg" width="223"></td>
									<td><IMG height="30" src="image/index/right3.jpg" width="217"></td>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td height="19">&nbsp;</td>
									<td colSpan="2">
										<table style="WIDTH: 440px; HEIGHT: 10px" height="10" cellSpacing="0" cellPadding="0" width="440"
											border="0">
											<tr>
												<td width="63"><IMG height="19" src="image/index/left4_1.jpg" width="63"></td>
												<!--<td width="38"><IMG height="19" src="image/index/left4_2.jpg" width="38"></td>-->
												<td style="FONT-SIZE:12px" nowrap align="left">用户名</td>
												<td style="WIDTH: 83px" noWrap width="83"><asp:textbox id="textUserid" runat="server" Width="102px" Height="19px"></asp:textbox></td>
												<!--<td width="25"><IMG height="19" src="image/index/right4_2.jpg" width="25">&nbsp;</td>-->
												<td style="FONT-SIZE:12px" nowrap align="right">密码</td>
												<td noWrap width="100"><asp:textbox id="textPassword" runat="server" Width="80px" Height="19px" TextMode="Password"></asp:textbox></td>
												<td width="62"><IMG style="CURSOR: hand" onclick="javascript:return checked();" runat="server" height="19"
														src="image/index/right4_3.jpg" width="62"></td>
												<td width="46"><IMG height="19" src="image/index/right4_1.jpg" width="46"></td>
											</tr>
										</table>
									</td>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td height="16">&nbsp;</td>
									<td vAlign="top" colSpan="2"><IMG height="11" src="image/index/bottom.jpg" width="440"></td>
									<td>&nbsp;</td>
								</tr>
							</table>
						</DIV>
						<P align="center">
							<asp:Label id="LBLDomain" runat="server"> 域  </asp:Label>
							<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList>
							<asp:Button id="Button1" runat="server" Width="0px" Text="Button"></asp:Button></P>
						<P><asp:label id="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red" Visible="False"
								BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:label></P>
					</td>
				</tr>
				<tr>
					<td height="178" bgcolor="#ffffff" vAlign="top" align="center">
						<IMG id="oImg" alt="系统优化..." src="../Images/zfLogon.gif" style="PADDING-RIGHT: 10px; PADDING-LEFT: 13px; FILTER: progid: DXImageTransform.Microsoft.Alpha(
                                                                              						style=2,opacity=25,finishOpacity=34); COLOR: #daa520; BACKGROUND-COLOR: skyblue">
						<!--iframe name="frmIncreaseSpeed" id="frmIncreaseSpeed" src="iFrames/IncreaseSpeed.htm" width="0"
							height="0"></iframe-->
					</td>
				</tr>
			</table>
		</form>
		<SCRIPT language="javascript">
			document.all['textUserid'].focus();
			document.all['textUserid'].select();
			
			var oImg=document.getElementById('oImg');
			var x=0;
			var interval=1;
			
			function applyTransition ()
			{	
				if (x>99)
					interval=-1;
				else if(x<1)
					interval=1;
				x +=interval;
				
				oImg.style.filter="progid:DXImageTransform.Microsoft.Alpha(style=2,opacity="+x+",finishOpacity=" + (100-x)+ ")"; 
				window.setTimeout("applyTransition()",30);
			}
			/*
			function applyTransition ()
			{	
					
				if (x>99||y>99)
					interval=-5;
				if(x<1||y<1)
					interval=5;
				
				if(y>=0&&y<=100)
					y +=interval;
				if(x>=0&&x<=100)
					x +=interval;	
				
				//oImg.filters(0).Apply();
				//oImg.style.visibility = "visible";
				window.status=x;
				oImg.style.filter="progid:DXImageTransform.Microsoft.Alpha(style=1,opacity=0,finishOpacity=100,startX=" + x + ",finishX="+(100-x)+",startY=" + y + ",finishY=" + (100-x);
				window.setTimeout("applyTransition()",20);
				//oImg.filters(0).Play();
			}
			*/
			function Transit()
			{
				//
				x++;
				window.status=x;
			}
		</SCRIPT>
	</body>
</HTML>
