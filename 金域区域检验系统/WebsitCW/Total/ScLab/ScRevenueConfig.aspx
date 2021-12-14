<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScRevenueConfig.aspx.cs"
    Inherits="OA.Total.ScLab.ScRevenueConfig" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>创建的新栏目列表维护</title>
    <link href="../../Css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" width="100%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" height="50px" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="6">创建的新栏目列表维护</font>
                                <input type="hidden" id="hidrmsg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <asp:Label runat="server" ID="labmsg"></asp:Label>
                                <asp:DataGrid ID="myDataGrid" runat="server" PageSize="30" Font-Size="Smaller" BorderWidth="1px"
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
                                            <asp:TemplateColumn HeaderText="编号">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="labreportid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reportid") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="名称">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.reportname") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                             <asp:TemplateColumn Visible=false HeaderText="类型">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.reporttype") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                           <asp:TemplateColumn HeaderText="创建日期">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.reportdate") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                                           
                                            <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver" Wrap="False">
                                                </HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Font-Bold="True">
                                                </ItemStyle>
                                                <ItemTemplate>                                                    
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">删除</asp:LinkButton>  
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
