<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Diagram.IntroProject1" Codebehind="IntroProject1.aspx.cs" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Xml" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>IntroProject</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body topmargin=0 rightmargin=0 leftmargin=0 bottommargin=0>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="0" border="0">
							<TBODY>
								<TR>
									<TD noWrap>T1:
									</TD>
									<TD noWrap><FONT face="宋体"><asp:textbox id="TextBoxCurrentPercent1" runat="server" Width="40px"></asp:textbox></FONT></TD>
									<TD noWrap><FONT face="宋体"><FONT face="宋体" size="4">%</FONT></FONT></TD>
									<TD noWrap><FONT face="宋体">&lt;= 试剂费用/应完成量 &lt;=</FONT></TD>
									<TD noWrap><FONT face="宋体"><asp:textbox id="TextBoxCurrentPercent2" runat="server" Width="40px"></asp:textbox></FONT></TD>
									<TD noWrap><FONT face="宋体" size="4">%</FONT></TD>
									<TD noWrap><FONT face="宋体"></FONT></TD>
									<TD noWrap>截止日期<asp:textbox id="TextBoxEndDate" runat="server" Width="104px"></asp:textbox></TD>
								</TR>
								<tr>
									<TD noWrap>T2:
									</TD>
									<TD noWrap><FONT face="宋体"><asp:textbox id="TextBoxTotalPercent1" runat="server" Width="40px"></asp:textbox></FONT></TD>
									<TD noWrap><FONT face="宋体" size="4">%</FONT></TD>
									<TD noWrap><FONT face="宋体"><FONT face="宋体">&lt;= 试剂费用/购买总量 &lt;=</FONT>&nbsp;</FONT></TD>
									<TD noWrap><FONT face="宋体"><asp:textbox id="TextBoxTotalPercent2" runat="server" Width="40px"></asp:textbox></FONT></TD>
									<TD noWrap align="center"><FONT face="宋体"><FONT face="宋体" size="4">%</FONT></FONT></TD>
									<TD noWrap align="center"><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </FONT>
									</TD>
									<TD noWrap align="center"><asp:button id="buttAnalyze" runat="server" Text="统计" onclick="buttAnalyze_Click"></asp:button></TD>
								</tr></TBODY>
							
						</TABLE>统计类别
						<asp:dropdownlist id="DropDownListCategory" runat="server" Width="176px">
							<asp:ListItem Value="EName12" Selected="True">按区域</asp:ListItem>
							<asp:ListItem Value="EName3">按用户</asp:ListItem>
							<asp:ListItem Value="EName4">按仪器</asp:ListItem>
							<asp:ListItem Value="EName5">按销售商</asp:ListItem>
						</asp:dropdownlist>&nbsp;&nbsp;
						&nbsp;&nbsp;&nbsp;&nbsp;统计图类型
						<asp:dropdownlist id="DropDownList1" runat="server">
							<asp:ListItem Value="0" Selected="true">柱状图:簇形柱状图</asp:ListItem>
							<asp:ListItem Value="1">柱状图:堆积柱状图</asp:ListItem>
							<asp:ListItem Value="2">柱状图:百分比堆积柱状图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="3">条形图:簇状条形图</asp:ListItem>
							<asp:ListItem Value="4">条形图:堆积条形图</asp:ListItem>
							<asp:ListItem Value="5">条形图:百分比堆积条形图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="6">折线图:折线图</asp:ListItem>
							<asp:ListItem Value="7">折线图:数据点折线图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="8">折线图:堆积折线图</asp:ListItem>
							<asp:ListItem Value="9">折线图:堆积数据点折线图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="10">折线图:百分比堆积折线图</asp:ListItem>
							<asp:ListItem Value="11">折线图:百分比堆积数据点折线图</asp:ListItem>
							<asp:ListItem Value="0">-------------------------</asp:ListItem>
							<asp:ListItem Value="20">饼  图:复合饼图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="29">面积图:面积图</asp:ListItem>
							<asp:ListItem Value="30">面积图:堆积面积图</asp:ListItem>
							<asp:ListItem Value="31">面积图:百分比堆积面积图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="32">圆环图:圆环图</asp:ListItem>
							<asp:ListItem Value="33">圆环图:分离形圆环图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="34">雷达图:雷达图</asp:ListItem>
							<asp:ListItem Value="35">雷达图:数据点雷达图</asp:ListItem>
							<asp:ListItem Value="36">雷达图:填充雷达图</asp:ListItem>
							<asp:ListItem Value="37">雷达图:平滑雷达图</asp:ListItem>
							<asp:ListItem Value="38">雷达图:数据点平滑雷达图</asp:ListItem>
							<asp:ListItem Value="0">-----------------------</asp:ListItem>
							<asp:ListItem Value="39">散点图</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><FONT face="宋体">
      <OBJECT id=ChartSpace2 style="WIDTH: 100%; HEIGHT: 370px" 
      classid=CLSID:0002E55D-0000-0000-C000-000000000046 name=ChartSpace2 
      VIEWASTEXT>
	<PARAM NAME="XMLData" VALUE='<xml xmlns:x="urn:schemas-microsoft-com:office:excel">&#13;&#10; <x:WebChart>&#13;&#10;  <x:OWCVersion>9.0.0.2710</x:OWCVersion>&#13;&#10;  <x:Width>23892</x:Width>&#13;&#10;  <x:Height>9790</x:Height>&#13;&#10; </x:WebChart>&#13;&#10;</xml>'>
	<PARAM NAME="ScreenUpdating" VALUE="-1">
	</OBJECT>
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD>
						<P>
      <OBJECT id=Spreadsheet2 style="WIDTH: 98.69%; HEIGHT: 350px" height=350 
      width=872 classid=CLSID:0002E559-0000-0000-C000-000000000046 VIEWASTEXT>
	<PARAM NAME="HTMLURL" VALUE="">
	<PARAM NAME="HTMLData" VALUE="<html xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;&#13;&#10;xmlns=&quot;http://www.w3.org/TR/REC-html40&quot;>&#13;&#10;&#13;&#10;<head>&#13;&#10;<style type=&quot;text/css&quot;>&#13;&#10;<!--tr&#13;&#10;&#9;{mso-height-source:auto;}&#13;&#10;td&#13;&#10;&#9;{white-space:nowrap;}&#13;&#10;.wc8CC98FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;mso-background-source:auto;&#13;&#10;&#9;mso-pattern:auto;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:none;&#13;&#10;&#9;border-left:none;&#13;&#10;&#9;border-right:none;&#13;&#10;&#9;border-bottom:none;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc48E98FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:Percent;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;mso-background-source:auto;&#13;&#10;&#9;mso-pattern:auto;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:none;&#13;&#10;&#9;border-left:none;&#13;&#10;&#9;border-right:none;&#13;&#10;&#9;border-bottom:none;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc47F98FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:#FFFFCA;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc830A8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:Percent;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:#FFFFCA;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc851A8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:white;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wcC12A8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:Percent;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:white;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;-->&#13;&#10;</style>&#13;&#10;</head>&#13;&#10;&#13;&#10;<body>&#13;&#10;<!--[if gte mso 9]><xml>&#13;&#10; <x:ExcelWorkbook>&#13;&#10;  <x:ExcelWorksheets>&#13;&#10;   <x:ExcelWorksheet>&#13;&#10;    <x:OWCVersion>9.0.0.2710</x:OWCVersion>&#13;&#10;    <x:Label Style='border-top:solid .5pt silver;border-left:solid .5pt silver;&#13;&#10;     border-right:solid .5pt silver;border-bottom:solid .5pt silver'>&#13;&#10;     <x:Caption>Microsoft Office Spreadsheet</x:Caption>&#13;&#10;    </x:Label>&#13;&#10;    <x:Name>Sheet1</x:Name>&#13;&#10;    <x:WorksheetOptions>&#13;&#10;     <x:Selected/>&#13;&#10;     <x:Height>9260</x:Height>&#13;&#10;     <x:Width>23574</x:Width>&#13;&#10;     <x:TopRowVisible>0</x:TopRowVisible>&#13;&#10;     <x:LeftColumnVisible>0</x:LeftColumnVisible>&#13;&#10;     <x:ProtectContents>False</x:ProtectContents>&#13;&#10;     <x:DefaultRowHeight>210</x:DefaultRowHeight>&#13;&#10;     <x:StandardWidth>2389</x:StandardWidth>&#13;&#10;    </x:WorksheetOptions>&#13;&#10;   </x:ExcelWorksheet>&#13;&#10;  </x:ExcelWorksheets>&#13;&#10;  <x:MaxHeight>80%</x:MaxHeight>&#13;&#10;  <x:MaxWidth>80%</x:MaxWidth>&#13;&#10;  <x:Calculation>ManualCalculation</x:Calculation>&#13;&#10; </x:ExcelWorkbook>&#13;&#10;</xml><![endif]-->&#13;&#10;&#13;&#10;<table class=wc8CC98FC x:str>&#13;&#10; <col class=wc8CC98FC width=&quot;83&quot;>&#13;&#10; <col class=wc8CC98FC width=&quot;56&quot;>&#13;&#10; <col class=wc8CC98FC width=&quot;56&quot;>&#13;&#10; <col class=wc8CC98FC width=&quot;56&quot;>&#13;&#10; <col class=wc8CC98FC width=&quot;56&quot;>&#13;&#10; <col class=wc8CC98FC width=&quot;56&quot;>&#13;&#10; <col class=wc48E98FC width=&quot;56&quot;>&#13;&#10; <col class=wc8CC98FC width=&quot;56&quot;>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc47F98FC></td>&#13;&#10;  <td class=wc47F98FC x:str=&quot;年购买量&quot;></td>&#13;&#10;  <td class=wc47F98FC x:str=&quot;购买总量&quot;></td>&#13;&#10;  <td class=wc47F98FC x:str=&quot;应完成量&quot;></td>&#13;&#10;  <td class=wc47F98FC x:str=&quot;已完成量&quot;></td>&#13;&#10;  <td class=wc47F98FC x:str=&quot;试剂费用&quot;></td>&#13;&#10;  <td class=wc830A8FC x:str=&quot;付款比例&quot;></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wc851A8FC></td>&#13;&#10;  <td class=wcC12A8FC></td>&#13;&#10;  <td class=wc8CC98FC></td>&#13;&#10; </tr>&#13;&#10;</table>&#13;&#10;&#13;&#10;</body>&#13;&#10;&#13;&#10;</html>&#13;&#10;">
	<PARAM NAME="DataType" VALUE="HTMLDATA">
	<PARAM NAME="AutoFit" VALUE="0">
	<PARAM NAME="DisplayColHeaders" VALUE="-1">
	<PARAM NAME="DisplayGridlines" VALUE="-1">
	<PARAM NAME="DisplayHorizontalScrollBar" VALUE="-1">
	<PARAM NAME="DisplayRowHeaders" VALUE="-1">
	<PARAM NAME="DisplayTitleBar" VALUE="-1">
	<PARAM NAME="DisplayToolbar" VALUE="-1">
	<PARAM NAME="DisplayVerticalScrollBar" VALUE="-1">
	<PARAM NAME="EnableAutoCalculate" VALUE="0">
	<PARAM NAME="EnableEvents" VALUE="-1">
	<PARAM NAME="MoveAfterReturn" VALUE="-1">
	<PARAM NAME="MoveAfterReturnDirection" VALUE="0">
	<PARAM NAME="RightToLeft" VALUE="0">
	<PARAM NAME="ViewableRange" VALUE="1:65536">
	</OBJECT>
						</P>
					</TD>
				</TR>
				<TR>
					<TD>
						<P><FONT face="宋体"></FONT>&nbsp;</P>
						<P><FONT face="宋体">全部数据</FONT></P>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="0" width="100%" bgColor="#0066ff" border="0">
							<TR bgColor="#ffffca" height="25">
								<TD noWrap>合同编号</TD>
								<TD noWrap>日期</TD>
								<TD noWrap>区域</TD>
								<TD noWrap>用户</TD>
								<TD noWrap>仪器</TD>
								<TD noWrap>经销商</TD>
								<TD noWrap>年购买量</TD>
								<TD noWrap>购买总量</TD>
								<TD noWrap>应完成量</TD>
								<TD noWrap>已完成量</TD>
								<TD noWrap>试剂费用</TD>
								<TD style="WIDTH: 77px" noWrap colSpan="2">付款比例</TD>
								<TD noWrap width="40%">费用比例
								</TD>
								<td>T1</td>
								<td>T2</td>
							</TR>
							<%
							string SpreadSheetText="";
							int rowsCount=0;
							if(nodesProjects!=null)
							{
								rowsCount=nodesProjects.Count;
								foreach(XmlNode eachProject in nodesProjects)
								{
								string Contract=(eachProject.SelectSingleNode("td[@Column='EName1']")==null||eachProject.SelectSingleNode("td[@Column='EName1']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName1']").InnerXml;
								string SignedDate=(eachProject.SelectSingleNode("td[@Column='EName7']")==null||eachProject.SelectSingleNode("td[@Column='EName7']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName7']").InnerXml;
								string Zone=(eachProject.SelectSingleNode("td[@Column='EName12']")==null||eachProject.SelectSingleNode("td[@Column='EName12']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName12']").InnerXml;
								string User=(eachProject.SelectSingleNode("td[@Column='EName3']")==null||eachProject.SelectSingleNode("td[@Column='EName3']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName3']").InnerXml;
								string Machine=(eachProject.SelectSingleNode("td[@Column='EName4']")==null||eachProject.SelectSingleNode("td[@Column='EName4']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName4']").InnerXml;
								string Deputy=(eachProject.SelectSingleNode("td[@Column='EName5']")==null||eachProject.SelectSingleNode("td[@Column='EName5']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName5']").InnerXml;
								string YearlyBuy=(eachProject.SelectSingleNode("td[@Column='EName6']")==null||eachProject.SelectSingleNode("td[@Column='EName6']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName6']").InnerXml;
								
								string ContentAmount=(eachProject.SelectSingleNode("td[@Column='EName10']")==null||eachProject.SelectSingleNode("td[@Column='EName10']").InnerXml=="")?"&nbsp;":eachProject.SelectSingleNode("td[@Column='EName10']").InnerXml;
								int needPayment=NeedPaymentCount(eachProject);
								float paymentAmount=SumNodes(eachProject.SelectNodes("Table[@EName='XSTJ']/tr/td[@Column='EName3']"));
								int SamplesFee=SamplesFeeCount(eachProject);
								
								float maxValue=needPayment>paymentAmount?needPayment:paymentAmount;
								maxValue=maxValue>SamplesFee?maxValue:SamplesFee;
								
								float percent=0;
								try
								{
									percent=paymentAmount/(float.Parse(ContentAmount));
								}
								catch{}
								
								
								double T1=0;
								double T2=0;
								
								try{
									T1=100*(double)SamplesFee/needPayment;
									T2=100*SamplesFee/Convert.ToDouble(ContentAmount);
								}
								catch{}
								string T11=Request.Form["TextBoxCurrentPercent1"];
								string T12=Request.Form["TextBoxCurrentPercent2"];
								string T21=Request.Form["TextBoxTotalPercent1"];
								string T22=Request.Form["TextBoxTotalPercent2"];
								bool bT11=false,bT12=false,bT21=false,bT22=false;
								try
								{
									double fT11=Convert.ToDouble(T11);
									if(fT11<T1)
										bT11=true;
									else
										bT11=false;
								}
								catch
								{
									bT11=true;
								}
								
								try
								{
									double fT11=Convert.ToDouble(T12);
									if(fT11>T1)
										bT12=true;
									else
										bT12=false;
								}
								catch
								{
									bT12=true;
								}
								
								try
								{
									double fT11=Convert.ToDouble(T21);
									if(fT11<T2)
										bT21=true;
									else
										bT21=false;
								}
								catch
								{
									bT21=true;
								}
								
								try
								{
									double fT11=Convert.ToDouble(T22);
									if(fT11>T2)
										bT22=true;
									else
										bT22=false;
								}
								catch
								{
									bT22=true;
								}
								
								if(!(bT11&&bT12&&bT21&&bT22))
								{
									rowsCount--;
									continue;
								}
								
								T1=(double)((int)(T1*10))/10;
								T2=(double)((int)(T2*10))/10;
									
								
								
								SpreadSheetText +=Contract +"★" +SignedDate +"★" + Zone + "★" +User +"★" 
													+Machine +"★" +Deputy +"★" +YearlyBuy +"★" 
													+ContentAmount +"★" +needPayment +"★" +paymentAmount +"★" 
													+SamplesFee +"★" + percent+"●";
								percent=percent*100;
								string strPercent=percent.ToString();
								
								if(strPercent.IndexOf(".")>-1&&strPercent.Length>strPercent.IndexOf(".")+1)
									strPercent=strPercent.Substring(0,strPercent.IndexOf(".")+2);
									
								%>
							<TR bgColor="#ffffff" height="30">
								<TD noWrap><%=Contract%></TD>
								<TD noWrap><%=SignedDate%></TD>
								<TD noWrap><%=Zone%></TD>
								<TD noWrap><%=User%></TD>
								<TD noWrap><%=Machine%></TD>
								<TD noWrap><%=Deputy%></TD>
								<TD noWrap align="right"><%=YearlyBuy%></TD>
								<TD noWrap align="right"><%=ContentAmount%></TD>
								<TD noWrap align="right"><%=needPayment%><FONT face="宋体"></FONT></TD>
								<TD noWrap align="right"><%=paymentAmount%></TD>
								<TD align="right"><%=SamplesFee%></TD>
								<TD style="WIDTH: 42px" noWrap bgColor="#99ccff"><IMG style="FILTER: Alpha(Opacity=0, FinishOpacity=100, Style=1, StartX=0, StartY=0, 
                                                                                                                                                                                                                                                            												FinishX=0, FinishY=45)" height=20 src="../image/percent/percent.jpg" width="<%=percent%>%" ></TD>
								<TD noWrap><%=strPercent%>
									%</TD>
								<TD align="center">
									<table cellSpacing="0" cellPadding="0" width="98%" border="0">
										<tr bgColor="#ffffff">
											<TD height="2"></TD>
										</tr>
										<tr bgColor="#ffffff">
											<TD height="8">
												<table 
                  style="FILTER: Alpha(Opacity=0, FinishOpacity=100, Style=1, StartX=0, StartY=0, 
                                                                                                                                                                                                                                                                                                                                                                                          												FinishX=0, FinishY=45)" 
                  height="100%" width="<%=(int)(needPayment/maxValue*100)%>%" 
                  bgColor=#9999ff>
													<tr>
														<td></td>
													</tr>
												</table>
											</TD>
										</tr>
										<tr bgColor="#ffffff">
											<TD height="8">
												<table 
                  style="FILTER: Alpha(Opacity=0, FinishOpacity=100, Style=1, StartX=100, StartY=100, 
                                                                                                                                                                                                                                                                                                                                                                                          												FinishX=100, FinishY=34)" 
                  height="100%" width="<%=(int)(paymentAmount/maxValue*100)%>%" 
                  bgColor=#9933cc>
													<tr>
														<td></td>
													</tr>
												</table>
											</TD>
										</tr>
										<tr bgColor="#ffffff">
											<TD height="8">
												<table 
                  style="FILTER: Alpha(Opacity=0, FinishOpacity=100, Style=1, StartX=100, StartY=100, 
                                                                                                                                                                                                                                                                                                                                                                                          												FinishX=100, FinishY=56)" 
                  height="100%" width="<%=(int)(SamplesFee/maxValue*100)%>%" 
                  bgColor=#ffae26>
													<tr>
														<td></td>
													</tr>
												</table>
											</TD>
										</tr>
										<tr bgColor="#ffffff">
											<TD height="1"></TD>
										</tr>
									</table>
								</TD>
								<td><%=T1%>%</td>
								<td><%=T2%>%</td>
							</TR>
							<%}
							}%>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>&nbsp;<br>
      <OBJECT id=ChartSpace1 style="WIDTH: 100%; HEIGHT: 370px" 
      classid=CLSID:0002E55D-0000-0000-C000-000000000046 name=ChartSpace1 
      VIEWASTEXT>
	<PARAM NAME="XMLData" VALUE='<xml xmlns:x="urn:schemas-microsoft-com:office:excel">&#13;&#10; <x:WebChart>&#13;&#10;  <x:OWCVersion>9.0.0.2710</x:OWCVersion>&#13;&#10;  <x:Width>23892</x:Width>&#13;&#10;  <x:Height>9790</x:Height>&#13;&#10; </x:WebChart>&#13;&#10;</xml>'>
	<PARAM NAME="ScreenUpdating" VALUE="-1">
	</OBJECT>
					</td>
				</tr>
				<tr>
					<td>
						<P><FONT face="宋体"></FONT><br>
      <OBJECT id=Spreadsheet1 style="WIDTH: 98.69%; HEIGHT: 350px" height=350 
      width=872 classid=CLSID:0002E559-0000-0000-C000-000000000046 VIEWASTEXT>
	<PARAM NAME="HTMLURL" VALUE="">
	<PARAM NAME="HTMLData" VALUE="<html xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;&#13;&#10;xmlns=&quot;http://www.w3.org/TR/REC-html40&quot;>&#13;&#10;&#13;&#10;<head>&#13;&#10;<style type=&quot;text/css&quot;>&#13;&#10;<!--tr&#13;&#10;&#9;{mso-height-source:auto;}&#13;&#10;td&#13;&#10;&#9;{white-space:nowrap;}&#13;&#10;.wc0A8D8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;mso-background-source:auto;&#13;&#10;&#9;mso-pattern:auto;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:none;&#13;&#10;&#9;border-left:none;&#13;&#10;&#9;border-right:none;&#13;&#10;&#9;border-bottom:none;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc02AD8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:&quot;Long Date&quot;;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;mso-background-source:auto;&#13;&#10;&#9;mso-pattern:auto;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:none;&#13;&#10;&#9;border-left:none;&#13;&#10;&#9;border-right:none;&#13;&#10;&#9;border-bottom:none;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc8FAD8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;mso-background-source:auto;&#13;&#10;&#9;mso-pattern:auto;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:left;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:none;&#13;&#10;&#9;border-left:none;&#13;&#10;&#9;border-right:none;&#13;&#10;&#9;border-bottom:none;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc01CD8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:Percent;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;mso-background-source:auto;&#13;&#10;&#9;mso-pattern:auto;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:none;&#13;&#10;&#9;border-left:none;&#13;&#10;&#9;border-right:none;&#13;&#10;&#9;border-bottom:none;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc00DD8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:#FFFFCA;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc4CDD8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:&quot;Long Date&quot;;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:#FFFFCA;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc48ED8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:#FFFFCA;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:left;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc44FD8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:Percent;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:#FFFFCA;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc400E8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:white;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc8C0E8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:&quot;Long Date&quot;;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:white;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc881E8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:General;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:white;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:left;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;.wc842E8FC&#13;&#10;&#9;{white-space:nowrap;&#13;&#10;&#9;font-family:宋体;&#13;&#10;&#9;mso-number-format:Percent;&#13;&#10;&#9;font-size:auto;&#13;&#10;&#9;font-weight:auto;&#13;&#10;&#9;font-style:auto;&#13;&#10;&#9;text-decoration:auto;&#13;&#10;&#9;background:white;&#13;&#10;&#9;mso-color-source:auto;&#13;&#10;&#9;text-align:general;&#13;&#10;&#9;vertical-align:bottom;&#13;&#10;&#9;border-top:solid .5pt black;&#13;&#10;&#9;border-left:solid .5pt black;&#13;&#10;&#9;border-right:solid .5pt black;&#13;&#10;&#9;border-bottom:solid .5pt black;&#13;&#10;&#9;mso-protection:locked;}&#13;&#10;-->&#13;&#10;</style>&#13;&#10;</head>&#13;&#10;&#13;&#10;<body>&#13;&#10;<!--[if gte mso 9]><xml>&#13;&#10; <x:ExcelWorkbook>&#13;&#10;  <x:ExcelWorksheets>&#13;&#10;   <x:ExcelWorksheet>&#13;&#10;    <x:OWCVersion>9.0.0.2710</x:OWCVersion>&#13;&#10;    <x:Label Style='border-top:solid .5pt silver;border-left:solid .5pt silver;&#13;&#10;     border-right:solid .5pt silver;border-bottom:solid .5pt silver'>&#13;&#10;     <x:Caption>Microsoft Office Spreadsheet</x:Caption>&#13;&#10;    </x:Label>&#13;&#10;    <x:Name>Sheet1</x:Name>&#13;&#10;    <x:WorksheetOptions>&#13;&#10;     <x:Selected/>&#13;&#10;     <x:Height>9260</x:Height>&#13;&#10;     <x:Width>23574</x:Width>&#13;&#10;     <x:TopRowVisible>0</x:TopRowVisible>&#13;&#10;     <x:LeftColumnVisible>0</x:LeftColumnVisible>&#13;&#10;     <x:ProtectContents>False</x:ProtectContents>&#13;&#10;     <x:DefaultRowHeight>210</x:DefaultRowHeight>&#13;&#10;     <x:StandardWidth>2389</x:StandardWidth>&#13;&#10;    </x:WorksheetOptions>&#13;&#10;   </x:ExcelWorksheet>&#13;&#10;  </x:ExcelWorksheets>&#13;&#10;  <x:MaxHeight>80%</x:MaxHeight>&#13;&#10;  <x:MaxWidth>80%</x:MaxWidth>&#13;&#10;  <x:Calculation>ManualCalculation</x:Calculation>&#13;&#10; </x:ExcelWorkbook>&#13;&#10;</xml><![endif]-->&#13;&#10;&#13;&#10;<table class=wc0A8D8FC x:str>&#13;&#10; <col class=wc0A8D8FC width=&quot;83&quot;>&#13;&#10; <col class=wc02AD8FC width=&quot;97&quot; style='mso-width-source:userset'>&#13;&#10; <col class=wc02AD8FC width=&quot;97&quot; style='mso-width-source:userset'>&#13;&#10; <col class=wc0A8D8FC width=&quot;50&quot; style='mso-width-source:userset'>&#13;&#10; <col class=wc8FAD8FC width=&quot;59&quot; style='mso-width-source:userset'>&#13;&#10; <col class=wc0A8D8FC width=&quot;56&quot;>&#13;&#10; <col class=wc0A8D8FC width=&quot;56&quot;>&#13;&#10; <col class=wc0A8D8FC width=&quot;56&quot;>&#13;&#10; <col class=wc0A8D8FC width=&quot;56&quot;>&#13;&#10; <col class=wc0A8D8FC width=&quot;56&quot;>&#13;&#10; <col class=wc0A8D8FC width=&quot;56&quot;>&#13;&#10; <col class=wc01CD8FC width=&quot;56&quot;>&#13;&#10; <col class=wc0A8D8FC width=&quot;56&quot;>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;合同编号&quot;></td>&#13;&#10;  <td class=wc4CDD8FC x:str=&quot;日期&quot;></td>&#13;&#10;  <td class=wc4CDD8FC x:str=&quot;区域&quot;></td>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;用户&quot;></td>&#13;&#10;  <td class=wc48ED8FC x:str=&quot;仪器&quot;></td>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;经销商&quot;></td>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;年购买量&quot;></td>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;购买总量&quot;></td>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;应完成量&quot;></td>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;已完成量&quot;></td>&#13;&#10;  <td class=wc00DD8FC x:str=&quot;试剂费用&quot;></td>&#13;&#10;  <td class=wc44FD8FC x:str=&quot;付款比例&quot;></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10; <tr height=&quot;20&quot; style='mso-height-source:userset;mso-height-alt:300'>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc8C0E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc881E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc400E8FC></td>&#13;&#10;  <td class=wc842E8FC></td>&#13;&#10;  <td class=wc0A8D8FC></td>&#13;&#10; </tr>&#13;&#10;</table>&#13;&#10;&#13;&#10;</body>&#13;&#10;&#13;&#10;</html>&#13;&#10;">
	<PARAM NAME="DataType" VALUE="HTMLDATA">
	<PARAM NAME="AutoFit" VALUE="0">
	<PARAM NAME="DisplayColHeaders" VALUE="-1">
	<PARAM NAME="DisplayGridlines" VALUE="-1">
	<PARAM NAME="DisplayHorizontalScrollBar" VALUE="-1">
	<PARAM NAME="DisplayRowHeaders" VALUE="-1">
	<PARAM NAME="DisplayTitleBar" VALUE="-1">
	<PARAM NAME="DisplayToolbar" VALUE="-1">
	<PARAM NAME="DisplayVerticalScrollBar" VALUE="-1">
	<PARAM NAME="EnableAutoCalculate" VALUE="0">
	<PARAM NAME="EnableEvents" VALUE="-1">
	<PARAM NAME="MoveAfterReturn" VALUE="-1">
	<PARAM NAME="MoveAfterReturnDirection" VALUE="0">
	<PARAM NAME="RightToLeft" VALUE="0">
	<PARAM NAME="ViewableRange" VALUE="1:65536">
	</OBJECT>
						</P>
					</td>
				</tr></TD></TR></TD></TR></TABLE>
			<script language="javascript" for="DropDownList1" event="onchange">
				if(Form1.DropDownList1.options[Form1.DropDownList1.selectedIndex].text.indexOf("-")<0)
				{
					Form1.ChartSpace1.Charts(0).Type=Form1.DropDownList1.options[Form1.DropDownList1.selectedIndex].value;
					Form1.ChartSpace2.Charts(0).Type=Form1.DropDownList1.options[Form1.DropDownList1.selectedIndex].value;
					
				}
			</script>
			<script language="javascript">
			var spText='<%=SpreadSheetText.Replace("'","\\'")%>';
			//var regex=new RegExp("●");
			spText=spText.replace(/●/g,"\n");
			spText=spText.replace(/\&nbsp;/g,"");
			
			Form1.Spreadsheet1.Range("A2:K<%=rowsCount+1%>").ParseText(spText,"★");
			
			
			</script>
			<script language="javascript">
				function FillCategorySpreadSheet(iRows,sColumn,iBegins,iEnds)
				{
					Form1.Spreadsheet2.Cells(iRows,1).Value =Form1.Spreadsheet1.Range(sColumn+iBegins).Value ;
					Form1.Spreadsheet2.Cells(iRows,2).Value=Form1.Spreadsheet1.ActiveSheet.Eval("=sum(G"  + iBegins +":G" + iEnds +")");
					Form1.Spreadsheet2.Cells(iRows,3).Value=Form1.Spreadsheet1.ActiveSheet.Eval("=sum(H"  + iBegins +":H" + iEnds +")");
					Form1.Spreadsheet2.Cells(iRows,4).Value=Form1.Spreadsheet1.ActiveSheet.Eval("=sum(I"  + iBegins +":I" + iEnds +")");
					Form1.Spreadsheet2.Cells(iRows,5).Value=Form1.Spreadsheet1.ActiveSheet.Eval("=sum(J"  + iBegins +":J" + iEnds +")");
					Form1.Spreadsheet2.Cells(iRows,6).Value=Form1.Spreadsheet1.ActiveSheet.Eval("=sum(K"  + iBegins +":K" + iEnds +")");
					Form1.Spreadsheet2.Cells(iRows,7).Value="=(E"  + iRows +"/C" + iRows +")";
					
					//alert(sColumn+iBegins);
				}
				
				function FillChartSpace2(iRows)
				{
					Form1.ChartSpace2.Clear();
					Form1.ChartSpace2.Charts.Add();
					var c = Form1.ChartSpace2.Constants;
					Form1.ChartSpace2.DataSource =Form1.Spreadsheet2;
				        
					// Add three series to the chart.
					Form1.ChartSpace2.Charts(0).SeriesCollection.Add();
					Form1.ChartSpace2.Charts(0).SeriesCollection.Add();
					Form1.ChartSpace2.Charts(0).SeriesCollection.Add();
				    
					Form1.ChartSpace2.Charts(0).SeriesCollection(0).SetData( c.chDimSeriesNames, 0, "D1");
					Form1.ChartSpace2.Charts(0).SeriesCollection(0).SetData( c.chDimCategories, 0, "A2:A" + iRows);
					Form1.ChartSpace2.Charts(0).SeriesCollection(0).SetData( c.chDimValues, 0, "D2:D" + iRows);
				    
					Form1.ChartSpace2.Charts(0).SeriesCollection(1).SetData( c.chDimSeriesNames, 0, "E1");
					Form1.ChartSpace2.Charts(0).SeriesCollection(1).SetData( c.chDimCategories, 0, "A2:A" + iRows);
					Form1.ChartSpace2.Charts(0).SeriesCollection(1).SetData( c.chDimValues, 0, "E2:E" + iRows);
				    
					Form1.ChartSpace2.Charts(0).SeriesCollection(2).SetData( c.chDimSeriesNames, 0, "F1");
					Form1.ChartSpace2.Charts(0).SeriesCollection(2).SetData( c.chDimCategories, 0, "A2:A" + iRows);
					Form1.ChartSpace2.Charts(0).SeriesCollection(2).SetData( c.chDimValues, 0, "F2:F" + iRows);
				    
					Form1.ChartSpace2.Charts(0).HasLegend = true;
					Form1.ChartSpace2.Charts(0).Type=<%=DropDownList1.SelectedValue%>;
					Form1.ChartSpace2.Charts(0).Axes(0).Font.Size=10;
					//Form1.ChartSpace2.Charts(0).Axes(0).Font.Color="blue";
					Form1.ChartSpace2.Charts(0).Axes(1).Font.Size=10;
					Form1.ChartSpace2.Charts(0).Legend.Font.Size=9;
					
				}
			
			</script>
			<script language="javascript">
				var cate='<%=DropDownListCategory.SelectedItem.Text%>';
				var Column='C';
				var Count=<%=rowsCount%>;
				switch(cate)
				{
					case '按区域':
						Form1.Spreadsheet1.Range("a2:K<%=rowsCount+1%>").Sort(3, 0);
						Column='C';
						break;
					case '按用户':
						Form1.Spreadsheet1.Range("a2:K<%=rowsCount+1%>").Sort(4, 0);
						Column='D';
						break;
					case '按仪器':
						Form1.Spreadsheet1.Range("a2:K<%=rowsCount+1%>").Sort(5, 0);
						Column='E';
						break;
					case '按销售商':
						Form1.Spreadsheet1.Range("a2:K<%=rowsCount+1%>").Sort(6, 0);//c.ssAscending);
						Column='F';
						break;
					default:
						break;
				}
				Form1.Spreadsheet2.Range("A1").ParseText(cate);
				var begins=2;
				var ends=2;
				var iFillRows=2;
				for(var i=2;i<=Count +1;i++)
				{
					if(i!=Count+1)
					{
						if(Form1.Spreadsheet1.Range(Column + i).value==Form1.Spreadsheet1.Range(Column + (i+1)).value)
						{
							ends++;
							continue;
						}
					}
					else
					{
						ends++;
					}
						FillCategorySpreadSheet(iFillRows,Column,begins,ends);
						iFillRows++;
						begins=i+1;
						ends++;
				}
				if(iFillRows>=3)
					FillChartSpace2(iFillRows-1);
			</script>
			<script language="vbscript">
			Form1.ChartSpace1.Clear
			'Form1.ChartSpace1.type=2
			Form1.ChartSpace1.Charts.Add
			Set c = Form1.ChartSpace1.Constants
		    
			' Set the chart DataSource property to the spreadsheet.
			' It is possible to specify multiple data sources, but this example uses only one.
			Form1.ChartSpace1.DataSource =Form1.Spreadsheet1
		        
			' Add three series to the chart.
			Form1.ChartSpace1.Charts(0).SeriesCollection.Add
			Form1.ChartSpace1.Charts(0).SeriesCollection.Add
			Form1.ChartSpace1.Charts(0).SeriesCollection.Add
		    
			' Connect the chart to data by specifying spreadsheet cell references
			' for the different data dimensions. Notice that the SetData method uses
			' a data source index of 0; this is the first data source, which was previously
			' set to the spreadsheet. If you had created multiple data sources,
			' you could specify the index to any item in the WCDataSources collection for the
			' data source index. For example, if two spreadsheet controls were attached to this
			' chart workspace, you could set data from the first control using index 0
			' and set data from the second control using index 1.
		    
			' Notice that the series name is also bound to a spreadsheet cell. Changing
			' the contents of the cell "B1" will also change the name that appears in the legend.
			' If you don't want this behavior, set SeriesCollection(0).Caption instead of
			' using the SetData method to bind the series name to the spreadsheet.
		    
			' Series one contains election data for Perot.
			' Bind the series name, the category names, and the values.
			Form1.ChartSpace1.Charts(0).SeriesCollection(0).SetData c.chDimSeriesNames, 0, "I1"
			Form1.ChartSpace1.Charts(0).SeriesCollection(0).SetData c.chDimCategories, 0, "A2:A<%=rowsCount+1%>"
			Form1.ChartSpace1.Charts(0).SeriesCollection(0).SetData c.chDimValues, 0, "I2:I<%=rowsCount+1%>"
		    
			' Series two contains election data for Clinton.
			Form1.ChartSpace1.Charts(0).SeriesCollection(1).SetData c.chDimSeriesNames, 0, "J1"
			Form1.ChartSpace1.Charts(0).SeriesCollection(1).SetData c.chDimCategories, 0, "A2:A<%=rowsCount+1%>"
			Form1.ChartSpace1.Charts(0).SeriesCollection(1).SetData c.chDimValues, 0, "J2:J<%=rowsCount+1%>"
		    
			' Series two contains election data for Bush.
			Form1.ChartSpace1.Charts(0).SeriesCollection(2).SetData c.chDimSeriesNames, 0, "K1"
			Form1.ChartSpace1.Charts(0).SeriesCollection(2).SetData c.chDimCategories, 0, "A2:A<%=rowsCount+1%>"
			Form1.ChartSpace1.Charts(0).SeriesCollection(2).SetData c.chDimValues, 0, "K2:K<%=rowsCount+1%>"
		    
			' Make the chart legend visible, format the left value axis as percentage, 
			' and specify that value gridlines are at 10% intervals.
			Form1.ChartSpace1.Charts(0).HasLegend = True
			Form1.ChartSpace1.Charts(0).Type=<%=DropDownList1.SelectedValue%>
			Form1.ChartSpace1.Charts(0).Axes(0).Font.Size=10
			Form1.ChartSpace1.Charts(0).Axes(0).Font.Color=vbBlue
			Form1.ChartSpace1.Charts(0).Axes(1).Font.Size=10
			Form1.ChartSpace1.Charts(0).Legend.Font.Size=9
			
			'Form1.ChartSpace1.Charts(0).Axes(c.chAxisPositionLeft).NumberFormat = "0%"
			'Form1.ChartSpace1.Charts(0).Axes(c.chAxisPositionLeft).MajorUnit = 0.1
			</script>
			<script language="javascript">
			function testDate()
			{
				alert(Form1.Spreadsheet1.Range("I3").NumberFormat);
				alert(Form1.Spreadsheet1.Cells(3,9).value);
				Form1.Spreadsheet1.Range("I3").NumberFormat="";
				alert("1899格式=" + Form1.Spreadsheet1.Cells(3,9).value);
				var date=new Date(Form1.Spreadsheet1.Cells(3,9).value-25569);
				var strDate=date.getFullYear() + "-" + date.getMonth() + "-" + date.getDay();
				alert(strDate);
			}
			</script>
			&nbsp;
		</form>
	</body>
</HTML>
