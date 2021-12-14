<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientComboList.aspx.cs"
    Inherits="OA.Total.ClientComboList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户套餐列表</title>
    <link href="../Css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../Css/calendar1.js"></script>

    <script type="text/javascript">
        //得到名称为posy的cookie值
        function GetCookie(name) {
            var arg = name + "=";
            var alen = arg.length;
            var clen = document.cookie.length;
            var i = 0;
            while (i < clen) {
                var j = i + alen;
                //判断是否是所指定的key
                if (document.cookie.substring(i, j) == arg)
                    return getCookieVal(j);
                i = document.cookie.indexOf("   ", i) + 1;
                if (i == 0) break;
            }
            return null;
        }
        //得到key的值value
        function getCookieVal(offset) {
            var endstr = document.cookie.indexOf(";", offset);
            if (endstr == -1)
                endstr = document.cookie.length;
            return unescape(document.cookie.substring(offset, endstr));
        }
        //设置当前滑动坐标值到缓存
        function SetCookie(name, value) {
            document.cookie = name + "=" + escape(value)
        }
        function showUserDialog(strurl) {
            SetCookie('posy', document.documentElement.scrollTop + 20);
            var r = window.showModalDialog(strurl, this, 'dialogWidth:800px;dialogHeight:650px;');
            //var r = window.showModelessDialog(strurl, this, 'dialogWidth:800px;dialogHeight:650px;'); 
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                window.location.href = "clientcombolist.aspx";
            }
            //document.documentElement.scrollTop = GetCookie('posy');
        }
        function showOpenWindow(strurl) {
            var toptmp = 50;
            //SetCookie('posy', document.documentElement.scrollTop + 20);
            window.open(strurl, 'neww', 'width=800px,height=650px,top=' + toptmp + ',left=200px,scollbars=yes');
        }
        function reloadpage()
        { window.location.href = "clientcombolist.aspx"; }
        //显示隐藏单位套餐列表
        function selectImg(simg) 
        {
            var imgsrc = simg.src.substring(simg.src.length - 7);
            if (simg.parentNode.parentNode.parentNode.rows[1].cells[1].childNodes[0] != null) {
                if (imgsrc == "sub.gif") {
                    simg.parentNode.parentNode.parentNode.rows[1].cells[1].childNodes[0].style.display = "none";
                    simg.src = simg.src.substring(0, simg.src.length - 7) + 'add.gif';
                }
                else {
                    simg.parentNode.parentNode.parentNode.rows[1].cells[1].childNodes[0].style.display = "";
                    simg.src = simg.src.substring(0, simg.src.length - 7) + 'sub.gif';
                }
            }
        }


        var num = 0;
        function GetClientSearch() {
            //键盘事件
            var key = window.event.keyCode;
            if (key != 40 && key != 38 && key != 13) {
                //送检单位关键字
                var clientkey = document.getElementById('txtBox').value;
                PopupDiv.innerHTML = "";
                showimage();
                OA.Total.ClientComboList.GetClientListByKey(clientkey, GetCallclientresult);
            }
            else
            { changediv(key); }
        }
        //回调结果
        function GetCallclientresult(result) {
            var r = result.value;
            //alert(r);
            if (r != "" && r != null) {
                PopupDiv.innerHTML = r;
                showDIV();
                num = PopupDiv.childNodes.length - 1;
            }
            else
            { hidDIV(); }
            //取得查找的数据
            closeimage();
        }

        function DivSetVisible(state) {
            var DivRef = document.getElementById('PopupDiv');
            var IfrRef = document.getElementById('DivShim');
            if (state) {
                DivRef.style.display = "block";
                IfrRef.style.width = DivRef.offsetWidth;
                IfrRef.style.height = DivRef.offsetHeight;
                IfrRef.style.top = DivRef.style.top + 35;
                IfrRef.style.left = DivRef.style.left;
                IfrRef.style.zIndex = DivRef.style.zIndex - 1;
                IfrRef.style.display = "block";
            }
            else {
                DivRef.style.display = "none";
                IfrRef.style.display = "none";
            }
        }
        function DivSetVisible1(state, top, left, width) {
            //alert('DivSetVisible,top'+top+',left'+left);
            var arr = getOjbPosition();
            var left = arr[0];
            var top = arr[1];
            var DivRef = document.getElementById('PopupDiv');
            //num = DivRef.childNodes.length;
            var IfrRef = document.getElementById('DivShim');
            if (state) {
                DivRef.style.display = "block";
                DivRef.style.top = top;
                DivRef.style.left = left;
                DivRef.style.width = width - 10;
                IfrRef.style.width = DivRef.offsetWidth;
                IfrRef.style.height = DivRef.offsetHeight;
                IfrRef.style.top = DivRef.style.top;
                IfrRef.style.left = DivRef.style.left;
                IfrRef.style.zIndex = DivRef.style.zIndex - 1;
                IfrRef.style.display = "block";

            }
            else {
                DivRef.style.display = "none";
                IfrRef.style.display = "none";
            }
        }
        function SelectV(str) {
            var str1 = str.innerText;
            $('txtBox').value = str1.substring(str1.indexOf(')') + 1);
            DivSetVisible(false);
        }
        function mouseOver(e) {
            e.style.background = "#0080ff";
        }
        function mouseOut(e) {
            e.style.background = "#F2F5EF";
        }


        //
        var k = 0;
        var selecto = null;
        function changediv(key) {
            if (PopupDiv.style.display == "block") {
                //下
                if (key == 40) {
                    k++;
                    if (k >= num) {
                        k = 0;
                    }
                }
                //上
                if (key == 38) {

                    k--;
                    if (k < 0) {
                        k = num - 1;
                    }
                }
                for (var i = 0; i < num; i++) {
                    if (i == k) {
                        PopupDiv.childNodes[i].style.background = "#0080ff";
                        selecto = PopupDiv.childNodes[i];
                    }
                    else {
                        PopupDiv.childNodes[i].style.background = "";
                    }
                }
                if (key == 13) {
                    SelectV(selecto);
                }
            }
        }
        //显示出下拉div
        function showDIV() {
            var txt = window.document.getElementById("txtBox");
            var txtTop = txt.offsetTop + txt.offsetHeight;
            var txtLeft = txt.offsetLeft;
            var txtwidth = txt.offsetWidth;
            if (txt.value == "") {
                hidDIV();
                closeimage();
            }
            else {
                DivSetVisible1(true, txtTop, txtLeft, txtwidth);
            }
        }
        //关闭下拉div
        function hidDIV() {
            DivSetVisible1(false);
        }
        //用于div里记录的上下翻动
        function onkey() {
            //键盘事件
            var key = window.event.keyCode;
            changediv(key);
            if (key == 13) {
                event.keyCode = 9;
            }
        }
        function $(s) {
            return document.getElementById ? document.getElementById(s) : document.all[s];
        }
        //得到输入框坐标
        function getOjbPosition() {
            var arr = new Array();
            var obj = document.getElementById("txtBox");
            var x = obj.offsetLeft;
            var y = obj.offsetTop + obj.offsetHeight;
            while (obj.offsetParent) {
                obj = obj.offsetParent;
                x += obj.offsetLeft;
                y += obj.offsetTop;
            }
            arr[0] = x;
            arr[1] = y;
            return arr;
        }


        //显示图片
        function showimage() {

            if (document.getElementById('autocompleteAnimatecontainerId') && document.getElementById('autocompleteAnimateImageId')) {
                document.getElementById('autocompleteAnimatecontainerId').style.display = "block";
            }
            else {
                var arr = getOjbPosition();
                var left = arr[0];
                var top = arr[1];
                var obj = document.getElementById("txtBox");
                var c = document.createElement("DIV");
                c.id = "autocompleteAnimatecontainerId";
                c.style.top = top - obj.offsetHeight + 7;
                c.style.left = left + obj.offsetWidth - 15;
                c.style.zIndex = "10";
                c.style.position = "absolute";
                c.style.display = "block";
                c.style.width = "14";
                ig = document.createElement("IMG");
                ig.id = "autocompleteAnimateImageId";
                ig.src = "../images/indicator.gif";
                ig.width = "14";
                ig.height = "10";
                document.body.appendChild(c);
                c.appendChild(ig);
            }
        }
        //关闭图片
        function closeimage() {
            var c = document.getElementById('autocompleteAnimatecontainerId');
            if (c) {
                c.style.display = "none";
            }
        }
        function CloseExcel(flag, filename) {
            location.href = "downloadfile.aspx?file=Excel/" + escape(filename);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
           <div id="PopupDiv" style="position: absolute; cursor: hand; filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
        color: Black; padding: 4px; background: #F2F5E0; display: none; z-index: 100">
        <div style="width: 100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">
            AAAA</div>
        <div style="width: 100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">
            bbbb</div>
        <div style="width: 100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">
            cccc</div>
        <div style="width: 100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">
            dddd</div>
        <div style="width: 100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">
            eeeeeee</div>
        <div style="text-align: right; width: 100%" onclick="hidDIV();" onmouseover="mouseOver(this)"
            onmouseout="mouseOut(this)">
            <u>关闭</u></div>
    </div>
    <iframe id="DivShim" src="" scrolling="no" frameborder="0" style="position: absolute;
        top: 0px; left: 0px; display: none; filter=progid: DXImageTransform.Microsoft.Alpha(style=0,opacity=0);">
    </iframe>
    <table cellpadding="0" width="98%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="6">客户套餐管理</font>
                                <input type="hidden" id="labcbid" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <asp:DropDownList runat="server" Visible="false" CssClass="input_var" ID="drpclientarealist">
                                </asp:DropDownList>
                                &nbsp; 送检单位:
                                <asp:TextBox onkeyup="GetClientSearch();" Width="180px" CssClass="input_text" ID="txtBox" MaxLength="50" runat="server"></asp:TextBox>
                                &nbsp;
                                <asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />
                                &nbsp;<asp:Button ID="btnExport" runat="server" Text="导出客户套餐" 
                                    CssClass="buttonstyle" onclick="btnExport_Click" Width="94px" />
                                &nbsp;<asp:Button ID="btnExportClientItemAgio" runat="server" 
                                    Text="导出客户项目价格特殊设置" CssClass="buttonstyle" 
                                    onclick="btnExportClientItemAgio_Click" Width="165px" />
                                &nbsp;<a href="javascript:showOpenWindow('clientcomboaddlist.aspx');">客户套餐批量添加</a>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="labmsg"></asp:Label>
                                            <asp:DataGrid ID="dgclient" runat="server" Style="cursor: hand; padding: 0px; margin: 0px;
                                                font-size: 12px" PageSize="50" Font-Size="Smaller" BorderWidth="1px" CellPadding="3"
                                                BorderStyle="None" AllowPaging="True" BackColor="White" AutoGenerateColumns="False"
                                                BorderColor="BlanchedAlmond" Width="100%"
                                                OnItemCreated="dgclient_ItemCreated" 
                                                OnItemDataBound="dgclient_ItemDataBound" 
                                                onpageindexchanged="dgclient_PageIndexChanged">
                                                <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                </HeaderStyle>
                                                <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="95%" BackColor="Silver"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                                font-size: 12px" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 3%">
                                                                        <%# Container.ItemIndex + 1%>
                                                                    </td>
                                                                    <td style="width: 40%">
                                                                        <asp:Label runat="server" ID="labclientno" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.clientno") %>'></asp:Label>
                                                                        <img src="../css/sub.gif" id="dgimg" onclick="selectImg(this);" />                                                               
                                                                        <asp:Label runat="server" ID="labclientcombocount" ForeColor="Red"></asp:Label>
                                                                        <%# DataBinder.Eval(Container, "DataItem.cname") %>(<%# DataBinder.Eval(Container, "DataItem.clientshowno")%>)
                                                                    </td>
                                                                    <td style="width: 20%">
                                                                        <%# DataBinder.Eval(Container, "DataItem.clientarea") %>
                                                                    </td>
                                                                    <td style="width: 20%">
                                                                        <%# DataBinder.Eval(Container, "DataItem.clientstyle") %>
                                                                    </td>
                                                                    <td style="width: 20%">
                                                                        <a style="display:none" href="javascript:showOpenWindow('ClientContractItem.aspx?clientno=<%# DataBinder.Eval(Container, "DataItem.clientno") %>&clientname=<%# DataBinder.Eval(Container, "DataItem.cname") %>');">
                                                                            客户合同项目</a> &nbsp; <a href="javascript:showUserDialog('ClientComboAdd.aspx?clientno=<%# DataBinder.Eval(Container, "DataItem.clientno") %>&clientname=<%# DataBinder.Eval(Container, "DataItem.cname") %>');">
                                                                                客户套餐编辑</a> 
                                                                                <a href="ClientItemManage.aspx?clientno=<%# DataBinder.Eval(Container, "DataItem.clientno") %>&clientname=<%# HttpUtility.UrlEncode(DataBinder.Eval(Container, "DataItem.cname").ToString(),System.Text.Encoding.GetEncoding("GB2312")) %>" target="_blank">客户项目折扣</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 3%">
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:DataGrid ID="dgclientcombo" runat="server" CellPadding="4" BackColor="White"
                                                                            BorderWidth="1px" Width="100%" AllowPaging="false" BorderStyle="None" BorderColor="#3366CC"
                                                                            AutoGenerateColumns="False">
                                                                            <FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
                                                                            <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                                                            <ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
                                                                            <HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#990000"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle Width="10px" BackColor="Silver"></HeaderStyle>
                                                                                    <ItemTemplate>
                                                                                        <%# Container.ItemIndex+1 %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                 <asp:TemplateColumn HeaderText="就诊类型">
                                                                                    <HeaderStyle Width="100px" BackColor="Silver"></HeaderStyle>
                                                                                    <ItemTemplate>
                                                                                      <asp:Label runat="server" ID="labsicktypeno" Text='<%# DataBinder.Eval(Container.DataItem,"sicktypeno") %>'></asp:Label>
                                                                                        
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="套餐名">
                                                                                    <HeaderStyle Width="100px" BackColor="Silver"></HeaderStyle>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem,"cbname") %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="单位折扣">
                                                                                    <HeaderStyle Width="100px" BackColor="Silver"></HeaderStyle>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem,"clientagio") %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="开始时间">
                                                                                    <HeaderStyle Width="80px" BackColor="Silver"></HeaderStyle>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem,"begindate") %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="结束时间">
                                                                                    <HeaderStyle Width="80px" BackColor="Silver"></HeaderStyle>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem,"enddate") %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                            <PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages">
                                                                            </PagerStyle>
                                                                        </asp:DataGrid>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
