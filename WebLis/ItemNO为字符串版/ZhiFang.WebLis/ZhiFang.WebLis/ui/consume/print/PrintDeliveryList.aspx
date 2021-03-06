<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDeliveryList.aspx.cs"
    Inherits="ZhiFang.WebLis.consume.print.PrintDeliveryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>外送清单打印</title>
</head>
<script language="javascript">
    function printme() {
        pagesetup_null();
        document.body.innerHTML = document.getElementById('NRequestFormList').innerHTML;
        window.print();
    }
    function pagesetup_null() {
        try {
            var RegWsh = new ActiveXObject("WScript.Shell")
            hkey_key = "header"
            RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
            hkey_key = "footer"
            RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
        } catch (e) { }
    }

</script>
<body>
    <form id="form1" runat="server">
    <input type="button" value="打印" onclick="printme()" />
    <div id="NRequestFormList">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" style="font-size: 12px">
            <tr style="font-size: 20px; font-weight: bold; font-family: 楷体">
                <td align="center" colspan="2">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 15px; font-weight: bold">
                <td>
                    外送单位：<asp:Label ID="Label2" runat="server" Text="四川大家实验室"></asp:Label>
                </td>
                <td>
                    送检单位：<asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                </td>
                <td align="right">
                    送检日期：
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="pad" id="List_Table"
            runat="server">
            <tr style="padding-top: 2px">
                <td colspan="10">
                    <hr size="3" color="black" />
                </td>
            </tr>
            <tr style="font-size: 12px; font-weight: bold;">
                <td width="5%" align="center">
                    序号
                </td>
                <td width="10%" align="center">
                    申请时间
                </td>
                <td width="10%" align="center">
                    条码号
                </td>
                <td width="10%" align="center">
                    就诊类型
                </td>
                <td width="10%" align="center">
                    病历号
                </td>
                <td width="5%" align="center">
                    姓名
                </td>
                <td width="5%" align="center">
                    性别
                </td>
                <td width="5%" align="center">
                    年龄
                </td>
                <td width="10%" align="center">
                    医生
                </td>
                <td align="center">
                    项目
                </td>
            </tr>
            <tr style="padding-top: 2px">
                <td colspan="10">
                    <hr size="3" color="black" />
                </td>
            </tr>
        </table>
        <div width="100%" border="0" cellpadding="0" cellspacing="1">
            <asp:Panel ID="writetxt" runat="server">
                <asp:Label ID="Label5" runat="server" Text="检验标本总数:"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="0" Style="padding: 0px 50px;"></asp:Label>
                <asp:Label ID="Label7" runat="server" Text="病理标本总数:" Style="padding: 0px 80px;"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Label ID="Label8" runat="server" Text="客户签名:"></asp:Label>
                <asp:Label ID="Label9" runat="server" Text="物流签名:" Style="padding: 0px 150px;"></asp:Label>
                <asp:Label ID="Label10" runat="server" Text="物流接收时间:"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Label ID="Label11" runat="server" Text="实验室签名:" Style="padding: 0px 140px 0 220px;"></asp:Label>
                <asp:Label ID="Label12" runat="server" Text="实验室接收时间:"></asp:Label>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
