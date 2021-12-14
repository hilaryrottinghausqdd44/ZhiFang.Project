<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.PermissionTreeView" Codebehind="PermissionTreeView.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PermissionTreeView</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" for="Treeview1" event="onclick">
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			locateHref(node);
		</script>
		<script language="javascript">
			function locateHref(node)
			{
				if(node!=null)
				{
				
					window.frames['permissionContent'].location='ButtonAuth.aspx';
				}
				
			}
		</script>
		<script language="javascript">
			function TableSettingClick()
			{
				window.open("TableSetFramework.aspx"+window.location.search, "mainContent");
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div>
				<table width="100%">
					<tr>
						<td valign="top"><input type="button" value="±íÈ¨ÏÞÅäÖÃ" onclick="TableSettingClick()" ></td>
					</tr>
					<tr>
						<td width="120" valign="top">
							<div>
								<%--Response.Write(Server.HtmlEncode(xmlTree.OuterXml));--%>
								<?XML:NAMESPACE PREFIX=TVNS />
								<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
				<tvns:treeview id="TreeView1" <%if(xmlTree!=null)
				{
					Response.Write(" treenodesrc=\"" + xmlTree.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%> HelperID="__TreeView1_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" target="mainContent" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)">
				</tvns:treeview>
							</div>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
