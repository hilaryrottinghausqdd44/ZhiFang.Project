<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.RunExec.QCPCR" Codebehind="QCPCR.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QCPCR</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/Style/cssnew.css" type="text/css" rel="stylesheet">
		<script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() {



}

function ShowInfo()
{
	
}


//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" language="javascript" onLoad="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" width="800" border="0">
				<TR>
					<TD align="center">Ⱦɫ�����ʿ�ͳ��</TD>
				</TR>
				<TR>
					<TD class="labTxt">�����Ŀ
						<asp:DropDownList id="DropDownList1" runat="server" Width="160px">
							<asp:ListItem Value="����">����</asp:ListItem>
							<asp:ListItem Value="�ܰͽ�">�ܰͽ�</asp:ListItem>
							<asp:ListItem Value="ʵ������">ʵ������</asp:ListItem>
							<asp:ListItem Value="��ˮ">��ˮ</asp:ListItem>
							<asp:ListItem Value="��ëĤ">��ëĤ</asp:ListItem>
							<asp:ListItem Value="����">����</asp:ListItem>
						</asp:DropDownList>����
						<asp:TextBox id="sTime" runat="server"></asp:TextBox>��
						<asp:TextBox id="eTime" runat="server"></asp:TextBox>
						<asp:Button id="queryBtn" runat="server" Text="�� ѯ"></asp:Button></TD>
				</TR>
				<TR>
					<TD class="labTxt">
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="TabInfo" cellSpacing="1" width="100%" border="0" height="100%" bgcolor="black"
							runat="server">
							<thead bgcolor="#cccccc">
								<TR height="10%" bgcolor="#ffffff">
									<TD class="labTxt">��ʼ����</TD>
									<TD class="labTxt">����</TD>
									<TD class="labTxt">��ɼ��(%)</TD>
									<TD class="labTxt">δ��ɼ��(%)</TD>
									<TD class="labTxt">ʧ��(%)</TD>
									<TD class="labTxt">����(%)</TD>
									<TD class="labTxt">�쳣(%)</TD>
									<TD class="labTxt">����(%)</TD>
								</TR>
							</thead>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="saveBtn" runat="server" Text="�� ��"></asp:Button></TD>
				</TR>
				<TR>
					<TD align="right">
						<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=226&amp;WindowSize=Max&amp;RBACModuleID=487">����(������)</asp:HyperLink>
						<asp:HyperLink id="Hyperlink2" runat="server" NavigateUrl="http://www.digitlab.com.cn/cqpcr/news/browse/HomePage.aspx?id=230&amp;RBACModuleID=492">����ʵ�����ʿ�</asp:HyperLink>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
