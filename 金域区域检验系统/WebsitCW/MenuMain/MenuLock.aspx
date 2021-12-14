<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuLock.aspx.cs" Inherits="OA.MenuMain.MenuLock" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>免单管理</title>
    <link href="Css/style.css" rel="stylesheet" charset="utf-8" />

    <link href="../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">

        var num = 0;
        function GetClientSearch() {
            //键盘事件
            var key = window.event.keyCode;
            if (key != 40 && key != 38 && key != 13) {
                //送检单位关键字
                var clientkey = document.getElementById('txtBox').value;
                PopupDiv.innerHTML = "";
                showimage();
                OA.MenuMain.MenuLock.GetClientListByKey(clientkey, GetCallclientresult);
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
            $('txtbox').value = str1.substring(str1.indexOf(')') + 1);
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
                ig.src = "image/indicator.gif";
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
    </script>

    <script language="javascript" type="text/javascript">
        function GetCookie(name) {
            var arg = name + "=";
            var alen = arg.length;
            var clen = document.cookie.length;
            var i = 0;
            while (i < clen) {
                var j = i + alen;
                if (document.cookie.substring(i, j) == arg)
                    return getCookieVal(j);
                i = document.cookie.indexOf(" ", i) + 1;
                if (i == 0) break;
            }
            return null;
        }

        function getCookieVal(offset) {
            var endstr = document.cookie.indexOf(";", offset);
            if (endstr == -1)
                endstr = document.cookie.length;
            return unescape(document.cookie.substring(offset, endstr));
        }

        function SetCookie(name, value) {
            document.cookie = name + "=" + escape(value)
        } 
    </script>

    <style type="text/css">
        .style1
        {
            width: 84%;
        }
        .div1
        {
            background-color: Blue;
            position: absolute;
            height: 500px;
        }
    </style>
</head>
<body onclick="hidDIV();" onload="document.body.scrollTop=GetCookie('posy')" onunload="SetCookie('posy',document.body.scrollTop)">
    <form id="form1" onkeydown="if (event.keyCode == 13) { return false; }" runat="server">
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
    <table cellpadding="0" width="100%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr valign="top">
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="6">免单管理</font>
                                <input type="hidden" id="labcbid" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <table>
                                    <tr align="left">
                                        <td align="right">
                                            送检单位:
                                        </td>
                                        <td align="right">
                                            <asp:TextBox onkeyup="GetClientSearch();" Width="113px" ID="txtBox" MaxLength="50"
                                                runat="server" CssClass="input_var"></asp:TextBox>
                                        </td>
                                        <td>
                                            条码号:<asp:TextBox runat="server" CssClass="input_text" ID="txtserialno" Width="97px"></asp:TextBox>
                                        </td>
                                        <td>
                                            姓名:<asp:TextBox runat="server" CssClass="input_text" ID="txtname" Width="82px" Height="19px"></asp:TextBox>
                                        </td>
                                        <td>
                                            病历号:<asp:TextBox ID="txtpatno" runat="server" CssClass="input_text" Width="95px"></asp:TextBox>
                                        </td>
                                        <td>
                                            录入时间:<asp:TextBox runat="server" ID="operbegin" CssClass="input_text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                                Width="80px"></asp:TextBox>
                                            -
                                            <asp:TextBox runat="server" ID="txtend" CssClass="input_text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                                Width="83px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label Visible="true" Width="200" runat="server" ID="labmsg" ForeColor="Red"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Width="700"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Width="60" Text="免单类型:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList Width="100" ID="AllFreeType" runat="server" OnSelectedIndexChanged="AllFreeType_SelectedIndexChanged">
                                                            <asp:ListItem>业务免单</asp:ListItem>
                                                            <asp:ListItem>差错免单</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Btn_AllMenu" runat="server" Text="所有单子免单" CssClass="buttonstyle"
                                                            OnClick="Btn_AllMenu_Click" Width="96px" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Btn_AllCancelMenu" runat="server" Text="所有单子解单" CssClass="buttonstyle"
                                                            OnClick="Btn_AllCancelMenu_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            &nbsp; &nbsp;
                                            <asp:DataGrid ID="nrequestform" runat="server" Style="cursor: hand; padding: 0px;
                                                margin: 0px; font-size: 12px" PageSize="20" Font-Size="Smaller" BorderWidth="1px"
                                                CellPadding="3" BorderStyle="None" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                                BorderColor="BlanchedAlmond" Width="100%" OnItemCreated="nrequestform_ItemCreated"
                                                OnPageIndexChanged="nrequestform_PageIndexChanged">
                                                <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                </HeaderStyle>
                                                <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="95%" BackColor="Silver"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <table id="header" width="100%" style="cursor: hand; padding: 0px; margin: 0px; font-size: 12px"
                                                                cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="center" style="width: 1%">
                                                                    </td>
                                                                    <td align="center" style="width: 15%">
                                                                        送检单位
                                                                    </td>
                                                                    <td align="center" style="width: 7%">
                                                                        条码号
                                                                    </td>
                                                                    <td align="right" style="width: 5%">
                                                                        姓名
                                                                    </td>
                                                                    <td align="right" style="width: 3%">
                                                                        性别
                                                                    </td>
                                                                    <td align="right" style="width: 3%">
                                                                        年龄
                                                                    </td>
                                                                    <td align="center" style="width: 6%">
                                                                        录入时间
                                                                    </td>
                                                                    <td align="center" style="width: 30%">
                                                                        项目名称
                                                                    </td>
                                                                    <td align="left">
                                                                        免单类型
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                                font-size: 12px" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 1%">
                                                                        <%# Container.ItemIndex + 1%>
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                        <asp:Label runat="server" ID="labclientno" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.nrequestformno") %>'></asp:Label>
                                                                        <%# DataBinder.Eval(Container, "DataItem.clientname")%>
                                                                    </td>
                                                                    <td align="left" style="width: 7%">
                                                                        <%# DataBinder.Eval(Container, "DataItem.serialno") %>
                                                                    </td>
                                                                    <td align="left" style="width: 5%">
                                                                        <%#DataBinder.Eval(Container, "DataItem.cname") %>
                                                                    </td>
                                                                    <td align="left" style="width: 3%">
                                                                        <%#DataBinder.Eval(Container, "DataItem.sex") %>
                                                                    </td>
                                                                    <td align="left" style="width: 3%">
                                                                        <%#DataBinder.Eval(Container, "DataItem.age") %>
                                                                    </td>
                                                                    <td align="left" style="width: 10%">
                                                                        <%# DataBinder.Eval(Container, "DataItem.operdate")%>
                                                                    </td>
                                                                    <td align="left" style="width: 32%">
                                                                        <asp:Label runat="server" ID="item"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <!--免单类型-->
                                                                        <asp:Label runat="server" ID="isfreetype"></asp:Label>
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" Height="18px" Width="68px">
                                                                            <asp:ListItem>业务免单</asp:ListItem>
                                                                            <asp:ListItem>差错免单</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Button runat="server" Width="25%" CssClass="buttonstyle" ID="BtnFree" Text='此单免单'
                                                                            CommandArgument='<%# Bind("nrequestformno") %>' OnClick="BtnFree_Click" />
                                                                        <asp:Button runat="server" Width="25%" CssClass="buttonstyle" ID="Btn_jCancle" Text='此单解单'
                                                                            CommandArgument='<%# Bind("nrequestformno") %>' OnClick="Btn_jCancle_Click" />
                                                                        <a href="menu_list.aspx?nrequestformno=<%# DataBinder.Eval(Container, "DataItem.nrequestformno") %>&&flag=1"
                                                                            style="text-decoration: underline" target="_blank">列表</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 3%">
                                                                    </td>
                                                                    <td colspan="5">
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
