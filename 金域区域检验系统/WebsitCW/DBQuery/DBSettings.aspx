<%@ Page language="c#" Codebehind="DBSettings.aspx.cs" AutoEventWireup="false" Inherits="Zhifang.Utilities.Query.DBSettings" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DBSettingsTest</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style>TD { FONT-SIZE: 12px }
	INPUT { BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; FONT-SIZE: 12px; BORDER-LEFT-COLOR: skyblue; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: skyblue; BORDER-TOP-COLOR: skyblue; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: skyblue }
	SELECT { FONT-SIZE: 12px }
	TD.ttd { FONT-SIZE: 12px; LINE-HEIGHT: 150% }
	.newsp0 { FONT-SIZE: 12px; COLOR: #ffffff }
	A:link { COLOR: #0000ff; TEXT-DECORATION: underline }
	A:visited { COLOR: #800080; TEXT-DECORATION: underline }
	A:active { COLOR: #ff0000; TEXT-DECORATION: underline }
	A:hover { COLOR: #ff0000; TEXT-DECORATION: underline }
	.l15 { LINE-HEIGHT: 150% }
	.l16 { FONT-SIZE: 14px; LINE-HEIGHT: 160% }
	.f12 { FONT-SIZE: 12px }
	.title0 { FONT-WEIGHT: bold; FONT-SIZE: 17px; COLOR: #000000; FONT-FAMILY: arial; TEXT-DECORATION: none }
	A.title1:link { FONT-WEIGHT: bold; FONT-SIZE: 17px; COLOR: #000000; FONT-FAMILY: arial; TEXT-DECORATION: none }
	A.title1:visited { FONT-WEIGHT: bold; FONT-SIZE: 17px; COLOR: #000000; FONT-FAMILY: arial; TEXT-DECORATION: none }
	A.title1:active { FONT-WEIGHT: bold; FONT-SIZE: 17px; COLOR: #000000; FONT-FAMILY: arial; TEXT-DECORATION: none }
	A.title1:hover { FONT-WEIGHT: bold; FONT-SIZE: 17px; COLOR: #000000; FONT-FAMILY: arial; TEXT-DECORATION: none }
	A.title2:link { COLOR: #000000; TEXT-DECORATION: none }
	A.title2:visited { COLOR: #000000; TEXT-DECORATION: none }
	A.title2:active { COLOR: #ff0000; TEXT-DECORATION: none }
	A.title2:hover { COLOR: #ff0000; TEXT-DECORATION: none }
	A.sinatail:link { FONT-SIZE: 12px; COLOR: #0000ff; TEXT-DECORATION: underline }
	A.sinatail:visited { FONT-SIZE: 12px; COLOR: #0000ff; TEXT-DECORATION: underline }
	A.sinatail:active { FONT-SIZE: 12px; COLOR: #000000; TEXT-DECORATION: underline }
	A.sinatail:hover { FONT-SIZE: 12px; COLOR: #ff0000; TEXT-DECORATION: underline }
	A.news01:link { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: none }
	A.news01:visited { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: none }
	A.news01:active { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: underline }
	A.news01:hover { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: underline }
	A.news1:link { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: none }
	A.news1:visited { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: none }
	A.news1:active { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: underline }
	A.news1:hover { FONT-SIZE: 12px; COLOR: #ffffff; TEXT-DECORATION: underline }
	A.news2:link { FONT-SIZE: 12px; COLOR: #001a69; TEXT-DECORATION: none }
	A.news2:visited { FONT-SIZE: 12px; COLOR: #001a69; TEXT-DECORATION: none }
	A.news2:active { FONT-SIZE: 12px; COLOR: #001a69; TEXT-DECORATION: none }
	A.news2:hover { FONT-SIZE: 12px; COLOR: #001a69; TEXT-DECORATION: none }
	A.news3:link { COLOR: #0000ff; TEXT-DECORATION: none }
	A.news3:visited { COLOR: #800080; TEXT-DECORATION: none }
	A.news3:active { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.news3:hover { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.news4:link { COLOR: #0000ff }
	A.news4:visited { COLOR: #0000ff }
	A.news4:active { COLOR: #ff0000 }
	A.news4:hover { COLOR: #ff0000 }
	.c03 { BORDER-LEFT-COLOR: #000000; BORDER-BOTTOM-COLOR: #000000; COLOR: #000000; BORDER-TOP-COLOR: #000000; BORDER-RIGHT-COLOR: #000000 }
	.nh01 { LINE-HEIGHT: 130% }
	.n01 { COLOR: #0000ff }
	.n02 { FONT-SIZE: 14px; LINE-HEIGHT: 150% }
	.n03 { COLOR: #ff0000 }
	.n04 { FONT-SIZE: 11px; COLOR: #6666cc }
	.n05 { COLOR: #ffffff }
	.n06 { COLOR: #c83747 }
	.n07 { COLOR: #0c00ad; LINE-HEIGHT: 120% }
	.f7 { FONT-SIZE: 7px; COLOR: #00349a }
	A.a01:link { COLOR: #000000; TEXT-DECORATION: none }
	A.a01:visited { COLOR: #000000; TEXT-DECORATION: none }
	A.a01:active { COLOR: #ff0000; TEXT-DECORATION: none }
	A.a01:hover { COLOR: #ff0000; TEXT-DECORATION: none }
	A.a02:link { TEXT-DECORATION: none }
	A.a02:visited { COLOR: #828282; TEXT-DECORATION: none }
	A.a02:active { COLOR: #0000ff; TEXT-DECORATION: underline }
	A.a02:hover { COLOR: #0000ff; TEXT-DECORATION: underline }
	A.a03:link { COLOR: #0c00ad }
	A.a03:visited { COLOR: #5852ac }
	A.a03:active { COLOR: #ff0000 }
	A.a03:hover { COLOR: #ff0000 }
	A.a04:link { COLOR: #0c00ad; TEXT-DECORATION: none }
	A.a04:visited { COLOR: #5852ac; TEXT-DECORATION: none }
	A.a04:active { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.a04:hover { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.a05:link { COLOR: #ffffff; TEXT-DECORATION: none }
	A.a05:visited { COLOR: #ffffff; TEXT-DECORATION: none }
	A.a05:active { COLOR: #ffffff; TEXT-DECORATION: underline }
	A.a05:hover { COLOR: #ffffff; TEXT-DECORATION: underline }
	A.a06:link { TEXT-DECORATION: underline }
	A.a06:visited { TEXT-DECORATION: underline }
	A.a06:active { TEXT-DECORATION: underline }
	A.a06:hover { TEXT-DECORATION: underline }
	A.a07:link { COLOR: #0000aa; TEXT-DECORATION: none }
	A.a07:visited { COLOR: #800080; TEXT-DECORATION: none }
	A.a07:active { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.a07:hover { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.a08:link { COLOR: #000000; TEXT-DECORATION: underline }
	A.a08:visited { COLOR: #000000; TEXT-DECORATION: underline }
	A.a08:active { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.a08:hover { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.a09:link { COLOR: #0c35b9; TEXT-DECORATION: underline }
	A.a09:visited { COLOR: #7f7f7f; TEXT-DECORATION: underline }
	A.a09:active { COLOR: #b10306; TEXT-DECORATION: underline }
	A.a09:hover { COLOR: #b10306; TEXT-DECORATION: underline }
	.c05 { BORDER-LEFT-COLOR: #a6a6a6; BORDER-BOTTOM-COLOR: #a6a6a6; COLOR: #a6a6a6; BORDER-TOP-COLOR: #a6a6a6; BORDER-RIGHT-COLOR: #a6a6a6 }
	.s01 { BORDER-RIGHT: 1px inset; BORDER-TOP: 1px inset; VERTICAL-ALIGN: text-bottom; BORDER-LEFT: 1px inset; BORDER-BOTTOM: 1px inset; BACKGROUND-COLOR: #ffffff }
	.s02 { BORDER-RIGHT: 1px ridge; BORDER-TOP: #ffffff 1px ridge; BORDER-LEFT: #ffffff 1px ridge; BORDER-BOTTOM: 1px ridge; BACKGROUND-COLOR: #cccccc }
	.s03 { BORDER-RIGHT: #939598 1px solid; BORDER-TOP: #939598 1px solid; VERTICAL-ALIGN: text-bottom; BORDER-LEFT: #939598 1px solid; BORDER-BOTTOM: #939598 1px solid; BACKGROUND-COLOR: #ffffff }
	.l12 { LINE-HEIGHT: 120% }
	.l14 { LINE-HEIGHT: 140% }
	.l20 { LINE-HEIGHT: 20px }
	.l22 { LINE-HEIGHT: 22px }
	.l24 { LINE-HEIGHT: 24px }
	.n07 { COLOR: #0c00ad; LINE-HEIGHT: 120% }
	A.a06:link { TEXT-DECORATION: underline }
	A.a06:visited { TEXT-DECORATION: underline }
	A.a06:active { TEXT-DECORATION: underline }
	A.a06:hover { TEXT-DECORATION: underline }
	.wc01 { FONT-SIZE: 12px; COLOR: #00349a }
	A.wa01:link { COLOR: #ffffff; TEXT-DECORATION: none }
	A.wa01:visited { COLOR: #ffffff; TEXT-DECORATION: none }
	A.wa01:active { COLOR: #00ffff; TEXT-DECORATION: none }
	A.wa01:hover { COLOR: #00ffff; TEXT-DECORATION: none }
	A.wa02:link { COLOR: #0000ff; TEXT-DECORATION: none }
	A.wa02:visited { COLOR: #800080; TEXT-DECORATION: none }
	A.wa02:active { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.wa02:hover { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.wa03:link { COLOR: #000000; TEXT-DECORATION: none }
	A.wa03:visited { COLOR: #800080; TEXT-DECORATION: none }
	A.wa03:active { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.wa03:hover { COLOR: #ff0000; TEXT-DECORATION: underline }
	A.wa04:link { COLOR: #0000ca }
	A.wa04:visited { COLOR: #800080 }
	A.wa04:active { COLOR: #ff0000 }
	A.wa04:hover { COLOR: #ff0000 }
	.nbtn01 { BORDER-RIGHT: #4879bc 1px solid; BORDER-TOP: #fff 1px solid; BORDER-LEFT: #fff 1px solid; BORDER-BOTTOM: #4879bc 1px solid; BACKGROUND-COLOR: #bbd2f4 }
	TD { BORDER-RIGHT: medium none; PADDING-RIGHT: 0px; BORDER-TOP: medium none; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: medium none; PADDING-TOP: 0px; BORDER-BOTTOM: medium none }
	FORM { BORDER-RIGHT: medium none; PADDING-RIGHT: 0px; BORDER-TOP: medium none; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: medium none; PADDING-TOP: 0px; BORDER-BOTTOM: medium none }
	.padtop3 { PADDING-TOP: 3px }
	.lh19 { LINE-HEIGHT: 19px }
	.hand { CURSOR: pointer }
		</style>
		<script language="javascript">
			function NewCreate()
			{
				if(document.Form1.btnCreate.value == "取消")
				{
					
					RestoreState();
					document.Form1.btnCreate.value = "新建";
					document.Form1.txtTag.value = "";
					
					document.Form1.txtModuleName.style.display = "none";
					document.Form1.dropDownListModule.style.display="";
					
				}
				else
				{
					
					SaveState();
					Clear();
					
					document.Form1.btnCreate.value = "取消"
					document.Form1.txtTag.value = "createTag";
					
					document.Form1.txtModuleName.style.display="";
					document.Form1.dropDownListModule.style.display="none";
					
				}						
			}
			//保存状态
			var stateArray = new Array();
			function SaveState()
			{
				
				stateArray[0] = document.Form1.all["tableSettings"].rows[0].style.display;
				stateArray[1] = document.Form1.all["tableSettings"].rows[1].style.display;
				stateArray[2] = document.Form1.all["tableSettings"].rows[2].style.display;
				stateArray[3] = document.Form1.all["tableSettings"].rows[3].style.display;
				//下拉框状态
				stateArray[4] = document.Form1.TextboxDBType.value;
				//
				stateArray[5] = document.Form1.txtSystemNumber.value;
				stateArray[6] = document.Form1.txtUserName.value;
				stateArray[7] = document.Form1.txtPassword.value;
				stateArray[8] = document.Form1.txtServerName.value;
				stateArray[9] = document.Form1.txtDatabase.value;
			}
			//恢复状态
			
			function RestoreState()
			{
				document.Form1.all["tableSettings"].rows[0].style.display = stateArray[0];
				document.Form1.all["tableSettings"].rows[1].style.display = stateArray[1];
				document.Form1.all["tableSettings"].rows[2].style.display = stateArray[2];
				document.Form1.all["tableSettings"].rows[3].style.display = stateArray[3];

				document.Form1.TextboxDBType.value = stateArray[4];
				
				document.Form1.txtSystemNumber.value = stateArray[5];
				document.Form1.txtUserName.value = stateArray[6];
				document.Form1.txtPassword.value = stateArray[7];
				document.Form1.txtServerName.value = stateArray[8];
				document.Form1.txtDatabase.value = stateArray[9];
			}
			
			function Clear()
			{
				document.Form1.txtModuleName.value = "";
				document.Form1.txtSystemNumber.value="";
				document.Form1.txtUserName.value = "";
				document.Form1.txtPassword.value="";
				document.Form1.txtServerName.value="";
				document.Form1.txtDatabase.value="";
				document.Form1.TextboxDBType.value="";
				document.Form1.all["tableSettings"].rows[0].style.display = "none"
				document.Form1.all["tableSettings"].rows[1].style.display = "none"
				document.Form1.all["tableSettings"].rows[2].style.display = "none"
				document.Form1.all["tableSettings"].rows[3].style.display = "none"
			}
			
			function SelectDBType(obj)
			{				
				switch( obj.options[obj.selectedIndex].text )
				{
					case "XML":
					
					document.Form1.all["tableSettings"].rows[0].style.display = "none"
					document.Form1.all["tableSettings"].rows[1].style.display = "none"
					document.Form1.all["tableSettings"].rows[2].style.display = "none"
					document.Form1.all["tableSettings"].rows[3].style.display = "none"
					break;
				
				case "MSSQL":
					
					document.Form1.all["tableSettings"].rows[0].style.display = ""
					document.Form1.all["tableSettings"].rows[1].style.display = ""
					document.Form1.all["tableSettings"].rows[2].style.display = ""
					document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;

				case "ORACEL":
					document.Form1.all["tableSettings"].rows[0].style.display = ""
					document.Form1.all["tableSettings"].rows[1].style.display = ""
					document.Form1.all["tableSettings"].rows[2].style.display = ""
					document.Form1.all["tableSettings"].rows[3].style.display = "none"
					break;

				case "MSACCESS":
					document.Form1.all["tableSettings"].rows[0].style.display = "none"
					document.Form1.all["tableSettings"].rows[1].style.display = "none"
					document.Form1.all["tableSettings"].rows[2].style.display = ""
					document.Form1.all["tableSettings"].rows[3].style.display = "none"
					
					break;

				case "DB2":
					document.Form1.all["tableSettings"].rows[0].style.display = "none"
					document.Form1.all["tableSettings"].rows[1].style.display = "none"
					document.Form1.all["tableSettings"].rows[2].style.display = "none"
					document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;

				case "EXCEL":
					document.Form1.all["tableSettings"].rows[0].style.display = "none"
					document.Form1.all["tableSettings"].rows[1].style.display = "none"
					document.Form1.all["tableSettings"].rows[2].style.display = "none"
					document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;

				case "UNKOWN":
					document.Form1.all["tableSettings"].rows[0].style.display = "none"
					document.Form1.all["tableSettings"].rows[1].style.display = "none"
					document.Form1.all["tableSettings"].rows[2].style.display = "none"
					document.Form1.all["tableSettings"].rows[3].style.display = ""
					break;
				
				default:
					document.Form1.all["tableSettings"].rows[0].style.display = "none"
					document.Form1.all["tableSettings"].rows[1].style.display = "none"
					document.Form1.all["tableSettings"].rows[2].style.display = "none"
					document.Form1.all["tableSettings"].rows[3].style.display = "none"
					break;
				}
				
			}
			//登入
			function Login()
			{
			  if( (document.Form1.dropDownListModule.selectedIndex == 0) ||
				  (document.Form1.txtTag.value == "createTag") )
			  {
				alert("请选择一个单表系统名称！");
			  }
			  else
				document.location.href="default.aspx?DB="
					+ document.Form1.txtSystemNumber.value
					+ "&name=" + document.Form1.dropDownListModule.options[document.Form1.dropDownListModule.selectedIndex].text;
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
			
			//保存是验证
			function SaveValidate()
			{
				var tag = document.Form1.txtTag.value;
				if(tag == "createTag")
				//新建配置文件
				{
					if(document.Form1.txtModuleName.value=="" || document.Form1.txtSystemNumber.value=="")
					{
						alert("系统名称和数据库名称不能为空");
						return false;
					}
				}
				else
				//修改配置文件
				{
					if(document.Form1.dropDownListModule.value == "-1")
					{
						alert("没有选择系统名称");
						return false;
					}
				}
				return true;
			}
			
			//删除是确认
			function DeleteConfirm()
			{
				var delConfirm = confirm("确定要删除吗?");
				if(delConfirm)
				{
					if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
					{
						alert("没有选择要删除的名称");
						delConfirm = false;
					}
				}
				return delConfirm;
			}
			//功能按钮表
			function InitFunctionTable()
			{
				document.Form1.all["Table3"].rows[0].style.display = "";
				document.Form1.all["Table3"].rows[1].style.display = "none";
				document.Form1.all["Table3"].rows[2].style.display = "none";
				document.Form1.all["Table3"].rows[3].style.display = "none";
				document.Form1.all["Table3"].rows[4].style.display = "none";
				document.Form1.all["Table3"].rows[5].style.display = "none";
				document.Form1.all["Table3"].rows[6].style.display = "none";
			}
			
			//***********************导出*****************************
			function Output()
			{
				if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
				{
					alert("选择要导出的系统名称");
					return;
				}
				
				document.Form1.all["Table3"].rows[0].style.display = "none";
				document.Form1.all["Table3"].rows[1].style.display = "";
				document.Form1.all["Table3"].rows[2].style.display = "";
				document.Form1.all["Table3"].rows[3].style.display = "none";
				document.Form1.all["Table3"].rows[4].style.display = "none";
				document.Form1.all["Table3"].rows[5].style.display = "none";

			}
			function ServerScriptOutput()
			{
				if(document.Form1.txtOutputName.value == "")
				{
					alert("没有输入导出名称");
					return false;
				}
				return true;
			}
			

			//**********************复制应用系统****************************
			function CloneSys() {
			    if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
			    {
			    	alert("没有选择系统名称");
			    	return;
			    }
			    document.Form1.all["Table3"].rows[0].style.display = "none";
			    document.Form1.all["Table3"].rows[1].style.display = "none";
			    document.Form1.all["Table3"].rows[2].style.display = "none";
			    document.Form1.all["Table3"].rows[3].style.display = "none";
			    document.Form1.all["Table3"].rows[4].style.display = "";
			    document.Form1.all["Table3"].rows[5].style.display = "";
			    document.Form1.all["Table3"].rows[6].style.display = "";
			    document.Form1.all["btnInputData"].value = "复制到";
			    document.Form1.all["HiddenInputOrClone"].value = document.Form1.all["btnInputData"].value;
			    

			}
			//**********************导入****************************
			function Input()
			{
				//if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
				//{
				//	alert("没有选择系统名称");
				//	return;
				//}
				document.Form1.all["Table3"].rows[0].style.display = "none";
				document.Form1.all["Table3"].rows[1].style.display = "none";
				document.Form1.all["Table3"].rows[2].style.display = "none";
				document.Form1.all["Table3"].rows[3].style.display = "";
				document.Form1.all["Table3"].rows[4].style.display = "";
				document.Form1.all["Table3"].rows[5].style.display = "";
				document.Form1.all["Table3"].rows[6].style.display = "";
				document.Form1.all["btnInputData"].value = "导入";
				document.Form1.all["HiddenInputOrClone"].value = document.Form1.all["btnInputData"].value;
				
			}
			//删除导入的信息
			function InputDelConfirm()
			{
				if(document.Form1.dropDownListInput.value == "-1")
				{
					alert("还没选择要删除的项");
					return false;
				}
				return confirm("确定要删除吗?");
			}
			//导入数据确认
			function InputDataConfirm()
			{
				if (document.Form1.FileOutput.value == "" && document.Form1.btnInputData.value == "导入")
				{
					alert('请先选择要导入的文件，此文件为rar格式的压缩文件');
					return false;
				}
				if(document.Form1.TextboxNewModule.value == "")
				{
					alert('请输入系统业务名称，注意，建议系统名称不要与现在已有的系统重复，否则会覆盖');
					return false;
				}

				return confirm("确定要" + document.Form1.btnInputData.value + "新系统吗？ 如果本系统中存在相同系统名称，则会替换全部功能（如果导入文件中饱含ＸＭＬ数据，则会替换）");
			}
			
			function SelectDB()
			{
				window.open("DBSettings/?returnValue=true",null,"height=600,width=650,status=no,toolbar=no,menubar=no,location=no,scrollbars=1 ");
			}
			
			//选择目录  add:Liufg 2007-09-04
			function ButtSelectModule_onclick()
			{
				var r;
				var r=window.showModalDialog('../RBAC/Modules/SelectModuleDialog.aspx','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
				if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
				{
					return;
				}
				else
				{
					var returns=r.split("\=");
					var s = returns[0].split("\v");
					document.getElementById("txtTemp").value = returns[1];//模块ID
					document.all["txtParent"].value=s[0];//模块名称
					document.all["txtDir"].value=s[0];//模块名称

				}
			}
			//清除父级目录  add:Liufg 2007-09-14
			function ClearModule_onclick()
			{
				
				document.getElementById("txtTemp").value = "";//模块ID
				document.all["txtParent"].value="";//模块名称
				document.all["txtDir"].value="";//模块名称

			}
			//-->
		</script>
		<script language="javascript" event="onclick" for="btSavexx">
			alert('aa');
		</script>
		<script language="javascript" event="onclick" for="btnDelete">
			if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
			{
				alert("没有选择要删除的名称");

			    return false;
			}
			if(confirm('本次删除将不保存还原记录，如果要备份，请使用导出功能备份该模块'))
			    return true;
			    
			return false;
		</script>
		
		<LINK href="../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onclick="ClearMessage()" MS_POSITIONING="GridLayout">
		<%--<table id="btSavexx"><tr><td>&nbsp;测试</td></tr></table>--%>
		<form id="Form1" name="Form1" method="post" runat="server">
			<!-- 页头  -->
			<table id="Table1" width="80%" align="center">
				<tr>
					<td align="center" colspan="3"><strong><FONT face="宋体" size="4">单表系统配置</FONT></strong></td>
				</tr>
				<tr>
					<td width="20%"><asp:textbox id="TextboxQuery" runat="server" Enabled="true" BackColor="LightSteelBlue"></asp:textbox>
					</td>
					<td width="10%">
					    <asp:button id="ButtonQuery" runat="server" Text="查询" 
                            onclick="ButtonQuery_Click"></asp:button>
					</td>
					<td>请输入系统名或数据库名进行查询
					</td>
				</tr>
			</table>
			<br>
			<!-- 模块名称, 编号, 数据库类型 -->
			<table id="Table2" width="80%" align="center" border="1">
				<tr>
					<td width="30%">系统名称</td>
					<td><asp:dropdownlist id="dropDownListModule" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:textbox id="txtModuleName" runat="server"></asp:textbox>
						<asp:Button id="btnAssociate" runat="server" Text="重新在模块树上进行关联" Visible="False"></asp:Button></td>
				</tr>
				<tr>
					<td width="30%">数据库名称</td>
					<td><FONT face="宋体"><asp:textbox id="txtSystemNumber" runat="server" Enabled="true"></asp:textbox></FONT>&nbsp;
						<input id="btnSelectDB" onclick="Javascript:SelectDB()" type="button" value="选择配置" name="btnSelectDB"></td>
				</tr>
				<tr>
					<td>数据库类型</td>
					<td>
						<asp:textbox id="TextboxDBType" runat="server"  contentEditable="false" 
                            BackColor="Silver"></asp:textbox>
						</td>
				</tr>
				<TR>
					<TD><FONT face="宋体">选择父级目录</FONT></TD>
					<TD><FONT face="宋体"><asp:textbox id="txtParent" runat="server" Enabled="true" BackColor="Silver" contentEditable="false"></asp:textbox><INPUT id="selectParent" onclick="ButtSelectModule_onclick()" type="button" value="选择目录">
							<INPUT id="btnClear" type="button" value="清除" onclick="ClearModule_onclick()"></FONT></TD>
				</TR>
			</table>
			<br>
			<!-- 配置体 -->
			<table id="tableSettings" width="80%" align="center" border="1" runat="server">
				<tr>
					<td width="30%">数据库登入名
					</td>
					<td><asp:textbox id="txtUserName" runat="server" Enabled="False"></asp:textbox></td>
				</tr>
				<tr>
					<td width="30%">登入密码</td>
					<td><asp:textbox id="txtPassword" runat="server" Enabled="False"></asp:textbox></td>
				</tr>
				<tr>
					<td width="30%">数据服务器</td>
					<td><asp:textbox id="txtServerName" runat="server" Enabled="False"></asp:textbox><asp:button id="Button1" runat="server" Text="Button"></asp:button></td>
				</tr>
				<tr>
					<td width="30%">数据库</td>
					<td><asp:textbox id="txtDatabase" runat="server" Enabled="False"></asp:textbox></td>
				</tr>
			</table>
			<br>
			<!-- 功能按钮　-->
			<table id="Table3" width="80%" align="center" border="1">
				<tr align="center">
					<td><INPUT onclick="javascript:NewCreate()" type="button" value="新建" name="btnCreate">
					</td>
					<td><asp:button id="btnSave" runat="server" Text="保存"></asp:button></td>
					<td><asp:button id="btnDelete" runat="server" Text="删除"></asp:button></td>
					<td><input onclick="CloneSys()" type="button" value="复制应用系统(Clone)" name="btnClone"></td>
					<td><input onclick="Input()" type="button" value="导入" name="btnInput"></td>
					<td><input onclick="Output()" type="button" value="导出" name="btnOutput"></td>
				</tr>
				<tr align="center">
					<td align="right" colSpan="3" style="HEIGHT: 20px"></td>
					<td colSpan="3" style="HEIGHT: 20px" align="left"><asp:textbox id="txtOutputName" Runat="server" contentEditable="false">压缩包</asp:textbox><asp:checkbox id="ckDataBase" runat="server" Text="导出数据" Checked="True" Enabled="False"></asp:checkbox></td>
				</tr>
				<tr>
					<td align="center" colSpan="3"><asp:button id="btnOutputConfirm" Text="导出" Runat="server"></asp:button></td>
					<td align="center" colSpan="3"><input onclick="InitFunctionTable()" type="button" value="取消" name="btnOutputCancel"></td>
				</tr>
				<tr align="center">
					<td align="right" colSpan="3">选择导入文件名：</td>
					<td align="left" colSpan="3"><FONT face="宋体"></FONT><INPUT id="FileOutput" type="file" name="File1" runat="server" style="WIDTH: 404px; HEIGHT: 23px"
							size="48"><asp:dropdownlist id="dropDownListInput" Runat="server" Visible="False"></asp:dropdownlist><asp:button id="btnInputDelete" Text="删除" Runat="server" Visible="False"></asp:button></td>
				</tr>
				<TR>
					<TD align="right" colSpan="3"><FONT face="宋体">选择父级模块位置：</FONT></TD>
					<TD colSpan="3"><asp:textbox id="txtDir" runat="server" Enabled="true" contentEditable="false"></asp:textbox></FONT><INPUT id="btnSelectDir" onclick="ButtSelectModule_onclick()" type="button" value="选择目录"
							name="Button2"> 
                        <asp:RadioButton ID="RadioButtonChildNode" runat="server" GroupName="RadioNodeLocation" 
                            Text="子节点"  Checked="True" />
                        <asp:RadioButton ID="RadioButtonUp" runat="server" GroupName="RadioNodeLocation" 
                            Text="节点前" Enabled="False" />
                        <asp:RadioButton ID="RadioButtonDown" runat="server" GroupName="RadioNodeLocation" 
                            Text="节点后" Enabled="False" /></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="3"><FONT face="宋体"><b>新的系统名称：</b></FONT></TD>
					<TD colSpan="3"><FONT face="宋体">
                        <input id="HiddenInputOrClone" type="hidden" name="HiddenInputOrClone" runat="server"/>
							<asp:textbox id="TextboxNewModule" runat="server" Enabled="true" BorderColor="Red" BorderStyle="Double"></asp:textbox></FONT></TD>
				</TR>
				<tr align="center">
					<td align="center" colSpan="3"><asp:button id="btnInputData" Text="导入" Runat="server"></asp:button></td>
					<td align="center" colSpan="3"><input onclick="InitFunctionTable()" type="button" value="取消" name="btnOutputCancel"></td>
				</tr>
				<tr>
					<td colSpan="6"><asp:label id="lblMessage" runat="server" EnableViewState="False" ForeColor="#FF8080" Font-Bold="True"></asp:label></td>
				</tr>
			</table>
			<input id="txtTag" style="Z-INDEX: 101; LEFT: 88px; WIDTH: 24px; POSITION: absolute; TOP: 416px; HEIGHT: 22px"
				type="hidden" size="1" name="txtTag" runat="server"> <input id="txtTemp" style="Z-INDEX: 101; LEFT: 88px; WIDTH: 24px; POSITION: absolute; TOP: 416px; HEIGHT: 22px"
				type="hidden" size="1" name="txtTag" runat="server">
		</form>
		<script language="javascript">
			document.Form1.txtModuleName.style.display = "none";
			InitFunctionTable();
		</script>
	</body>
</HTML>
