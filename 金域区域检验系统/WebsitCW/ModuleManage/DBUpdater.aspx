<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBUpdater.aspx.cs" Inherits="OA.ModuleManage.DBUpdater" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模块脚本升级</title>
    <link href="style.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <font face="宋体">
            <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
                cellpadding="1" width="100%" align="center" bgcolor="#f6f6f6" bordercolorlight="#aecdd5"
                border="1">
                <tbody>
                    <tr>
                        <td>
                            <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
                                cellpadding="10" width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5"
                                border="1">
                                <tbody>
                                    <tr>
                                        <td align="center" valign="top">
                                            数据库脚本升级
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="left" height="100%">
                                            <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Button runat="server" ID="btnUpGrade" Text="确定升级" 
                                                onclick="btnUpGrade_Click" />                                            
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </font>
    </div>
    </form>
</body>
</html>
