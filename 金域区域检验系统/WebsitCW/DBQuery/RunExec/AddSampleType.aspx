<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSampleType.aspx.cs" Inherits="OA.DBQuery.RunExec.AddSampleType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增样本类型</title>
    <link href="SubScriptCss.css" rel="stylesheet" type="text/css" media="all">
    <script language="javascript">
        function getvalue() {
            OA.DBQuery.RunExec.AddSampleType.Save(document.getElementById('SampleTypeName').value, document.getElementById('SampleTypeSName').value, document.getElementById('SampleTypeSCode').value, callback);
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
                        <td  style="background-color:#a3f1f5;margin-top:5px;" align="right">样本名称：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="SampleTypeName" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr style="display:none">
                        <td style="background-color:#a3f1f5;margin-top:5px" align="right">样本简称：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="SampleTypeSName" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td style="background-color:#a3f1f5;margin-top:5px" align="right">样本简码：
                        </td>        
                        <td style="background-color:#a3f1f5;margin-top:5px"><asp:TextBox ID="SampleTypeSCode" runat="server"></asp:TextBox>
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
