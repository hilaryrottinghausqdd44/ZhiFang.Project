<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginA.aspx.cs" Inherits="OA.SystemModules.LoginA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Literal runat="server" ID="litcss"></asp:Literal>

    <script type="text/javascript" language="javascript">
    function checkUserFill() {
        if (window.document.all["textUserid"].value == "") {
            alert("请输入用户名！")
            return false;
        }
        else if (document.all["textPassword"].value == "") {
            alert("请输密码！")
            return false;
        }
        else if (document.all["textCode"].value == "") {
            alert("请输验证码！")
            return false;
        }
        else {
            form1.submit();
        }
    }
    function JumpUrl()
    {
        window.parent.location.href = '../main/Index.aspx';
    }
    function Zc() {
        window.parent.location.href = "../Whcy/Whcy_RegAgreeMent.aspx";
    }
    </script>

    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .STYLE2
        {
            font-size: 13px;
            font-family: "宋体";
            color: #333333;
        }
        a:link
        {
            color: #333333;
            text-decoration: none;
        }
        a:visited
        {
            color: #333333;
            text-decoration: none;
        }
        a:hover
        {
            color: #FF0000;
            text-decoration: none;
        }
        a:active
        {
            color: #333333;
            text-decoration: none;
        }
        .STYLE3
        {
            font-size: 13px;
            color: #FFFFFF;
            font-weight: bold;
        }
        a.navfont:link
        {
            font-size: 13px;
            color: #477ac3;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont:visited
        {
            font-size: 13px;
            color: #477ac3;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont:hover
        {
            font-size: 13px;
            color: #FF0000;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont:active
        {
            font-size: 13px;
            color: #477ac3;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:link
        {
            font-size: 13px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:visited
        {
            font-size: 13px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:hover
        {
            font-size: 13px;
            color: #FF0000;
            text-decoration: none;
            font-weight: bold;
        }
        a.navfont1:active
        {
            font-size: 13px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        .STYLE6
        {
            color: #FFFFFF;
            font-weight: bold;
        }
        .STYLE7
        {
            font-size: 12px;
        }
        body, td, th
        {
            font-family: 宋体;
            font-size: 13px;
        }
    </style>
</head>
<body onload="javascript:document.getElementById('textUserid').focus()">
    <form id="form1" onkeydown="javascript:if(event.keyCode=='13') return checkUserFill();"
    runat="server">
    <table width="210" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="style-titletable">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="170" align="center" class="style-text">
                <table width="200" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2" align="center" style="font-weight: bolder">
                            用 户 登 陆
                        </td>
                    </tr>
                    <tr>
                        <td width="77" height="25" align="right">
                            用户名 ：
                        </td>
                        <td width="123" align="left">
                            <asp:TextBox class="inputbox" ID="textUserid" runat="server" Width="95px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                            密&nbsp;&nbsp;码 ：
                        </td>
                        <td align="left">
                            <asp:TextBox class="inputbox" ID="textPassword" runat="server" TextMode="Password"
                                Width="95px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                            验证码 ：
                        </td>
                        <td align="left">
                            <asp:TextBox class="inputbox" ID="textCode" runat="server" Width="95px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <font color="blue">双击图片<br />
                                换一张</font>
                        </td>
                        <td align="center">
                            <img title="看不清楚,双击图片换一张." ondblclick="this.src = 'LoginAImgCode.aspx?flag=' + Math.random() "
                                border="1" src="LoginAImgCode.aspx?flag=1" style="cursor: hand" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <a href="#" onclick="checkUserFill();"><img src="../documents/images/gif-0025.gif" width="58" height="19" border="0" /></a>
                            &nbsp;&nbsp;
                            <a href="#" onclick="Zc();"><img src="../documents/images/zc-1.gif" visible="false" runat="server" id="zcimg" width="58" height="19" border="0"/></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" Width="150px" ForeColor="Red" Visible="False"
                                BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="16px" CssClass="style1">提示信息</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style-textothers">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Label ID="LBLDomain" runat="server"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    </form>
</body>
</html>
