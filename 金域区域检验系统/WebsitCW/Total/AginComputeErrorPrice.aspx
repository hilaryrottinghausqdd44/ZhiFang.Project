<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AginComputeErrorPrice.aspx.cs"
    Inherits="OA.Total.AginComputeErrorPrice" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>计算出错项目价格列表</title>
    <link href="../Css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../Css/calendar1.js"></script>

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
                OA.Total.AginComputeErrorPrice.GetClientListByKey(clientkey, GetCallclientresult);
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
        function SelectAll(tempControl) {
            //将除头模板中的其它所有的CheckBox取反 
            var theBox = tempControl;
            xState = theBox.checked;

            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++)
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id) {
                if (elem[i].checked != xState)
                    elem[i].click();
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
    <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; width: 550; border-left: solid #000000 1px; border-top: solid #000000 1px;
        border-bottom: solid #000000 1px; border-right: solid #000000 1px; cursor: move;"
        cellspacing="0" cellpadding="0" oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
            border-top: #aecdd5 solid 8px; border-left: #aecdd5 solid 4px; border-right: #aecdd5 solid 4px;
            border-bottom: #aecdd5 solid 8px;" border="0">
            <tbody>
                <tr style="color: Red">
                    <td align="left" style="border-bottom: #aecdd5 solid 2px; width: 100;">
                        提示信息:
                    </td>
                    <td align="right" style="border-bottom: #aecdd5 solid 2px;">
                        <a href="#" onclick="closediv()">关闭</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divrmsg" style="word-wrap: break-word; float: left; text-align: left; overflow: hidden;
                            color: #313131;">
                            提示信息</div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="0" width="99%" height="450px" cellspacing="0" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                            border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                            <tbody>
                                <tr>
                                    <td align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                        <font size="6">计算出错项目价格列表</font>
                                        <input type="hidden" id="hidrmsg" runat="server" />
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    送检单位:
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox onkeyup="GetClientSearch();" Width="110px" ID="txtBox" MaxLength="50"
                                                        runat="server" CssClass="input_text"></asp:TextBox>
                                                </td>
                                                <td>
                                                    申请单号:<asp:TextBox runat="server" CssClass="input_text" ID="txtserialno" Width="90px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    姓名:<asp:TextBox runat="server" CssClass="input_text" ID="txtname" Width="50px" Height="19px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    病历号:<asp:TextBox ID="txtpatno" runat="server" CssClass="input_text" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    送检时间:<asp:TextBox runat="server" ID="operbegin" CssClass="input_text" ondblclick="calendar();"
                                                        Width="65px"></asp:TextBox>
                                                    -
                                                    <asp:TextBox runat="server" ID="txtend" CssClass="input_text" ondblclick="calendar();"
                                                        Width="65px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />
                                                </td>
                                                <td>
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
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
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: Silver" valign="top" width="100%">
                                        <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                            cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ForeColor="Red" ID="labmsg"></asp:Label>
                                                    <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 475px">
                                                        <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                            font-size: 12px" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                        </table>
                                                        <asp:DataGrid ID="dgerroritem" runat="server" Style="cursor: hand; padding: 0px;
                                                            margin: 0px; font-size: 12px" PageSize="50" Font-Size="Smaller" BorderWidth="1px"
                                                            CellPadding="3" BorderStyle="None" AllowPaging="True" BackColor="White" AutoGenerateColumns="False"
                                                            BorderColor="BlanchedAlmond" Width="98%" OnItemDataBound="dgcomboitem_ItemDataBound"
                                                            OnPageIndexChanged="dgcomboitem_PageIndexChanged">
                                                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                            <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                            <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                            </HeaderStyle>
                                                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="False" onclick="javascript:SelectAll(this);">
                                                                        </asp:CheckBox>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="chkitem" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="送检单位">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="28%" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        (<asp:Label ID="labclientno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.clientno") %>'></asp:Label>)
                                                                        <asp:Label ID="labclientname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.clientname") %>'></asp:Label>
                                                                        <asp:Label ID="labnrequestitemno" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nrequestitemno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="operdate" HeaderText="送检日期" DataFormatString="{0:d}">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                               
                                                                <asp:TemplateColumn HeaderText="姓名">
                                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.cname") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="病历号">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.patno") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="项目名称">
                                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        (<%# DataBinder.Eval(Container, "DataItem.paritemno") %>)
                                                                        <asp:Label runat="server" ID="labparitemname" Text='<%# DataBinder.Eval(Container, "DataItem.paritemname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="申请单号">
                                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.serialno") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="核收标志">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="6%" BackColor="Silver"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.receiveflag") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom" PageButtonCount="30">
                                                            </PagerStyle>
                                                        </asp:DataGrid>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnsave" Text="重新计算项目价格" CssClass="buttonstyle" OnClick="btnsave_Click" />
                                        &nbsp;<asp:Label runat="server" ID="labresult" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
