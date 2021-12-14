<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.FilesTree" Codebehind="FilesTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>FilesTree</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" for="Treeview1" event="onclick">
			var dirz='';
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			//parent.frames['right'].location="ModuleRole.aspx?ModuleID=" +node.getAttribute("NodeData");
			window.parent.document.all['ShowName'].value=node.getAttribute("text");
			parent.document.all['ShowId'].value=node.getAttribute("filePath");
			parent.document.all['ParentId'].value=node.getAttribute("ParentId");
			parent.document.all['Para'].value='Folder=\\'+node.getAttribute("filePath");
			parent.document.all['tooltip'].value=node.getAttribute("tooltip");
			parent.document.all['ImageUrl'].value=node.getAttribute("ImageUrl");
			parent.document.all['DataFrom'].value="Drive";
			dirz=node.getAttribute("text");
			getDir(node);
			document.all['FileDir1'].value=dirz;
			parent.document.all['ShowName'].value=parent.document.all['FileDir'].value+'/'+document.all['FileDir1'].value;
			function getDir(myNode)
			{	
				if(myNode.getParent()!=null)
				{
					dirz=myNode.getParent().getAttribute("Text")+'/'+dirz
					myNode=myNode.getParent();
					getDir(myNode);
				}
			}	
		</script>
  </head>
  <body MS_POSITIONING="GridLayout">
    <DIV align="left">
			<?XML:NAMESPACE PREFIX=TVNS />
			<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
			<tvns:treeview id="Treeview1" <%if(Xd!=null&&Xd.DocumentElement!=null)
				{
					Response.Write(" treenodesrc=\"" + Xd.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%> target="right" selectedNodeIndex="0" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" 
			style="height:100%;width:150px;">
			</tvns:treeview>
	</div>
	<form id="Form1" method="post" runat="server">
	<input type=text id=FileDir1 name=FileDir1 style="WIDTH:0">
     </form>
	
  </body>
</html>
