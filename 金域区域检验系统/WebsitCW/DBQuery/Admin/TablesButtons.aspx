<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.TablesButtons" Codebehind="TablesButtons.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TablesButtons</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<script id="clientEventHandlersJS" language="javascript">
	<!--

	function FireMove(obj)
	{
		if(obj.style.border!='#ccccff thin inset')
			obj.style.border='#ccccff thin outset';
	}
	function FireOut(obj)
	{
		if(obj.style.border!='#ccccff thin inset')
			obj.style.border='#ccccff 0px outset';
	}

	
	function window_onload() {
		DisplayButtonImages();
	}
	
	function DisplayButtonImages()
	{
		LoopSetImg(document.getElementById("Table2").children);
	} 
	
	function LoopSetImg(kids)
	{
		for(var i=0;i<kids.length;i++)
		{
			if(kids[i].nodeName.toUpperCase()=='INPUT'&&kids[i].type.toUpperCase()=='TEXT')
			{
				var InputId=kids[i].id;
					InputId=InputId.substr(7);
					
				if(kids[i].value!="")
				{
					document.getElementById("B" + InputId).src=kids[i].value;
					if(!document.getElementById("CheckBox"+ InputId).checked)
					{
						document.getElementById("B" + InputId).style.display="none";
						document.getElementById("TextBox" + InputId).value="";
					}
					document.getElementById("Button" + InputId).disabled=false;
					if(document.getElementById("CheckBox"+ InputId).disabled&&!document.getElementById("CheckBox"+ InputId).checked)
					{
						document.getElementById("Button" + InputId).disabled=true;
					}
				}
				else
				{
					document.getElementById("B" + InputId).style.display="none";
					document.getElementById("Button" + InputId).disabled=true;
					document.getElementById("CheckBox"+ InputId).checked=false;
					
				}
			}
			if(kids[i].hasChildNodes)
				LoopSetImg(kids[i].childNodes);
		}
	}
	var buttonCollections="";
	function CollectButtons(kids)
	{
		for(var i=0;i<kids.length;i++)
		{
			if(kids[i].nodeName.toUpperCase()=='INPUT'&&kids[i].type.toUpperCase()=='TEXT')
			{
				var InputId=kids[i].id;
					InputId=InputId.substr(7);
				
				buttonCollections+= "|" + kids[i].value;
			}
			if(kids[i].hasChildNodes)
				CollectButtons(kids[i].childNodes);
		}
	}
	
	
	function CheckBoxChange(obj)
	{
		var objID=obj.id;
		objID=objID.substr(8);
		if(obj.checked)
		{
			document.getElementById("B" + objID).style.display="";
			document.getElementById("Button" + objID).disabled=false;
			document.getElementById("TextBox" + objID).value=document.getElementById("B" + objID).src;
			PopupWindowSelectImage(objID);
		}
		else
		{	
			document.getElementById("B" + objID).style.display="none";
			document.getElementById("Button" + objID).disabled=true;
			document.getElementById("TextBox" + objID).value="";
		}
	}
	function PopupWindowSelectImage(strAction)
	{
		var r;
		r=window.showModalDialog("../input/SelectModalDialog.aspx?../Admin/TableButtonsGen.aspx?Action=" + strAction,'',"dialogWidth:588px;dialogHeight:568px;help:no;scroll:auto;status:no;sizable:1");
		if(r!=null&&typeof(r) != 'undefined'&&r!='')
		{
			//alert(r);
			document.getElementById("TextBox" + strAction).value=r;
			document.getElementById("B" + strAction).src=r;
		}
		else
			return null;
	}
	
	function checkSubmit()
	{
		CollectButtons(document.getElementById("Table2").children);
		if(buttonCollections.length>0)
			document.all["ButtonsCollection"].value=buttonCollections.substr(1);
		
		return true;
}

function ShowBatchConfig() {
    r = window.open('BatchConfig.aspx?<%=Request.ServerVariables["Query_string"]%>', '',
    'Width=' + (window.screen.availWidth - 100) + 'px,dialogHeight=' + (window.screen.availHeight-100) + 'px,resizable=yes,scrollbars=yes,status=no');
}
	//-->
	</script>
	<script language="javascript" for="CheckBoxBatch" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="CheckBoxAdd" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="CheckBoxDelete" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="CheckBoxCopy" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="CheckBoxModify" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="CheckBoxSave" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="CheckBoxCancel" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="CheckBoxExport" event="onpropertychange">
		CheckBoxChange(this);
	</script>
	<script language="javascript" for="ButtonSaveAs" event="onclick">
		var r=window.prompt('请输入要保存的按钮组名称,如:出库模板，借阅登记','');
		if(r!=null&&typeof(r) != 'undefined'&&r!='')
		{
			document.all["ButtonsTemplate"].value=r;
			
			CollectButtons(document.getElementById("Table2").children);
			if(buttonCollections.length>0)
				document.all["ButtonsCollection"].value=buttonCollections.substr(1);
			
			//alert(document.all["ButtonsCollection"].value);
			Form1.submit();
		}
		buttonCollections="";
	</script>
	
	<script language="javascript" for="ButtonLoad" event="onclick">
		var r;
		r=window.showModalDialog("../input/SelectModalDialog.aspx?../Admin/TableButtonsSelection.aspx",'',"dialogWidth:768px;dialogHeight:568px;help:no;scroll:auto;status:no;sizable:1");
		if(r!=null&&typeof(r) != 'undefined'&&r!='')
		{
			//alert(r);
			document.getElementById("ButtonsCollection").value=r;
			var AllButtons=r.split("|");
			if(AllButtons[0]!=""&&document.all["CheckBoxBatch"].checked)
				document.all["TextBoxBatch"].value=AllButtons[0];
			document.all["TextBoxAdd"].value=AllButtons[1];
			document.all["TextBoxDelete"].value=AllButtons[2];
			document.all["TextBoxCopy"].value=AllButtons[3];
			document.all["TextBoxModify"].value=AllButtons[4];
			document.all["TextBoxSave"].value=AllButtons[5];
			document.all["TextBoxCancel"].value=AllButtons[6];
			document.all["TextBoxExport"].value=AllButtons[7];
			DisplayButtonImages();
		}
	</script>
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
	</HEAD>
	<body language="javascript" onload="return window_onload()">
		<form id="Form1" method="post" runat="server" onsubmit="return checkSubmit()">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="720" border="0">
				<TR>
					<TD style="WIDTH: 100px" align="center"><IMG src="images/Buttons.jpg" style="BORDER:#ccccff thin outset"></TD>
					<TD style="WIDTH: 99%"><FONT face="宋体">按钮定制 [<b><%=Request.QueryString["TableName"]%></b>]</FONT></TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<P><FONT face="宋体"></FONT></P>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3">
							<TABLE id="Table2" cellSpacing="1" cellPadding="0" width="300" border="0" bgColor="#333399">
								<TR bgcolor="#ffffff">
									<TD noWrap>参考按钮</TD>
									<TD style="WIDTH: 133px"><IMG src="../image/middle/BBatch.jpg"></TD>
									<TD><IMG src="../image/middle/add.jpg"></TD>
									<TD><IMG src="../image/middle/delete.jpg"></TD>
									<TD><IMG src="../image/middle/copy.jpg"></TD>
									<TD><IMG src="../image/middle/gai.jpg"></TD>
									<TD><IMG src="../image/middle/save.jpg"></TD>
									<TD><IMG src="../image/middle/cancel.jpg"></TD>
									<TD><IMG src="../image/middle/export.jpg"></TD>
									<TD></TD>
								</TR>
								<TR bgcolor="#ffffff">
									<TD>选择</TD>
									<TD><asp:checkbox id="CheckBoxBatch" runat="server"  Enabled="False"></asp:checkbox>&nbsp;<input id="ButtonBatch" disabled="disabled" type="button" value="=>" onclick="PopupWindowSelectImage('Batch')" /></TD>
									<TD><asp:checkbox id="CheckBoxAdd" runat="server" Checked="True"></asp:checkbox>&nbsp;<input id="ButtonAdd" type="button" value="=>" name="Button1" onclick="PopupWindowSelectImage('Add')"/></TD>
									<TD><asp:checkbox id="CheckBoxDelete" runat="server" Checked="True"></asp:checkbox>&nbsp;<input id="ButtonDelete" type="button" value="=>" name="Button3" onclick="PopupWindowSelectImage('Delete')"/></TD>
									<TD><asp:checkbox id="CheckBoxCopy" runat="server" Checked="True"></asp:checkbox>&nbsp;<input id="ButtonCopy" type="button" value="=>" name="Button4" onclick="PopupWindowSelectImage('Copy')"/></TD>
									<TD><asp:checkbox id="CheckBoxModify" runat="server" Checked="True"></asp:checkbox>&nbsp;<input id="ButtonModify" type="button" value="=>" name="Button5" onclick="PopupWindowSelectImage('Modify')"/></TD>
									<TD><asp:checkbox id="CheckBoxSave" runat="server" Checked="True"></asp:checkbox>&nbsp;<input id="ButtonSave" type="button" value="=>" name="Button6" onclick="PopupWindowSelectImage('Save')"/></TD>
									<TD><asp:checkbox id="CheckBoxCancel" runat="server" Checked="True"></asp:checkbox>&nbsp;<input id="ButtonCancel" type="button" value="=>" name="Button7" onclick="PopupWindowSelectImage('Cancel')"/></TD>
									<TD><asp:checkbox id="CheckBoxExport" runat="server" Checked="True"></asp:checkbox>&nbsp;<input id="ButtonExport" type="button" value="=>" name="Button8" onclick="PopupWindowSelectImage('Export')"/></TD>
									<TD></TD>
								</TR>
								<TR bgcolor="#ffffff" height="35">
									<TD noWrap>按钮预览</TD>
									<TD noWrap align="center" valign="middle">
										<img id="BBatch" src="../image/middle/BBatch.jpg" width="79" height="24" border="0" onmouseover="FireMove(this)"
											onmouseout="FireOut(this)">
										<asp:TextBox id="TextBoxBatch" runat="server" Width="0px" Height="0px"></asp:TextBox></TD>
									<TD  noWrap align="center" valign="middle">
										<img id="BAdd" src="../image/middle/add.jpg" width="79" height="24" border="0" onmouseover="FireMove(this)"
											onmouseout="FireOut(this)" DESIGNTIMEDRAGDROP="292">
										<asp:TextBox id="TextBoxAdd" runat="server" Width="0px" Height="0px" >../image/middle/add.jpg</asp:TextBox></TD>
									<TD  noWrap align="center" valign="middle">
										<img id="BDelete" src="../image/middle/delete.jpg" width="79" height="24" border="0"
											onmouseover="FireMove(this)" onmouseout="FireOut(this)">
										<asp:TextBox id="TextBoxDelete" runat="server" Width="0px" Height="0px">../image/middle/delete.jpg</asp:TextBox></TD>
									<TD  noWrap align="center" valign="middle">
										<img id="BCopy" src="../image/middle/copy.jpg" width="79" height="24" border="0" onmouseover="FireMove(this)"
											onmouseout="FireOut(this)">
										<asp:TextBox id="TextBoxCopy" runat="server" Width="0px" Height="0px">../image/middle/copy.jpg</asp:TextBox></TD>
									<TD  noWrap align="center" valign="middle">
										<img id="BModify" src="../image/middle/gai.jpg" width="81" height="24" border="0" onmouseover="FireMove(this)"
											onmouseout="FireOut(this)">
										<asp:TextBox id="TextBoxModify" runat="server" Width="0px" Height="0px">../image/middle/gai.jpg</asp:TextBox></TD>
									<TD  noWrap align="center" valign="middle">
										<img id="BSave" src="../image/middle/save.jpg" width="79" height="24" border="0" onmouseover="FireMove(this)"
											onmouseout="FireOut(this)">
										<asp:TextBox id="TextBoxSave" runat="server" Width="0px" Height="0px">../image/middle/save.jpg</asp:TextBox></TD>
									<TD  noWrap align="center" valign="middle">
										<img id="BCancel" src="../image/middle/cancel.jpg" width="79" height="24" border="0"
											onmouseover="FireMove(this)" onmouseout="FireOut(this)">
										<asp:TextBox id="TextBoxCancel" runat="server" Width="0px" Height="0px" Wrap="false">../image/middle/cancel.jpg</asp:TextBox></TD>
									<TD  noWrap align="center" valign="middle">
										<img id="BExport" src="../image/middle/export.jpg" width="79" height="24" border="0"
											onmouseover="FireMove(this)" onmouseout="FireOut(this)">
										<asp:TextBox id="TextBoxExport" runat="server" Width="0px" Height="0px">../image/middle/export.jpg</asp:TextBox></TD>
									<TD></TD>
								</TR>
							</TABLE>
							<br />
						<table border="0">
						    <tr>
						        <td><asp:CheckBox ID="BatchModify" runat="server" Text="批量修改/编辑数据" /></td>
						        <td><input type="button" value="设置参数" onclick="ShowBatchConfig()"/></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						     <tr>
						        <td><asp:CheckBox ID="BatchAdd" runat="server" Text="批量新增数据" Enabled="False" /></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						     <tr>
						        <td><asp:CheckBox ID="BatchCopy" runat="server" Text="批量复制数据" Enabled="False" /></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						     <tr>
						        <td><asp:CheckBox ID="CheckBox9" runat="server" Text="数据导入" Enabled="False" /></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						     <tr>
						        <td><asp:CheckBox ID="CheckBox10" runat="server" Text="评论设置" Enabled="False" /></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						     <tr>
						        <td><asp:CheckBox ID="CheckBox11" runat="server" Text="评论查看" Enabled="False" /></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						     <tr>
						        <td><asp:CheckBox ID="CheckBox12" runat="server" Text="评论回复" Enabled="False" /></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						     <tr>
						        <td><asp:CheckBox ID="CheckBox13" runat="server" Text="评论清理" Enabled="False" /></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						    <tr>
						        <td><asp:CheckBox ID="BatchPrint" runat="server" Text="批量打印"/></td>
						        <td><input type="button" value="设置参数" disabled="disabled" /></td>
						        <td><input type="button" value="设置权限" /></td>
						    </tr>
						    <tr>
						        <td>导出类型</td>
						        <td colspan="2">
						            <asp:CheckBox ID="PLUG" runat="server" Text="Excel插件"/>
						            <asp:CheckBox ID="CSV" runat="server" Text="CSV"/>
						            <asp:CheckBox ID="PDF" runat="server" Text="PDF"/>
						            <asp:CheckBox ID="XML" runat="server" Text="XML"/>
						        </td>
						    </tr>
						    
						</table>
                        
						
							<BR><br>
							<asp:Button id="ButtonSave" runat="server" Text="保存按钮定制" 
                                onclick="ButtonSave_Click"></asp:Button>
							<P><INPUT type="button" value="选择预定义按钮组" id=ButtonLoad><INPUT type="button" value="保存为其他按钮组" id=ButtonSaveAs>
							</P></TD>
							
				</TR>
			</TABLE>
			<input name="ButtonsTemplate" value="" type="hidden">
			<input name="ButtonsCollection" value="" type="hidden">
		</form>
	</body>
</HTML>
