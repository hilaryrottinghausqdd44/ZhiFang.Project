<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptData.aspx.cs" Inherits="OA.DBQuery.RunExec.SubscriptData" %>

<html>
<head runat="server">
    <title>申请录入</title>
    <link href="SubScriptCss.css" rel="stylesheet" type="text/css" media="all">

    <script type="text/javascript" src="../../Includes/JS/WindowLocationSize.js"></script>   
    <!--#include file="../../Util/Calendar.js"-->
    
    <script type="text/javascript">
        var submitflag = "0";
	//设置页面上的回车为选择下一个控件
    function SetNextFoucs()
    {
        if(event.keyCode == "13")
        {
            event.keyCode = 9;
        }
    }
    //取得用户输入信息，调用“btnSelectUserInput”单击事件，查找相似项目
    function GetUserInputItem(obj)
    {
        var hiddenbtnSelectUserInput = document.getElementById("hiddenbtnSelectUserInput");
        var hiddenUserInput = document.getElementById("hiddenUserInput");
        hiddenUserInput.value = obj.value;
        if(hiddenbtnSelectUserInput && hiddenbtnSelectUserInput.value!="")
        {
            var btnSelectUserInput = document.getElementById(hiddenbtnSelectUserInput.value);
            btnSelectUserInput.onclick();
        }
    }
    var RowCount = 0;
    //页面装载时，将原有项目显示到表格中
    function SetOldTest()
    {
        var hiddenFlag = document.getElementById("hiddenFlag");
        var hiddenSelectTest = document.getElementById("hiddenSelectTest");
        if(hiddenFlag.value == "1")
        {
            //修改原有记录，首先将hiddenSelectTest中存储的值显示到“已有项目”。
            var TestInfoList = hiddenSelectTest.value.substring(0,hiddenSelectTest.value.length-1);
            TestInfoList = TestInfoList.split('|');
            for(var i = 0;i<TestInfoList.length;i++)
            {
                var CurTestNo = TestInfoList[i].split(',')[0];
                var CurTestName = TestInfoList[i].split(',')[1];
                var tmp = "";
                if(TestInfoList[i].split(',')[2]!="")
                {
                    var f = TestInfoList[i].split(',')[2];
                    var af = f.split('#');
                    
                    for (var j = 0; j < af.length; j++) {
                        tmp += "<tr style=\"background-color=#6699ff\" ><td>" + af[j].split('$')[0] + "</td><td>" + af[j].split('$')[1] + "</td><td>" + af[j].split('$')[2] + "</td></tr>";
                    }
                    tmp = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"1\" style=\"width: 300px;font-size:12px;background-color=#ffffff\">" + tmp + "</table>";
                }
                    if (CurTestNo != "") {
                    AddNewTest(CurTestNo, CurTestName, '1', null, tmp);
                }
            }
        }
        var SampleType = document.getElementById("SampleType");
        //SampleType.focus();
    }
    //TestNo:项目编号
    //TestName:项目名
    //Flag:0（插入新纪录），1（读取原有记录）
    function AddNewTest(TestNo,TestName,Flag,obj,f)
    {
        if(Flag=="0")
        {
            var hiddenSelectTest = document.getElementById("hiddenSelectTest");
            var TestInfoList = hiddenSelectTest.value.split(';');
            for(var i = 0;i<TestInfoList.length;i++)
            {
                var CurTestNo = TestInfoList[i].split(':')[0];
                if(CurTestNo == TestNo)
                {
                    alert("当前项目已存在项目列表中！不能重复选择。");
                    return;
                }
            }
            hiddenSelectTest.value += TestNo+":"+TestName+";";
        }
        RowCount++;
        
        var tr = tbTestList.insertRow();
        if (f != "" && f != "undefined") {
            tr.style.backgroundColor = '#6699ee';
        
            tr.onmousemove = function() { showpic('d', f); };



            tr.onmouseout = function() { hidden(); };
        }
        var td = tr.insertCell();
        td.innerHTML = TestName;
        var tdBtn = tr.insertCell();
        //alert(f);
        
        tdBtn.innerHTML = "<input type='button' onclick='DeleteRow(\""+TestNo+"\")' value='删除' />";
        if(obj != null)
        {
            obj.style.fontWeight = 'bold';
        }
    }
    //删除项目
    function DeleteRow(TestNo)
    {
        var tbTestList = document.getElementById("tbTestList");
        for(var i=0;i<RowCount;i++)
        {
            //清空已选项目列表
            tbTestList.deleteRow(0);
        }
        RowCount = 0;
        var hiddenSelectTest = document.getElementById("hiddenSelectTest");//已有项目
        var TestInfoList = hiddenSelectTest.value.substring(0,hiddenSelectTest.value.length-1);

        TestInfoList = TestInfoList.split(';');
        
        hiddenSelectTest.value = "";    //清空已有项目
        
        var NewList = "";
        
        for(var i = 0;i<TestInfoList.length;i++)
        {
            var CurTestNo = TestInfoList[i].split(':')[0];
            if(CurTestNo == TestNo || CurTestNo=="")
            {
                continue;
            }
            else
            {
                var CurTestName = TestInfoList[i].split(':')[1];
                AddNewTest(CurTestNo,CurTestName,'0');
                NewList += CurTestNo+":"+CurTestName+";";
            }
        }
        hiddenSelectTest.value = NewList;
    }
    function getItem()
    {
        var hiddenSelectTest = document.getElementById("hiddenSelectTest");//已有项目
        alert(hiddenSelectTest.value);
    }
    function SaveFlag(flag)
    {
        if(flag == '0')
        {
            window.alert("保存失败！");
        }
        if(flag == '1')
        {
            window.alert("保存成功！");
            var u=window.location.href;
            window.location.href = window.location.protocol + "//" + window.location.host + window.location.pathname;
        }
    }
    
    function SaveFlagPrint(flag,BarCodeFormNo,cname,Gender,Age,AgeUnit,Department,Time,Item,ClientName, SampleType, Total, ady) {
        if (flag == '0') {
            window.alert("保存失败！");
        }
        if (flag == '1') {
            if (print(BarCodeFormNo, 'controlbyid', cname, Gender, Age, AgeUnit, Department, Time, Item, ClientName, SampleType, Total, ady) == 1) {
                document.getElementById('hiddenbarcode').value = BarCodeFormNo;
                var o = document.getElementById('saveprintcount');
                o.click();
                window.alert("保存成功并打印！");
               
            }
            else {
                window.alert("保存成功打印未成功！");
            }

            window.location.href = window.location.protocol + "//" + window.location.host + window.location.pathname;
        }
    }
    function NumTest(obj)
    {
        var str = obj.value;
        if(str != "" && isNaN(str))
        {
            alert("必须为数字!");
            obj.value = "";
        }
    }
    function NumTestLength(obj,l,ll) {
        var str = obj.value;
        if (str != "" && isNaN(str)) {
            alert("必须为数字!");
            obj.value = "";
        }
        else {
            if (ll != 0) {
                if (str.length < l || str.length > ll) {
                    alert("必须为" + l + "-" + ll + "位!");
                    obj.value = "00";
                    obj.focus();
                }
            }
            else {
                if (str.length != l) {
                    alert("必须为" + l + "位!");
                    obj.value = "00";
                    obj.focus();
                }
            }
        }
    }
    </script>

    <script type="text/javascript">
    function GetPosTop(a)   
      {
          var t,l,b;  
          t = a.offsetTop;   
          while(a.tagName != "body" && a.tagName != "BODY")   
          { 
            a = a.offsetParent;   
            t = t + a.offsetTop;   
          }   
          return t;   
      }   
      function GetPosLeft(a)   
      {   
          var t,l,b;   
          t = a.offsetTop;   
          l = a.offsetLeft;   
          while(a.tagName != "body" && a.tagName != "BODY")   
          {   
            a = a.offsetParent;   
            l = l + a.offsetLeft;   
          }   
          return   l;   
      }
      
    var FirstTimesFlag = true;//统计次数
    var SelectRow = -1; //当按下键盘上 上下  键时选中的行编号
    var tmpc = "";
    var tmpn = "";
    var flag=true;
    //当用户录入时进行查询
    /*
    obj:当前操作的控件
    strHidden:需要写入ID的隐藏控件
    column:需要查询的字段名
    DataTable:需要查询的数据表名
    */
    function SelectClient(obj, strHidden, column, DataTable) {
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
            }
            else {
                document.getElementById('UpdatePanel1').style.top = "21px";
                document.getElementById('UpdatePanel1').style.left = "0px";
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
            var btnSearchInfo = document.getElementById("btnSearchInfo");
            btnSearchInfo.click();
        }
    }
    function SelectNextClient(obj)
    {
        var Div_Input = document.getElementById("Div_Input");
        if(Div_Input.style.display != "none") {
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
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#999999';
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
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#999999';
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
                    SetNextFoucs();
                }
            }
        }
    }
    function checkform(strlist) {
        var clist = strlist.split(',');
        for (var i = 0; i < clist.length; i++) {
            var clistsub = clist[i].split('=');
            if (document.getElementById(clistsub[0]).value == "") {
                alert(clistsub[1]);
                document.getElementById(clistsub[0]).focus();
                return false;
            }
        }
        return true;
    }
     var formflag = false;
    function checkform1(o) {

        formflag = checkform('txtApplyNO=申请号不能为空！,txtClientNo=送检单位不能为空！,txtName=姓名不能为空！,txtAge=年龄不能为空！,SampleType=样本类型不能为空！,txtCollecter=采样人不能为空！');
        if (formflag) {
            o.click();
        }
        else {
            form1.submit = false;
        }
    }
   
    
    //单击查询到送检单位，进行选择，还需要两个参数来确定:显示名写到那个控件，ID写到那个隐藏控件
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
    function CheckValue(o) {
        if (o.value != "") {
            return false;
        }
    }
    function GetTmpClient(c, n) {
        tmpc = c;
        tmpn = n;
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
                    tableUserInputSJDWList.rows[0].click();
                }
            }
            else { 
            
            }
            flag = false;
            GetClient(tmpc, tmpn);
        }
    }
    function ItemAdd(url) {

        var flag = window.showModalDialog(url, '新增项', 'dialogWidth:335px;dialogHeight:300px,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no,top=' + Math.floor(window.screen.height * 0.1) + ',left=' + Math.floor(window.screen.width * 0.05));
        
        if (flag == '1') {
            alert('操作成功！');
        }
        else {
            alert('操作未成功！');
        }
        document.getElementById(document.getElementById("hiddenControlName").value).focus();
    }
    function SetEnabled() {
        //form1.submit();
        document.getElementById('btnsave1').disabled = true;
        //document.getElementById('').disabled = true;
    }
    function showpic(divid, htmlstr) {
        document.getElementById(divid).innerHTML = htmlstr;
        document.getElementById(divid).style.display = "";
        var x = window.event.x;
        var y = window.event.y;
        document.getElementById(divid).style.left = x;
        document.getElementById(divid).style.top = y + document.documentElement.scrollTop;
    }
    function show2() {
        var div = document.getElementById('d');
        div.style.display = "block";
    }
    function hidden() {
        document.getElementById('d').style.display = 'none';
    }
    </script>

</head>
<body onload="SetOldTest();document.getElementById('UpdatePanel1').style.position = 'absolute';document.getElementById('UpdatePanel1').style.index = '998';" onkeydown="" style="font-size:12px">
    <form id="form1" runat="server">
    <div id="d" style=" width: 300px; position: absolute; display: none; z-index: 1000;" ></div>
    <asp:ScriptManager ID="myScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="YiZhu_Div">
        <table width="98%" border="0" cellspacing="0" cellpadding="0" class="boderblue boderbluenone">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="pad" style="font-size:12px">
                        <tr>
                            <td colspan="8" class="bottom">
                                <strong>申请录入</strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="right" nowrap="nowrap">
                                申请号:
                            </td>
                            <td><input name="text" type="text" class="inputText" id="txtApplyNO" runat="server"  tabindex="1" accesskey="0" onkeydown="SetNextFoucs();" style="border:solid 1px #d5060d;background-color:#fef5f5"/>
                                
                            </td>
                            <td align="right" nowrap="nowrap">
                                送检单位:
                            </td>
                            <td>
                                <select id="txtClientNo1" name="select" class="inputText" runat="server" tabindex="1"  onkeydown="SetNextFoucs();" visible="false">
                                </select>
                                <input name="txtClientNo" type="text" id="txtClientNo" onfocus="flag=true;SelectClient(this,'hiddenClient','WebLisSourceOrgID','Client')" onkeydown="SelectNextClient(this,'hiddenClient')" onpropertychange="SelectClient(this,'hiddenClient','WebLisSourceOrgID','Client')" onblur="SelectValue(this,'hiddenClient','WebLisSourceOrgID','Client');DisplayDiv(true);"
                                    class="inputText" runat="server"  tabindex="2"  style="border:solid 1px #d5060d;background-color:#fef5f5"/>
                            </td>
                            
                            <td align="right">
                                病例号:
                            </td>
                            <td>
                                <input name="text2" type="text" class="inputText" id="txtBingLiNO" runat="server" tabindex="2"  onkeydown="SetNextFoucs();"/>
                            </td>
                            <td align="right">
                                姓名:
                            </td>
                            <td>
                                <input name="text2" type="text" class="inputText" id="txtName" runat="server" tabindex="3"  onkeydown="SetNextFoucs();" style="border:solid 1px #d5060d;background-color:#fef5f5"/>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="right">
                                性别:
                            </td>
                            <td>
                                <select id="SelectGenderType" name="select" class="inputText" runat="server" tabindex="4" onkeydown="SetNextFoucs();" style="border:solid 1px #d5060d;background-color:#fef5f5">
                                </select>
                            </td>
                            <td align="right">
                                年龄:
                            </td>
                            <td>
                                <input name="text26" onblur="NumTest(this);" onkeypress="var   k=event.keyCode;   return   k>=48&&k<=57"  onpaste="return   !clipboardData.getData('text').match(/\D/)"    ondragenter="return   false"   style="ime-mode:Disabled;border:solid 1px #d5060d;background-color:#fef5f5" type="text" class="inputText1" id="txtAge"
                                    runat="server" tabindex="5" onkeydown="SetNextFoucs();" /><select id="SelectAgeUnit" name="select" class="inputText1" runat="server" tabindex="6"  onkeydown="SetNextFoucs();"  >
                                </select>
                            </td>
                            <td height="25" align="right" nowrap="nowrap">
                                就诊类型:
                            </td>
                            <td>
                                <select id="Selectjztype" name="select" runat="server" class="inputText" tabindex="7" onkeydown="SetNextFoucs();"  >
                                    <option value=" "> </option>
                                    <option value="1" selected="selected">住院</option>
                                    <option value="2">门诊</option>
                                    <option value="3">体检</option>                                    
                                </select>
                            </td>
                            <td align="right">
                                样本类型:
                            </td>
                            <td>
                                <input  type="text" id="SampleType" onfocus="flag=true;SelectClient(this,'hiddenSampleType','SampleTypeNo','SampleType')" onkeydown="SelectNextClient(this,'hiddenSampleType')" onpropertychange="SelectClient(this,'hiddenSampleType','SampleTypeNo','SampleType')" onblur="SelectValue(this,'hiddenSampleType','SampleTypeNo','SampleType');DisplayDiv(true);"
                                    class="inputText" runat="server"  tabindex="8"  style="border:solid 1px #d5060d;background-color:#fef5f5"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                民族:
                            </td>
                            <td>
                                <select id="SelectFolkType" name="select" class="inputText" runat="server" tabindex="9"  onkeydown="SetNextFoucs();" >
                                </select>
                            </td>
                            <td align="right">
                                病区:
                            </td>
                            <td>
                                <input type="text" id="SelectDistrict" onfocus="flag=true;SelectClient(this,'hiddenDistrict','DistrictNo','District')" onkeydown="SelectNextClient(this,'hiddenDistrict')"
                                    onpropertychange="SelectClient(this,'hiddenDistrict','DistrictNo','District')" onblur="SelectValue(this,'hiddenDistrict','DistrictNo','District');DisplayDiv(true);"
                                    class="inputText" runat="server" tabindex="10"  />
                            </td>
                            <td align="right">
                                病房:
                            </td>
                            <td>
                                <input type="text" id="SelectWardType" onfocus="flag=true;SelectClient(this,'hiddenWardNo','WardNo','WardType')" onkeydown="SelectNextClient(this,'hiddenWardNo')" 
                                onpropertychange="SelectClient(this,'hiddenWardNo','WardNo','WardType')"  onblur="SelectValue(this,'hiddenWardNo','WardNo','WardType');DisplayDiv(true);"
                                    class="inputText" runat="server" tabindex="11"  />
                            </td>
                            <td align="right">
                                床位:
                            </td>
                            <td>
                                <input name="text23" type="text" class="inputText" id="txtBed" runat="server" tabindex="12"  onkeydown="SetNextFoucs();"  />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="right">
                                科室:
                            </td>
                            <td id="aaa">
                                <input type="text" id="SelectDepartment" onfocus="flag=true;SelectClient(this,'hiddenDepartment','DeptNo','Department')" onkeydown="SelectNextClient(this,'hiddenDepartment');"
                                    onpropertychange="SelectClient(this,'hiddenDepartment','DeptNo','Department')" onblur="SelectValue(this,'hiddenDepartment','DeptNo','Department');DisplayDiv(true);"
                                    class="inputText" runat="server" tabindex="13"  />
                            </td>
                            <td align="right">
                                医生:
                            </td>
                            <td>
                                <input type="text" id="SelectDoctor"  onfocus="flag=true;SelectClient(this,'hiddenDoctorNo','DoctorNo','Doctor')" onkeydown="SelectNextClient(this,'hiddenDoctorNo')"
                                    onpropertychange="SelectClient(this,'hiddenDoctorNo','DoctorNo','Doctor')" onblur="SelectValue(this,'hiddenDoctorNo','DoctorNo','Doctor');DisplayDiv(true);"
                                    class="inputText" runat="server" tabindex="14"  />
                            </td>
                            <td align="right">
                                诊断结果:
                            </td>
                            <td>
                                <input name="txtResult" type="text" class="inputText" id="txtResult" runat="server" tabindex="15" onkeydown="SetNextFoucs();"   />
                            </td>
                            <td align="right">
                                检验类型:</td>
                            <td><select id="DDLTestType" name="DDLTestType" class="inputText" runat="server" tabindex="16"  onkeydown="SetNextFoucs();" >
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="right">
                                收费:
                            </td>
                            <td>
                                <input name="txtFee" onblur="NumTest(this);" onkeypress="var   k=event.keyCode;   return   k>=48&&k<=57||k==46||k==45" onpaste="return   !clipboardData.getData('text').match(/\D/)" ondragenter="return   false"   style="ime-mode:Disabled" type="text" class="inputText" id="txtCharge"
                                    runat="server" tabindex="17"  onkeydown="SetNextFoucs();"  />
                            </td>
                            <td align="right">
                                开单时间:
                            </td>
                            <td><input name="txtoTime" onfocus="setday(this);" type="text" id="txtoTime" runat="server" maxlength="150" size="9" width="150" tabindex="18"  onkeydown="SetNextFoucs();" readonly  />
                            <input name="txtoTimeH"  onblur="NumTestLength(this,1,2);" onkeypress="var   k=event.keyCode;   return   k>=48&&k<=57"  onpaste="return   !clipboardData.getData('text').match(/\D/)"    ondragenter="return   false"   style="ime-mode:Disabled" type="text" id="txtoTimeH" runat="server" maxlength="150" size="2" width="150" tabindex="19"  onkeydown="SetNextFoucs();"  />:<input name="txtoTimeM"  onblur="NumTestLength(this,1,2);" onkeypress="var   k=event.keyCode;   return   k>=48&&k<=57"  onpaste="return   !clipboardData.getData('text').match(/\D/)"    ondragenter="return   false"   style="ime-mode:Disabled" type="text" id="txtoTimeM" runat="server" maxlength="150" size="2" width="150" tabindex="20"  onkeydown="SetNextFoucs();"  />
                            <script>
                                function clear() { document.getElementById('txtoTime').value = ''; document.getElementById('txtoTimeH').value = ''; document.getElementById('txtoTimeM').value = ''; }

                            </script><a href="javascript:clear();" >清空</a>
                            </td>
                            <td align="right">
                                采样人:
                            </td>
                            <td>
                                <input name="txtCollecter" type="text" class="inputText" id="txtCollecter" runat="server" tabindex="21" onkeydown="SetNextFoucs();"   />
                            </td>
                            <td align="right" nowrap="nowrap">
                                采样时间:
                            </td>
                            <td  style="border:solid 1px #d5060d;background-color:#fef5f5"><input name="txtCollectTime" onfocus="setday(this);" type="text" id="txtCollectTime" runat="server" maxlength="150" size="9" width="150" tabindex="22"  onkeydown="SetNextFoucs();" readonly />
                            <input name="txtCollectTimeH"  onblur="NumTestLength(this,1,2);" onkeypress="var   k=event.keyCode;   return   k>=48&&k<=57"  onpaste="return   !clipboardData.getData('text').match(/\D/)"    ondragenter="return   false"   style="ime-mode:Disabled" type="text" id="txtCollectTimeH" runat="server" maxlength="150" size="2" width="150" tabindex="23"  onkeydown="SetNextFoucs();"  />:<input name="txtCollectTimeM"  onblur="NumTestLength(this,1,2);" onkeypress="var   k=event.keyCode;   return   k>=48&&k<=57"  onpaste="return   !clipboardData.getData('text').match(/\D/)"    ondragenter="return   false"   style="ime-mode:Disabled" type="text" id="txtCollectTimeM" runat="server" maxlength="150" size="2" width="150" tabindex="24"  onkeydown="SetNextFoucs();"  />
                            </td>
                        </tr>
                    </table>
                
    <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <div id="Div_Input" style="position: absolute; z-index: 999; display: none; font-size:12px" runat="server">
                <table id="tableUserInputSJDWList" border="1" cellpadding="0" cellspacing="0" runat="server">
                </table>
                <iframe style="position:absolute;width:100%;height:100%;_filter:alpha(opacity=0);opacity=0;border-style:none;"></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel></div></td>
            </tr>
        </table></div>
    <div class="Item_Div">
        <table width="98%" border="0" cellspacing="0" cellpadding="0" class="boderblue">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="pad">
                        <tr>
                            <td colspan="2" class="bottom">
                                <strong>项目</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25">
                                添加新项目:
                                <input type="text" onpropertychange="GetUserInputItem(this);" id="Text1" runat="server" tabindex="25" style="border:solid 1px #d5060d;background-color:#fef5f5"/>
                                <input type="button" value="测试已有项目" style="display:none;" onclick="getItem();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="98%" border="0" cellspacing="0" cellpadding="0" class="boderblue">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="pad">
                        <tr>
                            <td width="40%" valign="top" height="25" align="left">
                                <strong>已有项目:</strong><br />
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding:0px; margin:0px;" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table id="tbTestList" style="padding:0px; margin:0px;font-size:12px" bgcolor="#a3f1f5" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0"
                                                class="CheckedItem_Table">
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="60%" valign="top" align="left">
                                <asp:UpdatePanel ID="UpdatePanelItemList" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="linkGetAllItem" runat="server" Text="全部" OnClick="linkGetAllItem_Click"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="linkGetCommonItem" runat="server" Text="常用" OnClick="linkGetCommonItem_Click"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="linkGetManaItem" runat="server" Text="管理" OnClick="linkGetManaItem_Click"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="linkGetLCItem" runat="server" Text="临检" OnClick="linkGetLCItem_Click"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="linkGetSHItem" runat="server" Text="生化" OnClick="linkGetSHItem_Click"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="linkGetMYItem" runat="server" Text="免疫" OnClick="linkGetMYItem_Click"></asp:LinkButton>
                                        <input type="button" id="btnSelectUserInput" runat="server" style="display:none" onserverclick="btnSelectUserInput_Click" />
                                        <br />
                                        <asp:GridView Width="100%" ID="gvItem" BorderWidth="1" AllowPaging="true" PageSize="15" CssClass="pad"
                                            AutoGenerateColumns="false" runat="server" BorderColor="#00CCFF" BorderStyle="Solid"
                                            OnPageIndexChanging="gvItem_PageIndexChanging" OnRowDataBound="gvItem_RowDataBound1">
                                            <HeaderStyle BorderColor="#55aaff" BackColor="#55aaff" BorderWidth="1" />
                                            <RowStyle BorderColor="BlanchedAlmond" BorderWidth="1" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        项目
                                                     </HeaderTemplate>
                                                    <ItemTemplate>                                                    
                                                        <%# DataBinder.Eval(Container.DataItem, "cname")%>
                                                        <asp:Literal ID="literalTestID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "itemno")%>'></asp:Literal>
                                                        <asp:Literal ID="literalTestName" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "cname")%>'></asp:Literal>
                                                        <asp:Literal ID="IsProfile" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "IsProfile")%>' ></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        费用
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Literal ID="Price" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "price")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        名称
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ShortName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "shortname")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        意义
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        意义
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table height="30" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:UpdatePanel ID="OperateList" runat="server">
                        <ContentTemplate>
                        <input id="btnsave" type="button" runat="server"  value="保存[S]" onclick="this.disabled=true;document.getElementById('btnsaveprint').disabled=true;" AccessKey="S" onserverclick="btnsave_onclick" />
                            &nbsp;<input id="btnsaveprint" type="button" runat="server"  value="保存并打印" onclick="this.disabled=true;document.getElementById('btnsave').disabled=true;" onserverclick="btnsaveprint_onclick" />
                            <input id="saveprintcount" type="button" runat="server" style="display:none;" onserverclick="saveprintcount_onclick" />
                            
                            <input type="button" id="btnAbandon" onclick="window.close();" value="放弃" />
                            <asp:Label ID="label_backValue" runat="server"></asp:Label>
                            <asp:Button ID="btnSearchInfo" Style="display: none;" runat="server" OnClick="btnSearchInfo_Click" />
                            <asp:Button ID="btnSearchInfoByName" Style="display: none;"  runat="server" onclick="Button1_Click" />
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel><div style="font-size:12px;display:none"><object id="controlbyid" classid="clsid:{8f6c360e-6600-4519-98bf-dec47f5f4c4d}" codebase="../../Includes/setup.exe" ></object></div>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="HiddenValue" runat="server">
    <ContentTemplate>
    <input type="hidden" id="hiddenbtnSelectUserInput" runat="server" /><!-- 隐藏按钮的客户端ID -->
    <input type="hidden" id="hiddenFlag" runat="server" /><!-- 0新项目  1修改旧项目 -->
    <input type="hidden" id="hiddenSelectTest" runat="server" /><!-- 用户选择的项目  10000:血液病检查;10001:临床检验; -->
    <!-- 用户选中的送检单位编号 -->
    <input type="hidden" id="hiddenSelectedClient" runat="server" />
    <!-- 对照控件的横坐标 -->
    <input type="hidden" id="hiddenLeft" runat="server" />
    <!-- 对照控件的纵坐标 -->
    <input type="hidden" id="hiddenTop" runat="server" />
    <!-- 送检单位 -->
    <input type="hidden" id="hiddenClient" runat="server" />
    <!-- 样本类型 -->
    <input type="hidden" id="hiddenSampleType" runat="server" />
    <!-- 医生 -->
    <input type="hidden" id="hiddenDoctorNo" runat="server" />
    <!-- 病区 -->
    <input type="hidden" id="hiddenDistrict" runat="server" />
    <!-- 病房 -->
    <input type="hidden" id="hiddenWardNo" runat="server" />
    <!-- 科室 -->
    <input type="hidden" id="hiddenDepartment" runat="server" />
    <!-- 检测类型 -->
    <input type="hidden" id="hiddenTestTypeNo" runat="server" />
    <!-- ====================================临时存储============================================= -->
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
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>