<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" Codebehind="testt.aspx.cs" AutoEventWireup="false" Inherits="OA.RBAC.Modules.testt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>testt</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" for="TreeView1" event="onselectedindexchange1">
	         var myNode=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
			if(myNode!=null)
			    alert(myNode.getAttribute("Checked"));
	        return false;
		</script>
		
		<script language="javascript" for="TreeView1" event="onclick">
	       var node=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
			if(node!=null)
			    alert(node.getAttribute("Checked"));
		</script>
	</HEAD>
	<body ms_positioning="GridLayout">
		<form id="frmContent" name="frmContent" method="post" encType="multipart/form-data" runat="server">
			<iewc:TreeView id="TreeView1" EnableViewState="false" runat="server" Width="136px" Height="64px" style="Z-INDEX: 101; LEFT: 168px; POSITION: absolute; TOP: 56px">
				<iewc:TreeNode Checked="True" Text="asdf"  CheckBox="True" Expanded="true">
					<iewc:TreeNode  CheckBox="True" Checked="False" Text="Node1" Type="checkbox1" ChildType="checkbox1"></iewc:TreeNode>
					<iewc:TreeNode Checked="True"  CheckBox="True" Text="Node2"></iewc:TreeNode>
				</iewc:TreeNode>
			</iewc:TreeView>
			<asp:Button id="ButtonTest" style="Z-INDEX: 102; LEFT: 176px; POSITION: absolute; TOP: 336px"
				runat="server" Text="测试"></asp:Button>
			<asp:TextBox id="TextBoxReturn" style="Z-INDEX: 103; LEFT: 176px; POSITION: absolute; TOP: 160px"
				runat="server" Height="168px" Width="336px" TextMode="MultiLine"></asp:TextBox>
			<asp:TextBox id="TextBoxModuleID" style="Z-INDEX: 104; LEFT: 176px; POSITION: absolute; TOP: 128px"
				runat="server" Width="328px">1</asp:TextBox>
			<asp:Label id="Label1" style="Z-INDEX: 105; LEFT: 120px; POSITION: absolute; TOP: 128px" runat="server">模块ID</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 106; LEFT: 120px; POSITION: absolute; TOP: 160px" runat="server">返回值</asp:Label>
		</form>
	</body>
</HTML>
