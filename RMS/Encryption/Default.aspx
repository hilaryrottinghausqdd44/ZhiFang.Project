<%@ Page Language="c#" enableEventValidation="false" AutoEventWireup="True" Inherits="Labweb.Encryption._Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Default1</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <style type="text/css">
        BODY
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        A
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        TABLE
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        DIV
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        SPAN
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        TD
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        TH
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        INPUT
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        SELECT
        {
            font: 9pt "����" , Verdana, Arial, Helvetica, sans-serif;
        }
        BODY
        {
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 5px;
            padding-top: 5px;
        }
    </style>

    <script language="JavaScript" src="../HTMLEDIT/dialog/dialog.js"></script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    &nbsp;
    <table cellspacing="0" cellpadding="0" align="center" border="0">
        <tbody>
            <tr>
                <td>
                    <fieldset>
                        <legend>ע����Ϣ</legend>
                        <table style="width: 560px; height: 64px" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td colspan="9" height="5">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="7">
                                    </td>
                                    <td nowrap>
                                        �˿�����:
                                    </td>
                                    <td width="5">
                                        <asp:TextBox ID="TextBox1" runat="server" Width="222px"></asp:TextBox>
                                    </td>
                                    <td style="width: 163px">
                                        <font color="#ff6666">*</font>
                                    </td>
                                    <td width="40">
                                    </td>
                                    <td>
                                        ��ϵ�绰:
                                    </td>
                                    <td width="5">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Width="190px"></asp:TextBox>
                                    </td>
                                    <td width="7">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 23px" width="7">
                                    </td>
                                    <td style="height: 23px" nowrap>
                                        �����ʼ�
                                    </td>
                                    <td style="height: 23px" width="5">
                                        <asp:TextBox ID="TextBox2" runat="server" Width="222px"></asp:TextBox>
                                    </td>
                                    <td style="width: 163px; height: 23px">
                                        <font color="#ff6666">*</font>
                                    </td>
                                    <td style="height: 23px" width="40">
                                    </td>
                                    <td style="height: 23px">
                                        ��ϵ�绰2
                                    </td>
                                    <td style="height: 23px" width="5">
                                    </td>
                                    <td style="height: 23px">
                                        <asp:TextBox ID="TextBox4" runat="server" Width="189px"></asp:TextBox>
                                    </td>
                                    <td style="height: 23px" width="7">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 16px" width="7">
                                    </td>
                                    <td style="height: 16px">
                                        ��ַ
                                    </td>
                                    <td style="height: 16px" width="5" colspan="6">
                                        <asp:TextBox ID="TextBox5" runat="server" Width="496px"></asp:TextBox>
                                    </td>
                                    <td style="height: 16px" width="7">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 14px" width="7">
                                    </td>
                                    <td style="height: 14px">
                                        �ʱ�
                                    </td>
                                    <td style="height: 14px" width="5">
                                        <asp:TextBox ID="TextBox6" runat="server" Width="224px"></asp:TextBox>
                                    </td>
                                    <td style="width: 163px; height: 14px">
                                    </td>
                                    <td style="height: 14px" width="40">
                                    </td>
                                    <td style="height: 14px" nowrap>
                                        ��Ȩ��ϵ��ʽ
                                    </td>
                                    <td style="height: 14px" width="5">
                                    </td>
                                    <td style="height: 14px">
                                        <select style="width: 184px" name="selectContactWay">
                                            <option value="�����ʼ�" selected>�����ʼ�</option>
                                            <option value="�ֻ�����">�ֻ�����</option>
                                            <option value="�ʼ�">�ʼ�</option>
                                            <option value="����ע��">����ע��</option>
                                            <option value="�绰">�绰</option>
                                        </select>
                                    </td>
                                    <td style="height: 14px" width="7">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9" height="5">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <fieldset>
                        <legend>������Ϣ</legend>
                        <table id="Table1" style="width: 560px; height: 64px" cellspacing="0" cellpadding="0"
                            border="0">
                            <tr>
                                <td colspan="9" height="5">
                                </td>
                            </tr>
                            <tr>
                                <td width="7">
                                </td>
                                <td nowrap>
                                    Ӳ�̺�:
                                </td>
                                <td width="5">
                                </td>
                                <td style="width: 187px">
                                    <asp:DropDownList ID="txtHardDrive" runat="server" Width="176px">
                                    </asp:DropDownList>
                                </td>
                                <td width="40">
                                </td>
                                <td nowrap>
                                    ��վ����Ŀ¼:
                                </td>
                                <td width="5">
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVirtualDir" runat="server"></asp:TextBox>
                                </td>
                                <td width="7">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 23px" width="7">
                                </td>
                                <td style="height: 23px" nowrap>
                                    ������:
                                </td>
                                <td style="height: 23px" width="5">
                                </td>
                                <td style="width: 187px; height: 23px">
                                    <asp:DropDownList ID="txtNetDrive" runat="server" Width="175px">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 23px" width="40">
                                </td>
                                <td style="height: 23px">
                                    ��վ���ʵ�ַ:
                                </td>
                                <td style="height: 23px" width="5">
                                </td>
                                <td style="height: 23px">
                                    <asp:TextBox ID="txtHost" runat="server"></asp:TextBox>
                                </td>
                                <td style="height: 23px" width="7">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9" height="5">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <p>
                        <input style="display: none" disabled type="button" value="����ע��">&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="buttDowloadTemp" type="button" value="����ע�ᣨ���ر��" name="Button1" onclick="window.open('WebVegaע����Ϣ���.dot')">
                    </p>
                    <p>
                        �ʼ���ַ�� <span lang="EN-US" style="font-size: 9pt; font-family: 'Times New Roman'; mso-fareast-font-family: ����;
                            mso-font-kerning: 1.0pt; mso-ansi-language: EN-US; mso-fareast-language: ZH-CN;
                            mso-bidi-language: AR-SA"><a href="mailto:WebVega@zhifang.com.cn"><span style="mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">WebVega@zhifang.com.cn</span></a></span>
                        ��ҳ�� <a href="http://www.zhifang.com.cn/zhifang">www.zhifang.com.cn/zhifang</a>
                    </p>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>����ע����Ϣ</legend>
                        <table id="Table2" style="width: 560px; height: 64px" cellspacing="0" cellpadding="0"
                            border="0">
                            <tr>
                                <td colspan="9" height="5">
                                </td>
                            </tr>
                            <tr>
                                <td width="7">
                                </td>
                                <td nowrap>
                                    ע����:
                                </td>
                                <td width="5">
                                </td>
                                <td style="width: 163px">
                                    <asp:TextBox ID="LicenseNo" runat="server" Width="207px"></asp:TextBox>
                                </td>
                                <td width="40">
                                </td>
                                <td nowrap>
                                    <input id="SubmitActivate" type="submit" value="  ����  " name="SubmitActivate" runat="server"
                                        onserverclick="SubmitActivate_ServerClick">
                                </td>
                                <td width="5">
                                </td>
                                <td>
                                </td>
                                <td width="7">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9" height="5">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
