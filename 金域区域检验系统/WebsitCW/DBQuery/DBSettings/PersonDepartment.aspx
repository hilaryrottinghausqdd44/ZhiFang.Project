<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DBSettings.PersonDepartment" Codebehind="PersonDepartment.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>机构部门设置</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			var node;
			function SelectDataSource()
			{
				if(node==null)
					window.returnValue = "0";
				else
					window.returnValue = node.getAttribute('NodeData');
				
				window.close();
			}
		</script>
		<script language="javascript" for="Treeview1" event="onclick">
			node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
	
			<?XML:NAMESPACE PREFIX=TVNS />
			<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
			<tvns:treeview id="Treeview1" treenodesrc="../../RBAC/Organizations/xml/DeptTree.xml" target="MainList" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" style="height:100%;width:154px;">
			</tvns:treeview>
			
		<input type="text" name="nodedata" id="nodedata" onpropertychange="javascript:expandedChanged()" style="width:0px;height:0px">
			
		<input type="button" name="btnConfirm" value="确定" onclick="SelectDataSource()">
	</body>
</HTML>
