<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.DataSelectValue" Codebehind="DataSelectValue.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DataSelectValue</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<table width="400" align="center">
				<tr>
					<td align="center" width="100%" colSpan="2"><asp:textbox id="selectValue1" runat="server" Width="18px"  Visible="False"></asp:textbox>选择字段数值
						<asp:textbox id="selectValue" runat="server" Width="22px" Visible="False"></asp:textbox></td>
				</tr>
				<tr>
					<td align="center" width="100%" colSpan="2"><!--<textarea name="dataArea" rows="10" cols="30"></textarea>--><asp:textbox id="dataArea" runat="server" Width="304px" TextMode="MultiLine" Height="144px"></asp:textbox></td>
				</tr>
				<tr>
					<td align="center"><input onclick="btnConfirm()" type="button" value="确定"></td>
					<td align="center"><input onclick="btnCancel()" type="button" value="取消"></td>
				</tr>
			</table>
		</form>
		
		
	</body>
</HTML>
