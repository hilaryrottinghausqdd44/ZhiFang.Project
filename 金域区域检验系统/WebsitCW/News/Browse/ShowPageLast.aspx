<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPageLast.aspx.cs" Inherits="OA.News.Browse.ShowPageLast" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=Catagory%>-信息内容浏览</title>
    <asp:Literal runat="server" ID="litcss"></asp:Literal>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onload() {

        }
        function openWinPositionSizeF(url, ileft, itop, iwidth, iheight) {
            //alert(" " + window.screen.availWidth + "," + ileft + "," + itop + "," + iwidth + "," + iheight);
            //var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
            var mywin = window.open(url, "_blank", "toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top=" + itop + ",left=" + ileft + ",width=" + iwidth + ",height=" + iheight + ",scrollbars=yes");
        }

        function openWinPositionSize(url, strPositionSize) {
            //var strPositionSize=openPositionSize.value;
            var arrPositionSize = strPositionSize.split(",");
            var PositionLeft = window.screen.availWidth * .1;
            var PositionTop = window.screen.availHeight * .1;
            var PositionWidth = window.screen.availWidth * .8;
            var PositionHeight = window.screen.availHeight * .8;

            if (arrPositionSize.length == 4) {
                try {
                    var iPositionLeft = arrPositionSize[0];
                    var iPositionTop = arrPositionSize[1];
                    var iPositionWidth = parseFloat(arrPositionSize[2]);
                    //alert(Math.round(iPositionWidth));
                    var iPositionHeight = parseFloat(arrPositionSize[3]);
                    switch (arrPositionSize[0]) {
                        case "左":
                            PositionLeft = 0;
                            break;
                        case "中":
                            PositionLeft = window.screen.availWidth * (100 - iPositionWidth) / 200;
                            PositionLeft = Math.round(PositionLeft);
                            //alert(PositionHeight);
                            break;
                        case "右":
                            PositionLeft = window.screen.availWidth * (100 - iPositionWidth) / 100;
                            PositionLeft = Math.round(PositionLeft);
                            break;
                        default:
                            try {
                                PositionLeft = parseInt(iPositionLeft);
                            }
                            catch (e) { }
                            break;
                    }
                    switch (arrPositionSize[1]) {
                        case "上":
                            PositionTop = 0;
                            break;
                        case "中":
                            PositionTop = window.screen.availHeight * (100 - iPositionHeight) / 200;
                            PositionTop = Math.round(PositionTop);
                            break;
                        case "下":
                            PositionTop = window.screen.availHeight * (100 - iPositionHeight) / 100;
                            PositionTop = Math.round(PositionTop);
                            break;
                        default:
                            try {
                                PositionTop = parseInt(iPositionTop);
                            }
                            catch (e) { }
                            break;
                    }

                    switch (arrPositionSize[2]) {
                        default:
                            try {
                                PositionWidth = window.screen.availWidth * iPositionWidth / 100;
                                PositionWidth = Math.round(PositionWidth);
                            }
                            catch (e) { }
                            break;
                    }

                    switch (arrPositionSize[2]) {
                        default:
                            try {
                                PositionHeight = window.screen.availHeight * iPositionHeight / 100;
                                PositionHeight = Math.round(PositionHeight);
                            }
                            catch (e) { }
                            break;
                    }
                }
                catch (e) {
                    alert(e);
                }
            }
            //alert(para);
            //openWin('addOpenTable.aspx?btnid=viewinfo&'+para,window.screen.availWidth,window.screen.availHeight);
            openWinPositionSizeF(url, PositionLeft, PositionTop, PositionWidth, PositionHeight);
        }
// ]]>
    </script>
</head>
<body onload="return window_onload()">
    <form id="form1" runat="server">
    <table width="912" border="0" align="center" cellpadding="0" cellspacing="0" class="tablebg">
        <tr>
            <td class="tablebgtd">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="860" height="286" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="120" align="center" class="font-title">
                            <%if (dt==null || dt.Rows.Count == 0) { Response.Write("没有指定的信息，请检查"); Response.End(); }%><%=dt.Rows[0]["title"].ToString()%>
                            <hr />
                            <span class="font-titlefu">
                                <%=dt.Rows[0]["buildtime"].ToString()%>
                                <%=dt.Rows[0]["source"].ToString()%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;点击率:<%=dt.Rows[0]["hit"].ToString()%></span>
                        </td>
                    </tr>
                    <tr>
                        <td height="300" valign="top" class="font-content">
                            <%=dt.Rows[0]["text"].ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td height="30">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="80" align="center">
                            <table width="96%" border="0" cellspacing="0" cellpadding="0" class="fujian">
                                <tr>
                                    <td height="25" colspan="4" class="filetdbg">
                                        附件
                                    </td>
                                </tr>
                                <tr>
                                    <td height="10" colspan="4" bgcolor="#ace2ff">
                                    </td>
                                </tr>
                                <% if (atatchsEach.Length == 0)
                                   {%>
                                <tr>
                                    <td align="center" class="font-titlefu">
                                        没有附件可供下载
                                    </td>
                                </tr>
                                <% }%>
                                <%else
                                    { %>
                          
                                    <%for (int i = 1; i < atatchName.Length; i++)
                                      {
                                          if (i % 4 == 1 || i == 1)
                                          {
                                    %>
                                    <tr>
                                        <%} %>
                                        <td align="center" class="font-titlefu">
                                            <a href="downloadattach.aspx?file=<%=atatchName[i]%>" class="lian">
                                                <%=ataNamRoad[i]%>
                                            </a>
                                        </td>
                                        <%if (i % 4 == 0 || atatchName.Length <= 4)
                                          {%>
                                    </tr>
                                    <%} %>
                                    <% } 
                                    %>
                               
                                <%} %>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tablebgtd">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
