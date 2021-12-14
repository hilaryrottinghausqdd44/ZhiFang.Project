<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientComboAddList.aspx.cs"
    Inherits="OA.Total.ClientComboAddList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位套餐批量添加</title>
    <link href="../Css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../Css/calendar1.js"></script>

    <script type="text/javascript">
        function check() {
            var clientagio = document.getElementById('txtclientagio').value;
            if (clientagio == "" || clientagio == null) {
                alert('单位折扣不能为空!');
                document.getElementById('txtclientagio').focus();
                return false;
            }
            var reg = /^\d+(\.\d+)?$/g;
            if (clientagio != "" && reg.test(clientagio) == false) {
                alert('单位折扣必须为数字!');
                document.getElementById('txtclientagio').focus();
                return false;
            }
            if (document.getElementById('txtbegindate').value == "" || document.getElementById('txtbegindate').value == null) {
                alert('开始时间不能为空!');
                document.getElementById('txtbegindate').focus();
                return false;
            }
            var tbclientbegindate = document.getElementById('txtbegindate').value;
            var tbclientenddate = document.getElementById('txtenddate').value;
            //判断开始时间不能大小结束时间
            if (tbclientbegindate.length > 0 && tbclientenddate.length > 0) {
                var reg = new RegExp("-", "g");
                var bvalue = parseInt(tbclientbegindate.replace(reg, ""));
                var evalue = parseInt(tbclientenddate.replace(reg, ""));
                if (bvalue > evalue) {
                    alert('开始时间不能大于结束时间!');
                    document.getElementById('txtbegindate').focus();
                }
            }
            return true;
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
        var falpha;
        falpha = 0;
        function fchange() {
            if (falpha != 90) {
                table1.style.filter = "alpha(opacity=" + falpha + ")";
                falpha = falpha + 10;
                setTimeout("fchange()", 200);
            }
            else {
                falpha = 0;
            }
        }
        function showdiv() {
            table1.style.height = (window.document.body.clientHeight > window.document.body.scrollHeight) ? window.document.body.clientHeight : window.document.body.scrollHeight;
            table1.style.width = "100%";
            table1.style.display = 'block'
            table2.style.left = document.documentElement.scrollLeft + 100;
            table2.style.top = document.body.scrollTop + 100;
            table2.style.display = 'block';
            divrmsg.innerHTML = document.getElementById('hidrmsg').value;
            fchange();
        }

        function closediv() {
            document.getElementById('hidrmsg').value = "";
            table1.style.display = 'none';
            table2.style.display = 'none';
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
                OA.Total.ClientComboAddList.GetClientListByKey(clientkey, GetCallclientresult);
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
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; width: 550; border-left: solid #000000 1px;
        border-top: solid #000000 1px; border-bottom: solid #000000 1px; border-right: solid #000000 1px;
        cursor: move;" cellspacing="0" cellpadding="0" oncontextmenu="return false">
       
        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
            border-top: #aecdd5 solid 8px;border-left: #aecdd5 solid 4px;border-right: #aecdd5 solid 4px; border-bottom: #aecdd5 solid 8px;" border="0">
            <tbody>
                <tr style="color:Red">
                    <td align="left" style="border-bottom: #aecdd5 solid 2px;width:100;">提示信息:</td>
                    <td align="right" style="border-bottom: #aecdd5 solid 2px;">
                      <a href="#" onclick="closediv()">关闭</a>
                    </td>
                </tr>
                <tr><td colspan="2"><div id="divrmsg" style="word-wrap:break-word;float:left;text-align:left;overflow:hidden;color:#313131;">提示信息</div></td></tr>
            </tbody>
        </table>
    </div>
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
    <table cellpadding="0" width="98%" height="600px" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px;border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                单位套餐批量添加
                                <input type="hidden" id="hidrmsg" runat="server" />                                
                                <input type="hidden" id="labcbid" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <asp:DropDownList runat="server" Visible="false" CssClass="input_var" ID="drpclientarealist">
                                </asp:DropDownList>
                                &nbsp; 送检单位:<asp:TextBox onkeyup="GetClientSearch();" Width="180px" CssClass="input_text" ID="txtBox" MaxLength="50" runat="server"></asp:TextBox>&nbsp;
                                <asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="labmsg" ForeColor="Red"></asp:Label>
                                            <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 500px">
                                                <asp:DataGrid ID="dgclient" runat="server" Style="cursor: hand; padding: 0px; margin: 0px;
                                                    font-size: 12px" PageSize="50" Font-Size="Smaller" BorderWidth="1px" CellPadding="3"
                                                    BorderStyle="None" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                                    BorderColor="BlanchedAlmond" Width="100%" 
                                                    OnItemDataBound="dgclient_ItemDataBound" 
                                                    onpageindexchanged="dgclient_PageIndexChanged">
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
                                                                <asp:Literal runat="server" ID="litclientno" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.clientno") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center" Width="95%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                                    font-size: 12px" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 40%">
                                                                           <asp:Literal runat="server" ID="litclientname" Text='<%# DataBinder.Eval(Container, "DataItem.cname") %>'></asp:Literal>(<%# DataBinder.Eval(Container, "DataItem.clientno") %>)
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                            <%# DataBinder.Eval(Container, "DataItem.clientarea") %>
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                            <%# DataBinder.Eval(Container, "DataItem.clientstyle") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
                                                </asp:DataGrid></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                                    <tbody>
                                        <tr>
                                         <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                就诊类型:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:DropDownList runat="server" ID="drpsicktypelist">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                套餐:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:DropDownList runat="server" ID="drpcombolist">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                单位折扣:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:TextBox runat="server" Width="60px" CssClass="input_text" ID="txtclientagio"></asp:TextBox> <font color="red">如:95折应该填9.5</font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                开始时间:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:TextBox runat="server" ID="txtbegindate" CssClass="input_text" onfocus="calendar();"></asp:TextBox>
                                            </td>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                结束时间:
                                            </td>
                                            <td align="left" colspan="1" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:TextBox runat="server" ID="txtenddate" CssClass="input_text" onfocus="calendar();"></asp:TextBox>
                                                <input type="button" id="btnclear" value="清空" class="btn_blue" onclick="document.getElementById('txtenddate').value='';" />为空表示长期有效
                                            </td>
                                            <td colspan="2" style="border-bottom: #aecdd5 solid 1px;"><asp:CheckBox runat="server" ID="chkcomputeprice" />是否重新计算套餐价格</td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:Button runat="server" ID="btn" Text="保 存" CssClass="buttonstyle" OnClick="btn_Click" />&nbsp;&nbsp;<input type="button" id="btnclose" value="取消" class="buttonstyle" onclick="javascript:window.opener.reloadpage();window.close();" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </tbody>
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
