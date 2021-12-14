<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signOut.aspx.cs" Inherits="OA.EmployeeEvents.Employee.signOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>外出登记</title>
    <script type="text/javascript" src="../../Includes/JS/calendarDateTime.js"></script>
    <style type="text/css">
    table
    {
    	border-color:#339955;
    	border:1px;
    	width:600px;
    	height:400px;
    	border-bottom-color:#339955;
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
    }
    .TableBody
    {
    	border-color:#339955;
    	text-align:left;
    }
    .TableFoot
    {
    	border-color:#339955;
    	text-align:center;
    }
    </style>
    
    <script type="text/javascript">
    function checkUserInput()
    {
        var textStartTime = document.getElementById("textStartTime");
        var textEndTime = document.getElementById("textEndTime");
        if(textStartTime.value == "" || textEndTime.value == "")
        {
            alert("外出时间必须填写！");
            return false;
        }
        
        var textDeparture = document.getElementById("textDeparture");
        if(textDeparture.value == "")
        {
            alert("出发地必须填写！");
            return false;
        }
        
        var textDestination = document.getElementById("textDestination");
        if(textDestination.value == "")
        {
            alert("目的地必须填写！");
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
            var textEmployeeOutName = document.getElementById("textEmployeeOutName");
            var uName = userID.split('|')[1];
            textEmployeeOutName.innerText = uName;
        }
        return false;
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
                    &nbsp;&nbsp;<asp:Label ID="labelTitle" runat="server"></asp:Label>
                    <asp:Button ID="btnChooseOutEmp" Text="员工选择" Visible="false" runat="server" />
                    <asp:Label ID="textEmployeeOutName" Text="" Visible="false" runat="server"></asp:Label>
                    </td>
                </tr>
            </thead>
            <tr>
                <td class="TableBody" valign="top">
                    &nbsp;&nbsp;<asp:Label ID="labelTime" runat="server"></asp:Label>：
                    <asp:TextBox ID="textStartTime" onfocus="setday(this)" runat="server"></asp:TextBox>
                    至&nbsp;<asp:TextBox ID="textEndTime" onfocus="setday(this)" runat="server"></asp:TextBox>
                    <br />
                    &nbsp;&nbsp;出&nbsp;发&nbsp;地：&nbsp;<asp:TextBox ID="textDeparture" runat="server"></asp:TextBox><br />
                    &nbsp;&nbsp;目&nbsp;的&nbsp;地：&nbsp;<asp:TextBox ID="textDestination" runat="server"></asp:TextBox><br />
                    &nbsp;&nbsp;事由：
                    <br />
                    <textarea id="signOutRemark" rows="8" cols="250" runat="server"></textarea>
                    <br />
                    &nbsp;&nbsp;说明：
                    <br />
                    <textarea id="signOutExplanation" rows="8" cols="250" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="TableFoot">
                    <asp:Button ID="btnSignOut" Text="提交主管" runat="server" onclick="btnSignOut_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiddenEmpID" runat="server" /><!-- 存储员工信息 id|name -->
    </form>
</body>
</html>
