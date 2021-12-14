<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.RunExec.ExceSearches" Codebehind="ExceSearches.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ExceSearches</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/Style/cssnew.css" type="text/css" rel="stylesheet">
		<!--#include file="../../Includes/JS/Calendar.js"-->
		<!--script language="javascript" src=../../Includes/JS/Calendar.js></script-->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE width="100%" border="0">
				<TBODY>
					<TR>
						<TD class="titleFont" style="HEIGHT: 19px" align="center" colSpan="4">特殊检索</TD>
					</TR>
					<TR>
						<TD class="conFont" align="right" width="10%">日期
						</TD>
						<TD class="conFont" width="51%"><label><asp:textbox id="txtStarDate" onfocus="setday(this)" runat="server"></asp:textbox>至&nbsp;
								<asp:textbox id="txtEndDate" onfocus="setday(this)" runat="server"></asp:textbox></label></TD>
						<TD class="conFont" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" width="29%"><label><asp:label id="labMSG" runat="server" ForeColor="Red"></asp:label></label></TD>
					</TR>
					<TR>
						<TD class="conFont" style="HEIGHT: 26px" align="right" width="10%">检测项目
						</TD>
						<TD class="conFont" style="HEIGHT: 26px" width="51%"><label><asp:dropdownlist id="ddlItem" runat="server" Width="336px">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="白血病克隆性染色体异常检查">白血病克隆性染色体异常检查</asp:ListItem>
									<asp:ListItem Value="淋巴瘤克隆性染色体异常检查">淋巴瘤克隆性染色体异常检查</asp:ListItem>
									<asp:ListItem Value="常规外周血染色体检查">常规外周血染色体检查</asp:ListItem>
									<asp:ListItem Value="常规外周血染色体检查+嵌合体染色体检查">常规外周血染色体检查+嵌合体染色体检查</asp:ListItem>
									<asp:ListItem Value="高分辨率染色体检查">高分辨率染色体检查</asp:ListItem>
									<asp:ListItem Value="白血病及肿瘤荧光原位杂交(FISH)检查">白血病及肿瘤荧光原位杂交(FISH)检查</asp:ListItem>
									<asp:ListItem Value="白血病及肿瘤石蜡切片/印片荧光原位杂交(FISH)检查">白血病及肿瘤石蜡切片/印片荧光原位杂交(FISH)检查</asp:ListItem>
									<asp:ListItem Value="遗传性疾病荧光原位杂交(FISH)检查">遗传性疾病荧光原位杂交(FISH)检查</asp:ListItem>
									<asp:ListItem value="肿瘤融合基因的PCR分子检查"></asp:ListItem>
									<asp:ListItem value="脆性X染色体综合征基因检测"></asp:ListItem>
									<asp:ListItem value="遗传性耳聋基因突变检查"></asp:ListItem>
								</asp:dropdownlist></label></TD>
						<TD class="conFont" style="HEIGHT: 26px" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" style="HEIGHT: 26px" width="29%">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="conFont" align="center" colSpan="4"><asp:label id="labSelectMsg" runat="server" ForeColor="Red"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TD class="conFont" align="right" width="10%">结果</TD>
						<TD class="conFont" width="51%"><label><asp:dropdownlist id="ddlResult" runat="server" Width="80px">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="正常">正常</asp:ListItem>
									<asp:ListItem Value="异常">异常</asp:ListItem>
									<asp:ListItem Value="其他">其他</asp:ListItem>
								</asp:dropdownlist></label>ISCN
							<asp:textbox id="txtISCN" runat="server"></asp:textbox></TD>
						<TD class="conFont" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" width="29%"><label></label></TD>
					</TR>
					<TR>
						<TD class="conFont" style="WIDTH: 84px" align="right" width="84">&nbsp;</TD>
						<TD class="conFont" width="51%"><asp:button id="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click"></asp:button></TD>
						<TD class="conFont" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" width="29%">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="conFont" style="WIDTH: 84px" align="right" width="84">&nbsp;</TD>
						<TD class="conFont" width="51%">&nbsp;</TD>
						<TD class="conFont" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" width="29%">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="conFont" colSpan="4">检索结果</TD>
					</TR>
					<TR>
						<TD colSpan="4">
							<TABLE id="Info" cellSpacing="1" cellPadding="1" width="100%" bgColor="#000000" border="0"
								runat="server">
								<thead>
									<TR>
										<TD class="conFont" align="center" width="10%" bgColor="#ffffff" rowSpan="2">系统编号</TD>
										<TD class="conFont" align="center" width="10%" bgColor="#ffffff" rowSpan="2">检验编号</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff" rowSpan="2">病人姓名</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff" rowSpan="2">出生日期</TD>
										<TD class="conFont" align="center" width="13%" bgColor="#ffffff" rowSpan="2">样品接收日期</TD>
										<TD class="conFont" align="center" bgColor="#ffffff" colSpan="4">显示内容(可选)</TD>
									</TR>
									<TR>
										<TD class="conFont" align="center" width="16%" bgColor="#ffffff">检测项目</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff">检测结果</TD>
										<TD class="conFont" align="center" width="13%" bgColor="#ffffff">ISCN</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff">其它</TD>
									</TR>
								</thead>
								<tbody>
								</tbody>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD colSpan="4">&nbsp;
							<asp:label id="labInfoMSG" runat="server" ForeColor="Red"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
