<%@ Page Language="c#" AutoEventWireup="True" Inherits="main.defaultSingle" Codebehind="defaultSingle.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>
        -主页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <style type="text/css">
        hand
        {
            cursor: hand;
        }
    </style>
</head>
<body bgcolor="#999999">
    <form id="Form1" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="800" align="center" bgcolor="#ffffff"
        border="0">
        <tr>
            <td bgcolor="#ffffff" height="190">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" height="237">
                <div align="center">
                    <table cellspacing="0" cellpadding="0" width="800" align="center" border="0">
                        <tr>
                            <td height="64">
                                &nbsp;
                            </td>
                            <td>
                                <img height="64" src="image/index/left1.jpg" width="223">
                            </td>
                            <td>
                                <img height="64" src="image/index/right1.jpg" width="217">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="172" height="108">
                                <img height="108" src="image/index/left.jpg" width="172">
                            </td>
                            <td width="223">
                                <img height="108" src="image/index/left2.jpg" width="223">
                            </td>
                            <td>
                                <img height="108" src="image/index/right2.jpg" width="217">
                            </td>
                            <td width="188">
                                <img height="108" src="image/index/right.jpg" width="188">
                            </td>
                        </tr>
                        <tr>
                            <td height="30">
                                &nbsp;
                            </td>
                            <td>
                                <img height="30" src="image/index/left3.jpg" width="223">
                            </td>
                            <td>
                                <img height="30" src="image/index/right3.jpg" width="217">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="19">
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <table style="width: 440px; height: 10px" height="10" cellspacing="0" cellpadding="0"
                                    width="440" border="0">
                                    <tr>
                                        <td width="63">
                                            <img height="19" src="image/index/left4_1.jpg" width="63">
                                        </td>
                                        <td width="38">
                                            <img height="19" src="image/index/left4_2.jpg" width="38">
                                        </td>
                                        <td style="width: 83px" nowrap width="83">
                                            &nbsp;
                                            <asp:TextBox ID="textUserid" runat="server" Width="56px" Height="19px">Admin</asp:TextBox>
                                        </td>
                                        <td width="25">
                                            <img height="19" src="image/index/right4_2.jpg" width="25">&nbsp;
                                        </td>
                                        <td nowrap width="109">
                                            &nbsp;
                                            <asp:TextBox ID="textPassword" runat="server" Width="80px" Height="19px"></asp:TextBox>
                                        </td>
                                        <td width="62">
                                            <img style="cursor: hand" onclick="javascript:Form1.submit();" height="19" src="image/index/right4_3.jpg"
                                                width="62">
                                        </td>
                                        <td width="46">
                                            <img height="19" src="image/index/right4_1.jpg" width="46">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="16">
                                &nbsp;
                            </td>
                            <td valign="top" colspan="2">
                                <img height="11" src="image/index/bottom.jpg" width="440">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <p align="center">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red"
                        Visible="False" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:Label></p>
            </td>
        </tr>
        <tr>
            <td height="178" bgcolor="#ffffff">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
    <object id="RemoveIEToolbar" codebase="../Controls/activex/nskey.dll" height="1"
        width="1" classid="CLSID:2646205B-878C-11d1-B07C-0000C040BCDB" viewastext>
    </object>
</body>
</html>
