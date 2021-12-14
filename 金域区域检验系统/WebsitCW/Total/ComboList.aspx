<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeBehind="ComboList.aspx.cs" Inherits="OA.Total.ComboList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>套餐列表</title>
    <link href="../Css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../Css/calendar1.js" charset="gb2312"></script>

    <script type="text/javascript">
        function check() {
            if (document.getElementById('txtcbname').value == "" || document.getElementById('txtcbname').value == null) {
                alert('套餐名称不能为空!');
                document.getElementById('txtcbname').focus();
                return false;
            }

            return true;
        }
        var falpha;
        falpha = 0;
        function fchange() {
            if (falpha != 90) {
                table1.style.filter = "alpha(opacity=" + falpha + ")";
                falpha = falpha + 10;
                setTimeout("fchange()", 200);
            }
            else {
                falpha = 0;
            }
        }
        function showdiv() {
            table1.style.height = (window.document.body.clientHeight > window.document.body.scrollHeight) ? window.document.body.clientHeight : window.document.body.scrollHeight;
            table1.style.width = "100%";
            table1.style.display = 'block'
            table2.style.left = document.documentElement.scrollLeft + 30;
            table2.style.top = document.body.scrollTop + 30;
            table2.style.display = 'block';
            fchange();
        }

        function closediv() {
            table1.style.display = 'none';
            table2.style.display = 'none';
        }
        function Save() {
            //
        }
        function showUserDialog(strurl) {
            var r = window.showModalDialog(strurl, this, 'dialogWidth:900px;dialogHeight:650px;');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                window.location.href = "combolist.aspx";
            }
        }
        function showUserDialog1(strurl) {
            window.open(strurl, 'neww', 'width=1000px,height=900px,top=0,left=50px,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; width: 560; height: 180; border-left: solid #000000 1px;
        border-top: solid #000000 1px; border-bottom: solid #000000 1px; border-right: solid #000000 1px;
        cursor: hand;" cellspacing="0" cellpadding="0" oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="550" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
            border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
            <tbody>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        套餐名:
                    </td>
                    <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:TextBox runat="server" ID="txtcbname"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        状态:
                    </td>
                    <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:DropDownList runat="server" ID="drpstatus">
                            <asp:ListItem Value="0">禁用</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        备注:
                    </td>
                    <td align="left" colspan="3" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:TextBox runat="server" ID="txtcbremark" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:Button runat="server" ID="btn" CssClass="buttonstyle" Text="保 存" OnClick="btn_Click" />
                        &nbsp;&nbsp;
                        <input type="button" id="btnclose" class="buttonstyle" value="取 消" onclick="closediv();" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div>
        <table cellpadding="0" width="70%" cellspacing="0" border="0">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                        border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                        <tbody>
                            <tr>
                                <td align="center" width="100%" height="50px" style="border-bottom: #aecdd5 solid 1px;">
                                    <font size="6">套餐管理</font>
                                    <asp:Label runat="server" ID="labwebid" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="100%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                    套餐名:<asp:TextBox runat="server" ID="txtcbnamekey"></asp:TextBox>&nbsp;<asp:Button
                                        runat="server" ID="btnsearch" Text="查询" CssClass="buttonstyle" OnClick="btnsearch_Click" />
                                    &nbsp;&nbsp;<asp:Button runat="server" CssClass="buttonstyle" ID="btnadd" Text="新增套餐"
                                        OnClick="btnadd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="color: Silver" valign="middle">
                                    <asp:DataGrid ID="myDataGrid" runat="server" PageSize="15" Font-Size="Smaller" BorderWidth="1px"
                                        CellPadding="4" BorderStyle="None" borderColorDark="#ffffff" 
                                        borderColorLight="#666666" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                        BorderColor="#3366CC" Width="100%" OnItemDataBound="myDataGrid_ItemDataBound"
                                        OnItemCommand="myDataGrid_ItemCommand" 
                                        onpageindexchanged="myDataGrid_PageIndexChanged">
                                        <FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                        <ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#990000"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn SortExpression="序号" HeaderText="序号">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="套餐名" HeaderText="套餐名">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.cbname") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="状态" HeaderText="状态">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Literal runat="server" ID="litstatus" Text='<%# DataBinder.Eval(Container, "DataItem.cbstatus") %>'></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="备注" HeaderText="备注">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Literal runat="server" ID="litremark" Text='<%# DataBinder.Eval(Container, "DataItem.cbremark") %>'></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="创建时间" HeaderText="创建时间">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Literal runat="server" ID="litacccreatetime" Text='<%# DataBinder.Eval(Container, "DataItem.cbcreatedate") %>'></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver" Wrap="False">
                                                </HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" BackColor="#D1CEFF" Font-Bold="True">
                                                </ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandName="Update">编辑</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">删除</asp:LinkButton>
                                                    <asp:Label ID="labid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cbid") %>'
                                                        Visible="False">
                                                    </asp:Label>
                                                    <a href="#" style="display: none" onclick="showUserDialog('ComboItem.aspx?cbid=<%# DataBinder.Eval(Container, "DataItem.cbid") %>');">
                                                        套餐项目</a> <a href="#" onclick="showUserDialog1('ComboItemPrice.aspx?cbid=<%# DataBinder.Eval(Container, "DataItem.cbid") %>');">
                                                            套餐项目价格</a> <a href="#" style="display: none" onclick="showUserDialog1('ComboClient.aspx?cbid=<%# DataBinder.Eval(Container, "DataItem.cbid") %>');">
                                                                套餐单位</a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
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
