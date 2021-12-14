<%@ Page Language="C#" AutoEventWireup="true" CodeBehind ="SubscripList.aspx.cs" Inherits="OA.DBQuery.RunExec.SubscripList" %>
<html>
<head runat="server">
    <title>申请单列表</title>
    <link href="SubScriptCss.css" media="all" rel="Stylesheet" />
    <script type="text/javascript" src="../../Includes/JS/WindowLocationSize.js"></script>   
    <!--#include file="../../Util/Calendar.js"-->
    <script type="text/javascript">
    function OpenInsertPage(flag)
    {
        //flag == '0' 表示录入新病历单
        //flag == '1' 表示修改原病历单
        var url = "SubscriptData.aspx?Flag=0"; 
        window.open(url, "申请单维护", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
    }
    function ModifyTest(BarCode)
    {
        var url = "SubscriptData.aspx?Flag=1&BarCodeNo="+BarCode;
        window.open(url, "申请单维护", "width=" + Math.floor(window.screen.width * 0.9) + ",height=" + Math.floor(window.screen.height * 0.8) + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=" + Math.floor(window.screen.height * 0.1) + ",left=" + Math.floor(window.screen.width * 0.05));
    }
    function SetRowFocus(obj,bgColor)
    {
        obj.style.backgroundColor = bgColor;
    }
    function PrintBarCode()
    {
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
    function PrintCurBarCode(BarCode, cname, Gender, Age, AgeUnit, Department, Time, Item, ClientName, SampleType, Total, ady)
    {
        //打印当前唯一条码
        if (print(BarCode, 'controlbyid', cname, Gender, Age, AgeUnit, Department, Time, Item, ClientName, SampleType, Total, ady) == 1) {
            document.getElementById('hiddentmpBarCode').value = BarCode;
            var o = document.getElementById('saveprintcount');
            o.click(); 
            //saveprintcount.onclick();
        }
    }
    function GetBarCodeList()
    {
        var hiddenTestCount = document.getElementById("hiddenTestCount");
        var BarCodeList = "";
        for(var i=1;i<hiddenTestCount.value;i++)
        {
            var ChkID = "chk_"+i;
            var chk = document.getElementById(ChkID);
            if(chk && chk.checked)
            {
                if(chk.value!="")
                {
                    BarCodeList += chk.value+";";
                }
            }
        }
        return BarCodeList;
    }
    function setAllCheckBox(obj)
    {
        var hiddenTestCount = document.getElementById("hiddenTestCount");
        for(var i=0;i<hiddenTestCount.value;i++)
        {
            var ChkID = "chk_"+i;
            var chk = document.getElementById(ChkID);
            if(chk)
            {
                chk.checked = obj.checked;
            }
        }
    }

    function SetNextFoucs()
    {
        if(event.keyCode == "13" && SelectRow == -1 )
        {
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
    function SelectClient(objstr) {

        document.getElementById('UpdatePanel1').style.position = 'absolute';
        document.getElementById('UpdatePanel1').style.index = '998';
        document.getElementById('hiddeneleid').value = objstr;
        if(!FirstTimesFlag)
        {
            FirstTimesFlag = true;
            return;
        }
        SelectRow = -1;//每次查询之前将选中行置为-1
        var obj = document.getElementById(objstr);
        var ttop =obj.offsetTop;
	    ttop +=obj.offsetHeight;
	    var tleft =obj.offsetLeft;
        var hiddenLeft = document.getElementById("hiddenLeft");
        hiddenLeft.value = tleft;
        var hiddenTop = document.getElementById("hiddenTop");
        hiddenTop = ttop;
        if (Sys.ie == "8.0") {
            var o = obj.offsetParent;
            var countt = 0;
            var countl = 0;
            while (o) {
                countt += o.offsetTop;
                countl += o.offsetLeft;
                o = o.offsetParent;
            }
            document.getElementById('UpdatePanel1').style.top = countt + 21;
            document.getElementById('UpdatePanel1').style.left = countl;
            //document.getElementById('UpdatePanel1').style.top = "121px";
            //document.getElementById('UpdatePanel1').style.left = "0px";
        }
        else {
            var o = obj.offsetParent;
            var countt = 0;
            var countl = 0;
            while (o) {
                countt += o.offsetTop;
                countl += o.offsetLeft;
                o = o.offsetParent;
            }
            document.getElementById('UpdatePanel1').style.top = countt + 21;
            document.getElementById('UpdatePanel1').style.left = countl;
        }
        var Div_Input = document.getElementById("Div_Input");
        
        var hiddenUserInput = document.getElementById("hiddenUserInput");
        hiddenUserInput.value = obj.value;
        
        var btnSearchSJDW = document.getElementById("btnSearchSJDW");
		btnSearchSJDW.click();
    }
    var SelectRow = -1;//当按下键盘上 上下  键时选中的行编号
    function SelectNextClient(obj)
    {
        var Div_Input = document.getElementById("Div_Input");
        if(Div_Input.style.display != "none")
        {
            var tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
            var totalRow = tableUserInputSJDWList.rows.length;
            if(totalRow>0)
            {
                //event.keyCode=38   向上
                if(event.keyCode == 38)
                {
                    if(SelectRow != -1)
                    {
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#a3f1f5';
                    }
                    if(SelectRow == 0 || SelectRow == -1)
                    {
                        SelectRow = totalRow-1;
                    }
                    else
                    {
                        SelectRow = SelectRow - 1;
                    }
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#AAA';
                }
                //event.keyCode=40   向下
                if(event.keyCode == 40)
                {
                    if(SelectRow != -1)
                    {
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#a3f1f5';
                    }
                    if(SelectRow == (totalRow-1))
                    {
                        SelectRow = 0;
                    }
                    else
                    {
                        SelectRow = SelectRow + 1;
                    }
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#AAA';
                }
                //event.keyCode=13   回车键
                if(event.keyCode == 13)
                {
                    if (SelectRow != -1) {
                        tableUserInputSJDWList.rows[SelectRow].click();

                        //GetClient(tableUserInputSJDWList.rows[SelectRow][].click())

                    }
                    else {
                        if (obj.value != "") {
                            tableUserInputSJDWList.rows[0].click();
                        }
                    }
                    event.keyCode = 9;
                }
            }
        }
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
    function GetClient(id, name) {
        FirstTimesFlag = false;
        var obj = document.getElementById(document.getElementById("hiddeneleid").value);
        obj.value = name;
        if (document.getElementById("hiddeneleid").value == "inputName") {
            var hiddenSelectedClient = document.getElementById("hiddenSelectedClient");
            hiddenSelectedClient.value = id;
        }
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
    </script>

</head>
<body onload="document.getElementById('UpdatePanel1').style.position = 'absolute';document.getElementById('UpdatePanel1').style.index = '998';">
    <form id="form1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Operate_Div">
        <table class="Operate_Table">
            <tr>
                <td>
                    <input type="button" name="Input" accesskey="I" onclick="OpenInsertPage();" value="录入[I]" />
                    <input type="button" name="btnPrintBarCode" accesskey="P" onclick="PrintBarCode();"
                        value="打印条码[P]" />
                </td>
            </tr>
        </table>
    </div>
    <div class="Search_Div">
        <table width="98%" height="50" border="0" cellpadding="0" cellspacing="0" style="font-size:12px">
            <tr>
                <td>
                    送检单位：<input type="text" class="inputTextList" id="inputName" onfocus="SelectClient('inputName');" onkeydown="SelectNextClient(this)" onpropertychange="SelectClient('inputName')" onblur="SelectValue(this)" tabindex="0" />
                    姓名&nbsp;&nbsp;：<asp:TextBox  ID="txtPatientName" Width="100" class="inputTextList" runat="server"  tabindex="1"></asp:TextBox>                  
                </td>
                <td width="380" >
                    开单时间：<input name="txtCollectDate" onfocus="setday(this);" type="text" class="inputTextList"
                        id="txtStartDate" runat="server" readonly />-<input name="txtCollectDate"  onfocus="setday(this);" readonly
                            type="text" class="inputTextList" id="txtEndDate" runat="server" />
                </td>
            </tr>
            <tr><td>
                    医生&nbsp;&nbsp;&nbsp;&nbsp;：<input type="text" class="inputTextList" runat="server" id="doctor" onfocus="SelectClient('doctor');" onkeydown="SelectNextClient(this)" onpropertychange="SelectClient('doctor')" onblur="SelectValue(this)" tabindex="0" />  
                    病历号：<asp:TextBox ID="txtPatientID" Width="100" class="inputTextList" runat="server"  tabindex="2"></asp:TextBox>
                </td>
                
                <td width="380">
                    采样时间：<input name="txtCollectDate"  onfocus="setday(this);" type="text" class="inputTextList" readonly
                        id="txtCollectStartDate" runat="server" />-<input name="txtCollectDate"  onfocus="setday(this);" readonly
                            type="text" class="inputTextList" id="txtCollectEndDate" runat="server" />
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
                                OnClick="btnSearch_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div_Input" style=" position: absolute; display: none;" runat="server">
                
                <table id="tableUserInputSJDWList" border="1" cellpadding="0" cellspacing="0" runat="server">
                </table>
                <iframe style="position:absolute;width:100%;height:100%;_filter:alpha(opacity=0);opacity=0;border-style:none;"></iframe> 
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="boderblue">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="1" class="pad" id="List_Table"
                                runat="server" >
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
                                     <td  align="center"  width="65">
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
                                    <td  align="center"  width="65">
                                        诊断
                                    </td>
                                    <td width="30" align="center">
                                        修改
                                    </td>
                                    <td width="30" align="center">
                                        打印
                                    </td>                                    
                                </tr>
                            </table>
                            <table  style="font-size:12px">
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
                <asp:Button ID="btnSearchSJDW" runat="server" Width="0" Style="display: none;" OnClick="btnSearchSJDW_Click" />
                <input id="saveprintcount" type="button" runat="server" style="display:none;" onserverclick="saveprintcount_onclick" />
            </div>
            <!-- 用户选中的送检单位编号 -->
            <input type="hidden" id="hiddenSelectedClient" runat="server" />
            <!-- 查询到的送检单位数量,可以省略 -->
            <input type="hidden" id="hiddenTestCount" runat="server" />
            <!-- 用户在 送检单位 文本框中输入的内容 -->
            <input type="hidden" id="hiddenUserInput" runat="server" />
            <!-- 对照控件的横坐标 -->
            <input type="hidden" id="hiddenLeft" runat="server" />
            <!-- 对照控件的纵坐标 -->
            <input type="hidden" id="hiddenTop" runat="server" />
            <!-- 用户选中的文本框的ID -->
            <input type="hidden" id="hiddeneleid" runat="server" />
            <!-- tmpBarCode -->
            <input type="hidden" id="hiddentmpBarCode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel><div style="font-size:12px;display:none"><object id="controlbyid" classid="clsid:{8f6c360e-6600-4519-98bf-dec47f5f4c4d}" codebase="../../Includes/setup.exe" ></object></div>
    </form>
</body>
</html>
