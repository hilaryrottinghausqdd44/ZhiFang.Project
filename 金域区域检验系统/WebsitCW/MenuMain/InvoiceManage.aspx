<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="InvoiceManage.aspx.cs"
    Inherits="OA.MenuMain.InvoiceManage" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发票绑定管理</title>
    <link href="Css/style.css" rel="stylesheet" charset="utf-8" />

    <link href="../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function GetValue() {


            //用js获取服务器控件的值时首先要得到服务器控件的ClientID

            alert(FindControllerID("form1", "item"));

        }

        function FindControllerID(WebForm, IDString) {
            for (var i = 0; i < WebForm.document.all.length; i++) {
                var idString = WebForm.document.all[i].id;
                var MatchIDString = "(" + IDString + "){1}$";
                if (idString.match(MatchIDString) != null) {
                    return WebForm.document.all[i].id;
                    break;
                }


            }
            return null;

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
                OA.MenuMain.InvoiceManage.GetClientListByKey(clientkey, GetCallclientresult);
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

        function Openchild(nrequestformno) {
            var a = document.getElementById("txtsdbegin").value;
            var b = document.getElementById("txtsdend").value;
            var strurl = 'Lock_list.aspx?nrequestformno=' + nrequestformno + '&&flag=1&&sdbegin=' + a + '&&sdend=' + b;
            //alert(strurl);
            window.open(strurl, '300', 'toolbar=0,menubar=0'); 
            return false
        }
        
        //就诊类型可以多选
        function ShowSelectDiv(objtxt) {
            var retValue = null, objhiddens = null;
            if (objtxt.id == "txtSickType") {
                retValue = OA.MenuMain.InvoiceManage.GetSickType().value;
                objhiddens = "hiddenSickTypeNos";
            }           
            if (retValue != "") {
                var objdiv = document.getElementById("divShowChks");
                var txtTop = objtxt.getBoundingClientRect().top + 20 + document.body.scrollTop;
                var txtLeft = objtxt.getBoundingClientRect().left + document.body.scrollLeft;
                var txtwidth = objtxt.offsetWidth + 120;

                objdiv.style.display = "block";
                objdiv.innerHTML = retValue;
                objdiv.style.top = txtTop;
                objdiv.style.left = txtLeft;
                objdiv.style.width = txtwidth;
                //alert(retValue);
                LoadChecked(objhiddens);
            }
        }
        //隐藏Div
        function CloseDIV(obj) {
            obj.style.display = "none";
        }
        //初始化选中已选择的checkbox的值
        function LoadChecked(obj) {
            var objchks = document.getElementsByName("chkbm");
            for (var i = 0; i < objchks.length; i++) {
                objchks[i].checked = false;
            }
            var objhiddens = document.getElementById(obj).value;
            if (objhiddens != "") {
                var objhidden = objhiddens.split(',');
                for (var i = 0; i < objhidden.length; i++) {
                    if (document.getElementById(objhidden[i]) != null) {
                        document.getElementById(objhidden[i]).checked = true;
                    }
                }
            }
        }
        //选中的checkbox值赋值给文本
        function SelectChkValue(obj, type) {
            var objtxt = null, objhidden = null;
            if (type == "1") {
                objtxt = document.getElementById("txtSickType");
                objhidden = document.getElementById("hiddenSickTypeNos");
            }

            if (obj.type == "checkbox" && obj.checked) {
                if (objtxt.value == "") {
                    objtxt.value = obj.value.split('$')[1];
                    objhidden.value = obj.value.split('$')[0];
                } else {
                    if (objtxt.value.indexOf(obj.value.split('$')[1]) < 0) {
                        objtxt.value = objtxt.value + "," + obj.value.split('$')[1];
                        objhidden.value = objhidden.value + "," + obj.value.split('$')[0];
                    }
                }
            } else {
                if (objtxt.value.indexOf(obj.value.split('$')[1] + ",") >= 0)
                    objtxt.value = objtxt.value.replace(obj.value.split('$')[1] + ",", "");
                else {
                    if (objtxt.value.indexOf(",") >= 0)
                        objtxt.value = objtxt.value.replace("," + obj.value.split('$')[1], "");
                    else
                        objtxt.value = objtxt.value.replace(obj.value.split('$')[1], "");
                }

                if (objhidden.value.indexOf(obj.value.split('$')[0] + ",") >= 0)
                    objhidden.value = objhidden.value.replace(obj.value.split('$')[0] + ",", "");
                else {
                    if (objhidden.value.indexOf(",") >= 0)
                        objhidden.value = objhidden.value.replace("," + obj.value.split('$')[0], "");
                    else
                        objhidden.value = objhidden.value.replace(obj.value.split('$')[0], "");
                }
            }

            if (obj.type == "checkbox" && obj.checked && obj.id == "-1") {
                objtxt.value = "全部";
                objhidden.value = "-1";
                LoadChecked(objhidden.id);
            }

            if (type == "1")
                document.getElementById("hiddenSickTypeNames").value = objtxt.value;
         
        }

        function aaa() {
            document.getElementById("txtSickType").value = document.getElementById("hiddenSickTypeNames").value;
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
<body onclick="hidDIV();" onload="document.body.scrollTop=GetCookie('posy');aaa();" onunload="SetCookie('posy',document.body.scrollTop)">
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
    
    <div id="divShowChks" style="position: absolute; cursor: hand; filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
        color: Black; padding: 4px; background: #F2F5E0; display: none; z-index: 100">       
    </div>
    <input type="hidden" runat="server" id="hiddenSickTypeNos" />    
    <input type="hidden" runat="server" id="hiddenSickTypeNames" />
     
    <table cellpadding="0" width="100%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="6">发票绑定管理</font>
                                <input type="hidden" id="labcbid" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <table>
                                    <tr align="left">
                                        <td align="right">
                                            送检单位:
                                        </td>
                                        <td align="right">
                                            <asp:TextBox onkeyup="GetClientSearch();" Width="110px" ID="txtBox" MaxLength="50"
                                                runat="server" CssClass="input_text"></asp:TextBox>
                                        </td>
                                        <td>
                                            条 码 号:<asp:TextBox runat="server" CssClass="input_text" ID="txtserialno" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            姓    名:<asp:TextBox runat="server" CssClass="input_text" ID="txtname" Width="80px" ></asp:TextBox>
                                        </td>
                                        <td>
                                            病 历 号:<asp:TextBox ID="txtpatno" runat="server" CssClass="input_text" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            送检时间:<asp:TextBox runat="server" ID="operbegin" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                                Width="65px"></asp:TextBox>
                                            -
                                            <asp:TextBox runat="server" ID="txtend" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                                Width="65px"></asp:TextBox>
                                        </td>
                                         <td>
                                            审定时间:<asp:TextBox runat="server" ID="txtsdbegin" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                                Width="65px"></asp:TextBox>
                                            -
                                            <asp:TextBox runat="server" ID="txtsdend" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                                Width="65px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right">
                                            就诊类型:</td>
                                        <td align="left">
                                        <asp:TextBox ID="txtSickType" runat="server" onfocus="ShowSelectDiv(this)"  ReadOnly="true"  CssClass="input_text" Width="110px"></asp:TextBox>
                                            <asp:DropDownList ID="ddlSickType" runat="server" Width="110px" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            实验小组:<asp:DropDownList ID="ddlPGroup" runat="server" Width="80px">
                                            </asp:DropDownList>
                                        </td>                                        
                                        <td>
                                            是否绑定:<asp:DropDownList ID="ddlIsBind" runat="server">
                                                <asp:ListItem Value="0">全部</asp:ListItem>
                                                <asp:ListItem Value="1">是</asp:ListItem>
                                                <asp:ListItem Value="2">否</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>
                                            开票日期:<asp:DropDownList ID="ddlYear" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlMonth" runat="server">
                                                <asp:ListItem Selected="True" Value="0">未选</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="left">
                                            <asp:Button ID="btnsearchitem" runat="server" CssClass="buttonstyle" 
                                                OnClick="btnsearchitem_Click" Text="查询" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
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
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="3" align="right">
                                                    开票日期：<asp:TextBox runat="server" ID="txtOpenDate" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                                Width="80px"></asp:TextBox>
                                                    发票金额：<asp:TextBox ID="txtInvoiceMoney" runat="server" CssClass="input_text" 
                                                            Width="70px"></asp:TextBox>&nbsp;
                                                    发票号：<asp:TextBox ID="txtInvoice" runat="server" CssClass="input_text"></asp:TextBox>
                                                    
                                                    <asp:Button ID="Btn_AllSaveInvoice" runat="server" Text="批量绑定发票" 
                                                            CssClass="buttonstyle" Width="96px" onclick="Btn_AllSaveInvoice_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label Width="300px" runat="server" ID="labmsg" ForeColor="Red" Height="16px"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Width="800px" Height="16px"></asp:Label>
                                                    </td>
                                                    <td>
                                                    
                                                        
                                                    </td>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                      <asp:Label ID="lblAllInvoiceInfo" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            &nbsp; &nbsp;
                                            <asp:DataGrid ID="nrequestform" runat="server" Style="cursor: hand; padding: 0px;
                                                margin: 0px; font-size: 12px" Font-Size="Smaller" BorderWidth="1px" CellPadding="3"
                                                BorderStyle="None" BackColor="White" AutoGenerateColumns="False" BorderColor="BlanchedAlmond"
                                                Width="100%"
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
                                                            <table id="header" border="0" width="100%" style="cursor: hand; padding: 0px; margin: 0px; font-size: 12px"
                                                                cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left" style="width: 1%">
                                                                    </td>
                                                                    <td align="center" style="width: 20%">
                                                                        送检单位
                                                                    </td>
                                                                    <td align="center" style="width: 10%">
                                                                        条码号
                                                                    </td>
                                                                    <td align="left" style="width: 6%">
                                                                        就诊类型
                                                                    </td>
                                                                    <td align="left" style="width: 8%">
                                                                        小组
                                                                    </td>
                                                                    <td align="left" style="width: 5%">
                                                                        姓名
                                                                    </td>
                                                                    <td align="left" style="width: 3%">
                                                                        性别
                                                                    </td>
                                                                    <td align="left" style="width: 3%">
                                                                        年龄
                                                                    </td>
                                                                    <td align="left" style="width: 8%">
                                                                        送检时间
                                                                    </td>
                                                                    <td align="left" style="width: 8%">
                                                                        金额
                                                                    </td>
                                                                    <td align="left" style="width: 8%">
                                                                        开票日期
                                                                    </td>
                                                                     <td align="left" style="width: 10%;">
                                                                        发票号
                                                                    </td>
                                                                     <td align="left" style="width: 10%;">
                                                                        实际发票金额
                                                                    </td>
                                                                    <td style=" display:none;" align="center" >
                                                                        操作
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table id="tbcomboorg" width="100%" border="0" style="cursor: hand; padding: 0px; margin: 0px;
                                                                font-size: 12px" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left" style="width: 1%">
                                                                        <%# Container.ItemIndex + 1%>
                                                                    </td>
                                                                    <td align="left" style="width: 20%">
                                                                        <asp:Label runat="server" ID="labclientno" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.nrequestformno") %>'></asp:Label>
                                                                        <%# DataBinder.Eval(Container, "DataItem.clientname")%>
                                                                    </td>
                                                                    <td align="center" style="width: 10%">
                                                                        <%# DataBinder.Eval(Container, "DataItem.serialno") %>
                                                                        <asp:Label runat="server" ID="lblserialno" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.serialno") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="width: 6%">
                                                                        <%#DataBinder.Eval(Container, "DataItem.NFSickTypeName")%>
                                                                    </td>
                                                                    <td align="left" style="width: 8%">
                                                                        <asp:Label runat="server" ID="lblReceiveSectionName" ></asp:Label>
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
                                                                    <td align="left" style="width: 8%">
                                                                        <%# DataBinder.Eval(Container, "DataItem.operdate")%>
                                                                    </td>
                                                                    <td align="left" style="width: 8%">
                                                                        <%# DataBinder.Eval(Container, "DataItem.itemagioprice")%>
                                                                        
                                                                        <asp:Label runat="server" ID="item"></asp:Label>
                                                                        <asp:Label runat="server" ID="lblLockFlag" Visible="false" Text='<%# Eval("isLocked")%>' ></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="width: 8%">
                                                                         <%# DataBinder.Eval(Container, "DataItem.InvoiceCWDate")%>
                                                                    </td>
                                                                     <td align="left" style="width: 10%;">
                                                                        <%# DataBinder.Eval(Container, "DataItem.InvoiceCW")%>
                                                                    </td>
                                                                    <td align="left" style="width: 10%;">
                                                                        <%# DataBinder.Eval(Container, "DataItem.InvoiceCWMoney")%>
                                                                    </td>
                                                                    <td align="left" style=" display:none;" >
                                                                        <asp:TextBox ID="txtInvoiceGrid" runat="server" Width="75%" Text='<%# Eval("InvoiceCW")%>' ></asp:TextBox>
                                                                        <asp:Button runat="server" Width="15%" CssClass="buttonstyle" ID="BtnSave" Text='保存'
                                                                            CommandArgument='<%# Bind("nrequestformno") %>' onclick="BtnSave_Click"  />
                                                                   </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <div style="text-align:center">
 <asp:LinkButton ID="hlIndex" runat="server" onclick="hlIndex_Click" Enabled="false"
                        >首页</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="hlPre" runat="server" onclick="hlPre_Click"  Enabled="false"
                        >上一页</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="hlNext" runat="server" onclick="hlNext_Click"  Enabled="false"
                        >下一页</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="hlLast" runat="server" onclick="hlLast_Click"   Enabled="false"
                        >尾页</asp:LinkButton>                    
                    &nbsp;当前<asp:Label ID="lblCurrentPage" runat="server" Text="0"></asp:Label>页,
                    共<asp:Label ID="lblTotalPage" runat="server" Text="0"></asp:Label>页
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
        <tr>
            <td align="right">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;
            </td>
        </tr>
    </table>
     
     
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    </form>
</body>
</html>
