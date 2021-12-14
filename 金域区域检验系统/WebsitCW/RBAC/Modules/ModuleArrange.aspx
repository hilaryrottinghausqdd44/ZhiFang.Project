<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Modules.ModuleArrange" Codebehind="ModuleArrange.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>全部模块列表</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">
		<script language="javascript" event="onclick" for="Treeview1">
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			
			if(node!=null)
				{
					node.setAttribute("NavigateUrl","");
					var fullPath='';
					var text=node.getAttribute("Text");
					
					node_data = node;         //获取NodeData的节点
					node_sn = node;           //获取sn的节点
					
					while(node_sn!=null)
					{
						nd=node_sn.getAttribute("NodeData");
						fullPath="treenode[@NodeData=\'" +nd + "\']/" + fullPath;
						node_sn=node_sn.getParent();
						
					}
					nodedata=node_data.getAttribute("NodeData");
					if(Form1.click.value.length==0)
					{	
						Form1.click.sn=fullPath.substr(0,fullPath.length-1);
						Form1.click1.sn=fullPath.substr(0,fullPath.length-1);
						Form1.click.value= nodedata;
						Form1.click1.value=nodedata;
						beginDrag(text);
						
						//alert(Form1.click.value);
						
						
					}
					else
					{
						
						Form1.click2.value=nodedata;
						Form1.click2.sn=fullPath.substr(0,fullPath.length-1);
						
						//alert(Form1.click2.value);
						
						if(Form1.click2.sn.substr(0,Form1.click1.sn.length)==Form1.click1.sn)
						alert('不能选择子节点或自己，请再选择');
						else
						Form1.submit();
						
						
						
					}
					return false;
				}	
		</script>

		<script language="javascript" id="clientEventHandlersJS">
			
			var status=0;
			var topY=150;
			var leftX=220;

			
			function beginDrag(txt)
			{
				//hover.style.top=event.screenY-topY;
				//hover.style.left=event.screenX-leftX;
				hover.style.top=document.body.scrollTop+event.clientY -150;
				hover.style.left=document.body.scrollLeft+event.clientX;
				hover.innerHTML=txt;
				hover.style.display="";
				status=1;
				
			}		
				
			function move()
			{
			if(status=1)			
			{
			//	hover.style.top=event.screenY-topY;
			//	hover.style.left=event.screenX-leftX;
				hover.style.top=document.body.scrollTop+event.clientY + 10;
				hover.style.left=document.body.scrollLeft+event.clientX;
			}
			}
			function ClearMoving()
			{
				status=0;
				Form1.click.value="";
				Form1.click1.value="";
				//Form1.click2.value="";
				//node=null;
				hover.style.display="none";
				
			}
						
				
		</script>
	</HEAD>
	<body onmousemove="move()" topmargin=0 rightmargin=0 bottommargin=0 leftmargin=0 oncontextmenu="ClearMoving();return false;">
			<div id="hover" style="BORDER-RIGHT: #ffffcc 1px double; BORDER-TOP: #ffffcc 1px double; DISPLAY: none; FONT-SIZE: 12pt; BORDER-LEFT: #ffffcc 1px double; COLOR: #ffffff; BORDER-BOTTOM: #ffffcc 1px double; POSITION: absolute; BACKGROUND-COLOR: #0000ff">aaa</div>
		<form id="Form1" method="post" runat="server">
		<table width=100%>
			<tr>
			<td>	<asp:radiobuttonlist id="RadioButtonList1" runat="server"  Font-Size="13px">
							<asp:ListItem id="op1" Value="节点前" Selected="True" runat="server"></asp:ListItem>
							<asp:ListItem id="op2" Value="子节点" Selected="False" runat="server"></asp:ListItem>
						</asp:radiobuttonlist></td><p>说明：在线文档节点，只可以做为根节点出现；共享文档、公共文档只可以做为二级节点出现</p>
						
			</tr>
			<tr>
			<td>			<asp:Button id="Button1" style="Z-INDEX: 101" runat="server"
				Text="刷新"></asp:Button>
				<span style="FONT-SIZE: 10pt">全部模块列表</span></td>
			</tr>
			<tr>
			<td>
	
						<?XML:NAMESPACE PREFIX=TVNS />
						<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
						<tvns:treeview id="Treeview1" <%if(xd!=null&&xd.DocumentElement!=null)
				{
					Response.Write(" treenodesrc=\"" + xd.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%> target="MainList" selectedNodeIndex="0" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" onclick="return click()" style="height:200%;width:224px;">
						</tvns:treeview>
				</td>
			</tr>
		</table>
		<p><INPUT id="click" style="Z-INDEX: 102; LEFT: 0px; WIDTH: 0px; POSITION: absolute; TOP: 160px; HEIGHT: 0px"
								type="text" name="click" sn="">
								<INPUT id="click1" style="Z-INDEX: 103; LEFT: 0px; WIDTH: 0px; POSITION: absolute; TOP: 160px; HEIGHT: 0px"
								type="text" name="click1" sn=""> <INPUT id="click2" style="Z-INDEX: 104; LEFT: 0px; WIDTH: 0px; POSITION: absolute; TOP: 160px; HEIGHT: 0px"
								type="text" name="click2" sn="">&nbsp;</p>
		</form>
				

	</body>
</HTML>
