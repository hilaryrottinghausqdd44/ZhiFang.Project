<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_list.aspx.cs" Inherits="OA.MenuMain.menu_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>免单详细表</title>
    <link href="Css/style.css" rel="stylesheet" />

    <script src="JS/calendar1.js" charset="gb2312"></script>

    <script type="text/javascript">
        var checkFlag = true;
        function ChooseAll() {
            //if( !document.all("CheckAll").Checked ) // 全选　
            if (checkFlag) // 全选　
            {
                var inputs = document.all.tags("INPUT");
                for (var i = 0; i < inputs.length; i++) // 遍历页面上所有的 input 
                {
                    if (inputs[i].type == "checkbox" && inputs[i].id != "CheckAll") {
                        inputs[i].checked = true;
                    }
                }
                checkFlag = false;
            }
            else  // 取消全选
            {
                var inputs = document.all.tags("INPUT");
                for (var i = 0; i < inputs.length; i++) // 遍历页面上所有的 input 
                {
                    if (inputs[i].type == "checkbox" && inputs[i].id != "CheckAll") {
                        inputs[i].checked = false;
                    }
                }
                checkFlag = true;
            }
        }
    </script>

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
                MenuMain.MenuLock.GetClientListByKey(clientkey, GetCallclientresult);
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
        .div1
        {
            background-color: Blue;
            position: absolute;
            height: 500px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label Width="180px" runat="server" ID="labmsg" ForeColor="Red" Height="16px"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="选定项执行" Width="97px" OnClick="Button1_Click1"
        CssClass="buttonstyle" />
    <div>
        <asp:DataGrid ID="dgclientcombo" runat="server" CellPadding="4" BackColor="White"
            BorderWidth="1px" Width="90%" BorderStyle="None" BorderColor="#3366CC" AutoGenerateColumns="False">
            <FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
            <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
            <ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
            <HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#990000"></HeaderStyle>
            <Columns>
                <asp:TemplateColumn>
                    <HeaderStyle Width="3%" BackColor="Silver"></HeaderStyle>
                    <HeaderTemplate>
                        <input id="CheckAll" name="CheckAll" type="checkbox" onclick="ChooseAll()">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="CB_item" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle Width="3%" BackColor="Silver"></HeaderStyle>
                    <ItemTemplate>
                        <%# Container.ItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="编号">
                    <HeaderStyle Width="5%" Height="5%" BackColor="Silver"></HeaderStyle>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem,"nrequestitemno") %>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="项目名称">
                    <HeaderStyle Width="15%" Height="5%" BackColor="Silver"></HeaderStyle>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "cname")%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="折扣价格">
                    <HeaderStyle Width="5%" Height="5%" BackColor="Silver"></HeaderStyle>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "itemagioprice")%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="免单状态">
                    <HeaderStyle Width="10%" BackColor="Silver"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Button BorderWidth="0" runat="server" Width="100" ID="labstatus" Text='<%#Bind("isfree") %><%#Bind("isfreetype") %>'
                            CommandArgument='<%# Bind("nrequestitemno") %>' />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle Width="10%" BackColor="Silver"></HeaderStyle>
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem>业务免单</asp:ListItem>
                            <asp:ListItem>差错免单</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button runat="server" Width="50" ID="work" Text='<%#Bind("isfree") %>' CommandArgument='<%# Bind("nrequestitemno") %>'
                            OnClick="Button1_Click" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages">
            </PagerStyle>
        </asp:DataGrid>
    </div>
    <div style="text-align: right">
    </div>
    </form>
</body>
</html>
