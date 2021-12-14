<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.InputAssessorKeyColumns" Codebehind="InputAssessorKeyColumns.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InputAssessorKeyColumns</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
		</script>
		<%--
	<body onmousemove="drag()" onselectstart="return false;" MS_POSITIONING="GridLayout" oncontextmenu="end()">
	--%>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table style="BORDER-RIGHT: #6699cc 2px solid; BORDER-TOP: #6699cc 2px solid; BORDER-LEFT: #6699cc 2px solid; BORDER-BOTTOM: #6699cc 2px solid"
				width="100%" align="center" border="0">
				<tr>
					<td align="center" width="25%">所有字段</td>
					<td align="center" width="25%">显示列数<asp:DropDownList ID="dropDownListCols" Runat="server"></asp:DropDownList></td>
					<td align="center" width="25%"><asp:button id="btnSave" Runat="server" Text="保存" onclick="btnSave_Click"></asp:button></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="1" nowrap>
						<table width="100%" border="0">
							<tr>
								<td noWrap align="center" width="220"><asp:datalist id="dataListAllField" Runat="server" Width="100%" RepeatColumns="2" BorderWidth="1px"
										GridLines="Both" BorderColor="#99CCCC">
										<ItemTemplate>
											<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>
											<asp:Label ID="lblFieldName" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
						</table>
					</td>
					<td vAlign="top" align="center" colSpan="2">
						<table width="100%" border="0">
							<tr>
								<td vAlign="top" align="center" width="100%"><asp:datalist id="dataListOrder" Runat="server" CellPadding="0" BorderWidth="0px" GridLines="Both"
										CellSpacing="8" BorderColor="Blue" Width="100%" RepeatDirection="Horizontal">
										<ItemTemplate>
											<table border="0" id="table1" style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; BACKGROUND-COLOR: white"
												cellpadding="0" cellspacing="1">
												<%--onclick="Locate(this);" onmousemove="StartMove(this)"
												onmouseout="EndMove(this)">--%>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap align="left" style="BACKGROUND-COLOR: gainsboro">
														字段名
													</td>
													<th nowrap align="left" style="COLOR: dimgray">
														<asp:Label ID="lblSelectFieldName" Runat="server" Width="60"></asp:Label>
													</th>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">
														类型
													</td>
													<td align="left" nowrap>
														<asp:Label ID="lblType" Runat="server"></asp:Label>
													</td>
												</tr>
												<%--
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">排序
													</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtOrder" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												--%>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">显示字数
													</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtDisplayWordNum" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
											</table>
										</ItemTemplate>
									</asp:datalist>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE>
		</form>
		<div id="divMove" style="Z-INDEX: 100; POSITION: absolute"></div>
		<FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
		</FONT>
	</body>
</HTML>
