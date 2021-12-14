<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Roles.AllModules" Codebehind="AllModules.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>角色权限</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft FrontPage 4.0" name="GENERATOR">
		<meta content="FrontPage.Editor.Document" name="ProgId">
		<style type="text/css">.unnamed1 { BORDER-RIGHT: #0099ff 1px dashed; BORDER-TOP: #0099ff 1px dashed; BORDER-LEFT: #0099ff 1px dashed; BORDER-BOTTOM: #0099ff 1px dashed }
	</style>
		<script language="javascript" event="onbuttonclick" for="Toolbar1">
			switch (event.srcNode.getAttribute('Id'))
			{
				case 'Employee':
					PostModules.txtRoleTypeName.value="请稍候..";
					PostModules.txtRoleType.value="1";
					if(ChooseEmpl());
						firSubmit();
					break;
				case 'Department':
					window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Position':
					window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
				case 'Post':
					window.open('personadd.aspx?Id=0','','width=580px,height=460px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );	
					break;
			}
		</script>
		
		<script language="javascript" event="onnodebound" for="TreeView1">
			checkTreeNodes1All(TreeView1.getChildren());
			//testchecked(TreeView2.getChildren())
			parent.document.getElementById('oToolTip').style.display = 'none';
			window.status = "读取权限完毕！";
		</script>
		
		<%--<script language="javascript" event="onexpand" for="TreeView1">
		    if (this.clickedNodeIndex != null){
		        var node=TreeView1.getTreeNode(this.clickedNodeIndex);
			    CheckNodesInheritStyle(node.getChildren());
			}
		</script>
		--%>
		<script language="javascript" event="onnodebound" for="TreeView2">
//			testchecked(TreeView2.getChildren());
//			window.setTimeout(RunInherit,10);
//			window.status="权限逻辑组织完成";
//			//alert(document.getElementById('oToolTip').style.display);
		</script>
		<script language="javascript" event="onclick" for="TreeView1">
		    var node=TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
			if(node==null)
				return;
			
			parent.PostModules.hModuleIDSelected.value=node.getAttribute("NodeData");
			
			strUrl="OperateButtons.aspx?txtRoleType=" + parent.PostModules.txtRoleType.value;
			strUrl= strUrl + "&txtRoleID=" + parent.PostModules.txtRoleID.value;
			strUrl= strUrl + "&txtRoleName=" + parent.PostModules.txtRoleName.value;
			strUrl= strUrl + "&ModuleID=" + node.getAttribute("NodeData");
						
			parent.window.frames['frmAccess'].location=strUrl;
		</script>	
		<%--<script language="javascript" event="oncheck" for="TreeView1">
		</script>--%>
	<script language="javascript">
        <!--

	    function checkTreeview() {
	        var node1 = TreeView1.getTreeNode(TreeView1.clickedNodeIndex);
	        if (node1 == null)
	            return;
	        var bNode1Checked = true;
	        bNode1Checked = node1.getAttribute("checked");

	        window.status = '';

	        var node1Text = node1.getAttribute("text");
	        if (node1Text.indexOf("<b>") < 0)
	            node1.setAttribute("checked", bNode1Checked);
	        else {
	            node1.setAttribute("checked", !bNode1Checked)
	            alert('继承权限无法去除');
	            return;
	        }

	        //return;
	       
	        walkChildren(node1.getChildren(), bNode1Checked);

	        if (bNode1Checked) {
	            walkParent(node1.getParent());
	        }
	        
	        //checkTreeNodesAllStatus(TreeView1.getChildren());
	    }
	    function ViewRoleM(txtRoleType1, txtRoleID1,obj) {
	        parent.PostModules.txtRoleTypeName.value = "关联权限.....";
	        parent.PostModules.txtRoleType.value = txtRoleType1;
	        parent.PostModules.txtRoleID.value = txtRoleID1;
	        parent.PostModules.txtRoleName.value = obj.innerHTML;
	        parent.firSubmit();
	    }
	    function fucCheckLength(strTemp) {
	        var i, sum;
	        sum = 0;
	        for (i = 0; i < strTemp.length; i++) {
	            if ((strTemp.charCodeAt(i) >= 0) && (strTemp.charCodeAt(i) <= 255))
	                sum = sum + 1;
	            else
	                sum = sum + 2;
	        }
	        return sum;
	    } 
	    function dd(str, c) {
	        str = str.replace(/([\u0391-\uffe5])/ig, '$1a').substring(0, c).replace(/([\u0391-\uffe5])a/ig, '$1');
	        return str;
	    }
		　    
	    function RunInherit() {
	        CheckNodesInheritStyle(TreeView1.getChildren());
	        parent.document.getElementById('oToolTip').style.display = 'none';
		}
	    function CheckNodesInheritStyle(treenodes) {
	        for (var i = 0; i < treenodes.length; i++) {
	            var currentChild = treenodes[i];
	            //treenodes[i].setAttribute("Expanded", true);
				
	            //currentChild.setAttribute("Expanded", true);
	           window.status = "||==>正在检查模块权限继承关系,请稍候...[" + treenodes[i].getAttribute("Text") +"]";
	           //if (!currentChild.getAttribute("Checked"))
	            //   continue;
                var WhiteSpaceChar = "....................";
                var lengthText = fucCheckLength(treenodes[i].getAttribute("Text"));
                if (lengthText <= 16)
                   WhiteSpaceChar = WhiteSpaceChar.substr(0, 16 - lengthText);
                else
                   WhiteSpaceChar = "";
                //<a href='#' onclick='alert();return false;'>
                var nodeData = treenodes[i].getAttribute("NodeData");
                var ModuleRolesNames = OA.RBAC.Roles.AllModules.getModuleRoles(nodeData,
                    '<%=Request.QueryString["txtRoleType"] %>', '<%=Request.QueryString["txtRoleID"] %>').value;

                var treeNodeNewText = treenodes[i].getAttribute("Text");
                if (ModuleRolesNames.indexOf("<b>") >= 0) {
                    currentChild.setAttribute("DEFAULTSTYLE", "Color:blue;");
                    treeNodeNewText = "<b>" + treeNodeNewText + "</b>";
                    currentChild.setAttribute("Checked", true);
                }
                    
                //ModuleRolesNames = "<fieldset><legend></legend>" + ModuleRolesNames + "</fieldset>";

                //ToolTip不起作用，不知道为什么，可能是web treeview的问题
                //treenodes[i].setAttribute("ToolTip", ModuleRolesNames);

                //if (ModuleRolesNames.length > 140)
                //  ModuleRolesNames = ModuleRolesNames.substr(0, 138) + "......";

                //ModuleRolesNames = "<font color=blue>" + ModuleRolesNames + "</font>";

                 //var treeNodeNewText ="<fieldset><legend>"+ treenodes[i].getAttribute("Text")
                //    +"</legend>" + ModuleRolesNames + "</fieldset>";

               treeNodeNewText = treeNodeNewText + WhiteSpaceChar + ">|   " + ModuleRolesNames;

               treenodes[i].setAttribute("Text", treeNodeNewText);
               if (currentChild.getChildren())
                   CheckNodesInheritStyle(currentChild.getChildren());
               //else
                 //currentChild.setAttribute("Expanded", false);
	        }
	    }
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

			    //alert(currentChild.getAttribute("Text") + "1:=" + currentChild.getAttribute("Checked"));
				
				currentChild.setAttribute("Checked",bChecked);
				
				//alert(currentChild.getAttribute("Text") + "2:=" + currentChild.getAttribute("Checked"));
				//window.status += currentChild.getAttribute("Text") + "=" + currentChild.getChildren().length + ".  ";
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
			if(PostModules.txtRoleType.value==""||PostModules.txtRoleType.value=="null")
			{
				alert('没有选择员工，部门，职位或岗位');
				return false;
			}	
			PostModules.hArrayModuleID.value="";
			PostModules.hArrayModuleIDRemoved.value="";
			collectModules(TreeView1.getChildren());
			
			//alert(PostModules.hArrayModuleID.value + "\n\n" + PostModules.hArrayModuleIDRemoved.value);
			return true;
			
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
			
			if(treenodes==null||typeof(treenodes)=="undefined")
				return;
			
			//alert(treenodes[0]);
			for(var i=0;i<treenodes.length;i++) {
			    
			    window.status = "||==>正在初始化本角色权限,请稍候..." + treenodes[i].getAttribute("Text");
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
				if(treenodes[i].getAttribute("NodeData")==treenode1.getAttribute("NodeData")) {
				    //treenodes[i].setAttribute("Expanded", true);
					treenodes[i].setAttribute("Checked",true); 
					//alert(treenodes[i].getAttribute("Checked"));
					return;
				}
				testcheckedpost(treenodes[i].getChildren(),treenode1);
			}
		}
		function checkTreeNodes1All(treenodes)
		{
			for(var i=0;i<treenodes.length;i++) {
			    //treenodes[i].setAttribute("Expanded", true);
			    //treenodes[i].setAttribute("CheckBox", "True");
			    //treenodes[i].setAttribute("Checked", true);
			    //treenodes[i].setAttribute("Expanded","True");
			    //window.status = treenodes[i].getAttribute("Text") + "-->选中";

			    if (!treenodes[i].getAttribute("Checked"))
			        continue;
			   
			    var treeNodeNewText = treenodes[i].getAttribute("Text");
			    var WhiteSpaceChar = "....................";
			    var lengthText = fucCheckLength(treeNodeNewText);
			    if (lengthText <= 16)
			        WhiteSpaceChar = WhiteSpaceChar.substr(0, 16 - lengthText);
			    else
			        WhiteSpaceChar = "";

			    var ModuleRolesNames = treenodes[i].getAttribute("RoleInheritNames");
			    
			    if (ModuleRolesNames != "") {
			        treenodes[i].setAttribute("DEFAULTSTYLE", "Color:blue;");
			        treeNodeNewText = "<b>" + treenodes[i].getAttribute("text") + "</b>";
			        //treenodes[i].setAttribute("Checked", true);
			    }
			    treeNodeNewText = treeNodeNewText + WhiteSpaceChar + ">|   " + ModuleRolesNames;

			    treenodes[i].setAttribute("Text", treeNodeNewText);
			    
				checkTreeNodes1All(treenodes[i].getChildren());
				//treenodes[i].setAttribute("Expanded", false);
				//treenodes[i].setAttribute("Checked", false);
//				if (ModuleRolesNames != "") {
//				    treenodes[i].setAttribute("Checked", true);
//				}
			    
			}
        }
        function checkTreeNodesAllStatus(treenodes) {
            for (var i = 0; i < treenodes.length; i++) {
                window.status += treenodes[i].getAttribute("text") + ":" + treenodes[i].getAttribute("Checked") + ",";
                checkTreeNodesAllStatus(treenodes[i].getChildren());
                //treenodes[i].setAttribute("Expanded", false);
            }
        }
	//-->
		</script>
		<script language="javascript" id="clientEventHandlersJS">
        <!--

		    function window_onload() {
                //if('<%=Request.ServerVariables["QUERY_STRING"] %>'!='')
                //    document.getElementById('oToolTip').style.display = '';

		        parent.window.frames['frmAccess'].location.href = "about:blank";
		        //parent.document.getElementById('oToolTip').style.display = 'none';
		        return true;
		    }

function ChooseEmpl()
{
	var result;
	result = window.showModalDialog('../Organizations/searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
	if (result != 'undefined' && typeof(result)!='undefined')
	{
		var rv = result.split("|");
		if (rv.length == 2)
		{	PostModules.txtRoleTypeName.value='按员工分配';
			PostModules.txtRoleName.value = rv[1];
			PostModules.txtRoleID.value = rv[0];
			return true;
		}
		else
		{
			return false;
		}
	}
	else
	{
		return false;
	}
}
//-->
		</script>
		<script language="javascript">
		    function RoleEdit(id) {
		        window.open('RoleEdit.aspx?id=' + id, 'RoleEdit', 'width=500,height=400,scrollbars=0,top=170,left=200');
		    }
		    
            </script>
        <script language="javascript" for="window" event="onbeforeunload">
            parent.document.getElementById('oToolTip').style.display = '';
        </script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="return window_onload()" bgcolor="#f0f0f0">
		<form id="PostModules" name="PostModules" onsubmit="return firSubmit()" method="post"
			runat="server">
			
			<P align="left">
				<asp:Label id="Label1" runat="server" Font-Size="Small" ForeColor="#FF8080"></asp:Label></P>
			
			<P>
				<?XML:NAMESPACE PREFIX=TVNS />
				<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
				<tvns:treeview id="TreeView1" <%if(xd!=null  && xd.DocumentElement!=null)
				{
					Response.Write(" treenodesrc=\"" + xd.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%> target="MainList" 
				oncheck="checkTreeview()" EnableViewState="False" 
				HelperID="__TreeView1_State__" 
				systemImagesPath="/webctrl_client/1_0/treeimages/" 
				selectExpands="false" 
				onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" 
				oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" 
				
				onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)">
				</tvns:treeview>
			</P>
			<P>
				<input id="hArrayModuleID" type="hidden" name="hArrayModuleID">
			</P>
			<P align="left">&nbsp;</P>
			<P>
				<input id="hArrayModuleIDRemoved" type="hidden" name="hArrayModuleIDRemoved">
			</P>
			<P align="left">
				<tvns:treeview id="TreeView2" <%if(xdRole!=null && xdRole.DocumentElement!=null) 
				{
					Response.Write(" treenodesrc=\"" + xdRole.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%> target="MainList" HelperID="__TreeView2_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" style="height:0px;width:0px;">
				</tvns:treeview>
			</P>	

		</form>
		
	</body>
</HTML>
