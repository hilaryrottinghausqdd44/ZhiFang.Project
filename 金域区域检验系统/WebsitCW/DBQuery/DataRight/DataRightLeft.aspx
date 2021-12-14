<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.DataRightLeft" Codebehind="DataRightLeft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DataRightLeft</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
			#divBind { BORDER-RIGHT: #000080 1px solid; BORDER-TOP: #000080 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #000080 1px solid; BORDER-BOTTOM: #000080 1px solid }
			#divFieldAuth { BORDER-RIGHT: #000080 1px solid; BORDER-TOP: #000080 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #000080 1px solid; BORDER-BOTTOM: #000080 1px solid }
			#divDataCondition { BORDER-RIGHT: #000080 1px solid; BORDER-TOP: #000080 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #000080 1px solid; BORDER-BOTTOM: #000080 1px solid }
		</style>
		<script type="text/javascript">
			var lastSelectObj = null;
			var originBackColor = null;
			var clickedColor = "#00cccc";
			//======左边导航===============
			function RightNavigator(obj, tableName)
			{
			
				if(lastSelectObj == null)
				{
					obj.style.backgroundColor = clickedColor;
				}
				else if(lastSelectObj != obj)
				{
					lastSelectObj.style.backgroundColor = "";//originBackColor;
					obj.style.backgroundColor = clickedColor;
				}
				lastSelectObj = obj;
				//alert(obj.TableEName);英文名称
				window.parent.frames["Content"].document.location.href = "SourceOpAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + tableName + "&TableEName=" + obj.TableEName;
				//alert(arg1);
			}
			//=============End=============
			
			//=============字段授权导航=========
			function FieldAuthNav(obj, tableName)
			{
				if(lastSelectObj == null)
				{
					obj.style.backgroundColor = clickedColor;
				}
				else if(lastSelectObj != obj)
				{
					lastSelectObj.style.backgroundColor = "";//originBackColor;
					obj.style.backgroundColor = clickedColor;
				}
				lastSelectObj = obj;
				//alert(obj.TableEName);英文名称
				window.parent.frames["Content"].document.location.href = "SourceFieldAuth.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + tableName + "&TableEName=" + obj.TableEName;
			}
			//===============End================
			
			//==================数据条件授权=============
			function DataConditionAuth(obj, tableName)
			{
				if(lastSelectObj == null)
				{
					obj.style.backgroundColor = clickedColor;
				}
				else if(lastSelectObj != obj)
				{
					lastSelectObj.style.backgroundColor = "";//originBackColor;
					obj.style.backgroundColor = clickedColor;
				}
				lastSelectObj = obj;
				window.parent.frames["Content"].document.location.href = "DataRightMain.aspx?<%=Request.ServerVariables["Query_String"]%>&TableName=" + tableName + "&TableEName=" + obj.TableEName;
			}
			//====================End====================
			
			//==============鼠标事件===============
			
			function MouseOnSpan(obj)
			{
				
				//originBackColor = obj.style.backgroundColor;
				//obj.style.backgroundColor = "#99cccc";
				originBackColor = obj.style.color;
				obj.style.color = "cornflowerblue";
			}
			
			function MouseOutSpan(obj)
			{
				if(originBackColor != null)
				{
					//obj.style.backgroundColor = originBackColor;
					obj.style.color = originBackColor;
				}
				else
				{
					//obj.style.backgroundColor = "";
					obj.style.color = "";
				}
				originBackColor = null;
			}
			//====================End==============
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="divBind">
				<span style="FONT-WEIGHT: bold">资源操作权限配置</span><br>
				<asp:Repeater ID="rptNavigator" Runat="server">
					<ItemTemplate>
						<asp:PlaceHolder ID="plhImage" Runat="server">
							<img src="../Images/Table_Ico.gif" border="0" width="15px" height="15px">
						</asp:PlaceHolder>
						<%--<a id="anchorMain" runat="server"></a>--%>
						<span id="spanNav" runat="server" style="Cursor:pointer; Cursor:hand; text-decoration:underline; COLOR:DarkBlue"
							onmouseover="MouseOnSpan(this)" onmouseout="MouseOutSpan(this)"></span>
					</ItemTemplate>
					<SeparatorTemplate>
						<br>
					</SeparatorTemplate>
				</asp:Repeater>
			</div>
			<br>
			<div id="divFieldAuth">
				<span style="FONT-WEIGHT:bold">表字段授权</span><br>
				<asp:Repeater ID="rptField" Runat="server">
					<ItemTemplate>
						<asp:PlaceHolder ID="plhFieldAuth" Runat="server">
							<img src="../Images/Table_Ico.gif" border="0" width="15px" height="15px">
						</asp:PlaceHolder>
						<span id="Span1" runat="server" style="Cursor:pointer; Cursor:hand; text-decoration:underline; COLOR:DarkBlue"
							onmouseover="MouseOnSpan(this)" onmouseout="MouseOutSpan(this)"></span>
					</ItemTemplate>
					<SeparatorTemplate>
						<br>
					</SeparatorTemplate>
				</asp:Repeater>
			</div>
			<br>
			<div id="divDataCondition">
				<span style="FONT-WEIGHT:bold">数据过滤条件</span><br>
				<asp:Repeater ID="rptDataCondition" Runat="server">
					<ItemTemplate>
						<asp:PlaceHolder ID="plhCondition" Runat="server">
							<img src="../Images/Table_Ico.gif" border="0" width="15px" height="15px">
						</asp:PlaceHolder>
						<span id="Span2" runat="server" style="Cursor:pointer; Cursor:hand; text-decoration:underline; COLOR:DarkBlue"
							onmouseover="MouseOnSpan(this)" onmouseout="MouseOutSpan(this)"></span>
					</ItemTemplate>
					<SeparatorTemplate>
						<br>
					</SeparatorTemplate>
				</asp:Repeater>
			</div>
		</form>
	</body>
</HTML>
