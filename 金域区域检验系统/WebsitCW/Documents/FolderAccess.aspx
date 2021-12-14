<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.Documents.FolderAccess" Codebehind="FolderAccess.aspx.cs" %>
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
			function checkColor(ModuleCheckIndex,CheckBoxIndex){
				var tdColor=document.all['tdd'];
				if(document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].checked)
				{
					//集连前面的权限
					for(var i=0;i<=CheckBoxIndex;i++)
					{
						if(document.all['checkbox_'+ModuleCheckIndex+'_'+ i].disabled!=true)
						{
							document.all['checkbox_'+ModuleCheckIndex+'_'+ i].checked=true;
							document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopStyle='none';
							document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopWidth=0;
							document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopColor='white';
						}
					}
				}
				else
				{
					for(var i=7;i>0;i--)
					{	if(document.all['checkbox_'+ModuleCheckIndex+'_'+ i].checked)
							break;
						else
						{
							if(document.all['checkbox_'+ModuleCheckIndex+'_'+ i].disabled!=true)
							{
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].checked=false;
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopStyle='none';
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopWidth=0;
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopColor='white';
							}
						}
					}
					if(CheckBoxIndex==0)
					{
						document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].checked=true;
						//取消全部权限
						for(var i=7;i>0;i--)
						{	
							if(document.all['checkbox_'+ModuleCheckIndex+'_'+ i].disabled!=true)
							{
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].checked=false;
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopStyle='none';
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopWidth=0;
								document.all['checkbox_'+ModuleCheckIndex+'_'+ i].style.borderTopColor='white';
							}
						}
						alert('这个权限不能取消!\n\n如果要取消，请从角色中去除');
					}	
					else
					{
							document.activeElement.style.borderTopStyle='dotted';
							document.activeElement.style.borderTopWidth=2;
							document.activeElement.style.borderTopColor='red';
					}
					var boolNextChecked=false;
					for(var i=CheckBoxIndex;i<8;i++)
					{
						if(document.all['checkbox_'+ModuleCheckIndex+'_'+ i].checked)
						{
							boolNextChecked=true;
						}
					}
					if(boolNextChecked&&CheckBoxIndex!=0)
					{
						if(document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].disabled!=true)
						{
							document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].style.borderTopStyle='solid';
							document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].style.borderTopWidth=2;
							document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].style.borderTopColor='red';
						}
					}
					else
					{
						if(document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].disabled!=true)
						{
							document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].style.borderTopStyle='none';
							document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].style.borderTopWidth=0;
							document.all['checkbox_'+ModuleCheckIndex+'_'+ CheckBoxIndex].style.borderTopColor='white';
						}
					}
						
				}
				//
					var item;
					item = window.document.createElement("OPTION");
					item.text = "";
					item.value = "";
					var sAccess="";
					for(i=0;i<8;i++)
					{
						if(document.all['checkbox_'+ModuleCheckIndex+'_'+ i].checked)
							sAccess="1" + sAccess;
						else
							sAccess="0" + sAccess;
					}
					item.text=ModuleCheckIndex;
					item.value = sAccess;
					var Exist=false;
					for(var i=0;i<PostModules.lstModules.options.length;i++)
					{
						var myItem=PostModules.lstModules.options[i]
						if (myItem.text==item.text)
						{							
							myItem.value=sAccess;
							Exist=true;
						}
					}
					if(!Exist)
						PostModules.lstModules.add(item);
			}
			function EditShare(RoleID,FolderID,txtRoleType,type,Name)
			{
			
				window.open("ShareConfig.aspx?RoleID="+RoleID+"&FolderID="+FolderID+"&txtRoleType="+txtRoleType+"&TypeName="+type+"&Name="+Name,'','width=520px,height=340px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-540)/2 );
			}
			function  DelShare(txtRoleType,RoleID,ModuleID,folder)
			{
				
				location.href="FolderAccess.aspx?txtRoleType="+txtRoleType+"&RoleID="+RoleID+"&ModuleID="+ModuleID+"&folder="+folder+"&del=yes";
			}
			
			//-->
		</script>
		<style type="text/css">.style2 { COLOR: #ff0000 }
	.style4 { FONT-SIZE: 12px }
	.style5 { COLOR: #006666 }
	</style>
	</HEAD>
	<body bgColor="#f0f0f0" leftMargin="0" topMargin="0">
		<form id="PostModules" name="PostModules" onsubmit="return firSubmit()" method="post"
			runat="server">
			<script language="javascript">
				function RoleEdit(id)
				{
					window.open ('RoleEdit.aspx?id=' + id,'RoleEdit','width=500,height=400,scrollbars=0,top=170,left=200');
				}
			</script>
			<P>
				<table class="style4" cellSpacing="1" cellPadding="0" width="400" bgColor="#009999" border="0">
					<tr bgColor="#d5d5d5">
						<td height="20" align="center">名称</td>
						<td width="100" height="20" align="center">分类</td>
						<td align="center" width="55" height="20">上传文件</td>
						<td align="center" width="55" height="20">创建目录</td>
						<td align="center" width="40" height="20">重命名</td>
						<td align="center" width="35" height="20">删除</td>
						<td align="center" width="40" height="20"></td>
					</tr>
					<%initOperates();%>
				</table>
			</P>
			<asp:label id="Label1" runat="server" Font-Size="Small" ForeColor="#FF8080"></asp:label>
			<select style="WIDTH: 0px; HEIGHT: 0px" size="0" name="lstModules">
			</select>
		</form>
	</body>
</HTML>
