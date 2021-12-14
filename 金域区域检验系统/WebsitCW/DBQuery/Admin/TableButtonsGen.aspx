<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.TableButtonsGen" Codebehind="TableButtonsGen.aspx.cs" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TableButtonsGen</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
		BODY { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		A { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		TABLE { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		DIV { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		SPAN { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		TD { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		TH { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		INPUT { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		SELECT { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
		BODY { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px }
		</style>
		<script id="clientEventHandlersJS" language="javascript">
	<!--

	function ReturnImg(img) {
		if(img=="")
		{
			alert("请选择一个图片");
			return;
		}
		var path='../Buttons/<%=Request.QueryString["Action"]%>/';

		parent.window.returnValue = path + img;
		//alert(path + img);
		parent.window.close();
	}

	function deleteTemplet(fileName)
	{
		if(confirm('真的要删除这个文件吗？\r\n一旦删除，该文件将不可恢复，确定没有指定过该图片吗？'))
		{
			document.all['Action'].value='删除';
			document.all['FileName'].value=fileName;
			Form1.submit();
		}
		return false;
	}
	function SpanClick()
	{
		var r = window.showModalDialog("../htmledit/Dialog/selcolor.htm", "",  "dialogWidth:298px;dialogHeight:258px;help:no;scroll:auto;status:no");
		if(r != void 0)
		{
			var colorObj = document.getElementById("TextBoxColor");
			
			colorObj.value = r;
			colorObj.style.color = r;
			
			//var ButtonBackground=document.getElementById("ButtonBackground");
			//ButtonBackground.style.color=r;
		}
	}
	
	function SelectBackground(strAction)
	{
		var pageSize=3;
		var r=window.showModalDialog('../PopupSelectImageFile.aspx?path=' 
		+ 'ButtonsBackground/'+ strAction  +'/&pageSize='
		+ pageSize + '&style=list','','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
		if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
		{
			return;
		}
		else
		{
			var bgObject = document.getElementById("tdButtonBackground");
			bgObject.style.backgroundImage="url(" + r + ")";
			Form1.buttonBackGround.value=r.substr(r.lastIndexOf("/")+1);
		}
	}
	//-->
		</script>
		<script language="javascript" for="TextBoxName" event="onpropertychange">
		var str=this.value;
		str=str.replace(" ","\&nbsp;");
		str=str.replace(" ","\&nbsp;");
		str=str.replace(" ","\&nbsp;");
		str=str.replace(" ","\&nbsp;");
		str=str.replace(" ","\&nbsp;");
		str=str.replace(" ","\&nbsp;");
		str=str.replace(" ","\&nbsp;");
		document.all['ButtonBackground'].innerHTML=str;
		</script>
		<script language="javascript" for="TextBoxColor" event="onpropertychange">
			var r=this.value;
			try
			{
				this.style.color=r;
				var ButtonBackground=document.getElementById("ButtonBackground");
				ButtonBackground.style.color=r;
			}
			catch(e){}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server" encType="multipart/form-data">
			<TABLE id="Table2" cellSpacing="2" cellPadding="0" width="520" border="0" style="WIDTH: 520px; HEIGHT: 100%">
				<TR style="HEIGHT: 42px">
					<TD style="WIDTH: 242px">
						<FIELDSET style="WIDTH: 256px; HEIGHT: 196px"><LEGEND>上传图片按钮文件</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="249" border="0" style="WIDTH: 249px; HEIGHT: 136px">
								<TR>
									<TD width="111" style="WIDTH: 111px" vAlign="top" align="center"><IMG src="images/Buttons.jpg" style="BORDER-RIGHT: #ccccff thin outset; BORDER-TOP: #ccccff thin outset; BORDER-LEFT: #ccccff thin outset; BORDER-BOTTOM: #ccccff thin outset"></TD>
									<TD vAlign="top" colSpan="8"><STRONG>按钮定制</STRONG>：您可以上传制作好的按钮，或重新根据设置制作出自己想要的按钮，不同的按钮在各自的功能中具有更加清晰的含义。<br><br>按钮大小79*24</TD>
								</TR>
								<TR>
									<TD width="7" colSpan="9"><INPUT style="WIDTH: 248px; HEIGHT: 18px" type="file" size="22" name="fileUpload" runat="server"></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="9" height="5">
										<asp:Button id="Button2" runat="server" Text="上传按钮图片" onclick="Button2_Click"></asp:Button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
					<TD style="WIDTH: 299px" align="center">
						<FIELDSET style="WIDTH: 233px; HEIGHT: 184px"><LEGEND>制作新按钮图片</LEGEND>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD colSpan="9" height="5"></TD>
								</TR>
								<TR>
									<TD width="7"></TD>
									<TD colSpan="8">
										<TABLE id="Table5" cellSpacing="0" cellPadding="0" border="1" borderColorDark="#ffffff"
											borderColorLight="#66ccff">
											<TR>
												<TD style="WIDTH: 78px" noWrap>参考按钮</TD>
												<TD style="WIDTH: 132px"><IMG src="../image/middle/<%=Request.QueryString["Action"]%>.jpg"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 78px">按钮文件名</TD>
												<TD style="WIDTH: 132px">
													<asp:TextBox id="ButtonName" runat="server" Width="100px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 78px">按钮显示文字</TD>
												<TD style="WIDTH: 132px">
													<asp:TextBox id="TextBoxName" runat="server" Width="100px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 78px; HEIGHT: 21px" noWrap>文字颜色</TD>
												<TD style="WIDTH: 132px; HEIGHT: 21px">
													<asp:TextBox id="TextBoxColor" runat="server" Width="100px" ForeColor="#6495ed" Font-Bold="True">#6495ed</asp:TextBox><IMG id="imgBack" src="../htmledit/ButtonImage/standard/forecolor.gif" style="CURSOR:hand" onclick="SpanClick()" ></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 78px; HEIGHT: 21px" noWrap>文字大小</TD>
												<TD style="WIDTH: 132px; HEIGHT: 21px">
													<asp:DropDownList id="DropDownListSize" runat="server" Width="100px">
														<asp:ListItem Value="8">8</asp:ListItem>
														<asp:ListItem Value="9">9</asp:ListItem>
														<asp:ListItem Value="10">10</asp:ListItem>
														<asp:ListItem Value="11">11</asp:ListItem>
														<asp:ListItem Value="12"  Selected="true">12</asp:ListItem>
														<asp:ListItem Value="13">13</asp:ListItem>
														<asp:ListItem Value="14">14</asp:ListItem>
														<asp:ListItem Value="15">15</asp:ListItem>
														<asp:ListItem Value="16">16</asp:ListItem>
														<asp:ListItem Value="18">18</asp:ListItem>
														<asp:ListItem Value="20">20</asp:ListItem>
														<asp:ListItem Value="22">22</asp:ListItem>
														<asp:ListItem Value="25">25</asp:ListItem>
														<asp:ListItem Value="30">30</asp:ListItem>
														<asp:ListItem Value="40">40</asp:ListItem>
														<asp:ListItem Value="50">50</asp:ListItem>
														<asp:ListItem Value="60">60</asp:ListItem>
													</asp:DropDownList></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 78px; HEIGHT: 21px" noWrap>按钮背景</TD>
												<TD style="WIDTH: 132px; HEIGHT: 21px">
													<table width="100" height="24" cellspacing="0" cellpadding="0" border="0">
														<tr>
															<td width="82" id="tdButtonBackground" style="BACKGROUND-IMAGE:url(../ButtonsBackground/<%=Request.QueryString["Action"]%>/<%=Request.QueryString["Action"]
														%>.jpg)"><div id="ButtonBackground" style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: #6495ed; FONT-FAMILY: 宋体"></div>
															</td>
															<td width="18"><input type="button" value=".." style="HEIGHT:20px" onclick="SelectBackground('<%=Request.QueryString["Action"]%>')"></td>
														</tr>
													</table>
												</TD>
											</TR>
											<TR>
												<TD noWrap colSpan="10">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:Button id="buttCreate" runat="server" Text="生成按钮" onclick="buttCreate_Click"></asp:Button></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="center" colSpan="9" height="5"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						</FONT>
					</TD>
				</TR>
				<TR height="99%">
					<TD align="left" valign="top" colspan="2">
					<table><tr>	
					<%
					int pageSize=3;
					int i=0;
					if(dirInfo!=null)
					{
						foreach (FileInfo dirFile in dirInfo.GetFiles())
						{i++;
						%>
							<td>
							<table BORDER=1 CELLSPACING="0" CELLPADDING="0" width="160" align=left height="40" bordercolorlight="#99ccff" bordercolor="#ffffff" bordercolordark="#ffffff">
								<tr>
									<td width="120"><IMG id="a<%=i%>" style="cursor:hand; FILTER: progid:DXImageTransform.Microsoft.Alpha( style=0,opacity=72);" 
									src="../buttons/<%Response.Write(Request.QueryString["Action"]+ "/"+dirFile.Name);%>" 
									title="../buttons/<%Response.Write(Request.QueryString["Action"]+ "/"+dirFile.Name);%>" 
									onmouseover="this.border=0;this.filters[0].opacity=100;" 
									onmouseout="this.border=0; this.filters[0].opacity=72;" border="0" 
									onclick="ReturnImg('<%=dirFile.Name%>')"></td>
									<td width="20" valign=middle><IMG style="CURSOR:hand;" src="../images/icons/0014_b.gif" onclick="deleteTemplet('<%=dirFile.Name%>')" style="Display:<%if(dirFile.Name.ToUpper().IndexOf(Request.QueryString["Action"].ToString().ToUpper() + ".")>=0) Response.Write("none");%>"><%if(dirFile.Name.ToUpper().IndexOf(Request.QueryString["Action"].ToString().ToUpper() + ".")>=0) Response.Write("&nbsp;");%></td>
								</tr>
							</table>
							</td>
						<%
						if((double)i/pageSize==(int)i/pageSize)
							Response.Write("</tr><tr>");
						}
					}	
					%>
					</tr></table>
					</td>
					
				</TR>
				<tr>
					<TD style="WIDTH: 242px" colSpan="2" vAlign="top">
						<asp:Label id="lblMessage" runat="server" BorderWidth="1" BorderColor="#ccccff"></asp:Label></TD>
				</tr>
			</TABLE>
			<input name="Action" type="hidden"> <input name="FileName" type="hidden"> <input name="buttonBackGround" type="hidden" value="<%=Request.QueryString["Action"]%>.jpg">
		</form>
	</body>
</HTML>
