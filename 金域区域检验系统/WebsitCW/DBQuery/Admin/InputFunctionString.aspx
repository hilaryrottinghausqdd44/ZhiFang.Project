<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.InputFunctionString" Codebehind="InputFunctionString.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function MouseOverBatchField(obj)
		{
			obj.style.border='#ccccff 2px outset';
		}
		function MouseLeaveBatchField(obj)
		{
			obj.style.border='#ccccff 0px outset';
		}
		function Calc(strCalu)
		{
			var objID='txtFunctionRule';
			document.all[objID].value=document.all[objID].value + ' ' + strCalu;
			document.all[objID].value=document.all[objID].value.replace("  "," ");
			if(document.all[objID].value.indexOf(" ")==0)
				document.all[objID].value=document.all[objID].value.substr(1);
		}
		
		function CalcB(strCalu)
		{
			var objID='txtFunctionRule';
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
		
		function ConfirmFunctions()
		{
			var str=Form1.txtName.value;
			str +="|";
			str +=Form1.hNameStyle.value;
			
			var inPara="";
			var inputList=document.all['InPara'].getElementsByTagName('INPUT');
			var inputID;
			for(var i=0;i<inputList.length;i++)
			{
				if(inputList[i].checked)
				{
					inputID=inputList[i].id;
					inputID=inputID.replace('chkDisplay','lblFieldName');
					inPara +="," + document.getElementById(inputID).innerHTML;
					
					//inPara +=inputID + ",";
				}
			}
			if(inPara.length>0)
				inPara=inPara.substr(1);
			str +="|";
			str +=inPara;
			
			str +="|";
			str +=Form1.bFunctionRule.checked;
			str +="|";
			str +=Form1.txtFunctionRule.value;
			str +="|";
			str +=Form1.runFunction.value;
			str +="|";
			str += Form1.txtReturnTargets.value;
			
			if (Form1.rReturnTarget.options.length > 0)
			{
				for(var i=0;i<Form1.rReturnTarget.options.length;i++)
				{
					str +=Form1.rReturnTarget.options[i].value + ",";
				}
				
				str=str.substr(0,str.length-1);
			}
			;

			str = str.replace(/��/g, '+');
			Form1.hFunctionString.value=str;
			
			window.parent.returnValue = str;
			//alert(str);
			//return false;
			window.parent.close();
		}
		function initPage()
		{
			//FunctionRules[0]�����ܰ�ť��
			//FunctionRules[1]�����ܰ�ť���
			//FunctionRules[2]���������
			//FunctionRules[3]���Ƿ��ƹ���
			//FunctionRules[4]�����ƹ��ܻ���㹦�ܹ���
			//FunctionRules[5]���¼�
			//FunctionRules[6]����������
			var strF=Form1.hFunctionString.value.replace(/��/g,'+');
			if(strF.length>0)
			{
				try
				{
					var fList=strF.split("|");
					if(fList.length>6)
					{
						Form1.txtName.value=fList[0];
						Form1.hNameStyle.value=fList[1];
						////////////---------////////////---------////////////---------
						var inPara=fList[2];
						var inputList=inPara.split(",");
						var spanList=document.all['InPara'].getElementsByTagName('SPAN');
						var inputID;
						for(var i=0;i<inputList.length;i++)
						{
							for(var j=0;j<spanList.length;j++)
							{
								if(spanList[j].innerHTML==inputList[i])
								{
									inputID=spanList[j].id;
									inputID=inputID.replace('lblFieldName','chkDisplay');
									document.getElementById(inputID).checked=true;
									//inPara +="," + document.getElementById(inputID).innerHTML;
									break;
								}
							}
						}
						////////////---------////////////---------////////////---------
						if(fList[3].toUpperCase()=='TRUE')
							Form1.bFunctionRule.checked=true;
						else
							Form1.bFunctionRule.checked=false;
						Form1.txtFunctionRule.value=fList[4];
						Form1.runFunction.value = fList[5];
						Form1.txtReturnTargets.value = fList[6];
						
						//����һ��÷��ش������������ݴ���
						//Form1.runFunction.value=fList[5];
						
					}
					else
						alert('��������˭�ܽ���ѽ����������' + fList.length + fList);
				}
				catch(e)
				{
					alert('������');
				}
			}
		}
		</script>
		<script id="clientEventHandlersJS" language="javascript">
		<!--

		function window_onload() {
			initPage();
		}

		//-->
		</script>
		<script language=javascript for="hFunctionString" event="onpropertychange">
			initPage();
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="5" topMargin="5" rightMargin="0" language="javascript"
		onload="return window_onload()">
		<%//=Request.ServerVariables["Query_String"]%>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="1" style="BORDER-RIGHT: #3399ff 1px solid; BORDER-TOP: #3399ff 1px solid; BORDER-LEFT: #3399ff 1px solid; BORDER-BOTTOM: #3399ff 1px solid"
				borderColorLight="#ccccff">
				<TR>
					<TD colSpan="3"><STRONG><FONT face="����" color="#6633ff" size="4">[<%=Request.QueryString["TableName"]%>] 
								����������</FONT></STRONG></TD>
				</TR>
				<TR>
					<TD><STRONG><LABEL for="NameIt" style="CURSOR:hand">������������</LABEL></STRONG></TD>
					<TD><INPUT type="text" value="�������к�" id="txtName"> <INPUT onclick="selectTableFields('TextBoxRule',false)" type="button" value="�������"><INPUT id="hNameStyle" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1"><INPUT type="button" value="Ԥ��"></TD>
					<TD><FONT face="����">����Ϊ�ù�������</FONT></TD>
				</TR>
				<TR>
					<TD><FONT face="����"><STRONG>�������</STRONG></FONT></TD>
					<TD id=InPara><asp:datalist id="dataListAllField" Runat="server" BorderColor="#99CCCC" GridLines="Both" BorderWidth="1px"
							RepeatColumns="3" Width="100%">
							<ItemTemplate>
								<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>
								<asp:Label ID="lblFieldName" Runat="server" style="cursor:hand"></asp:Label>
							</ItemTemplate>
						</asp:datalist>
					</TD>
					<TD>
						<P><FONT face="����">����ѡ��ִ�д˹���ʱ��Ҫ�Ĳ���<BR>
								���������кţ�����ݴ���Ĳ���<BR>
								��������Ӧ�ı���<BR>
								����ݵ�ǰ������к���</FONT></P>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 101px"><FONT face="����"><STRONG>���ù��ܹ���</STRONG></FONT></TD>
					<TD style="HEIGHT: 101px">
						<P><INPUT type="checkbox" id="bFunctionRule" CHECKED><LABEL for="FunctionRule" style="CURSOR:hand">��Ҫ�����ⲿ����</LABEL><INPUT onclick="selectTableFields('TextBoxRule',false)" type="button" value="ѡ��..." disabled><BR>
							<TEXTAREA id="txtFunctionRule" style="WIDTH: 320px; HEIGHT: 92px" rows="5" cols="37"></TEXTAREA><BR>
							<INPUT onclick="Calc('+')" type="button" value="��"><INPUT onclick="Calc('-')" type="button" value="��"><INPUT onclick="Calc('*')" type="button" value="��"><INPUT onclick="Calc('/')" type="button" value="��"><INPUT onclick="selectTableFields('TextBoxRule',false)" type="button" value="ѡ��..." disabled></P>
						<P><FONT face="����">ִ�й���ʱ��<BR>
								<INPUT style="WIDTH: 96px; HEIGHT: 22px" type="text" size="12" value="onclick" id="runFunction">
								<input type="radio" checked name="EventName" onclick="document.all['runFunction'].value='onclick';">����
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='ondblclick';">˫��
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='onfocus';">�۽�
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='onlostfocus';">ʧ��
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='onpropertychange';">�ı�</FONT></P>
					</TD>
					<TD style="HEIGHT: 101px" vAlign="top">
						<P><FONT face="����"><a href="../help/hlpInputFuncString.aspx" target="_blank">���ù���˵��������</a></FONT></P>
						<P><FONT face="����">���ⲿ��������</FONT></P>
						<P><FONT face="����">��������</FONT></P>
						<P><FONT face="����">ִ��ʱ����onclick,ondblclick<BR>
								onfocus,onlostfocus,onchange<BR>
								onpropertychange,onkeydown<BR>
								onkeyup,�ȵ�</FONT></P>
					</TD>
				</TR>
				<TR>
					<TD><FONT face="����"><STRONG>���ؽ��</STRONG></FONT></TD>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD rowspan="4">
									<SELECT style="WIDTH: 256px; HEIGHT: 88px" size="5" id="rReturnTarget">
									</SELECT><br />
									<input type="text" id="txtReturnTargets" />
									</TD>
								<TD><INPUT onclick="" type="button" value="��"></TD>
							</TR>
							<TR>
								<TD><INPUT onclick="" type="button" value="��"></TD>
							</TR>
							<TR>
								<TD><INPUT onclick="" type="button" value="++"></TD>
							</TR>
							<TR>
								<TD><INPUT onclick="" type="button" value="--"></TD>
							</TR>
						</TABLE>
					</TD>
					<TD vAlign="top">
						<P><FONT face="����">���÷��ؽ�����ֶ�˳��<BR>
								��A1,A2,A3</FONT></P>
						<P><FONT face="����">���û�����÷��ؽ��Ŀ���ֶΣ�<BR>
								��Ĭ��Ϊ��������ǰѡ���ֶ�</FONT></P><br><a href="#"
onclick="document.all['vSource'].value=document.all['InPara'].innerHTML">�鿴Դ�ļ�</a>
					</TD>
				</TR>
			</TABLE>
			<FONT face="����">&nbsp;</FONT><INPUT onclick="ConfirmFunctions()" type="button" value="ȷ��"><FONT face="����">&nbsp;
			</FONT><INPUT onclick="javascript:window.parent.close();" type="button" value="ȡ��"><FONT face="����">&nbsp;
				<INPUT id="hFunctionString" type="hidden" value="<%=Request.QueryString["FunctionString"]%>"> </FONT>
			<textarea id="vSource" style="width:100%; height:0px"></textarea>
		</form>
	</body>
</HTML>
