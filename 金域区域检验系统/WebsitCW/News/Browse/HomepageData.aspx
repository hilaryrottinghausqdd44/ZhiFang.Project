<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.News.Browse.HomepageData" Codebehind="HomepageData.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=Catagory%>
        -信息内容浏览</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Main.css" rel="stylesheet" type="text/css">

<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
.STYLE2 {
	font-size: 13px;
	font-family: "宋体";
	color: #333333;
}
a:link {
	color: #0000CC;
	
}
a:visited {
	color: #993300;
}
a:hover {
	color: #FF0000;
}
a:active {
	color: #FF0000;
}
.STYLE3 {
    font-size: 13px;
	color: #FFFFFF;
	font-weight: bold;
}

a.navfont:link {font-size: 13px;
	color: #477ac3;
	text-decoration: none;font-weight: bold;
}
a.navfont:visited {font-size: 13px;
	color: #477ac3;
	text-decoration: none;font-weight: bold;
}
a.navfont:hover {font-size: 13px;
	color: #FF0000;
	text-decoration: none;font-weight: bold;
}
a.navfont:active {font-size: 13px;
	color: #477ac3;
	text-decoration: none;font-weight: bold;
}


a.navfont1:link {font-size: 13px;
	color: #FFFFFF;
	text-decoration: none;font-weight: bold;
}
a.navfont1:visited {font-size: 13px;
	color: #FFFFFF;
	text-decoration: none;font-weight: bold;
}
a.navfont1:hover {font-size: 13px;
	color: #FF0000;
	text-decoration: none;font-weight: bold;
}
a.navfont1:active {font-size: 13px;
	color: #FFFFFF;
	text-decoration: none;font-weight: bold;
}
.STYLE6 {
	color: #FFFFFF;
	font-weight: bold;
}
.STYLE7 {font-size: 12px}
body,td,th {
	font-family: 宋体;
	font-size: 13px;
}

-->
</style>
    <script language="javascript">
	
			
			var objLast=null;
			var parentSet="";
			var topSet="";
			var mainSet="";
			function ZoomAll(obj)
			{
				try
				{
					if(obj.src.indexOf("image/cursors/ico_zoomin.gif")>0)
					{
						obj.src="image/cursors/ico_zoomall.gif";
						
						var frm=parent.fset;
						parentSet=frm.cols;
						frm.cols="0,0,*";
						
					//	var frmTop=top.fset;
					//	topSet=frmTop.cols;
					//	frmTop.cols="0,0,*";
						
						var frmMain=top.fsetMain;
						mainSet=frmMain.rows;
						frmMain.rows="0,*";
					}
					else
					{
						obj.src="image/cursors/ico_zoomin.gif";
						
						var frm=parent.fset;
						frm.cols="209,0,*";
						
						//var frmTop=top.fset;
						//frmTop.cols=topSet;
						
						var frmMain=top.fsetMain;
						frmMain.rows="100,81%";
					}
				}
				catch(e)
				{
				
				}
			}

            function openWinPositionSizeF(strUrl,ileft,itop,iwidth,iheight)
			{
				//alert(" " + window.screen.availWidth + "," + ileft + "," + itop + "," + iwidth + "," + iheight);
				//var mywin=window.open(strUrl,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
				var mywin=window.open(strUrl,"_blank","toolbar=no,location=yes,status=yes,menubar=no,resizable=yes,top="+itop +",left="+ileft +",width="+iwidth+",height="+iheight+",scrollbars=yes");
			}
			
			function openWinPositionSize(strUrl,strPositionSize)
			{
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
	            //window.confirm(strUrl);
	            //oncontextmenu="JAVAScript:alert('<%=Request.ServerVariables["Query_String"]%>')"
				openWinPositionSizeF(strUrl,PositionLeft,PositionTop,PositionWidth,PositionHeight);
			}
    </script>

</head>
<body class="daohang1Body" style="margin-top:0px">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td colspan="2">
                <%=strContent%>
            </td>
        </tr>
        <tr>
            <td>
                <div id="D2">
                </div>
            </td>
            <td>
            </td>
        </tr>
        <tr height="0">
            <td width="90%">
                <!--div id="D1"></div-->
            </td>
            <td width="20">
                <img id="zoomWhole" height="23" width="23" src="./image/cursors/ico_zoom<%
				if(Request.QueryString["WindowSize"]!=null&&Request.QueryString["WindowSize"].ToString().ToUpper()=="MAX"){%>in<%}else{Response.Write("all");}%>.gif"
                    style="cursor: hand" onmousemove="this.style.border='#ccccff 0px outset'" onmouseout="this.style.border='#ccccff 0px outset'"
                    onclick="javascript:ZoomAll(this)">
            </td>

            <script language="javascript">
				 	<%if(Request.QueryString["WindowSize"]!=null&&Request.QueryString["WindowSize"].ToString().ToUpper()=="MAX"){%>
				 	
						ZoomAll(document.all["zoomWhole"]);
					<%}%>
					<%if(Jbig!=true){%>
					document.all.zoomWhole.style.display="none";
					<%}%>
            </script>

        </tr>
    </table>
   
</body>
</html>

<script language="javascript">
	if(window.parent==null||window.parent.name=="")
		document.all.zoomWhole.style.display="none";
	//alert(window.parent.name);
</script>

