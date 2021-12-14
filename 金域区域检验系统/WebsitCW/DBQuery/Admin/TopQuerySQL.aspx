<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.TopQuerySQL" Codebehind="TopQuerySQL.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>TopQuerySQL</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script id="clientEventHandlersJS" language="javascript">
	<!--

	function Form1_onsubmit() {

	}

var lastTable = null;
			function LinkTableNameMenu(obj)
			{
				obj.style.backgroundColor = "skyblue";
				
				if(lastTable!=null && lastTable!=obj)
				{
					lastTable.style.backgroundColor = "white";
				}
				lastTable = obj;
				/*
				EFieldName Ӣ���ֶ���
				TableName ����(/xx/xxx)
				*/
				BuildFieldTable(obj.title, obj.EFieldName);
				
				//alert(obj.EFieldName);
				//alert(obj.TableName);
				
				//=========�ѱ������������ֶ���ȥ===
				var tempTableName = "";
				if(obj.TableName.indexOf("/") == 0)
				{
					tempTableName = obj.TableName.substr(1);
				}
				//document.Form1.all["hiddenTableName"].value = tempTableName;
				//============End======================
			}
								
			//============�����ֶ�����==========
			function BuildFieldTable(fields, eFields)//�����ֶκ�Ӣ���ֶ�
			{
				var startTableTag = "<Table width=\"100%\" border='0' id='builtedTable' cellspacing='0' cellpadding='0'>";
				var endTableTag = "</table>";
				var strItem = "";
				var arrLength;
				var arrField = new Array();
				var arrEField = new Array();//Ӣ���ֶ���
				
				if(fields != "")
				{
					arrField = fields.split(',');
					arrEField = eFields.split(',');
					
					arrLength = arrField.length;
					
					//==========
					var startTr = "";
					var endTr = "";
					//=========
					var tempFieldName;
					for(var i =0; i<arrLength; i++)
					{
						if(arrField[i].length > 40)
						{
							tempFieldName = arrField[i].substr(0, 30) + "..";
						}
						else
						{
							tempFieldName = arrField[i];
						}
						if( (i+1)%2 != 0)
						{
							startTr = "<tr>";
							endTr = "";	
						}
						else
						{
							startTr = "";
							endTr = "</tr>";
						}
						
						strItem += startTr //"<tr>"
								+"<td width='10'><input type='checkbox' index='" + i  + "' id='chkBox" + i + "'"
								+"></td>"
								+"<td nowrap OnMouseOver='MouseOverField(this)' style='cursor:pointer; cursor:hand'"
								+ "TableFieldEName='" + arrEField[i] + "' "
								+"OnClick='MouseClickField(this)' "
								+ "title='" + arrField[i] + "' "
								+"OnMouseLeave='MouseLeaveField(this)'>" 
								+ tempFieldName	//arrField[i] 
								+ "</td>"//"</td></tr>";
								+ endTr;
					}
					
					document.Form1.all["fieldDiv"].innerHTML = startTableTag + strItem + endTableTag;	
				}
				else
				{
					document.Form1.all["fieldDiv"].innerHTML = startTableTag + endTableTag;
				}
				//alert(strItem);
			}
			
			//================================
			function MouseOverField(obj)
			{
				//obj.style.border='#ccccff 2px outset';
				obj.style.backgroundColor = 'gold';
			}
			function MouseLeaveField(obj)
			{
				//obj.style.border='#ccccff 0px outset';
				obj.style.backgroundColor = '';
			}
			function MouseClickField(obj)
			{
				var resultName = "";
				resultName ="" + obj.TableFieldEName + "";
				
				if(Form1.TextBoxSQL.value.length>0)
					Form1.TextBoxSQL.value +=" and (" + resultName + "='')";
				else
					Form1.TextBoxSQL.value +="(" + resultName + "='')";
				
			//	window.frames[0].Spreadsheet1.Selection.value = obj.title;
			//	window.frames[0].Spreadsheet1.Selection.Rows(2).value = resultName;
			}
			function   CalcA(myValue)   {  
				var myField=document.getElementById("TextBoxSQL");
				if   (document.selection)   {   
				myField.focus();   
				sel   =   document.selection.createRange();   
				sel.text   =   myValue;   
				}   
				else   if   (myField.selectionStart||   myField.selectionStart   ==   "0")   {   
				var   startPos   =   myField.selectionStart;   
				var   endPos   =   myField.selectionEnd;   
				myField.value   =   myField.value.substring(0,   startPos)   
				+   myValue   
				+   myField.value.substring(endPos,   myField.value.length);   
				}   else   {   
				myField.value   +=   myValue;   
				}   
			}   
	//-->
		</script>
</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" bgcolor="gainsboro">
		<form id="Form1" method="post" runat="server" language="javascript" onsubmit="return Form1_onsubmit()">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0" height="100%">
				<TR height="10">
					<TD><FONT face="����">SQL���</FONT></TD>
					<TD width="80%"><TABLE id="tableName" style="BORDER-RIGHT: #0099cc 1px solid; BORDER-TOP: #0099cc 1px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: #0099cc 1px solid; BORDER-BOTTOM: #0099cc 1px solid"
							runat="server">
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD height="10" valign="top">
						<asp:TextBox id="TextBoxSQL" runat="server" Width="400px" Height="216px" TextMode="MultiLine"></asp:TextBox></TD>
					<TD rowspan="4" valign="top" style="HEIGHT:80%"><div id="fieldDiv"><FONT face="����"></FONT></div>
					</TD>
				</TR>
				<TR height="10">
					<TD width="400" nowrap><INPUT type="button" value="ȷ��" onclick="parent.window.returnValue =Form1.TextBoxSQL.value;parent.window.close();">&nbsp;
						<INPUT type="button" value="��������" style="WIDTH: 56px; HEIGHT: 24px" id="ButtonTestData"
							name="Button1" runat="server" onserverclick="ButtonTestData_ServerClick"> <INPUT type="button" value="��" onclick="CalcA('+')" style="WIDTH: 22px; HEIGHT: 24px">
						<INPUT type="button" value="��" onclick="CalcA('-')" style="WIDTH: 22px; HEIGHT: 24px">
						<INPUT type="button" value="��" onclick="CalcA('*')" style="WIDTH: 22px; HEIGHT: 24px"> 
<INPUT style="WIDTH: 22px; HEIGHT: 24px" onclick="CalcA('/')" type=button value=��>&nbsp;
						<INPUT style="WIDTH: 17px; HEIGHT: 24px" onclick="CalcA('=')" type="button" value="��=">&nbsp;
						<INPUT type="button" value="and" onclick="CalcA('and')"> <INPUT type="button" value="or" onclick="CalcA('or')">
						<INPUT type="button" value="not" onclick="CalcA('not')"> <INPUT type="button" value="in" onclick="CalcA('in')">
						<INPUT type="button" value="like" onclick="CalcA('like')"><BR>
						<INPUT onclick="CalcA('getDate()')" type="button" value="��ǰ����">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<INPUT onclick="CalcA('like')" type="button" value="like">&nbsp;</TD>
				</TR>
				<TR height="10">
					<TD valign="top">
						<INPUT type="button" value="count(*)" onclick="CalcA('count(*)')" style="WIDTH: 56px; HEIGHT: 24px"><INPUT type="button" value="Sum()" onclick="CalcA('Sum()')">
						<INPUT type="button" value="top" onclick="CalcA('top')"><INPUT type="button" value="from" onclick="CalcA('from')">
						<INPUT type="button" value="distinct" onclick="CalcA('distinct')" style="WIDTH: 56px; HEIGHT: 24px"><INPUT type="button" value="Ave" onclick="CalcA('Ave')">
						<INPUT type="button" value="join" onclick="CalcA('join')"><INPUT type="button" value="on" onclick="CalcA('on')">
						<FONT face="����">
							<asp:TextBox id="TextBoxPreSQL" runat="server" Width="400px" Height="32px" BackColor="#8080FF">select * from</asp:TextBox></FONT></TD>
				</TR>
				<TR>
					<TD valign="top">
						<asp:TextBox id="TextBox1" runat="server" Width="400px" Height="161px" BackColor="LightSteelBlue"></asp:TextBox><br>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" colspan=2>
      <P>&nbsp;MS<SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"><A 
      href="http://blog.csdn.net/Teng_s2000/archive/2006/02/27/611113.aspx"><SPAN 
      style="FONT-SIZE: 18pt; mso-bidi-font-size: 12.0pt">Sql Server</SPAN><SPAN 
      lang=EN-US style="FONT-SIZE: 18pt; mso-bidi-font-size: 12.0pt"><SPAN 
      lang=EN-US>��������</SPAN></SPAN></A>&nbsp; <?xml:namespace prefix = o 
      />
<o:p></o:p></SPAN>
      <DIV class=postText>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 18pt; TEXT-INDENT: -18pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-list: l0 level1 lfo1; tab-stops: list 18.0pt" 
      align=left><B><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; COLOR: #003300; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"><SPAN 
      style="mso-list: Ignore">1.<SPAN 
      style="FONT: 7pt 'Times New Roman'">&nbsp;&nbsp; 
      </SPAN></SPAN></SPAN></B><B><SPAN 
      style="FONT-SIZE: 12pt; COLOR: #003300; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�ַ�������</SPAN></B><B><SPAN 
      style="FONT-SIZE: 12pt; COLOR: #3399ff; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"> 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></B></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 42pt; TEXT-INDENT: -21pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-list: l0 level2 lfo1; tab-stops: list 42.0pt" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"><SPAN 
      style="mso-list: Ignore">a)<SPAN 
      style="FONT: 7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
      </SPAN></SPAN></SPAN><B><SPAN 
      style="FONT-SIZE: 12pt; COLOR: #3399ff; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�����������</SPAN></B><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"> 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">datalength(Char_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�����ַ��������ַ���<SPAN 
      lang=EN-US>,</SPAN>������������Ŀո� <SPAN 
      lang=EN-US>substring(expression,start,length) </SPAN>����˵��<SPAN 
      lang=EN-US>,</SPAN>ȡ�Ӵ� <SPAN lang=EN-US>right(char_expr,int_expr) 
      </SPAN>�����ַ����ұ�<SPAN lang=EN-US>int_expr</SPAN>���ַ� <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 42pt; TEXT-INDENT: -21pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-list: l0 level2 lfo1; tab-stops: list 42.0pt" 
      align=left><B><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; COLOR: #3399ff; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"><SPAN 
      style="mso-list: Ignore">b)<SPAN 
      style="FONT: 7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp; 
      </SPAN></SPAN></SPAN></B><B><SPAN 
      style="FONT-SIZE: 12pt; COLOR: #3399ff; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�ַ������� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></B></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">upper(char_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">תΪ��д 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">lower(char_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">תΪСд 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">space(int_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����<SPAN 
      lang=EN-US>int_expr</SPAN>���ո� <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">replicate(char_expr,int_expr)</SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�����ַ���<SPAN 
      lang=EN-US>int_expr</SPAN>�� <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">reverse(char_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">��ת�ַ��� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">stuff(char_expr1,start,length,char_expr2) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���ַ���<SPAN 
      lang=EN-US>char_expr1</SPAN>�еĴ� <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">start</SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">��ʼ��<SPAN 
      lang=EN-US>length</SPAN>���ַ���<SPAN lang=EN-US>char_expr2</SPAN>���� <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ltrim(char_expr) 
      rtrim(char_expr) </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ȡ���ո� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ascii(char) 
      char(ascii) </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">��������Ӧ<SPAN 
      lang=EN-US>,</SPAN>ȡ<SPAN lang=EN-US>ascii</SPAN>��<SPAN 
      lang=EN-US>,</SPAN>����<SPAN lang=EN-US>ascii</SPAN>��ȡ�ַ� <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 42pt; TEXT-INDENT: -21pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-list: l0 level2 lfo1; tab-stops: list 42.0pt" 
      align=left><B><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; COLOR: #3399ff; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"><SPAN 
      style="mso-list: Ignore">c)<SPAN 
      style="FONT: 7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp; 
      </SPAN></SPAN></SPAN></B><B><SPAN 
      style="FONT-SIZE: 12pt; COLOR: #3399ff; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�ַ������� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></B></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">charindex(char_expr,expression) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����<SPAN 
      lang=EN-US>char_expr</SPAN>����ʼλ�� <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">patindex("%pattern%",expression) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����ָ��ģʽ����ʼλ��<SPAN 
      lang=EN-US>,</SPAN>����Ϊ<SPAN lang=EN-US>0 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal style="TEXT-ALIGN: left; mso-pagination: widow-orphan" 
      align=left><B><SPAN lang=EN-US 
      style="FONT-SIZE: 13.5pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt; mso-bidi-font-size: 12.0pt">2.</SPAN></B><B><SPAN 
      style="FONT-SIZE: 13.5pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt; mso-bidi-font-size: 12.0pt">��ѧ����</SPAN></B><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt"> 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">abs(numeric_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�����ֵ 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ceiling(numeric_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ȡ���ڵ���ָ��ֵ����С���� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">exp(float_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ȡָ�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">floor(numeric_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">С�ڵ���ָ��ֵ��������� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">pi() 
      3.1415926......... 
<o:p></o:p></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">power(numeric_expr,power) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����<SPAN 
      lang=EN-US>power</SPAN>�η� <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">rand([int_expr]) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����������� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">round(numeric_expr,int_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">��<SPAN 
      lang=EN-US>int_expr</SPAN>�涨�ľ����������� <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">sign(int_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">��������<SPAN 
      lang=EN-US>,0,</SPAN>����<SPAN lang=EN-US>,,</SPAN>����<SPAN 
      lang=EN-US>+1,0,-1 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">sqrt(float_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ƽ���� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal style="TEXT-ALIGN: left; mso-pagination: widow-orphan" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">3.</SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���ں��� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">getdate() 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�������� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">datename(datepart,date_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����������<SPAN 
      lang=EN-US> June 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">datepart(datepart,date_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ȡ����һ���� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">datediff(datepart,date_expr1.dateexpr2) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���ڲ� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">dateadd(datepart,number,date_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�������ڼ���<SPAN 
      lang=EN-US> number 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����������<SPAN 
      lang=EN-US>datepart</SPAN>�� д�� ȡֵ������ <SPAN 
      lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">yy 
      1753-9999 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">��� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">qq 
      1-4 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">mm 
      1-12 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">dy 
      1-366 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">dd 
      1-31 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">wk 
      1-54 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">dw 
      1-7 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�ܼ� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">hh 
      0-23 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">Сʱ 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">mi 
      0-59 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ss 
      0-59 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ms 
      0-999 </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����ת�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">convert() 
      <o:p></o:p></SPAN></P>
      <P class=MsoNormal style="TEXT-ALIGN: left; mso-pagination: widow-orphan" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">4.</SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">ϵͳ����<SPAN 
      lang=EN-US>&nbsp; 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">suser_name() 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�û���¼�� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">user_name() 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�û������ݿ��е����� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">user 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�û������ݿ��е����� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">show_role() 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�Ե�ǰ�û������õĹ��� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">db_name() 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���ݿ��� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">object_name(obj_id) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���ݿ������ 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">col_name(obj_id,col_id) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">col_length(objname,colname) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�г��� 
      <SPAN lang=EN-US>
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">valid_name(char_expr) 
      </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�Ƿ�����Ч��ʶ��<SPAN 
      lang=EN-US> 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">APP_NAME() 
      --</SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">�������ص�ǰִ�е�Ӧ�ó��������<SPAN 
      lang=EN-US> 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">select 
      app_name()<BR>COALESCE() --</SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">���������ڶ���ʽ�е�һ����<SPAN 
      lang=EN-US>NULL </SPAN>���ʽ��ֵ<SPAN 
      lang=EN-US><BR>COL_LENGTH(&lt;'table_name'&gt;, &lt;'column_name'&gt;) 
      --</SPAN>�������ر���ָ���ֶεĳ���ֵ<SPAN lang=EN-US><BR>COL_NAME(, ) 
      --</SPAN>�������ر���ָ���ֶε����Ƽ�����<SPAN lang=EN-US><BR>DATALENGTH() 
      --</SPAN>�����������ݱ��ʽ�����ݵ�ʵ�ʳ���<SPAN lang=EN-US><BR>DB_ID(['database_name']) 
      --</SPAN>�����������ݿ�ı��<SPAN lang=EN-US><BR>DB_NAME(database_id) 
      --</SPAN>�����������ݿ������<SPAN lang=EN-US><BR>HOST_ID() 
      --</SPAN>�������ط������˼����������<SPAN lang=EN-US><BR>HOST_NAME() 
      --</SPAN>�������ط������˼����������<SPAN lang=EN-US> 
<o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal 
      style="MARGIN-LEFT: 52.5pt; TEXT-ALIGN: left; mso-pagination: widow-orphan; mso-para-margin-left: 5.0gd" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">select 
      host_name()<BR>IDENTITY([, seed increment]) [AS 
      column_name])<BR>--IDENTITY() </SPAN><SPAN 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">����ֻ��<SPAN 
      lang=EN-US>SELECT INTO </SPAN>�����ʹ�����ڲ���һ��<SPAN lang=EN-US>identity 
      column</SPAN>�е��±���<SPAN lang=EN-US><BR>/*select identity(int, 1, 1) as 
      column_name<BR>into newtable<BR>from oldtable*/<BR>ISDATE() 
      --</SPAN>�����ж��������ı��ʽ�Ƿ�Ϊ��������<SPAN lang=EN-US><BR>ISNULL(, ) 
      --</SPAN>���������ʽ�е�<SPAN lang=EN-US>NULL </SPAN>ֵ��ָ��ֵ�滻<SPAN 
      lang=EN-US><BR>ISNUMERIC() --</SPAN>�����ж��������ı��ʽ�Ƿ�Ϊ�������ֵ<SPAN 
      lang=EN-US><BR>NEWID() --</SPAN>��������һ��<SPAN lang=EN-US>UNIQUEIDENTIFIER 
      </SPAN>���͵���ֵ<SPAN lang=EN-US><BR>NULLIF(, )<BR>--NULLIF </SPAN>������<SPAN 
      lang=EN-US>expression1 </SPAN>��<SPAN lang=EN-US>expression2 
      </SPAN>���ʱ����<SPAN lang=EN-US>NULL </SPAN>ֵ�������ʱ�򷵻�<SPAN 
      lang=EN-US>expression1 </SPAN>��ֵ<SPAN lang=EN-US> 
      <o:p></o:p></SPAN></SPAN></P>
      <P class=MsoNormal style="TEXT-ALIGN: left; mso-pagination: widow-orphan" 
      align=left><SPAN lang=EN-US 
      style="FONT-SIZE: 12pt; FONT-FAMILY: ����; mso-bidi-font-family: ����; mso-font-kerning: 0pt">
<o:p>&nbsp;</o:p></SPAN></P>
      <P class=MsoNormal><SPAN 
  lang=EN-US>
<o:p>&nbsp;</o:p></SPAN></P></DIV></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
