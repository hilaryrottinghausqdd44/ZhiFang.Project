<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.Main.Desktop.Destop_sj" Codebehind="Destop_sj.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>桌面配置系统</title>
</head>
<body ms_positioning="GridLayout">
    <table height="298" cellspacing="0" cellpadding="0" width="57" border="0" ms_2d_layout="TRUE">
        <tr valign="top">
            <td width="57" height="298">
                <form runat="server">
                <table height="40" cellspacing="0" cellpadding="0" width="286" border="0" ms_2d_layout="TRUE">
                    <tr valign="top">
                        <td width="10" height="16">
                        </td>
                        <td width="276">
                        </td>
                    </tr>
                    <tr valign="top">
                        <td height="24">
                        </td>
                        <td>
                            <font face="宋体">
                                <input type="file" id="File1" name="File1" runat="server">
                                <asp:Button ID="Button1" runat="server" Text="上传" Height="20px" OnClick="Button1_Click">
                                </asp:Button>
                                <asp:Label ID="lblMessage" runat="server" Width="176px"></asp:Label></font>
                        </td>
                    </tr>
                </table>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
