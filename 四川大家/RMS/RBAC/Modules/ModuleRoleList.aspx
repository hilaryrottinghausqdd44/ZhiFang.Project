<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.ModuleRoleList" Codebehind="ModuleRoleList.aspx.cs" %>
<%Response.Expires=0;%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>testt</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">
		<script language="javascript" for="Treeview1" event="onselectedindexchange">
			//var parentNode = event.treeNode.getAttribute("NodeData");
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			//alert(Treeview1.clickedNodeIndex);
			parent.frames['right'].location="ModuleRoleNew.aspx?ModuleID=" +node.getAttribute("NodeData");
			
		</script>
	    <script language="javascript" for="Treeview1" event="onnodebound">
			//var parentNode = event.treeNode.getAttribute("NodeData");
			//alert(event.newTreeNodeIndex);
			var node = Treeview1.getTreeNode('0');
			parent.frames['right'].location="ModuleRoleNew.aspx?ModuleID=" +node.getAttribute("NodeData");
			
		</script>

        <script language="javascript" type="text/javascript">
// <!CDATA[

            function window_onload() {
                //
            }

// ]]>
        </script>
</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#e9e9e9" onload="return window_onload()">

		<span style="FONT-SIZE: 10pt">全部模块列表</span>
		<DIV align="left">
			<?XML:NAMESPACE PREFIX=TVNS />
			<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
			<tvns:treeview id="Treeview1"  <%if(xd!=null&&xd.DocumentElement!=null)
				{
					Response.Write(" treenodesrc=\"" + xd.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%>  target="right" selectedNodeIndex="0" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" 
			style="height:100%;width:150px;">
			</tvns:treeview>
		</DIV>
	</body>
</HTML>
