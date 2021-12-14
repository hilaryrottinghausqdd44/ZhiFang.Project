<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.BatchDataReselect" Codebehind="BatchDataReselect.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BatchDataReselect</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			function BatchAddField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var listLength = field.options.length;
				
				for(var i=0; i<listLength; i++)
				{
					
					if(field.selectedIndex != -1)
					{
						var selectedText = field.options[field.selectedIndex].text;
						var obj = new Option(selectedText, "", "", "");
						fieldSelected.options[fieldSelected.options.length] = obj;
						field.options[field.selectedIndex] = null;
					}
					
				}
			}
			
			function BatchRemoveField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var listLength = fieldSelected.options.length;
				
				for(var i=0; i<listLength; i++)
				{
					if(fieldSelected.selectedIndex != -1)
					{
						var selectedText = fieldSelected.options[fieldSelected.selectedIndex].text;
						var obj = new Option(selectedText, "", "", "");
						field.options[field.options.length] = obj;
						fieldSelected.options[fieldSelected.selectedIndex] = null;
					}
					else
					{
						//alert("没有选择要移除的项");
					}
				}
			}
			
			function BatchAddAllField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var obj;
				var selectedText;
				
				for(var i=0; i<field.options.length; i++)
				{
					selectedText = field.options[i].text;
					obj = new Option(selectedText, "", "", "");
					fieldSelected.options[fieldSelected.options.length] = obj;
					//field.options[i] = null;
				}
			
				for(var index=field.options.length-1; index>=0; index--)
				{
					field.options[index] = null;
				}
			}
			
			function BatchRemoveAllField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var obj;
				var selectedText;
				
				for(var i=0; i<fieldSelected.options.length; i++)
				{
					selectedText = fieldSelected.options[i].text;
					obj = new Option(selectedText, "", "", "");
					field.options[field.options.length] = obj;
					//field.options[i] = null;
				}
			
				for(var index=fieldSelected.options.length-1; index>=0; index--)
				{
					fieldSelected.options[index] = null;
				}
			}
			
			//=======================点击确定=============
			function ConfirmClick()
			{
				var r = "";
				var obj = document.Form1.all["listBatchSelectedField"];
				for(var i=0; i<obj.length; i++)
				{
					r += obj.options[i].text + ","; 
				}
				
				if(r.charAt(r.length-1) == ",")
				{
					r = r.substring(0, r.length-1);
				}
				
				window.parent.returnValue = r;
				window.parent.close();
			}
			
			
			//=====================End============================
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		
		
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; " width="100%"
				align="center">
				<TR>
					<TD width="40%">
						<asp:listbox id="listBatchAllField" SelectionMode="Multiple" Height="350" Runat="server" Width="100%"></asp:listbox></TD>
					<TD vAlign="middle" align="center" width="20%"><INPUT style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchAddAllField()" type="button" value="添加全部>"
							name="batchAddAll">
						<BR>
						<INPUT style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchAddField()" type="button" value="-->>"
							name="batchAdd">
						<BR>
						<INPUT style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchRemoveField()" type="button" value="<<--"
							name="batchRemove">
						<BR>
						<INPUT style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchRemoveAllField()" type="button"
							value="<全部移除" name="batchRemoveAll">
					</TD>
					<TD width="40%">
						<asp:listbox id="listBatchSelectedField" SelectionMode="Multiple" Height="350" Runat="server"
							Width="100%"></asp:listbox></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" style="Z-INDEX: 102; LEFT: 32px; " width="80%"
				align="center" border="0">
				<TR>
					<TD align="center"><INPUT onclick="return ConfirmClick()" type="button" value="确定" name="btnConfirm"></TD>
					<TD align="center"><INPUT onclick="return window.parent.close()" type="button" value="取消" name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
