<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewConfig.aspx.cs" Inherits="OA.DBQuery.Admin.ViewConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<html>
<head>
    <title>ViewConfig</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table style="border-right: #6699cc 2px solid; border-top: #6699cc 2px solid; border-left: #6699cc 2px solid;
        border-bottom: #6699cc 2px solid" width="100%" align="center" border="0">
        <tbody>
            <tr>
                <td>
                    <table width="100%" border="0">
                        <tr>
                            <td nowrap align="center" width="100%">
                                <p>
                                    <font face="宋体"><span style="width: 73.94%; height: 14px; text-align: center">所有字段</span></font><font
                                        face="宋体"></font></p>
                                <font face="宋体">
                                    <p>
                                        <font face="宋体">
                                            <asp:DataList ID="dataListAllField" runat="server" BorderColor="#99CCCC" GridLines="Both"
                                                BorderWidth="1px" RepeatColumns="2" Width="100%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDisplay" runat="server"></asp:CheckBox>
                                                    <asp:Label ID="lblFieldName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:DataList></font>
                                </font></P>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style="width: 18.95%; height: 20px; text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="保存"></asp:Button>
                    </span>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
