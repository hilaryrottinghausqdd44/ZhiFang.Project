<%@ Page language="c#" Codebehind="defaultF.aspx.cs" AutoEventWireup="true" Inherits="OA.Main._defaultF" %>
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
		<style type="text/css">
        <!--

        .STYLE1 {font-size: 12px}
        body {
	        background-image: url(images/bg-0003.jpg);
        }
        -->
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
	<body bgColor="#999999">
		<OBJECT id="RemoveIEToolbar" codeBase="../Includes/Activex/nskey.dll" height="1" width="1"
			classid="CLSID:2646205B-878C-11d1-B07C-0000C040BCDB" VIEWASTEXT>
			<PARAM NAME="ToolBar" VALUE="1">
		</OBJECT>
		<form id="Form1" method="post" runat="server">
			
			<table width="492" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:100px; border: #CCCCCC solid 1px;">
              <tr>
                <td><img src="images/bg.jpg" width="492" height="156" /></td>
              </tr>
              <tr>
                <td height="35" align="center" bgcolor="f4ffff"><table width="480" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="40"><img src="images/admin.gif" width="25" height="26" /></td>
                    <td width="60"><span class="STYLE1">用户名：</span></td>
                    <td width="120"><asp:textbox id="textUserid" runat="server" Width="102px" Height="19px"></asp:textbox></td>
                    <td width="50"><span class="STYLE1">密码：</span></td>
                    <td width="120"><asp:textbox id="textPassword" runat="server" Width="80px" Height="19px" TextMode="Password"></asp:textbox></td>
                    <td><a href="#"><img src="images/dl.gif" width="74" height="24" border="0" onclick="javascript:return checked();"  /></a></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td>
							<asp:Label id="LBLDomain" runat="server"> 域  </asp:Label>
							<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList>
							<asp:Button id="Button1" runat="server" Width="0px" Text="Button"></asp:Button>
						<asp:label id="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red" Visible="False"
								BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:label>
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
			
			function Transit()
			{
				//
				x++;
				window.status=x;
			}
		</SCRIPT>
	</body>
</HTML>
