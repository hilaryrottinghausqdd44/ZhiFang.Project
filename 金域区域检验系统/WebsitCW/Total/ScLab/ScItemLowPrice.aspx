<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScItemLowPrice.aspx.cs"
    Inherits="OA.Total.ScLab.ScItemLowPrice" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>低于最低报价项目列表</title>
    <link href="../../Css/style.css" rel="stylesheet" />

    <script type="text/javascript">
        var num = 0;
        var changename = "";
        function GetClientSearch() {
            //键盘事件
            var key = window.event.keyCode;
            changename = "txtBox";
            if (key != 40 && key != 38 && key != 13) {
                //送检单位关键字
                var clientkey = document.getElementById('txtBox').value;
                PopupDiv.innerHTML = "";
                showimage();
                OA.Total.ScLab.ScItemLowPrice.GetClientListByKey(clientkey, GetCallclientresult);
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
        function GetItemSearch() {
            //键盘事件
            var key = window.event.keyCode;
            changename = "txtitemname";
            if (key != 40 && key != 38 && key != 13) {
                //送检单位关键字
                var clientkey = document.getElementById('txtitemname').value;
                PopupDiv.innerHTML = "";
                showimage();
                OA.Total.ScLab.ScItemLowPrice.GetItemListByItemKey(clientkey, GetCallitemresult);
            }
            else
            { changediv(key); }
        }
        //回调结果
        function GetCallitemresult(result) {
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
            $(changename).value = str1.substring(str1.indexOf(')') + 1);
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
            var txt = window.document.getElementById(changename);
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
            var obj = document.getElementById(changename);
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

                var arr = getOjbPosition();
                var left = arr[0];
                var top = arr[1];
                var obj = document.getElementById(changename);
                document.getElementById('autocompleteAnimatecontainerId').style.top = top - obj.offsetHeight + 7;
                document.getElementById('autocompleteAnimatecontainerId').style.left = left + obj.offsetWidth - 15;
                document.getElementById('autocompleteAnimatecontainerId').style.display = "block";
            }
            else {
                var arr = getOjbPosition();
                var left = arr[0];
                var top = arr[1];
                var obj = document.getElementById(changename);
                var c = document.createElement("DIV");
                c.id = "autocompleteAnimatecontainerId";
                //alert(top + ',' + left + ',' + obj.offsetHeight + ',' + obj.offsetWidth);
                c.style.top = top - obj.offsetHeight + 7;
                c.style.left = left + obj.offsetWidth - 15;
                c.style.zIndex = "10";
                c.style.position = "absolute";
                c.style.display = "block";
                c.style.width = "14";
                ig = document.createElement("IMG");
                ig.id = "autocompleteAnimateImageId";
                ig.src = "../../images/indicator.gif";
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

    <style type="text/css">
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
<body>
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table cellpadding="0" width="98%" cellspacing="0" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                            border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                            <tbody>
                                <tr>
                                    <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                        <font size="4">低于最低报价项目列表</font>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                        <table cellspacing="0" cellpadding="0" bgcolor="#fcfcfc" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        送检单位:
                                                        <asp:TextBox onkeyup="GetClientSearch();" Width="180px" CssClass="input_text" ID="txtBox"
                                                            MaxLength="50" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                            TargetControlID="txtBox" WatermarkText="请输入送检单位" WatermarkCssClass="input_text" />
                                                    </td>
                                                    <td align="left">
                                                        &nbsp; 套餐:
                                                        <asp:DropDownList runat="server" ID="drpcb">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp; 项目名:<asp:TextBox onkeyup="GetItemSearch()" Width="180px" runat="server" CssClass="input_text"
                                                            ID="txtitemname"></asp:TextBox>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                                            TargetControlID="txtitemname" WatermarkText="请输入项目关键字" WatermarkCssClass="input_text" />
                                                    </td>
                                                    <td>
                                                        &nbsp;<asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle"
                                                            OnClick="btnsearchitem_Click" />
                                                    </td>
                                                     <td width="20px">
                                                    </td>
                                                    <td>
                                                    <asp:Button runat="server" Visible="False" CssClass="buttonstyle" ID="btnExport"
                                                    Text="另存为Excel" OnClientClick="showdiv();" OnClick="btnExport_Click"></asp:Button>
                                                    &nbsp;<asp:HyperLink ID="hyl1" runat="server" Visible=false></asp:HyperLink> 
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: Silver" valign="top" width="100%">
                                        <table border="1" cellpadding="0" style="padding: 0px; margin: 0px;" cellspacing="0"
                                            width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="labmsg" ForeColor="Red"></asp:Label>
                                                    <asp:DataGrid ID="dgclient" runat="server" Style="cursor: hand; padding: 0px; margin: 0px;
                                                        font-size: 12px" PageSize="50" Font-Size="Smaller" BorderWidth="1px" CellPadding="3"
                                                        BorderStyle="None" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                                        BorderColor="BlanchedAlmond" Width="100%" OnItemDataBound="dgclient_ItemDataBound"
                                                        OnPageIndexChanged="dgclient_PageIndexChanged">
                                                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                        <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                        <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                        </HeaderStyle>
                                                        <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="套餐名称">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.cbname") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="客户名称">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <a href='../clientitemmanage.aspx?clientno=<%# DataBinder.Eval(Container, "DataItem.clientno") %>&&clientname=<%# DataBinder.Eval(Container, "DataItem.clientname") %>'
                                                                        target="_blank">
                                                                        <%# DataBinder.Eval(Container, "DataItem.clientname") %></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="就诊类型">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.sicktypename") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="项目名称">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <a href='../clientitemmanage.aspx?clientno=<%# DataBinder.Eval(Container, "DataItem.clientno") %>&&clientname=<%# DataBinder.Eval(Container, "DataItem.clientname") %>&&itemname=<%# DataBinder.Eval(Container, "DataItem.itemname") %>'
                                                                        target="_blank">
                                                                        <%# DataBinder.Eval(Container, "DataItem.itemname") %></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="项目折扣价">
                                                                <HeaderStyle HorizontalAlign="Center" Width="12%" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.itemagioprice") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="项目底价">
                                                                <HeaderStyle HorizontalAlign="Center" Width="12%" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.lowprice")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom">
                                                        </PagerStyle>
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
                                <img src="../../images/loading.gif" /><font color="red">Loading...</font>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
