<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.Main.IndexLeft" Codebehind="IndexLeft.aspx.cs" %>
<%@ Import Namespace="System.Xml"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>功能模块</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../style.css">
		<script language="javascript" for="Treeview1" event="onclick">
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			locateHref(node);
		</script>
		<base target="MainList">
	<script id=clientEventHandlersJS language=javascript>
	<!--

	iSelected='-1';
	function swapImage(iNode,NodeData)
	{
		if(iNode!=iSelected)
		{
			var tdbg=document.all['tdbg'+iNode];
			if(tdbg!=null)
			{
				tdbg.background='../images/navigation/2.jpg';
				tdbg.className='navigation_black';
				tdbg.style.height=108;
				document.all['tdleftmargin'+iNode].style.width=2;
				ExpandNode(NodeData,true);
			}
		
		
		
			var tdbg1=document.all['tdbg'+iSelected];
			if(tdbg1!=null)
			{
				tdbg1.background='../images/navigation/1.jpg';
				tdbg1.className='navigation';
				tdbg1.style.height=103;
				document.all['tdleftmargin'+iSelected].style.width=7;
				ExpandNode(iSelected,false);
			}
		}
		iSelected=iNode;
	}
	
	function ExpandNode(NodeData,bExpand)
	{
		var node = Treeview1.getTreeNode(NodeData);
		if(node!=null)
		{
			node.setAttribute('Expanded',bExpand);
			if(bExpand)
			{
				Treeview1.setAttribute('selectedNodeIndex',NodeData);
				locateHref(node);
			}
		}
		
	}
	/*
	function ExpandChildren(node,'True')
	{
		while(node!=null)
		{
			node.setAttribute('Expanded','False');
			node=node.nextSibling;
		}
	}
	*/
	function window_onload() {
		return true;
	}

	function locateHref(node)
	{
		if(node!=null)
		{
			parent.frames['MainList'].location='../../RBAC/MODULES/ModuleRun.aspx?ModuleID=' +node.getAttribute('NodeData');
			//alert(node.getAttribute('NavigateUrl'));
		}
	}
	function ExpandNodeByName(treenodes) {
	    for(var i=0;i<treenodes.length;i++) {
//	        if (treenodes[i].getAttribute("text")=='实验室质量管理') {
//	            treenodes[i].setAttribute('Expanded', true);
//	            window.status = '[' + treenodes[i].getAttribute("text") + "] 展开了！ Expanded";
//	            continue;
	        //	        }
	        var paraAttr = treenodes[i].getAttribute("para");
	        if (paraAttr != null
	            && paraAttr != ''
	            && paraAttr.indexOf('NodeExpand=true') >= 0) {
	            treenodes[i].setAttribute('Expanded', true);
	            window.status = '[' + treenodes[i].getAttribute("text") + "] 展开了！ Expanded";
	            continue;
	        }
	    }
	}
	//-->
	</script>
	<script language="javascript" event="onnodebound" for="Treeview1">
		ExpandNodeByName(Treeview1.getChildren());
	</script>
	<script language="javascript" event="onexpand" for="Treeview1">
		//alert(Treeview1.style.width);
	</script>
	</HEAD>
	<body bottomMargin="0"  leftMargin="0" topMargin=0 rightMargin="0" ondblclick="location='IndexLeft.aspx?refresh=true'" language=javascript onload="return window_onload()">
		<table width="34" height="450" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td height="419" bgcolor="#e3e3e3" valign=top>
				<IMG id=ImgSwap width="2" height="16"  src="../images/tiao/short.jpg" style="DISPLAY: none; CURSOR: hand; POSITION: absolute" border="0" onclick="swapIMG()">
					<table id=tableModuleListLeft width="2"  border="0" cellpadding="0" cellspacing="0">
						
						
						<tr><td></td></tr>
						<!--tr>
							<td height="108"><table width="34" height="108" border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td width="2"></td>
										<td background="../images/navigation/2.jpg" class="navigation_black"><div align="center" class="navigation_black"><a href="#" class="navigation_black">系 
													统 设 置</a></div>
										</td>
									</tr>
								</table>
							</td>
						</tr-->
						
					</table>
				</td>
				<TD bgColor="#ffffff" vAlign="top" align="center">
					<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="154" border="0">
						<TR>
							<TD width="1" bgColor="#ffffff"><img width="1"></TD>
							<TD background="../images/tiao/expand.jpg" width="45%"></TD>
							<TD width="16"><IMG height="17" src="../images/tiao/arrow.jpg" width="16" border="0"  style="cursor:hand" onclick="swapIMG()"></TD>
							<TD background="../images/tiao/expand.jpg" width="45%">
							</TD>
							<TD width="3" background="../images/tiao/right.jpg">
							</TD>
							<td><img width="1"></td>
							
						</TR>
						<TR>
							<TD width="100%" colspan="6" bgcolor="#ffffff">
							<?XML:NAMESPACE PREFIX=TVNS />
								<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
								<tvns:treeview id="Treeview1" treenodesrc="../../RBAC/Modules/XML/<%="1" + "/" + UserID%>.xml" 
								target="MainList" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" 
								selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" 
								style="height:100%;">
								</tvns:treeview>
							</TD>
						</TR>
						
					</TABLE>
				</TD>
				<td width="1" bgcolor="#e3e3e3"><img width="1"></td>
			</tr>
			<tr>
			    <td colspan="3"  style="HEIGHT:1px" bgcolor="#e3e3e3"><img height="1"></td>
			</tr>
			
		</table>
		<input type=hidden id=hRefresh value="" onpropertychange="document.location.href='indexleft.aspx?refresh'">
	</body>
</HTML>

<script language=javascript>
<!--
function swapIMG()
{
	var frm=parent.fset;
	if(frm.cols.indexOf("209,")>-1)
	{
		frm.cols=frm.cols.replace("209,","1,");
		//tableModuleListLeft.style.display="none";
		ImgSwap.style.display="none";
		//Treeview1.style.display="none";
		var frm=parent.topFrame;
		if(frm.document.all("TopExpand"))
		    frm.document.all("TopExpand").style.display='';
		
	}
	else
	{
		frm.cols=frm.cols.replace("1,","209,");
		ImgSwap.style.display="none";
		tableModuleListLeft.style.display="";
		Treeview1.style.display="";
	}
}
//--
</script>
<%if(LeftDisplay!=null&&LeftDisplay.ToString().ToLower()=="no"){%>
<script language=javascript>
	swapIMG()
</script>
<%}%>
