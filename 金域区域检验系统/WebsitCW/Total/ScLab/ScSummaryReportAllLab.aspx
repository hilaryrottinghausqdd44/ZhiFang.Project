<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScSummaryReportAllLab.aspx.cs" Inherits="OA.Total.ScLab.ScSummaryReportAllLab" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>汇总统计(四川大家+攀钢)</title>
    <link href="../../Css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../../Css/calendar1.js"></script>

    <script type="text/javascript">
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
            OA.Total.ScLab.ScSummaryReportAllLab.GetExportList(GetCallresult);
            //document.getElementById("divaaa").innerHTML = r.value;         
        }
        function GetCallresult(result) {
            document.getElementById("divaaa").innerHTML = result.value;
        }
        function showopen() {
            window.showModalDialog('../Progress.aspx', this, 'dialogHeight: 100px; dialogWidth: 350px; edge: Raised; center: Yes; help: No; resizable: No; status: No;scroll:No;');
        }
        //下载收入明细报表统计excel
        function CloseExcel(flag, filename) {
            //alert(flag);
            closediv();
            if (flag == "1") {
                window.location.href = "../downloadfile.aspx?file=sclab/TotalExcel/" + filename;
            }
            else { alert('导出财务汇总统计报表出错!'); }
        }


        var changename = "";
        var num = 0;
        function GetClientSearch() {
            //键盘事件
            var key = window.event.keyCode;
            changename = "txtBox";
            if (key != 40 && key != 38 && key != 13) {
                //送检单位关键字
                var clientkey = document.getElementById('txtBox').value;
                PopupDiv.innerHTML = "";
                showimage();
                OA.Total.ScLab.ScSummaryReportAllLab.GetClientListByKey(clientkey, GetCallclientresult);
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
                OA.Total.ScLab.ScSummaryReportAllLab.GetItemListByItemKey(clientkey, GetCallitemresult);
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
            if (obj != null) {
                var x = obj.offsetLeft;
                var y = obj.offsetTop + obj.offsetHeight;
                while (obj.offsetParent) {
                    obj = obj.offsetParent;
                    x += obj.offsetLeft;
                    y += obj.offsetTop;
                }
                arr[0] = x;
                arr[1] = y;
            }
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
<body onclick="hidDIV();" onload="ShowDivSearch();">
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; border-left: solid #000000 1px; border-top: solid #000000 1px;
        border-bottom: solid #000000 1px; border-right: solid #000000 1px; cursor: move;"
        oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="120%" bgcolor="#fcfcfc" border="0">
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
    <div id="divSearch" style="position: absolute; background: #fcfcfc; display: none;
        z-index: 3; border-left: solid #000000 1px; border-top: solid #000000 1px; border-bottom: solid #000000 1px;
        border-right: solid #000000 1px; cursor: move;" oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="630px" bgcolor="#fcfcfc" border="0">
            <tbody>
                <tr>
                    <td colspan="2">
                        <div id="divSearchin" style="word-wrap: break-word; float: left; text-align: left;
                            overflow: hidden; color: #313131;">
                            <table cellspacing="0" cellpadding="1" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                                border-top: #aecdd5 solid 8px; cursor: hand; border-bottom: #aecdd5 solid 8px;"
                                border="0">
                                <tbody>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            项目编号:
                                        </td>
                                        <td align="left" width="100px" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtitemno" CssClass="input_text" Width="80px"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            销售员:
                                        </td>
                                        <td align="left" width="100px" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpbusinessname">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            项目名称:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" onkeyup="GetItemSearch()" ID="txtitemname" CssClass="input_text"
                                                Width="180px"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            片区:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" CssClass="input_var" ID="drpclientarea">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            客户(标本来源):
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox onkeyup="GetClientSearch();" CssClass="input_text" Width="180px" ID="txtBox"
                                                MaxLength="50" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            客户类型:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtclientstyle" CssClass="input_text" Width="60px"
                                                Style="display: none"></asp:TextBox>
                                            <asp:DropDownList ID="ddlClientStyle" runat="server" Width="80px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            采样日期:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtcolectdatebegin" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>
                                            -<asp:TextBox runat="server" ID="txtcollectdateend" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            病人:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtcname" Width="60px" CssClass="input_text"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            送检日期:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtoperdatebegin" Width="70px" CssClass="input_text"
                                                Text="" ondblclick="calendar();"></asp:TextBox>
                                            -<asp:TextBox runat="server" ID="txtoperdateend" Width="70px" CssClass="input_text"
                                                Text="" ondblclick="calendar();"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            样本类型:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpsampletype">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            一审日期:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtsendtime1begin" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>
                                            -<asp:TextBox runat="server" ID="txtsendtime1end" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            一审人员:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtsendtime1er" CssClass="input_text" Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            二审日期:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtsendtime2begin" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>
                                            -<asp:TextBox runat="server" ID="txtsendtime2end" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            二审人员:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtsendtime2er" CssClass="input_text" Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            财务审核日期:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtislockdatebegin" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>-
                                            <asp:TextBox runat="server" ID="txtislockdateend" Width="70px" CssClass="input_text"
                                                ondblclick="calendar();"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            财务审核人员:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtislocker" CssClass="input_text" Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            是否免单:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpisfree">
                                                <asp:ListItem Value="全部" Text="全部"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="未免单"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="已免单"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            财务是否审核:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpislocked">
                                                <asp:ListItem Value="全部" Text="全部"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="已审核"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            就诊类型:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpsicktype">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            医生:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtdoctor" CssClass="input_text" Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            科室:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpdept">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            检验者:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" CssClass="input_text" ID="txttestdater" Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            录入人:
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:TextBox runat="server" ID="txtopertor" CssClass="input_text" Width="60px"></asp:TextBox>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            小组
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpsection">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                            数据来源
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:DropDownList runat="server" ID="drpTargetClientNoSummary">
                                                <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                                                <asp:ListItem Text="四川大家实验室" Value="1">四川大家实验室</asp:ListItem>
                                                <asp:ListItem Text="攀钢职工总医院" Value="2">攀钢职工总医院</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" style="border-bottom: #aecdd5 solid 1px;">    
                                            &nbsp;                      
                                        </td>
                                        <td align="left" style="border-bottom: #aecdd5 solid 1px;">  
                                            &nbsp;                                           
                                        </td>
                                    </tr>
                                   
                                   
                                    <tr>
                                        <td colspan="4">
                                            <asp:Panel runat="server" ID="panelconfig">
                                                <a href="#" onclick="ColumnConfig();">栏目配置</a></asp:Panel>
                                            <table id="columnconfigTable" cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc"
                                                style="display: none; border: #aecdd5 solid 1px; border-top: #aecdd5 solid 1px;
                                                border-bottom: #aecdd5 solid 1px; border-left: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;"
                                                border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            客户
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            销售员
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            项目编号
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            项目名称
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            小组
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            就诊类型
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            仪器
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            客户类型
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            片区
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px;">
                                                            客户分类
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ToolTip="nfclientname" ID="chkclientnameisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ToolTip="nfclientname" ID="chkclientnameissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtclientnameorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtclientnameorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpclientnamesort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="nfclientname asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="nfclientname desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfbusinessname" ID="chkbusinessnameisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfbusinessname" ID="chkbusinessnameissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtbusinessnameorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtbusinessnameorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpbusinessnamesort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="nfbusinessname asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="nfbusinessname desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="paritemno" ID="chkparitemnoisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="paritemno" ID="chkparitemnoissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtparitemnoorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtparitemnoorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpparitemnosort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="paritemno asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="paritemno desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="itemnamecw" ID="chkitemnameisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="itemnamecw" ID="chkitemnameissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtitemnameorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtitemnameorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpitemnamesort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="itemnamecw asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="itemnamecw desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="receivesectionname" ID="chksecteionisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="receivesectionname" ID="chksectionissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtsectionorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtsectionorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpsectionsort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="receivesectionname asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="receivesectionname desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfsicktypename" ID="chknfsicktypenameisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfsicktypename" ID="chknfsicktypenameissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtnfsicktypenameorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtnfsicktypenameorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpnfsicktypenamesort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="nfsicktypename asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="nfsicktypename desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nequipname" ID="chknequipnameisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nequipname" ID="chknequipnameissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtnequipnameorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtnequipnameorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpnequipnamesort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="nequipname asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="nequipname desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfclientstyle" ID="chknfclientstyleisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfclientstyle" ID="chknfclientstyleissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtnfclientstyleorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtnfclientstyleorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpnfclientstylesort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="nfclientstyle asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="nfclientstyle desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfclientarea" ID="chknfclientareaisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfclientarea" ID="chknfclientareaissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtnfclientareaorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtnfclientareaorder"
                                                                            ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpnfclientareasort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="nfclientarea asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="nfclientarea desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfclienttype" ID="chknfclienttypeisvisible" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="nfclienttype" ID="chknfclienttypeissubtotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtnfclienttypeorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                            ControlToValidate="txtnfclienttypeorder" ErrorMessage="必须为数字" SetFocusOnError="True"
                                                                            ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="drpnfclienttypesort">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="nfclienttype asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="nfclienttype desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            折前价格
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            折后价格
                                                        </td>
                                                        <td align="left" width="10%" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            数据来源
                                                        </td>
                                                    </tr>
                                                    <tr>                                                    
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ToolTip="itemprice" ID="chkitempriceshow" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ToolTip="itemprice" ID="chkitempricetotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtitempriceorder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                                            ControlToValidate="txtitempriceorder" ErrorMessage="必须为数字" SetFocusOnError="True"
                                                                            ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="ddlitemprice">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="itemprice asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="itemprice desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ToolTip="ItemAgioPrice" ID="chkItemAgioPriceShow" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="ItemAgioPrice" ID="chkItemAgioPriceTotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtItemAgioPriceOrder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                                                            ControlToValidate="txtItemAgioPriceOrder" ErrorMessage="必须为数字" SetFocusOnError="True"
                                                                            ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="ddlItemAgioPrice">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="ItemAgioPrice asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="ItemAgioPrice desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" style="border-bottom: #aecdd5 solid 1px; border-right: #aecdd5 solid 1px;">
                                                            <table>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ToolTip="LabName" ID="chkTargetClientNoShow" />显示
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ToolTip="LabName" ID="chkTargetClientNoTotal" />小计
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        次序:<asp:TextBox runat="server" CssClass="input_text" Width="30px" ID="txtTargetClientNoOrder"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                            ControlToValidate="txtTargetClientNoOrder" ErrorMessage="必须为数字" SetFocusOnError="True"
                                                                            ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" Width="50px" ID="ddlTargetClientNo">
                                                                            <asp:ListItem Value="">未设置</asp:ListItem>
                                                                            <asp:ListItem Value="LabName asc">正序asc</asp:ListItem>
                                                                            <asp:ListItem Value="LabName desc">倒序desc</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr style="display: none">
                                                        <td align="right" colspan="10" style="border-right: #aecdd5 solid 1px;">
                                                            栏目名称:<asp:TextBox runat="server" CssClass="input_text" ID="txtcolumnsname"></asp:TextBox>
                                                            &nbsp;<asp:Button runat="server" ID="btnSaveColumn" Text="创建" OnClientClick="javascript:if(document.getElementById('txtcolumnsname').value==''){
                                                            alert('请输入创建新栏目名称');document.getElementById('txtcolumnsname').focus();return false;}return true;"
                                                                CssClass="buttonstyle" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="center" colspan="4" style="border-bottom: #aecdd5 solid 1px;">
                                            <asp:Button runat="server" ID="btnsearch" Text="查 询" CssClass="buttonstyle" OnClick="btnsearch_Click" />
                                            &nbsp;&nbsp;<input type="button" id="btnclosediv" value="关 闭" class="buttonstyle"
                                                onclick="closesearchdiv();" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <table cellpadding="0" width="100%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" height="50px" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="6">财务汇总统计报表</font>
                                <input type="hidden" id="hidflag" runat="server" value="0" />
                                <input type="hidden" id="hidrmsg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <input type="button" id="btnshowsearch" runat="server" value="显示查询条件" class="buttonstyle"
                                                    onclick='document.getElementById("hidflag").value="0";ShowDivSearch();' />
                                                <asp:Button runat="server" Visible="False" CssClass="buttonstyle" ID="btnExport"
                                                    Text="另存为Excel" OnClientClick="showdiv();" OnClick="btnExport_Click"></asp:Button>
                                                &nbsp;<input onclick="javascript:ShowPrint();" class="buttonstyle" type="button"
                                                    value="打印" name="button_print">
                                                     <asp:HyperLink ID="hyl1" runat="server" Visible="false"></asp:HyperLink>
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
                                            <div id="showdiv">
                                                <asp:Label runat="server" ID="labmsg" ForeColor="Red" Font-Size="X-Small"></asp:Label>
                                                <br />
                                                
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvReport" runat="server" AllowPaging="True" Width="100%" BorderWidth="1px"
                                                        CellPadding="4" BackColor="White" BorderColor="#003366" 
                                                        Font-Size="Smaller" onpageindexchanging="gvReport_PageIndexChanging" 
                                                        PageSize="20" onrowdatabound="gvReport_RowDataBound" ShowFooter="true">
                                                        
                                                            <PagerTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="text-align: right">
                                                                            第<asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label>页
                                                                            共<asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>"></asp:Label>页
                                                                            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                CommandName="Page" Text="首页"></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                CommandName="Page" Text="上一页"></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                CommandName="Page" Text="下一页"></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                CommandName="Page" Text="尾页"></asp:LinkButton>
                                                                            <asp:TextBox ID="txtNewPageIndex" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1%>"
                                                                                Width="20px"></asp:TextBox>
                                                                            <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1"
                                                                                CommandName="Page" Text="GO"></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </PagerTemplate>
                                                        
                                                            <PagerStyle HorizontalAlign="Center" />
                                                        
                                                        <HeaderStyle Font-Size="X-Small" Height="26px" ForeColor="Black"
                                                            BackColor="#CCCCCC" HorizontalAlign="Left"></HeaderStyle>
                                                        <PagerSettings Mode="NextPreviousFirstLast" Position="Bottom" 
                                                            PageButtonCount="30" FirstPageText="第一页" LastPageText="最后一页" 
                                                            NextPageText="下一页" PreviousPageText="上一页" />
                                                        <FooterStyle ForeColor="#330099" BackColor="White"></FooterStyle>
                                                        <Columns>
                                                        </Columns>
                                                       
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                
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
