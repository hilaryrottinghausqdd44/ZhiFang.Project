<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Text.RegularExpressions" %>

<%@ Page validateRequest="false" enableEventValidation="false" language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Input.InputOneTable" Codebehind="InputOneTable.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>单表</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%if(cssFile.Trim()==""){%>
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<%}else{%>
		<LINK href="../<%=cssFile%>" type="text/css" rel="stylesheet">
		<%}%>
		<script language="javascript" src="../js/dialog.js"></script>
		
		<style>         
					/*定义菜单方框的样式1*/ 
			.skin0 { 
			position:absolute; 
			padding-top:4px; 
			text-align:left; 
			width:180px; /*宽度，可以根据实际的菜单项目名称的长度进行适当地调整*/ 
			border:2px solid black; 
			background-color:menu; /*菜单的背景颜色方案，这里选择了系统默认的菜单颜色*/ 
			font-family:"宋体"; 
			line-height:20px; 
			cursor:default; 
			visibility:hidden; /*初始时，设置为不可见*/ 
			top:0px;
			left:0px;
			} 
			/*定义菜单方框的样式2*/ 
			.skin1 { 
			padding-top:4px; 
			cursor:default; 
			font:menutext; 
			position:absolute; 
			text-align:left; 
			font-family: "宋体"; 
			font-size: 10pt; 
			width:100px; /*宽度，可以根据实际的菜单项目名称的长度进行适当地调整*/ 
			background-color:menu; /*菜单的背景颜色方案，这里选择了系统默认的菜单颜色*/ 
			border:1 solid buttonface; 
			visibility:hidden; /*初始时，设置为不可见*/ 
			border:2 outset buttonhighlight; 
			} 

			/*定义菜单条的显示样式*/ 
			.menuitems { 
			padding:2px 1px 2px 10px; 
			} 
			.div {
	        margin:0px auto 0 auto;
            height:110px;
            overflow:auto;
	        }
		</style>
		<script language="javascript"> 
			<!-- 
			//定义菜单显示的外观，可以从上面定义的2种格式中选择其一 
			var menuskin = "skin0"; 
			//是否在浏览器窗口的状态行中显示菜单项目条对应的链接字符串 
			var display_url = 0; 

			function showmenuie5() { 
			//获取当前鼠标右键按下后的位置，据此定义菜单显示的位置 
			var rightedge = document.body.clientWidth-event.clientX; 
			var bottomedge = document.body.clientHeight-event.clientY; 

			//如果从鼠标位置到窗口右边的空间小于菜单的宽度，就定位菜单的左坐标（Left）为当前鼠标位置向左一个菜单宽度 
			if (rightedge <ie5menu.offsetWidth) 
			ie5menu.style.left = document.body.scrollLeft + event.clientX - ie5menu.offsetWidth; 
			else 
			//否则，就定位菜单的左坐标为当前鼠标位置 
			ie5menu.style.left = document.body.scrollLeft + event.clientX; 

			//如果从鼠标位置到窗口下边的空间小于菜单的高度，就定位菜单的上坐标（Top）为当前鼠标位置向上一个菜单高度 
			if (bottomedge <ie5menu.offsetHeight) 
			ie5menu.style.top = document.body.scrollTop + event.clientY - ie5menu.offsetHeight; 
			else 
			//否则，就定位菜单的上坐标为当前鼠标位置 
			ie5menu.style.top = document.body.scrollTop + event.clientY; 

			//设置菜单可见 
			ie5menu.style.visibility = "visible"; 
			return false; 
			} 
			function hidemenuie5() { 
			//隐藏菜单 
			//很简单，设置visibility为hidden就OK！ 
			ie5menu.style.visibility = "hidden"; 
			} 

			function highlightie5() { 
			//高亮度鼠标经过的菜单条项目 

			//如果鼠标经过的对象是menuitems，就重新设置背景色与前景色 
			//event.srcElement.className表示事件来自对象的名称，必须首先判断这个值，这很重要！ 
			if (event.srcElement.className == "menuitems") { 
			event.srcElement.style.backgroundColor = "highlight"; 
			event.srcElement.style.color = "white"; 

			//将链接信息显示到状态行 
			//event.srcElement.url表示事件来自对象表示的链接URL 
			if (display_url) 
			window.status = event.srcElement.url; 
			} 
			} 

			function lowlightie5() { 
			//恢复菜单条项目的正常显示 

			if (event.srcElement.className == "menuitems") { 
			event.srcElement.style.backgroundColor = ""; 
			event.srcElement.style.color = "black"; 
			window.status = ""; 
			} 
			} 

			//右键下拉菜单功能跳转 
			function jumptoie5() { 
			//转到新的链接位置 
			var seltext=window.document.selection.createRange().text 

			if (event.srcElement.className == "menuitems") { 
			//如果存在打开链接的目标窗口，就在那个窗口中打开链接 
			if (event.srcElement.getAttribute("target") != null) 
			window.open(event.srcElement.url, event.srcElement.getAttribute("target")); 
			else 
			//否则，在当前窗口打开链接 
			window.location = event.srcElement.url; 
			} 
			} 
			
			//显示隐藏TopBar.aspx
			function displayTopBar()
			{
				if(window.parent.parent.TopBar!=null)
				{
						parent.parent.TopBar.openWide();
				}
			}

						
			var frm=parent.parent.fset;
			if(frm.rows=="60,*")    //如果已经显示则调用displayTopBar()
			{
				displayTopBar();
			}
			//--> 
		</script> 
		<script language="javascript">
		
		function openWin(url,w,h)
		{
			var px=Math.round((window.screen.availWidth-w)/2);
			var py=Math.round((window.screen.availHeight-h)/2);
			var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=yes,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes");
		}
		function openWinPositionSize(url, ileft, itop, iwidth, iheight, WindowfrmName)
		{
			//alert(" " + window.screen.availWidth + "," + ileft + "," + itop + "," + iwidth + "," + iheight);
			//var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
		    var mywin = window.open(url, WindowfrmName, "toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top=" + itop + ",left=" + ileft + ",width=" + iwidth + ",height=" + iheight + ",scrollbars=yes");
		}
			
		var SelTD = null;
		var para='';
			
		///有bug,应该传递自身的AssesorXpath
		
		var iCurrentRow=0;
		
		function SelectTd(eid,DataXPath)
		{
			eid.style.backgroundColor= 'skyblue';
			//alert('111');
			if(DataXPath!=null&&DataXPath!="")
					Expound(DataXPath);
			if (SelTD != null&&SelTD!=eid)
			{
				SelTD.style.backgroundColor = 'Transparent';
			}
			SelTD = eid;
			if(SelTD.id!=null)
				iCurrentRow=parseInt(SelTD.id.substr(2));
			//SelTD.focus();				
		}
		
		function SelectTddb(urlPop,strPositionSize,bOpen,windowFrmName) {
		    //alert('a');
		    //if (urlPop == "" || !bOpen)
		    //     return true;
			//var strPositionSize=openPositionSize.value;
			var arrPositionSize=strPositionSize.split(",");
			var PositionLeft=window.screen.availWidth * .1;
			var PositionTop=window.screen.availHeight * .1;
			var PositionWidth=window.screen.availWidth * .8;
			var PositionHeight=window.screen.availHeight * .8;
			
			if(arrPositionSize.length==4)
			{
				try
				{
					var iPositionLeft=arrPositionSize[0];
					var iPositionTop=arrPositionSize[1];
					var iPositionWidth=parseFloat(arrPositionSize[2]);
					//alert(Math.round(iPositionWidth));
					var iPositionHeight=parseFloat(arrPositionSize[3]);
					switch(arrPositionSize[0])
					{
						case "左":
							PositionLeft=0;
							break;
						case "中":
							PositionLeft=window.screen.availWidth * (100-iPositionWidth)/200;
							PositionLeft=Math.round(PositionLeft);
							//alert(PositionHeight);
							break;
						case "右":
							PositionLeft=window.screen.availWidth * (100-iPositionWidth)/100;
							PositionLeft=Math.round(PositionLeft);
							break;
						default:
							try
							{
								PositionLeft=parseInt(iPositionLeft);
							}
							catch(e){}
							break;
					}
					switch(arrPositionSize[1])
					{
						case "上":
							PositionTop=0;
							break;
						case "中":
							PositionTop=window.screen.availHeight * (100-iPositionHeight)/200;
							PositionTop=Math.round(PositionTop);
							break;
						case "下":
							PositionTop=window.screen.availHeight * (100-iPositionHeight)/100;
							PositionTop=Math.round(PositionTop);
							break;
						default:
							try
							{
								PositionTop=parseInt(iPositionTop);
							}
							catch(e){}
							break;
					}
					
					switch(arrPositionSize[2])
					{
						default:
							try
							{
								PositionWidth=window.screen.availWidth * iPositionWidth/100;
								PositionWidth=Math.round(PositionWidth);
							}
							catch(e){}
							break;
					}
					
					switch(arrPositionSize[2])
					{
						default:
							try
							{
								PositionHeight=window.screen.availHeight * iPositionHeight/100;
								PositionHeight=Math.round(PositionHeight);
							}
							catch(e){}
							break;
					}
				}
				catch(e)
				{
					alert(e);
				}
			}
			//alert(para);
			//openWin('addOpenTable.aspx?btnid=viewinfo&'+para,window.screen.availWidth,window.screen.availHeight);
			//alert(urlPop + bOpen);
			if (urlPop == "") {
			    if (bOpen) { //去掉这个括号有问题
			        openWinPositionSize('addOpenTable.aspx?btnid=viewinfo&' + para, PositionLeft, PositionTop, PositionWidth, PositionHeight, windowFrmName);
			    }           //支掉这个括号有问题
			}
			else
			    openWinPositionSize(urlPop, PositionLeft, PositionTop, PositionWidth, PositionHeight, windowFrmName);

			//alert(urlPop);
		}
		</script>
		<script language="javascript">
		function fireAdd(strPositionETSize)
		{
			urlPop='<%="http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath %>/DBQuery/Input/addOpenTable.aspx?btnid=BAdd&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&DataXPath='
			    + parent.document.all["hParentXpath"].value
			    +'&AssessorXpath='+parent.document.all["hParentXpath"].value;
			
			SelectTddb(urlPop,strPositionETSize,true,'');
		}
		
		function fireEdit(eid,DataXPath,strPositionETSize)
		{
			SelectTd(eid,DataXPath);
			
			urlPop='<%="http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath %>/DBQuery/Input/addOpenTable.aspx?btnid=BModify&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&DataXPath='
			    + parent.document.all["hParentXpath"].value
			    +'&AssessorXpath='+parent.document.all["hParentXpath"].value;
			
			SelectTddb(urlPop,strPositionETSize,true,'');
		}
		
		
		var intervalID;
		function fireDelete(eid,DataXPath)
		{
			SelectTd(eid,DataXPath);
			
           
            if(window.document.Form1.hiCalculator.value!=0)
            {
                var confirmDel=confirm("确认要删除此记录数据吗？要是该表记录有子表，\n该记录的子表数据也将全部删除！");
                if(confirmDel)
                {
	                //调用删除
	                intervalID=window.setInterval(deleteTimeInterval,100);
                }
            }
            else
                window.status='没有要删除的记录';
        }
		
		function deleteTimeInterval()
		{
		    if(window.frames["Right"]!=null
					&&window.frames["Right"].document!=null
					&&window.frames["Right"].document.readyState=="complete")
		    {
		        window.frames["Right"].Form1.hAction.value="BDelete";
	            window.frames["Right"].Form1.submit();
	            window.clearInterval(intervalID);
	            
	            Form1.submit();
	        }
		}
		
		var openWinXD="";
		function Expound(DataXPath)
		{	
			try
			{
				<%
				string strTemp=Request.ServerVariables["Query_String"];
				string TableEName = Request.QueryString["TableEName"];
				string db = Request.QueryString["db"];
				
				if(strTemp.IndexOf("&AssessorXpath")>-1)
					strTemp=strTemp.Remove(strTemp.IndexOf("&AssessorXpath"),strTemp.IndexOf("&",strTemp.IndexOf("&AssessorXpath")+1)-strTemp.IndexOf("&AssessorXpath"));
				%>
				var strTemp="<%=strTemp.Replace("'","\\'")%>";
				parent.document.all["hParentXpath"].value=DataXPath;
				window.frames["Right"].location="InputOneTableMain.aspx"
					+"?"+strTemp+"&DataXPath="
					+ DataXPath +"&AssessorXpath=" +DataXPath;// + "&Items=北京垂杨柳医院";
				//alert(parent.document.all["hParentXpath"].value);
				para = strTemp+"&DataXPath="+ DataXPath +"&AssessorXpath=" +DataXPath;
				window.document.Form1.openWinUrl.value = "DataXPath="+ DataXPath +"&AssessorXpath=" +DataXPath;
				
			}
			catch(e){}
		}
		function Add()
		{
			var TbStr=document.all["ddd"].innerHTML;
			
			TbStr=TbStr.substring(0,TbStr.length-8);
			TbStr=TbStr+"<TR><TD>3</TD><TD></TD><TD></TD><TD></TD></TR></TBODY>";
			alert(TbStr)
			document.all["ddd"].innerHTML=TbStr;
			alert(document.all["ddd"].length)
		}
		var arg1,arg2,arg3;
		function startSorting(ColumnIndex,orderByDirection,strColumnType)
		{
			top.window.document.body.setCapture(); 
			top.window.document.body.style.cursor = "wait"; 
			window.document.body.style.cursor="progress";
			arg1=ColumnIndex;
			arg2=orderByDirection;
			arg3=strColumnType;
			var thisTable=document.all["TableOrderBy"];
			window.document.body.style.cursor="wait";
			thisTable.style.cursor="wait";
			thisTable.setCapture(true);
			window.document.body.style.cursor="wait";
			
			window.setTimeout(startSorting0,5);
		}
		function startSorting0()
		{
			TdOrderBy(arg1,arg2,arg3);
		}
		function TdOrderByAll(obj,strColumnEName)
		{
            var strOrderByType=obj.title;
            if(strOrderByType=='' || strOrderByType=='正序')
                strColumnEName=strColumnEName +"(1)";
            
            Form1.ColumnOrderBy.value=strColumnEName;
            //alert(strColumnEName);
            Form1.submit();
		}
		function TdOrderBy_bak(ColumnIndex,orderByDirection,strColumnType,objThisCell)
		{
			//window.setTimeout("progressSetCaptureThis()",1);
			window.document.body.style.cursor="progress";
			
			var thisTable=document.all["TableOrderBy"];
			thisTable.style.backgroundColor="gainsboro";
			thisTable.setCapture(true);
			window.document.body.style.cursor="wait";
			//thisTable.deleteRow(ColumnIndex);
			var thisTableTrs=thisTable.rows;
			var rowsCount=thisTableTrs.length;
			rowsCount='<%=NodeTrBodyList!=null?NodeTrBodyList.Count+1:2%>';//排序修改2007-10-08 lizj
			if(rowsCount>thisTableTrs.length-1)
				rowsCount=thisTableTrs.length-1;
			if(objThisCell.title=="")
			{
				objThisCell.title="A->Z";//正序
				objThisCell.style.paddingTop  ='1pt';
			}
			else
			{
				objThisCell.title="";//倒序
				objThisCell.style.paddingTop='';
			}
			
			if(objThisCell.title=="")
			{
				for(var i=1;i<rowsCount;i++)
				{
					for(var j=i+1;j<rowsCount;j++)
					{
						if(strColumnType=='1')
						{
							try
							{
								var iii=parseFloat(thisTableTrs[i].cells[ColumnIndex].innerHTML);
								var jjj=parseFloat(thisTableTrs[j].cells[ColumnIndex].innerHTML);
								if(iii>jjj)
									swapTr(thisTable,i,j);
							}
							catch(e)
							{
								continue;
							}
						}
						else if(thisTableTrs[i].cells[ColumnIndex].innerHTML>thisTableTrs[j].cells[ColumnIndex].innerHTML)
						{
							swapTr(thisTable,i,j);
						}	
						if(thisTableTrs[j].cells[ColumnIndex].innerHTML.indexOf("<A")<0)
							window.status="Sorting:  " + thisTableTrs[j].cells[ColumnIndex].innerHTML;
						else
							window.status="Sorting:  " + thisTableTrs[j].cells[ColumnIndex].firstChild.innerHTML;
					}
				}
			}
			else
			{
				for(var i=1;i<rowsCount;i++)
				{
					for(var j=i+1;j<rowsCount;j++)
					{
						if(strColumnType=='1')
						{
							try
							{
								var iii=parseFloat(thisTableTrs[i].cells[ColumnIndex].innerHTML);
								var jjj=parseFloat(thisTableTrs[j].cells[ColumnIndex].innerHTML);
								if(iii<jjj)
									swapTr(thisTable,i,j);
							}
							catch(e)
							{
								continue;
							}
						}
						else if(thisTableTrs[i].cells[ColumnIndex].innerHTML<thisTableTrs[j].cells[ColumnIndex].innerHTML)
						{
							swapTr(thisTable,i,j);
						}	
						if(thisTableTrs[j].cells[ColumnIndex].innerHTML.indexOf("<A")<0)
							window.status="Sorting:  " + thisTableTrs[j].cells[ColumnIndex].innerHTML;
						else
							window.status="Sorting:  " + thisTableTrs[j].cells[ColumnIndex].firstChild.innerHTML;
					}
				}
			}
			window.setTimeout(ReleaseCaptureThis,150);
			
		}
		function ReleaseCaptureThis()
		{
			var thisTable=document.all["TableOrderBy"];
			window.document.body.style.cursor="default";
			thisTable.releaseCapture();
			thisTable.style.backgroundColor="transparent";
		}
		
		function progressSetCaptureThis()
		{
			window.document.body.style.cursor="progress";
			var thisTable=document.all["TableOrderBy"];
			thisTable.style.backgroundColor="gainsboro";
		}
		
		var bStart=true;
		function applyTransition()
		{
			window.status="aaa";
			bStart=false;
		}
		function swapTr(thisTable,i,j)
		{
			//thisTable.rows[i].swapNode(thisTable.rows[j]);
			thisTable.moveRow(j,i);
			return;
		}
		
		function RunPage(StartPage,PageCount)
		{
			Form1.hPageBegins.value=StartPage;
			Form1.hPageSize.value=PageCount;
			Form1.submit();
		}
		
		function RunPageSize(PageCount)
		{
			Form1.hPageSize.value=PageCount;
			Form1.submit();
		}
		
		function BrowseNews(strObj,strDataBase)
		{
			window.open('inputBrowseNews.aspx?FilePath=' + strDataBase + "&FileName=" +strObj,'','width=500,height=600,scrollbars=1,resizable=yes,top=0,left=200');
		}
		
		
		function NavigateRow(rowNum)
		{
			if(iCurrentRow==0)
				iCurrentRow=rowNum
			//alert(event.keyCode);
			//debugger;
			if(event.keyCode==40)
			{
				iCurrentRow=iCurrentRow+1;
				var nextObj=document.getElementById("aa" + iCurrentRow);
				
				if(nextObj!=null)
				{
					
					if(nextObj.getAttribute("onclick")!=null
						&&nextObj.getAttribute("onclick")!=""
						&&nextObj.getAttribute("onclick")!="undefined")
					{
						
						var strOnclick="";
						strOnclick="" + nextObj.getAttribute("onclick");
						
						strOnclick=strOnclick.substr(strOnclick.indexOf("'"));
						
						strOnclick=strOnclick.substr(1,strOnclick.length-5);
						//alert(strOnclick);
						//return;
						//alert(strOnclick);
						SelectTd(nextObj,strOnclick.replace(/\\/g,""));
					}
				}
				else
					iCurrentRow=iCurrentRow-1;
			}
			else if(event.keyCode==38)
			{
				iCurrentRow=iCurrentRow-1;
				var nextObj=document.getElementById("aa" + iCurrentRow);
				
				if(nextObj!=null)
				{
					
					if(nextObj.getAttribute("onclick")!=null
						&&nextObj.getAttribute("onclick")!=""
						&&nextObj.getAttribute("onclick")!="undefined")
					{
						
						var strOnclick="";
						strOnclick="" + nextObj.getAttribute("onclick");
						
						strOnclick=strOnclick.substr(strOnclick.indexOf("'"));
						
						strOnclick=strOnclick.substr(1,strOnclick.length-5);
						//alert(strOnclick);
						//return;
						//alert(strOnclick);
						SelectTd(nextObj,strOnclick.replace(/\\/g,""));
					}
				}
				else
					iCurrentRow=iCurrentRow+1;
			}
			
			else
				return false;
		}
		var num = 0;
			var k = -1;
			var selecto = null;
			var g_iFrame = "";
			var g_objID = "";
			

			function keyPressdiv(key, iFrame, ObjID, OrgValue) {
			    //alert(key);
			    if (key == 13) {
			        DivSetVisible(false, 0, 0,'','');
			        return;
			    }
			    if (key != 40 && key != 38)
			        return;
			    g_iFrame = iFrame;
			    g_objID = ObjID;
			    var DivRef = document.getElementById('PopupDiv');
			    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
			        num = DivRef.firstChild.rows.length;
			    }
			    if (num <= 0)
			        return;
			    //alert(key);
			    if (PopupDiv.style.display == "block") {
			        //下
			        if (key == 40) {
			            k++;
			            if (k >= num) {
			                k = -1;
			            }
			        }
			        //上
			        if (key == 38) {
			            k--;
			            if (k < 0) {
			                k = num;
			            }
			        }
			        //alert(k);
			        
			        for (var i = 0; i < num; i++) {
			            mouseOut(DivRef.firstChild.rows[i]);
			        }
			        if (k >= 0 && k < num) {//选定值
			            mouseOver(DivRef.firstChild.rows[k]);
			            selecto = DivRef.firstChild.rows[k].cells[0].innerHTML;
			            SelectV(selecto);
			        }
			        else if(OrgValue.indexOf('=')>0)//原始值
			        {
			            OrgValue=OrgValue.substr(OrgValue.indexOf('=')+1);
			            SelectV(OrgValue);
			        }
			    }
			}

			function innerHTMLdiv(strList) {
			    var DivRef = document.getElementById('PopupDiv');
			    DivRef.innerHTML = strList;
			    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
			        num = DivRef.firstChild.rows.length;
			    }
			}

			
			function SelectV(str) {
			    window.frames[g_iFrame].SetV(g_objID, str);
			}

			function ClickSelectV(str) {
			    window.frames[g_iFrame].SetV(g_objID, str);
			    DivSetVisible(false, 0, 0, '', '');
			}
			function mouseOver(e) {
			    e.style.backgroundColor = "blue";
			    e.style.color = "white";
			    //e.style.fontWeight = "bold";
			}
			function mouseOut(e) {
			    e.style.backgroundColor = "#dedede";
			    e.style.color = "black";
			    //e.style.fontWeight = "normal";
			}
			function DivSetVisible(state, objTop, objLeft, iFrame, ObjID) {
			    var DivRef = document.getElementById('PopupDiv');
			   
			    //alert('DivRef.offsetTop=' + DivRef.offsetTop + ' : document.body.scrollTop=' + document.body.scrollTop);
			    DivRef.style.top = objTop;//+  DivRef.offsetTop; //document.body.scrollTop + event.clientY + 10;
			    DivRef.style.left = objLeft; //document.body.scrollLeft + event.clientX;
			    
			    var IfrRef = document.getElementById('framePopupDiv');
			    if (state) {
			        DivRef.style.display = "block";
			        IfrRef.style.width = DivRef.offsetWidth;
			        IfrRef.style.height = DivRef.offsetHeight;
			        IfrRef.style.top = DivRef.style.top;
			        IfrRef.style.left = DivRef.style.left;
			        IfrRef.style.zIndex = DivRef.style.zIndex - 1;
			        IfrRef.style.display = "block";
			        g_iFrame = iFrame;
			        g_objID = ObjID;
			    }
			    else {
			        DivRef.style.display = "none";
			        IfrRef.style.display = "none";
			    }
			}
		    function window_onload() {
                
            }
		</script>
	</HEAD>
	<body class="ListBody" onload="return window_onload()">
	<%//Response.Write("<br>时间：" + DateTime.Now + ":" + DateTime.Now.Millisecond);
	if(NodeTdTitleList==null || ParentTdTitle==null)
	{
		;
		%>
		<script language="javascript">
			var frm=parent.parent.fset;
			if(frm.rows!="60,*")
			{
				frm.rows=frm.rows.replace("0,*","60,*");
			}
			else
			{
				frm.rows=frm.rows.replace("60,*","0,*");
			}
			
		</script>
		<%
		Response.End();
	}
	%>
		<form id="Form1" method="post" runat="server">
		<div id="Div1" style="border: 1px solid #0000FF; background-position: #F2F5E0; position: absolute; cursor: hand; filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
            color:Black; padding: 4px; background:#F2F5E0; display:none; z-index: 100; width:150px; left:0px; top:0px">
                <div style="width:100%" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">AAAA</div>
                <div style="width:100%" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">bbbb</div>
                <div style="width:100%" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">cccc</div>
                <div style="width:100%" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">dddd</div>
        </div>
        <iframe id="framePopupDiv" style="position: absolute; Display:none; z-index: 99; width: 0; height: 0;
            top: 0; left: 0; scrolling: no;" frameborder="0" src=""></iframe>
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" 
				border="0">
				<TR>
					<TD style="WIDTH: <%if(NodeTdTitleList.Count==0)Response.Write("0%");else Response.Write("20%");%>; " valign="top"  align="center">
						<table height="10%" style="DISPLAY:<%if(NodeTdTitleList.Count==0)Response.Write("none");%>" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td valign="top"  class="div">
									<div id="divFlow" style=" VISIBILITY: visible; OVERFLOW: auto; height:auto;width:auto">
									<TABLE class="ListTable" id="TableOrderBy" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr class="ListHeader" height="20">
											<%--
											<td nowrap style="Cursor:url('../image/cursors/H_IBEAM.CUR');WIDTH:15px" align="center" onclick="TdOrderBy(0,true)">序</td>
											--%>
											<%
											//========================判断是否排序====================
													bool isSort = false;
			
													XmlNode sortAttrNode = null;
													string PositionSize="中,中,80%,80%";
													try
													{
														sortAttrNode =NodeTdTitleList[0].ParentNode.ParentNode.Attributes["SortMethod"];
														XmlNode attrPositionSize=NodeTdTitleList[0].ParentNode.ParentNode.Attributes["PositionSize"];
														if(attrPositionSize!=null&&attrPositionSize.Value.Split(",".ToCharArray()).Length==4)
															PositionSize=attrPositionSize.Value.Trim();
															
													}
													catch
													{
														%>
															没有配置完成;
														
														<%
													}
													if(sortAttrNode != null)
													{
														isSort = sortAttrNode.Value=="0" ? false : true;
													}
													//if(isSort)
													{
													   // NodeTrBodyList = SortNodeList(NodeTrBodyList);
													   myArrayList =  SortNodeList(NodeTrBodyList, isSort);
													}
												
											//-----------------------------End-------------------------
												
												if(ParentTdTitle != null)
												{
													string displayWidth;
													
													for(int i=0; i<ParentTdTitle.Count; i++)
													{
														displayWidth = ParentTdTitle[i].SelectSingleNode("Query/@DisplayLength").InnerXml;
														int iColumnWidth=1;
														try
														{
															iColumnWidth=Int32.Parse(displayWidth);
														}
														catch
														{
															iColumnWidth=displayWidth.Length;
														}
														%>
															<td nowrap align="center"  style="Cursor:url('../image/cursors/H_IBEAM.CUR'); Width:<%=iColumnWidth*12+7%>px"><%=ParentTdTitle[i].Attributes.GetNamedItem("ColumnCName").Value%></td>
														<%
													}
													
												}
												%> 
												
											<%
											//int iCount=0;
											
											int iPages=0;
											
											int iCalculator=0;
											
											string xpathFirstRow="";
											
											string strKeyIndex="";
											
											int iOrderByLocation=0;
											
											int BlankTableRows=iPageSize;

                                            							    
											
											foreach(XmlNode myNode in NodeTdTitleList){
												string strColumnCName=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
												string strColumnEName=myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
												string strColumnType=myNode.Attributes.GetNamedItem("ColumnType").InnerXml;
												
												string strColumnWidth=myNode.SelectSingleNode("Query/@DisplayLength").InnerXml;
												int iColumnWidth=1;
												try
												{
													iColumnWidth=Int32.Parse(strColumnWidth);
												}
												catch
												{
													iColumnWidth=strColumnCName.Length;
												}
											
												iOrderByLocation++;	
												string strKeyIndex1=myNode.Attributes.GetNamedItem("KeyIndex").InnerXml;
												if(strKeyIndex1=="Yes")
													strKeyIndex=strColumnEName;

                                                string orderbyType = "0";
                                                string strOrderByTitle="";
                                                foreach (string eachOrderby in OrderbyENamesCollection)
                                                {
                                                    if (eachOrderby.IndexOf("(") < 0 && eachOrderby == strColumnEName)
                                                        strOrderByTitle = "正序";
                                                    else if (eachOrderby.IndexOf("(") > 0)
                                                    {   
                                                        if(eachOrderby == strColumnEName+"(0)")
                                                            strOrderByTitle = "正序";
                                                        else if (eachOrderby == strColumnEName + "(1)")
                                                            strOrderByTitle = "倒序";
                                                            
                                                    }
                                                }
                                                
                                                %>
												<td nowrap align="center"  title="<%=strOrderByTitle%>" 
												style="Cursor:url('../image/cursors/H_IBEAM.CUR'); Width:<%=iColumnWidth*12+7%>px;<%=(strOrderByTitle=="倒序")?"text-decoration:underline":""%>"
													onclick="TdOrderByAll(this,'<%=strColumnEName%>')"><%=strColumnCName%></td><%//TdOrderBy(<%=iOrderByLocation-1s%dfs>,true,'sdf<s%=sdfstrColumnType%ds>',sthis) %>
											<%
											}
                                            if (ButtonsLocation == "in")
                                            {
                                                string iColspanButton = "1";
                                                if (buttModify && buttDelete)
                                                    iColspanButton = "2";
                                                string strAddName = (buttAdd) ? "<IMG SRC=\"../images/icons/0013_b.gif\"><a href=\"#\" onclick=\"fireAdd('" + PositionSize + "')\">新增</a>" : "";
                                                if (buttAdd && (buttModify && buttDelete))
                                                    strAddName = "操作" + strAddName;
                                                //buttAdd = true; buttModify = true; buttDelete = true;
                                                if(buttAdd||buttModify||buttDelete)
                                                    Response.Write("<td nowrap colspan=" + iColspanButton + ">"+strAddName+"</td>");
                                            }
											%>
											
										</tr>
										<%
										
										if(NodeTrBodyList!=null)
										{
											/*
											try
											{
												iCount=NodeTrBodyList.Count;
											}
											catch
											{
												Response.Write("</table>输入日期条件,或数字条件有误");
												Response.End();
											}
											*/
										iPages=iCount/iPageSize;
										iPages++;
										if((int)iCount/iPageSize==(double)iCount/iPageSize)
											iPages--;
										//if(iPages>0)
										//	iPageBegins=1;
										
										//补空表格行
										
											
										//foreach(XmlNode myTrNode in NodeTrBodyList)
										foreach(XmlNode myTrNode in  myArrayList)
										{
											//翻页
											if(XmlDBType)
											{
												if(iCalculator<(iPageBegins-1)*iPageSize||iCalculator>=(iPageBegins)*iPageSize)
												{
													iCalculator++;
													continue;
												}
											}
											iCalculator++;	
											BlankTableRows--;
											
											if(strKeyIndex=="")
												strKeyIndex=myTrNode.SelectSingleNode("td").Attributes.GetNamedItem("Column").InnerXml;
											XmlNode nodeKeyIndex=myTrNode.SelectSingleNode("td[@Column='"+strKeyIndex+"']");
											string strItem="";
											if(nodeKeyIndex!=null)
												strItem=nodeKeyIndex.InnerXml;
											
											
											
										%>
											<tr id="aa<%=iCalculator%>" class="" bgcolor="<%if(xpathFirstRow==""){%>skyblue<%}else{%>Transparent<%}%>" height="20" style="cursor:hand"
												
												onclick="SelectTd(this,'<%=CollectAssessorXPath(myTrNode).Replace("'","\\'")%>')" 
												<%if(isDBClick){%>
												ondblclick="SelectTddb('','<%=PositionSize%>',<%=dblClickRows.ToString().ToLower() %>,'')"
												<%}%>
												onmouseover="this.style.color= 'red';" 
												onmouseout="this.style.color= 'black';"
												onkeydown="NavigateRow(<%=iCalculator%>);">
												<%--去掉序<td nowrap><%=iCalculator</td> --%>
												
												<% //得到数据
												string strParentData; 
												for(int i=0; i<ParentTdTitle.Count; i++)
													{
														strParentData = ParentDataInChild(ParentTdTitle[i], myTrNode);
														
														//=================列宽度设置============
															
														string strColumnWidth=ParentTdTitle[i].SelectSingleNode("Query/@DisplayLength").InnerXml;
														int iColumnWidth=1;
														try
														{
															iColumnWidth=Int32.Parse(strColumnWidth);
														}
														catch
														{
															iColumnWidth=strParentData.Length;
														}	
														System.Text.Encoding en=System.Text.Encoding.GetEncoding("GB2312");
														String str=strParentData;
														str=Regex.Replace(str,"(&)[Nn][Bb][Ss][Pp](;)","");
														
														
														bool bLong=false;
														while(en.GetByteCount(str)>iColumnWidth*2)
														{
															str=str.Substring(0,str.Length-1);
															bLong=true;
														}
														if (bLong)
														{
															//最后为两个字符ASCII
															if(en.GetByteCount(str.Substring(str.Length-2))==2)
																str=str.Substring(0,str.Length-2) + "..";
															
															//最后为两个汉字UNICODE
															else if (en.GetByteCount(str.Substring(str.Length-2))==4)
																str=str.Substring(0,str.Length-1) + "..";
															
															//最后为一个字符ASCII＋一个汉字UNICODE
															else if (en.GetByteCount(str.Substring(str.Length-2))==3)
															{
																if (en.GetByteCount(str.Substring(str.Length-1))==1)
																	str=str.Substring(0,str.Length-1) + ".";
																else if (en.GetByteCount(str.Substring(str.Length-1))==2)
																	str=str.Substring(0,str.Length-1) + "..";
																
															}
																	
														}
														//====================End================
														
														if(str=="") str="&nbsp;";
														%>
															
															<td title="<%=strParentData%>"><%=str%></td>
														<%
													}
												%>
												<%
                                                    foreach (XmlNode myNode in NodeTdTitleList)
                                                    {

                                                        string strColumnType = "1";
                                                        //if(myNode.Attributes.GetNamedItem("ColumnType")!=null)
                                                        strColumnType = myNode.Attributes.GetNamedItem("ColumnType").InnerXml;

                                                        string strColumnEName = myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                                                        string strColumnCName = myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                                                        XmlNode ThisNode = myTrNode.SelectSingleNode("td[@Column='" + strColumnEName + "']");
                                                        string strData = "&nbsp;";
                                                        if (ThisNode != null)
                                                            strData = ThisNode.InnerXml;

                                                        string strColumnWidth = myNode.SelectSingleNode("Query/@DisplayLength").InnerXml;
                                                        int iColumnWidth = 1;
                                                        try
                                                        {
                                                            iColumnWidth = Int32.Parse(strColumnWidth);
                                                        }
                                                        catch
                                                        {
                                                            iColumnWidth = strColumnCName.Length;
                                                        }

                                                        //货币方式
                                                        bool myMoneyStyle = false;
                                                        string myMoneySymbol = "";
                                                        XmlNode nodeMoneyStyle = myNode.SelectSingleNode("Query/@MoneyStyle");
                                                        {
                                                            if (nodeMoneyStyle != null)
                                                            {
                                                                myMoneyStyle = (nodeMoneyStyle.Value == "Yes") ? true : false;
                                                                if (myMoneyStyle)
                                                                {
                                                                    myMoneySymbol = myNode.SelectSingleNode("Query/@MoneySymbol").InnerXml;
                                                                }
                                                            }
                                                        }


                                                        System.Text.Encoding en = System.Text.Encoding.GetEncoding("GB2312");
                                                        String str = strData;
                                                        str = Regex.Replace(str, "(&)[Nn][Bb][Ss][Pp](;)", "");


                                                        bool bLong = false;
                                                        while (en.GetByteCount(str) > iColumnWidth * 2)
                                                        {
                                                            str = str.Substring(0, str.Length - 1);
                                                            bLong = true;
                                                        }
                                                        if (bLong)
                                                        {
                                                            //最后为两个字符ASCII
                                                            if (en.GetByteCount(str.Substring(str.Length - 2)) == 2)
                                                                str = str.Substring(0, str.Length - 2) + "..";

                                                            //最后为两个汉字UNICODE
                                                            else if (en.GetByteCount(str.Substring(str.Length - 2)) == 4)
                                                                str = str.Substring(0, str.Length - 1) + "..";

                                                            //最后为一个字符ASCII＋一个汉字UNICODE
                                                            else if (en.GetByteCount(str.Substring(str.Length - 2)) == 3)
                                                            {
                                                                if (en.GetByteCount(str.Substring(str.Length - 1)) == 1)
                                                                    str = str.Substring(0, str.Length - 1) + ".";
                                                                else if (en.GetByteCount(str.Substring(str.Length - 1)) == 2)
                                                                    str = str.Substring(0, str.Length - 1) + "..";
                                                            }
                                                        }
                                                        if (myMoneyStyle)
                                                        {
                                                            try
                                                            {
                                                                str = Convert.ToDouble(str).ToString("#,###");
                                                                //str = myMoneySymbol + " " + str;
                                                            }
                                                            catch { }
                                                        }
                                                        //后处理功能，弹出窗口-----------------------------------------------------------------------{
                                                        string FunctionOnQuery = "";
                                                        if (myNode.SelectSingleNode("Query/@FunctionOnQuery") != null && myNode.SelectSingleNode("Query/@FunctionOnQuery").Value != "")
                                                        {
                                                            FunctionOnQuery = myNode.SelectSingleNode("Query/@FunctionOnQuery").Value;
                                                        }
                                                        if (FunctionOnQuery != "")
                                                        {
                                                            string[] FunctionOnQuerys = FunctionOnQuery.Split("|".ToCharArray());
                                                            string FunctionNames = "";
                                                            string FunctionValues = "";
                                                            string FuncPopUpLoSi="";
                                                            string FunctionUrl = FunctionOnQuerys[0];
                                                            //||姓名,合同编号|true|a.aspx?a=1&b=2|onclick||中,中,80%,80%
                                                            if (FunctionOnQuerys.Length > 7)
                                                            {
                                                                FuncPopUpLoSi=FunctionOnQuerys[7];
                                                                FunctionNames = FunctionOnQuerys[2];
                                                                FunctionUrl = FunctionOnQuerys[4];
                                                                string[] FunctionNamesArray = FunctionNames.Split(",，".ToCharArray());
                                                                foreach (string eachFuncFieldName in FunctionNamesArray)
                                                                {
                                                                    string eachFuncFieldCName="";// = myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
                                                                    //string strColumnCName = myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
                                                                    string eachFuncFieldEName = "";
                                                                    string eachFuncFieldValue = "";
                                                                    XmlNode eachFuncField=myNode.ParentNode.SelectSingleNode("td[@ColumnCName='" + eachFuncFieldName.Trim() + "']");
                                                                    if (eachFuncField != null)
                                                                    {
                                                                        eachFuncFieldEName = eachFuncField.Attributes.GetNamedItem("ColumnEName").InnerXml;
                                                                        eachFuncFieldCName = eachFuncField.Attributes.GetNamedItem("ColumnCName").InnerXml;
                                                                        
                                                                    }
                                                                    XmlNode eachFieldValue=myTrNode.SelectSingleNode("td[@Column='" + eachFuncFieldEName + "']");
                                                                    if (eachFieldValue != null)
                                                                        eachFuncFieldValue = eachFieldValue.InnerXml;
                                                                    FunctionValues += "," + eachFuncFieldValue;
                                                                    if(eachFuncFieldCName.Trim()!="")
                                                                        FunctionUrl = FunctionUrl.Replace("@" + eachFuncFieldCName, eachFuncFieldValue);
                                                                    //str = str + ":" + "@" + eachFuncFieldCName + "=" + eachFuncFieldValue;
                                                                }
                                                                if (FunctionValues.Length > 0)
                                                                    FunctionValues = FunctionValues.Substring(1);
                                                                
                                                                if(FunctionUrl.IndexOf("?")>=0)
                                                                    FunctionUrl += "&";
                                                                else
                                                                    FunctionUrl += "?";
                                                                FunctionUrl += "FunctionNames=" + FunctionNames;
                                                                FunctionUrl += "&FunctionValues=" + FunctionValues;
                                                                
                                                            }
                                                            
                                                            string WindowNameFrm="";
                                                            if (FunctionOnQuerys.Length > 8)
                                                            {
                                                                WindowNameFrm=FunctionOnQuerys[8];
                                                            }
                                                            
                                                            if (FunctionValues.Length > 0)
                                                            {   //urlPop,strPositionSize,bOpen
                                                                if (str == "")
                                                                    str = "&nbsp";
                                                                else
                                                                    str = "<a href=\"#\" onclick=\"JAVAScript:SelectTddb('" + FunctionUrl.Replace("\\","\\\\") + "','" + FuncPopUpLoSi + "',true,'" + WindowNameFrm + "')\">" + str + "</a>";
                                                            }

                                                            //str = str + FunctionUrl;
                                                        }
                                                       
                                                        //后处理功能，弹出窗口-----------------------------------------------------------------------}
                                                        if (strColumnType == "3")//File
                                                        {
														%>
															
															<td title="<%=strColumnCName + "=" + strData%>"><a href="DownLoadFile.aspx?File=<%=strData%>" target="frmDataRun"><%if (strData.Length >= 10) { Response.Write("下载"); }
                                                                                                                                   else
                                                                                                                                   {%><%}%></a><%if (strData.Length < 10)
                                                                                                                                                                                                  {%>&nbsp;<%}%></td>
														<%
                                                        }
                                                        else if (strColumnType == "4") //News
                                                        {
														%>
															
															<td title="<%=strColumnCName + "=" + strData%>"><a href="#" onclick="javascript:BrowseNews('<%=strData%>','<%=Request.QueryString["db"]%>')"><%if (strData.Length >= 10)
                                                                                                                                                              {%>浏览<%}
                                                                                                                                                              else
                                                                                                                                                              {%><%}%></a><%if (strData.Length < 10)
                                                                                                                                                                                                              {%>&nbsp;<%}%></td>
														<%
                                                        }
                                                        else
                                                        {
														%>
														<td title="<%=strColumnCName + "=" + strData%>" style="<%if(myMoneyStyle){%>text-align:right<%}%>"><%Response.Write(str.Trim() == "" ? "&nbsp;" : str);%></td>
														<%
                                                    }

                                                    }
												if(ButtonsLocation=="in")
												{
                                                    if (buttModify)
                                                    {%>
                                                        <td nowrap onclick="JAVAScritp:fireEdit(this,'<%=CollectAssessorXPath(myTrNode).Replace("'","\\'")%>','<%=PositionSize%>')"><IMG SRC="../images/icons/0015_b.gif">修改</td>
												    <%
                                                    }
                                                    if (buttDelete)
                                                    {%>
                                                        <td nowrap onclick="fireDelete(this,'<%=CollectAssessorXPath(myTrNode).Replace("'","\\'")%>')"><IMG SRC="../images/icons/0014_b.gif">删除</td>
												    <%
                                                    }
                                                     if (buttAdd &&!buttModify&&!buttDelete)
                                                    {%>
                                                        <td style="text-align:center">◎</td>
												    <%
                                                    }
                                                }%>
											</tr>
											<!--汇总-->
											<!--汇总完-->
											
											
											<%
											if(xpathFirstRow=="")
											{
											%>
												<!--script language="javascript">SelTD=document.all['aa<%=iCalculator%>'];</script-->
												<script language="javascript">SelTD=document.all['aa<%=iCalculator%>'];SelectTd(document.all['aa<%=iCalculator%>'],'<%=CollectAssessorXPath(myTrNode).Replace("'","\\'")%>')</script>
											<%
											}	
											if(xpathFirstRow=="")
												xpathFirstRow=CollectAssessorXPath(myTrNode);
											
											}
											
										}%>
										<%
											int intButtonsLocation=0;
                                            if (ButtonsLocation == "in")
                                            {
                                                if (buttModify && buttDelete)
                                                    intButtonsLocation = 2;
                                                else if (buttModify || buttDelete|| buttAdd)
                                                    intButtonsLocation = 1;
                                                //else if(buttAdd)
                                            }
											for(int myBlank=0;myBlank<BlankTableRows;myBlank++)
											{
											%>
												<tr height="20" bgcolor="Transparent" style="cursor:not-allowed">
												<%
												try
												{
													for(int myTD=0;myTD<NodeTdTitleList.Count+ParentTdTitle.Count + intButtonsLocation;myTD++){%> <%--更改这个for(int myTD=0;myTD<NodeTdTitleList.Count+1;myTD++)--%>
													<td>&nbsp;</td>
													<%}
												}
												catch{}%>
												</tr>
											<%
											}
										%>
										
										<%if(SumPage=="true"){ %>
										<!--页内汇总信息SumPage-->
										
											<tr class="ListHeader">
												<%
                                                    int iPageInternal = 0;								    
												    for(int i=0; i<ParentTdTitle.Count; i++)
												    {
                                                        if (iPageInternal == 0)
                                                        {
                                                            iPageInternal++;
														    %>
														    <td nowrap>∑本页</td>
														    <%
                                                        }
                                                        else
                                                        {
                                                            %>
														    <td>&nbsp;</td>
														    <%
                                                        }
												    }
													for(int myTD=0; myTD<NodeTdTitleList.Count; myTD++)
													{%>
														<%
															if(NodeTdTitleList[myTD] == null)
															{
																%>
																<td>&nbsp;</td>
																<%
																continue;
															}
																
															if(NodeTdTitleList[myTD].SelectSingleNode("Query[@AllowSum='Yes']") == null)
															{
															     if (iPageInternal == 0)
                                                                {
                                                                    iPageInternal++;
														            %>
														            <td nowrap>∑本页</td>
														            <%
                                                                }
                                                                else
                                                                {
                                                                    %>
														            <td>&nbsp;</td>
														            <%
                                                                }
                                                            }
															else
															{
																XmlNode myTdSumNode;
																double iSum = 0;
																if(NodeTrBodyList!=null)
																{
																	foreach(XmlNode trNode in NodeTrBodyList)
																	{
																		myTdSumNode = trNode.SelectSingleNode(string.Format("td[@Column='{0}']", NodeTdTitleList[myTD].Attributes["ColumnEName"].Value));
																		if(myTdSumNode != null)
																		{
																			try
																			{
																				iSum += double.Parse(myTdSumNode.InnerText.Trim());
																			}
																			catch
																			{
																			}
																		}
																	}
																}
															%>
																<td style="text-align:right">￥<%=iSum.ToString("#,###")%></td>
															<%}%>
													<%
													}
													for(int i=0; i<intButtonsLocation; i++)
												    {
														%>
														<td>&nbsp;</td>
														<%
												    }%>
											</tr>
											
											<!--页内汇总信息（完）-->
											<%}if(SumAll=="true"){ %>
											<!--全部汇总信息-->
										
											<tr class="ListHeader">
												<%
                                                    int iPageInternal = 0;								    
												    for(int i=0; i<ParentTdTitle.Count; i++)
												    {
                                                        if (iPageInternal == 0)
                                                        {
                                                            iPageInternal++;
														    %>
														    <td nowrap>∑全部</td>
														    <%
                                                        }
                                                        else
                                                        {
                                                            %>
														    <td>&nbsp;</td>
														    <%
                                                        }
												    }
													for(int myTD=0; myTD<NodeTdTitleList.Count; myTD++)
													{%>
														<%
															if(NodeTdTitleList[myTD] == null)
															{
																%>
																<td>&nbsp;</td>
																<%
																continue;
															}
																
															if(NodeTdTitleList[myTD].SelectSingleNode("Query[@AllowSum='Yes']") == null)
															{
															     if (iPageInternal == 0)
                                                                {
                                                                    iPageInternal++;
														            %>
														            <td nowrap>∑全部</td>
														            <%
                                                                }
                                                                else
                                                                {
                                                                    %>
														            <td>&nbsp;</td>
														            <%
                                                                }
                                                            }
															else
															{
                                                                string sSumAll = "";
                                                                if (myArrayList != null && myArrayList.Count > 0)
                                                                {
                                                                    XmlNode eachSumNode = (XmlNode)myArrayList[0];
                                                                    sSumAll = eachSumNode.ParentNode.Attributes.GetNamedItem(NodeTdTitleList[myTD].Attributes.GetNamedItem("ColumnEName").Value + "_Sum").Value;
                                                                }
                                                                //    myTrNode.SelectSingleNode("td[@Column='"+strKeyIndex+"']");
                                                                //if(NodeTdTitleList[myTD].ParentNode.ParentNode.Attributes.GetNamedItem(myTD + "_Sum")!=null)
																//    sSumAll=NodeTdTitleList[myTD].ParentNode.ParentNode.Attributes.GetNamedItem(myTD + "_Sum").Value;
                                                                try
                                                                {
                                                                    sSumAll = Convert.ToDouble(sSumAll).ToString("#,###");
                                                                }
                                                                catch { }
															%>
																<td style="text-align:right">￥<%=sSumAll%></td>
															<%}%>
													<%
													}
													for(int i=0; i<intButtonsLocation; i++)
												    {
														%>
														<td>&nbsp;</td>
														<%
												    }%>
											</tr>
											
											<!--全部汇总信息（完）-->
											<%}%>
											
									</TABLE>
									</div>
								</td>
							</tr>
							<tr height="25" style="DISPLAY:<%if(!RunPagers) Response.Write("none");//iPages<=1 || %>">
								<td align="right">
									<table style="padding-top:6px" width="100<%if(DisplayManner=="2")Response.Write("%");%>" height="25" border="0" cellpadding="0" cellspacing="0" class="ListRecord" align=left>
										<tr align=left>
											<td width="2">&nbsp;</td>
											<%iPageBegins=iPageBegins>iPages?iPages:iPageBegins;%>
											<td valign="bottom" nowrap align=left width="5%"><input type=text style="border:#666666 1px solid;" size=2 ONKEYPRESS="if(event.keyCode==13) {RunPage(this.value,<%=iPageSize%>);return;}event.returnValue=IsDigit();" id="txtiPageBegins" value="<%=iPageBegins%>">/<%=iPages%>页(共<%=iCount%>记录)</td>
											<td width="15">&nbsp;</td>
											<td width="24" valign="bottom"><img src="../image/bottom/Previous.jpg" border="0" 
												<%if(iPageBegins>1){%>
													style="Cursor:url('../image/cursors/H_POINT.CUR')"
													title="第<%=iPageBegins-1%>页"
													onclick="RunPage('<%=iPageBegins-1%>',<%=iPageSize%>)"
												<%}
												else
												{%>
													style="Cursor:url('../image/cursors/H_NODROP.CUR')"
												<%}%>
												></td>
										    <%
										        if(PagerStyle=="2"){
                                                    int iBeginsManner = 1;
                                                    int iPagesManner = iPages;
                                                        
                                                    if (iPageBegins > 9 && iPageBegins < iPages - 9){
                                                        iBeginsManner = iPageBegins - 9;
                                                        iPagesManner=iPageBegins+9;
                                                    }
                                                    else if (iPageBegins > 9){
                                                        iBeginsManner = iPageBegins - 9;
                                                        //iPagesManner=iPages+9;
                                                    }
                                                    else if (iPageBegins < iPages - 9)
                                                    {
                                                        iPagesManner = iPageBegins + 9;
                                                    }




                                                    for (int iManner2 = iBeginsManner; iManner2 <= iPagesManner; iManner2++)
                                                    {%>
    											
											    <td align="center" nowrap width="20" valign="bottom" 
											        style="cursor:hand; <%=(iManner2 ==iPageBegins)?"text-decoration: underline;":""%>"  bgcolor="" 
											        onclick="RunPage('<%=iManner2 %>',<%=iPageSize%>)"><%=iManner2 %></td>
    											
											    <%}}%>
											<td width="24" valign="bottom"><img src="../image/bottom/Next.jpg" border="0" 
												<%if(iPages>iPageBegins){%>
													style="Cursor:url('../image/cursors/H_POINT.CUR')"
													title="第<%=iPageBegins+1%>页"
													onclick="RunPage('<%=iPageBegins+1%>',<%=iPageSize%>)"
												<%}
												else
												{%>
													style="Cursor:url('../image/cursors/H_NODROP.CUR')"
												<%}%>
											></td>
											<td width="50%">&nbsp;</td>
											
											 <%
                                                 if (PagerStyle == "2")
                                                 {
                                             %>
                                                <td nowrap width="45" valign="bottom">条/页:</td>
                                                <td align="center" nowrap width="28" valign="bottom" 
											            style="cursor:hand; <%=(iPageSize !=10 && iPageSize !=20 && iPageSize !=30 && iPageSize !=50)?"stext-decoration: underline;":""%>"  bgcolor="" 
											            onclick="document.location.href=document.location.href">默认</td>
                                             <%
                                                    int[] iPagesCountManner = new int[]{10,20,30,50};
                                                    
                                                    foreach (int iManner2 in iPagesCountManner)
                                                    {%>
    											
											        <td align="center" nowrap width="20" valign="bottom" 
											            style="cursor:hand; <%=(iManner2 ==iPageSize)?"background-color:lightblue;text-decoration: underline;":""%>"  bgcolor="" 
											            onclick="RunPageSize(<%=iManner2 %>)"><%=iManner2%></td>
        											
											        <%}}%>
										</tr>
									</table>
									 
								</td>
							</tr>
						</table>
						
						<!--Begins,hide-->
							<span style="LEFT: 0px; POSITION: absolute; TOP: 0px;DISPLAY:none">
							<input type="hidden" id="openWinUrl" name="openWinUrl" value="<%if(xpathFirstRow==""){Response.Write("DataXPath=" + xpathFirstRow);}else{Response.Write("DataXPath=" + xpathFirstRow + "&AssessorXpath=" +xpathFirstRow);}%>">
							<input id="hTableFields" type="hidden" name="hTableFields" value=""> 
							<input type="hidden" id="hQueryCollection" name="hQueryCollection" value="<%=hQueryCollection%>">
							<input type="hidden" id="hPageBegins" name="hPageBegins" value="<%=iPageBegins %>">
							<input type="hidden" id="hPageSize" name="hPageSize" value="<%=iPageSize%>">
							<input type="hidden" id="hiCalculator" name="hiCalculator" value="<%=iCalculator%>">
							<input type="hidden" id="hKeyIndex" name="hKeyIndex" value="<%=strKeyIndex%>">
							<input type="hidden" id="ColumnOrderBy" name="ColumnOrderBy" value="<%=ColumnOrderBy %>" />
							
							<!--保存传到InputOneTableMain的参数-->
							<input type="hidden" id="hPara" name="hPara" value="">
							<!--为弹出窗口修改-->
							<input type=hidden  id="hLoaded" value="0">
							
							<%if(xpathFirstRow!=""){%>
							<script language="javascript">
								try{
								parent.document.all["hParentXpath"].value='<%=xpathFirstRow.Replace("'","\\'")%>';
								}
								catch(e){}
								//alert(parent.document.all["hParentXpath"].value);
							</script>
							<%}%>
							
							<%//=Request.Form["hTableFields"]%>
							<input type="hidden" id="hiddenXPath" runat="server" NAME="hiddenXPath">
							<script language="javascript">
							    //alert(document.getElementById("hiddenXPath").value);
							</script>

							
							<%//Response.Write("<br>时间：" + DateTime.Now + ":" + DateTime.Now.Millisecond);%>
							
						<!--右键菜单-->
						
						</span>
						<!--Begins,hide-->
						
						<div onselectstart="return false" id="ie5menu" class="skin0" onMouseover="highlightie5()" onMouseout="lowlightie5()" onClick="jumptoie5();"> 
						<div class="menuitems" url="javascript:history.back();">后退</div> 
						<div class="menuitems" url="javascript:history.forward();">前进</div>
							<script>
							    var frmm = parent.parent.TopBar;
							    if (frmm.document.location.href == "about:blank") {
							        //alert('ok');
							        //document.write("<div class=\"menuitems1\" url=\"javascript:;\">显示\/隐藏管理菜单<\/div>");
							    }
							    else {
							        document.write("<hr><div class=\"menuitems\" url=\"javascript:displayTopBar();\" >显示\/隐藏管理菜单<\/div><hr>");
							        document.write("  <a href=\"#\" onclick=\"document.all['vSource'].value=document.body.innerHTML;document.all['vSource'].style.display='';\">调试\/查看源文件</a>");
							        //document.body.innerHTML
							        //window.location = 'view-source:' + window.location.href;
							    }
							</script>
							
						</div>
						<textarea id="vSource" style="width:100%; height:600px;display:none;"></textarea>
						<script language="JavaScript">
						    //如果当前浏览器是Internet Explorer，document.all就返回真 
						    if (document.all && window.print) {

						        //选择菜单方块的显示样式 
						        ie5menu.className = menuskin;

						        //重定向鼠标右键事件的处理过程为自定义程序showmenuie5 
						        document.oncontextmenu = showmenuie5;

						        //重定向鼠标左键事件的处理过程为自定义程序hidemenuie5 
						        document.body.onclick = hidemenuie5;

						    }


                        </script>
						<%if(NodeTdTitleList.Count==0){ //window.location = 'view-source:' + window.location.href;%>
						<img src="../image/bottom/Previous.jpg" border="0" 
							<%if(iPageBegins>1){%>
								style="Cursor:url('../image/cursors/H_POINT.CUR')"
								title="第<%=iPageBegins-1%>/<%=iCount%>页"
								onclick="RunPage('<%=iPageBegins-1%>',<%=iPageSize%>)"
							<%}
							else
							{%>
								style="Cursor:url('../image/cursors/H_NODROP.CUR')"
							<%}%>
						>
						<img src="../image/bottom/Next.jpg" border="0" 
							<%if(iPages>iPageBegins){%>
								style="Cursor:url('../image/cursors/H_POINT.CUR')"
								title="第<%=iPageBegins+1%>/<%=iCount%>页"
								onclick="RunPage('<%=iPageBegins+1%>',<%=iPageSize%>)"
							<%}
							else
							{%>
								style="Cursor:url('../image/cursors/H_NODROP.CUR')"
							<%}%>
						>
						<%}%>
						
							<%//Response.Write(ShowDoc.OuterXml.Replace(((char)160).ToString(),"&nbsp;"));%>
							<%
							string strTemp=Request.ServerVariables["Query_String"];
							if(strTemp.IndexOf("&AssessorXpath")>-1)
								strTemp=strTemp.Remove(strTemp.IndexOf("&AssessorXpath"),strTemp.IndexOf("&",strTemp.IndexOf("&AssessorXpath")+1)-strTemp.IndexOf("&AssessorXpath"));
							%>
				
					</TD>
				<%if(DisplayManner=="2"){%>
				</TR><TR height="99%">
				<%}%>
				
					<!--判断是否隐藏录入界面-->
					<%
						string iheight="",iwidth="";
						if(HideInput==false)
						{
							iheight="100%";
							iwidth="100%";
						}
						else
						{
							iheight="0";
							iwidth="0";
						}
					%>
				<TD vAlign="top" style="display:<%=HideInput?"none":""%>"><iframe id="Right" name="Right" src="InputOneTableMain.aspx?<%if(xpathFirstRow==""){Response.Write(Request.ServerVariables["Query_String"] + "&DataXPath=" + xpathFirstRow);}else{Response.Write(strTemp +"&DataXPath=" + xpathFirstRow + "&AssessorXpath=" +xpathFirstRow);}%>" frameBorder="0" width="<%=iwidth%>"
							scrolling="auto" height="<%=iheight%>"></iframe></TD>
				
				</TR>
				
			</TABLE><!--end-->

	</form>
	<div id="PopupDiv" style="position: absolute;cursor: hand;filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
        color:Black; padding: 4px; background:#F2F5E0; display:none;z-index: 100">
            <div style="width:100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">AAAA</div>
            <div style="width:100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">bbbb</div>
            <div style="width:100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">cccc</div>
            <div style="width:100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">dddd</div>
    </div>		
	</body>

</HTML>
