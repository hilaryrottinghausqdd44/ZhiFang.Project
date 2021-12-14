<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.MyTestPage" enableViewState="True" Codebehind="MyTestPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MyTestPage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		
		<!--
			function NewCreate()
			{
				if(document.Form1.btnCreate.value == "取消")
				{
					
					document.Form1.btnCreate.value = "新建";
					document.Form1.txtTag.value = "";
					
					document.Form1.txtModuleName.style.display = "none";
					document.Form1.dropDownListModule.style.display="";
					
				}
				else
				{
					document.Form1.btnCreate.value = "取消"
					document.Form1.txtTag.value = "createTag";
					
					document.Form1.txtModuleName.style.display="";
					document.Form1.dropDownListModule.style.display="none";
					
				}						
			}
			
			function SelectDBType(obj)
			{				
				switch( obj.options[obj.selectedIndex].text )
				{
					case "XML":
					
					document.Form1.all["tableUser"].style.display = "none";
					document.Form1.all["tableServer"].style.display = "none";
					document.Form1.all["tableDatabase"].style.display = "none";
					break;
				
				case "MSSQL":
					
					document.Form1.all["tableUser"].style.display = "";
					document.Form1.all["tableServer"].style.display = "";
					document.Form1.all["tableDatabase"].style.display = "";
					break;

				case "ORACEL":
					document.Form1.all["tableUser"].style.display = "";
					document.Form1.all["tableServer"].style.display = "";
					document.Form1.all["tableDatabase"].style.display = "none";
					break;

				case "MSACCESS":
					document.Form1.all["tableUser"].style.display = "";
					document.Form1.all["tableServer"].style.display = "none";
					document.Form1.all["tableDatabase"].style.display = "";
					
					break;

				case "DB2":
					document.Form1.all["tableUser"].style.display = "none";
					document.Form1.all["tableServer"].style.display = "none";
					document.Form1.all["tableDatabase"].style.display = "";
					break;

				case "EXCEL":
					document.Form1.all["tableUser"].style.display = "none";
					document.Form1.all["tableServer"].style.display = "none";
					document.Form1.all["tableDatabase"].style.display = "";
					break;

				case "UNKOWN":
					document.Form1.all["tableUser"].style.display = "none";
					document.Form1.all["tableServer"].style.display = "none";
					document.Form1.all["tableDatabase"].style.display = "";
					break;
				
				default:
					break;
				}
				
			}
			//登入
			function Login()
			{
			  if( (document.Form1.dropDownListModule.selectedIndex == 0) ||
				  (document.Form1.txtTag.value == "createTag") )
			  {
				alert("请选择一个模块名称！");
			  }
			  else
				document.location.href="default.aspx?DB="+ document.Form1.dropDownListModule.options[document.Form1.dropDownListModule.selectedIndex].text;
			}
			
			
			//清除提示信息lblMessage
			function ClearMessage()
			{
				document.Form1.all["lblMessage"].innerText = "";
			}
			
			function beforeDelete()
			{				
				return(confirm('数据库文件也删除吗？'));
			}
			//-->
		</script>
		<script event="onclick" for="btnDelete">
			//alert("真的要删除吗？");
			//return false;
		</script>
	</HEAD>
	<body onclick="javascript:ClearMessage()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td align="center"><b>数据库配置</b></td>
				</tr>
			</table>
			<br>
			<table width="80%" align="center" border="1">
				<tr>
					<td style="HEIGHT: 26px" width="35%">模块名称</td>
					<td style="HEIGHT: 26px"><asp:dropdownlist id="dropDownListModule" runat="server" AutoPostBack="True" onselectedindexchanged="dropDownListModule_SelectedIndexChanged"></asp:dropdownlist><asp:textbox id="txtModuleName" runat="server" EnableViewState="False"></asp:textbox></td>
				</tr>
				<tr>
					<td width="35%">系统编号</td>
					<td><asp:textbox id="txtSystemNumber" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td id="TD1" width="35%">数据库类型</td>
					<td><asp:dropdownlist id="dropDownListDBType" runat="server" onchange="javascript:SelectDBType(this)">
							<asp:ListItem Value="0">XML</asp:ListItem>
							<asp:ListItem Value="1">MSSQL</asp:ListItem>
							<asp:ListItem Value="2">ORACEL</asp:ListItem>
							<asp:ListItem Value="3">MSACCESS</asp:ListItem>
							<asp:ListItem Value="4">DB2</asp:ListItem>
							<asp:ListItem Value="5">EXCEL</asp:ListItem>
							<asp:ListItem Value="6">UNKOWN</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
			</table>
			<br>
			<table id="tableUser" width="80%" align="center" border="1" runat="server">
				<tr>
					<td width="35%">数据库登入名</td>
					<td><asp:textbox id="txtUserName" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td width="35%">登入密码</td>
					<td><asp:textbox id="txtPassword" runat="server"></asp:textbox></td>
				</tr>
			</table>
			<br>
			<table id="tableServer" width="80%" align="center" border="1" runat="server">
				<tr>
					<td width="35%">数据服务器</td>
					<td><asp:textbox id="txtServerName" runat="server"></asp:textbox><asp:button id="Button1" runat="server" Text="Button"></asp:button></td>
				</tr>
			</table>
			<br>
			<table id="tableDatabase" width="80%" align="center" border="1" runat="server">
				<tr>
					<td width="35%"><asp:label id="Label1" runat="server">数据库</asp:label></td>
					<td><asp:textbox id="txtDatabase" runat="server"></asp:textbox></td>
				</tr>
			</table>
			<br>
			<table width="100%" border="1">
				<tr>
					<td colSpan="4"><asp:label id="lblMessage" runat="server" EnableViewState="False" ForeColor="#FF8080" Font-Bold="True"></asp:label></td>
				</tr>
				<tr>
					<td align="center"><INPUT onclick="javascript:NewCreate()" type="button" value="新建" name="btnCreate"></td>
					<td align="center"><asp:button id="btnSave" runat="server" Text="保存" onclick="btnSave_Click"></asp:button></td>
					<td align="center"><asp:button id="btnDelete" runat="server" Text="删除" onclick="btnDelete_Click"></asp:button></td>
					<td align="center"><INPUT onclick="javascript:Login()" type="button" value="登入"></td>
				</tr>
			</table>
			<input id="txtTag" type="hidden" name="txtTag" runat="server">
		</form>
		<script language="javascript">
			document.Form1.txtModuleName.style.display = "none";
		</script>
	</body>
</HTML>
