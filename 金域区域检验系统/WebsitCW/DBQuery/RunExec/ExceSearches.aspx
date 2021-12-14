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
						<TD class="titleFont" style="HEIGHT: 19px" align="center" colSpan="4">�������</TD>
					</TR>
					<TR>
						<TD class="conFont" align="right" width="10%">����
						</TD>
						<TD class="conFont" width="51%"><label><asp:textbox id="txtStarDate" onfocus="setday(this)" runat="server"></asp:textbox>��&nbsp;
								<asp:textbox id="txtEndDate" onfocus="setday(this)" runat="server"></asp:textbox></label></TD>
						<TD class="conFont" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" width="29%"><label><asp:label id="labMSG" runat="server" ForeColor="Red"></asp:label></label></TD>
					</TR>
					<TR>
						<TD class="conFont" style="HEIGHT: 26px" align="right" width="10%">�����Ŀ
						</TD>
						<TD class="conFont" style="HEIGHT: 26px" width="51%"><label><asp:dropdownlist id="ddlItem" runat="server" Width="336px">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="��Ѫ����¡��Ⱦɫ���쳣���">��Ѫ����¡��Ⱦɫ���쳣���</asp:ListItem>
									<asp:ListItem Value="�ܰ�����¡��Ⱦɫ���쳣���">�ܰ�����¡��Ⱦɫ���쳣���</asp:ListItem>
									<asp:ListItem Value="��������ѪȾɫ����">��������ѪȾɫ����</asp:ListItem>
									<asp:ListItem Value="��������ѪȾɫ����+Ƕ����Ⱦɫ����">��������ѪȾɫ����+Ƕ����Ⱦɫ����</asp:ListItem>
									<asp:ListItem Value="�߷ֱ���Ⱦɫ����">�߷ֱ���Ⱦɫ����</asp:ListItem>
									<asp:ListItem Value="��Ѫ��������ӫ��ԭλ�ӽ�(FISH)���">��Ѫ��������ӫ��ԭλ�ӽ�(FISH)���</asp:ListItem>
									<asp:ListItem Value="��Ѫ��������ʯ����Ƭ/ӡƬӫ��ԭλ�ӽ�(FISH)���">��Ѫ��������ʯ����Ƭ/ӡƬӫ��ԭλ�ӽ�(FISH)���</asp:ListItem>
									<asp:ListItem Value="�Ŵ��Լ���ӫ��ԭλ�ӽ�(FISH)���">�Ŵ��Լ���ӫ��ԭλ�ӽ�(FISH)���</asp:ListItem>
									<asp:ListItem value="�����ںϻ����PCR���Ӽ��"></asp:ListItem>
									<asp:ListItem value="����XȾɫ���ۺ���������"></asp:ListItem>
									<asp:ListItem value="�Ŵ��Զ�������ͻ����"></asp:ListItem>
								</asp:dropdownlist></label></TD>
						<TD class="conFont" style="HEIGHT: 26px" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" style="HEIGHT: 26px" width="29%">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="conFont" align="center" colSpan="4"><asp:label id="labSelectMsg" runat="server" ForeColor="Red"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TD class="conFont" align="right" width="10%">���</TD>
						<TD class="conFont" width="51%"><label><asp:dropdownlist id="ddlResult" runat="server" Width="80px">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="����">����</asp:ListItem>
									<asp:ListItem Value="�쳣">�쳣</asp:ListItem>
									<asp:ListItem Value="����">����</asp:ListItem>
								</asp:dropdownlist></label>ISCN
							<asp:textbox id="txtISCN" runat="server"></asp:textbox></TD>
						<TD class="conFont" align="right" width="12%">&nbsp;</TD>
						<TD class="conFont" width="29%"><label></label></TD>
					</TR>
					<TR>
						<TD class="conFont" style="WIDTH: 84px" align="right" width="84">&nbsp;</TD>
						<TD class="conFont" width="51%"><asp:button id="btnSearch" runat="server" Text="��ѯ" onclick="btnSearch_Click"></asp:button></TD>
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
						<TD class="conFont" colSpan="4">�������</TD>
					</TR>
					<TR>
						<TD colSpan="4">
							<TABLE id="Info" cellSpacing="1" cellPadding="1" width="100%" bgColor="#000000" border="0"
								runat="server">
								<thead>
									<TR>
										<TD class="conFont" align="center" width="10%" bgColor="#ffffff" rowSpan="2">ϵͳ���</TD>
										<TD class="conFont" align="center" width="10%" bgColor="#ffffff" rowSpan="2">������</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff" rowSpan="2">��������</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff" rowSpan="2">��������</TD>
										<TD class="conFont" align="center" width="13%" bgColor="#ffffff" rowSpan="2">��Ʒ��������</TD>
										<TD class="conFont" align="center" bgColor="#ffffff" colSpan="4">��ʾ����(��ѡ)</TD>
									</TR>
									<TR>
										<TD class="conFont" align="center" width="16%" bgColor="#ffffff">�����Ŀ</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff">�����</TD>
										<TD class="conFont" align="center" width="13%" bgColor="#ffffff">ISCN</TD>
										<TD class="conFont" align="center" width="12%" bgColor="#ffffff">����</TD>
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
