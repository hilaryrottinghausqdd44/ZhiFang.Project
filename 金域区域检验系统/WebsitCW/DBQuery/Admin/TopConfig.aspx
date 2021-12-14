<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Admin.TopConfig" Codebehind="TopConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TopConfig</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
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
			
			//=======================用于弹出窗口，设定默认值==========================
			function DBClickDefault(obj)
			{
				var DlgRtnValue = window.showModalDialog("WinOpenDefaultValueModalDialog.aspx", "", "status:no;resizable:no");
				if(DlgRtnValue != void 0)
				{
					obj.value = DlgRtnValue;
				}
			}
			//=================================End====================================
			
			function ShowGroupQuery()
			{
				r = window.open ('../../PopupSelectDialog.aspx?DBQuery/admin/TopQueryGroup.aspx?<%=Request.ServerVariables["Query_string"]%>','','dialogWidth=145;dialogHeight=130;resizable=yes;scroll=yes;status=no');
				if(r != ''&& typeof(r)!='undefined')
				{
					
				}
			}
		</script>
		<%--
	<body onmousemove="drag()" onselectstart="return false;" MS_POSITIONING="GridLayout" oncontextmenu="end()">
	--%>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<INPUT style="Z-INDEX: 104; LEFT: 224px; WIDTH: 96px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; POSITION: absolute; TOP: 8px; HEIGHT: 20px; BACKGROUND-COLOR: crimson; BORDER-BOTTOM-STYLE: none"
				type="button" value="高级查询"><INPUT style="Z-INDEX: 107; LEFT: 392px; WIDTH: 96px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; POSITION: absolute; TOP: 8px; HEIGHT: 20px; BACKGROUND-COLOR: cornflowerblue; BORDER-BOTTOM-STYLE: none"
				type="button" value="功能导航">
			<asp:CheckBox id="CheckBoxUserNavigator" style="Z-INDEX: 106; LEFT: 336px; POSITION: absolute; TOP: 8px"
				runat="server" Text="隐藏"></asp:CheckBox>&nbsp;
			<asp:CheckBox id="CheckBoxUserDefinedQuery" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 8px"
				runat="server" Text="启用"></asp:CheckBox>
			<asp:CheckBox id="CheckBoxSophQuery" style="Z-INDEX: 102; LEFT: 176px; POSITION: absolute; TOP: 8px"
				runat="server" Text="启用"></asp:CheckBox><INPUT style="Z-INDEX: 103; LEFT: 64px; WIDTH: 96px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; POSITION: absolute; TOP: 8px; HEIGHT: 20px; BACKGROUND-COLOR: crimson; BORDER-BOTTOM-STYLE: none"
				type="button" value="数据分类查询" onclick="ShowGroupQuery()">
			<TABLE style="BORDER-RIGHT: #6699cc 2px solid; BORDER-TOP: #6699cc 2px solid; BORDER-LEFT: #6699cc 2px solid; BORDER-BOTTOM: #6699cc 2px solid"
				width="100%" align="center" border="0">
				<tr>
					<td style="HEIGHT: 15px" noWrap align="center" width="20%"><asp:button id="btnSave" Runat="server" Text="保存" onclick="btnSave_Click"></asp:button>显示风格
						<asp:dropdownlist id="dropDownListDisplay" Width="90" Runat="server">
							<asp:ListItem Value="1">折叠</asp:ListItem>
							<asp:ListItem Value="2">展开</asp:ListItem>
						</asp:dropdownlist></td>
					<td style="HEIGHT: 15px" noWrap align="center" width="20%">列数<asp:dropdownlist id="dropDownList" Width="90" Runat="server">
							<asp:ListItem Value="1">--选择列数--</asp:ListItem>
						</asp:dropdownlist></td>
					<td style="HEIGHT: 15px" noWrap align="center" width="20%"><FONT face="宋体"></FONT></td>
					<td style="HEIGHT: 15px" align="center" width="20%"><FONT face="宋体"><asp:checkbox id="chkIsHiddenTop" Runat="server" Text="隐藏查询"></asp:checkbox></FONT></td>
					<td style="HEIGHT: 15px" align="center">所有字段</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="4">
						<table width="100%" border="0">
							<tr>
								<td vAlign="top" align="center" width="100%"><asp:datalist id="dataListOrder" Width="100%" Runat="server" RepeatDirection="Horizontal" BorderColor="Blue"
										CellSpacing="8" GridLines="Both" BorderWidth="0px" CellPadding="0">
										<ItemTemplate>
											<table border="0" id="table1" style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; BACKGROUND-COLOR: white">
												<%--cellpadding="0" cellspacing="1" onclick="Locate(this);" onmousemove="StartMove(this)"
												onmouseout="EndMove(this)">--%>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap align="left" style="BACKGROUND-COLOR: gainsboro">
														字段名
													</td>
													<th nowrap align="left" style="COLOR: dimgray">
														<asp:Label ID="lblOrderFieldName" Runat="server" Width="60"></asp:Label>
													</th>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">字段类型
													</td>
													<td align="left" nowrap>
														<asp:Label ID="lblDisplayType" Runat="server" Width="60"></asp:Label>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">显示方式
													</td>
													<td align="left" nowrap onclick="return false;">
														<asp:DropDownList Runat="server" ID="dropDownDisplay" Width="80"></asp:DropDownList>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">默认条件
													</td>
													<td align="left" nowrap onclick="return false;">
														<asp:TextBox ID="txtDefaultQuery" Runat="server" Width="80"></asp:TextBox>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">子表中显示
													</td>
													<td nowrap align="left">
														<asp:CheckBox Runat="server" ID="chkChildDisplay"></asp:CheckBox>
													</td>
												</tr>
												<%--
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">排列顺序
													</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtOrder" Runat="server" Width="80"></asp:TextBox>
													</td>
												</tr>
												--%>
											</table>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
						</table>
					</td>
					<td vAlign="top" noWrap align="center" width="220">
						<table width="100%" border="0">
							<tr>
								<td align="center" width="100%"><asp:datalist id="dataListAllField" Width="100%" Runat="server" BorderColor="#99CCCC" GridLines="Both"
										BorderWidth="1px" RepeatColumns="2">
										<ItemTemplate>
											<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>
											<asp:Label ID="lblFieldName" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
		<div id="divMove" style="Z-INDEX: 100; POSITION: absolute"></div>
		<FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
		</FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT>
	</body>
</HTML>
