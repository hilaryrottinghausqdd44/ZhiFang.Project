<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComboTestItemPrice.aspx.cs" Inherits="OA.Total.ComboTestItemPrice" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>组合项目子项目列表</title>
      <link href="../Css/style.css" rel="stylesheet" />
     
      <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table cellpadding="0" width="100%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                组合项目子项目列表
                                <input type="hidden" id="hidrmsg" runat="server" />
                                <input type="hidden" id="labcbid" runat="server" />
                                <input type="hidden" id="labparitemno" runat="server" />
                            </td>
                        </tr>  
                        <tr>
                           <td colspan="2"><asp:Label runat="server" ID="labparitemname"></asp:Label><asp:Label runat="server" ForeColor="Red" ID="labparitemprice"></asp:Label></td>
                        </tr>                    
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td><asp:Label runat="server" ID="labmsg"></asp:Label> 
                                            <div id="divdg" align="center" style="overflow: auto; width: 100%; height:370px">
                                              <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                    font-size: 12px" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                               </table>                                                
                                               <asp:DataGrid ID="dgcomboitem" runat="server" style="cursor: hand; padding: 0px; margin: 0px;
                                                    font-size: 12px" PageSize="50" Font-Size="Smaller" BorderWidth="1px"
                                                    CellPadding="3" BorderStyle="None" AllowPaging="false" BackColor="White" AutoGenerateColumns="False"
                                                    BorderColor="BlanchedAlmond" Width="100%" 
                                                    OnItemDataBound="dgcomboitem_ItemDataBound"  
                                                    onitemcommand="dgcomboitem_ItemCommand">
                                                    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                    <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                    <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                    </HeaderStyle>
                                                    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                    <Columns>
                                                     
                                                        <asp:TemplateColumn SortExpression="项目名" HeaderText="项目名">
                                                            <HeaderStyle HorizontalAlign="Center" Width="30%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                (<asp:Label ID="labitemno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.itemno") %>'></asp:Label>)
                                                                 <asp:Label ID="labitemname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cname") %>'></asp:Label>
                                                                <asp:Label ID="labciid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ciid") %>'
                                                                    Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="英文名" HeaderText="英文名">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.ename") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>                                                        
                                                         <asp:TemplateColumn SortExpression="原价" HeaderText="原价">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.price") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="价格" HeaderText="价格">
                                                            <HeaderStyle HorizontalAlign="left" Width="15%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Width="30px" ID="txtcomboitemprice" Text='<%# DataBinder.Eval(Container, "DataItem.ItemPrice") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="必填项"
                                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtcomboitemprice"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtcomboitemprice"
                                                                    ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>                                                        
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom" 
                                                        PageButtonCount="30"></PagerStyle>
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnsave" Text="保存项目价格" CssClass="buttonstyle" OnClick="btnsave_Click" />  
                                                         
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
