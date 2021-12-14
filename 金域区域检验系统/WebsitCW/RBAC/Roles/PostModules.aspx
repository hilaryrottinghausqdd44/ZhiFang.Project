<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.PostModule" Codebehind="PostModules.aspx.cs" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PersonList</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft FrontPage 4.0" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">
    <style type="text/css">
        .unnamed1
        {
            border-right: #0099ff 1px dashed;
            border-top: #0099ff 1px dashed;
            border-left: #0099ff 1px dashed;
            border-bottom: #0099ff 1px dashed;
        }
    </style>

    <script language="javascript" event="onclick" for="TreeView1">
		var node=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
		if(node.getAttribute("Checked"))
		{
			//alert(typeof(node.getAttribute("Checked")) + "=1" + node.getAttribute("Checked"));
			node.setAttribute("Checked",false);
		}
		else
		{
			//alert(typeof(node.getAttribute("Checked")) + "=2" + node.getAttribute("Checked"));
			node.setAttribute("Checked",true);
		}
		
		checkChain(node);
		window.status=node.getAttribute("Text");
		//alert(node.getAttribute("Checked")+"--clicked");
    </script>

    <script language="javascript" event="oncheck" for="TreeView1">
		//if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)
		var node1=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
		
		
		//alert(typeof(node1.getAttribute("Checked")) +" = " + node1.getAttribute("Checked"));
		
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
		
		function firSubmit()
		{
			PostModules.hArrayModuleID.value="";
			PostModules.hArrayModuleIDRemoved.value="";
			collectModules(TreeView1.getChildren());
			
			//alert(PostModules.hArrayModuleID.value + "\n\n" + PostModules.hArrayModuleIDRemoved.value);
			
			//return false;
			
		}
		function collectModules(TreeNodes)
		{
			if(TreeNodes!=null&&typeof(TreeNodes) != "undefined")
			{
				for(var i=0;i<	TreeNodes.length;i++)
				{
					var currentChild;
					currentChild = TreeNodes[i];
					if(currentChild.getAttribute("Checked"))
						PostModules.hArrayModuleID.value +="," + currentChild.getAttribute("NodeData");
					else
						PostModules.hArrayModuleIDRemoved.value +="," + currentChild.getAttribute("NodeData");
					
					var childNodes;
					childNodes=currentChild.getChildren();
					collectModules(childNodes);
				}
			}
				//添加的模块
				if(PostModules.hArrayModuleID.value.substring(0,1)==",")
					PostModules.hArrayModuleID.value=PostModules.hArrayModuleID.value.substring(1,PostModules.hArrayModuleID.value.length);
					
				//删除的模块
				if(PostModules.hArrayModuleIDRemoved.value.substring(0,1)==",")
					PostModules.hArrayModuleIDRemoved.value=PostModules.hArrayModuleIDRemoved.value.substring(1,PostModules.hArrayModuleIDRemoved.value.length);
		}
		
		function testchecked(treenodes)
		{
			for(var i=0;i<treenodes.length;i++)
			{
				testcheckedpost(TreeView1.getChildren(),treenodes[i]);
				
				testchecked(treenodes[i].getChildren());
			}
		}
		function testcheckedpost(treenodes,treenode1)
		{
			if(treenodes==null||typeof(treenodes)=="undefined")
				return;
			for(var i=0;i<treenodes.length;i++)
			{
				if(treenodes[i].getAttribute("NodeData")==treenode1.getAttribute("NodeData"))
				{
					treenodes[i].setAttribute("Checked",true); 
					return;
				}
				testcheckedpost(treenodes[i].getChildren(),treenode1);
			}
		}
		
	//-->
    </script>

    <script id="clientEventHandlersJS" language="javascript">
<!--

function window_onload() {
	testchecked(TreeView2.getChildren());
}

//-->
    </script>

</head>
<body onload="return window_onload()" topmargin="0" leftmartgin="0">
    <form id="PostModules" name="PostModules" onsubmit="firSubmit()" method="post" runat="server">
    <table height="462" width="490" bgcolor="#ffffff">
        <tr bgcolor="lightgrey" heigth="40">
            <td colspan="3" height="38">
                <b><font color="red">指定岗位使用功能</font></b>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f0f0f0" colspan="3" height="17">
                <p align="left">
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" bgcolor="#f0f0f0" colspan="2">
                <table width="178" border="0">
                    <tr bgcolor="#ffffff">
                        <td valign="middle" align="right" width="61" bgcolor="#f0f0f0" height="1%">
                            <p align="right">
                                岗位&nbsp;</p>
                        </td>
                        <td valign="middle" align="center" width="107" bgcolor="#f0f0f0" height="40">
                            <p align="left">
                                <asp:DropDownList ID="postlist" runat="server" AutoPostBack="True" DataValueField="Id"
                                    DataTextField="CName" Width="88px" OnSelectedIndexChanged="postlist_SelectedIndexChanged">
                                </asp:DropDownList>
                            </p>
                        </td>
                    </tr>
                    <tr bgcolor="#ffffff">
                        <td valign="middle" align="right" width="61" bgcolor="#f0f0f0" height="34">
                            <font size="2">岗位描述</font>
                        </td>
                        <td class="unnamed1" valign="top" align="center" bgcolor="#f0f0f0" height="34">
                            <p align="left">
                                <font size="2">
                                    <asp:Label ID="postDescr" runat="server"></asp:Label></font></p>
                        </td>
                    </tr>
                </table>
                <p align="center">
                    <font size="2">&nbsp;
                        <asp:Button ID="buttSave" runat="server" Text="保 存" OnClick="buttSave_Click"></asp:Button>&nbsp;
                        <asp:Button ID="reset" runat="server" Text="复位" OnClick="reset_Click"></asp:Button></font></p>
                <p align="center">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></p>
                <p align="center">
                    <iewc:TreeView ID="TreeView2" runat="server" Width="0px" Height="0px"></iewc:TreeView></p>
            </td>
            <td valign="top" align="left" width="253" bgcolor="#f0f0f0" height="100%">
                <iewc:TreeView ID="TreeView1" runat="server"></iewc:TreeView>
            </td>
        </tr>
        <tr bgcolor="white" height="30">
            <td align="right" bgcolor="#f0f0f0" colspan="3" height="46">
                <p align="left">
                </p>
            </td>
        </tr>
    </table>

    <script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
    </script>

    <input id="hArrayModuleID" name="hArrayModuleID" type="hidden">
    <input id="hArrayModuleIDRemoved" name="hArrayModuleIDRemoved" type="hidden">
    </form>
</body>
</html>
