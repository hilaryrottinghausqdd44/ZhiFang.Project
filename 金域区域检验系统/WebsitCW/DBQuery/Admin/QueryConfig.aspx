<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Admin.QueryConfig" Codebehind="QueryConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QueryConfig</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">

			function DBClickQueryFunction(obj,inputFunctionString)
			{
				try
				{
				    //inputFunctionString=inputFunctionString.replace(/&/g,'$1$');
				    inputFunctionString=obj.value;
				    inputFunctionString=inputFunctionString.replace(/&/g,'$1$');
				    var url = "QueryConfigFunctions.aspx?FunctionString1=" + inputFunctionString +"&<%=Request.ServerVariables["Query_String"]%>"; //../../DataInput/SetGridViewFunctionOnQueryForm.aspx";
				    //alert(url);
				    //screen
					var DlgRtnValue = window.showModalDialog(url,'','status:yes;resizable:yes;dialogHeight:'+screen+';dialogWidth:780px;center:yes');
					if(DlgRtnValue != void 0)
					{
						obj.value = DlgRtnValue;
					}
				}
				catch(e)
				{
					alert('������:');
				}
			}

		/*
			var DragKid=null;
			var DragKidDropOn=null;
			var strKids="";
				
			var pickedOne=false;
			function Locate(obj)
			{
				if(!pickedOne)
				{
					divMove.innerHTML=obj.outerHTML;
					LoopRemoveInput(divMove.children);
					var kids=obj.children;
					LoopFindKidOrder(kids)
					strKids="";
					pickedOne=true;
				}
				else
				{
					LoopFindKidDropOn(obj.children);
					DragKid.value=DragKidDropOn.value;
					//DragKidDropOn.value='22';
					end();
				}
				
			}
			function end()
			{
				try{
				divMove.innerHTML="";
				DragKid=null;
				DragKidDropOn=null;
				pickedOne=false;
				}
				catch(e){}
			}
			function drag()
			{
				try{
				divMove.style.left=event.x+1;
				divMove.style.top=event.y+1;
				}
				catch(e){}
			}
			
			function LoopFindKidOrder(kids)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].tagName.toUpperCase()=='INPUT'&&kids[i].type.toUpperCase()=='TEXT')
					{
						DragKid = kids[i];
						return;
					}
					if(kids[i].hasChildNodes)
						LoopFindKidOrder(kids[i].children);
				}
			}
			
			function LoopFindKidDropOn(kids)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].tagName.toUpperCase()=='INPUT'&&kids[i].type.toUpperCase()=='TEXT')
					{
						DragKidDropOn = kids[i];
						return;
					}
					if(kids[i].hasChildNodes)
						LoopFindKidDropOn(kids[i].children);
				}
			}
			
			function LoopRemoveInput(kids)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].tagName.toUpperCase()=='INPUT'&&kids[i].type.toUpperCase()=='TEXT')
					{
						kids[i].parentNode.removeChild(kids[i]);
					}
					else
					{
						if(kids[i].hasChildNodes)
							LoopRemoveInput(kids[i].children);
					}
				}
			}
			
			function StartMove(obj)
			{
				try{
				obj.style.borderColor="red";
				obj.style.borderTopWidth=2;
				}
				catch(e){}
			}
			function EndMove(obj)
			{
				try{
				obj.style.borderColor="#6699cc";
				obj.style.borderTopWidth=1;
				}
				catch(e){}
			}
		*/
		</script>
		<%--
	<body MS_POSITIONING="GridLayout" onmouseover="drag()" onselectstart="return false;" oncontextmenu="end()">
	--%>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table style="BORDER-RIGHT: #6699cc 2px solid; BORDER-TOP: #6699cc 2px solid; BORDER-LEFT: #6699cc 2px solid; BORDER-BOTTOM: #6699cc 2px solid"
				width="100%" align="center" border="0">
				<tr>
					<td align="center" width="25%">�����ֶ�</td>
					<td align="left" width="25%" nowrap><asp:button id="btnSave" Text="����" Runat="server" onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp; 
						��ʾ����<asp:textbox id="txtRows" Runat="server" Width="50px"></asp:textbox>&nbsp;����
						<asp:DropDownList ID="dropListSort" Runat="server">
							<asp:ListItem Value="0">����</asp:ListItem>
							<asp:ListItem Value="1">����</asp:ListItem>
						</asp:DropDownList>
                        </td>
					<td align="left" width="25%"></td>
				</tr>
				<tr>
					<td vAlign="top" noWrap colSpan="1">
						<table width="100%" border="0" id="tableAll">
							<tr>
								<td noWrap align="center" width="220"><asp:datalist id="dataListAllField" Width="100%" Runat="server" RepeatColumns="2" BorderWidth="1px"
										GridLines="Both" BorderColor="#99CCCC">
										<ItemTemplate>
											<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>
											<asp:Label ID="lblFieldName" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
						</table>
					</td>
					<td vAlign="top" align="left" colSpan="2">
						<table style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; BACKGROUND-COLOR: #C0C0C0"
							cellpadding="1" cellspacing="1" >
							<tr bgcolor="white">
								<td colspan="2"><asp:datalist id="dataListOrder" Width="100%" Runat="server" RepeatDirection="Horizontal">
										<ItemTemplate>
											<table border="0" id="tableSelected" style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; BACKGROUND-COLOR: white"
												cellpadding="0" cellspacing="1">
												<%--onclick="Locate(this);" onmousemove="StartMove(this)" onmouseout="EndMove(this)">--%>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap align="left" style="BACKGROUND-COLOR: gainsboro">
														�ֶ���
													</td>
													<th nowrap align="left" style="COLOR: dimgray">
														<asp:Label ID="lblSelectFieldName" Runat="server"></asp:Label>
													</th>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">
														����
													</td>
													<td nowrap align="left">
														<asp:Label ID="lblType" Runat="server"></asp:Label>
													</td>
												</tr>
												<!--
												<tr style="BACKGROUND-COLOR: whitesmoke">
													
													<td nowrap style="BACKGROUND-COLOR: gainsboro">���
													</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtWidth" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												-->
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">������</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtFunction" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">��ʾ����</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtDisplayLength" Runat="server" Width="60"></asp:TextBox></td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">�������
													</td>
													<td nowrap align="left">
														<asp:CheckBox ID="chkSum" Runat="server"></asp:CheckBox>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">��������
													</td>
													<td nowrap align="left">
														<asp:CheckBox ID="CheckBoxMoney" Runat="server"></asp:CheckBox>
														<asp:TextBox ID="txtMoneySymbol" Runat="server" Width="30" Text=""></asp:TextBox>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">���ӱ�����ʾ
													</td>
													<td nowrap align="left">
														<asp:CheckBox ID="chkChildDisplay" Runat="server"></asp:CheckBox>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">�б�����
													</td>
													<td nowrap align="left">
                                                        <asp:DropDownList ID="DropDownListOrderBy" runat="server">
                                                            <asp:ListItem Value="0">δ����</asp:ListItem>
                                                            <asp:ListItem Value="1">����asc</asp:ListItem>
                                                            <asp:ListItem Value="-1">����desc</asp:ListItem>
                                                        </asp:DropDownList>
													</td>
												</tr>
											</table>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
							<tr bgcolor="white">
							    <td width="19%"><asp:CheckBox ID="CheckBoxSumPage" runat="server" Text="��ҳ�ڻ���" />
						        </td><td>��ҳ��ѯ���ܡ�</td>
							</tr>
							<tr bgcolor="white">
							    <td><asp:CheckBox ID="CheckBoxSumAll" runat="server" Text="��ȫ������" />
							    </td><td>ȫ�����ݻ��ܡ�</td>
							</tr>
							<tr bgcolor="white">
							    <td colspan="2">
							        <fieldset style="Width:350px"><legend>
                                        <asp:CheckBox ID="CheckBoxPager" runat="server" />ֻ��ʾ��һҳ��ѯ���������ҳ</legend>
							            <asp:RadioButtonList ID="RadioButtonListPagesStyle" runat="server" RepeatColumns="0">
                                        </asp:RadioButtonList>
                                    </fieldset><br />
                                    <asp:CheckBox ID="CheckBoxDBLClick" runat="server" />˫������������ʾ��ϸ��Ϣ
                                </td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<div id="divMove" style="Z-INDEX: 100; POSITION: absolute"></div>
		<FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����">
		</FONT><FONT face="����"></FONT>
	</body>
</HTML>
