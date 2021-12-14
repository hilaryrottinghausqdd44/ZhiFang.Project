<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMS.aspx.cs" Inherits="OA.RBACApplication.SMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onload() {

        }
        function selectPersions() {
            var r = window.showModalDialog("../PopupSelectDialog.aspx?RBAC/Organizations/SelectPersons.aspx");
            if (r) {
                resetSelection(r);
            }
        }
        
        function resetSelection(ret)
        {
            //alert(ret);
            var allPS = ret.split("|");
            var bSend = false;
            var PersonsCount = 0;
            var SendMobilesCount = 0;
            for (var i = 0; i < allPS.length; i++) {
                PersonsCount = allPS.length;
                var eachP = allPS[i].split(",");
                if (eachP.length > 0) {
                    if (eachP[0] != "" && eachP[1] != "") {
                        bSend = true;
                        document.frames["frmSMS"].document.getElementById("RHSC").value = eachP[1];
                        document.frames["frmSMS"].document.getElementById("RName").value = eachP[0];
                        document.frames["frmSMS"].document.getElementById("AddButton").click();
                        SendMobilesCount++;
                    }
                }
            }
            if (PersonsCount > 0) {
                alert("共选择人员：" + PersonsCount + "\n\n其中带手机号的有：" + SendMobilesCount);
            }
            if (bSend)
                ;
                //document.frames["frmSMS"].all["Button1"].click();
        }

        function queryMobileRecords() {
            var r = window.open("../SMSOA/SMSHistoryAssignment.aspx");
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-weight: bold">
    
        企业互联网手机短信发送平台</div>
    <table class="style1">
        <tr>
            <td valign="top" width="30%">
                <iframe id="frmSMS" src="../SMSOA/SMSaddmessage.aspx" width="550" height="500" frameborder="0"></iframe></td>
            <td valign="top">
                短信发送说明:<br />
                <br />
                <input id="ButtonMyOrg" type="button" value="选择本单位人员" onclick="selectPersions();"/><br />
                <br />
                <input id="ButtonCustomers" type="button" value="选择客户" disabled="disabled" /><br />
                <br />
                <input id="ButtonMyOrg1" type="button" value="查询发送短信记录" onclick="queryMobileRecords();" /><br />
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
