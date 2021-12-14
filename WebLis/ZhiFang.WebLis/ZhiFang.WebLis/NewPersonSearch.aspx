<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPersonSearch.aspx.cs"
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
<body style="padding: 0px; margin: 0px;">
    <form id="form1" runat="server" onsubmit="return     ValidateForm();">
        <div style="width: 1200px; height: 700px; position: absolute; top: 50%; left: 50%; margin: -350px 0 0 -600px; background-position: center; background-repeat: no-repeat; background-image: url(cutterman/背景.png);">
            <div style="background-color: rgba(0,210,255,0.5); width: 100%; height: 100%;">
                <div style="position: absolute; background-image: url('cutterman/基因-logo.png'); background-repeat: no-repeat; background-position: 50% top; top: 60px; left: 80px; width: 316px; height: 90px">
                </div>
                <div style="position: absolute; background-image: url('cutterman/越本质，越精准.png'); background-repeat: no-repeat; background-position: 50% top; top: 500px; left: 80px; width: 316px; height: 90px"></div>
                <div style="position: absolute; background-image: url('cutterman/Themoreessential,themoreaccurate.png'); background-repeat: no-repeat; background-position: 50% top; top: 540px; left: 116px; width: 316px; height: 90px"></div>
                <div style="position: absolute; background-image: url('cutterman/文字装饰竖条.png'); background-repeat: no-repeat; background-position: 50% top; top: 500px; left: -35px; width: 316px; height: 90px"></div>
                <table border="0" align="center" style="" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style=" background-repeat: no-repeat; background-position: bottom; width: 476px; height: 30px"></td>
                    </tr>
                    <tr>
                        <td style="position: relative; top: 150px;left:300px;background-image: url(cutterman/蓝色边框.png);  background-repeat: no-repeat; background-position: top; width: 400px; height: 450px">
                            <table border="0" align="center" style="position: relative; top: -30px;">
                                <tr>
                                    <td style="width:15px; text-align:center;position: relative; bottom: 30px;" colspan="2">
                                        <img src="cutterman/欢迎登录.png" ></img>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="position: relative; top: -10px;" colspan="2">
                                        <asp:TextBox ID="BarcodeTxt" runat="server" Style="background-image: url(cutterman/登录条1.png); background-repeat: no-repeat; background-position: top; width: 318px; height: 47px; border: 0px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="position: relative; top: 10px;" colspan="2">
                                        <asp:TextBox ID="NameTxt" runat="server" Style="background-image: url(cutterman/登录条2.png); background-repeat: no-repeat; background-position: top; width: 318px; height: 47px; border: 0px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="position: relative; top: 20px;" valign="middle" style="width: 135px">
                                        <asp:TextBox ID="CheckTxt" runat="server" Height="30"></asp:TextBox>
                                    </td>
                                    <td style="position: relative; top: 20px;"  valign="middle">
                                        <img id="ImageCheck" src="Util/ValidateCode.aspx" width="100" height="30" style="vertical-align: middle; cursor: pointer;" alt="" />
                                        <asp:Image runat="server" ID="ImageCheck1" Style="display: none; top: 10px; border: 0px; width: 100px; height: 30px;" ImageUrl="Util/ValidateCode.aspx"></asp:Image><a href="javascript:RefreshCheckImg(0);">刷新</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 50px;position: relative; top: 30px;" colspan="2">
                                        <div id="ErrorInfo" style="color: Red;" runat="server"></div>
                                        <asp:Button ID="Searchbtn" runat="server" Text="" Style="background-image: url(cutterman/登录条4.png); background-repeat: no-repeat; background-position: top; width: 300px; height: 50px; border: 0px; font-size: large; " OnClick="Searchbtn_Click" />
                                        <img style="position:relative;left:-50%;top:-10px" src="cutterman/登录.png"/>
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
