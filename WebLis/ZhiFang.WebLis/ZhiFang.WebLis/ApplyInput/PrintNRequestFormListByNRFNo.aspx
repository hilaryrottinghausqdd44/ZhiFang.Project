<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintNRequestFormListByNRFNo.aspx.cs"
    Inherits="ZhiFang.WebLis.ApplyInput.PrintNRequestFormListByNRFNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>外送清单打印</title>
</head>
<script src="../ui/easyui/jquery.min.js" type="text/javascript"></script>
<script src="../ui/util/util.js?v20200811" type="text/javascript"></script>
<script language="javascript">
    $(function () {
        var aaa = $('#Label4').text();
        $('#Label1').text(aaa);
    });
    function printme() {
        var para = Shell.getRequestParams();
        var updateweblisflag = false;
        if (para["IsUpDateWeblisFlag"] && para["IsUpDateWeblisFlag"] == "1") {
            updateweblisflag = true;
        }
        if (updateweblisflag) {
            alert("打印即表示已经开始外送！");
            var BarCodeList = $('#BarCodeList').val();
            $.ajax({
                type: "GET",
                url: "../ServiceWCF/NRequestFromService.svc/SendBarCodeFromByBarCodeList?BarCodeList=" + BarCodeList,
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        alert('样本状态已标志为已外送！');
                        pagesetup_null();
                        document.body.innerHTML = document.getElementById('NRequestFormList').innerHTML;
                        window.print();
                    }
                    else {
                        if (data.ErrorInfo != null && data.ErrorInfo != "") {
                            alert('样本状态表示失败！ErrorInfo:' + data.ErrorInfo);
                        }
                    }
                }
            });
        }

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
                    <td>外送单位：<asp:Label ID="Label2" runat="server" Text="四川大家实验室"></asp:Label>
                    </td>
                    <td>送检单位：<asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right">送检日期：
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="1" class="pad" id="List_Table"
                runat="server">
                <tr style="padding-top: 2px">
                    <td colspan="12">
                        <hr size="3" color="black" />
                    </td>
                </tr>
                <tr style="font-size: 12px; font-weight: bold;">
                    <td width="5%" align="center">序号
                    </td>
                    <td width="10%" align="center">申请时间
                    </td>
                    <td width="10%" align="center">条码号
                    </td>
                    <td width="10%" align="center">送检类型
                    </td>
                    <td width="10%" align="center">就诊类型
                    </td>
                    <td width="10%" align="center">病历号
                    </td>
                    <td width="5%" align="center">姓名
                    </td>
                    <td width="5%" align="center">性别
                    </td>
                    <td width="5%" align="center">年龄
                    </td>
                    <td width="5%" align="center">医生
                    </td>
                    <td width="10%" align="center">身份证
                    </td>
                    <td align="center">项目
                    </td>
                </tr>
                <tr style="padding-top: 2px">
                    <td colspan="12">
                        <hr size="3" color="black" />
                    </td>
                </tr>
            </table>
            <div width="100%" >
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="检验标本总数:"></asp:Label>  
                            <asp:Label ID="Label6" runat="server" Text="0" ></asp:Label>
                            <div style="display: none">
                                <asp:TextBox ID="BarCodeList" runat="server" Text=""></asp:TextBox>
                            </div>
                        </td>
                        <td>                          
                            <asp:Label ID="Label8" runat="server" Text="客户签名:" ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="物流人签名:"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height:20px"></tr>
                    <tr>
                        <td><asp:Label ID="Label10" runat="server" Text="物流接收时间:" ></asp:Label></td>
                        <td><asp:Label ID="Label13" runat="server" Text="物流人手机号:" ></asp:Label></td>
                        <td><asp:Label ID="Label14" runat="server" Text="物流运送车牌号:"></asp:Label></td>
                    </tr>
                     <tr style="height:20px"></tr>
                    <tr>
                        <td><asp:Label ID="Label11" runat="server" Text="实验室签名:" ></asp:Label></td>
                        <td><asp:Label ID="Label12" runat="server" Text="实验室接收时间:"></asp:Label></td>
                    </tr>
                </table>
                <asp:Panel ID="writetxt" runat="server">
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
