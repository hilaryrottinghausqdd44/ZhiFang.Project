<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Admin.TablesConfigMain" CodeBehind="TablesConfigMain.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TablesConfigMain</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
			
			//============
			var checkedObj = null;//���汻ѡ��������ID
			//=====================
			function IsSameName()
			{
			    
				var rowsNum = document.Form1.all["Table1"].rows.length;
				if(rowsNum <= 2)
					return true;
			
				for(var i=1; i<rowsNum; i++)
				{					
					sc1 = document.Form1.all["txtBoxCN" + i].value;
					se1 = document.Form1.all["txtBoxEN" + i].value;
					
					for(var j=i+1; j<rowsNum; j++)
					{
						sc2 = document.Form1.all["txtBoxCN" + j].value;
						se2 = document.Form1.all["txtBoxEN" + j].value;
						
						if( (sc1 == sc2) || (se1 == se2) )
						{
							alert("�ֶ���������ͬ");
							return false;
						}
					}
				}
			
				return true;
			} 
			
			function RadioButtonClick(obj)
			{		
				var strID = obj.id;
				//strID ����ʽ��radio+����
				var subNum = strID.substring(5, strID.length);
				
				if(checkedObj == strID)
				{
					document.Form1.all["radio" + subNum].checked = false;
					document.Form1.all["check" + subNum].disabled = false;
					checkedObj = null;
				}
				else{
				//var subNum = strID.substring(5, strID.length);
				
				//table������
				var rowsNum = document.Form1.all["Table1"].rows.length;
				
				for(var i=1; i<rowsNum; i++)
				{
					document.Form1.all["check" + i].disabled = false;
					//document.Form1.all["check" + i].checked = true;
				}
			
				//ѡ�е�����
				document.Form1.all["check" + subNum].checked = false;
				document.Form1.all["check" + subNum].disabled = true;	
				
				checkedObj = strID;
				}	
			}

//***************************������������************************************************
			
			var MyTagRow = 0;//���ڱ��������ѡ�����
			function Details(obj)
			{
				var rowNum;
				switch(obj.type)
				{
					case "text":
					rowNum = (obj.id).substring(8, (obj.id.length));
					break;
					
					case "select-one":
					rowNum = (obj.id).substring(6, (obj.id.length));
					break;
					
					default:
					rowNum = 0;
					break;				
				}
			
				document.Form1.all["lblCName"].innerText =  document.Form1.all["txtBoxCN" + rowNum].value;
				var typeName = document.Form1.all["Select" + rowNum].options[document.Form1.all["Select" + rowNum].selectedIndex].text;
				document.Form1.all["lblType"].innerText = typeName;
				
				//�������������ؿؼ��� 
				MyTagRow = rowNum;
				
				HideRows();//�������е���
				
				var hiddenField = parseInt(document.Form1.all["hidden" + rowNum].value);//�����ֶε�ֵ,ת��������(����)
				
				//==============================���ص�����Դ��Ϣ=====================================
				var hideSource = document.Form1.all["hideSo" + rowNum].value; //Xml�� DataSource
				var hideSourceName = document.Form1.all["hideNa" + rowNum].value; //Xml�� DataSourceName
				//------------------------------------------------------------------------------------
						
				switch(typeName)
				{
					case "����":	
						document.Form1.all["Table2"].rows[2].style.display = "";
							
						document.Form1.all["txtPrecision"].value = hiddenField;				
						    
						break;
					
					case "�ַ�":
						
						break;
											
					case "����":
						document.Form1.all["Table2"].rows[3].style.display = "";						
			
						if(hiddenField > document.Form1.all["dropListDate"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
						
						document.Form1.all["dropListDate"].options[hiddenField].selected = true;
												
						break;
					
					case "�ļ�":
						
						document.Form1.all["Table2"].rows[5].style.display = "";
						
						if(hiddenField > document.Form1.all["dropListFile"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
						
						document.Form1.all["dropListFile"].options[hiddenField].selected = true;
												
						break;
						
					case "��¼����Ϣ":
						
						document.Form1.all["Table2"].rows[7].style.display = "";
						//document.Form1.all["Table2"].rows[10].stye.display = "";
						
						if(hiddenField > document.Form1.all["dropListLogic"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
				
						document.Form1.all["dropListLogic"].options[hiddenField].selected = true;
						
						//==================ֻ����ѡ���¼�߲��ŵ�ʱ�����ʾ=============
						if(hiddenField == 2) //2��ʾ��¼�߲���
						{
							document.Form1.all["Table2"].rows[10].style.display = "";
							document.Form1.all["txtPerson"].value = document.Form1.all["person" + MyTagRow].value;
						}						
						
						break;

						
					case "�б�":
						
						document.Form1.all["Table2"].rows[4].style.display = "";
						
						//=========��ʾ����Դ����=========================
						document.Form1.all["Table2"].rows[8].style.display = "";
						document.Form1.all["Table2"].rows[9].style.display = "";
						//--------------------------------------------------
						
						if(hiddenField > document.Form1.all["dropDownList"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
						
						document.Form1.all["dropDownList"].options[hiddenField].selected = true;
						
						//=======================��������=================
						document.Form1.all["txtDataSource"].value = hideSourceName;	
						
						switch(hideSource)
						{
							case "Local":
							document.Form1.all["dropDownListSource"].options[1].selected = true;
							break;
							
							case "Fixed":
							document.Form1.all["dropDownListSource"].options[2].selected = true;
							break;
							
							case "Remote":
							document.Form1.all["dropDownListSource"].options[3].selected = true;
							break;
							
							default:
							document.Form1.all["dropDownListSource"].options[0].selected = true;
							break;
						}					
						break;
						
					case "������Ϣ":
						
						document.Form1.all["Table2"].rows[6].style.display = "";
						
						if(hiddenField > document.Form1.all["dropListNews"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
						
						document.Form1.all["dropListNews"].options[hiddenField].selected = true;
						
						break;
						
					default:
					break;
				}
			}
			
			//���ر��е���
			function HideRows()
			{
				document.Form1.all["Table2"].rows[2].style.display = "none";
				document.Form1.all["Table2"].rows[3].style.display = "none";
				document.Form1.all["Table2"].rows[4].style.display = "none";
				document.Form1.all["Table2"].rows[5].style.display = "none";
				document.Form1.all["Table2"].rows[6].style.display = "none";
				document.Form1.all["Table2"].rows[7].style.display = "none";
				
				//=============��������Դ����=======================
				document.Form1.all["Table2"].rows[8].style.display = "none";
				document.Form1.all["Table2"].rows[9].style.display = "none";
				
				//=============���������Ϣ=======================
				document.Form1.all["Table2"].rows[10].style.display = "none";
			}
			
			//����ѡ��Ĳ�ͬ������ʾ��ͬ����Ϣ
			function DisplayTypeInfo(obj)
			{
				var displayRow="";
				
				return displayRow;
			}
			
			function LostBlur(obj)
			{
				//document.Form1.all["lblCName"].innerText = "";
				//document.Form1.all["lblType"].innerText = "";
			}
			
			//���������͸ı�
			function SelectChange(obj)
			{
				//var boolTranslate = confirm("ת�����ܶ�ʧ���ݣ�\nȷ��ת����[ȷ��],������[ȡ��]");
				//if(boolTranslate)
				//{
				//	alert("change");
					Details(obj);
				//}
			}
			
//****************************�ı�ĳ���͵�ֵ���������¼�****************************************

			function TypeDetailChange(obj)
			{
				document.Form1.all["hidden" + MyTagRow].value = document.Form1.all[obj.name].value;
				
			}
			
			//================��¼�߲���������仯=============================
			function TypePersonDetailChange(obj)
			{
				//document.Form1.all["hidden" + MyTagRow].value = document.Form1.all[obj.name].value;
				if(document.Form1.all[obj.name].selectedIndex == 2)//����
				{
					document.Form1.all["Table2"].rows[10].style.display = "";
					//document.Form1.all["txtPerson"].value = document.Form1.all["person" + MyTagRow].value; 
				}
				else
				{
					document.Form1.all["Table2"].rows[10].style.display = "none";
					document.Form1.all["person" + MyTagRow].value = "";
				}
				document.Form1.all["hidden" + MyTagRow].value = document.Form1.all[obj.name].value;	
			}
			
			function DropDataSourceChange(obj)
			{
				switch(document.Form1.all[obj.name].value)
				{
					case "0":
					document.Form1.all["hideSo" + MyTagRow].value = "";
					break;
					
					case "1":
					document.Form1.all["hideSo" + MyTagRow].value = "Local"; //�ֵ��
					
					break;
					
					case "2":
					document.Form1.all["hideSo" + MyTagRow].value = "Fixed"; //�̶��б�
					break;
					
					case "3":
					document.Form1.all["hideSo" + MyTagRow].value = "Remote";
					break;
				}
				//==================����ı���?��bug=========================
				document.Form1.all["txtDataSource"].value = "";
				
				//=================================================
			}
			
			function DataSourceChange(obj)
			{
				document.Form1.all["hideNa" + MyTagRow].value = document.Form1.all[obj.name].value;
			}
			
			//============���˲��������ı�======
			function PersonDataChange(obj)
			{
				document.Form1.all["person" + MyTagRow].value = document.Form1.all[obj.name].value;
			}
			//==================End====================
			
			//���ȶ�ʧ�˽��㣬Ҫ�ж���������Ƿ�������
			function PrecLostFocus(obj)
			{
				var i = document.Form1.all[obj.name].value;
				
				if( isNaN(i) )
				{
					alert("����ĸ�ʽ����");
					//document.Form1.all[obj.name].focus();
					document.Form1.all[obj.name].select();
					return false;
				}
			}
			
			//===============================ѡ������Դ����������==================================
			function PopWindows()
			{
				//==========���������ı�����ǰ��ֵ====================
				
				switch(document.Form1.all["dropDownListSource"].value)
				{
					case "1"://�ֵ��
					var r = window.showModalDialog("PromptSelectDictionary.aspx", "", "");
					
					if ( typeof(r) != 'undefined')
					{
						document.Form1.all["txtDataSource"].value = r.replace(" ","");
					}
					
					//===============�ı����ص�ֵ====================
					//DataSourceChange(document.Form1.all["txtDataSource"]);
					
					break;
					
					case "2"://�̶��б�
					var r = window.showModalDialog("PromptSelectFixList.aspx", document.Form1.all["txtDataSource"].value, "");
					if( typeof(r) != 'undefined')
					{
						document.Form1.all["txtDataSource"].value = r;
					}
					break;
					
					case "3"://��̬�б�
					var r = window.showModalDialog("PromptSelectDynamicList.aspx", "", "");
					if( typeof(r) != 'undefined')
					{
						document.Form1.all["txtDataSource"].value = r;
					}
					break;
					
					case "0":
					alert("û��ѡ��������Դ");
					break;
				}
				DataSourceChange(document.Form1.all["txtDataSource"]);
			}
			//=======================���������Ϣ======================
			function PopPerson()
			{
				var r= window.showModalDialog("PersonDepartment.aspx", "", "");
				if ( typeof(r) != 'undefined')
				{
					document.Form1.all["txtPerson"].value = r;
					PersonDataChange(document.Form1.all["txtPerson"]);
				}
			}
			
			//=========================����==========================
			function FieldSort()
			{
				con = window.confirm("�������û�б���,��������ʱ����ʧ,ȷ���ȱ����˸��ġ�ȷ��Ҫ������");
				if(con == false)
					return false;
				
				var tableName = document.Form1.all["tableName"].innerText;
				var r = window.showModalDialog("FieldSort.aspx?<%=Request.ServerVariables["Query_String"]%>", "", 
					"status:yes;resizable:yes;dialogHeight:560px;dialogWidth:780px;center:yes");
				//if( typeof(r) != 'undefined')
				//{
					document.location = document.location;
				//}
			}
			
			
			//=============================˫��������Ĭ��ֵ=================
			function DBClickDefault(obj)
			{
				//objSelects=obj.parentElement.parentElement.getElementsByTagName("select");
				//for(var i=0;i<objSelects.length;i++)
				//{
				//	alert(objSelects[i].name);
				//}
				//var dataTypeSelect=false;
				//if(objSelect.options[objSelect.selectedIndex].value=="4")
				//	dataTypeSelect=true;
				//alert(obj.parentElement.parentElement.getElementsByTagName("select")[0].name);
				//alert(objSelect.options[objSelect.selectedIndex].value);
				
				var objSelect=obj.parentElement.parentElement.getElementsByTagName("select")[0];
				///�Ƿ�����
				var objSelectValue=objSelect.options[objSelect.selectedIndex].value;
				var objSelectText=objSelect.options[objSelect.selectedIndex].text;
				window.status=objSelectText;
				switch(objSelectValue)
				{
					case "3":
						alert('�ļ����ͣ�����������Ĭ��ֵ');
						obj.style.backgroundColor="red";
						break;
					case "4":
						var objValue=obj.value//.replace("$ģ��:","");
						
						var DlgRtnValue = window.showModalDialog("PromptSelectDialog.aspx?WinOpenDefaultValueTemplet.aspx?DefaultValue="
							+ escape(objValue), "", "status:yes;resizable:yes;dialogHeight:560px;dialogWidth:780px;center:yes");
						if(DlgRtnValue != void 0)
						{
							obj.value = DlgRtnValue;
						}
						
						break;
					default:
						var DlgRtnValue = window.showModalDialog("WinOpenDefaultValueModalDialog.aspx", "", "status:no;resizable:no");
						if(DlgRtnValue != void 0)
						{
							obj.value = DlgRtnValue;
						}
						break;
				}
				
			}
			//=================================End==========================
			
			function ColumnEdit(name, username, password,database,TableName)
			{
			    Form2.Server.value=name;
			    Form2.username.value=username;
			    Form2.password.value=password;
			    Form2.database.value=database;
			    Form2.table.value=TableName;
			    //var sUrl='/SqlWebAdmin2008/columns.aspx?database='+database+'&table='+TableName;
			    var sUrl='/SqlWebAdmin2008/columns.aspx?database='+database+'&table='+TableName;
			    if(window.confirm('��С�Ĳ�����ֻ�޸��ֶ����ͣ����ȣ�˵���ȣ���Ҫ�������ݿ�Ĺؼ����ƣ�λ�õ���Ϣ'
			    +'\n\n���������Ҫ��¼������������ݿ�ָ��Ļ���������¼��������\n\n'
			    + '���ݿ����ӿ�����Ҫ�ܳ�ʱ�䣬��Ⱥ�.......ȷ��Ҫȥ�޸���')){
			        Form2.submit();
			    }
			    //window.open(sUrl);
			}
		</script>
		<script language="javascript" for="newField" event="onclick">
			var r=window.showModalDialog('../input/SelectModalDialog.aspx?../Admin/WinOpenSelectFields.aspx?<%=Request.ServerVariables["Query_String"]%>','','dialogWidth:458px;dialogHeight:528px;help:no;scroll:auto;status:no');
			if (r == '' || typeof(r) == 'undefined')
			{
				return false;
			}
			Form1.hSelectedFields.value=r;
			__doPostBack('newField','');
			
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!-- span �е�id���ܸĺ�ɾ�� -->
			<P><FONT face="����"><IMG src="../images/icons/0041_a.gif">�����߼���ϵ:</FONT><FONT face="����" size="5"><span id="tableName"><%Response.Write(Request.QueryString["TableName"]);%></span></FONT></P>
			<P>&nbsp;<input id="newField" type="button" value="ѡ���ֶ�" name="newField" runat="server" onserverclick="newField_ServerClick">&nbsp;
				<asp:button id="btnSave" runat="server" Text="����" onclick="btnSave_Click"></asp:button>
				&nbsp;<input type="button" value="����" name="btnSort" onclick="return FieldSort()">
				&nbsp;<input type="button" value="���ݿ��ֶα༭" id="btnColumnEdit" name="btnColumnEdit" runat="server"></P>
			<TABLE class="blackborder12px" id="Table1" cellSpacing="1" cellPadding="0" width="100%"
				border="0" runat="server">
				<TR>
					<TD noWrap width="50">����
					</TD>
					<td noWrap width="50">����
					</td>
					<TD noWrap align="center" width="80">�ֶ�������
					</TD>
					<TD noWrap align="center" width="80">�ֶ�Ӣ����
					</TD>
					<TD noWrap align="center" width="80">����
					</TD>
					<TD noWrap>�����
					</TD>
					<TD noWrap>Ĭ��ֵ
					</TD>
					<td noWrap>ֻ��
					</td>
					<TD noWrap><FONT face="����">ɾ��</FONT></TD>
					<TD noWrap>
						<!--�������ֶ�--></TD>
					<td></td>
					<td></td>
					<td><!--��¼����Ϣ--></td>
					<td><!--�����߲���������Դ--></td>
				</TR>
			</TABLE>
			<p>
				<table id="Table2" cellSpacing="1" cellPadding="0" width="50%" bgColor="#ffffff" border="1">
					<tr>
						<td style="WIDTH: 104px">�����ֶ���</td>
						<td><asp:label id="lblCName" Runat="server"></asp:label></td>
					</tr>
					<tr>
						<td style="WIDTH: 104px">����</td>
						<td><asp:label id="lblType" Runat="server"></asp:label></td>
					</tr>
					<tr>
						<td><asp:label id="lblPrecision" Runat="server">����</asp:label></td>
						<td><asp:textbox id="txtPrecision" Runat="server" Width="88px"></asp:textbox>
							<!--�������������ҵ���Ӧ����--></td>
					</tr>
					<tr>
						<td><asp:label id="lblDate" Runat="server">���ڸ�ʽ</asp:label></td>
						<td><asp:dropdownlist id="dropListDate" Runat="server" Width="105">
								<asp:ListItem Value="0">--ѡ�����ڸ�ʽ--</asp:ListItem>
								<asp:ListItem Value="1">YYYY-MM-DD</asp:ListItem>
								<asp:ListItem Value="2">YYYY-MM-DD-HH</asp:ListItem>
								<asp:ListItem Value="3">YYYY-MM-DD-HH-MM</asp:ListItem>
								<asp:ListItem Value="4">YYYY-MM-DD-HH-MM-SS</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td><asp:label id="lblDropDownList" Runat="server">�б�����</asp:label></td>
						<td><asp:dropdownlist id="dropDownList" Runat="server" Width="105">
								<asp:ListItem Value="0">--ѡ���б�����--</asp:ListItem>
								<asp:ListItem Value="1">��ѡ</asp:ListItem>
								<asp:ListItem Value="2">��ѡ</asp:ListItem>
								<asp:ListItem Value="3">Radio��ѡ</asp:ListItem>
								<asp:ListItem Value="4">Check��ѡ</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td><asp:label id="lblFile" Runat="server">�ļ�</asp:label></td>
						<td><asp:dropdownlist id="dropListFile" Runat="server" Width="105">
								<asp:ListItem Value="0">--�ļ���������--</asp:ListItem>
								<asp:ListItem Value="1">���ش洢</asp:ListItem>
								<asp:ListItem Value="2">����</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="HEIGHT: 17px"><asp:label id="lblNews" Runat="server">����</asp:label></td>
						<td style="HEIGHT: 17px"><asp:dropdownlist id="dropListNews" Runat="server" Width="105">
								<asp:ListItem Value="0">--���ű�������--</asp:ListItem>
								<asp:ListItem Value="1">���ش洢</asp:ListItem>
								<asp:ListItem Value="2">����</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td><asp:label id="lblLogic" Runat="server">��¼����Ϣ</asp:label></td>
						<td><asp:dropdownlist id="dropListLogic" Runat="server" Width="105">
								<asp:ListItem Value="0">--ѡ������--</asp:ListItem>
								<asp:ListItem Value="1">��¼������</asp:ListItem>
								<asp:ListItem Value="2">��¼�߲���</asp:ListItem>
								<asp:ListItem Value="3">��¼��ְλ</asp:ListItem>
								<asp:ListItem Value="4">��¼�߸�λ</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<!--�����ӵģ�ѡ������Դ-->
					<tr>
						<td>������Դ
						</td>
						<td><asp:dropdownlist id="dropDownListSource" Runat="server" Width="105">
								<asp:ListItem Value="0">--ѡ��Դ--</asp:ListItem>
								<asp:ListItem Value="1">�ֵ��</asp:ListItem>
								<asp:ListItem Value="2">�̶��б�</asp:ListItem>
								<asp:ListItem Value="3">��̬�б�</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td>����Դ��</td>
						<td><asp:textbox id="txtDataSource" Runat="server" Width="105"></asp:textbox><input id="btnSelectSource" onclick="PopWindows()" type="button" value="ѡ��">
						</td>
					</tr>
					<!--���������Ϣ-->
					<tr>
						<td>����Դ</td>
						<td><asp:textbox id="txtPerson" Runat="server" Width="105"></asp:textbox><input id="btnPerson" onclick="PopPerson()" type="button" value="ѡ��">
						</td>
					</tr>
				</table>
			</p>
			<script language="javascript">
				document.Form1.all["Table2"].rows[2].style.display = "none";
				document.Form1.all["Table2"].rows[3].style.display = "none";
				document.Form1.all["Table2"].rows[4].style.display = "none";
				document.Form1.all["Table2"].rows[5].style.display = "none";
				document.Form1.all["Table2"].rows[6].style.display = "none";
				document.Form1.all["Table2"].rows[7].style.display = "none";
				
				//===============����������==============================
				document.Form1.all["Table2"].rows[8].style.display = "none";
				document.Form1.all["Table2"].rows[9].style.display = "none";
				
				//================���������Ϣ=======================
				document.Form1.all["Table2"].rows[10].style.display = "none";
				
				//=================�ҵ��ĸ�������ѡ��========================
				var tableRowsNum = document.Form1.all["Table1"].rows.length;
				for(var rowsNumber=1; rowsNumber<tableRowsNum; rowsNumber++)
				{
					
					if(!document.Form1.all["check" + rowsNumber].checked)
					{
		
						checkedObj = document.Form1.all["radio" + rowsNumber].id;
					}
				}
				//=====================End======================================
			</script>
			<input type="hidden" name="hSelectedFields" id="hSelectedFields">
		</form>
		<form id="Form2" name="Form2" action="/SqlWebAdmin2008/DefaultNew.aspx" target="_blank" method="post">
		    <input type="hidden" name="Server" id="Server">
		    <input type="hidden" name="username" id="username">
		    <input type="hidden" name="password" id="password">
		    <input type="hidden" name="database" id="database">
		    <input type="hidden" name="table" id="table">
		</form>
	</body>
</HTML>
