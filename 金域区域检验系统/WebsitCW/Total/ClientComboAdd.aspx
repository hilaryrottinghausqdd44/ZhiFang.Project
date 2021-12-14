<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientComboAdd.aspx.cs"
    Inherits="OA.Total.ClientComboAdd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位套餐维护</title>
    <base target="_self" />
    <link href="../Css/style.css" rel="stylesheet" />

    <link href="../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        function check() {
            var clientagio = document.getElementById('txtclientagio').value;
            if (clientagio == "" || clientagio == null) {
                alert('单位折扣不能为空!');
                document.getElementById('txtclientagio').focus();
                return false;
            }
            var reg = /^\d+(\.\d+)?$/g;
            if (clientagio != "" && reg.test(clientagio) == false) {
                alert('单位折扣必须为数字!');
                document.getElementById('txtclientagio').focus();
                return false;
            }
            if (document.getElementById('txtbegindate').value == "" || document.getElementById('txtbegindate').value == null) {
                alert('开始时间不能为空!');
                document.getElementById('txtbegindate').focus();
                return false;
            }
            var tbclientbegindate = document.getElementById('txtbegindate').value;
            var tbclientenddate = document.getElementById('txtenddate').value;
            //判断开始时间不能大小结束时间
            if (tbclientbegindate.length > 0 && tbclientenddate.length > 0) {
                var reg = new RegExp("-", "g");
                var bvalue = parseInt(tbclientbegindate.replace(reg, ""));
                var evalue = parseInt(tbclientenddate.replace(reg, ""));
                if (bvalue > evalue) {
                    alert('开始时间不能大于结束时间!');
                    document.getElementById('txtbegindate').focus();
                }
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
            var r = window.showModalDialog(strurl, this, 'dialogWidth:1000px;dialogHeight:900px;');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
            }
        }
        function showUserDialog1(strurl) {
            window.open(strurl, 'neww', 'width=1000px,height=900px,top=0,left=50px,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');

        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; width: 500; height: 150; border-left: solid #000000 1px;
        border-top: solid #000000 1px; border-bottom: solid #000000 1px; border-right: solid #000000 1px;
        cursor: hand;" cellspacing="0" cellpadding="0" oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="500" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
            border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
            <tbody>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        套餐名:
                    </td>
                    <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:DropDownList runat="server" ID="drpcombolist">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        就诊类型:
                    </td>
                    <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:DropDownList runat="server" ID="drpsicktypelist">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        单位折扣:
                    </td>
                    <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:TextBox runat="server" Width="30px" ID="txtclientagio"></asp:TextBox>
                        <font color="red">如:95折应该填9.5</font>
                        &nbsp;<asp:CheckBox runat="server" ID="chkcomputeprice" />是否重新计算单位套餐项目价格
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        开始时间:
                    </td>
                    <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:TextBox runat="server" ID="txtbegindate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                        结束时间:
                    </td>
                    <td align="left" colspan="3" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:TextBox runat="server" ID="txtenddate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"></asp:TextBox>
                        <input type="button" id="btnclear" class="buttonstyle" value="清空" onclick="document.getElementById('txtenddate').value='';" />为空表示长期有效
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4" style="border-bottom: #aecdd5 solid 1px;">
                        <asp:Button runat="server" ID="btn" Text="保 存" CssClass="buttonstyle" OnClick="btn_Click" />
                        &nbsp;&nbsp;
                        <input type="button" id="btnclose" value="取 消" class="buttonstyle" onclick="closediv();" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div>
        <table cellpadding="0" width="100%" cellspacing="0" border="0">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                        border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                        <tbody>
                            <tr>
                                <td align="center" width="100%" height="50px" style="border-bottom: #aecdd5 solid 1px;">
                                    <font size="6">单位套餐管理</font>
                                    <asp:Label runat="server" ID="labclientno" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="labclientname" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="labcoid" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="100%" height="30px" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                    <asp:Button runat="server" CssClass="buttonstyle" ID="btnadd" Text="新增套餐" OnClick="btnadd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="color: Silver" valign="middle">
                                    <asp:Label runat="server" ID="labmsg"></asp:Label>
                                    <asp:DataGrid ID="myDataGrid" runat="server" PageSize="15" Font-Size="Smaller" BorderWidth="1px"
                                        CellPadding="3" BorderStyle="Solid" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                        BorderColor="#A7C4F7" Width="100%" OnItemDataBound="myDataGrid_ItemDataBound"
                                        OnItemCommand="myDataGrid_ItemCommand">
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
                                            <asp:TemplateColumn HeaderText="就诊类型">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="labsicktypeno" Text='<%# DataBinder.Eval(Container, "DataItem.sicktypeno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="套餐名" HeaderText="套餐名">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.cbname") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="折扣" HeaderText="折扣">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.clientagio") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="开始时间" HeaderText="开始时间">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.begindate") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="结束时间" HeaderText="结束时间">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.enddate") %>
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
                                                    <asp:Label ID="labcoidin" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.coid") %>'
                                                        Visible="False">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input type="button" id="btnclose" class="buttonstyle" value="关 闭" onclick="window.returnValue='0';window.close();" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
