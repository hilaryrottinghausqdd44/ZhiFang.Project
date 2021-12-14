<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iHDcreateSN.aspx.cs" Inherits="OA.DBQuery.RunExec.iHDcreateSN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户充值生成授权码</title>
    <link href="../../modulemanage/style.css" rel="stylesheet" />

    <script type="text/javascript">
        function check() {

            var txtpay = document.getElementById("<%=txtpay.ClientID %>");
            if (txtpay.value == null || txtpay.value == "") {
                alert('请输入充值金额');
                txtpay.focus();
                return false;
            }
            else {
                var reg = /^\d+(\.\d+)?$/g;
                //var reg = /^(([1-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/g;
                if (reg.test(txtpay.value) == false) {
                    alert('请输入充值金额格式不正确');
                    txtpay.focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtdate.ClientID %>").value == null || document.getElementById("<%=txtdate.ClientID %>").value == "") {
                alert('到期时间不能为空');
                document.getElementById("<%=txtdate.ClientID %>").focus();
                return false;
            }
            return true;
        }

        //根据输入金额判断到期时间
        function JudgeDateByPay() {
            var txtpay = document.getElementById("<%=txtpay.ClientID %>");

            if (txtpay.value == null || txtpay.value == "") {
                div_pay.innerHTML = "<font color=red>请输入充值金额</font>";
                txtpay.focus();
            }
            else {
                var reg = /^\d+(\.\d+)?$/g;
                //var reg = /^(([1-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/g;
                if (reg.test(txtpay.value) == false) {
                    div_pay.innerHTML = "<font color=red>请输入充值金额格式不正确</font>";
                    txtpay.focus();
                }
                else {
                    div_pay.innerHTML = "<img src='../../images/loading.gif'/>";
                    var equno = document.getElementById("<%=txtequ.ClientID %>").value;
                    OA.DBQuery.RunExec.iHDcreateSN.GetEndDate(txtpay.value, equno, GetCallPayresult);
                }
            }
        }
        //回调结果
        function GetCallPayresult(result) {
            var r = result.value;
            if (r != null && r != "" && r.length > 1) {

                var tmp = r.indexOf(',');
                if (tmp > 0) {

                    //alert(r);
                    var pam1 = new Array;
                    pam1 = r.split(",");
                    //本次到期时间
                    //alert(pam1[0]);
                    document.getElementById("<%=txtdate.ClientID %>").value = pam1[0];
                    //上次到期时间
                    //alert(pam1[1]);
                    document.getElementById("<%=txtlastdate.ClientID %>").value = pam1[1];
                    div_pay.innerHTML = "";
                    document.getElementById("btnSave").disabled = false;

                }
                else {
                    div_pay.innerHTML = "<font color=red>发生错误,返回结果值出错</font>";
                }

            }
            else {
                div_pay.innerHTML = "<font color=red>发生错误,返回结果值出错</font>";

            }
        }
        //根据设备编号和到期时间生成授权码
        function CreateSn() {
            if (check()) {
                //设备编号
                var txtequ = document.getElementById("<%=txtequ.ClientID %>").value;
                //到期时间
                var txtdate = document.getElementById("<%=txtdate.ClientID %>").value;
                //
                if (txtequ == "" || txtdate == "") {
                    div_sn.innerHTML = "<font color=red>本次到期是和设备编号不能为空</font>";
                }
                else {
                    div_sn.innerHTML = "<img src='../../images/loading.gif'/>";
                    OA.DBQuery.RunExec.iHDcreateSN.CreateSN(txtequ, txtdate, GetCallSnresult);
                }
            }
        }
        //回调结果
        function GetCallSnresult(result) {
            var r = result.value;
            if (r != null && r != "") {

                if (r.length == 16) {
                    document.getElementById("<%=txtsn.ClientID %>").value = r;
                    div_sn.innerHTML = "<font color=red>生成成功</font>";
                    PostValueToParentForm();
                }
                else {
                    div_sn.innerHTML = "<font color=red>"+r+"</font>";
                }
            }
            else 
            {
                div_sn.innerHTML = "<font color=red>发生错误,返回结果值出错</font>";
            }
        }
        //给父窗体控件赋值
        function PostValueToParentForm() {
            //充值金额
            window.opener.document.all["Payment"].value = document.getElementById("<%=txtpay.ClientID %>").value;
            //实收金额
            window.opener.document.all["ActualPayment"].value = document.getElementById("<%=txtpay.ClientID %>").value;
            //本次到期日
            window.opener.document.all["CurrentExpireDate"].value = document.getElementById("<%=txtdate.ClientID %>").value;
            //上次到期日
            window.opener.document.all["LastExpireDate"].value = document.getElementById("<%=txtlastdate.ClientID %>").value;
            //授权码
            window.opener.document.all["GetCode"].value = document.getElementById("<%=txtsn.ClientID %>").value;
            window.close();
        }
        //从父窗体得到值用于打印信息
        function CValueByParentForm() {
            divequno.innerHTML = window.opener.document.all["EquipID"].value;
            divlastdate.innerHTML = window.opener.document.all["LastExpireDate"].value;
            divcurrentdate.innerHTML = window.opener.document.all["CurrentExpireDate"].value;
            divpay.innerHTML = window.opener.document.all["Payment"].value;
            divsn.innerHTML = window.opener.document.all["GetCode"].value;
            divsale.innerHTML = window.opener.document.all["SalesDepartment"].value;
            divuser.innerHTML = window.opener.document.all["UserName"].value;
        }
        function Print(cmdid, cmdexecopt) {
            //setUnShowPageTags();
            document.all("printmenu").style.display = "none";

            document.body.focus();

            try {
                var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';

                document.body.insertAdjacentHTML('BeforeEnd', WebBrowser);

                WebBrowser1.ExecWB(cmdid, cmdexecopt, null, null);
                WebBrowser1.outerHTML = "";

                if (cmdid == 6 && cmdexecopt == 6) {
                    alert('打印完成');
                }
            }
            catch (e) { }
            finally {
                document.all("printmenu").style.display = "";
                //setShowPageTags();
            }
        }  
    </script>

    <style type="text/css">
        .hidecss
        {
            display: none;
        }
        @media Print
        {
            .prt
            {
                visibility: hidden;
            }
        }
    </style>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txtmsg" Text="传入设备编号格式有误!请在\'更改设备编号\'功能中查找该用户并修改其设备编号为实际所购的设备编号!" Visible="false"></asp:TextBox>
        <asp:Panel runat="server" ID="pa1">
            <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
                cellpadding="1" width="98%" align="center" bgcolor="#f6f6f6" bordercolorlight="#aecdd5"
                border="1">
                <tbody>
                    <tr>
                        <td valign="top">
                            <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="1"
                                width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
                                <tbody>
                                    <tr>
                                        <td colspan="2" align="center">
                                            生成设备授权码<asp:TextBox runat="server" CssClass="hidecss" ID="txtlastdate"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            设备编号:
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtequ"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            充值金额:
                                        </td>
                                        <td>
                                            <div style="float: left">
                                                <asp:TextBox runat="server" onblur="JudgeDateByPay();" ID="txtpay"></asp:TextBox>
                                            </div>
                                            <div id="div_pay">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            本次到期日:
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtdate" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="left">
                                            授权码:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsn" runat="server" Width="201px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <div>
                                                <input type="button" id="btnSave" disabled value="生 成" class="buttonstyle" onclick="CreateSn();" />
                                                &nbsp;&nbsp;
                                                <input type="button" id="btnClose" value="关 闭" class="buttonstyle" onclick="window.close();" />
                                            </div>
                                            <div id="div_sn">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pa2" Visible="false">
            <div class="prt" id="printmenu" align="left">
                <input class="prt" onclick="javascript:Print(8,1);" type="button" value="打印页面设置"
                    name="button_show">
                &nbsp;&nbsp;<input class="prt" onclick="javascript:Print(7,1);" type="button" value="打印预览"
                    name="button_setup">
                &nbsp;&nbsp;
                <input class="prt" onclick="javascript:window.print()" type="button" value="打印" name="button_print">
            </div>
            <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="1"
                width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            IHD设备授权信息
                        </td>
                    </tr>
                     <tr>
                        <td align="left">
                            用户名:
                        </td>
                        <td>
                            <div id="divuser">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="120px">
                            设备编号:
                        </td>
                        <td>
                            <div id="divequno">
                            </div>
                        </td>
                    </tr>
                   
                    <tr>
                        <td align="left">
                            上次到期日:
                        </td>
                        <td>
                            <div id="divlastdate">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            充值金额:
                        </td>
                        <td>
                            <div id="divpay">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            本次到期日:
                        </td>
                        <td>
                            <div id="divcurrentdate">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            授权码:
                        </td>
                        <td>
                            <div id="divsn">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            开户营业点:
                        </td>
                        <td>
                            <div id="divsale">
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
