<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BrowseConfig.aspx.cs" Inherits="OA.DBQuery.Admin.BrowseConfig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ViewConfig</title>
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
                        <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click"></asp:Button>
                    </span>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
