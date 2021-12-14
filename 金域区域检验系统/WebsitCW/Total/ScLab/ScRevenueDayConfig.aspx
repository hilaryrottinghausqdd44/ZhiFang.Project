<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScRevenueDayConfig.aspx.cs" Inherits="OA.Total.ScLab.ScRevenueDayConfig" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>收入日报表配置</title>
     <link href="../../Css/style.css" rel="stylesheet" />
    <base target="_self"></base>
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
                                <font size="6">收入日报表数据配置</font>
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
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%" height="100%">
                                    <tr>
                                        <td>
                                            显示列表配置
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>                                            
                                            <asp:DataList ID="myColumnsList" runat="server" HorizontalAlign="left" GridLines="None"
                                                CellSpacing="1" RepeatDirection="Horizontal" RepeatColumns="4" BorderWidth="0"
                                                CellPadding="2" Width="80%" onitemdatabound="myColumnsList_ItemDataBound">
                                                <ItemTemplate>
                                                    <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="2"
                                                        width="200px" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
                                                         <tr>
                                                            <td bgcolor="Gainsboro" width="60px">
                                                                英文名称
                                                            </td>
                                                            <td bgcolor="#ffffff">
                                                                <asp:Label runat="server" ID="labename" Text='<%# DataBinder.Eval(Container, "DataItem.columnename") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="Gainsboro">
                                                                中文名称
                                                            </td>
                                                            <td bgcolor="#ffffff">
                                                                <asp:TextBox runat="server" MaxLength=50 ID="txtcname" Text='<%# DataBinder.Eval(Container, "DataItem.columncname") %>'
                                                                    Width="80px">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="Gainsboro">
                                                                是否显示
                                                            </td>
                                                            <td bgcolor="#ffffff">
                                                            <asp:Label Runat="server" Visible="False" ID="labisvisible" Text='<%# DataBinder.Eval(Container, "DataItem.isvisible") %>'/>
                                                                <asp:DropDownList runat="server" ID="drpisvisible">
                                                                    <asp:ListItem Value="yes" Text="显示" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="no" Text="隐藏"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="Gainsboro">
                                                                显示顺序
                                                            </td>
                                                            <td bgcolor="#ffffff">
                                                                <asp:TextBox runat="server" ID="txtorder" MaxLength=50 Text='<%# DataBinder.Eval(Container, "DataItem.order") %>'
                                                                    Width="50px">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <br />
                                            小计配置
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                             <asp:DataList ID="mySubTotal" runat="server" HorizontalAlign="left" GridLines="None"
                                                CellSpacing="1" RepeatDirection="Horizontal" RepeatColumns="4" BorderWidth="0"
                                                CellPadding="1" Width="80%" onitemdatabound="mySubTotal_ItemDataBound">
                                                <ItemTemplate>
                                                    <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="2"
                                                        width="200px" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
                                                         <tr>
                                                            <td bgcolor="Gainsboro" width="60px">
                                                                英文名称
                                                            </td>
                                                            <td bgcolor="#ffffff">
                                                                <asp:Label runat="server" ID="labename" Text='<%# DataBinder.Eval(Container, "DataItem.subtotalename") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="Gainsboro">
                                                                小计中文名称
                                                            </td>
                                                            <td bgcolor="#ffffff">
                                                                 <asp:TextBox runat="server" ID="txtcname" MaxLength=50 Text='<%# DataBinder.Eval(Container, "DataItem.subtotalcname") %>'
                                                                    Width="80px">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="Gainsboro">
                                                                是否进行小计
                                                            </td>
                                                            <td bgcolor="#ffffff">
                                                            <asp:Label Runat="server" Visible="False" ID="labissubtotal" Text='<%# DataBinder.Eval(Container, "DataItem.issubtotal") %>'/>
                                                                <asp:DropDownList runat="server" ID="drpissubtotal">
                                                                    <asp:ListItem Value="yes" Text="是"></asp:ListItem>
                                                                    <asp:ListItem Value="no" Text="否" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>                                                       
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                          <tr><td align="left"><br /><asp:Button runat="server" ID="btncolumn" 
                                            CssClass="buttonstyle" Text="保存配置" onclick="btncolumn_Click" />
                                            &nbsp;&nbsp;<input type="button" id="btnclose" value="关 闭" class="buttonstyle" onclick="window.close();" /></td></tr>
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
