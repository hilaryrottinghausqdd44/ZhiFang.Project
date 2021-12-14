<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.PromptSelectDictionary" Codebehind="PromptSelectDictionary.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PromptSelectDictionary</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		<!--
			function SelectDataSource(obj)
			{
				window.returnValue=obj.innerText;
				window.close();
				
			}
			function MouseOverData(obj)
			{
				obj.style.backgroundColor = "#6699cc";
				obj.style.borderColor="red";
				obj.style.borderTopWidth=2;
				
			}
			function MouseLeave(obj)
			{
				obj.style.backgroundColor = "";
			}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" name="Form1">
			<table align="center" width="90%">
				<tr>
					<td align="center" colspan="1">
						选择数据源
					</td>
				</tr>
				<tr>
					<td align="center" colspan="1" width="100%">
						<asp:DataList id="dataListDataSource" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"
							BorderColor="#33CCFF" BorderWidth="1px" GridLines="Both" Width="100%">
							<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<span onclick="SelectDataSource(this)" onmouseover="MouseOverData(this)" onmouseout="MouseLeave(this)">
								
									<%# DataBinder.Eval(Container.DataItem, "Value")%>
								
								</span>
							</ItemTemplate>
						</asp:DataList>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
