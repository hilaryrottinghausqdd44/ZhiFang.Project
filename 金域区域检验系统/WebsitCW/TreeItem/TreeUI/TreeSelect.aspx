<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeSelect.aspx.cs" Inherits="TreeItem.TreeUI.TreeSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>树选择</title>    
    <base target="_self" />
    <script type="text/javascript">
    function ShowCheckbox() 
  {
      var TreeView = "<%=tvMenu.ClientID %>";
      var checkNode = document.getElementById("CheckedNode");
      var checkboxs = document.getElementById(TreeView).getElementsByTagName("INPUT");
    for(i=0;i<checkboxs.length;i++) {
//        if (checkboxs[i].checked) {
//            alert(checkboxs[i].type + "|" + checkboxs[i].name.substr(0, TreeView.length) + "&" + TreeView + "|" + checkboxs[i].checked + "|" + checkboxs[i].title);
//        }
      if(checkboxs[i].type == "checkbox"  
        &&  checkboxs[i].name.substr(0,TreeView.length) == TreeView 
        && checkboxs[i].checked ) 
      {
          checkNode.value += checkboxs[i].title + ",";
          //alert(checkNode.value);
      } 
    } 
  } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="All" 
     ImageSet="Custom" LeafNodeStyle-ImageUrl="~/Images/icons/0057_b.gif"
      ParentNodeStyle-ImageUrl="~/Images/icons/0019_b.gif"
       RootNodeStyle-ImageUrl="~/Images/icons/0041_b.gif" ShowLines=true Width="100%" ExpandDepth="0" Target="middle" >
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
        <input type="hidden" name="CheckedNode"  id="CheckedNode"/> 
        <asp:Button runat="server" ID="btnselect" OnClientClick="ShowCheckbox()" 
            Text="选择" onclick="btnselect_Click" />
    </div>
    </form>
</body>
</html>
