<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.PromptSelectFixList" Codebehind="PromptSelectFixList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PromptSelectFixList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		<!--
			function btnConfirm()
			{
				var inputData = document.Form1.all["dataArea"].value;
				
				window.returnValue = TransformData(inputData);
				window.close();
			}
			function btnCancel()
			{
				window.close();
			}
			
			function TransformData(data)
			{
				var retString = "";
				var newData = data.split("\n");
				for(var i=0; i<newData.length; i++)
				{
					newData[i] = trim_string(newData[i]);
					retString += ":" + newData[i];
				}
				if(retString.length >0)
				{
					retString = retString.substr(1);
				}
				return  trim_string(retString);
			}
			
			function trim_string(strOriginalValue) 
			{
				var ichar, icount;
				var strValue = strOriginalValue;
				ichar = strValue.length - 1;
				icount = -1;
				while (strValue.charAt(ichar)==' ' && ichar > icount)
					--ichar;
				if (ichar!=(strValue.length-1))
					strValue = strValue.slice(0,ichar+1);
				ichar = 0;
				icount = strValue.length - 1;
				while (strValue.charAt(ichar)==' ' && ichar < icount)
					++ichar;
				if (ichar!=0)
					strValue = strValue.slice(ichar,strValue.length);
				return strValue;
			}
			
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center" width="400">
				<tr>
					<td align="center" colspan="2" width="100%">输入数据源项</td>
				</tr>
				<tr>
					<td colspan="2" width="100%" align="center">
						<textarea name="dataArea" rows="10" cols="30"></textarea>
					</td>
				</tr>
				<tr>
					<td align="center"><input type="button" value="确定" onclick="btnConfirm()"></td>
					<td align="center"><input type="button" value="取消" onclick="btnCancel()"></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
			var argument = window.dialogArguments;
			alert(argument);
			var index = 0;
			while(argument.indexOf(":", index) != -1)
			{
				var i = argument.indexOf(":", index);
				argument = argument.replace(":", "\n");
				index = i;
			}
			
			document.Form1.all["dataArea"].value = argument;
	
		</script>
	</body>
</HTML>
