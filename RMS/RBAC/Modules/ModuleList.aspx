<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.ModuleList" Codebehind="ModuleList.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>全部模块列表</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">
		<script language="javascript" for="TreeView1" event="onclick">
			//var parentNode = event.treeNode.getAttribute("NodeData");
			var node = TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
			
			if(node!=null)
				//parent.frames['right'].location="ModuleTest.aspx?ModuleID=" +node.getAttribute("NodeData");
				parent.frames['right'].location="Module.aspx?ModuleID=" +node.getAttribute("NodeData");
			//alert("Module.aspx?ModuleID=" +node.getAttribute("NodeData"));
		</script>
		<script language="javascript" for="TreeView1" event="onnodebound" type="text/javascript">
		   
			var node = TreeView1.getChildren();
			if(node!=null)
				parent.frames['right'].location="Module.aspx?ModuleID=" +node[0].getAttribute("NodeData");
			
			//alert("Module.aspx?ModuleID=" +node[0].getAttribute("NodeData"));
		</script>
		<script id=clientEventHandlersJS language=javascript>
		<!--

		function window_onload() {
			
				
		}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#e9e9e9" onload="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="Button1" style="Z-INDEX: 101" runat="server"
				Text="刷新"></asp:Button>
				<span style="FONT-SIZE: 10pt">全部模块列表</span>	
				
				
				<?XML:NAMESPACE PREFIX=TVNS />
								<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
								<tvns:treeview id="TreeView1" treenodesrc="<%=xd!=null?xd.DocumentElement.OuterXml.Replace("\"","\'").Replace("&amp;","&#38;") :"" %>" 
								target="MainList" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" 
								selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" 
								style="height:100%;width:190px;">
								</tvns:treeview>
		</form>
				

	</body>
</HTML>
