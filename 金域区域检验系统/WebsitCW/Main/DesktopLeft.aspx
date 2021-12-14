<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesktopLeft.aspx.cs" Inherits="OA.Main.DesktopLeft" %>

<%@ Import Namespace="System.Xml"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<title>功能模块</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="MainList">
	<HTML>
	<HEAD>
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
				tdbg.background='../Images/Navigation/2.jpg';
				tdbg.className='navigation_black';
				tdbg.style.height=108;
				document.all['tdleftmargin'+iNode].style.width=2;
				ExpandNode(NodeData,true);
			}
		
		
		
			var tdbg1=document.all['tdbg'+iSelected];
			if(tdbg1!=null)
			{
				tdbg1.background='../Images/Navigation/1.jpg';
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
			parent.frames['content'].location='./DesktopContent.aspx?ModuleID=' +node.getAttribute('NodeData');
		}
		
	}
	//-->
	</script>
	</HEAD>
	<body bottomMargin="0"  leftMargin="0" topMargin=0 rightMargin="0" ondblclick="location='DesktopLeft.aspx?refresh=true'" language="javascript" onload="return window_onload()">
		<table width="34" height="650" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td height="419" bgcolor="#e3e3e3" valign=top>
				<IMG id=ImgSwap width="24" height="16"  src="../Images/Tree/short.jpg" style="DISPLAY: none; CURSOR: hand; POSITION: absolute" border="0" onclick="swapIMG()">
					<table id=tableModuleListLeft width="34" height="419" border="0" cellpadding="0" cellspacing="0">
						
						<%
						int i=0;
						foreach(XmlNode myNode in ModuleList)
						{%>
						<tr>
							<td id="tdheight<%=i%>" onclick="swapImage('<%=i%>','<%=i%>')" height="103" valign="top">
								<table width="34" height="100%" border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td width="7" id="tdleftmargin<%=i%>">&nbsp;</td>
										<td id="tdbg<%=i%>" width="27" height="103" background="../Images/Navigation/1.jpg" class="navigation" align=center valign="middle">
										<div id="div<%=i%>" align="center" style="width:10;cursor:hand"><%=myNode.Attributes.GetNamedItem("text").InnerXml%></div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<%i++;}%>
						<tr><td></td></tr>
						<!--tr>
							<td height="108"><table width="34" height="108" border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td width="2"></td>
										<td background="../Images/Navigation/2.jpg" class="navigation_black"><div align="center" class="navigation_black"><a href="#" class="navigation_black">系 
													统 设 置</a></div>
										</td>
									</tr>
								</table>
							</td>
						</tr-->
						
					</table>
				</td>
				<TD bgColor="#ffffff" height="419" vAlign="top">
					<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="154" border="0">
						<TR>
							<TD width="1" bgColor="#ffffff"><img width="1"></TD>
							<TD background="../Images/Tree/expand.jpg" width="45%"></TD>
							<TD width="16"><IMG height="17" src="../Images/Tree/arrow.jpg" width="16" border="0"  style="cursor:hand" onclick="swapIMG()"></TD>
							<TD background="../Images/Tree/expand.jpg" width="45%">
							</TD>
							<TD width="3" background="../Images/Tree/right.jpg">
							</TD>
							<td><img width="1"></td>
							
						</TR>
						<TR>
							<TD width="100%" colspan="6" bgcolor="#ffffff">
							<?XML:NAMESPACE PREFIX=TVNS />
								<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
								<tvns:treeview id="Treeview1" treenodesrc="../temp/XML/<%="1" + "/" + UserID%>.xml" target="MainList" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" style="height:120%;width:165px;">
								</tvns:treeview>
							</TD>
						</TR>
						
					</TABLE>
				</TD>
				<td width="1" bgcolor="#e3e3e3"><img width="1"></td>
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
	if(frm.cols.indexOf("35,")>-1)
	{
		frm.cols=frm.cols.replace("35,","209,");
		ImgSwap.style.display="none";
		tableModuleListLeft.style.display="";
		Treeview1.style.display="";
	}
	else
	{
		frm.cols=frm.cols.replace("209,","35,");
		tableModuleListLeft.style.display="none";
		ImgSwap.style.display="";
		Treeview1.style.display="none";
		
	}
}
//--
</script>
<%if(LeftDisplay!=null&&LeftDisplay.ToString().ToLower()=="no"){%><script language=javascript>
	swapIMG()
</script><%}%>