<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDepartment.aspx.cs" Inherits="OA.DBQuery.RunExec.AddDepartment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增科室</title>
    <link href="SubScriptCss.css" rel="stylesheet" type="text/css" media="all">
    <script language="javascript">
        function getvalue() {
            OA.DBQuery.RunExec.AddDepartment.Save(document.getElementById('DeptName').value, document.getElementById('DeptSName').value, document.getElementById('DeptSCode').value, callback);
        }
        function callback(result) {
            if (result.value == '1') {
                window.returnValue = '1'; window.close();
            }
            else {
                window.returnValue = '0'; window.close();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table height="300" border="0" width="100%">
    <tr>
       <td valign="middle" colspan="3">
       
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" style="font-size:12px; background-color:Blue;"  >
                        <tr>
                        <td  style="background-color:#a3f1f5;margin-top:5px;" align="right">科室名称：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="DeptName" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td style="background-color:#a3f1f5;margin-top:5px" align="right">科室简称：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="DeptSName" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td style="background-color:#a3f1f5;margin-top:5px" align="right">科室简码：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="DeptSCode" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <td style="background-color:#a3f1f5;" align="center" colspan="2">
                        <input type="button" value="确定" onclick="getvalue();" />&nbsp;&nbsp;
                       <input type="button" value="关闭" onclick="window.close();" />
                        </td>
                        </tr>
                 </table>
                 </td></tr></table>
                
    </form>
</body>
</html>
