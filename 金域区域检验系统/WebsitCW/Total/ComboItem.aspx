<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComboItem.aspx.cs" Inherits="OA.Total.ComboItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>套餐项目</title>
    <link href="../Css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../Css/calendar1.js"></script>
<script type="text/javascript">

    function checkprice(obj) {
        var reg=/^\d+$/g;
        if (reg.test(obj.value) == false)
            {   
                alert("项目价格请输入数字！");
                obj.focus();
                return false;
            }
            return true;
        }
        //得到名称为posy的cookie值
        function GetCookie(name) {
            var arg = name + "=";
            var alen = arg.length;
            var clen = document.cookie.length;
            var i = 0;
            while (i < clen) {
                var j = i + alen;
                //判断是否是所指定的key
                if (document.cookie.substring(i, j) == arg)
                    return getCookieVal(j);
                i = document.cookie.indexOf("   ", i) + 1;
                if (i == 0) break;
            }
            return null;
        }
        //得到key的值value
        function getCookieVal(offset) 
        {
            var endstr = document.cookie.indexOf(";", offset);
            if (endstr == -1)
                endstr = document.cookie.length;
            return unescape(document.cookie.substring(offset, endstr));
        }
        //设置当前滑动坐标值到缓存
        function SetCookie(name, value) {
            document.cookie = name + "=" + escape(value)
        } 
</script>
    <base target="_self" />
</head>
<body onload="document.all('divdg').scrollTop=GetCookie('posy')">
    <form id="form1" runat="server">
    <table cellpadding="0" width="98%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="12">套餐项目管理</font>
                                <asp:Label runat="server" ID="labcbid" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                         <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                已有项目名:<asp:TextBox runat="server" ID="txtitemname"></asp:TextBox>&nbsp;<asp:Button
                                    runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" 
                                    onclick="btnsearchitem_Click" />
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                项目名:<asp:TextBox runat="server" ID="txtcbnamekey"></asp:TextBox>&nbsp;<asp:Button
                                    runat="server" ID="btnsearch" Text="查询" CssClass="buttonstyle" OnClick="btnsearch_Click" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="50%">
                                <strong>已有项目:</strong><br />
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                        <div id="divdg" align="center" style="OVERFLOW:auto; WIDTH:100%; HEIGHT:550px">
                                            <asp:DataGrid ID="dgcomboitem" runat="server" PageSize="15" Font-Size="Smaller" BorderWidth="1px"
                                                CellPadding="3" BorderStyle="None" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                                BorderColor="#A7C4F7" Width="98%" OnItemDataBound="myDataGrid_ItemDataBound"
                                                OnItemCommand="dgcomboitem_ItemCommand">
                                                <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                </HeaderStyle>
                                                <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                <Columns>
                                                    <asp:TemplateColumn SortExpression="序号" HeaderText="序号">
                                                        <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%# Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn SortExpression="项目名" HeaderText="项目名">
                                                        <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.cname") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn SortExpression="英文名" HeaderText="英文名">
                                                        <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.ename") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn SortExpression="价格" HeaderText="价格">
                                                        <HeaderStyle HorizontalAlign="left" BackColor="Silver"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" Width="30px" ID="txtcomboitemprice" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>'></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="必填项"
                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtcomboitemprice"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtcomboitemprice"
                        ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                        Display="Dynamic"></asp:RegularExpressionValidator></div>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" OnClientClick="SetCookie('posy', document.all('divdg').scrollTop-20);" runat="server" CommandName="Delete">删除</asp:LinkButton>
                                                            <asp:Label ID="labciid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ciid") %>'
                                                                Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="color: Silver" valign="top" width="50%">
                                <strong>项目列表:</strong>
                                <br />
                                <asp:DataGrid ID="dgitem" runat="server" PageSize="25" Font-Size="Smaller" BorderWidth="1px"
                                    CellPadding="3" BorderStyle="None" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                    BorderColor="#A7C4F7" Width="100%" OnItemDataBound="myDataGrid_ItemDataBound"
                                    OnItemCommand="dgitem_ItemCommand" OnPageIndexChanged="dgitem_PageIndexChanged">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                    <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                    <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                    </HeaderStyle>
                                    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ltnSelect" OnClientClick="SetCookie('posy', document.all('divdg').scrollTop+20);" runat="server" CommandName="select">选择</asp:LinkButton>
                                                <asp:Label ID="labitemno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.itemno") %>'
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>                                       
                                        <asp:TemplateColumn SortExpression="项目名" HeaderText="项目名">
                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.cname") %>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="英文名" HeaderText="英文名">
                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.ename") %>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="价格" HeaderText="价格">
                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="labitemprice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>'
                                                  ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false" SortExpression="方法" HeaderText="方法">
                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.diagmethod") %>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="left" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr><td colspan="2"><asp:Button runat="server" ID="btnsave" Text="保存已有套餐项目" 
                                CssClass="buttonstyle" onclick="btnsave_Click" /></td></tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
