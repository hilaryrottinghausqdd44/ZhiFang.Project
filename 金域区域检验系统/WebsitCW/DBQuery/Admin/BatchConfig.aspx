<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Admin.BatchConfig" Codebehind="BatchConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>设置批量处理字段</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		
	

        <script language="javascript" type="text/javascript">
// <!CDATA[

            function window_onload() {

            }

            
// ]]>
        </script>
</HEAD>
	<body MS_POSITIONING="GridLayout" onload="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<table style="BORDER-RIGHT: #6699cc 2px solid; BORDER-TOP: #6699cc 2px solid; BORDER-LEFT: #6699cc 2px solid; BORDER-BOTTOM: #6699cc 2px solid"
				width="100%" align="center" border="0">
				<tr>
					<td align="center" width="25%">所有字段</td>
					<td align="left" width="25%" nowrap><asp:button id="btnSave" Text="保存" Runat="server" onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp; 
						
                        </td>
					<td align="left" width="25%"></td>
				</tr>
				<tr>
					<td vAlign="top" noWrap colSpan="3">
						<table width="100%" border="0" id="tableAll">
							<tr>
								<td noWrap align="left"><asp:datalist id="dataListAllField" Runat="server" RepeatColumns="10" BorderWidth="1px"
										GridLines="Both" BorderColor="#99CCCC" RepeatDirection="Horizontal" HeaderStyle-Wrap="False" ItemStyle-Wrap="False">
										<ItemTemplate>
											<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>
											<asp:Label ID="lblFieldName" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top" align="left" colSpan="3">
						<table style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; BACKGROUND-COLOR: #C0C0C0"
							cellpadding="1" cellspacing="1" >
							<tr bgcolor="white">
								<td colspan="2"><asp:datalist id="dataListOrder" Width="100%" Runat="server" RepeatDirection="Horizontal">
										<ItemTemplate>
											<table border="0" id="tableSelected" style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; BACKGROUND-COLOR: white"
												cellpadding="0" cellspacing="1">
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap align="left" style="BACKGROUND-COLOR: gainsboro">
														字段名
													</td>
													<th nowrap align="left" style="COLOR: dimgray">
														<asp:Label ID="lblSelectFieldName" Runat="server"></asp:Label>
													</th>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">
														类型
													</td>
													<td nowrap align="left">
														<asp:Label ID="lblType" Runat="server"></asp:Label>
													</td>
												</tr>
												<!--
												<tr style="BACKGROUND-COLOR: whitesmoke">
													
													<td nowrap style="BACKGROUND-COLOR: gainsboro">宽度
													</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtWidth" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												-->
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">处理功能</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtFunction" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">长度</td>
													<td nowrap align="left">
														<asp:TextBox ID="txtDisplayLength" Runat="server" Width="60"></asp:TextBox></td>
												</tr>
												
											</table>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
							
						</table>
					</td>
				</tr>
			</table>
		</form>
		<div id="divMove" style="Z-INDEX: 100; POSITION: absolute"></div>
		
	</body>
</HTML>
