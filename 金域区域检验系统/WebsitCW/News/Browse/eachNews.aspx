<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Browse.eachNews" CodeBehind="eachNews.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=Catagory%>-信息内容浏览</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Main.css" rel="stylesheet" type="text/css">
    <style type="text/css">
        <!
        -- .style1
        {
            font-size: 10pt;
        }
        -- ></style>

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
			
			function openWinPositionSize(url,strPositionSize)
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
				openWinPositionSizeF(url	,PositionLeft,PositionTop,PositionWidth,PositionHeight);
			}
    // ]]>
    </script>

</head>
<body leftmargin="0" topmargin="5" onload="return window_onload()">
    <table width="664" border="0" align="center">
        <tr>
            <td height="29" class="tdBlack1px">
                <br>
                <h3 align="center">
                    <%if (dt == null || dt.Rows.Count == 0) { Response.Write("没有指定的信息，请检查"); Response.End(); }%><%=dt.Rows[0]["title"].ToString()%></h3>
                <hr width="600">
                <p align="center">
                    <%=dt.Rows[0]["buildtime"].ToString()%>
                    <%=dt.Rows[0]["source"].ToString()%>
                    点击率:<font color="red"><%=dt.Rows[0]["hit"].ToString()%></font> 评论数:<font color="red"><%=dtCount.Rows[0][0].ToString()%></p>
                </font>
                <p align="center" class="style1">
                    <%=dt.Rows[0]["text"].ToString()%></p>
            </td>
        </tr>
        <tr>
            <td>
                <%=(atatchsEach.Length==0)?"没有附件可供下载":"附件：  "%>
                <%for (int i = 0; i < atatchName.Length - 1; i++)
                  { %>
                <a href="downloadattach.aspx?file=<%=atatchName[i]%>">
                    <%=ataNamRoad[i]%>
                </a>
                <%if (i < atatchName.Length - 1)
                  {%>
                |
                <%}%>
                <% } 
                %>
            </td>
        </tr>
        <tr>
            <td>
                <b>发表评论及推荐朋友</b>
            </td>
        </tr>
    </table>
    <table width="664" border="0" align="center" class="tdBlack1px">
        <tr>
            <td>
                <iframe id="ReplyList" width="100%" frameborder="0" src="../../News/publish/ReplyList.aspx?SystemArgs=<%=Request.QueryString["ModuleID"] %>,<%=Server.UrlEncode(Request.RawUrl.ToString()) %>,<%=Server.UrlEncode(dt.Rows[0]["title"].ToString().Replace(",", "|-|"))%>&SystemNameID=新闻系统,<%=Catagory %>,<%=Server.UrlEncode(Request.QueryString["id"].ToString().Replace(",", "|-|")) %>"></iframe>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
</body>
</html>
