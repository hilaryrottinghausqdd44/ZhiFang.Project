<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false " CodeBehind="BusinessClient.aspx.cs"
    Inherits="OA.MenuMain.BusinessClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>业务员管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="Css/style.css" rel="stylesheet" charset="utf-8" />

    <script src="JS/calendar1.js" charset="gb2312"></script>

    <script type="text/javascript">

        function bgChange(obj) {
            obj.bgColor = obj.bgColor == "" ? "#B6DC73" : "";
        }

        function DoIt() {
            form1.submit();
        }
        function openwindow() {
            window.open('AddBussiness.aspx', 'neww', 'width=450px,height=300px,top=200px,left=300px,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');
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
                OA.MenuMain.BusinessClient.GetClientListByKey(clientkey, GetCallclientresult);
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

    <link href="../css/style.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .oo
        {
            border-bottom: 1px solid #1F3F56;
            font-size: 14px;
            font-weight: bold;
            color: #000000;
            background-color: #99CCFF;
            background-image: url('../graphics/bg1.gif');
            background-repeat: no-repeat;
            height: 20px;
            text-indent: 20px;
            vertical-align: middle;
            padding-top: 2px;
            width: auto;
        }
        .SearchBar
        {
            width: 133%;
        }
        .style5
        {
            border-bottom: 1px solid #1F3F56;
            font-size: 14px;
            font-weight: bold;
            color: #000000;
            background-color: #99CCFF;
            background-image: url('../graphics/bg1.gif');
            background-repeat: no-repeat;
            height: 15px;
            text-indent: 20px;
            vertical-align: middle;
            padding-top: 2px;
            width: 959px;
        }
        .style7
        {
            width: 959px;
            height: 14px;
        }
        .style10
        {
            width: 931px;
        }
        .style14
        {
            height: 31px;
        }
        .style15
        {
            width: 98px;
        }
        .style16
        {
        }
        .input_var
        {
            border: solid 1px black;
            font-size: 12px;
            width: 80px;
        }
        .input_text
        {
            height: 18px;
            width: 120px;
            background-color: #E3EFF8;
            border: 1px solid #6199C5;
            font-size: 12px;
            color: #333333;
            text-decoration: none;
        }
        .style18
        {
            width: 482px;
        }
        .style19
        {
            height: 31px;
            width: 482px;
        }
        .style21
        {
            width: 228px;
        }
        .style22
        {
            width: 70px;
        }
        body
        {
            font-size: 12px;
            color: #999999;
        }
        #lySearch
        {
            position: absolute;
            left: 308px;
            top: 178px;
            width: 410px;
            height: 194px;
            z-index: 1;
        }
        .STYLE1
        {
            color: #999999;
            filter: alpha(opacity=40);
            font-weight: bold;
        }
    </style>
</head>
<body onclick="hidDIV();" onload="document.body.scrollTop=GetCookie('posy')" onunload="SetCookie('posy',document.body.scrollTop)">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
    <div id="PopupDiv" style="position: absolute; cursor: hand; filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
        color: Black; padding: 4px; background: #F2F5E0; display: none; z-index: 100">
        <div style="width: 100%" onclick="SelectV(this);" onmouseover="mouseOver(this)" onmouseout="mouseOut(this)">
            AAAA</div>
        <div style="text-align: right; width: 100%" onclick="hidDIV();" onmouseover="mouseOver(this)"
            onmouseout="mouseOut(this)">
            <u>关闭</u></div>
    </div>
    <iframe id="DivShim" src="" scrolling="no" frameborder="0" style="position: absolute;
        top: 0px; left: 0px; display: none; filter=progid: DXImageTransform.Microsoft.Alpha(style=0,opacity=0);">
    </iframe>
    <table style="width: 930px">
        <tbody>
            <tr>
                <td colspan="2" class="style5">
                </td>
            </tr>
            <tr>
                <td valign="bottom" colspan="2" class="style7">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;<span class="style10" style="font-size: 12px">&nbsp; 客户名称：<asp:TextBox onkeyup="GetClientSearch();"
                                            Width="160px" ID="txtBox" MaxLength="100" runat="server" CssClass="input_text"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                TargetControlID="txtBox" WatermarkText="请输入送检单位" WatermarkCssClass="input_text" />
                                        </span>&nbsp;
                                        <asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />
                                    </td>
                                    <td>
                                        <input id="Button2" class="buttonstyle" type="button" onclick="openwindow()" value="新增业务员" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div id="lySearch">
                                <table align="left" width="351" height="67" border="0" cellpadding="0" cellspacing="1"
                                    bgcolor="#999999">
                                    <tr>
                                        <td bgcolor="#fcfcfc">
                                            <div align="center" class="STYLE1">
                                                <img src="../images/loading.gif" /><font color="red">Loading...</font>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    客户列表:
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" Width="489px" Style="cursor: hand; padding: 0px;
                                margin: 0px; font-size: 12px" AutoGenerateColumns="False" BorderColor="BlanchedAlmond"
                                BorderStyle="None" Font-Size="Smaller" OnRowDataBound="GridView1_RowDataBound"
                                OnRowCommand="GridView1_RowCommand" onclick="bgChange(this)" AllowPaging="True"
                                PageSize="25" OnPageIndexChanging="GridView1_PageIndexChanging1">
                                <RowStyle ForeColor="#333333" BackColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField FooterText="ID" ItemStyle-HorizontalAlign="Left" HeaderText="客户编号"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("clientno") %>'></asp:Label>
                                            <asp:Button ID="btnHiddenPostButton" CommandName="HiddenPostButtonCommand" runat="server"
                                                Text="HiddenPostButton" Style="display: none;" />
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px" Font-Size="Small" Width="15%" ForeColor="Black"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" ItemStyle-HorizontalAlign="Left" HeaderText="客户名称"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="Labmoney" runat="server" Text='<%# Bind("cname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px" Width="45%" Font-Size="Small" ForeColor="Black"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" ItemStyle-HorizontalAlign="Left" HeaderText="所属区域"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="area" runat="server" Text='<%# Bind("clientarea") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px" Width="15%" Font-Size="Small" ForeColor="Black"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" ItemStyle-HorizontalAlign="Left" HeaderText="医院级别"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="type" runat="server" Text='<%# Bind("clientstyle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px" Font-Size="Small" ForeColor="Black"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" ItemStyle-HorizontalAlign="Left" HeaderText="业务员"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="businessname" runat="server" Text='<%# Bind("businessname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px" Font-Size="Small" ForeColor="Black"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="Silver" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Silver" ForeColor="#000000" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td valign="top" align="left">
                    业务员列表:
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" Width="367px" AutoGenerateColumns="False"
                                Style="margin-bottom: 0px; margin-top: 0px;" HorizontalAlign="Left" Font-Size="Small"
                                OnRowDataBound="GridView2_RowDataBound">
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <RowStyle ForeColor="#333333" BackColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField FooterText="ID" ItemStyle-HorizontalAlign="Left" HeaderText="编号"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("cbid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" ItemStyle-HorizontalAlign="Left" HeaderText="业务员姓名"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="Labmoney0" runat="server" Text='<%# Bind("cname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" ItemStyle-HorizontalAlign="Left" HeaderText="开始时间"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="begindate" runat="server" Text='<%# Bind("begindate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" ItemStyle-HorizontalAlign="Left" HeaderText="结束时间"
                                        HeaderStyle-Height="12">
                                        <ItemTemplate>
                                            <asp:Label ID="enddate" runat="server" Text='<%# Bind("enddate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="12px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="NoteInfo" HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button runat="server" Width="60" ID="Btn_cancleCheck" Text='删除' CssClass="buttonstyle"
                                                CommandArgument='<%# Bind("cbid") %>' OnClick="Btn_cancleCheck_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Silver" ForeColor="#000000" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
