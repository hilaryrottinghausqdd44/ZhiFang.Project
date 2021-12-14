<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.TopQueryGroupSelection" Codebehind="TopQueryGroupSelection.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ExecuteSort</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style>
		TABLE { BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid }
		TD { BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid }
		</style>
		<script language="javascript">
			var selectedObj=null;
			var selectedRow=-1;
			function SelectMove(obj,iRow)
			{
				if(selectedObj==null)
				{
					obj.style.backgroundColor="skyblue";
					selectedObj=obj;
					ShowMoving(obj.title);
					selectedRow=iRow;
				}
				else
				{
					if(selectedObj.title!=obj.title)
					{
						selectedObj.style.backgroundColor="transparent";
						DragMoving(selectedObj,obj,selectedRow,iRow);
						ClearMoving();
						obj.style.backgroundColor="yellow";
						selectedRow=-1;
						btnConfirm();
						Form1.submit();
					}
				}	
			}
			var topY=60;
			var leftX=240;
			function moving()
			{
				hover=document.all["showTip"];
				hover.style.top=document.body.scrollTop+event.clientY + 10;
				hover.style.left=document.body.scrollLeft+event.clientX;
			}
			
			function ClearMoving()
			{
				selectedObj=null
				hover=document.all["showTip"];
				hover.style.display="none";
			}
			
			function ShowMoving(strTitle)
			{
				document.all["showTip"].innerHTML=strTitle;
				hover=document.all["showTip"];
				hover.style.display="";
			}
			
			function DragMoving(selectedObj,obj,row,TargetRow)
			{
				document.Form1.all["tableField"].moveRow(row,TargetRow);
				return;
				if(row > -1)
				{
					var currentRow = document.Form1.all["tableField"].rows[row].cells[0].innerHTML;
					document.Form1.all["tableField"].rows[row].cells[0].innerHTML = document.Form1.all["tableField"].rows[TargetRow].cells[0].innerHTML;
					document.Form1.all["tableField"].rows[TargetRow].cells[0].innerHTML = currentRow;
					thisTable.rows[i].swapNode(thisTable.rows[j]);
				}
				return false;
			}
			
			function UPOne(obj)
			{
				//ID = imgUp
				var row = parseInt((obj.name).substring(5, obj.name.length));
				
				if(row > 0)
				{
					var currentRow = document.Form1.all["tableField"].rows[row].cells[0].innerHTML;
					document.Form1.all["tableField"].rows[row].cells[0].innerHTML = document.Form1.all["tableField"].rows[row-1].cells[0].innerHTML;
					document.Form1.all["tableField"].rows[row-1].cells[0].innerHTML = currentRow;
				}
				return false;
			}
	
			function DownOne(obj)
			{
				//ID = imgDown
				var row = parseInt((obj.name).substring(7, obj.name.length));

				if(row < document.Form1.all["tableField"].rows.length-1)
				{
					var currentRow = document.Form1.all["tableField"].rows[row].cells[0].innerHTML;
					document.Form1.all["tableField"].rows[row].cells[0].innerHTML = document.Form1.all["tableField"].rows[row+1].cells[0].innerHTML;
					document.Form1.all["tableField"].rows[row+1].cells[0].innerHTML = currentRow;
				}
				return false;
			}
			
			function btnConfirm()
			{
				var fieldName="";
				for(var num=0; num<document.Form1.all["tableField"].rows.length; num++)
				{
					fieldName += document.Form1.all["tableField"].rows[num].cells[0].innerText + ",";
				}
				fieldName = fieldName.substring(0, fieldName.length-1);
			
				//document.Form1.all["executeSort"].src = "ExecuteSort.aspx?FieldName=" + fieldName + "&<%=Request.ServerVariables["Query_String"]%>";
				document.Form1.all["sortedFieldName"].value = fieldName;
			}
			
			function btnCloseWindow()
			{
				//window.returnValue = "";	
				window.close();
			}
			
			function __doPostBack(eventTarget, eventArgument) {
				var theform;
				if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
					theform = document.Form1;
				}
				else {
					theform = document.forms["Form1"];
				}
				theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
				theform.__EVENTARGUMENT.value = eventArgument;
				theform.submit();
			}
			function SelectThisDic(objName,ObjWhereCause)
			{
				parent.returnValue=objName + "\v" + ObjWhereCause;
				window.close();
			}
			function DBClickDefault(obj)
			{
				var DlgRtnValue = window.showModalDialog("../admin/WinOpenDefaultValueModalDialog.aspx", "", "status:no;resizable:no");
				if(DlgRtnValue != void 0)
				{
					obj.value = DlgRtnValue;
				}
			}
		</script>
	</HEAD>
	<body onmousemove="moving()" oncontextmenu="ClearMoving()">
		<form id="Form1" method="post" runat="server">
			<table id="tableField" runat="server" border="0" align="center">
			</table>
			<table align="center">
				<tr>
					<td align="center">
						<%--<input type="button" name="btnExecute" value="保存" onclick="return btnConfirm()">--%>
						<asp:Button ID="btnExecute" Text="确定" Runat="server"></asp:Button></td>
					<td align="center">
						<input type="button" name="btnClose" value="关闭" onclick="btnCloseWindow()" runat="server"
							ID="Button1"></td>
				</tr>
			</table>
			<input type="hidden" runat="server" name="sortedFieldName" id="sortedFieldName">
			<marquee id="showTip" style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; BORDER-LEFT: blue 1px solid; COLOR: red; BORDER-BOTTOM: blue 1px solid; POSITION: absolute"
				width="150">请点击字段名！！！</marquee>
		</form>
	</body>
</HTML>
