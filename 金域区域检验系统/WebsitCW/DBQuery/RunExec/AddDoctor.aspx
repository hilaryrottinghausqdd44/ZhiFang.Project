<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDoctor.aspx.cs" Inherits="OA.DBQuery.RunExec.AddDoctor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增医生</title>
    <link href="SubScriptCss.css" rel="stylesheet" type="text/css" media="all">
    <script language="javascript">
        function getvalue() {
            OA.DBQuery.RunExec.AddDoctor.Save(document.getElementById('DoctorName').value, document.getElementById('DoctorSName').value, document.getElementById('DoctorSCode').value, callback);
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
    <div>
    <table height="300" border="0" width="100%">
    <tr>
       <td valign="middle" colspan="3">
       
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" style="font-size:12px; background-color:Blue;"  >
                        <tr>
                        <td  style="background-color:#a3f1f5;margin-top:5px;" align="right">医生名称：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="DoctorName" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr style="display:none">
                        <td style="background-color:#a3f1f5;margin-top:5px" align="right">医生简称：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="DoctorSName" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td style="background-color:#a3f1f5;margin-top:5px" align="right">医生简码：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="DoctorSCode" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <td style="background-color:#a3f1f5;" align="center" colspan="2">
                        <input type="button" value="确定" onclick="getvalue();" />&nbsp;&nbsp;
                       <input type="button" value="关闭" onclick="window.close();" />
                        </td>
                        </tr>
                 </table>
                 </td></tr></table>
    </div>
    </form>
</body>
</html>
