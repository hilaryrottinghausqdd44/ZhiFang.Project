<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main_PatNo.aspx.cs" Inherits="ZhiFang.WebLis.Other.Main_PatNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript">
		var tdid="Dept";
		function ShowList(id)
		{
			defaulttd(tdid);
			tdid=id;
			document.getElementById("DeptTr").style.display='none';
			document.getElementById("ItemTr").style.display='none';
			document.getElementById(id+"Tr").style.display='block';
		}
		function UpList(id)
		{
			if(id!=tdid)
			{
				defaulttd('Dept');
				defaulttd('Item');
				focustd(id);
				focustd(tdid);
			}
		}
		function OutList(id)
		{
			if(id!=tdid)
			{
			defaulttd(id)
			}
		}
		function defaulttd(id)
		{
			document.getElementById(id).style.backgroundColor="#ffffff";
			document.getElementById(id).style.color="#000000";
		}
		function focustd(id)
		{
			document.getElementById(id).style.backgroundColor="#0099cc";
			document.getElementById(id).style.color="#ffffff";
		}
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#9966cc" style="FONT-SIZE: 12px" >
    <tr height="30" style="CURSOR: hand">
    <td id="Dept" onmouseover="UpList('Dept')" onmouseout="OutList('Dept')" onmousedown="ShowList('Dept')" bgcolor="#0099cc" style="color:#ffffff" align="center">查看报告</td>
    <td id="Item" onmouseover="UpList('Item')" onmouseout="OutList('Item')" onmousedown="ShowList('Item')" bgcolor="#ffffff" align="center">修改密码</td></tr>
    <tr id="DeptTr">
					<td colspan="2" bgcolor="#ffffff">
					<iframe src="ReportForm_PatNo.aspx?StartDate=<%=DateTime.Now.AddDays(-3).ToShortDateString() %>,1&EndDate=<%=DateTime.Now.AddDays(3).ToShortDateString() %>,1&PatNo=<%=Common.Cookie.CookieHelper.Read("PatNo_Value") %>,0" frameborder="0" width="100%" height="800"></iframe>
					</td>
					</tr>
					<tr id="ItemTr" style="display:none">
					<td colspan="3" bgcolor="#ffffff">
					<iframe src="ChangPassWords_PatNo.aspx" frameborder="0" width="100%" height="800"></iframe>
					</td>
					
    </table>
    </div>
    </form>
</body>
</html>
