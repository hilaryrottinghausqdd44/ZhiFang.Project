<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScMonthReport.aspx.cs"
    Inherits="OA.Total.ScLab.ScMonthReport" %>
<%@ Register TagPrefix="idatagrid" Assembly="WebSiteOA" Namespace="OA.ZCommon.CommonControl"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月报表数据统计</title>
    <link href="../../Css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../../Css/calendar1.js"></script>

    <script type="text/javascript">
        function check() {
            if (document.getElementById('txtbegindate').value == "" || document.getElementById('txtbegindate').value == null) {
                alert('开始时间不能为空!');
                document.getElementById('txtbegindate').focus();
                return false;
            }
            if (document.getElementById('txtenddate').value == "" || document.getElementById('txtenddate').value == null) {
                alert('结束时间不能为空!');
                document.getElementById('txtenddate').focus();
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
                    return false;
                }
            }
            showdiv();
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
            table2.style.left = document.documentElement.scrollLeft + 20;
            table2.style.top = document.body.scrollTop + 100;
            table2.style.display = 'block';
            //divrmsg.innerHTML = document.getElementById('hidrmsg').value;
            fchange();
        }
        function closediv() {
            table1.style.display = 'none';
            table2.style.display = 'none';
        }
        //弹出打印页面
        function ShowPrint() {
            window.open("../TotalReport_MiddlePrint.aspx");
        }
        //得到datagrid的html值
        function GetDataGridShow() {
            return document.getElementById('showdiv').innerHTML;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; border-left: solid #000000 1px; border-top: solid #000000 1px;
        border-bottom: solid #000000 1px; border-right: solid #000000 1px; cursor: move;"
        cellspacing="0" cellpadding="0" oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" border="0">
            <tbody>
                <tr>
                    <td colspan="2">
                        <div id="divrmsg" style="word-wrap: break-word; float: left; text-align: left; overflow: hidden;
                            color: #313131;">
                            正在查询...<img src="../../Css/indicator_big.gif" alt="" />
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <table cellpadding="0" width="100%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                月报表数据统计
                                <input type="hidden" id="hidrmsg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                                    <tbody>
                                        <tr align="left">
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:DropDownList runat="server" Visible="false" ID="drpclientarea">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp; 送检单位:<asp:DropDownList runat="server" ID="drpclientname">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp; 起止时间:
                                                <asp:TextBox runat="server" ID="txtbegindate" Width="70px" CssClass="input_text"
                                                    onfocus="calendar();"></asp:TextBox>
                                                -<asp:TextBox runat="server" ID="txtenddate" Width="70px" CssClass="input_text" onfocus="calendar();"></asp:TextBox>
                                                <font color="red">此起止时间以月为单位</font>
                                                <asp:Button runat="server" ID="btnsearch" Text="查 询" CssClass="buttonstyle"
                                                    OnClick="btnsearch_Click" />
                                            </td>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:Button runat="server" Visible="False" CssClass="buttonstyle" ID="btnExport"
                                                    Text="另存为Excel" OnClick="btnExport_Click"></asp:Button>
                                                &nbsp;&nbsp;<input onclick="javascript:ShowPrint();" class="buttonstyle" type="button"
                                                    value="打印" name="button_print">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%" height="100%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="labmsg"></asp:Label>
                                            <div id="showdiv">
                                                <idatagrid:InheritDataGrid ID="dgmonthreport" runat="server" PageSize="50" AllowPaging="false"
                                                    AutoGenerateColumns="False" BorderWidth="1px" CellPadding="4" BorderStyle="None"
													BackColor="White" BorderColor="#A7C4F7" Width="100%" Font-Size="Smaller" OnItemDataBound="dgmonthreport_ItemDataBound">
													<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
													<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
													<HeaderStyle Font-Size="Smaller" Font-Bold="True" Height="26px" ForeColor="Black" BackColor="#CCCCCC"></HeaderStyle>
													<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="时 间">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="laboperdate" Text='<%# DataBinder.Eval(Container, "DataItem.operdate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                       
                                                        <asp:TemplateColumn HeaderText="送检单位">
                                                            <HeaderStyle HorizontalAlign="Center" Width="15%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="labclientname" Text='<%# DataBinder.Eval(Container, "DataItem.clientname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="项目名称">
                                                            <HeaderStyle HorizontalAlign="left" Width="15%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="labitemname" Text='<%# DataBinder.Eval(Container, "DataItem.cname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="项目个数">
                                                            <HeaderStyle HorizontalAlign="left" Width="10%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                 <asp:Label runat="server" ID="labitemcount" Text='<%# DataBinder.Eval(Container, "DataItem.itemcount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="总价">
                                                            <HeaderStyle HorizontalAlign="left" Width="10%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="labsumitemprice" Text='<%# DataBinder.Eval(Container, "DataItem.sumitemprice") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="总折扣价">
                                                            <HeaderStyle HorizontalAlign="left" Width="10%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="labitemagioprice" Text='<%# DataBinder.Eval(Container, "DataItem.sumitemagioprice") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom" PageButtonCount="30">
                                                    </PagerStyle>
                                                </idatagrid:InheritDataGrid>
                                            </div>
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
