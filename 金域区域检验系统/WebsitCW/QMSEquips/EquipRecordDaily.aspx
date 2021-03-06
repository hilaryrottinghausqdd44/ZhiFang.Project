<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipRecordDaily.aspx.cs" Inherits="OA.QMSEquips.EquipRecordDaily" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>仪器日维护保养记录</title>
    <style type="text/css">
        table
        {
        	border:1px;
        }
        .STYLE1 {color: #FFFFFF}
        .STYLE5 {
	        color: #e14d4d;
	        font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">
    function getOperatorChange(obj)
    {
        var objHiddenOperator = document.getElementById("hiddenOperatorChange");
        var addvalue = (obj.value+"|"+obj.id)+"@";
        objHiddenOperator.value += addvalue;
    }
    
    //将全部操作者姓名写入隐藏控件，形式：姓名1|姓名2...
    function getOperatorName()
    {
        var maxDay = 31;
        var operatorNameID = "txt_";
        var objHiddenOperator = document.getElementById("hiddenOperatorName");
        objHiddenOperator.value = "";//清空原有值
        for(var i=1;i<=maxDay;i++)
        {
            var txtboxOperatorName = document.getElementById(operatorNameID+i);
            if(txtboxOperatorName != null)
            {
                objHiddenOperator.value += (txtboxOperatorName.value+"|");
            }
        }
    }
    
    
    //将本次修改过的维护记录写入隐藏控件 hiddenAllRecordInfo
    function selectRecord(obj)
    {
        var recordInfo = document.getElementById("hiddenAllRecordInfo");//取得隐藏控件，存储页面上修改过的记录
        //隐藏控件存储内容的格式：复选框ID|选中状态|value@复选框ID|选中状态|value@。。。
        var cellID = obj.id;//当前操作的记录，记录编号格式：1_4表示：1代表维护项目在数据库中存储的ID,4代表维护的星期
        var cellChecked = obj.checked;//记录的选中状态
        var cellValue = obj.value;//维护内容
        
        //详细信息复选框
        var curDayCheck = document.getElementById("check_"+cellID.split('_')[0]);
        if(curDayCheck && !selectTwiceTime)
        {
            curDayCheck.checked = cellChecked;
        }

        var cellHaveChecked = false; //标志符，true表示当前记录修改过
        
        var allRecords = recordInfo.value.toString();
        if(allRecords.length>0)
        {
            allRecords = allRecords.substring(0,allRecords.length-1);
        }
        var recordInfoDetails = allRecords.split('@');
        
        //查看当前记录是否被修改过
        for(var i=0;i<recordInfoDetails.length;i++)
        {
            var dateRecord = recordInfoDetails[i].split('|');//具体一周的维护记录(ID,checked,value)
            
            var curRecordID = dateRecord[0];//维护项目ID
            if(curRecordID == cellID)
            {
                if(!selectTwiceTime)//查看当前记录是否被修改过（第二次修改）
                {
                    recordInfoDetails[i] = "";   //如果维护过则删除记录
                    cellHaveChecked = true;
                }
                else
                {
                    recordInfoDetails[i] = obj.id+"|"+cellChecked+"|"+cellValue;
                    selectTwiceTime = false;
                    cellHaveChecked = true;
                }
            }
        }
        
        //记录第一次被操作，则直接写入隐藏控件，格式  复选框ID|复选框状态@复选框ID|复选框状态@
        if(!cellHaveChecked)
        {
             recordInfo.value += obj.id+"|"+cellChecked+"|"+cellValue+"@";
        }
        //如果记录被修改过则重新拼接数组recordInfoDetails，数据内容在上面已经被修改过
        else
        {
            var tempRecord = "";
            for(var k=0;k<recordInfoDetails.length;k++)
            {
                if(recordInfoDetails[k] == "")//字段为空则跳过
                {
                    continue;
                }
                tempRecord += recordInfoDetails[k]+"@";
            }
            recordInfo.value = tempRecord;
        }
    }

    var curLocation = window.location.toString();
    var htmlUrl = curLocation.substring(0,curLocation.indexOf('?'));
    var parameter = curLocation.substring(curLocation.indexOf('?')+1,curLocation.length);
    var parameters = parameter.split('&');
    
    function fillMonthLink()
    {
	var PanelDateLink = document.getElementById("PanelDateLink");
	if(!PanelDateLink)
	{
		return false;
	}


	var dateString="";
        for(var p=0;p<parameters.length;p++)
        {
            var evenParameterValue = parameters[p].split('=');
		if(evenParameterValue[0].toLowerCase() == "recordmonth")
            if(evenParameterValue[0].toLowerCase() == "recordmonth")
            {
                //传递的参数包含日期
		if(evenParameterValue[1]==null || evenParameterValue[1] == '')
{
var date = new Date();
dateString = date.getYear()+"-"+(date.getMonth()+1)+"-01";
evenParameterValue[1] = dateString;
}
                
                    var dateInfo = evenParameterValue[1].split('-');

                    for(var m=1;m<=12;m++)
                    {
                        var newHref = "";
                        var newParameter = "";
                        var linkID = "a_"+m;
                        var monthLink = document.getElementById(linkID);
                        if(m<10)
                        {
                            evenParameterValue[1] = dateInfo[0]+"-0"+m+"-"+dateInfo[2];
                        }
                        else
                        {
                            evenParameterValue[1] = dateInfo[0]+"-"+m+"-"+dateInfo[2];
                        }
                        parameters[p] = "recordMonth"+"="+evenParameterValue[1];
 
                        for(var newP=0;newP<parameters.length;newP++)
                        {
                            newParameter += (parameters[newP]+'&');
                        }
                        newHref = htmlUrl+'?'+newParameter.substring(0,newParameter.length-1);
                        monthLink.href=newHref;
                    }
                break;
            }
            else
            {
                continue;
            }
        }
    }
    
    //在年份输入框上单击回车按钮时重新加载页面
    function reloadPage()
    {
        if(event.keyCode == 13)
        {   
            var newParameter = "";
            var newYear = document.getElementById("year");
            var newInputYear = newYear.value;
            
            if(isNaN(newInputYear) || newInputYear.length != 4)
            {
                alert("年份必须为4位数字类型。");
                return false;
            }
var dateString="";
            for(var p=0;p<parameters.length;p++)
            {
                var evenParameterValue = parameters[p].split('=');
                if(evenParameterValue[0].toLowerCase() == "recordmonth")
                {
			if(evenParameterValue[1] ==null || evenParameterValue[1] == '')
{
var date = new Date();
dateString = date.getYear()+"-"+(date.getMonth()+1)+"-01";alert(dateString );
evenParameterValue[1] = dateString;
}
                    //传递的参数包含日期
                    //if(evenParameterValue[1]!=null && evenParameterValue[1] != '')
                    //{
                        var dateInfo = evenParameterValue[1].split('-');

                        dateInfo[0] = newYear.value;
                        var newDate = dateInfo[0]+"-"+dateInfo[1]+"-"+dateInfo[2];
                        parameters[p] = "recordMonth"+"="+newDate;

                    break;
                }
            }
            for(var newP=0;newP<parameters.length;newP++)
            {
                newParameter += (parameters[newP]+'&');
            }
            var newHref = htmlUrl+'?'+newParameter.substring(0,newParameter.length-1);

            //取消表单的提交事件，防止页面在原有基础上刷新，无法打开新纪录
            document.getElementById("form1").onsubmit=function notSubmit(){return false;};
            window.location.href = newHref;
            
        }
    }
    //如果记录包含文字信息则允许鼠标放到复选框上显示提示
    //参数obj表示鼠标移动到的复选框
    function  showRecordDetailInfo(obj)
    {
        var strValue = "";
        strValue = obj.value;
        if(strValue != null &&　strValue!= "")
        {
            var left = document.documentElement.scrollLeft+event.clientX+10;
            var top = document.documentElement.scrollTop+event.clientY;
            
            var divInfo = document.getElementById("divRecordDetailInfo");
            divInfo.innerHTML = strValue;
            divInfo.style.marginLeft = left;
            divInfo.style.marginTop = top;
            divInfo.style.display = "";
        }
    }
    //鼠标移出复选框取消DIV显示
    function hideDIV()
    {
        var divInfo = document.getElementById("divRecordDetailInfo");
        divInfo.style.display = "none";
    }
    //显示维护信息录入页面
    function showInputRecord()
    {
        inputRecord.style.display='';
    }
    
    //取得当天维护信息
    function getTodayRecord1()
    {
        var inputCheckBoxs = document.getElementsByName("inputCheckBox");//当天维护信息复选框
        var inputTextBoxes = document.getElementsByName("inputTextBox");//当天/周维护信息文本框
        
        
        var hiddenTodayRecord = document.getElementById("hiddenTodayRecord");//存储当天维护信息

        if(curDay == "")//当前周
        {
            var hiddenDay = document.getElementById("hiddenDay");
            curDay = hiddenDay.value;
        }
        
        var todayRecordInfo = "";//new Array(inputCheckBoxs.length);
        
        for(var r = 0; r<inputCheckBoxs.length; r++)
        {
            var ProjectID = inputCheckBoxs[r].id.split('_')[1];
            var inputTextBox = document.getElementById("input_"+ProjectID);//当天相同维护项目记录信息
            todayRecordInfo += ProjectID;
            todayRecordInfo += "#";//复选框ID与复选框之间的分隔
            todayRecordInfo += inputCheckBoxs[r].checked;
            todayRecordInfo += "|";//复选框与文本框之间的分隔
            todayRecordInfo += inputTextBox.value;
            todayRecordInfo += "@";//维护项目之间的分隔
        }
        var index = todayRecordInfo.lastIndexOf('@');
        todayRecordInfo = todayRecordInfo.substring(0,index);
        hiddenTodayRecord.value = todayRecordInfo;
        
        return true;
    }
    
    
    //取得当天维护信息
    var selectTwiceTime = false;//详细信息是在总体表格修改结束后进行的操作则 为true
    function getTodayRecord()
    {
        var inputCheckBoxes = document.getElementsByName("inputCheckBox");//当天/周维护信息复选框
        var inputTextBoxes = document.getElementsByName("inputTextBox");//当天/周维护信息文本框

        if(curDay == "")//当天
        {
            var hiddenDay = document.getElementById("hiddenDay");
            curDay = hiddenDay.value;
        }
        for(var r = 0; r<inputCheckBoxes.length; r++)
        {
            var ProjectID = inputCheckBoxes[r].id.split('_')[1];//项目编号
            var inputTextBox = document.getElementById("input_"+ProjectID);//本周相同维护项目记录信息（文本框内容）

            var otherInputID = "oth_"+ProjectID+"_"+curDay;//如果不是复选框，则需填充相应控件
            var otherInput = document.getElementById(otherInputID);
            if(otherInput)
            {
                otherInput.value = inputTextBox.value;
                
                //changeChkValue(otherInput);
            }
            
            
            var detailCheckID = ProjectID+"_"+curDay; //当前记录对应的 维护信息复选框
            
            var detailCheck = document.getElementById(detailCheckID);
            if(detailCheck)
            {
                detailCheck.value = inputTextBox.value;
                if(inputTextBox.value != "")
                {
                    detailCheck.parentNode.style.backgroundColor = "#8080ff";
                }
                else
                {
                    detailCheck.parentNode.style.backgroundColor = "#d3dfee";
                }
                
                selectTwiceTime = true;
                //调用维护记录复选框的单击事件
                selectRecord(detailCheck);
            }
            
            
        }
        //隐藏录入框
        var inputRecord = document.getElementById("inputRecord");
        inputRecord.style.display='none';
    }
    
    
    function changeChkValue(obj)
    {
        var id = obj.id;
        id = id.substring(4,id.length);//取得隐藏复选框
        
        var inputID = "input_"+id.split('_')[0];//本周录入内容的文本框
        
        var chk = document.getElementById(id);

        
        if(chk)
        {
            chk.value = obj.value;
            selectRecord(chk);
        }
        var input = document.getElementById(inputID);
        if(input)
        {
            input.value = obj.value;
        }
    }
    
    var QX = "";
    
    var url = window.location;
    var windowUrl = url.toString();
    var urlPares = windowUrl.split("?");
    var pares =  urlPares[1].split("&");
    for(var i=0;i<pares.length;i++)
    {
        var userQX = pares[i].split("=");
        if(userQX[0] == "userQX")
        {
            QX = userQX[1];
            QX = QX.toLowerCase();
        }
    }
    
    //将修改的日期（星期）写入隐藏控件（如：2009-04#3表示2009年4月第三周）
    var curDay = "";
    function getModefyDay(str,obj)
    {
        var hiddenToday = document.getElementById("hiddenToday");
        var curToday = hiddenToday.value;//今天的日期 “2009-5-14”

        curDay = str.split('#')[1];//当前选中日期
        
        var curDate = str.split('#')[0] +"-"+curDay;
        
        var disabled = false;//详细信息不可修改
        if(curToday != curDate && QX!="completecontrol")
        {
            disabled = true;
        }

        var cell = equipRecordTable.rows[0].childNodes.length;
        for(var i = 1 ;i<cell;i++)
        {
            equipRecordTable.rows[0].childNodes[i].style.backgroundColor="#4f81bd";
        }
        equipRecordTable.rows[0].childNodes[curDay].style.backgroundColor="#b07e32";

        //循环读取表格当前列，将当前列的内容写入详细表格
        var records = equipRecordTable.rows.length;
        for(var r = 1;r<records-1;r++)
        {
            //取得单元格内的复选框
            for(var c = 0 ;c<equipRecordTable.rows[r].childNodes[curDay].childNodes.length;c++)
            {
                var checkBox = equipRecordTable.rows[r].childNodes[curDay].childNodes[c];
                
                if(checkBox.tagname = "input" && checkBox.type == "checkbox")
                {
                    var checkBoxID = checkBox.id;
                    var equipID = checkBox.id.split('_')[0];
                    var detailCheck = document.getElementById("check_"+equipID);
                    var detailText = document.getElementById("input_"+equipID);
                   
                    if(detailCheck)
                    {
                        detailCheck.checked = checkBox.checked;
                        detailCheck.disabled = disabled;
                    }
                    if(detailText)
                    {
                        detailText.value = checkBox.value;
                        detailText.disabled = disabled;
                    }
                }
            }
        }
    }
    
    //修改每周维护的复选框，同时修改详细维护内容的复选框
    //单击每天详细维护记录复选框时触发
    function checkDetailCheck(obj)
    {
        var projectId = obj.id.split('_')[1];
        if(curDay == "")
        {
            var hiddenDay = document.getElementById("hiddenDay");
            curDay = hiddenDay.value;
        }
        var detailCheckID = projectId+"_"+curDay
        var detailCheck = document.getElementById(detailCheckID);
        if(detailCheck)
        {
            detailCheck.checked = obj.checked;
        }
    }
   </script>
</head>
<body onload="fillMonthLink();">
    <form id="form1" runat="server">
    <div id="divRecordDetailInfo" style="border:1px; border-color:Yellow; background-color:#FFFF33; position:absolute; width:100px; height:20px; display:none;"></div>
    <div>
    <font size="2">
    <table border="0">
        <tr>
            <td width="1000" align="center">
                <b><asp:Label ID="equipRecordHead" runat="server"  Text="每日维护保养记录表"></asp:Label></b>
            </td>
        </tr>
        
        <tr>
            <td align="left">&nbsp;&nbsp;记录时间：<asp:TextBox ID="year" Width="40px" 
                    BorderStyle="Groove" runat="server"></asp:TextBox>年<asp:Label ID="month" runat="server" Text=""></asp:Label>月
            &nbsp;
            <span ID="PanelDateLink" Visible="false" runat="server">
                <a id="a_1">1月</a>
                <a id="a_2">2月</a>
                <a id="a_3">3月</a>
                <a id="a_4">4月</a>
                <a id="a_5">5月</a>
                <a id="a_6">6月</a>
                <a id="a_7">7月</a>
                <a id="a_8">8月</a>
                <a id="a_9">9月</a>
                
                <a id="a_10">10月</a>
                <a id="a_11">11月</a>
                <a id="a_12">12月</a>
            </span>
            </td>
        </tr>
    </table>
    <table id="equipRecordTable" cellpadding=0 cellspacing=0 border=1 runat="server">
    </table>
    <table border=0 width="1000">
    <tr>
        <td align="left"><b>注：完成后打钩</b><asp:Button ID="btnSaveRecord" runat="server" Text="保存" 
            onclick="btnSaveRecord_Click" />
    <font size=2>
            <input type="button" id="btnShowInput" visible="false" value="填写今天维护记录" onclick="showInputRecord();" runat="server" /></font>
        </td>
    </tr>
    </table>
    </font>
    </div>
    <div id="inputRecord" style="display:none;">
    <table border="1" width="600" style=" border-bottom:none; margin-bottom:0px;" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="2">
                <b>
                    填写维护记录
                </b>
            </td> 
        </tr>
    </table>
    <table id="inputTable" runat="server" width="600" border="1" cellpadding="0" cellspacing="0">
    </table>

    <table border="1" width="600" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" style="border-top:none; margin-top:0px;">
                <input type="button" value="确定" onclick="getTodayRecord();" />
            </td>
        </tr>
    </table>
    </div>
    <input type="hidden" id="hiddenEquipID" runat="server" />
    <input type="hidden" id="hiddenAllRecordInfo" runat="server" /><!-- 修改过的维护记录 -->
    <input type="hidden" id="hiddenOperatorName" runat="server" /><!-- 页面上全部操作者姓名 -->
    <input type="hidden" id="hiddenOperatorChange" runat="server" /><!-- 修改过的操作者姓名 -->
    
    <input type="hidden" id="hiddenToday" runat="server" /><!-- 当前日期：2009-5-14 -->
    
    <input type="hidden" id="hiddenDay" runat="server" /><!-- 当前日期（1-31） -->
    </form>
</body>
</html>
