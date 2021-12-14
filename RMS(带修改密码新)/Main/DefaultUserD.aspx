<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultUserD.aspx.cs" Inherits="OA.Main.DefaultUserD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="css.css" />
    <title>雍和园管委会项目申报系统</title>

    <script type="text/javascript" language="javascript">
    function checkUserFill() {
        if (window.document.all["textUserid"].value == "") {
            alert("请输入用户名！")
            return false;
        }
        else {
            Form1.submit();
        }
    }
    function Zc() {
        window.location.href = "../Whcy/Whcy_RegAgreeMent.aspx";
    }
    </script>

</head>
<body onload="javascript:document.getElementById('textUserid').focus()">
    <object id="RemoveIEToolbar" codebase="../Includes/Activex/nskey.dll" height="1"
        width="1" classid="CLSID:2646205B-878C-11d1-B07C-0000C040BCDB" viewastext>
        <param name="ToolBar" value="1">
    </object>
    <div class="body">
        <form id="Form1" onkeydown="javascript:if(event.keyCode=='13') return checkUserFill();"
        method="post" runat="server">
        <table id="table_main" border="0" cellpadding="0" style="width: 900px;" cellspacing="0">
            <!-- 页面顶部LOGO -->
            <tr>
                <td colspan="2" align="center" valign="middle">
                    <table id="table_top" border="0" cellpadding="0" style="width: 900px;" cellspacing="0">
                        <tr>
                            <td>
                                <!--<div class="body">-->
                                <div id="banner">
                                </div>
                                <div class="banner-bottom">
                                </div>
                                <!--</div>-->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!--<div class="body">-->
            <!-- 页面中心信息 -->
            <tr>
                <td valign="top" style="width: 200px;">
                    <table id="table_body_left" border="0" style="width: 200px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div id="left">
                                    <div class="login">
                                        <div class="tongzhi">
                                            用 户 登 陆</div>
                                        <div class="tongzhi-line">
                                            &nbsp;</div>
                                        <div style="padding-bottom: 7px;">
                                            <label for="mod_login_username">
                                                用户名：
                                            </label>
                                            <asp:TextBox class="inputbox" ID="textUserid" runat="server"></asp:TextBox>
                                        </div>
                                        <div style="padding-bottom: 7px;">
                                            <label for="mod_login_password">
                                                密&nbsp;&nbsp;码：
                                            </label>
                                            <asp:TextBox class="inputbox" ID="textPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                        <input class="button" type="button" onclick="checkUserFill();" value="登录" name="Submit2" />&nbsp;&nbsp;
                                        <input class="button" type="button" value="注册" onclick="Zc();" name="Submit1" />
                                        <div class="tongzhi-line">
                                        </div>
                                    </div>
                                    <div>
                                        <div>
                                            <iframe marginheight="0" src='../documents/OA_News_Listwhcy.aspx?id=操作手册&NewsNum=13&ChaNum=10&cssID=skyblue.css&morepar=/whcy/news/browse/CategoryNews.aspx&PopWinLS=中,中,80%,80%'
                                                style="height: 250px; width: 200px;" frameborder="0" name='' marginwidth="0"
                                                scrolling="auto"></iframe>
                                        </div>
                                    </div>
                                </div>
    </div>
    </td> </tr> </table> </td>
    <td valign="top" style="height: 400px; width: 700px;">
        <div id="sysInfo" class="right-r">
            <table id="table_body_right" border="0" style="height: 350px; width: 700px; margin: 0px;"
                cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 700px;" align="center" valign="top">
                        <iframe marginheight="0" src='../documents/OA_News_Listwhcy.aspx?id=政策文件&NewsNum=13&ChaNum=28&cssID=skyblue.css&morepar=/whcy/news/browse/CategoryNews.aspx&PopWinLS=中,中,80%,80%'
                            style="height: 380px; width: 700px;" frameborder="0" name='' marginwidth="0"
                            scrolling="auto"></iframe>
                    </td>
                </tr>
            </table>
        </div>
    </td>
    </tr> </div>
    <tr>
        <td colspan="2" align="center" valign="middle">
            <div class="body foot">
            </div>
            <iframe marginheight="0" src='../news/browse/homepageNologin.aspx?id=389' frameborder="0"
                name='' width="100%" height="45" marginwidth="0" scrolling="no"></iframe>
        </td>
    </tr>
    </table>
    <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red"
        Visible="False" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:Label>
    <asp:Label ID="LBLDomain" runat="server"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    </form>

    <script language="javascript">
    document.all['textUserid'].focus();
    document.all['textUserid'].select();
    </script>

</body>
</html>
