<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.FieldSort" Codebehind="FieldSort.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FieldSort</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
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
			
				document.Form1.all["executeSort"].src = "ExecuteSort.aspx?FieldName=" + fieldName + "&<%=Request.ServerVariables["Query_String"]%>";
			}
			
			function btnCloseWindow()
			{
				//window.returnValue = "";	
				window.close();
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<%--
			<table id="tableField" runat="server" border="1" align="center">
			</table>
			<table align="center">
				<tr>
					<td align="center"><input type="button" name="btnExecute" value="±£´æ" onclick="return btnConfirm()"></td>
					<td align="center"><input type="button" name="btnClose" value="¹Ø±Õ" onclick="btnCloseWindow()"></td>
				</tr>
			</table>
		--%>
			<iframe id="executeSort" src="ExecuteSort.aspx?<%=Request.ServerVariables["Query_String"]%>" width="100%" height="100%" scrolling="auto"></iframe>
			
		</form>
	</body>
</HTML>
