<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginC.aspx.cs" Inherits="OA.SystemModules.LoginC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>外部用户验证form提交验证</title>
    
    <link rel="stylesheet" type="text/css" href="../main/css.css" />
    <script type="text/javascript" language="javascript">
    function checkUserFill() {
        if (window.document.all["textUserid"].value == "") 
        {
            alert("请输入用户名！")
            return false;
        }
        else 
        {
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

</head>
<body onload="javascript:document.getElementById('textUserid').focus()">
    <form id="form1" runat="server" method="post" onkeydown="javascript:if(event.keyCode=='13') return checkUserFill();">
    <table id="table_body_left" border="0" style="width: 100%;" cellpadding="0" cellspacing="0">
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
                            
                           
                        </div>
                    </div>
                </td>
            </tr>
        </table>
  
    <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red"
        Visible="False" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">提示信息</asp:Label>
    <asp:Label ID="LBLDomain" runat="server"></asp:Label>
    <asp:DropDownList ID="DropDownList1" Visible="false" runat="server">
    </asp:DropDownList>
    </form>
</body>
</html>
