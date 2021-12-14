<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectBusinessMan.aspx.cs"
    Inherits="OA.RBAC.Organizations.SelectBusinessMan" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择业务员</title>

    <script type="text/javascript">

        function setParent(empID, empName) {
            var pa = window.dialogArguments;
            if (empID != null && empName != null) {
                if (pa) {
                    pa.setValue('0', empID, empName);
                    window.close();
                    //alert(empID + ',' + empName);
                }
            }
        }

        function openchild(subWindow, subWidth, subHeight) {
            var receiver = window.showModalDialog(subWindow, window, "dialogWidth=" + subWidth + "px;dialogHeight=" + subHeight + "px;help:no;status:no");
            if (receiver) {
                if (receiver != "0") {
                    alert(receiver);
                    $('labmid').value = receiver;
                }
            }
            else {
                //alert('没有接收到父窗体的值');
            }
        }
        function test() {
            openchild("Moduleaddressselect.aspx", 800, 600);
        }
        function $(s) {
            return document.getElementById ? document.getElementById(s) : document.all[s];
        }
        function SelectAll(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                if (elm[i].checked != xState)
                    elm[i].click();
            }
        }
        var prev = null;
        function selectx(row, index)   /**//*改变选中行的颜色还原为选中行的颜色*/
        {
            if (prev != null) {
                prev.style.backgroundColor = '#fff';
            }
            row.style.backgroundColor = '#e4ecf1';
            var result = $('ID_' + index).value + "," + $('NAME_' + index).value;
            //alert(result);
            window.returnValue = result;
            window.close();
        }
    </script>

    <style type="text/css">
        .hideClass
        {
            display: none;
        }
    </style>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="10"
            width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        业务员信息
                        <asp:Literal runat="server" Visible="false" ID="litid"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="labmsg"></asp:Label>
                        <asp:DataGrid ID="myDataGrid" runat="server" PageSize="15" Font-Size="Smaller" BorderWidth="1px"
                            CellPadding="3" borderColorDark="#ffffff" borderColorLight="#666666" BorderStyle="None"
                            AllowPaging="false" BackColor="White" AutoGenerateColumns="False" BorderColor="#A7C4F7"
                            Width="100%" OnItemDataBound="myDataGrid_ItemDataBound">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                            <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                            </HeaderStyle>
                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn SortExpression="业务员名称" HeaderText="业务员名称">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Visible="False" ID="labbmanno" Text='<%# DataBinder.Eval(Container, "DataItem.bmanno") %>' />
                                        <%# DataBinder.Eval(Container, "DataItem.cname") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle CssClass="hideClass"></HeaderStyle>
                                    <ItemStyle CssClass="hideClass"></ItemStyle>
                                    <ItemTemplate>
                                        <input id='<%#"ID_" + DataBinder.Eval(Container, "DataItem.bmanno")%>' value='<%#DataBinder.Eval(Container, "DataItem.bmanno")%>'
                                            type="text" />
                                        <input id='<%#"NAME_" + DataBinder.Eval(Container, "DataItem.bmanno")%>' value='<%# DataBinder.Eval(Container, "DataItem.cname") %>'
                                            type="text" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </td>
                </tr>
               
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
