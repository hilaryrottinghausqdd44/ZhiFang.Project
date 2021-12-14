<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScCheckAccount.aspx.cs"
    Inherits="OA.Total.MkLab.ScCheckAccount" %>

<%@ Register TagPrefix="idatagrid" Assembly="WebSiteOA" Namespace="OA.ZCommon.CommonControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生成财务每月对帐</title>
    <link href="../../Css/style.css" rel="stylesheet" />

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
                if (i == 0)
                    break;
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
            document.cookie = name + "=" + escape(value);
        }
        function check() {
            if (document.getElementById('txtoperdatebegin').value == "" || document.getElementById('txtoperdatebegin').value == null) {
                alert('送检日期开始时间不能为空!');
                document.getElementById('txtoperdatebegin').focus();
                return false;
            }
            if (document.getElementById('txtoperdateend').value == "" || document.getElementById('txtoperdateend').value == null) {
                alert('送检日期结束时间不能为空!');
                document.getElementById('txtoperdateend').focus();
                return false;
            }

            var tbclientbegindate = document.getElementById('txtoperdatebegin').value;
            var tbclientenddate = document.getElementById('txtoperdateend').value;
            //判断开始时间不能大小结束时间
            if (tbclientbegindate.length > 0 && tbclientenddate.length > 0) {
                var reg = new RegExp("-", "g");
                var bvalue = parseInt(tbclientbegindate.replace(reg, ""));
                var evalue = parseInt(tbclientenddate.replace(reg, ""));
                if (bvalue > evalue) {
                    alert('送检日期开始时间不能大于结束时间!');
                    document.getElementById('txtoperdatebegin').focus();
                    return false;
                }
            }
            closesearchdiv();
            showdiv();
            return true;
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
            table2.style.left = document.documentElement.scrollLeft + 20;
            table2.style.top = document.body.scrollTop + 100;
            table2.style.display = 'block';
            //divrmsg.innerHTML = document.getElementById("labexportexcelmsg").outerText;
            fchange();
        }
        function closediv() {
            table1.style.display = 'none';
            table2.style.display = 'none';
        }
        //弹出打印页面
        function ShowPrint() {
            window.open("../TotalReport_MiddlePrint.aspx");
        }
        //得到datagrid的html值
        function GetDataGridShow() {
            return document.getElementById('showdiv').innerHTML;
        }
        function ShowExport() {
            //if (document.getElementById("labexportexcelmsg").outerText != "" && document.getElementById("labexportexcelmsg").outerText != null) {
            GetExportClient();
            //showdiv();
            setTimeout("ShowExport()", 1000);
            //}
            //else {//closediv();
            //document.getElementById("labexportexcelmsg").outerText = "没了";}
        }
        function GetExportClient() {
            OA.Total.MkLab.ScCheckAccount.GetExportList(GetCallresult);
            //document.getElementById("divaaa").innerHTML = r.value;         
        }
        function GetCallresult(result) {
            document.getElementById("divaaa").innerHTML = result.value;
        }
        function showopen() {
            window.showModalDialog('../Progress.aspx', this, 'dialogHeight: 100px; dialogWidth: 350px; edge: Raised; center: Yes; help: No; resizable: No; status: No;scroll:No;');
        }
        
        function ShowMsg(msg) {
            //alert(flag);
            closediv();
            alert(msg);
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
                OA.Total.MkLab.ScCheckAccount.GetClientListByKey(clientkey, GetCallclientresult);
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
            document.getElementById('hidclientno').value= str1.substring(1,str1.indexOf(')'));
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
        //显示查询条件
        function ShowDivSearch() {
            if (document.getElementById('hidflag').value == "0") {
                divSearch.style.left = document.documentElement.scrollLeft + 150;
                divSearch.style.top = document.body.scrollTop + 80;
                divSearch.style.display = 'block';
            }
        }
        function closesearchdiv() {
            divSearch.style.display = 'none';
        }
        function showUserDialog() {
            var strurl = "ScRevenueDayConfig.aspx";
            window.showModalDialog(strurl, this, 'dialogWidth:650px;dialogHeight:500px;');
        }
        //栏目配置
        function ColumnConfig() {
            var cctable = document.getElementById('columnconfigTable');
            if (cctable.style.display == "none") {
                cctable.style.display = "block";
            }
            else { cctable.style.display = "none"; }
        }
        //有参数传进时直接查询
        function LoadReportId() {
            document.getElementById('btnsearch').click();
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
<body onclick="hidDIV();" onload="divdg.scrollTop=GetCookie('posy')" onunload="SetCookie('posy',divdg.scrollTop)">
    <form id="form1" runat="server">
    <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; border-left: solid #000000 1px; border-top: solid #000000 1px;
        border-bottom: solid #000000 1px; border-right: solid #000000 1px; cursor: move;"
        oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" border="0">
            <tbody>
                <tr>
                    <td colspan="2">
                        <div id="divrmsg" style="word-wrap: break-word; float: left; text-align: left; overflow: hidden;
                            color: #313131;">
                            正在执行...<img src="../../Css/indicator_big.gif" alt="" />
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="PopupDiv" style="position: absolute; cursor: hand; filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
        color: Black; padding: 4px; background: #F2F5E0; display: none; z-index: 100">
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
                        <tr>
                            <td colspan="2" align="center" height="50px" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="6">生成月对帐报表统计</font>
                                <input type="hidden" id="hidclientno" runat="server" value="0" />
                                <input type="hidden" id="hidrmsg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <div id="divSearchin" style="word-wrap: break-word; float: left; text-align: left;
                                                    overflow: hidden; color: #313131;">
                                                    <table cellspacing="0" cellpadding="1" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                                                        border-top: #aecdd5 solid 8px; cursor: hand; border-bottom: #aecdd5 solid 8px;"
                                                        border="0">
                                                        <tbody>
                                                            <tr align="left">                                                                
                                                                <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                                    客户:<asp:TextBox onkeyup="GetClientSearch();" CssClass="input_text" Width="180px" ID="txtBox"
                                                                        MaxLength="50" runat="server"></asp:TextBox>
                                                                </td> 
                                                                                                                            
                                                                 <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                                    对帐月:<asp:DropDownList runat="server" ID="drpyear"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td align="center" colspan="4" style="border-bottom: #aecdd5 solid 1px;">
                                                                    <asp:Button runat="server" ID="btnsearch" Text="查 询" CssClass="buttonstyle" OnClick="btnsearch_Click" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button runat="server" CssClass="buttonstyle" ID="btnExport"
                                                                        Text="生成对帐报表" OnClientClick="showdiv();" OnClick="btnExport_Click"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%" height="100%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ForeColor="Red" ID="labmsg"></asp:Label>
                                            <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 500px">
                                                <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                    font-size: 12px" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                </table>
                                                <idatagrid:InheritDataGrid ID="dgrp" AutoGenerateColumns="False" runat="server" 
                                                    PageSize="50" BorderWidth="1px"
                                                    CellPadding="4" BorderStyle="None" BackColor="White" BorderColor="#A7C4F7" Width="100%"
                                                    Font-Size="Smaller" OnItemDataBound="dgrp_ItemDataBound" 
                                                    onitemcommand="dgrp_ItemCommand" AllowPaging="True" 
                                                    onpageindexchanged="dgrp_PageIndexChanged">
                                                    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                    <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                    <HeaderStyle Font-Size="Smaller" Font-Bold="True" Height="26px" ForeColor="Black"
                                                        BackColor="#CCCCCC"></HeaderStyle>
                                                    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                    <Columns>      
                                                         <asp:TemplateColumn HeaderText="对帐月">
                                                            <HeaderStyle HorizontalAlign="Center" Width="8%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="labyname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.yname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>                                              
                                                        <asp:TemplateColumn HeaderText="客户单位">
                                                            <HeaderStyle HorizontalAlign="Center" Width="25%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                  (<asp:Label ID="labclientno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cclientno") %>'
                                                                    ></asp:Label>)
                                                                <asp:Label ID="labclientname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cclientname") %>'></asp:Label>
                                                                <asp:Label ID="labcid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cid") %>'
                                                                    Visible="False"></asp:Label>                                                                   
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>   
                                                        <asp:TemplateColumn HeaderText="金额">
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.csumprice") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>                                                         
                                                        <asp:TemplateColumn HeaderText="对帐单">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndown" runat="server" Visible="false" CommandName="download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.cfilepath") %>'>下载</asp:LinkButton>
                                                                <asp:Label ID="labcfilepath" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cfilepath") %>'></asp:Label>
                                                                <asp:Label ID="labfileitem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cfilepathitem") %>'></asp:Label>
                                                                <asp:Label ID="labpathfile" Visible=false runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cfilepath") %>'></asp:Label>
                                                                <asp:Label ID="labcfilepathitem" Visible=false runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cfilepathitem") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                         <asp:TemplateColumn HeaderText="审核状态">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                            <asp:Label ID="labaudit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cauditstatus") %>'></asp:Label>
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="生成日期">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.ccreatedate") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="left" Width="20%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate> 
                                                                <asp:LinkButton ID="lbtnaudit" runat="server" CommandName="audit">审核上传</asp:LinkButton>&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbtnCreate" runat="server" CommandName="replycreate">重新生成对帐单</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" PageButtonCount="30">
                                                    </PagerStyle>
                                                </idatagrid:InheritDataGrid>
                                            </div>
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
