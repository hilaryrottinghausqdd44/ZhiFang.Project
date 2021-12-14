<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Admin.InputConfig" Codebehind="InputConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InputConfig</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			//=================================
			//==========隐藏FieldSet=====
			function CheckedBatch(obj)
			{
				if(obj.checked)
				{
					document.Form1.all["fieldBSettings"].style.display = "";
					document.Form1.all["fieldBtnSettings"].style.display = "";
				}
				else
				{
					document.Form1.all["fieldBSettings"].style.display = "none";
					document.Form1.all["fieldBtnSettings"].style.display = "none";
				}
			}
			
			//==========选择批录字段====================
			function SelectBatchField(obj, strId, strEName)
			{
				if(document.Form1.all["chkBAdd"].checked == true)
				{
					//strId 形式是 chkField+数字
				//	var num = strId.substring(8, strId.length);
					//if(document.Form1.all["dataListAllField__ctl" + num + "_" + strId].checked == true)
					//{
					//document.Form1.all["txtBatchField"].value = obj.innerText;
					document.Form1.all["txtBatchField"].value = strEName;
					document.Form1.all["hiddenFieldType"].value = obj.title;//字段类型放入隐藏字段
					//}
					//else
					//{
					//	alert("未配置为显示的录入字段，不能作为批量来源字段");
					//}
				}
			}
			function MouseOverBatchField(obj)
			{
				obj.style.border='#ccccff 2px outset';
			}
			function MouseLeaveBatchField(obj)
			{
				obj.style.border='#ccccff 0px outset';
			}
			//==================END======================
			
			function BatchSettings()//点击增加批量按钮
			{
				var fieldEName = document.Form1.all["txtBatchField"].value;
				var fieldType = document.Form1.all["hiddenFieldType"].value;
				
				if(fieldEName == "")
				{
					alert("还没有选择批入字段");
					return;
				}
				//var r = window.showModalDialog("BatchRowsConfig.aspx?FieldEName=" + fieldEName + "&FieldType=" + fieldType, "", "");
				var r = window.showModalDialog("BatchRowsConfig.aspx?<%=Request.ServerVariables["Query_String"]%>&FieldEName=" + fieldEName, "", "");
				if( typeof(r) != 'undefined')
				{
					document.Form1.all["txtBatchArea"].value = r;
				}
			}
			function DBClickInputFunction(obj,inputFunctionString)
			{
				try
				{
					var DlgRtnValue = window.showModalDialog("SelectModalDialog.aspx?InputFunctionString.aspx?FunctionString=" 
						+ escape(obj.value.replace(/\+/g,'＋')) + "&<%=Request.ServerVariables["Query_String"]%>", "", "status:yes;resizable:yes;dialogHeight:560px;dialogWidth:780px;center:yes");
					if(DlgRtnValue != void 0)
					{
						obj.value = DlgRtnValue;
					}
				}
				catch(e)
				{
					alert('出错了:');
				}
			}
			
			//打开模版编辑窗口
			function EditTemplate()
			{
				var strValue = document.getElementById("dropListTemplate").value;
				
				if(document.Form1.dropListTemplate.selectedIndex==1)
		        {
		            var strValue = document.getElementById("dropListTemplate").value;
		            var strpara = '<%=Request.ServerVariables["Query_String"]%>';
		            //alert('ok'); 
		           var winTemplate = window.open('DynamicModule.aspx?DefaultValue='+strValue+'&'+strpara ,'','resizable=yes,height='+screen.availHeight/2+',toolbar=no,top='+screen.availHeight/2+'status=yes,scrollbars=yes,Width='+screen.availWidth/2+',left=0,top=0');
    			  
	            }
	            else
	            {
	        
				    //alert(strValue);
				    //window.open("InputTemplateConfig.aspx?Template="+strValue ,"","height="+h+",width="+w+",left="+left+",right="+right);
				    var strpara = '<%=Request.ServerVariables["Query_String"]%>';
				    //alert(strpara);
				    var winTemplate = window.open('InputTemplateConfig.aspx?DefaultValue='+strValue+'&'+strpara ,'','resizable=yes,height='+screen.availHeight+',toolbar=no,status=yes,scrollbars=yes,Width='+screen.availWidth+',left=0,top=0');
				}
			}
		</script>
		<script language="javascript" event="onclick" for="chkopen">
		
		//if(chkopen.checked == true)
		//{
		//    if(document.Form1.dropListTemplate.selectedIndex==0)
		//    {
		//        alert('请选择相应模板');
		//       document.Form1.chkopen.checked=false; 
		  
		//    }
	    //}
		   
			//if(chkopen.checked==true)
			//{
			//	fieldOSettings.style.display="";
			//}
			//else
			//{
			//	fieldOSettings.style.display="none";
			//}
			
		</script>
		<script language="javascript" event="onchangeNone" for="dropListTemplate">
		    if(document.Form1.dropListTemplate.selectedIndex==1)
		    {
		        var strValue = document.getElementById("dropListTemplate").value;
		        var strpara = '<%=Request.ServerVariables["Query_String"]%>';
		        //alert('ok'); 
		       var winTemplate = window.open('DynamicModule.aspx?DefaultValue='+strValue+'&'+strpara ,'','resizable=yes,height='+screen.availHeight/2+',toolbar=no,top='+screen.availHeight/2+'status=yes,scrollbars=yes,Width='+screen.availWidth/2+',left=0,top=0');
				   
	        }
		   
		</script>
		
		<script language="javascript" event="onpropertychange" for="CheckBoxSMS">
		    if(document.Form1.CheckBoxSMS.checked)
		    {
		        document.Form1.buttSMSConfig.disabled=false; 
	        }
	        else
	            document.Form1.buttSMSConfig.disabled=true; 
		</script>
		<%--
	<body oncontextmenu="end()" onmousemove="drag()" onselectstart="return false;" MS_POSITIONING="GridLayout">
	--%>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<table style="BORDER-RIGHT: #6699cc 2px solid; BORDER-TOP: #6699cc 2px solid; BORDER-LEFT: #6699cc 2px solid; BORDER-BOTTOM: #6699cc 2px solid"
				width="100%" align="center" border="0">
				<tr>
					<td colSpan="3">
						<fieldset style="WIDTH: 100%"><legend>配置信息</legend>
							<span style="WIDTH: 26.7%; HEIGHT: 22px; TEXT-ALIGN: center">录入列数<asp:dropdownlist id="dropDownListCols" Runat="server"></asp:dropdownlist></span>
							<SPAN style="WIDTH: 18.95%; HEIGHT: 20px; TEXT-ALIGN: center">
								<asp:CheckBox id="CheckBoxSubsIuputShow" runat="server" Text="启用子表同屏录入" Width="130px"></asp:CheckBox></SPAN>
								<span><asp:button id="btnSave" Runat="server" Text="保存" onclick="btnSave_Click"></asp:button><asp:CheckBox id="CheckBoxSMS" runat="server" Text="启用短信集成"></asp:CheckBox>
									<input ID="buttSMSConfig" type="button" runat="server" value="配置" onclick="return SMSConfig()" /><SPAN id="SpanSubMeShow" style="WIDTH: 32.86%; HEIGHT: 19px; TEXT-ALIGN: center" runat="server">
									<asp:CheckBox id="CheckBoxSubMeInputShow" runat="server" Text="在父表中多行录入"></asp:CheckBox>
									<asp:TextBox id="TextBoxSubMeInputLines" runat="server" Width="29px">10</asp:TextBox>行
									
									</SPAN>
									</span></fieldset>
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">
						<fieldset id="fieldOSettings" style="WIDTH: 610px; HEIGHT: 72px"><legend>录入方式及按钮位置配置</legend>&nbsp;&nbsp; 
							<!--<asp:button id="Button1" runat="server" Text="增加/编辑模板"></asp:button>-->
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<TABLE id="Table2" cellSpacing="1" cellPadding="0" border="0" width="100%" bgColor="#9999ff">
								<TR bgcolor="#ffffff">
									<TD nowrap="nowrap"><FONT face="宋体"><SPAN style="WIDTH: 87.42%; HEIGHT: 16px; TEXT-ALIGN: center">
												<asp:checkbox id="chkopen" runat="server" Text="弹出"></asp:checkbox></SPAN></FONT></TD>
								    <td><asp:checkbox id="chkNew" runat="server" Text="新增" Visible="False"></asp:checkbox>
								    <asp:checkbox id="chkModify" runat="server" Text="修改" Visible="False"></asp:checkbox>
								    <asp:checkbox id="chkBrowse" runat="server" Text="浏览" Visible="False"></asp:checkbox>
								    <asp:checkbox id="chkHideInput" runat="server" Text="隐藏录入界面"></asp:checkbox>
								    </td>
									<TD><FONT face="宋体">录入模版&nbsp;
											<asp:dropdownlist id="dropListTemplate" runat="server">
											<asp:ListItem Text="--默认--" value = "0"></asp:ListItem>
											<asp:ListItem Text="[动态模板]" Value="[动态模板]"></asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD><FONT face="宋体"><INPUT onclick="EditTemplate()" type="button" value="增加/编辑模板"></FONT></TD>
									<TD>按钮:</TD>
									<TD><FONT face="宋体">
											<asp:RadioButton id="RBButLocBot" runat="server" Text="下" GroupName="LocationButtons" Checked="True"></asp:RadioButton>
										</FONT>
									</TD>
									<TD><label for="RBButLocBot"><IMG onclick="javscript:Form1.RBButLocBot.checked=true;" style="CURSOR:hand" src="images/0005_a_1.gif"></label></TD>
									<TD>
										<asp:RadioButton id="RBButLocIn" runat="server" Text="上" GroupName="LocationButtons"></asp:RadioButton></TD>
									<TD><label for="RBButLocIn"><IMG onclick="javscript:Form1.RBButLocIn.checked=true;Form1.chkopen.checked=true;alert('弹出方式自动选定,此操作不支持同屏录入')"
												style="CURSOR:hand" src="images/0005_a.gif"></label></TD>
								</TR>
								<TR bgcolor="#ffffff">
									<TD colSpan="8"><FONT face="宋体">
											<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="1">
												<TR>
													<TD><FONT face="宋体">弹出界面(或浏览界面)位置(水平,垂直)与大小(宽,高)</FONT></TD>
													<TD style="WIDTH: 112px">
														<asp:TextBox id="TextBoxPositionSize" runat="server" Width="112px" Height="25px">中,中,80%,80%</asp:TextBox></TD>
													<TD colSpan="8"><FONT face="宋体"><INPUT onclick="alert('请先手工设置位置(水平,垂直)与大小(宽,高)比例')" type="button" value="设置"></FONT></TD>
												</TR>
											</TABLE>
										</FONT>
									</TD>
								</TR>
							</TABLE>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td vAlign="top" noWrap width="220" colSpan="1">
						<table width="100%" border="0">
							<tr>
								<td noWrap align="center" width="100%">
									<P><FONT face="宋体"><SPAN style="WIDTH: 73.94%; HEIGHT: 14px; TEXT-ALIGN: center">所有字段</SPAN></FONT><FONT face="宋体"></FONT></P>
									<FONT face="宋体">
										<P><FONT face="宋体"><asp:datalist id="dataListAllField" Runat="server" BorderColor="#99CCCC" GridLines="Both" BorderWidth="1px"
													RepeatColumns="2" Width="100%">
													<ItemTemplate>
														<asp:CheckBox ID="chkDisplay" Runat="server"></asp:CheckBox>
														<asp:Label ID="lblFieldName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:datalist></FONT>
									</FONT></P></td>
							</tr>
						</table>
						<br>
						<asp:checkbox id="chkBAdd" Runat="server" Text="批量增加"></asp:checkbox><br>
						<p></p>
						<fieldset id="fieldBSettings"><legend>批量设置</legend>
							<table width="100%" border="1">
								<tr>
									<td noWrap>批量字段</td>
									<td><asp:textbox id="txtBatchField" Runat="server" Width="100" contentEditable="false"></asp:textbox></td>
								</tr>
								<tr>
									<td noWrap>批量记录数</td>
									<td><asp:textbox id="txtBatchArea" Runat="server" Width="100" TextMode="MultiLine" Rows="5"></asp:textbox></td>
								</tr>
								<tr>
									<td align="center" colSpan="2"><input onclick="return BatchSettings()" type="button" value="增加批量" name="btnAddBatch">
									</td>
								</tr>
							</table>
						</fieldset>
						<br>
						<p></p>
						<fieldset id="fieldBtnSettings"><legend>单行增删复设置</legend>
							<table>
								<tr>
									<td><asp:checkboxlist id="chkADC" Runat="server" RepeatDirection="Horizontal">
											<asp:ListItem>增加</asp:ListItem>
											<asp:ListItem>删除</asp:ListItem>
											<asp:ListItem>复制</asp:ListItem>
										</asp:checkboxlist></td>
								</tr>
							</table>
						</fieldset>
						<br>
						<input id="hiddenFieldType" type="hidden" name="hiddenFieldType" runat="server">
					</td>
					<td vAlign="top" align="center" colSpan="2">
						<table>
							<tr>
								<td><asp:datalist id="dataListOrder" Runat="server" Width="100%">
										<ItemTemplate>
											<table id="table1" border="0" style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; BACKGROUND-COLOR: white"
												cellpadding="0" cellspacing="1">
												<%-- onclick="Locate(this);" onmousemove="StartMove(this)"
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
													<td align="left">
														<asp:Label ID="lblType" Runat="server"></asp:Label>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td style="BACKGROUND-COLOR: gainsboro" nowrap>行高度
													</td>
													<td align="left">
														<asp:TextBox ID="txtHeight" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td style="BACKGROUND-COLOR: gainsboro" nowrap>处理功能</td>
													<td align="left">
														<asp:TextBox ID="txtFunction" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												<%--
												<tr style="BACKGROUND-COLOR: whitesmoke">
													<td nowrap style="BACKGROUND-COLOR: gainsboro">排序
													</td>
													<td align="left" nowrap>
														<asp:TextBox ID="txtOrder" Runat="server" Width="60"></asp:TextBox>
													</td>
												</tr>
												--%>
											</table>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
		<div id="divMove" style="Z-INDEX: 100; POSITION: absolute"></div>
		<script language="javascript">
			if(document.Form1.all["chkBAdd"].checked == true)
			{
				document.Form1.all["fieldBSettings"].style.display = "";
				document.Form1.all["fieldBtnSettings"].style.display = "";
			}
			else
			{
				document.Form1.all["fieldBSettings"].style.display = "none";
				document.Form1.all["fieldBtnSettings"].style.display = "none";
			}
			
			
			//if(document.all.chkopen.checked==true)
			//{
			//	document.all.fieldOSettings.style.display="";
			//}
			//else
			//{
			//	document.all.fieldOSettings.style.display="none";
			//}


			function window_onload() {
			    if (document.Form1.CheckBoxSMS.checked) {
			        document.Form1.buttSMSConfig.disabled = false;
			    }
			    else
			        document.Form1.buttSMSConfig.disabled = true;
			}

			function SMSConfig() {
			    var strpara = '<%=Request.ServerVariables["Query_String"]%>';
			    //alert(strpara);
			    var winTemplate = window.open('../../whcy/whcy_smsconfig.aspx?' + strpara, '', 'resizable=yes,height=' + screen.availHeight + ',toolbar=no,status=yes,scrollbars=yes,Width=' + screen.availWidth + ',left=0,top=0');
			    return false;
			}


        </script>
	</body>
</HTML>
