<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplySelect.aspx.cs" Inherits="ZhiFang.WebLis.ApplySelect.ApplySelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Weblis报告查询打印</title>
    <link type="text/css" rel="stylesheet" href="../Css/jyjgcxstyle.css" />
    <script type="text/javascript" src="../js/calendarb.js"></script>
    <script type="text/javascript" src="../js/Tools.js"></script>
    <script type="text/javascript" src="../js/LodopPrinter_weblis.js"></script>
    <script type="text/javascript" src="../js/PrinterOperatorInterface.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 12px">
        <tr width="100%">
            <td class="style3">
                送检单位
                <select id="Client" name="Client" style="width: 197px; margin: 0px; padding: 0px"
                    runat="server">
                    <option></option>
                </select>
            </td>
            <td>
            </td>
            <td class="style3">
                采样时间：
                <input id="txtStartDate" name="StartDate" type="text" onfocus="calendar()" style="width: 148px;"
                    runat="server" />
                <input id="txtEndDate" name="EndDate" type="text" onfocus="calendar()" style="width: 148px;"
                    runat="server" />
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical" AllowPaging="True" Height="242px" Width="299px"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                    OnRowCommand="GridView1_RowCommand" 
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="WebLisSourceOrgName" HeaderText="送检单位" />
                         <asp:BoundField DataField="WebLisSourceOrgID" HeaderText="送检单位ID" />
                        <asp:BoundField DataField="BarCodeCount" HeaderText="样本总数" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </td>
            <td>
                &nbsp; &nbsp;<asp:Button ID="Button1" runat="server" 
                    Text="Button" onclick="Button1_Click" Style="display:none"/><asp:HiddenField ID="ClientNo"
                        runat="server" />
            </td>
            <td>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical" AllowPaging="True" Height="240px" Width="703px"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    OnPageIndexChanging="GridView2_PageIndexChanging" 
                    OnRowDataBound="GridView2_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="BarCode" HeaderText=" 条码号" />
                        <asp:BoundField DataField="Cname" HeaderText="姓名" />
                        <asp:BoundField DataField="GenderName" HeaderText="性别" />
                        <asp:BoundField DataField="Age" HeaderText="年龄" />
                        <asp:BoundField DataField="DoctorName" HeaderText="医生" />
                        <asp:BoundField DataField="SENDITEMNAME" HeaderText="项目名称" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
