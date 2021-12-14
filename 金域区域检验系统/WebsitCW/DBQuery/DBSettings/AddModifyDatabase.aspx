<%@ Page language="c#" validateRequest=false AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.AddModifyDatabase" Codebehind="AddModifyDatabase.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddModifyDatabase</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script type="text/javascript">
			function InitTable()
			{
				var selectObj = document.getElementById("dropDownDBType");
				var tableObj = document.getElementById("tblDatabaseInfo");
				
				WhichDisplay(tableObj, selectObj.options[selectObj.selectedIndex].text);
				
				document.getElementById("CheckBox2").disabled=true;
				document.getElementById("CheckBox2").checked=false;
			}
			
			function WhichDisplay(TableObj, DBType)
			{
				document.getElementById("ButtonConvert").disabled=false;
				switch(DBType)
				{
					case "XML":
					
					TableObj.rows[3].style.display = "none"
					TableObj.rows[4].style.display = "none"
					TableObj.rows[5].style.display = "none"
					TableObj.rows[6].style.display = "none"
					document.getElementById("ButtonConvert").disabled=true;
					break;
				
				case "MSSQL":
					
					TableObj.rows[3].style.display = ""
					TableObj.rows[4].style.display = ""
					TableObj.rows[5].style.display = ""
					TableObj.rows[6].style.display = ""
					break;

				case "ORACEL":
					TableObj.rows[3].style.display = ""
					TableObj.rows[4].style.display = ""
					TableObj.rows[5].style.display = ""
					TableObj.rows[6].style.display = "none"
					break;

				case "MSACCESS":
					TableObj.rows[3].style.display = "none"
					TableObj.rows[4].style.display = "none"
					TableObj.rows[5].style.display = ""
					TableObj.rows[6].style.display = "none"
					
					break;

				case "DB2":
					TableObj.rows[3].style.display = "none"
					TableObj.rows[4].style.display = "none"
					TableObj.rows[5].style.display = "none"
					TableObj.rows[6].style.display = ""
					break;

				case "EXCEL":
					TableObj.rows[3].style.display = "none"
					TableObj.rows[4].style.display = "none"
					TableObj.rows[5].style.display = "none"
					TableObj.rows[6].style.display = ""
					break;

				case "UNKOWN":
					TableObj.rows[3].style.display = "none";
					TableObj.rows[4].style.display = "none";
					TableObj.rows[5].style.display = "none";
					TableObj.rows[6].style.display = "";
					break;
				
				default:
					TableObj.rows[3].style.display = "none";
					TableObj.rows[4].style.display = "none";
					TableObj.rows[5].style.display = "none";
					TableObj.rows[6].style.display = "none";
					break;
				}
			}
			
			function DatabaseTypeChange(obj)
			{
				var tableObj = document.getElementById("tblDatabaseInfo");
				WhichDisplay(tableObj, obj.options[obj.selectedIndex].text);
			}
			
			//=======保存更改，验证条件是否满足====
			function ClickSaveChange()
			{
				var dbNameObj = document.getElementById("txtDBName");
				var strName = dbNameObj.value;
				
				if(strName.length == 0)
				{
					alert("数据库名不能为空！");
					return false;
				}
				return true;
			}
		</script>
		<script language="javascript" event="onpropertychange" for="CheckBox1">
			if(this.checked)
				document.getElementById("CheckBox2").disabled=false;
			else
			{
				document.getElementById("CheckBox2").disabled=true;
				document.getElementById("CheckBox2").checked=false;
			}
			
		</script>
		<script language="javascript" id="clientEventHandlersJS">
	<!--

	function Form1_onsubmit() {
		Form1.TextBox1.innerHTML="";
		return true;
	}

	//-->
		</script>
	</HEAD>
	<body onload="InitTable()">
		<form language="javascript" id="Form1" onsubmit="return Form1_onsubmit()" method="post"
			runat="server">
			<TABLE id="tableDataName" style="DISPLAY: none; Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 8px"
				cellSpacing="1" cellPadding="1" width="100%" border="1" runat="server">
				<tr>
					<td></td>
				</tr>
			</TABLE>
			<table id="tblDatabaseInfo" width="100%">
				<TR>
					<TD style="FONT-WEIGHT: bold; WIDTH: 150px; COLOR: #ffffff; BACKGROUND-COLOR: #000080"
						align="center" colSpan="2">数据库配置信息</TD>
				</TR>
				<TR>
					<TD align="right">数据库名称</TD>
					<TD align="left">&nbsp;
						<asp:textbox id="txtDBName" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="right">数据库类型</TD>
					<TD align="left">
						<P>&nbsp;
							<asp:dropdownlist id="dropDownDBType" Runat="server">
								<asp:ListItem Value="0">XML</asp:ListItem>
								<asp:ListItem Value="1">MSSQL</asp:ListItem>
								<asp:ListItem Value="2">ORACEL</asp:ListItem>
								<asp:ListItem Value="3">MSACCESS</asp:ListItem>
								<asp:ListItem Value="4">DB2</asp:ListItem>
								<asp:ListItem Value="5">EXCEL</asp:ListItem>
								<asp:ListItem Value="99">UNKOWN</asp:ListItem>
							</asp:dropdownlist><FONT face="宋体"><BR>
							</FONT>
							<asp:button id="ButtonConvert" runat="server" Text="从XML转换结构到数据库" onclick="ButtonConvert_Click"></asp:button>
							<asp:Button id="Button1" runat="server" Text="删除SQL数据库表" onclick="Button1_Click"></asp:Button><FONT face="宋体"><BR>
							</FONT><FONT face="宋体">(另外还要设置表的主外键关系)</FONT><BR>
							<asp:checkbox id="CheckBox1" runat="server" Text="同时转入XML数据"></asp:checkbox><FONT face="宋体"><BR>
								<asp:checkbox id="CheckBox2" runat="server" Text="同时删除XML数据"></asp:checkbox></P>
						</FONT></TD>
				</TR>
				<TR>
					<TD align="right">数据库登录名</TD>
					<TD align="left">&nbsp;
						<asp:textbox id="txtLoginName" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="right">登录密码</TD>
					<TD align="left">&nbsp;
						<asp:textbox id="txtPassword" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="right">数据服务器</TD>
					<TD align="left">&nbsp;
						<asp:textbox id="txtServer" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="right">数据库</TD>
					<TD align="left">&nbsp;
						<asp:textbox id="txtDatabaseName" Runat="server"></asp:textbox><INPUT type="button" value="测试连接"></TD>
				</TR>
				<TR>
					<TD align="right"><asp:button id="btnChange" Runat="server" Text="保存更改" onclick="btnChange_Click"></asp:button></TD>
					<TD align="left"><INPUT onclick="window.close();" type="button" value="取消"></TD>
				</TR>
				<TR>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><asp:label id="LabelMSG" runat="server"></asp:label></TD>
				</TR>
			</table>
			<asp:textbox id="TextBox1" runat="server" Height="214px" Width="100%" TextMode="MultiLine"></asp:textbox></form>
	</body>
</HTML>
