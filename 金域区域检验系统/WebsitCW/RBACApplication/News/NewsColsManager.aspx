<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.RBACApplication.NewsColsManager" Codebehind="NewsColsManager.aspx.cs" %>
<%Response.Expires=0;%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>新闻栏目管理</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../../news/Main.css">
		<style>
		.upDown{
			BORDER-RIGHT: 1px solid; 
			BORDER-BOTTOM: 1px solid
		}
		</style>
	<script id=clientEventHandlersJS language=javascript>
	<!--

	function window_onresize() {

	}
	function ModuleTree_load()
	{
		var objSrc="";
		
		objSrc +=OA.RBACApplication.NewsColsManager.ModulesLoad().value;
		
		if(objSrc.length==0)
			alert("登录过期,请重新登录");
		
		Treeview1.treeNodeSrc =objSrc;
		Treeview1.databind();
		
		//oXML.XMLDocument.documentElement.xml=objSrc;
		//alert(oXML.XMLDocument.documentElement.xml);
		//alert(Treeview1.getChildren().length);
		window.setTimeout("NewsModuleInit",6000);
		
	}
	function NewsModuleInit()
	{
		walkChildren(Treeview1.getChildren());
		Treeview1.setAttribute('selectedNodeIndex',Treeview1.clickedNodeIndex);
	}
	
	function walkChildren(arrayChildren)
	{
		var currentChild;
		for (var i = 0; i < arrayChildren.length; i++)
		{
			currentChild = arrayChildren[i];
			if(currentChild.getAttribute("para")!=null&&currentChild.getAttribute("para").indexOf("Catagory=")>=0)
			{
				currentChild.setAttribute("DEFAULTSTYLE","Color:blue;font-weight:bold");
				currentChild.setAttribute("ModuleType","news");
				
				if(currentChild.getParent()!=null)
				{
					currentChild.getParent().setAttribute("Expanded",true);
					currentChild.getParent().setAttribute("DEFAULTSTYLE","Color:red;font-weight:bold");
				}
			}
			//window.status=currentChild.getAttribute("para");
			walkChildren(currentChild.getChildren());
		}
	} 
	
	//无法实现currentChild.remove();
	//递归可能出在索引问题，remove一个,Index可能要少一个
	
	function ClearChildren(arrayChildren)
	{
		var currentChild;
		//debugger;
		var iCount=arrayChildren.length-1;
		for (var i = iCount; i >-1 ; i--)
		{
			currentChild = arrayChildren[i];
			
			//Clear2Children(currentChild.getChildren());
			//ClearChildren(currentChild.getChildren());
			
			var hasCHILD=false;
			//if(currentChild.getChildren().length>0)
			hasCHILD=hasNewsNode(currentChild)
			
			if(!hasCHILD&&!(currentChild.getAttribute("para")!=null
					&&currentChild.getAttribute("para").indexOf("Catagory=")>=0))
			{
				if(currentChild.getParent()!=null)
					window.setTimeout("currentChild.getParent().setAttribute('Expanded',true)",1000);
					
				currentChild.remove();
			}
		}
	}
	
	function Clear2Children(arrayChildren)
	{
		var currentChild;
		//debugger;
		var iCount=arrayChildren.length-1;
		for (var i = iCount; i >-1 ; i--)
		{
			currentChild = arrayChildren[i];
			var currentChildj;
			var currentChildjs=currentChild.getChildren();
			
			var jCount=currentChildjs.length-1;
			
			for (var j = jCount; j >=0; j--)
			{
				currentChildj = currentChildjs[j];
				var hasCHILDj=false;
				
				//if(currentChild.getChildren().length>0)
					hasCHILDj=hasNewsNode(currentChildj)
				
				if(!hasCHILDj
					&&!(currentChildj.getAttribute("para")!=null
					&&currentChildj.getAttribute("para").indexOf("Catagory=")>=0))
				{
					//if(currentChildj.getAttribute("para")!=null)
					//	alert(currentChildj.getAttribute("para").indexOf("Catagory="));
					
					currentChildj.remove();
				}
			}
		}
	} 
	
	function hasNewsNode(currentChild)
	{
		arrayChildren=currentChild.getChildren();
		var hasCHILD=false;
		for (var i = 0; i < arrayChildren.length; i++)
		{
			node = arrayChildren[i];
			
			if(node.getAttribute("para")==null||node.getAttribute("para").indexOf("Catagory=")<0)
				continue;
			else
				hasCHILD=true;
		}
		return hasCHILD;
	}
	

	

		var mySelectedTr=null;
		var iCurrentRow=0;
		function SelectTr(obj)
		{
			//alert(document.all['TableListData'].id);
			//return;
			if(mySelectedTr!=null)
				mySelectedTr.style.backgroundColor= 'transparent';
			mySelectedTr=obj;
			obj.style.backgroundColor= 'skyblue';
			for(var i=0;i<document.all['TableListData'].rows.length;i++)
			{
				if(obj==TableListData.rows[i])
				{
					iCurrentRow=i;
					theXMLisland.recordset.AbsolutePosition =i;
				}
			}
		}
		
		function NavigateRow()
		{
			//alert(event.keyCode);
			
			if(event.keyCode==40)
			{
				if(theXMLisland.recordset.AbsolutePosition < theXMLisland.recordset.RecordCount)
				{
					theXMLisland.recordset.moveNext();
					SelectTr(document.all['TableListData'].rows[iCurrentRow+1]);
				}
			}
			else if(event.keyCode==38)
			{
				if(theXMLisland.recordset.AbsolutePosition > 1)
				{
					theXMLisland.recordset.movePrevious();
					SelectTr(document.all['TableListData'].rows[iCurrentRow-1]);
				}
			}
			else if(event.keyCode==13)
			{
				DTPicker1.focus();
			}
		}
		function BrowseEditNews(objNews)
		{
			//alert(objNews.parentNode.firstChild.value);
		    window.open("../../news/browse/CategoryNews.aspx?Catagory=" + objNews.parentNode.firstChild.value, "_blank", "toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top=0,left=0,width=" + window.screen.availWidth + ",height=" + window.screen.availHeight + ",scrollbars=yes");
		}
		function ClearNews(objNews)
		{
			//alert(objNews.parentNode.firstChild.value);
			if(confirm(objNews.parentNode.firstChild.value 
				+"\n\n确实要删除[" +objNews.parentNode.firstChild.value +"]全部文档吗?"
				+ "\n文章内容一旦删除,不能恢复" 
				+ "\n\n本操作只清空文章,不删除栏目,如果要删除栏目,请点击上面[删除栏目]功能"))
			{
				var ColsName=objNews.parentNode.firstChild.value;
				var iClearNums=OA.RBACApplication.NewsColsManager.ClearNewsPages(ColsName).value;
				//if(iClearNums>=0)
				//	alert("[" +objNews.parentNode.firstChild.value +"]共清除" + iClearNums);
				//else
				//	alert("[" +objNews.parentNode.firstChild.value +"]清除失败");
				
				//alert(objNews.parentNode.parentNode.parentNode.parentNode.childNodes.length);
				var objTable=objNews.parentNode.parentNode.parentNode.parentNode;
				//return;
				var tableRow=findThisRow(objTable.childNodes,ColsName);
				//objNews.parentNode.parentNode.parentNode.removeChild(objNews.parentNode.parentNode);
				
				JRemoveRecord(tableRow);
				
				
				window.status=tableRow;
				theXMLisland.recordset.update();
			}
		}
		
		function findThisRow(Nodes,ColsName)
		{
			for (var i = 1; i <= Nodes.length; i++)
			{
				//alert(Nodes[i].firstChild.firstChild.firstChild.innerHTML + ColsName);
				if(Nodes[i].firstChild.firstChild.firstChild.innerHTML == ColsName)
					return i;
				//alert(Nodes[i].firstChild.firstChild.innerHTML == ColsName);
			}
		}
		function JRemoveRecord(iCols){

			//get the cooresponding node index from the number in the textbox

			var index = parseInt(iCols)-1;

			//retrieve the child node cooresponding to that index

			var childNode = theXMLisland.XMLDocument.documentElement.childNodes(index);

			//call the removeChild method to delete this node

			theXMLisland.XMLDocument.documentElement.removeChild(childNode);

			childNode="";

		}
		function testClick()
		{
			var i="0.0";
			alert(Treeview1.clickedNodeIndex);
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			locateHref(node);
			
		}
		function SaveNewsModule()
		{
			var sRun=document.all["buttonRunDescr"].value;
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			if(node==null)
				return false;
				
			var bRunNew=OA.RBACApplication.NewsColsManager.EditModule(sRun,node.getAttribute("NodeData"),document.all["GroupName"].value,document.all["ModuleImg"].src).value;
			if(bRunNew)
			{
				ModuleTree_load();
				NewsModuleInit();
			}
			return;
		}
		function locateHref(node)
		{
			if(node!=null)
			{
				document.all["GroupName"].value=node.getAttribute("Text");
				document.all["ModuleImg"].src=node.getAttribute("ImageUrl");
				if(node.getAttribute("para")!=null&&node.getAttribute("para").indexOf("Catagory=")>=0)
					document.all["buttonSave"].disabled=false;
				else
					document.all["buttonSave"].disabled=true;
				
				var nodeTemp=node;
				document.all["ParentName"].value ="";
				while(nodeTemp!=null)
				{
					document.all["ParentName"].value ="/" + nodeTemp.getAttribute("Text") + document.all["ParentName"].value;
					nodeTemp=nodeTemp.getParent();
				}
			}
		}
	//-->
	</script>
	<script language="javascript" for="Toolbar1" event="onbuttonclick">
		
		switch (event.srcNode.getAttribute('ID'))
		{
			case 'newsCols':
				//var node=Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
				//alert(node.getAttribute("para"));
				walkChildren(Treeview1.getChildren());
				ClearChildren(Treeview1.getChildren());
				Clear2Children(Treeview1.getChildren());
				Treeview1.setAttribute('selectedNodeIndex',Treeview1.clickedNodeIndex);//(Treeview1.clickedNodeIndex).setAttribute("selected",true);
				var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
				locateHref(node);
				break;
				
			case 'ExpandCols':
				walkChildren(Treeview1.getChildren());
				
				break;
			case 'AllCols':
				//alert(Treeview1.treeNodeSrc);
				//ModuleTree_load();
				//Treeview1.treeNodeSrc =objSrc;
				Treeview1.databind();
				walkChildren(Treeview1.getChildren());
				Treeview1.setAttribute('selectedNodeIndex',Treeview1.clickedNodeIndex);
				var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
				locateHref(node);
				break;
		}
	</script>
	
	
	
	<script language="javascript" for="Toolbar2" event="onbuttonclick">
		
		switch (event.srcNode.getAttribute('ID'))
		{
			case 'new':
				//alert(Treeview1.clickedNodeIndex)
				document.all["GroupName"].value="";
				document.all["ModuleImg"].src="../../images/icons/htmlicon.gif";
				
				
				document.all["ParentName"].value ="";
				var nodeTemp = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
				while(nodeTemp!=null)
				{
					document.all["ParentName"].value ="/" + nodeTemp.getAttribute("Text") + document.all["ParentName"].value;
					nodeTemp=nodeTemp.getParent();
				}
				document.all["buttonSave"].disabled=false;
				document.all["buttonRunDescr"].value="New";
				
				break;
				
			case 'modify':
				var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
				if(node!=null)
				{
					if(node.getAttribute("para")!=null&&node.getAttribute("para").indexOf("Catagory=")>=0)
						;
					else
					{
						alert("不是新闻模块, 不允许在这里修改, 请回去基础模块设置功能内进行操作");
						return;
					}
					document.all["GroupName"].value=node.getAttribute("Text");
					document.all["ModuleImg"].src=node.getAttribute("ImageUrl");
					
					var nodeTemp=node;
					document.all["ParentName"].value ="";
					while(nodeTemp!=null)
					{
						document.all["ParentName"].value ="/" + nodeTemp.getAttribute("Text") + document.all["ParentName"].value;
						nodeTemp=nodeTemp.getParent();
					}
					document.all["buttonRunDescr"].value="Modify";
				}
				break;
			case 'Remove'://Restore
				var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
				if(node!=null)
				{
					if(node.getAttribute("para")!=null&&node.getAttribute("para").indexOf("Catagory=")>=0)
						;
					else
					{
						alert("不是新闻模块, 不允许在这里删除, 如果要删除, 请回去基础模块设置功能内进行操作");
						return;
					}
						
					if(!confirm("真的要删除["+node.getAttribute("Text")+"]吗？模块删除不能恢复"))
						return;
					document.all["GroupName"].value=node.getAttribute("Text");
					document.all["ModuleImg"].src=node.getAttribute("ImageUrl");
					
					var nodeTemp=node;
					document.all["ParentName"].value ="";
					while(nodeTemp!=null)
					{
						document.all["ParentName"].value ="/" + nodeTemp.getAttribute("Text") + document.all["ParentName"].value;
						nodeTemp=nodeTemp.getParent();
					}
					if(node.getAttribute("para")!=null&&node.getAttribute("para").indexOf("Catagory=")>=0)
					{
						var bRunNew=OA.RBACApplication.NewsColsManager.EditModule("Delete",node.getAttribute("NodeData"),document.all["GroupName"].value,document.all["ModuleImg"].src).value;
						if(bRunNew)
						{
							ModuleTree_load();
							NewsModuleInit();
						}
						else
							alert("删除失败, 请先删除子模块或该模块已经删除");
					}
				}
				break;
		}
	</script>
	
	<script language="javascript" event="onnodebound" for="Treeview1">
		walkChildren(Treeview1.getChildren());
		Treeview1.setAttribute('selectedNodeIndex',Treeview1.clickedNodeIndex);
		var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
		locateHref(node);
		document.all["buttonRunDescr"].value="Modify";
	</script>
	
	<script language="javascript" for="Treeview1" event="onclick">
		var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
		locateHref(node)
	</script>

	</HEAD>
	<body bottomMargin="1" leftMargin="1" topMargin="1" rightMargin="1" onload="ModuleTree_load();NewsModuleInit()" language=javascript onresize="return window_onresize()">
		<form id="Form1" method="post" runat="server">
			<?XML:NAMESPACE PREFIX="TBNS" /><?IMPORT NAMESPACE="TBNS" IMPLEMENTATION="/webctrl_client/1_0/toolbar.htc" />
			<?XML:NAMESPACE PREFIX="TVNS" /><?IMPORT NAMESPACE="TVNS" IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />				
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" height="100%" border="1">
				
					<TR>
						<TD vAlign="top" width="20%">
						
						<TBNS:Toolbar id="Toolbar1" oncheckchange="JScript:if (event.srcNode != null) __Toolbar1_State__.value+=((event.srcNode.getAttribute('selected')=='true')?'+':'-')+event.flatIndex+';';" onbuttonclick="JScript:if (event.srcNode != null) if ((event.srcNode.getType() != 'checkbutton') || (event.srcNode.getAttribute('_autopostback') != null)) if (getAttribute('_submitting') != 'true'){setAttribute('_submitting', 'true');try{__doPostBack('Toolbar1',event.flatIndex);}catch(e){setAttribute('_submitting', 'false');}}" onwcready="JScript:try{__Toolbar1_State__.value = ''}catch(e){}" style="background-color:#EEFF99;border-color:Blue;border-style:Double;cursor:hand;">

							<TBNS:ToolbarButton ID="newsCols" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0065_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">新闻栏目</TBNS:ToolbarButton>
							<!--TBNS:ToolbarButton ID="ExpandCols" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0029_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">展开</TBNS:ToolbarButton-->
							<TBNS:ToolbarButton ID="AllCols" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0069_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">全部</TBNS:ToolbarButton>
						
						</TBNS:Toolbar>
						
						<tvns:treeview id="Treeview1" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" style="height:100%;width:174px;">
						</tvns:treeview>	
							
						</TD>
						<TD vAlign="top">
							<TBNS:Toolbar id="Toolbar2" oncheckchange="JScript:if (event.srcNode != null) __Toolbar1_State__.value+=((event.srcNode.getAttribute('selected')=='true')?'+':'-')+event.flatIndex+';';" onbuttonclick="JScript:if (event.srcNode != null) if ((event.srcNode.getType() != 'checkbutton') || (event.srcNode.getAttribute('_autopostback') != null)) if (getAttribute('_submitting') != 'true'){setAttribute('_submitting', 'true');try{__doPostBack('Toolbar1',event.flatIndex);}catch(e){setAttribute('_submitting', 'false');}}" onwcready="JScript:try{__Toolbar1_State__.value = ''}catch(e){}" style="background-color:#FFFFFF;border-color:Blue;border-style:Double;cursor:hand;">
								<TBNS:ToolbarButton ID="new" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0013_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">&nbsp;增加栏目</TBNS:ToolbarButton>
								<TBNS:ToolbarButton ID="modify" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0015_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">&nbsp;修改栏目</TBNS:ToolbarButton>
								<TBNS:ToolbarButton ID="Remove" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0014_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">&nbsp;删除栏目</TBNS:ToolbarButton>
								<TBNS:ToolbarButton ID="Restore" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0032_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">&nbsp;恢复栏目</TBNS:ToolbarButton>
								<TBNS:ToolbarButton ID="Help" defaultStyle="display;border:solid 1px white;display:;" imageUrl="../../images/icons/0067_b.gif" hoverstyle="border:solid 1px red;" onkeydown="if (event.keyCode==13){event.returnValue=false}">&nbsp;栏目管理帮助</TBNS:ToolbarButton>
							</TBNS:Toolbar>
							<div id=newsEdit>
							
							<table align=center width="95%" border=0>
								<tr bgcolor=white height=30>
									<td colspan=2 align=Center width=70% id=NewsTitle>
										新闻编辑
									</td>
								</tr>
								
								<tr bgcolor=white>
									<td align=right style="WIDTH: 30%">
										<font color=red>*</font>名称：
									</td>
									<td align=left width=70%>
										<input type="text" id="GroupName" name="GroupName" style="WIDTH:80%">
									</td>
								</tr>
								<tr bgcolor=white>
									<td align=right style="WIDTH: 30%">
										位置：
									</td>
									<td align=left width=70%>
										<input type="text" id="ParentName" name="ParentName" style="WIDTH: 80%; BACKGROUND-COLOR: gainsboro" disabled>
									</td>
								</tr>
								<tr bgcolor=white height="30">
									<td align=right valign=top style="WIDTH: 30%">
										&nbsp;图片：
									</td>
									<td align=left width=70%>
										<img id="ModuleImg" src="../../images/icons/htmlicon.gif">
									</td>
								</tr>
							</table>
							<table width="100%" border=0 height=100 cellspacing=0>
								<tr height="30">
									<td colspan=2 valign=top align=middle>
										<br>
										<input id=buttonSave type=button value=" 保存 " onclick="SaveNewsModule()"><input type="hidden" id="buttonRunDescr" value="Modify">
										<input type=button value=" 取消 ">
									</td>
								</tr>
							</table>
							</div>
							
							<div>全部文章信息统计</div>
							<div style="BORDER-RIGHT: black 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 2px solid; PADDING-LEFT: 10px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=gold, EndColorStr=#FFFFFF); LEFT: 0px; PADDING-BOTTOM: 10px; FONT: 10pt tahoma; BORDER-LEFT: black 2px solid; WIDTH: 100%; PADDING-TOP: 10px; BORDER-BOTTOM: black 2px solid; POSITION: relevant; TOP: 0px; HEIGHT: 120px">
							<TABLE class="ListTable" id="TableListData" cellSpacing="0" cellPadding="0" width="100%" border="0" datasrc="#theXMLisland" name="TableListData" style="BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid">
									<thead bgcolor="transparent" class="ListHeader" style="FONT-WEIGHT: bold;HEIGHT:30px" height="30">
										<td class="upDown" align="center">新闻栏目</td>
										<td class="upDown" align="center">文章总数</td>
										<td class="upDown" align="center">点击数</td>
										<td class="upDown" align="center">最近发表日期</td>
										<td class="upDown" align="center">操作</td>
									</thead>
									<tr class="" title=""
										bgColor="transparent" height="25">
										<td nowrap align="left" class="upDown"><div  style="FONT-WEIGHT: bold" datafld="Catagory">&nbsp;</div></td>
										<td nowrap align="center" class="upDown"><div datafld="TitleCount"></div></td>
										<td nowrap align="center" class="upDown"><div datafld="HitCount"></div></td>
										<td nowrap align="center" class="upDown"><div datafld="Newbuildtimet"></div></td>
										<td nowrap align="center" class="upDown"><input type="hidden" datafld="Catagory"><input type="button" value="清空" onclick="ClearNews(this)"><input type="button" value="浏览编辑" onclick="BrowseEditNews(this)"></td>
										
									</tr>
							</TABLE>
							</div>
							<input type="button" onclick="testClick()" value="测试">
						</TD>
					</TR>
				</TABLE>
			
		</form><XML id="oXML"></XML>
		<XML ID="theXMLisland"><%=xmlData%></XML>

		
	</body>
</HTML>
