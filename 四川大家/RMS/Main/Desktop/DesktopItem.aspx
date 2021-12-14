<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.DesktopItem" Codebehind="DesktopItem.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DesktopItem</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript" for="Treeview1" event="onclick">
			//var parentNode = event.treeNode.getAttribute("NodeData");
			//if(node!=null)
			//{
			var node = Treeview1.getTreeNode(Treeview1.clickedNodeIndex);
			if(node!=null)
			{
			var SN=node.getAttribute("SN");
			var i=0;
			var dirz='';
		if(node.getAttribute("NodeData")!='8'&&node.getAttribute("NodeData")!='7')
		{			
			document.all['ShowName'].value=node.getAttribute("Text");
			document.all['ShowId'].value=node.getAttribute("NodeData");
			document.all['ParentId'].value="";
			document.all['tooltip'].value=node.getAttribute("tooltip");
			document.all['ImageUrl'].value=node.getAttribute("ImageUrl");			
			document.all['Para'].value=node.getAttribute("para");
			document.all['DataFrom'].value="";
			
			dirz=node.getAttribute("Text");			
			getDir(node);
			document.all['FileDir'].value=dirz;
			document.all['ShowName'].value=document.all['FileDir'].value;
			if(document.all['tooltip'].value=="共享文件夹")
			{
				getPnode(node);
								
				if(i==2)
				{
					document.frames['Tree'].location="FilesTree.aspx?ModuleId="+node.getAttribute("NodeData")+"&ImageUrl="+node.getAttribute("ImageUrl")+"&tooltip="+node.getAttribute("tooltip");				
					document.all['DataFrom'].value="Drive";
				}
			}
			else
			{
				if(document.all['tooltip'].value!='新闻')
				{
					document.frames['Tree'].location="FilesTree.aspx?ModuleId="+node.getAttribute("NodeData")+"&ImageUrl="+node.getAttribute("ImageUrl")+"&tooltip="+node.getAttribute("tooltip");				
					if(node.getAttribute("NodeData")!='125')
						document.all['DataFrom'].value="Drive";
				}
				else
				{
					document.frames['Tree'].location="FilesTree.aspx";
				}
			}			
		}
			function getDir(myNode)
			{	
				if(myNode.getParent()!=null&&myNode.getParent().getAttribute("NodeData")!='8')
				{
					dirz=myNode.getParent().getAttribute("Text")+'/'+dirz
					myNode=myNode.getParent();
					getDir(myNode);
				}
			}			
			function getPnode(myNode)
			{
				if(myNode.getParent()!=null)
				{
					i++;
					if(myNode.getParent().getAttribute("NodeData")!='64')
					{
						myNode=myNode.getParent();
						getPnode(myNode);
					}
				}
			}
			}
		</script>
		<script id="clientEventHandlersJS" language="javascript">
<!--
	var Num=0;
			function NumberOfLay(folder)
			{
				
				var StarNum=1;
				while(folder.indexOf("/",StarNum+1)>0)
				{
					var i=0;
					i=folder.indexOf("/",StarNum+1);
					StarNum=i;
					Num++;
					
				}
			}
function buttReturn_onclick() {
	if(document.all['ShowName'].value==''||document.all['ShowId'].value=='')
	{
		alert('请选择一个桌面条目');
		return false;
	}
	 NumberOfLay(document.all['ShowName'].value);
	
	var str="";
	str="left,";
	if(RadioRight.checked)
		str="right,";
	str +="list,";
	if(RadioStyleIcon.checked)
		str +="icon,";
	str += txtRecords.value+",";
	str += document.all['ShowName'].value+",";
	str +=document.all['ParentId'].value+",";		
	str += document.all['tooltip'].value+",";
	//apara表示para因为&para属于html的特殊符号
	str += document.all['Para'].value+",";
	str += document.all['ImageUrl'].value+",";
	str += document.all['DataFrom'].value+",";
	str += document.all['ShowId'].value+","	
	
	
	//var str="";
	//str="addto=left";
	//if(RadioRight.checked)
	//	str="addto=right";
	//str +="&style=list";
//	if(RadioStyleIcon.checked)
	//	str +="&style=icon";
	//str +="&size=" + txtRecords.value;
//	str +="&name=" + document.all['ShowName'].value;
//	str +="&ParentId=" + document.all['ParentId'].value;		
//	str +="&tooltip=" + document.all['tooltip'].value;
//	//apara表示para因为&para属于html的特殊符号
//	str +="&apara=" + document.all['Para'].value;
//	str +="&ImageUrl=" + document.all['ImageUrl'].value;
//	str +="&DataFrom=" + document.all['DataFrom'].value;
//	str +="&desktop=" + document.all['ShowId'].value	
	window.parent.returnValue=str;
	window.parent.close();
	
}

//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<TABLE WIDTH="436" BORDER="1" CELLSPACING="1" CELLPADDING="1" style="WIDTH: 436px; HEIGHT: 356px"
			id="Table1">
			<TR>
				<TD style="WIDTH: 320px;HEIGHT:200">
					<form id="Form1" method="post" runat="server">
					<table>
					<tr>
					<td>
					<DIV align="left" >
			<?XML:NAMESPACE PREFIX=TVNS />
			<?IMPORT NAMESPACE=TVNS IMPLEMENTATION="/webctrl_client/1_0/treeview.htc" />
			<tvns:treeview id="Treeview1" <%if(SaveXmlTree!=null&&SaveXmlTree.DocumentElement!=null)
				{
					Response.Write(" treenodesrc=\"" + SaveXmlTree.DocumentElement.OuterXml.Replace("\"","\'") + "\" ");
				}
				%> target="right" selectedNodeIndex="0" HelperID="__ModuleTree_State__" systemImagesPath="/webctrl_client/1_0/treeimages/" selectExpands="false" onexpand="javascript: if (this.clickedNodeIndex != null) this.queueEvent('onexpand', this.clickedNodeIndex)" oncollapse="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncollapse', this.clickedNodeIndex)" oncheck="javascript: if (this.clickedNodeIndex != null) this.queueEvent('oncheck', this.clickedNodeIndex)" onselectedindexchange="javascript: if (event.oldTreeNodeIndex != event.newTreeNodeIndex) this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex)" 
			style="height:270px;width:200px;">
			</tvns:treeview>
	</div>
					</td>
					</tr>
					</table>						
					</form>
				</TD>
				<td><iframe id="Tree" style="WIDTH: 216px; HEIGHT: 274px" src="FilesTree.aspx"></iframe>
				</td>
			</TR>
			<TR>
				<TD style="WIDTH: 326px; HEIGHT: 50px">
					<table width="100%" id="Table2">
						<tr>
							<td>桌面显示名称：</td><td><input type=text id=ShowName name=ShowName style="WIDTH:120"><input type=text id=ShowId name=ShowId style="WIDTH:0"><input type=text id=ParentId name=ParentId style="WIDTH:0">
							<input type=text id=Para name=Para style="WIDTH:0"><input type=text id=tooltip name=tooltip style="WIDTH:0">
							<input type=text id=ImageUrl name=ImageUrl style="WIDTH:0">
							<input type=text id=DataFrom name=DataFrom style="WIDTH:0">
							<input type=text id=FileDir name=FileDir style="WIDTH:0">
							</td>
						</tr>
						<tr>
							<td><fieldset style="WIDTH: 84px; HEIGHT: 51px"><legend>位置</legend><INPUT type="radio" id="RadioLeft" CHECKED name="RadioPostion" value="RadioLeft">左<INPUT type="radio" id="RadioRight" name="RadioPostion" value="RadioRight">右</fieldset>
							</td>
							<td><fieldset style="WIDTH: 114px; HEIGHT: 49px"><legend>显示风格</legend><INPUT type="radio" id="RadioStyleList" CHECKED name="RadioStyle" value="RadioStyleList">列表<INPUT type="radio" id="RadioStyleIcon" name="RadioStyle" value="RadioStyleIcon" disabled>图标</fieldset>
							</td>
							<td><fieldset style="WIDTH: 61px; HEIGHT: 49px" align="absMiddle"><legend>记录数</legend><INPUT style="WIDTH: 44px; HEIGHT: 22px" type="text" size="2" value="10" id="txtRecords"
										name="txtRecords"></fieldset>
							</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD style="WIDTH: 325px" align="center"><INPUT type="button" value="确定" id="buttReturn" language="javascript" onclick="return buttReturn_onclick()"
						name="buttReturn"></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
