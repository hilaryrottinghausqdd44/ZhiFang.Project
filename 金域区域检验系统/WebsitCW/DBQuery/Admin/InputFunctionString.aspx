<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.InputFunctionString" Codebehind="InputFunctionString.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>处理功能</title>
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

			str = str.replace(/＋/g, '+');
			Form1.hFunctionString.value=str;
			
			window.parent.returnValue = str;
			//alert(str);
			//return false;
			window.parent.close();
		}
		function initPage()
		{
			//FunctionRules[0]　功能按钮名
			//FunctionRules[1]　功能按钮风格
			//FunctionRules[2]　传入参数
			//FunctionRules[3]　是否定制功能
			//FunctionRules[4]　定制功能或核算功能规则
			//FunctionRules[5]　事件
			//FunctionRules[6]　传出参数
			var strF=Form1.hFunctionString.value.replace(/＋/g,'+');
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
						
						//留下一块该返回传出参数的内容代码
						//Form1.runFunction.value=fList[5];
						
					}
					else
						alert('不明规则，谁能解释呀？？？？？' + fList.length + fList);
				}
				catch(e)
				{
					alert('出错了');
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
					<TD colSpan="3"><STRONG><FONT face="宋体" color="#6633ff" size="4">[<%=Request.QueryString["TableName"]%>] 
								处理功能配置</FONT></STRONG></TD>
				</TR>
				<TR>
					<TD><STRONG><LABEL for="NameIt" style="CURSOR:hand">命名功能名称</LABEL></STRONG></TD>
					<TD><INPUT type="text" value="生成序列号" id="txtName"> <INPUT onclick="selectTableFields('TextBoxRule',false)" type="button" value="风格设置"><INPUT id="hNameStyle" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1"><INPUT type="button" value="预览"></TD>
					<TD><FONT face="宋体">可以为该功能命名</FONT></TD>
				</TR>
				<TR>
					<TD><FONT face="宋体"><STRONG>输出参数</STRONG></FONT></TD>
					<TD id=InPara><asp:datalist id="dataListAllField" Runat="server" BorderColor="#99CCCC" GridLines="Both" BorderWidth="1px"
							RepeatColumns="3" Width="100%">
							<ItemTemplate>
								<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>
								<asp:Label ID="lblFieldName" Runat="server" style="cursor:hand"></asp:Label>
							</ItemTemplate>
						</asp:datalist>
					</TD>
					<TD>
						<P><FONT face="宋体">可以选择执行此功能时需要的参数<BR>
								如生成序列号，会根据传入的参数<BR>
								来生成相应的编码<BR>
								或根据当前界面进行核算</FONT></P>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 101px"><FONT face="宋体"><STRONG>调用功能规则</STRONG></FONT></TD>
					<TD style="HEIGHT: 101px">
						<P><INPUT type="checkbox" id="bFunctionRule" CHECKED><LABEL for="FunctionRule" style="CURSOR:hand">需要调用外部功能</LABEL><INPUT onclick="selectTableFields('TextBoxRule',false)" type="button" value="选择..." disabled><BR>
							<TEXTAREA id="txtFunctionRule" style="WIDTH: 320px; HEIGHT: 92px" rows="5" cols="37"></TEXTAREA><BR>
							<INPUT onclick="Calc('+')" type="button" value="＋"><INPUT onclick="Calc('-')" type="button" value="－"><INPUT onclick="Calc('*')" type="button" value="×"><INPUT onclick="Calc('/')" type="button" value="÷"><INPUT onclick="selectTableFields('TextBoxRule',false)" type="button" value="选择..." disabled></P>
						<P><FONT face="宋体">执行功能时机<BR>
								<INPUT style="WIDTH: 96px; HEIGHT: 22px" type="text" size="12" value="onclick" id="runFunction">
								<input type="radio" checked name="EventName" onclick="document.all['runFunction'].value='onclick';">单击
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='ondblclick';">双击
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='onfocus';">聚焦
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='onlostfocus';">失焦
								<INPUT type="radio" name="EventName" onclick="document.all['runFunction'].value='onpropertychange';">改变</FONT></P>
					</TD>
					<TD style="HEIGHT: 101px" vAlign="top">
						<P><FONT face="宋体"><a href="../help/hlpInputFuncString.aspx" target="_blank">调用规则说明帮助：</a></FONT></P>
						<P><FONT face="宋体">如外部程序名称</FONT></P>
						<P><FONT face="宋体">或核算规则</FONT></P>
						<P><FONT face="宋体">执行时机：onclick,ondblclick<BR>
								onfocus,onlostfocus,onchange<BR>
								onpropertychange,onkeydown<BR>
								onkeyup,等等</FONT></P>
					</TD>
				</TR>
				<TR>
					<TD><FONT face="宋体"><STRONG>返回结果</STRONG></FONT></TD>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD rowspan="4">
									<SELECT style="WIDTH: 256px; HEIGHT: 88px" size="5" id="rReturnTarget">
									</SELECT><br />
									<input type="text" id="txtReturnTargets" />
									</TD>
								<TD><INPUT onclick="" type="button" value="上"></TD>
							</TR>
							<TR>
								<TD><INPUT onclick="" type="button" value="下"></TD>
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
						<P><FONT face="宋体">设置返回结果的字段顺序<BR>
								如A1,A2,A3</FONT></P>
						<P><FONT face="宋体">如果没有设置返回结果目标字段，<BR>
								则默认为返回至当前选定字段</FONT></P><br><a href="#"
onclick="document.all['vSource'].value=document.all['InPara'].innerHTML">查看源文件</a>
					</TD>
				</TR>
			</TABLE>
			<FONT face="宋体">&nbsp;</FONT><INPUT onclick="ConfirmFunctions()" type="button" value="确定"><FONT face="宋体">&nbsp;
			</FONT><INPUT onclick="javascript:window.parent.close();" type="button" value="取消"><FONT face="宋体">&nbsp;
				<INPUT id="hFunctionString" type="hidden" value="<%=Request.QueryString["FunctionString"]%>"> </FONT>
			<textarea id="vSource" style="width:100%; height:0px"></textarea>
		</form>
	</body>
</HTML>
