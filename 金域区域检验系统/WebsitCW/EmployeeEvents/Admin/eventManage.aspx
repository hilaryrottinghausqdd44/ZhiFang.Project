<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eventManage.aspx.cs" Inherits="OA.EmployeeEvents.Admin.eventManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>考勤管理</title>
    <script type="text/javascript" src="../../Includes/JS/calendarDateTime.js"></script>
    <style type="text/css">
    table
    {
    	border:#c3d9ff;
    }
    a
    {
         text-decoration:none;	
    }
    </style>
    <script type="text/javascript">
    var lastTD;
    function colorTD(obj)
    {
        var myRecord = document.getElementById("tableAllEvent");
        if(lastTD != undefined)
        {
            lastTD.style.backgroundColor = "#FFFFFF";
        }
        obj.style.backgroundColor = "#ffffcc";
        lastTD = obj;
    }
    </script>
</head>
<body style="font-size:12px;  margin:0; padding:0;">
    <form id="form1" runat="server">
    <div id="pageSelect" runat="server" style=" text-align:right;">
        <img src="../../Images/diary/pre.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.history.go(-1);" alt="前一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/next.gif" onmouseover="this.style.cursor='pointer'" onclick="window.history.go(1);" alt="后一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/main.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.parent.location=window.parent.location;" alt="主页" width="60" height="24" />&nbsp;
    </div>
    <div>
    &nbsp;姓名：<asp:TextBox ID="textEmployeeName" runat="server"></asp:TextBox>&nbsp;
    &nbsp;部门：<asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>&nbsp;
    &nbsp;状态：<asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>&nbsp;<br />
    &nbsp;时间：<asp:TextBox ID="textStartTime" onfocus="setday(this)" runat="server"></asp:TextBox>&nbsp;至
    &nbsp;<asp:TextBox ID="textEndTime" onfocus="setday(this)" runat="server"></asp:TextBox>&nbsp;
    <asp:Button ID="btnSearch"  Text="查看" runat="server" onclick="btnSearch_Click" />
    
    <br />
        <table border="1" cellpadding="0" bordercolor="#c3ffff" cellspacing="0" id="tableAllEvent" runat="server">    
        </table>
    </div>
    </form>
</body>
</html>
