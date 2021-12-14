<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonSearch_PKI.aspx.cs"
    Inherits="ZhiFang.WebLis.PersonSearch_PKI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安智-报告单查询</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <meta name="renderer" content="webkit" />
    <!--不缓存缓存页面-->
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-control" content="no-cache" />
    <meta http-equiv="Cache" content="no-cache" />
    <link href="ui/easyui/demo/demo.css" rel="stylesheet" type="text/css" />
    <link href="ui/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="ui/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="ui/easyui/jquery-1.11.0-vsdoc.js" type="text/javascript"></script>
    <script src="ui/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="ui/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="ui/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="ui/util/jquery.cookie.js" type="text/javascript"></script>
    <script src="ui/util/util_Person.js" type="text/javascript"></script>
    <script src="ui/PersonSearch_PKI.js" type="text/javascript"></script>
</head>
<body style="padding: 0px; margin: 0px; background-color: #dcdede;">
    <form id="form1" runat="server" onsubmit="return ValidateForm();">
    <div style="position: absolute; left: 0px; top: 0px; background-image: url(images/PersonSearch_LeftImage_PKI.png);
        background-repeat: no-repeat; background-position: top; width: 266px; height: 171px">
    </div>
    <div style="position: absolute; right: 0px; top: 0px; background-image: url(images/PersonSearch_RightImage_PKI.png);
        background-repeat: no-repeat; background-position: top; width: 368px; height: 470px">
    </div>
    <table border="0" align="center" style="" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url(images/PersonSearch_AnzhiLogo_PKI.png); background-repeat: no-repeat;
                background-position: bottom; width: 476px; height: 260px">
            </td>
        </tr>
        <tr>
            <td style="background-image: url(images/PersonSearch_LoginBox.png); background-repeat: no-repeat;
                background-position: top; width: 551px; height: 390px">
                <table border="0" align="center" style="margin-top: 0px">
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="BarcodeTxt" runat="server" Style="background-image: url(images/PersonSearch_PKI_Background_TextLable.png);
                                background-repeat: no-repeat; background-position: top; width: 318px; height: 47px;
                                border: 0px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="NameTxt" runat="server" Style="background-image: url(images/PersonSearch_PKI_Background_TextLable.png);
                                background-repeat: no-repeat; background-position: top; width: 318px; height: 47px;
                                border: 0px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" style="width:135px">    
                            <asp:TextBox ID="CheckTxt" runat="server" Height=30></asp:TextBox>
                            </td>
                            <td valign="middle"> <img id="ImageCheck" src="Util/ValidateCode.aspx" width="100" height="30" style="vertical-align:middle;cursor:pointer;" alt=""/>
                            <asp:Image runat="server" ID="ImageCheck1" Style="display:none; top:10px; border: 0px;width: 100px; height: 30px;" ImageUrl="Util/ValidateCode.aspx"></asp:Image><a href="javascript:RefreshCheckImg(0);">刷新</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 50px" colspan="2">
                        <div id="ErrorInfo" style="color:Red;" runat=server></div>
                            <asp:Button ID="Searchbtn" runat="server" Text="登   录" Style="background-image: url(images/PersonSearch_PKI_Background_LoginButton.png);background-repeat: no-repeat; background-position: top; width: 196px; height: 37px;border: 0px; font-size: large; color: #ffffff"  OnClick="Searchbtn_Click" />   
                            <button id='btnt' type='button' onclick="Searchbtn_Click" runat=server>123</button>                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="background-image: url(images/PersonSearch_FooterText_PKI.png); background-repeat: no-repeat;
                background-position: top; width: 476px; height: 44px">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
