<%@ Page Language="C#" AutoEventWireup="true" validateRequest=false CodeBehind="DefaultSample.aspx.cs" Inherits="OA.Main.DefaultSample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        .style2
        {
            font-size: small;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" language="javascript">
    function checkUserFill() {
        if (window.document.all["textUserid"].value == "") {
            alert("请输入用户名！")
            return false;
        }
        else if (document.all["textPassword"].value == "") {
            alert("请输密码！")
            return false;
        }
        else {
            form1.submit();
        }
    }
    function JumpUrl(sUrl)//weblis
    {
        //window.parent.location.href = "/oa2008" + sUrl;
        window.open('/weblis' + sUrl);
    }
    function Zc() {
        window.parent.location.href = "../Whcy/Whcy_RegAgreeMent.aspx";
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="1">
            <tr>
                <td nowrap="nowrap">
                    <span class="style2">请输入条码号</span>:</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" />
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
