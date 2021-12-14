<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Analysis.AnalysisMain" Codebehind="AnalysisMain.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>AnalysisMain</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../css/ioffice.css">
		<script language="javascript">
		function edit(func)
		{
			document.all['ButtonSave'].value='�����¹���';
			if(func=='Modi')
			{
				if(Form1.ListBox1.options.length>0&&Form1.ListBox1.selectedIndex>-1)
				{
					var str=Form1.ListBox1.options[Form1.ListBox1.selectedIndex].text;
					str=str.substr(str.indexOf(" ")+1);
					document.all['TextBoxDesc'].value=str.substr(0,str.indexOf(" "));
					document.all['TextBoxDesc'].value=document.all['TextBoxDesc'].value.substr(1,document.all['TextBoxDesc'].value.length-2);
					
					str=str.substr(str.indexOf(" ")+1);
					document.all['TextBoxTableFieldName'].value=str.substr(0,str.indexOf("="));
					str=str.substr(str.indexOf("=")+1);
					document.all['TextBoxRule'].value=str.substr(0,str.lastIndexOf("{-"));
					str=str.substr(str.lastIndexOf("{-")+2);
					document.all['TextBoxWhereClause'].value=str.substr(0,str.lastIndexOf("-}"));
					
					
				}
				else
				{
					alert('��ѡ������');
					return;
				}
				document.all['ButtonSave'].value='�޸ľɹ���';
			}
			document.all['Table1'].style.visibility='';
			
			document.all['op'].value=func;
		}
		
		function selectTableFields(objID,objAllFieldsOpen)
		{
			var sUrl="../input/SelectModalDialog.aspx";
			sUrl +="?../Analysis/SelectAllTableFields.aspx?";
			sUrl +="<%=Request.ServerVariables["Query_String"]%>";
			r=window.showModalDialog(sUrl + '&myFlag=' + objAllFieldsOpen,'','dialogWidth:588px;dialogHeight:468px;resizable:yes;scroll:auto;status:no');
			if (r != '' && typeof(r) != 'undefined')
			{
				document.all[objID].value=document.all[objID].value + ' ' + r;
			}
			document.all[objID].value=document.all[objID].value.replace("  "," ");
			if(document.all[objID].value.indexOf(" ")==0)
				document.all[objID].value=document.all[objID].value.substr(1);
		}
		
		function Calc(strCalu)
		{
			var objID='TextBoxRule';
			document.all[objID].value=document.all[objID].value + ' ' + strCalu;
			document.all[objID].value=document.all[objID].value.replace("  "," ");
			if(document.all[objID].value.indexOf(" ")==0)
				document.all[objID].value=document.all[objID].value.substr(1);
		}
		
		function CalcB(strCalu)
		{
			var objID='TextBoxWhereClause';
			document.all[objID].value=document.all[objID].value + ' ' + strCalu;
			document.all[objID].value=document.all[objID].value.replace("  "," ");
			if(document.all[objID].value.indexOf(" ")==0)
				document.all[objID].value=document.all[objID].value.substr(1);
		}
		
		function CalcA(strCalu)
		{
			var objID='TextBoxRule';
			if(document.all[objID].value.indexOf(strCalu + "(")!=0)
				document.all[objID].value=strCalu + "(" + document.all[objID].value + ')';
			document.all[objID].value=document.all[objID].value.replace("  "," ");
			if(document.all[objID].value.indexOf(" ")==0)
				document.all[objID].value=document.all[objID].value.substr(1);
		}
		
		</script>
		<script id="clientEventHandlersJS" language="javascript">
		<!--

		function Form1_onsubmit() {

		}

		//-->
		</script>
		<script language="javascript" for="buttonDelete" event="onclick">
			document.all['op'].value='Del';
		</script>
		
		<script language="javascript" for="ListBox1" event="onpropertychange">
			edit('Modi');
		</script>
</HEAD>
	<body bottomMargin="5" leftMargin="5" topMargin="5" rightMargin="5">
		<form id="Form1" method="post" runat="server" language="javascript" onsubmit="return Form1_onsubmit()">
			����ͳ�ƹ���
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="300" border="0">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="680" border="1" style="WIDTH: 680px; HEIGHT: 128px">
							<TR>
								<TD style="WIDTH: 749px">
									<asp:ListBox id="ListBox1" runat="server" Width="736px" Height="120px"></asp:ListBox></TD>
								<TD><FONT face="����">
										<TABLE id="Table4" style="WIDTH: 32px; HEIGHT: 41px" cellSpacing="0" cellPadding="0" width="32"
											border="0">
											<TR>
												<TD><FONT face="����">����</FONT></TD>
											</TR>
											<TR>
												<TD><FONT face="����">����</FONT></TD>
											</TR>
										</TABLE>
									</FONT>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 47px"><FONT face="����"><INPUT id="buttNew" type="button" value="��������" onclick="edit('Add')">
							&nbsp;<INPUT type="button" value="�޸Ĺ���" onclick="edit('Modi')">&nbsp;
							<asp:Button id="ButtonDelete" runat="server" Text="ɾ������" onclick="ButtonDelete_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp; 
<INPUT type=button value=����ֱ�ӱ�д onclick="window.open('../CodeXMLEditor.aspx?file=<%=System.Configuration.ConfigurationSettings.AppSettings["SharedDirectory"].Replace("\\","\\\\") +"/Configuration/ConfigurationXml/"+"1"+"/"+Request.QueryString["name"]%>/Analysis.xml&CreateNew=New','_self')"></FONT></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1" style="VISIBILITY: hidden">
							<TR>
								<TD noWrap><FONT face="����">��������</FONT></TD>
								<TD style="WIDTH: 295px">
									<asp:TextBox id="TextBoxDesc" runat="server" Width="360px"></asp:TextBox></TD>
								<TD><FONT face="����"></FONT></TD>
							</TR>
							<TR>
								<TD><FONT face="����">ͳ���ֶ�</FONT></TD>
								<TD style="WIDTH: 295px">
									<asp:TextBox id="TextBoxTableFieldName" runat="server" Width="360px"></asp:TextBox></TD>
								<TD><INPUT type="button" value="ѡ��..." onclick="selectTableFields('TextBoxTableFieldName',false)"></TD>
							</TR>
							<TR>
								<TD><FONT face="����">ͳ�ƹ���</FONT></TD>
								<TD style="WIDTH: 295px">
									<asp:TextBox id="TextBoxRule" runat="server" Width="360px" Height="72px" TextMode="MultiLine"></asp:TextBox></TD>
								<TD vAlign="top" align="left"><INPUT type="button" value="ѡ��..." onclick="selectTableFields('TextBoxRule',false)"><BR>
									<INPUT type="button" value="��" onclick="Calc('+')"><INPUT type="button" value="��" onclick="Calc('-')"><INPUT type="button" value="��" onclick="Calc('*')"><INPUT type="button" value="��" onclick="Calc('/')"><FONT face="����"><BR>
										<INPUT type="button" value="Sum" onclick="CalcA('Sum')"><INPUT type="button" value="Ave" onclick="CalcA('Ave')"></FONT></TD>
							</TR>
							<TR>
								<TD>��������</TD>
								<TD style="WIDTH: 295px">
									<asp:TextBox id="TextBoxWhereClause" runat="server" Width="360px"></asp:TextBox></TD>
								<TD><INPUT type="button" value="ѡ��..." onclick="selectTableFields('TextBoxWhereClause',true)"><br><INPUT onclick="CalcB('=')" type=button value== DESIGNTIMESP="27632"><INPUT type="button" value="and" onclick="CalcB('and')">
								<br></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 17px"><FONT face="����">����:</FONT></TD>
								<TD colSpan="2"><UL>
										<LI>
											<FONT face="����">������ɶ��ģ۹��������ݣ������С�ƣ������ܼƣ�ʹ�����ϼƵȵ�</FONT>
										<LI>
											<FONT face="����">ͳ�ƹ���ֻѡ�������ֶεĹ�ϵ����a��b, a��b,&nbsp;a��b, a��b</FONT>
										<LI>
											<FONT face="����">�������������¼�����С�ƻ�ϼ�ʱ��������, ֻ�е����ⲿ�ֵ������������ݽ��вο�ת��ʱ�������á�</FONT></LI></UL>
								</TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:Button id="ButtonSave" runat="server" Text="����" onclick="ButtonSave_Click"></asp:Button>
									<input type="hidden" id="op" name="op"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
