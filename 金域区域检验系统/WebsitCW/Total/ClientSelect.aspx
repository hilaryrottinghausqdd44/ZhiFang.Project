<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientSelect.aspx.cs" Inherits="OA.Total.ClientSelect" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位选择</title>
        <link href="../Css/style.css" rel="stylesheet" />
    <script type="text/javascript">
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
            table2.style.left = document.documentElement.scrollLeft + 100;
            table2.style.top = document.body.scrollTop + 100;
            table2.style.display = 'block';
            divrmsg.innerHTML = document.getElementById('hidrmsg').value;
            fchange();
        }

        function closediv() {
            table1.style.display = 'none';
            table2.style.display = 'none';
        }
        function SelectAll(tempControl) {
            //将除头模板中的其它所有的CheckBox取反 
            var theBox = tempControl;
            xState = theBox.checked;

            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++)
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id) {
                if (elem[i].checked != xState)
                    elem[i].click();
            }
        }
    </script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <div id="table1" style="background: #FFFFFF; display: none; position: absolute; z-index: 1;
        filter: alpha(opacity=40)" oncontextmenu="return false">
    </div>
    <div id="table2" style="position: absolute; onmousedown=mdown(table2); background: #fcfcfc;
        display: none; z-index: 2; width: 550; border-left: solid #000000 1px;
        border-top: solid #000000 1px; border-bottom: solid #000000 1px; border-right: solid #000000 1px;
        cursor: move;" cellspacing="0" cellpadding="0" oncontextmenu="return false">
       
        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
            border-top: #aecdd5 solid 8px;border-left: #aecdd5 solid 4px;border-right: #aecdd5 solid 4px; border-bottom: #aecdd5 solid 8px;" border="0">
            <tbody>
                <tr style="color:Red">
                    <td align="left" style="border-bottom: #aecdd5 solid 2px;width:100;">提示信息:</td>
                    <td align="right" style="border-bottom: #aecdd5 solid 2px;">
                      <a href="#" onclick="closediv()">关闭</a>
                    </td>
                </tr>
                <tr><td colspan="2"><div id="divrmsg" style="word-wrap:break-word;float:left;text-align:left;overflow:hidden;color:#313131;">提示信息</div></td></tr>
            </tbody>
        </table>
    </div>
    <table cellpadding="0" width="98%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                               单位选择
                                <input type="hidden" id="labclientno" runat="server" />
                                 <input type="hidden" id="hidrmsg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                区域:<asp:DropDownList runat="server" CssClass="input_var" ID="drpclientarealist">
                                </asp:DropDownList>
                                &nbsp; 单位名:<asp:TextBox runat="server" CssClass="input_text" ID="txtclientname"></asp:TextBox>&nbsp;
                                <asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />
                                &nbsp;&nbsp; 
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="labmsg"></asp:Label>                                             
                                            <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 530px">
                                            <asp:DataGrid ID="dgclient" runat="server" Style="cursor: hand; padding: 0px; margin: 0px;
                                                font-size: 12px" PageSize="300" Font-Size="Smaller" BorderWidth="1px" CellPadding="3"
                                                BorderStyle="None" AllowPaging="false" BackColor="White" AutoGenerateColumns="False"
                                                BorderColor="BlanchedAlmond" Width="100%" OnItemDataBound="dgclient_ItemDataBound">
                                                <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                </HeaderStyle>
                                                <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                <Columns>
                                                      <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="False" onclick="javascript:SelectAll(this);">
                                                                </asp:CheckBox>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkitem" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                      <asp:TemplateColumn SortExpression="单位名称" HeaderText="单位名称">
                                                            <HeaderStyle HorizontalAlign="Center" Width="30%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="labclientname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cname") %>'></asp:Label>(<asp:Label ID="labclientno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.clientno") %>'></asp:Label>)
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                         <asp:TemplateColumn SortExpression="区域" HeaderText="区域">
                                                            <HeaderStyle HorizontalAlign="Center" Width="20%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.clientarea") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                         <asp:TemplateColumn SortExpression="级别" HeaderText="级别">
                                                            <HeaderStyle HorizontalAlign="Center" Width="20%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.clientstyle")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                          <tr>
                            <td colspan="2" align="center">
                                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                                    <tbody>                                      
                                        <tr>
                                            <td colspan="3" align="center" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:Button runat="server" ID="btn" Text="保存" CssClass="buttonstyle" OnClick="btn_Click" />&nbsp;&nbsp;<input type="button" id="btnclose" value="取消" class="buttonstyle" onclick="javascript:window.close();" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </tbody>
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
