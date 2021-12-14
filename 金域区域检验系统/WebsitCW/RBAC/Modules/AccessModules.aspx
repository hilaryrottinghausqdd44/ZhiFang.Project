<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.AccessModuls" Codebehind="AccessModules.aspx.cs" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>权限访问控制</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft FrontPage 4.0" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">

    <script language="javascript" event="onclick" for="TreeView1">
			var node=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
			if(node==null)
				return;
			
			
			strUrl="AccessConfig.aspx?txtRoleType=" + parent.PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + parent.PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + parent.PostModules.txtRoleName.value;
			strUrl= strUrl + "&ModuleID=" + node.getAttribute("NodeData");
			
			if(!node.getAttribute("Checked"))
			{
				parent.window.frames['frmAccess'].location=strUrl;
			}
			
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
				walkChildren(node1.getChildren() ,true);
				walkParent(node1.getParent());
			}
			else
			{
				walkChildren(node1.getChildren() ,false);
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

</head>
<body leftmargin="0" topmargin="0" onload="return window_onload()" bgcolor="#f0f0f0">
    <form id="PostModules" name="PostModules" onsubmit="return firSubmit()" method="post"
    runat="server">

    <script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
    </script>

    <p align="left">
        <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF8080"></asp:Label></p>
    <p>
        <iewc:TreeView ID="TreeView1" runat="server" BackColor="#F0F0F0"></iewc:TreeView></p>
    </form>
</body>
</html>
