<%@ Page validateRequest=false  Language="C#" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="OA.DBQuery.RunExecMSG.SendMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发送电子邮件</title>
    <style type="text/css">
        .style1
        {
            margin-bottom: 229px;
        }
        .style2
        {
            color: #0000FF;
        }
        .style3
        {
            height: 38px;
        }
    </style>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <link href="../css/ioffice.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onload() {

        }
        function checkSubmit() {
            if (form1.TextBoxMailAddress.value == '') {
                alert('没有指定收件人邮件地址');
                return false;
            }
            return true;
        }
// ]]>
    </script>
</head>
<body bgcolor="#d4d0c8" onload="return window_onload()">
    <form id="form1" runat="server" onsubmit="return checkSubmit();">
    <div>
        <table class="style1">
            <tr>
                <td class="style3">
        <asp:Button ID="ButtonSend" runat="server" Text="发送" 
            onclick="ButtonSend_Click" />
                </td>
                <td class="style3">
                    <asp:Label ID="LabelMSG" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td class="style3">
                    </td>
            </tr>
            <tr>
                <td>
                    收件人</td>
                <td>
                    <asp:TextBox ID="TextBoxMailAddress" runat="server" Width="332px" 
                        style="margin-top: 0px"></asp:TextBox>
                    <asp:TextBox ID="TextBoxEmployName" runat="server" Width="124px" 
                        BackColor="Silver"></asp:TextBox>
                        <asp:Label runat="server" Visible="false" ID="labemailaddress"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    多个收件人，请用;分号分隔</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    主题</td>
                <td>
                    <asp:TextBox ID="TextBoxTopic" runat="server" Width="472px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td>
                    邮件内容</td>
                <td>
                    <asp:TextBox ID="TextBoxContent" runat="server" Height="168px" Width="470px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
