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
			var checkedObj = null;//保存被选中主键的ID
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
							alert("字段名不能相同");
							return false;
						}
					}
				}
			
				return true;
			} 
			
			function RadioButtonClick(obj)
			{		
				var strID = obj.id;
				//strID 的形式是radio+数字
				var subNum = strID.substring(5, strID.length);
				
				if(checkedObj == strID)
				{
					document.Form1.all["radio" + subNum].checked = false;
					document.Form1.all["check" + subNum].disabled = false;
					checkedObj = null;
				}
				else{
				//var subNum = strID.substring(5, strID.length);
				
				//table的行数
				var rowsNum = document.Form1.all["Table1"].rows.length;
				
				for(var i=1; i<rowsNum; i++)
				{
					document.Form1.all["check" + i].disabled = false;
					//document.Form1.all["check" + i].checked = true;
				}
			
				//选中的主键
				document.Form1.all["check" + subNum].checked = false;
				document.Form1.all["check" + subNum].disabled = true;	
				
				checkedObj = strID;
				}	
			}

//***************************点击中文输入框************************************************
			
			var MyTagRow = 0;//用于保存所点击选择的行
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
				
				//保存行数到隐藏控件中 
				MyTagRow = rowNum;
				
				HideRows();//隐藏所有的行
				
				var hiddenField = parseInt(document.Form1.all["hidden" + rowNum].value);//隐藏字段的值,转换成整型(精度)
				
				//==============================隐藏的数据源信息=====================================
				var hideSource = document.Form1.all["hideSo" + rowNum].value; //Xml中 DataSource
				var hideSourceName = document.Form1.all["hideNa" + rowNum].value; //Xml中 DataSourceName
				//------------------------------------------------------------------------------------
						
				switch(typeName)
				{
					case "数字":	
						document.Form1.all["Table2"].rows[2].style.display = "";
							
						document.Form1.all["txtPrecision"].value = hiddenField;				
						    
						break;
					
					case "字符":
						
						break;
											
					case "日期":
						document.Form1.all["Table2"].rows[3].style.display = "";						
			
						if(hiddenField > document.Form1.all["dropListDate"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
						
						document.Form1.all["dropListDate"].options[hiddenField].selected = true;
												
						break;
					
					case "文件":
						
						document.Form1.all["Table2"].rows[5].style.display = "";
						
						if(hiddenField > document.Form1.all["dropListFile"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
						
						document.Form1.all["dropListFile"].options[hiddenField].selected = true;
												
						break;
						
					case "登录者信息":
						
						document.Form1.all["Table2"].rows[7].style.display = "";
						//document.Form1.all["Table2"].rows[10].stye.display = "";
						
						if(hiddenField > document.Form1.all["dropListLogic"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
				
						document.Form1.all["dropListLogic"].options[hiddenField].selected = true;
						
						//==================只有在选择登录者部门的时候才显示=============
						if(hiddenField == 2) //2表示登录者部门
						{
							document.Form1.all["Table2"].rows[10].style.display = "";
							document.Form1.all["txtPerson"].value = document.Form1.all["person" + MyTagRow].value;
						}						
						
						break;

						
					case "列表":
						
						document.Form1.all["Table2"].rows[4].style.display = "";
						
						//=========显示数据源的行=========================
						document.Form1.all["Table2"].rows[8].style.display = "";
						document.Form1.all["Table2"].rows[9].style.display = "";
						//--------------------------------------------------
						
						if(hiddenField > document.Form1.all["dropDownList"].length-1)
						{
							hiddenField = 0;
							document.Form1.all["hidden" + rowNum].value = 0;
						}
						
						document.Form1.all["dropDownList"].options[hiddenField].selected = true;
						
						//=======================隐藏数据=================
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
						
					case "新闻信息":
						
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
			
			//隐藏表中的行
			function HideRows()
			{
				document.Form1.all["Table2"].rows[2].style.display = "none";
				document.Form1.all["Table2"].rows[3].style.display = "none";
				document.Form1.all["Table2"].rows[4].style.display = "none";
				document.Form1.all["Table2"].rows[5].style.display = "none";
				document.Form1.all["Table2"].rows[6].style.display = "none";
				document.Form1.all["Table2"].rows[7].style.display = "none";
				
				//=============隐藏数据源的行=======================
				document.Form1.all["Table2"].rows[8].style.display = "none";
				document.Form1.all["Table2"].rows[9].style.display = "none";
				
				//=============个人身份信息=======================
				document.Form1.all["Table2"].rows[10].style.display = "none";
			}
			
			//根据选择的不同类型显示不同的信息
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
			
			//下拉框类型改变
			function SelectChange(obj)
			{
				//var boolTranslate = confirm("转换可能丢失数据！\n确定转换点[确定],放弃点[取消]");
				//if(boolTranslate)
				//{
				//	alert("change");
					Details(obj);
				//}
			}
			
//****************************改变某类型的值，触发的事件****************************************

			function TypeDetailChange(obj)
			{
				document.Form1.all["hidden" + MyTagRow].value = document.Form1.all[obj.name].value;
				
			}
			
			//================登录者部门下拉框变化=============================
			function TypePersonDetailChange(obj)
			{
				//document.Form1.all["hidden" + MyTagRow].value = document.Form1.all[obj.name].value;
				if(document.Form1.all[obj.name].selectedIndex == 2)//部门
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
					document.Form1.all["hideSo" + MyTagRow].value = "Local"; //字典表
					
					break;
					
					case "2":
					document.Form1.all["hideSo" + MyTagRow].value = "Fixed"; //固定列表
					break;
					
					case "3":
					document.Form1.all["hideSo" + MyTagRow].value = "Remote";
					break;
				}
				//==================清空文本框?有bug=========================
				document.Form1.all["txtDataSource"].value = "";
				
				//=================================================
			}
			
			function DataSourceChange(obj)
			{
				document.Form1.all["hideNa" + MyTagRow].value = document.Form1.all[obj.name].value;
			}
			
			//============个人部门输入框改变======
			function PersonDataChange(obj)
			{
				document.Form1.all["person" + MyTagRow].value = document.Form1.all[obj.name].value;
			}
			//==================End====================
			
			//精度丢失了焦点，要判断所输入的是否是数字
			function PrecLostFocus(obj)
			{
				var i = document.Form1.all[obj.name].value;
				
				if( isNaN(i) )
				{
					alert("输入的格式不对");
					//document.Form1.all[obj.name].focus();
					document.Form1.all[obj.name].select();
					return false;
				}
			}
			
			//===============================选择数据源，弹出窗口==================================
			function PopWindows()
			{
				//==========保存数据文本框以前的值====================
				
				switch(document.Form1.all["dropDownListSource"].value)
				{
					case "1"://字典表
					var r = window.showModalDialog("PromptSelectDictionary.aspx", "", "");
					
					if ( typeof(r) != 'undefined')
					{
						document.Form1.all["txtDataSource"].value = r.replace(" ","");
					}
					
					//===============改变隐藏的值====================
					//DataSourceChange(document.Form1.all["txtDataSource"]);
					
					break;
					
					case "2"://固定列表
					var r = window.showModalDialog("PromptSelectFixList.aspx", document.Form1.all["txtDataSource"].value, "");
					if( typeof(r) != 'undefined')
					{
						document.Form1.all["txtDataSource"].value = r;
					}
					break;
					
					case "3"://动态列表
					var r = window.showModalDialog("PromptSelectDynamicList.aspx", "", "");
					if( typeof(r) != 'undefined')
					{
						document.Form1.all["txtDataSource"].value = r;
					}
					break;
					
					case "0":
					alert("没有选择数据来源");
					break;
				}
				DataSourceChange(document.Form1.all["txtDataSource"]);
			}
			//=======================个人身份信息======================
			function PopPerson()
			{
				var r= window.showModalDialog("PersonDepartment.aspx", "", "");
				if ( typeof(r) != 'undefined')
				{
					document.Form1.all["txtPerson"].value = r;
					PersonDataChange(document.Form1.all["txtPerson"]);
				}
			}
			
			//=========================排序==========================
			function FieldSort()
			{
				con = window.confirm("如果更改没有保存,进入排序时将丢失,确认先保存了更改。确定要继续吗？");
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
			
			
			//=============================双击，设置默认值=================
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
				///是否新闻
				var objSelectValue=objSelect.options[objSelect.selectedIndex].value;
				var objSelectText=objSelect.options[objSelect.selectedIndex].text;
				window.status=objSelectText;
				switch(objSelectValue)
				{
					case "3":
						alert('文件类型，好象不能设置默认值');
						obj.style.backgroundColor="red";
						break;
					case "4":
						var objValue=obj.value//.replace("$模板:","");
						
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
			    if(window.confirm('请小心操作，只修改字段类型，长度，说明等，不要更改数据库的关键名称，位置等信息'
			    +'\n\n如果界面需要登录，请输入该数据库指向的机器名，登录名与密码\n\n'
			    + '数据库连接可能需要很长时间，请等候.......确定要去修改吗？')){
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
			<!-- span 中的id不能改和删除 -->
			<P><FONT face="宋体"><IMG src="../images/icons/0041_a.gif">功能逻辑关系:</FONT><FONT face="黑体" size="5"><span id="tableName"><%Response.Write(Request.QueryString["TableName"]);%></span></FONT></P>
			<P>&nbsp;<input id="newField" type="button" value="选择字段" name="newField" runat="server" onserverclick="newField_ServerClick">&nbsp;
				<asp:button id="btnSave" runat="server" Text="保存" onclick="btnSave_Click"></asp:button>
				&nbsp;<input type="button" value="排序" name="btnSort" onclick="return FieldSort()">
				&nbsp;<input type="button" value="数据库字段编辑" id="btnColumnEdit" name="btnColumnEdit" runat="server"></P>
			<TABLE class="blackborder12px" id="Table1" cellSpacing="1" cellPadding="0" width="100%"
				border="0" runat="server">
				<TR>
					<TD noWrap width="50">主键
					</TD>
					<td noWrap width="50">索引
					</td>
					<TD noWrap align="center" width="80">字段中文名
					</TD>
					<TD noWrap align="center" width="80">字段英文名
					</TD>
					<TD noWrap align="center" width="80">类型
					</TD>
					<TD noWrap>允许空
					</TD>
					<TD noWrap>默认值
					</TD>
					<td noWrap>只读
					</td>
					<TD noWrap><FONT face="宋体">删除</FONT></TD>
					<TD noWrap>
						<!--放隐藏字段--></TD>
					<td></td>
					<td></td>
					<td><!--登录者信息--></td>
					<td><!--登入者部门是数据源--></td>
				</TR>
			</TABLE>
			<p>
				<table id="Table2" cellSpacing="1" cellPadding="0" width="50%" bgColor="#ffffff" border="1">
					<tr>
						<td style="WIDTH: 104px">中文字段名</td>
						<td><asp:label id="lblCName" Runat="server"></asp:label></td>
					</tr>
					<tr>
						<td style="WIDTH: 104px">类型</td>
						<td><asp:label id="lblType" Runat="server"></asp:label></td>
					</tr>
					<tr>
						<td><asp:label id="lblPrecision" Runat="server">精度</asp:label></td>
						<td><asp:textbox id="txtPrecision" Runat="server" Width="88px"></asp:textbox>
							<!--这个标记是用来找到对应的行--></td>
					</tr>
					<tr>
						<td><asp:label id="lblDate" Runat="server">日期格式</asp:label></td>
						<td><asp:dropdownlist id="dropListDate" Runat="server" Width="105">
								<asp:ListItem Value="0">--选择日期格式--</asp:ListItem>
								<asp:ListItem Value="1">YYYY-MM-DD</asp:ListItem>
								<asp:ListItem Value="2">YYYY-MM-DD-HH</asp:ListItem>
								<asp:ListItem Value="3">YYYY-MM-DD-HH-MM</asp:ListItem>
								<asp:ListItem Value="4">YYYY-MM-DD-HH-MM-SS</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td><asp:label id="lblDropDownList" Runat="server">列表类型</asp:label></td>
						<td><asp:dropdownlist id="dropDownList" Runat="server" Width="105">
								<asp:ListItem Value="0">--选择列表类型--</asp:ListItem>
								<asp:ListItem Value="1">单选</asp:ListItem>
								<asp:ListItem Value="2">多选</asp:ListItem>
								<asp:ListItem Value="3">Radio单选</asp:ListItem>
								<asp:ListItem Value="4">Check多选</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td><asp:label id="lblFile" Runat="server">文件</asp:label></td>
						<td><asp:dropdownlist id="dropListFile" Runat="server" Width="105">
								<asp:ListItem Value="0">--文件保存类型--</asp:ListItem>
								<asp:ListItem Value="1">本地存储</asp:ListItem>
								<asp:ListItem Value="2">链接</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="HEIGHT: 17px"><asp:label id="lblNews" Runat="server">新闻</asp:label></td>
						<td style="HEIGHT: 17px"><asp:dropdownlist id="dropListNews" Runat="server" Width="105">
								<asp:ListItem Value="0">--新闻保存类型--</asp:ListItem>
								<asp:ListItem Value="1">本地存储</asp:ListItem>
								<asp:ListItem Value="2">链接</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td><asp:label id="lblLogic" Runat="server">登录者信息</asp:label></td>
						<td><asp:dropdownlist id="dropListLogic" Runat="server" Width="105">
								<asp:ListItem Value="0">--选择类型--</asp:ListItem>
								<asp:ListItem Value="1">登录者姓名</asp:ListItem>
								<asp:ListItem Value="2">登录者部门</asp:ListItem>
								<asp:ListItem Value="3">登录者职位</asp:ListItem>
								<asp:ListItem Value="4">登录者岗位</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<!--新增加的，选择数据源-->
					<tr>
						<td>数据来源
						</td>
						<td><asp:dropdownlist id="dropDownListSource" Runat="server" Width="105">
								<asp:ListItem Value="0">--选择源--</asp:ListItem>
								<asp:ListItem Value="1">字典表</asp:ListItem>
								<asp:ListItem Value="2">固定列表</asp:ListItem>
								<asp:ListItem Value="3">动态列表</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td>数据源名</td>
						<td><asp:textbox id="txtDataSource" Runat="server" Width="105"></asp:textbox><input id="btnSelectSource" onclick="PopWindows()" type="button" value="选择">
						</td>
					</tr>
					<!--个人身份信息-->
					<tr>
						<td>数据源</td>
						<td><asp:textbox id="txtPerson" Runat="server" Width="105"></asp:textbox><input id="btnPerson" onclick="PopPerson()" type="button" value="选择">
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
				
				//===============隐藏数据行==============================
				document.Form1.all["Table2"].rows[8].style.display = "none";
				document.Form1.all["Table2"].rows[9].style.display = "none";
				
				//================个人身份信息=======================
				document.Form1.all["Table2"].rows[10].style.display = "none";
				
				//=================找到哪个主键被选中========================
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
