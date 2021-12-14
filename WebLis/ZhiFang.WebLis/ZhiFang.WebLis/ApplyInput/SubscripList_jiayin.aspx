<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscripList_jiayin.aspx.cs"
    Inherits="ZhiFang.WebLis.ApplyInput.SubscripList_jiayin" %>

<html>
<head id="Head1" runat="server">
    <title>申请单列表</title>
    <link href="SubScriptCss.css" media="all" rel="Stylesheet" />
    <script type="text/javascript" src="../JS/WindowLocationSize.js"></script>
    <script src="../JS/Tools.js" type="text/javascript"></script>
    <script charset="UTF-8" src="../jquery-easyui-1.3/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.3/easyloader.js" type="text/javascript"></script>
    <!--#include file="../JS/Calendar.js"-->
    <script type="text/javascript">
        function OpenInsertPage(flag) {
            //flag == '0' 表示录入新病历单
            //flag == '1' 表示修改原病历单
            var url = "ApplyInput_Weblis_jiayin.aspx?Flag=0&BarCodeInputFlag=" + getQueryString("BarCodeInputFlag");
            window.open(url, "申请单维护", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
        }
        function ModifyTest(BarCode) {
            var url = "ApplyInput_Weblis_jiayin.aspx?Flag=1&BarCodeNo=" + BarCode + "&BarCodeInputFlag=" + getQueryString("BarCodeInputFlag");
            window.open(url, "申请单维护", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
        }
        function DeleteTest(BarCode) {
          event.returnValue = confirm("删除是不可恢复的，你确认要删除吗？");
          if (event.returnValue) {
              var i = ZhiFang.WebLis.ApplyInput.SubscripList_jiayin.DeletRequest(BarCode);
              if (i = true) {
                  alert("删除成功");
                  document.getElementById('btnSearch').click();
              }
              else {
                  alert("删除失败");
              }
          }
        }
        function getQueryString(name) {
            var urlall = location.href;
            var urla = urlall.split('?');
            var paramat;
            if (urla.length > 1) {
                var para = urla[1].split('&');
                for (var i = 0; i < para.length; i++) {
                    paramat = para[i].split('=');
                    if (paramat[0] == name) {
                        return paramat[1];
                    }
                }
            }
        }
        function SetRowFocus(obj, bgColor) {
            obj.style.backgroundColor = bgColor;
        }
        function PrintBarCode() {
            //打印条码号    
            for (var i = 1; i < 50; i++) {
                try {
                    if (document.getElementById('p' + i)) {
                        if (document.getElementById('chk_' + i) && document.getElementById('chk_' + i).selected)
                            document.getElementById('p' + i).click();
                    }
                }
                catch (e) {
                    return;
                }
            }
        }
        //        function PrintCurBarCode(BarCode, cname, Gender, Age, AgeUnit, Department, Time, Item, ClientName, SampleType, Total, ady) {
        //            //打印当前唯一条码
        //            if (print(BarCode, 'controlbyid', cname, Gender, Age, AgeUnit, Department, Time, Item, ClientName, SampleType, Total, ady) == 1) {
        //                document.getElementById('hiddentmpBarCode').value = BarCode;
        //                var o = document.getElementById('saveprintcount');
        //                o.click();
        //                //saveprintcount.onclick();
        //            }
        //        }
        function PrintCurBarCode(BarCode, ConnStr, ItemName, ItemCode, Total, ady) {
            alert(BarCode);
            var dsStr = ZhiFang.WebLis.ApplyInput.SubscripList_jiayin.GetBarCodeView(BarCode);
            alert(dsStr.value);
            if (print1(dsStr.value, ConnStr, 'controlbyid', ItemName, ItemCode, Total, ady) == 1) {
                document.getElementById('hiddentmpBarCode').value = BarCode;
                var o = document.getElementById('saveprintcount');
                o.click();
            }
        }
        //        function PrintCurBarCode1(BarCode, cname, Gender, Age, AgeUnit, Department, Time, Item, WeblisOrgName, SampleType, ClientName, ModelPrint,shortCode, Total, ady) {
        //            if (print1(BarCode, cname, 'controlbyid', Gender, Age, AgeUnit, Department, Time, Item, WeblisOrgName, SampleType, ClientName, ModelPrint, shortCode, Total, ady) == 1) {
        //                document.getElementById('hiddentmpBarCode').value = BarCode;
        //                var o = document.getElementById('saveprintcount');
        //                o.click();
        //            }
        //        }
        function GetBarCodeList() {
            var hiddenTestCount = document.getElementById("hiddenTestCount");
            var BarCodeList = "";
            for (var i = 1; i < hiddenTestCount.value; i++) {
                var ChkID = "chk_" + i;
                var chk = document.getElementById(ChkID);
                if (chk && chk.checked) {
                    if (chk.value != "") {
                        BarCodeList += chk.value + ";";
                    }
                }
            }
            return BarCodeList;
        }
        function setAllCheckBox(obj) {
            var hiddenTestCount = document.getElementById("hiddenTestCount");
            for (var i = 0; i < hiddenTestCount.value; i++) {
                var ChkID = "chk_" + i;
                var chk = document.getElementById(ChkID);
                if (chk) {
                    chk.checked = obj.checked;
                }
            }
        }

        function SetNextFoucs() {
            if (event.keyCode == "13" && SelectRow == -1) {
                event.keyCode = 9;  //转化tab键
            }
        }
    
    </script>
    <script type="text/javascript">
        var FirstTimesFlag = true; //统计次数
        var SelectRow = -1; //当按下键盘上 上下  键时选中的行编号
        var tmpc = "";
        var tmpn = "";
        var flag = true;
        //当用户录入时进行查询
        function SelectInputItem(obj, strHidden, column, DataTable) {
            if (flag) {
                document.getElementById('UpdatePanel1').style.position = 'absolute';
                document.getElementById('UpdatePanel1').style.index = '998';
                if (!FirstTimesFlag) {
                    FirstTimesFlag = true;
                    return;
                }
                SelectRow = -1; //每次查询之前将选中行置为-1
                var ttop = GetPosTop(obj);
                var tleft = GetPosLeft(obj);
                var hiddenControlName = document.getElementById("hiddenControlName");
                hiddenControlName.value = obj.id; //当用户选择下拉表格内容时，将名称写入此控件
                var hiddenControlValue = document.getElementById("hiddenControlValue");
                hiddenControlValue.value = strHidden; //当用户选择下拉表格内容时，该控件指引系统将值写入的位置


                var hiddenLeft = document.getElementById("hiddenLeft"); //当前控件横坐标
                hiddenLeft.value = tleft;
                //hiddenLeft.style.left;
                var hiddenTop = document.getElementById("hiddenTop"); //当前控件纵坐标
                hiddenTop.value = ttop;
                var Sys = {};
                var ua = navigator.userAgent.toLowerCase();
                if (window.ActiveXObject) {
                    Sys.ie = ua.match(/msie ([\d.]+)/)[1]; //获取浏览器版本
                }
                if (Sys.ie == "8.0") {
                    var o = obj.offsetParent;
                    var countt = 0;
                    var countl = 0;
                    while (o) {
                        countt += o.offsetTop;
                        countl += o.offsetLeft;
                        o = o.offsetParent;
                    }
                    //document.getElementById('UpdatePanel1').style.top = countt+21;
                    //document.getElementById('UpdatePanel1').style.left = countl;
                    document.getElementById('UpdatePanel1').style.top = "21px";
                    document.getElementById('UpdatePanel1').style.left = "0px";
                    document.getElementById('listiframe').style.display = "block";
                }
                else {
                    document.getElementById('UpdatePanel1').style.top = "21px";
                    document.getElementById('UpdatePanel1').style.left = "0px";
                    document.getElementById('listiframe').style.display = "none";
                }
                var Div_Input = document.getElementById("Div_Input"); //显示信息的DIV

                var hiddenUserInput = document.getElementById("hiddenUserInput"); //用户录入内容
                hiddenUserInput.value = obj.value;

                var hiddenColumn = document.getElementById("hiddenColumn"); //待查询字段名
                hiddenColumn.value = column;

                var hiddenDataTable = document.getElementById("hiddenDataTable"); //待查询表名
                hiddenDataTable.value = DataTable;

                /*if(obj.value == null || obj.value == "")
                {
                var hiddenSelectedClient = document.getElementById(strHidden);
                hiddenSelectedClient.value = "";
                Div_Input.style.display = "none";
                return;
                }*/
                //alert(strHidden + ',' + column + ',' + DataTable);
                //alert(DataTable + ',' + obj.value + ',' + column);  
                var ClientNo = document.getElementById("hiddenClient");
                ZhiFang.WebLis.Ashx.ApplyInput.SelectList(DataTable, obj.value, column, SelectInputItem_CallBack);
            }
        }
        function SelectInputItem_CallBack(o) {

            if (o != null && o.value != null) {
                var selectinputitem_table = document.getElementById('tableUserInputSJDWList');
                //alert(o.value);
                //alert(selectinputitem_table.innerHTML);
                selectinputitem_table.innerHTML = o.value;
                var Div_Input = document.getElementById("Div_Input");
                Div_Input.style.display = 'block';
                Div_Input.style.left = document.getElementById('hiddenLeft').value;
                Div_Input.style.top = document.getElementById('hiddenTop').value;
            }
        }
        var SelectRow = -1; //当按下键盘上 上下  键时选中的行编号
        function SelectNextClient(obj) {
            var Div_Input = document.getElementById("Div_Input");
            if (Div_Input.style.display != "none") {
                var tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
                var thtml = tableUserInputSJDWList.innerHTML;
                //alert(thtml);
                var thtmla = thtml.split('</TR>');
                var totalRow = thtmla.length - 1;

                if (totalRow > 0) {
                    //event.keyCode=38   向上
                    if (event.keyCode == 38) {
                        if (SelectRow != -1) {
                            document.getElementById('selecttr_' + SelectRow).style.backgroundColor = '#a3f1f5';
                        }
                        if (SelectRow == 0 || SelectRow == -1) {
                            SelectRow = totalRow - 1;
                        }
                        else {
                            SelectRow = SelectRow - 1;
                        }
                        document.getElementById('selecttr_' + SelectRow).style.backgroundColor = '#999999';
                    }
                    //event.keyCode=40   向下
                    if (event.keyCode == 40) {
                        if (SelectRow != -1) {
                            document.getElementById('selecttr_' + SelectRow).style.backgroundColor = '#a3f1f5';
                        }
                        if (SelectRow == (totalRow - 1) || SelectRow == -1) {
                            SelectRow = 0;
                        }
                        else {
                            SelectRow = SelectRow + 1;
                        }
                        document.getElementById('selecttr_' + SelectRow).style.backgroundColor = '#999999';
                    }
                    //event.keyCode=13   回车键
                    if (event.keyCode == 13) {
                        if (SelectRow != -1) {
                            document.getElementById('selecttr_' + SelectRow).click();

                            //GetClient(tableUserInputSJDWList.rows[SelectRow][].click())

                        }
                        else {
                            if (obj.value != "") {
                                document.getElementById('selecttr_' + 0).click();
                            }
                        }
                        SetNextFoucs();
                    }
                }
            }
        }
        function SelectValue(o, h, c, t) {

            if (!FirstTimesFlag) {
                var hiddenControlName = document.getElementById("hiddenControlName");
                hiddenControlName.value = o.id; //当用户选择下拉表格内容时，将名称写入此控件
                var hiddenControlValue = document.getElementById("hiddenControlValue");
                hiddenControlValue.value = h; //当用户选择下拉表格内容时，该控件指引系统将值写入的位置

                var hiddenUserInput = document.getElementById("hiddenUserInput"); //用户录入内容
                hiddenUserInput.value = o.value;

                var hiddenColumn = document.getElementById("hiddenColumn"); //待查询字段名
                hiddenColumn.value = c;

                var hiddenDataTable = document.getElementById("hiddenDataTable"); //待查询表名
                hiddenDataTable.value = t;
                document.getElementById("btnSearchInfoByName").click();
                alert(document.getElementById(h).value);
            }
            else {

                if (tmpc == "" && tmpn == "") {
                    if (o.value != "") {
                        document.getElementById('selecttr_' + 0).click();
                    }
                }
                else {

                }
                flag = false;
                GetClient(tmpc, tmpn);
            }
        }
        function GetClient(id, name) {
            var objC = document.getElementById("hiddenControlName");
            var obj = document.getElementById(objC.value);
            obj.value = name;
            var hiddenControlValue = document.getElementById("hiddenControlValue");
            var hiddenValue = document.getElementById(hiddenControlValue.value);
            hiddenValue.value = id;
            tmpc = "";
            tmpn = "";

            DisplayDiv(true);
        }
        function DisplayDiv(flag) {
            if (flag) {
                var Div_Input = document.getElementById("Div_Input");
                Div_Input.style.display = "none";
                SelectRow = -1; //恢复选中行号为-1
            }
        }
        function GetTmpClient(c, n) {
            tmpc = c;
            tmpn = n;
        }
        function SelectValue(o) {

            if (!FirstTimesFlag) {
            }
            else {

                if (tmpc == "" && tmpn == "") {
                    if (o.value != "") {
                        tableUserInputSJDWList.rows[0].click();
                    }
                }
                else {

                }
                flag = false;
                GetClient(tmpc, tmpn);
            }
        }
        function CheckBarcodePrintFlag() {
            if (getQueryString('BarCodeInputFlag') == '1') {
                //alert('a');
                document.getElementById('btnPrintBarCode').style.display = 'none';
                document.getElementById('td_printbarcode').style.display = 'none';
            }
            else {
                //alert('b' + "@" + getQueryString('BarCodeInputFlag'));
                document.getElementById('btnPrintBarCode').style.display = 'block';
            }
        }
    </script>
</head>
<body onload="document.getElementById('UpdatePanel1').style.position = 'absolute';document.getElementById('UpdatePanel1').style.index = '998';CheckBarcodePrintFlag();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Operate_Div">
        <table class="Operate_Table">
            <tr>
                <td>
                    <input type="button" name="Input" accesskey="I" onclick="OpenInsertPage();" value="录入[I]" />
                </td>
                <td>
                    <input type="button" id="btnPrintBarCode" name="btnPrintBarCode" accesskey="P" onclick="PrintBarCode();"
                        value="打印条码[P]" />
                </td>
            </tr>
        </table>
    </div>
    <div class="Search_Div">
        <table width="98%" height="50" border="0" cellpadding="0" cellspacing="0" style="font-size: 12px">
            <tr>
                <td>
                    送检单位：<%--<input type="text" class="inputText" id="txtClientNo" name="txtClientNo" runat="server"
                        onfocus="flag=true;SelectInputItem(this,'hiddenClient','WebLisSourceOrgID','Client');"
                        onkeydown="SelectNextClient(this,'hiddenClient')" onpropertychange="SelectInputItem(this,'hiddenClient','WebLisSourceOrgID','Client')"
                        onblur="SelectValue(this,'hiddenClient','WebLisSourceOrgID','Client');DisplayDiv(true);"
                        tabindex="0" /><input type="hidden" id="hiddenClient" runat="server" />--%>
                    <input type="text" class="inputText" id="txtClientNo" name="txtClientNo" runat="server"
                        onfocus="flag=true;SelectInputItem(this,'hiddenClient','WebLisSourceOrgID','Client');"
                        onkeydown="SelectNextClient(this,'hiddenClient')" onpropertychange="SelectInputItem(this,'hiddenClient','WebLisSourceOrgID','Client')"
                        onblur="SelectValue(this,'hiddenClient','WebLisSourceOrgID','Client');DisplayDiv(true);"
                        tabindex="0" /><input type="hidden" id="hiddenClient" runat="server" />
                    姓名&nbsp;&nbsp;：<asp:TextBox ID="txtPatientName" Width="100" class="inputTextList"
                        runat="server" TabIndex="1"></asp:TextBox>
                </td>
                <td width="380">
                    开单时间：<input name="txtCollectDate" onfocus="setday(this);" type="text" class="inputTextList"
                        id="txtStartDate" runat="server" readonly />-<input name="txtCollectDate" onfocus="setday(this);"
                            readonly type="text" class="inputTextList" id="txtEndDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    医生&nbsp;&nbsp;&nbsp;&nbsp;：<input type="text" id="SelectDoctor" onfocus="flag=true;SelectInputItem(this,'hiddenDoctorNo','DoctorNo','Doctor')"
                        onkeydown="SelectNextClient(this,'hiddenDoctorNo')" onpropertychange="SelectInputItem(this,'hiddenDoctorNo','DoctorNo','Doctor')"
                        onblur="SelectValue(this,'hiddenDoctorNo','DoctorNo','Doctor');DisplayDiv(true);"
                        class="inputText" runat="server" tabindex="14" /><input type="hidden" id="hiddenDoctorNo"
                            runat="server" />
                    病历号：<asp:TextBox ID="txtPatientID" Width="100" class="inputTextList" runat="server"
                        TabIndex="2"></asp:TextBox>
                </td>
                <td width="380">
                    <div style="display: none;">
                        采样时间：<input name="txtCollectDate" onfocus="setday(this);" type="text" class="inputTextList"
                            readonly id="txtCollectStartDate" runat="server" />-<input name="txtCollectDate"
                                onfocus="setday(this);" readonly type="text" class="inputTextList" id="txtCollectEndDate"
                                runat="server" /></div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox TabIndex="3" ID="chkOnlyNoPrintBarCode" Checked="true" Text="仅未打印条码"
                        runat="server" />
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Button TabIndex="4" ID="btnSearch" runat="server" Text="查询[S]" AccessKey="S"
                                OnClick="btnSearch_Click" Style="height: 21px" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div id="UpdatePanel1">
        <div id="Div_Input" style="position: absolute; z-index: 999; display: none; font-size: 12px">
            <div id="tableUserInputSJDWList">
            </div>
            <iframe id='listiframe' style="position: absolute; width: 100%; height: 100%; _filter: alpha(opacity=0);
                opacity=0; border-style: none; display: none"></iframe>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="boderblue">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="1" class="pad" id="List_Table"
                                runat="server">
                                <tr bgcolor="#c3c1c5">
                                    <td width="20" align="center">
                                        <input type="checkbox" onclick="setAllCheckBox(this);" checked="checked" id="chkAll" />
                                    </td>
                                    <td width="60" align="center">
                                        条码号
                                    </td>
                                    <td width="55" align="center">
                                        姓名
                                    </td>
                                    <td width="35" align="center">
                                        性别
                                    </td>
                                    <td width="30" align="center">
                                        年龄
                                    </td>
                                    <td width="36" align="center">
                                        样本
                                    </td>
                                    <td align="center" width="65">
                                        项目
                                    </td>
                                    <td width="36" align="center">
                                        医生
                                    </td>
                                    <td width="85" align="center">
                                        开单时间
                                    </td>
                                    <td width="85" align="center">
                                        采样时间
                                    </td>
                                    <td width="65" align="center">
                                        送检单位
                                    </td>
                                    <td width="65" align="center">
                                        站点名称
                                    </td>
                                    <td align="center" width="65">
                                        诊断
                                    </td>
                                    <td width="30" align="center">
                                        修改
                                    </td>
                                    <td width="30" align="center">
                                        删除
                                    </td>
                                    <td width="30" align="center" id="td_printbarcode">
                                        打印
                                    </td>
                                </tr>
                            </table>
                            <table style="font-size: 12px">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkBPrePage" Text="上一页" runat="server" OnClick="LinkBPrePage_Click"></asp:LinkButton>
                                        当前页：<asp:Label ID="labelCurPage" runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlNewPage" runat="server">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="GotoNewPage" Text="跳转" runat="server" OnClick="GotoNewPage_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkBNextPage" Text="下一页" runat="server" OnClick="LinkBNextPage_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <input id="saveprintcount" type="button" runat="server" style="display: none;" onserverclick="saveprintcount_onclick" />
            </div>
            <input type="hidden" id="hiddenLeft" runat="server" />
            <!-- 对照控件的纵坐标 -->
            <input type="hidden" id="hiddenTop" runat="server" />
            <!-- 需要显示名称的控件 -->
            <input type="hidden" id="hiddenControlName" runat="server" />
            <!-- 需要存储值的控件(指示‘值控件’名) -->
            <input type="hidden" id="hiddenControlValue" runat="server" />
            <!-- 条码号 -->
            <input type="hidden" id="hiddenbarcode" runat="server" />
            <!-- =========查询时要找到不确定的字段，以及需要检索的表，其中项目名字段已经确定：CName========= -->
            <input type="hidden" id="hiddenColumn" runat="server" />
            <input type="hidden" id="hiddenDataTable" runat="server" />
            <!-- 用户在文本框中输入的内容 -->
            <input type="hidden" id="hiddenUserInput" runat="server" />
            <input type="hidden" id="hiddenTestCount" runat="server" />
            <input type="hidden" id="hiddentmpBarCode" runat="server" />
            <input type="hidden" id="hiddenInputClientNo" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="font-size: 12px; display: none">
        <object id="controlbyid" classid="clsid:{7796fee9-846e-40b8-be83-d394ac3c3246}">
        </object>
    </div>
    </form>
</body>
</html>
