<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="askForLeave.aspx.cs" Inherits="OA.EmployeeEvents.Employee.askForLeave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>请假申请</title>
    <script type="text/javascript" src="../../Includes/JS/calendarDateTime.js"></script>
    <style type="text/css">
    body
    {
    	font-size:small;
    }
    table
    {
    	border-color:#339955;
    	border:1px;
    	width:600px;
    	height:400px;
    	border-bottom-color:#339955;
    	border-style:groove;
    }
    
    textarea
    {
    	width:500px;
    	height:100px;
    	margin-left:10px;
    }
    
    .head
    {
    	height:20px;
    	background-color:#339955;
    	text-align:left;
    	border:1px;
    }
    .TableBody
    {
    	border-color:#339955;
    	text-align:left;
    	border:1px;
    }
    .TableFoot
    {
    	border-color:#339955;
    	text-align:center;
    	border:1px;
    }
    </style>
    <script type="text/javascript">
    function checkUserInput()
    {
        var ddlLeaveClass = document.getElementById("ddlLeaveClass");
        if(ddlLeaveClass.value == "0")
        {
            alert("请选择请假类别！");
            return false;
        }
        
        var textApplyStartTime = document.getElementById("textApplyStartTime");
        var textApplyEndTime = document.getElementById("textApplyEndTime");
        if(textApplyStartTime.value == "" || textApplyEndTime.value == "")
        {
            alert("请填写申请时间！");
            return false;
        }
    }
    
    function chooseEmp()
    {
        var userID;//用户信息：350|李健
        userID = showModalDialog('../../RBAC/Organizations/searchperson.aspx','','width=700px,height=650px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-650)/2 );
        var hiddenEmpID = document.getElementById("hiddenEmpID");
        if(userID!=null && userID!="")
        {
            hiddenEmpID.value = userID;
            var textEmployeeLeaveName = document.getElementById("textEmployeeLeaveName");
            var uName = userID.split('|')[1];
            textEmployeeLeaveName.innerText = uName;
        }
        return false;
    }
    function checkTotalTime()
    {
        var textApplyStartTime = document.getElementById("textApplyStartTime");
        var textApplyEndTime = document.getElementById("textApplyEndTime");
        
        if(textApplyStartTime.value == "" || textApplyEndTime.value == "")
        {
            return;
        }
        else
        {
            //alert(textApplyStartTime.value);checkDateTime
            var dt = OA.EmployeeEvents.Employee.askForLeave.checkDateTime(textApplyStartTime.value,textApplyEndTime.value);
            dtcheck = dt.value;
            
            var hour = document.getElementById("hour");
            hour.innerHTML = dtcheck.split('#')[1];
            var day = document.getElementById("day");
            day.innerHTML = dtcheck.split('#')[0];
        }
    }
    </script>
</head>
<body style="font-size:12px;">
    <form id="form1" runat="server">
    <div style=" text-align:right;">
        <img src="../../Images/diary/pre.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.history.go(-1);" alt="前一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/next.gif" onmouseover="this.style.cursor='pointer'" onclick="window.history.go(1);" alt="后一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/main.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.parent.location=window.parent.location;" alt="主页" width="60" height="24" />&nbsp;
    </div>
    <div>
        <table border="1" cellpadding="0" cellspacing="0">
            <thead>
                <tr>
                    <td class="head">
                    &nbsp;&nbsp;请假申请
                    <asp:Button ID="btnChooseLeaveEmp" Text="员工选择" Visible="false" runat="server" />
                    <label id="textEmployeeLeaveName" runat="server"></label>
                    </td>
                </tr>
            </thead>
            <tr>
                <td class="TableBody" valign="top">
                    &nbsp;&nbsp;请假类别：&nbsp;<asp:DropDownList ID="ddlLeaveClass" runat="server"></asp:DropDownList><br />
                    &nbsp;&nbsp;申请时间：
                    <asp:TextBox ID="textApplyStartTime" onfocus="setday(this)" onpropertychange="checkTotalTime();" Width="140" runat="server"></asp:TextBox>&nbsp;至
                    <asp:TextBox ID="textApplyEndTime" onfocus="setday(this)" onpropertychange="checkTotalTime();" Width="140" runat="server"></asp:TextBox>
                    <br />
                    <!--
                    &nbsp;&nbsp;请假期限：
                    <asp:Label ID="LabelApproveStartTime" BorderStyle="Solid" BorderWidth="1" Width="143" runat="server"></asp:Label>&nbsp;至
                    <asp:Label ID="LabelApproveEndTime" BorderStyle="Solid" BorderWidth="1" Width="143" runat="server"></asp:Label>
                    <br /-->
                    &nbsp;&nbsp;共&nbsp;&nbsp;&nbsp;&nbsp;计：
                    <asp:Label ID="hour"  BorderWidth="1" Width="30" runat="server"></asp:Label>小时
                    <asp:Label ID="day"  BorderWidth="1" Width="30" runat="server"></asp:Label>天
                    <br />
                    &nbsp;&nbsp;事由：
                    <br />
                    <textarea id="signLeaveRemark" rows="8" cols="250" runat="server"></textarea>
                    <br />
                    &nbsp;&nbsp;说明：
                    <br />
                    <textarea id="signLeaveExplanation" rows="8" cols="250" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="TableFoot">
                    <asp:Button ID="btnAskForLeave" Text="提交主管" runat="server" onclick="btnAskForLeave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiddenEmpID" runat="server" /><!-- 存储员工信息 id|name -->
    </form>
</body>
</html>
