<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signOnOff.aspx.cs" Inherits="OA.EmployeeEvents.Employee.signOnOff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>签到签退</title>
    <style type="text/css">
    table
    {
    	border-color:#339955;
    	border:1px;
    	width:550px;
    	height:280px;
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
    <script type="text/javascript" src="../../Includes/JS/calendarDateTime.js"></script>
    <script type="text/javascript">

    </script>
</head>
<body style="font-size:12px;">
    <form id="form1" runat="server">
        <asp:Panel ID="panelSignOn" runat="server">
        <div style=" text-align:right;">
            <img src="../../Images/diary/pre.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.history.go(-1);" alt="前一页" width="60" height="24" />&nbsp;
            <img src="../../Images/diary/next.gif" onmouseover="this.style.cursor='pointer'" onclick="window.history.go(1);" alt="后一页" width="60" height="24" />&nbsp;
            <img src="../../Images/diary/main.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.parent.location=window.parent.location;" alt="主页" width="60" height="24" />&nbsp;
        </div>
        
            <table border="1" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <td class="head">
                        <asp:Label ID="labelOnOrOff" runat="server"></asp:Label>
                        </td>
                    </tr>
                </thead>
                <tr>
                    <td class="TableBody" valign="top">
                        &nbsp;<asp:Label ID="labelSignTime" runat="server"></asp:Label>：
                        <input type="text" onfocus="setday(this)" ID="textSignTime" runat="server" /><br />
                        &nbsp;标准时间：&nbsp;<asp:Label ID="labelStandardTime" runat="server"></asp:Label>
                        <br /><br />
                        &nbsp;说明：
                        <br />
                        <textarea id="signOnExplanation" rows="8" cols="250" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="TableFoot">
                        <asp:Button ID="btnSignOn" Text="登记"  runat="server" onclick="btnSignOn_Click" />  
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField ID="hiddenEmpID" runat="server" /><!-- signID -->
        <input type="hidden" id="hiddenSignOnOrOff" runat="server" /><!-- 签到(on)，签退(off) -->
    </form>
</body>
</html>
