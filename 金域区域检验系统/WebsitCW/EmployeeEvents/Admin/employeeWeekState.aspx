<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employeeWeekState.aspx.cs" Inherits="OA.EmployeeEvents.Admin.employeeWeekState" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>员工状态</title>
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
        var myRecord = document.getElementById("employeeRecord");
        if(lastTD != undefined)
        {
            lastTD.style.backgroundColor = "#FFFFFF";
        }
        obj.style.backgroundColor = "#ffffcc";
        lastTD = obj;
    }
    function showDiv(strValue)
    {
        var left = document.documentElement.scrollLeft + event.clientX;
        var top = event.clientY + document.documentElement.scrollTop;
        if((document.body.offsetWidth-left)<250)
        {
            left = left - 250;
        }
        if((document.body.offsetHeight-top)<100)
        {
            top = top - 100;
        }
        var eventDetailInfo = document.getElementById("eventDetailInfo");
        eventDetailInfo.innerHTML = strValue;
        eventDetailInfo.style.borderStyle = "groove";
        
        eventDetailInfo.style.marginLeft = left;
        eventDetailInfo.style.marginTop = top;
        
        eventDetailInfo.style.display = "";
    }
    function unShowDiv()
    {
        var eventDetailInfo = document.getElementById("eventDetailInfo");
        eventDetailInfo.innerHTML = "";
        eventDetailInfo.style.display = "none";
    }
    </script>
</head>
<body style="margin:0; padding:0;">
    <form id="form1" runat="server">
    <div id="eventDetailInfo" style="border:1px; border-color:Yellow; background-color:#FFFF33; font-size:12px; position:absolute; width:250px;display:none;"></div>
    <div id="pageSelect" runat="server" style="text-align:right;"><img src="../../Images/diary/pre.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.history.go(-1);" alt="前一页" width="60" height="24" />&nbsp;<img src="../../Images/diary/next.gif" onmouseover="this.style.cursor='pointer'" onclick="window.history.go(1);" alt="后一页" width="60" height="24" />&nbsp;<img src="../../Images/diary/main.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.parent.location=window.parent.location;" alt="主页" width="60" height="24" />&nbsp;
    </div>
    <div>
        <table style="border-style:none;font-size:12px; " cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" colspan="2">
                <asp:ImageButton ID="preWeek" ImageUrl="../../Images/diary/left.JPG" ToolTip="上一周" runat="server" BorderStyle="None" onclick="preWeek_Click" />
                <asp:ImageButton ID="nextWeek" ImageUrl="../../Images/diary/right.JPG" ToolTip="下一周" runat="server" BorderStyle="None" onclick="nextWeek_Click" />
                <asp:DropDownList ID="ddlYear"  runat="server"></asp:DropDownList>年
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="1">一</asp:ListItem>
                    <asp:ListItem Value="2">二</asp:ListItem>
                    <asp:ListItem Value="3">三</asp:ListItem>
                    <asp:ListItem Value="4">四</asp:ListItem>
                    <asp:ListItem Value="5">五</asp:ListItem>
                    <asp:ListItem Value="6">六</asp:ListItem>
                    <asp:ListItem Value="7">七</asp:ListItem>
                    <asp:ListItem Value="8">八</asp:ListItem>
                    <asp:ListItem Value="9">九</asp:ListItem>
                    <asp:ListItem Value="10">十</asp:ListItem>
                    <asp:ListItem Value="11">十一</asp:ListItem>
                    <asp:ListItem Value="12">十二</asp:ListItem>
                </asp:DropDownList>月&nbsp;
                第&nbsp;<asp:DropDownList ID="ddlWeek" runat="server"></asp:DropDownList>&nbsp;周&nbsp;
                <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>部门&nbsp;
                <asp:Button ID="btnSearch" Text="查看" runat="server" onclick="btnSearch_Click" />
                <span>员工考勤汇总</span>
            </td>    
        </tr>
        <tr>
            <td colspan="2">
                <table id="employeeRecord" style="font-size:12px;" runat="server" bordercolor="#c3ffff" border="1" cellpadding="0" cellspacing="0"> 
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
