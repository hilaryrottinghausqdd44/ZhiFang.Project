<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Style.StyleAdmin" Codebehind="StyleAdmin.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>StyleAdmin</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function DeleteCSSFile()
			{
				if(document.getElementById("dropListStyleFile").selectedIndex == 0)
				{
					alert("没有选择要删除的样式文件");
					return false;
				}
				return true;
			}
			function SaveCSSFile()
			{
				if(document.getElementById("dropListStyleFile").selectedIndex == 0)
				{
					alert("没有选择要保存的样式文件");
					return false;
				}
				
				//window.frames["frmEdit"].Form1.submit();
				return true;
			}
			
			function FileUpLoad()
			{
				var dataBase = document.getElementById("hiddenDatabaseName").value;
				
				var r=window.showModalDialog('UploadStyleDialog.aspx?Database=' + dataBase,'','dialogWidth:398px;dialogHeight:158px;help:no;scroll:auto;status:no');
				//alert(r);
			}
			
			function SpanMouseOver(obj)
			{
				obj.style.backgroundColor = "Red";
				obj.style.border = "1";
			}
			function SpanMouseOut(obj)
			{
				obj.style.backgroundColor = "";
				obj.style.border = "0";
			}
			
			function SpanClick()
			{
				var r = window.showModalDialog("../htmledit/Dialog/selcolor.htm", "",  "dialogWidth:298px;dialogHeight:258px;help:no;scroll:auto;status:no");
				if(r != void 0)
				{
					var colorObj = document.getElementById("txtColor");
					
					//document.getElementById("txtColor").value = r;
					colorObj.value = r;
					colorObj.style.backgroundColor = r;
					
				}
			}
			var parentSet="";
			var topSet="";
			var mainSet="";
			function ZoomAll(obj)
			{	
				//try
				//{
					if(obj.src.indexOf("image/cursors/ico_zoomin.gif")>0)
					{
						obj.src="../image/cursors/ico_zoomall.gif";
						
						var frm=parent.fset;
						parentSet=frm.rows;
						frm.rows="0,*";
						
						var frmTop=top.fset;
						topSet=frmTop.cols;
						frmTop.cols="0,0,*";
						
						var frmMain=top.fsetMain;
						mainSet=frmMain.rows;
						frmMain.rows="0,*";
					}
					else
					{
						obj.src="../image/cursors/ico_zoomin.gif";
						
						var frm=parent.fset;
						frm.rows=parentSet;
						
						var frmTop=top.fset;
						frmTop.cols=topSet;
						
						var frmMain=top.fsetMain;
						frmMain.rows=mainSet;
					}
				//}
				//catch(e)
				//{
				//}
			}
		</script>
		<script language=javascript event="onpropertychange" for="dropListStyleFile">
			window.frames["frmEdit"].location="editstyle.aspx?File=" + escape(Form1.dropListStyleFile.options[Form1.dropListStyleFile.selectedIndex].text);
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="styleTable" width="100%" border="0" height="100%">
				<tr>
					<td vAlign="top" height="20">
						<!--功能配置部分-->
						<table width="100%">
							<tr>
								<td style="WIDTH: 117px" align="center" width="117">风格文件</td>
								<td align="left"><asp:dropdownlist id="dropListStyleFile" Runat="server"></asp:dropdownlist></td>
								<td width="300" align=right><a href="../../main/Index.aspx" target="_top"><IMG id="zoomWhole" height=23 width=23 SRC="../images/icons/0078_b.gif" border="0"></a>&nbsp;&nbsp;&nbsp;<IMG id="zoomWhole" height=23 width=23 SRC="../image/cursors/ico_zoomin.gif" style="cursor:hand;"
				 onmousemove="this.style.border='#ccccff 1px outset'" 
				 onmouseout="this.style.border='#ccccff 0px outset'" 
				 onclick="javascript:ZoomAll(this)"></td>
							</tr>
							<tr>
								<td style="WIDTH: 117px" align="center" width="117">风格颜色</td>
								<td align="left" colSpan="2"><asp:textbox id="txtColor" Runat="server" Width="100"></asp:textbox><span onmouseover="SpanMouseOver(this)" onclick="SpanClick()" onmouseout="SpanMouseOut(this)"><IMG id="imgBack" src="../htmledit/ButtonImage/standard/BackColor.gif"></span>
									&nbsp;&nbsp;<input id="btnUpload" onclick="FileUpLoad()" type="button" value="上传" name="btnUpload">
									&nbsp;&nbsp;<asp:button id="btnDelete" Runat="server" Text="删除" onclick="btnDelete_Click"></asp:button>
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 117px" align="center" width="117">
									<input id="hiddenDatabaseName" type="hidden" runat="server" style="WIDTH: 24px; HEIGHT: 22px"
										size="1"></td>
								<td><asp:button id="btnSave" Runat="server" Text="应用此风格" onclick="btnSave_Click"></asp:button></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD vAlign="top"><iframe id="frmEdit" name="frmEdit" src="editstyle.aspx?file=<%=filePath%>" width="100%" height="100%"></iframe></TD>
				</TR>
			</table>
			<FONT face="宋体"></FONT>
		</form>
	</body>
</HTML>
