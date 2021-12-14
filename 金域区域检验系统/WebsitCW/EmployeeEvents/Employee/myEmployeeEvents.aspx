<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myEmployeeEvents.aspx.cs" Inherits="OA.EmployeeEvents.Employee.myEmployeeEvents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>我的考勤</title>
    <style type="text/css">
    table
    {
    	border:#c3d9ff;
    }
    span
    {
    	text-decoration:none;
    }
    </style>
    <script type="text/javascript">
    function colorTD(obj)
    {
        var myRecord = document.getElementById("myRecord");
        for(var i=2;i<myRecord.rows.length;i++)
        {
            for(var j=0;j<myRecord.rows[i].childNodes.length;j++)
            {
                myRecord.rows[i].childNodes[j].style.backgroundColor = "#FFFFFF";
            }
        }
        obj.style.backgroundColor = "#ffffcc";
    }
    function openDiary(date,isSelf,signID)
    {
        window.location.href = "EmployeeDailyDiary.aspx?isSelf="+isSelf+"&signID="+signID+"&rdate="+date;
        //alert(date+":"+isSelf+":"+signID);"+isSelf+"
    }
    </script>
</head>
<body style="font-size:12px; margin:0; padding:0; overflow:auto;">
    <form id="form1" runat="server">
    <div id="pageSelect" runat="server" style=" text-align:right;width:100%;">
        <img src="../../Images/diary/pre.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.history.go(-1);" alt="前一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/next.gif" onmouseover="this.style.cursor='pointer'" onclick="window.history.go(1);" alt="后一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/main.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.parent.location=window.parent.location;" alt="主页" width="60" height="24" />&nbsp;
    </div>
    <div style="width:100%">
    
    <table style="border-style:none;" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <asp:ImageButton ID="imgBtnLastMonth" ImageUrl="../../Images/diary/left.JPG" ToolTip="上一月" runat="server" onclick="imgBtnLastMonth_Click" />
                <asp:ImageButton ID="imgBtnNextMonth" ImageUrl="../../Images/diary/right.JPG" ToolTip="下一月" runat="server" onclick="imgBtnNextMonth_Click" />
                <asp:DropDownList ID="ddlYear"  runat="server"></asp:DropDownList>年
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="1">一月</asp:ListItem>
                    <asp:ListItem Value="2">二月</asp:ListItem>
                    <asp:ListItem Value="3">三月</asp:ListItem>
                    <asp:ListItem Value="4">四月</asp:ListItem>
                    <asp:ListItem Value="5">五月</asp:ListItem>
                    <asp:ListItem Value="6">六月</asp:ListItem>
                    <asp:ListItem Value="7">七月</asp:ListItem>
                    <asp:ListItem Value="8">八月</asp:ListItem>
                    <asp:ListItem Value="9">九月</asp:ListItem>
                    <asp:ListItem Value="10">十月</asp:ListItem>
                    <asp:ListItem Value="11">十一月</asp:ListItem>
                    <asp:ListItem Value="12">十二月</asp:ListItem>
                </asp:DropDownList>月&nbsp;
                <asp:Button ID="btnSearch" Text="查看" runat="server" onclick="btnSearch_Click" />
                <span><asp:Label ID="labelTitle" runat="server"></asp:Label></span>
            </td>    
        </tr>
        <tr>
            <td>
                <table id="myRecord" name="myRecord" runat="server" width="700" border="1" cellpadding="0" cellspacing="0">
                    <tr bordercolor="#c3d9ff" align="left" style=" height:15px; font-weight:bold;">
                        <td>姓名</td>
                        <td colspan="6"><asp:Label ID="myName" runat="server">&nbsp;</asp:Label></td>
                    </tr>
                    <tr bordercolor="#c3d9ff" align="center" style=" height:15px; color:#112abb; background-color:#c3d9ee;">
                        <td>周日</td>
                        <td>周一</td>
                        <td>周二</td>
                        <td>周三</td>
                        <td>周四</td>
                        <td>周五</td>
                        <td>周六</td>
                    </tr> 
                </table>
            </td>
        </tr>
    </table>
</div>
    <div id="detailInfo"></div>
    <asp:HiddenField ID="hiddenEmpID" runat="server" /><!-- signID -->
    </form>
</body>
</html>
