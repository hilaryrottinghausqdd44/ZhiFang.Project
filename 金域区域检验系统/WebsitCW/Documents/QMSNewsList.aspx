<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QMSNewsList.aspx.cs" Inherits="OA.Documents.QMSNewsList" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Literal runat="server" ID="litcss"></asp:Literal>  

    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .STYLE2
        {
            font-size: 13px;
            font-family: "宋体";
            color: #333333;
        }
        a:link
        {
            color: #333333;
            text-decoration: none;
        }
        a:visited
        {
            color: #333333;
            text-decoration: none;
        }
        a:hover
        {
            color: #FF0000;
            text-decoration: none;
        }
        a:active
        {
            color: #333333;
            text-decoration: none;
        }
        .STYLE3
        {
            font-size: 13px;
            color: #FFFFFF;
            font-weight: bold;
        }
        a.navfont:link
        {
            font-size: 13px;
            color: #477ac3;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont:visited
        {
            font-size: 13px;
            color: #477ac3;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont:hover
        {
            font-size: 13px;
            color: #FF0000;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont:active
        {
            font-size: 13px;
            color: #477ac3;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:link
        {
            font-size: 13px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:visited
        {
            font-size: 13px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:hover
        {
            font-size: 13px;
            color: #FF0000;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:active
        {
            font-size: 13px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        .STYLE6
        {
            color: #FFFFFF;
            font-weight: bold;
        }
        .STYLE7
        {
            font-size: 12px;
        }
        body, td, th
        {
            font-family: 宋体;
            font-size: 13px;
        }
        </style>
        <script src="../includes/js/JsModuleDist.js"></script>
	
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
			
			function openWinPositionSize(url,strPositionSize,NewsID)
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
				//frm1,/OA2008/news/browse/homepage.aspx,ID,{新闻编号};[500,600],/OA2008/news/browse/homepage.aspx,ID,{新闻编号};',

				//debugger;
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
			function PubPrev(url)
			{
			    window.open(url,'','width='+screen.availWidth+',height='+screen.availHeight+',status=1,scrollbars=yes,resizable=yes,top=0,left=0' );	

			}
// ]]>
</script><!--bgColor="#f4ab3f"-->
</head>
<body bottommargin="0" topmargin="0" leftmargin="0" rightmargin="0">
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="style-tableall">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="style-titlepic">
                            &nbsp;
                        </td>
                        <td class="style-titletext">
                            <%=classname%>
                        </td>
                        <td class="style-titlemore">
                            <%if (Request.QueryString["morepar"] != null)
                              { %><a href="<%=morestr%>" target="_blank">more</a><%} %>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="160" align="center" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="style-texttable">
                    <% 
                        for (int i = 0; i < dtOANews.Rows.Count; i++)
                        {
                            string id = dtOANews.Rows[i]["id"].ToString();												
                    %>
                    <tr>
                        <td class="style-textpic">
                            &nbsp;
                        </td>
                        <td align="left" class="style-text">
                            <div style="cursor: hand" onclick="javascript:PubPrev('../news/browse/showpagelast.aspx?id='+<%=id%>);"
                                class="style-text" title="<%=dtOANews.Rows[i]["title"].ToString()%>">
                                <%=CString(dtOANews.Rows[i]["title"].ToString(), CharacterNum)%></div>
                        </td>
                    </tr>
                    <%}	%>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="style-foot1">
                            &nbsp;
                        </td>
                        <td class="style-foot2">
                            &nbsp;
                        </td>
                        <td class="style-foot3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
