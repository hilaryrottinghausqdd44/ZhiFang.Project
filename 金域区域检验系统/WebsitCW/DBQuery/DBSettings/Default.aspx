<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings._Default" Codebehind="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">
			th{font-size:15px; font-weight:600;}
			td{font-size:12px;}
			.InputButton{border-style:outset; border-width:2px; background-color:#000080; color:white;border-color:#666666;}
		</style>
		<script type="text/javascript">
			function EditLink(DBName)
			{
				window.open("AddModifyDatabase.aspx?DatabaseName=" + DBName, "", "status=no,height=600,width=800,toolbar=no,scrollbars=1,resizable =1");
			}
			
			function CreateDatabase()
			{
				window.open("AddModifyDatabase.aspx", "", "status=no,height=400,width=500,toolbar=no");
			}
			
			//=========删除数据库=====
			function DeleteDatabase(databaseName,bHasModules)
			{
			    if (window.confirm("确定要删除数据库：" + databaseName + " 吗？")) {
			        if (bHasModules) {
			            if (window.confirm("确定要删除数据库：" + databaseName + " 包含的全部模块吗？ 小心，删除不可以恢复!")) {
			                return true;
			            }
			        }
			        else {
			            return true;
			        }
			    }
			    return false;
			}
			//============End=========
			
			//=======================
			function MouseOverButton(obj)
			{
				obj.style.backgroundColor = "#ccccff";
			}
			function MouseOutButton(obj)
			{
				obj.style.backgroundColor = "";
			}
			//=======================
			
			function SelectDB(strCon)
			{
					var myArray=strCon.split("|▲↓▲|");

					window.opener.document.Form1.TextboxDBType.value = myArray[0];
					window.opener.document.Form1.txtUserName.value=myArray[1];
					window.opener.document.Form1.txtPassword.value=myArray[2];
					window.opener.document.Form1.txtServerName.value=myArray[3];
					window.opener.document.Form1.txtDatabase.value=myArray[4];
					window.opener.document.Form1.txtSystemNumber.value=myArray[5];

					SelectDBType(window.opener.document.Form1.TextboxDBType);
					window.close();
			}
			
			function SelectDBandName(a,b)
			{
				//alert(a + b);
				parent.window.returnValue=a + "," + b;
				window.close();
			}
			
			
			function SelectDBType(obj)
			{				
				switch( obj.value )
				{
					case "XML":
					
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = "none"
					break;
				
				case "MSSQL":
					
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = ""
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = ""
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = ""
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;

				case "ORACEL":
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = ""
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = ""
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = ""
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = "none"
					break;

				case "MSACCESS":
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = ""
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = "none"
					
					break;

				case "DB2":
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;

				case "EXCEL":
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;

				case "UNKOWN":
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;
				
				default:
					window.opener.document.Form1.all["tableSettings"].rows[0].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[1].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[2].style.display = "none"
					window.opener.document.Form1.all["tableSettings"].rows[3].style.display = "none"
					break;
				}
				
			}
			function Form1_onsubmit() {
			    //Form1.ButtonSearch.click();
			}

			function window_onload() {
			    document.all['TextBoxSearch'].focus();
			    document.all['TextBoxSearch'].select();
			}

        </script>
	</HEAD>
	<body onload="return window_onload()">
		<form id="Form1" method="post" runat="server" onsubmit="return Form1_onsubmit()">
			<table width="100%" border="2" bordercolor="#000080" bgcolor="#ffffe0" style="BORDER-TOP-STYLE:solid;BORDER-RIGHT-STYLE:solid;BORDER-LEFT-STYLE:solid;BORDER-COLLAPSE:collapse;BORDER-BOTTOM-STYLE:solid">
			<tr>
			    <td width="10%" nowrap>
                    <asp:TextBox ID="TextBoxSearch" runat="server"></asp:TextBox></td>
			    <td width="5%" nowrap>
                    <asp:Button ID="ButtonSearch" runat="server" Text="检索" 
                        onclick="ButtonSearch_Click" /></td>
			    <td width="80%" nowrap>请输入数据库名称(或系统名称)进行模糊匹配查询</td>
			</tr>
			</table>
			<table width="100%" border="2" bordercolor="#000080" bgcolor="#ffffe0" style="BORDER-TOP-STYLE:solid;BORDER-RIGHT-STYLE:solid;BORDER-LEFT-STYLE:solid;BORDER-COLLAPSE:collapse;BORDER-BOTTOM-STYLE:solid">
				<asp:Repeater id="rptConfigInfo" runat="server">
					<HeaderTemplate>
						<thead bgcolor="#000080" style="COLOR: #ffffff">
							<tr>
								<th align="center">
									编辑</th>
								<th align="center">
									数据库名称</th>
								<th align="center">
									数据库类型
								</th>
								<th align="center">
									系统名称</th>
								<th align="center">
								</th>
							</tr>
						</thead>
					</HeaderTemplate>
					<ItemTemplate>
						<tr bordercolor="#000080">
							<td align="center"><a name="tableEdit" id="tableEdit" runat="server" style="Cursor:hand;Text-decoration:Underline;">编辑表</a>
							</td>
							<td>
								<asp:Label ID="lblDatabaseName" Runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDatabaseType" Runat="server"></asp:Label></td>
							<td>
								<asp:PlaceHolder ID="plhSysNum" Runat="server"></asp:PlaceHolder></td>
							<td><a name="dbEdit" id="dbEdit" runat="server" style="Cursor:hand;Text-decoration:Underline;">编辑库</a>
								&nbsp;
								<asp:LinkButton ID="lbtnDelete" Runat="server" text="删除"></asp:LinkButton>
							</td>
						</tr>
					</ItemTemplate>
					<AlternatingItemTemplate>
						<tr bordercolor="#000080" bgcolor="#ffffff">
							<td align="center"><a name="tableEdit" id="tableEdit" runat="server" style="Cursor:hand;Text-decoration:Underline;">编辑表</a>
							</td>
							<td>
								<asp:Label ID="lblDatabaseName" Runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDatabaseType" Runat="server"></asp:Label></td>
							<td>
								<asp:PlaceHolder ID="plhSysNum" Runat="server"></asp:PlaceHolder></td>
							<td><a name="dbEdit" id="dbEdit" runat="server" style="Cursor:hand;Text-decoration:Underline;">编辑库</a>
								&nbsp;
								<asp:LinkButton ID="lbtnDelete" Runat="server" text="删除"></asp:LinkButton>
							</td>
						</tr>
					</AlternatingItemTemplate>
					<FooterTemplate>
						<tr bordercolor="#000080" bgcolor="#ffffff">
							<td align="center" colspan="5"><input type="button" value="新建数据库" onclick="CreateDatabase()" class="InputButton"></td>
						</tr>
					</FooterTemplate>
				</asp:Repeater>
			</table>
		</form>
	</body>
</HTML>
