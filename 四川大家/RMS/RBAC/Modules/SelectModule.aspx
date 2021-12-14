<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.SelectModule" Codebehind="SelectModule.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>全部模块列表</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">
		<script language="javascript" event="onclick" for="Treeview1">
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			
			if(node!=null)
				{
					//node.setAttribute("NavigateUrl","");
					var fullPath='';
					var text=node.getAttribute("Text");
					var nodedata=node.getAttribute("NodeData");
					
					var objDiv=document.all["divModuleInfo"];
					objDiv.innerHTML="模块名称: " 
						+ text + "<p>地址:<u>../../RBAC/MODULES/ModuleRun.aspx?ModuleID=" 
						+ nodedata + "</u>"; 
					return false;
				}	
		</script>
		<script language=javascript>
		function ChooseThisModule()
		{
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			
			if(node!=null)
			{
				var text=node.getAttribute("Text");
				var nodedata=node.getAttribute("NodeData");
				var strReturn=text + "\v../../RBAC/MODULES/ModuleRun.aspx?ModuleID=" + nodedata;
				parent.window.returnValue=strReturn;
				parent.window.close();
			}
			else
				document.all["divModuleInfo"].innerHTML="<b>请选择一个模块</b>";
		}
		</script>

		
	</HEAD>
	<body topmargin=0 rightmargin=0 bottommargin=0 leftmargin=0>
			<div id="hover" style="BORDER-RIGHT: #ffffcc 1px double; BORDER-TOP: #ffffcc 1px double; DISPLAY: none; FONT-SIZE: 12pt; BORDER-LEFT: #ffffcc 1px double; COLOR: #ffffff; BORDER-BOTTOM: #ffffcc 1px double; POSITION: absolute; BACKGROUND-COLOR: #0000ff">aaa</div>
		<form id="Form1" method="post" runat="server">
		<table width=100%>
			
			<tr>
			<td colspan=2>
				<span style="FONT-SIZE: 12pt">全部模块列表</span><INPUT type="button" value="选定这个模块" onclick="ChooseThisModule();">&nbsp;
				<INPUT type="button" value="取消并关闭窗口" onclick="parent.window.close();"></asp:Button></td>
			</tr>
			<tr>
			<td width="10" >
	
						<?XML:NAMESPACE PREFIX=TVNS />
						<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
						<tvns:treeview id="Treeview1" <%if(xd!=null&&xd.DocumentElement!=null)
				{
					Response.Write(" treenodesrc=\"" + xd.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%> target="MainList" selectedNodeIndex="0" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" onclick="return click()" style="height:200%;width:224px;">
						</tvns:treeview>
				</td>
				<td valign=top width="99%" bgcolor="#cce4ff" bordercolor="#ff0099"><div id="divModuleInfo"></div></td>
			</tr>
		</table>
		<p><INPUT id="click" style="Z-INDEX: 102; LEFT: 0px; WIDTH: 0px; POSITION: absolute; TOP: 160px; HEIGHT: 0px"
								type="text" name="click">
								<INPUT id="click1" style="Z-INDEX: 103; LEFT: 0px; WIDTH: 0px; POSITION: absolute; TOP: 160px; HEIGHT: 0px"
								type="text" name="click1"> <INPUT id="click2" style="Z-INDEX: 104; LEFT: 0px; WIDTH: 0px; POSITION: absolute; TOP: 160px; HEIGHT: 0px"
								type="text" name="click2">&nbsp;</p>
		</form>
				

	</body>
</HTML>