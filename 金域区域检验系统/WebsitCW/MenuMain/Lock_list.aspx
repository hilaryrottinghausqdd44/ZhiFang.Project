<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lock_list.aspx.cs" Inherits="OA.MenuMain.Lock_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>锁定详细列表</title>
    <link href="Css/style.css" rel="stylesheet" />

    <script src="JS/calendar1.js" charset="gb2312"></script>

    <script type="text/javascript">
        var checkFlag = true;
        function ChooseAll() {
            //if( !document.all("CheckAll").Checked ) // 全选　
            if (checkFlag) // 全选　
            {
                var inputs = document.all.tags("INPUT");
                for (var i = 0; i < inputs.length; i++) // 遍历页面上所有的 input 
                {
                    if (inputs[i].type == "checkbox" && inputs[i].id != "CheckAll") {
                        inputs[i].checked = true;
                    }
                }
                checkFlag = false;
            }
            else  // 取消全选
            {
                var inputs = document.all.tags("INPUT");
                for (var i = 0; i < inputs.length; i++) // 遍历页面上所有的 input 
                {
                    if (inputs[i].type == "checkbox" && inputs[i].id != "CheckAll") {
                        inputs[i].checked = false;
                    }
                }
                checkFlag = true;
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function GetCookie(name) {
            var arg = name + "=";
            var alen = arg.length;
            var clen = document.cookie.length;
            var i = 0;
            while (i < clen) {
                var j = i + alen;
                if (document.cookie.substring(i, j) == arg)
                    return getCookieVal(j);
                i = document.cookie.indexOf(" ", i) + 1;
                if (i == 0) break;
            }
            return null;
        }

        function getCookieVal(offset) {
            var endstr = document.cookie.indexOf(";", offset);
            if (endstr == -1)
                endstr = document.cookie.length;
            return unescape(document.cookie.substring(offset, endstr));
        }
        function SetCookie(name, value) {
            document.cookie = name + "=" + escape(value)
        } 
    </script>

    <style type="text/css">
        .div1
        {
            background-color: Blue;
            position: absolute;
            height: 500px;
        }
    </style>
</head>
<body onload="document.body.scrollTop=GetCookie('posy')" onunload="SetCookie('posy',document.body.scrollTop);">
    <form id="form1" onkeydown="if (event.keyCode == 13) { return false; }" runat="server">
    <asp:Label Width="180px" runat="server" ID="labmsg" ForeColor="Red" Height="16px"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="选定项执行" Width="97px"
        CssClass="buttonstyle" />
    <table cellpadding="0" width="98%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td style="color: Fuchsia" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="dgclientcombo" runat="server" CellPadding="4" BackColor="White"
                                                BorderWidth="1px" Width="100%" BorderStyle="None" BorderColor="#3366CC" AutoGenerateColumns="False">
                                                <FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
                                                <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                                <ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#990000"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle Width="2%" BackColor="Silver"></HeaderStyle>
                                                        <HeaderTemplate>
                                                            <input id="CheckAll" name="CheckAll" type="checkbox" onclick="ChooseAll()">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="CB_item" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle Width="3%" BackColor="Silver"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <%# Container.ItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="编号">
                                                        <HeaderStyle Width="5%" Height="5%" BackColor="Silver"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem,"nrequestitemno") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="项目名称">
                                                        <HeaderStyle Width="5%" Height="5%" BackColor="Silver"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "cname")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="折扣价格">
                                                        <HeaderStyle Width="5%" Height="5%" BackColor="Silver"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "itemagioprice")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="锁定状态">
                                                        <HeaderStyle Width="5%" BackColor="Silver"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" Width="50" ID="BtnLock" Text='<%#Bind("islocked") %>'
                                                                CommandArgument='<%# Bind("nrequestitemno") %>' BorderWidth="0" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle Width="5%" BackColor="Silver"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" Width="50" ID="work" Text='操作' CommandArgument='<%# Bind("nrequestitemno") %>'
                                                                OnClick="work_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages">
                                                </PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>

    </table>
 
    </form>
</body>
</html>
