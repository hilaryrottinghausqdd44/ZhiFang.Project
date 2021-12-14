<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.OrgTree" Codebehind="OrgTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>部门组织机构图</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		var Exnode;
		</script>
		<script language="javascript" for="Treeview1" event="onclick">
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			locateHref(node);
		</script>
	<script id=clientEventHandlersJS language=javascript>
<!--

function window_onload() {
	//applyTransition ();
}
function locateHref(node)
{
    if (node != null) {
        var urlFrm4 = parent.frames['frm4'].location.href;
        if (urlFrm4.indexOf("DataOutQuery") > 0) {
            urlFrm4 = urlFrm4.substr(0, urlFrm4.indexOf("&DataOutQuery"));
        }
        urlFrm4 = urlFrm4 + "&DataOutQuery=" + node.getAttribute('Text');

        parent.frames['frm4'].location = urlFrm4;
        //parent.frames['MainList'].location = '../../RBAC/Organizations/Deptlist.aspx?id=' + node.getAttribute('NodeData');
    }
}

function applyTransition ()
{
	var frm=parent.fset;
	if(frm.cols.indexOf(",0,")>-1)
	{
		window.setTimeout("applyTransition()",1000);
	}
	else
	{
		if(parent.frames["MainList"].document.location.href.indexOf("Deptlist.aspx")>-1)
		{
			window.setTimeout("applyTransition()",1000);					
		}
		else
		{
			frm.cols=frm.cols.replace(",170,",",0,");
			window.setTimeout("applyTransition()",1000);
		}
	}
}
		
function expandedChanged() 
{
	var frm=parent.fset;
	var treenodes=Treeview1.getChildren();
	//if(frm.cols.indexOf(",0,")==-1)
	//{
		if(document.all["nodedata"]!=null&&document.all["nodedata"].value!="")
		{
			//var node=Treeview1.getTreeNode(document.all["nodedata"].value)
			var node=testchecked(treenodes,document.all["nodedata"].value);
			
		}
	//}
}

function testchecked(treenodes,Nodedata)
{
	
	if(treenodes==null||typeof(treenodes)=="undefined")
		return null;
	
	for(var i=0;i<treenodes.length;i++)
	{
		if(treenodes[i].getAttribute("NodeData")==Nodedata)
		{
			Exnode=treenodes[i];
			Exnode.setAttribute("Expanded",true);
			
			Treeview1.setAttribute('selectedNodeIndex',Exnode.getNodeIndex());
			
			return Exnode;
		}
		
		testchecked(treenodes[i].getChildren(),Nodedata);
	}
}
//-->
</script>
</HEAD>
	<body MS_POSITIONING="GridLayout" ondblclick="location=location + '?refresh=true'" language=javascript onload="return window_onload()" bgcolor="#d1d1d1" topmargin=0 rightmargin=0 leftmargin=0 bottommargin=0>
	
	<table border="0" width="100%" align="center" cellspacing="0" cellpadding="0" style="COLOR: white;BORDER-COLLAPSE: collapse"
			bgcolor="steelblue"><tr height="45"><td><img src="../../images/icons/0019_a.gif" align="absBottom"></td><td  nowrap><%=Company%></td></tr>
			<tr><td></td><td  nowrap>组织机构图</td></tr></table>
		<?XML:NAMESPACE PREFIX=TVNS />
		<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
		<tvns:treeview id="Treeview1" treenodesrc="xml/DeptTree.xml" target="MainList" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" style="height:100%;width:154px;">
		</tvns:treeview>
		
	<input type="text" name="nodedata" id="nodedata" onpropertychange="javascript:expandedChanged()" style="width:0px;height:0px">
		
		
		
	</body>
</HTML>
