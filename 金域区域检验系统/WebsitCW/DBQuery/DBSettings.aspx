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
				if(document.Form1.btnCreate.value == "ȡ��")
				{
					
					RestoreState();
					document.Form1.btnCreate.value = "�½�";
					document.Form1.txtTag.value = "";
					
					document.Form1.txtModuleName.style.display = "none";
					document.Form1.dropDownListModule.style.display="";
					
				}
				else
				{
					
					SaveState();
					Clear();
					
					document.Form1.btnCreate.value = "ȡ��"
					document.Form1.txtTag.value = "createTag";
					
					document.Form1.txtModuleName.style.display="";
					document.Form1.dropDownListModule.style.display="none";
					
				}						
			}
			//����״̬
			var stateArray = new Array();
			function SaveState()
			{
				
				stateArray[0] = document.Form1.all["tableSettings"].rows[0].style.display;
				stateArray[1] = document.Form1.all["tableSettings"].rows[1].style.display;
				stateArray[2] = document.Form1.all["tableSettings"].rows[2].style.display;
				stateArray[3] = document.Form1.all["tableSettings"].rows[3].style.display;
				//������״̬
				stateArray[4] = document.Form1.TextboxDBType.value;
				//
				stateArray[5] = document.Form1.txtSystemNumber.value;
				stateArray[6] = document.Form1.txtUserName.value;
				stateArray[7] = document.Form1.txtPassword.value;
				stateArray[8] = document.Form1.txtServerName.value;
				stateArray[9] = document.Form1.txtDatabase.value;
			}
			//�ָ�״̬
			
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
			//����
			function Login()
			{
			  if( (document.Form1.dropDownListModule.selectedIndex == 0) ||
				  (document.Form1.txtTag.value == "createTag") )
			  {
				alert("��ѡ��һ������ϵͳ���ƣ�");
			  }
			  else
				document.location.href="default.aspx?DB="
					+ document.Form1.txtSystemNumber.value
					+ "&name=" + document.Form1.dropDownListModule.options[document.Form1.dropDownListModule.selectedIndex].text;
			}
			
			
			//�����ʾ��ϢlblMessage
			function ClearMessage()
			{
				document.Form1.all["lblMessage"].innerText = "";
			}
			
			function beforeDelete()
			{				
				return(confirm('���ݿ��ļ�Ҳɾ����'));
			}
			
			//��������֤
			function SaveValidate()
			{
				var tag = document.Form1.txtTag.value;
				if(tag == "createTag")
				//�½������ļ�
				{
					if(document.Form1.txtModuleName.value=="" || document.Form1.txtSystemNumber.value=="")
					{
						alert("ϵͳ���ƺ����ݿ����Ʋ���Ϊ��");
						return false;
					}
				}
				else
				//�޸������ļ�
				{
					if(document.Form1.dropDownListModule.value == "-1")
					{
						alert("û��ѡ��ϵͳ����");
						return false;
					}
				}
				return true;
			}
			
			//ɾ����ȷ��
			function DeleteConfirm()
			{
				var delConfirm = confirm("ȷ��Ҫɾ����?");
				if(delConfirm)
				{
					if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
					{
						alert("û��ѡ��Ҫɾ��������");
						delConfirm = false;
					}
				}
				return delConfirm;
			}
			//���ܰ�ť��
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
			
			//***********************����*****************************
			function Output()
			{
				if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
				{
					alert("ѡ��Ҫ������ϵͳ����");
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
					alert("û�����뵼������");
					return false;
				}
				return true;
			}
			

			//**********************����Ӧ��ϵͳ****************************
			function CloneSys() {
			    if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
			    {
			    	alert("û��ѡ��ϵͳ����");
			    	return;
			    }
			    document.Form1.all["Table3"].rows[0].style.display = "none";
			    document.Form1.all["Table3"].rows[1].style.display = "none";
			    document.Form1.all["Table3"].rows[2].style.display = "none";
			    document.Form1.all["Table3"].rows[3].style.display = "none";
			    document.Form1.all["Table3"].rows[4].style.display = "";
			    document.Form1.all["Table3"].rows[5].style.display = "";
			    document.Form1.all["Table3"].rows[6].style.display = "";
			    document.Form1.all["btnInputData"].value = "���Ƶ�";
			    document.Form1.all["HiddenInputOrClone"].value = document.Form1.all["btnInputData"].value;
			    

			}
			//**********************����****************************
			function Input()
			{
				//if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
				//{
				//	alert("û��ѡ��ϵͳ����");
				//	return;
				//}
				document.Form1.all["Table3"].rows[0].style.display = "none";
				document.Form1.all["Table3"].rows[1].style.display = "none";
				document.Form1.all["Table3"].rows[2].style.display = "none";
				document.Form1.all["Table3"].rows[3].style.display = "";
				document.Form1.all["Table3"].rows[4].style.display = "";
				document.Form1.all["Table3"].rows[5].style.display = "";
				document.Form1.all["Table3"].rows[6].style.display = "";
				document.Form1.all["btnInputData"].value = "����";
				document.Form1.all["HiddenInputOrClone"].value = document.Form1.all["btnInputData"].value;
				
			}
			//ɾ���������Ϣ
			function InputDelConfirm()
			{
				if(document.Form1.dropDownListInput.value == "-1")
				{
					alert("��ûѡ��Ҫɾ������");
					return false;
				}
				return confirm("ȷ��Ҫɾ����?");
			}
			//��������ȷ��
			function InputDataConfirm()
			{
				if (document.Form1.FileOutput.value == "" && document.Form1.btnInputData.value == "����")
				{
					alert('����ѡ��Ҫ������ļ������ļ�Ϊrar��ʽ��ѹ���ļ�');
					return false;
				}
				if(document.Form1.TextboxNewModule.value == "")
				{
					alert('������ϵͳҵ�����ƣ�ע�⣬����ϵͳ���Ʋ�Ҫ���������е�ϵͳ�ظ�������Ḳ��');
					return false;
				}

				return confirm("ȷ��Ҫ" + document.Form1.btnInputData.value + "��ϵͳ�� �����ϵͳ�д�����ͬϵͳ���ƣ�����滻ȫ�����ܣ���������ļ��б����أͣ����ݣ�����滻��");
			}
			
			function SelectDB()
			{
				window.open("DBSettings/?returnValue=true",null,"height=600,width=650,status=no,toolbar=no,menubar=no,location=no,scrollbars=1 ");
			}
			
			//ѡ��Ŀ¼  add:Liufg 2007-09-04
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
					document.getElementById("txtTemp").value = returns[1];//ģ��ID
					document.all["txtParent"].value=s[0];//ģ������
					document.all["txtDir"].value=s[0];//ģ������

				}
			}
			//�������Ŀ¼  add:Liufg 2007-09-14
			function ClearModule_onclick()
			{
				
				document.getElementById("txtTemp").value = "";//ģ��ID
				document.all["txtParent"].value="";//ģ������
				document.all["txtDir"].value="";//ģ������

			}
			//-->
		</script>
		<script language="javascript" event="onclick" for="btSavexx">
			alert('aa');
		</script>
		<script language="javascript" event="onclick" for="btnDelete">
			if((document.Form1.dropDownListModule.value == "-1") || (document.Form1.txtTag.value !="") )
			{
				alert("û��ѡ��Ҫɾ��������");

			    return false;
			}
			if(confirm('����ɾ���������滹ԭ��¼�����Ҫ���ݣ���ʹ�õ������ܱ��ݸ�ģ��'))
			    return true;
			    
			return false;
		</script>
		
		<LINK href="../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onclick="ClearMessage()" MS_POSITIONING="GridLayout">
		<%--<table id="btSavexx"><tr><td>&nbsp;����</td></tr></table>--%>
		<form id="Form1" name="Form1" method="post" runat="server">
			<!-- ҳͷ  -->
			<table id="Table1" width="80%" align="center">
				<tr>
					<td align="center" colspan="3"><strong><FONT face="����" size="4">����ϵͳ����</FONT></strong></td>
				</tr>
				<tr>
					<td width="20%"><asp:textbox id="TextboxQuery" runat="server" Enabled="true" BackColor="LightSteelBlue"></asp:textbox>
					</td>
					<td width="10%">
					    <asp:button id="ButtonQuery" runat="server" Text="��ѯ" 
                            onclick="ButtonQuery_Click"></asp:button>
					</td>
					<td>������ϵͳ�������ݿ������в�ѯ
					</td>
				</tr>
			</table>
			<br>
			<!-- ģ������, ���, ���ݿ����� -->
			<table id="Table2" width="80%" align="center" border="1">
				<tr>
					<td width="30%">ϵͳ����</td>
					<td><asp:dropdownlist id="dropDownListModule" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:textbox id="txtModuleName" runat="server"></asp:textbox>
						<asp:Button id="btnAssociate" runat="server" Text="������ģ�����Ͻ��й���" Visible="False"></asp:Button></td>
				</tr>
				<tr>
					<td width="30%">���ݿ�����</td>
					<td><FONT face="����"><asp:textbox id="txtSystemNumber" runat="server" Enabled="true"></asp:textbox></FONT>&nbsp;
						<input id="btnSelectDB" onclick="Javascript:SelectDB()" type="button" value="ѡ������" name="btnSelectDB"></td>
				</tr>
				<tr>
					<td>���ݿ�����</td>
					<td>
						<asp:textbox id="TextboxDBType" runat="server"  contentEditable="false" 
                            BackColor="Silver"></asp:textbox>
						</td>
				</tr>
				<TR>
					<TD><FONT face="����">ѡ�񸸼�Ŀ¼</FONT></TD>
					<TD><FONT face="����"><asp:textbox id="txtParent" runat="server" Enabled="true" BackColor="Silver" contentEditable="false"></asp:textbox><INPUT id="selectParent" onclick="ButtSelectModule_onclick()" type="button" value="ѡ��Ŀ¼">
							<INPUT id="btnClear" type="button" value="���" onclick="ClearModule_onclick()"></FONT></TD>
				</TR>
			</table>
			<br>
			<!-- ������ -->
			<table id="tableSettings" width="80%" align="center" border="1" runat="server">
				<tr>
					<td width="30%">���ݿ������
					</td>
					<td><asp:textbox id="txtUserName" runat="server" Enabled="False"></asp:textbox></td>
				</tr>
				<tr>
					<td width="30%">��������</td>
					<td><asp:textbox id="txtPassword" runat="server" Enabled="False"></asp:textbox></td>
				</tr>
				<tr>
					<td width="30%">���ݷ�����</td>
					<td><asp:textbox id="txtServerName" runat="server" Enabled="False"></asp:textbox><asp:button id="Button1" runat="server" Text="Button"></asp:button></td>
				</tr>
				<tr>
					<td width="30%">���ݿ�</td>
					<td><asp:textbox id="txtDatabase" runat="server" Enabled="False"></asp:textbox></td>
				</tr>
			</table>
			<br>
			<!-- ���ܰ�ť��-->
			<table id="Table3" width="80%" align="center" border="1">
				<tr align="center">
					<td><INPUT onclick="javascript:NewCreate()" type="button" value="�½�" name="btnCreate">
					</td>
					<td><asp:button id="btnSave" runat="server" Text="����"></asp:button></td>
					<td><asp:button id="btnDelete" runat="server" Text="ɾ��"></asp:button></td>
					<td><input onclick="CloneSys()" type="button" value="����Ӧ��ϵͳ(Clone)" name="btnClone"></td>
					<td><input onclick="Input()" type="button" value="����" name="btnInput"></td>
					<td><input onclick="Output()" type="button" value="����" name="btnOutput"></td>
				</tr>
				<tr align="center">
					<td align="right" colSpan="3" style="HEIGHT: 20px"></td>
					<td colSpan="3" style="HEIGHT: 20px" align="left"><asp:textbox id="txtOutputName" Runat="server" contentEditable="false">ѹ����</asp:textbox><asp:checkbox id="ckDataBase" runat="server" Text="��������" Checked="True" Enabled="False"></asp:checkbox></td>
				</tr>
				<tr>
					<td align="center" colSpan="3"><asp:button id="btnOutputConfirm" Text="����" Runat="server"></asp:button></td>
					<td align="center" colSpan="3"><input onclick="InitFunctionTable()" type="button" value="ȡ��" name="btnOutputCancel"></td>
				</tr>
				<tr align="center">
					<td align="right" colSpan="3">ѡ�����ļ�����</td>
					<td align="left" colSpan="3"><FONT face="����"></FONT><INPUT id="FileOutput" type="file" name="File1" runat="server" style="WIDTH: 404px; HEIGHT: 23px"
							size="48"><asp:dropdownlist id="dropDownListInput" Runat="server" Visible="False"></asp:dropdownlist><asp:button id="btnInputDelete" Text="ɾ��" Runat="server" Visible="False"></asp:button></td>
				</tr>
				<TR>
					<TD align="right" colSpan="3"><FONT face="����">ѡ�񸸼�ģ��λ�ã�</FONT></TD>
					<TD colSpan="3"><asp:textbox id="txtDir" runat="server" Enabled="true" contentEditable="false"></asp:textbox></FONT><INPUT id="btnSelectDir" onclick="ButtSelectModule_onclick()" type="button" value="ѡ��Ŀ¼"
							name="Button2"> 
                        <asp:RadioButton ID="RadioButtonChildNode" runat="server" GroupName="RadioNodeLocation" 
                            Text="�ӽڵ�"  Checked="True" />
                        <asp:RadioButton ID="RadioButtonUp" runat="server" GroupName="RadioNodeLocation" 
                            Text="�ڵ�ǰ" Enabled="False" />
                        <asp:RadioButton ID="RadioButtonDown" runat="server" GroupName="RadioNodeLocation" 
                            Text="�ڵ��" Enabled="False" /></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="3"><FONT face="����"><b>�µ�ϵͳ���ƣ�</b></FONT></TD>
					<TD colSpan="3"><FONT face="����">
                        <input id="HiddenInputOrClone" type="hidden" name="HiddenInputOrClone" runat="server"/>
							<asp:textbox id="TextboxNewModule" runat="server" Enabled="true" BorderColor="Red" BorderStyle="Double"></asp:textbox></FONT></TD>
				</TR>
				<tr align="center">
					<td align="center" colSpan="3"><asp:button id="btnInputData" Text="����" Runat="server"></asp:button></td>
					<td align="center" colSpan="3"><input onclick="InitFunctionTable()" type="button" value="ȡ��" name="btnOutputCancel"></td>
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
