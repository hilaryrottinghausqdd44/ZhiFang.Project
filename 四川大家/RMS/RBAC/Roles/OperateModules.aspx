<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.OperateModules" Codebehind="OperateModules.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>权限访问控制</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft FrontPage 4.0" name="GENERATOR">
		<meta content="FrontPage.Editor.Document" name="ProgId">
		<script language="javascript" event="onclick" for="TreeView1">
			var node=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
			if(node==null)
				return;
			
			
			strUrl="OperateButtons.aspx?txtRoleType=" + parent.PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + parent.PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + parent.PostModules.txtRoleName.value;
			strUrl= strUrl + "&ModuleID=" + node.getAttribute("NodeData");
			
						
			parent.window.frames['frmAccess'].location=strUrl;
			
			
			walkChildren(TreeView1.getChildren() ,false);
			node.setAttribute("Checked",true);
			
			window.status=node.getAttribute("Text");
			
			/*
			checkChain(node);
			*/
		</script>
		<script language="javascript" event="oncheck" for="TreeView1">
			var node1=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
			if(node1==null)
				return;
			
			if(node1.getAttribute("Checked"))
			{
				//walkChildren(node1.getChildren() ,true);
				//walkParent(node1.getParent());
			}
			else
			{
				//walkChildren(node1.getChildren() ,false);
			}
			
		</script>
		<script language="javascript">
		<!--
			function checkChain(myNode)
			{
				if(myNode.getAttribute("Checked"))
				{
					walkChildren(myNode.getChildren(),true);
					walkParent(myNode.getParent());
				}
				else
				{
					walkChildren(myNode.getChildren(),false);
				}
			}
			
			function walkChildren(arrayChildren,bChecked)
			{
				var currentChild;
				for (var i = 0; i < arrayChildren.length; i++)
				{
					currentChild = arrayChildren[i];
					currentChild.setAttribute("Checked",bChecked);
					
					walkChildren(currentChild.getChildren(), bChecked);
					
				}
			} 
			function walkParent(nodeParent)
			{
				if(nodeParent!=null&&typeof(nodeParent) != "undefined")
				{
					nodeParent.setAttribute("Checked",true);
					walkParent(nodeParent.getParent());
				}
			}
		//-->
		</script>
		<script language="javascript" id="clientEventHandlersJS">
			<!--

			function window_onload() {
				//testchecked(TreeView2.getChildren());
				//return true;
			}


			//-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="return window_onload()" bgcolor="#f0f0f0">
		<form id="PostModules" name="PostModules" onsubmit="return firSubmit()" method="post"
			runat="server">
			<script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
			</script>
			<P align="left">
				<asp:Label id="Label1" runat="server" Font-Size="Small" ForeColor="#FF8080"></asp:Label></P>
			<P>
			<?XML:NAMESPACE PREFIX=TVNS />
			<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
			<tvns:treeview id="TreeView1" <%if(xd!=null&&xd.DocumentElement!=null)
			{
				Response.Write(" treenodesrc=\"" + xd.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
			}
			%> target="MainList" HelperID="__TreeView1_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)">
			</tvns:treeview>
			</P>
		</form>
	</body>
</HTML>
