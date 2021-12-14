<%@ Page language="c#" Codebehind="OA_News_Listwhcy.aspx.cs" AutoEventWireup="True" Inherits="OA.Documents.OA_News_List_whcy" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>新闻显示列表模式</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css"  href="CSS/<%=cssFile%>"/>
		
	
        <script language="javascript" type="text/javascript">
        // <!CDATA[

        function window_onload() {

        }
         function openWinPositionSizeF(url,ileft,itop,iwidth,iheight)
			{
				//alert(" " + window.screen.availWidth + "," + ileft + "," + itop + "," + iwidth + "," + iheight);
				//var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
				var mywin=window.open(url,"_blank","toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top="+itop +",left="+ileft +",width="+iwidth+",height="+iheight+",scrollbars=yes");
			}

			function openWinPositionSize(url, strPositionSize, NewsID)
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
				//alert(para);
				//openWin('addOpenTable.aspx?btnid=viewinfo&'+para,window.screen.availWidth,window.screen.availHeight);
				//openWinPositionSizeF(url, PositionLeft, PositionTop, PositionWidth, PositionHeight);
				
				if ('<%=Request.QueryString["OutPutParaUrl"]%>' == '') {
				    openWinPositionSizeF(url, PositionLeft, PositionTop, PositionWidth, PositionHeight);
				}
				else {
				    ModuleDist('<%=Request.QueryString["ModuleArgPrv"]%>',
                   '/oa2008/documents/NewsList.aspx?id=首页&NewsNum&moreurl=/OA/news/browse/CategoryNews.aspx',
                   '新闻编号',
                  NewsID,
                  '<%=Request.QueryString["OutPutParaUrl"]%>',
                  '',
                  '',
                   false);
				}
				return false;
			}
// ]]>
</script>
</head>
	<body MS_POSITIONING="GridLayout" bottommargin="0" topmargin="0" leftmargin="0" rightmargin="0" onload="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" class="style-tableall">
				<tr>
					<td>
						<table  width="100%" class="style-titletable" border="0" cellspacing="0" cellpadding="0">
							<tr>
							    <td width="13"><div class="style-titlepic"></div></td>
								<td nowrap><div class="style-titletext"><%=classname%></div></td>
								<!--<td><%if (Request.QueryString["morepar"] != null){ %><a href="<%=morestr%>" class="style-titlemore" target="_blank"></a><%} %></td>-->
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="left">
						<table class="style-texttable" border="0" cellspacing="0" cellpadding="0"  width="100%">
						<% 
			if(dtOANews != null && dtOANews.Rows.Count > 0)
            {			    
    for (int i = 0; i < dtOANews.Rows.Count; i++)
    {
        string id = dtOANews.Rows[i]["id"].ToString();												
						%>
							<tr>
							    <td width="13" nowrap>
								<div class="style-textpic"></div></td>
								<td nowrap><div ><a href="#" onClick="openWinPositionSize('../news/browse/homepageNologin.aspx?id='+<%=id%>,'<%=strWinPositionSize%>','<%=id%>' );" 
								class="style-text" title="<%=dtOANews.Rows[i]["title"].ToString()%>"><%=StringLength(dtOANews.Rows[i]["title"].ToString(), CharacterNum)%></a></div>
								</td>
							</tr>
						<%}}	%>
						</table>
						
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
