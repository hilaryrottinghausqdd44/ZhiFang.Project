<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientContractItem.aspx.cs" Inherits="OA.Total.ClientContractItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>单位合同项目列表</title>
    <link href="../Css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../Css/calendar1.js"></script>
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
        function showdiv() 
        {
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
            document.getElementById('hidrmsg').value = "";
            table1.style.display = 'none';
            table2.style.display = 'none';
        }       
        function showOpenWindow(strurl) 
        {
            strurl = strurl + "?clientno=" + document.getElementById('labclientno').value + "&&clientname=" + document.getElementById('labclientname').outerText;
            window.open(strurl, 'new2', 'width=800px,height=650px,top=30,left=100px,scollbars=yes');
        }
        function showUserDialog(strurl) {
            strurl = strurl + "?clientno=" + document.getElementById('labclientno').value + "&&clientname=" + document.getElementById('labclientname').outerText;
            var r = window.showModalDialog(strurl, this, 'dialogWidth:800px;dialogHeight:680px;');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                window.location.href = "clientcontractitem.aspx?clientno=" + document.getElementById('labclientno').value + "&&clientname=" + document.getElementById('labclientname').outerText;
            }
        }
        //刷新页面
        function reloadpage() 
        {
            window.location.href = "clientcontractitem.aspx?clientno=" + document.getElementById('labclientno').value + "&&clientname=" + document.getElementById('labclientname').outerText;
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
    <table cellpadding="0" width="98%" height="100%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px;border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <asp:Label runat="server" ID="labclientname"></asp:Label>单位合同项目维护
                                <input type="hidden" id="hidrmsg" runat="server" />                            
                                <input type="hidden" id="labclientno" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                项目名:<asp:TextBox runat="server" CssClass="input_text" ID="txtitemname"></asp:TextBox>
                                &nbsp;<asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />&nbsp;
                                <a href="javascript:showUserDialog('ClientContractItemSelect.aspx');">单位套餐项目选择</a>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="labmsg" ForeColor="Red"></asp:Label>
                                            <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 500px">
                                                <asp:DataGrid ID="dgclient" runat="server" Style="cursor: hand; padding: 0px; margin: 0px;
                                                    font-size: 12px" PageSize="50" Font-Size="Smaller" BorderWidth="1px" CellPadding="3"
                                                    BorderStyle="None" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                                    BorderColor="BlanchedAlmond" Width="100%" OnItemDataBound="dgclient_ItemDataBound" 
                                                    onpageindexchanged="dgclient_PageIndexChanged" 
                                                    onitemcommand="dgclient_ItemCommand">
                                                    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                    <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                    <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                    </HeaderStyle>
                                                    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                    <Columns>                                                        
                                                        <asp:TemplateColumn SortExpression="项目名" HeaderText="项目名">
                                                            <HeaderStyle HorizontalAlign="Center" Width="40%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                (<asp:Label ID="labitemno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.itemno") %>'></asp:Label>)
                                                                <asp:Label ID="labitemname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.itemname") %>'></asp:Label>
                                                                <asp:Label ID="labContractItemId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ContractItemId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="英文名" HeaderText="英文名">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.ename") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                         <asp:TemplateColumn SortExpression="原价格" HeaderText="原价格">
                                                            <HeaderStyle HorizontalAlign="Center" Width="12%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                               <%# DataBinder.Eval(Container, "DataItem.price") %>                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>                                                     
                                                           
                                                         <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="left" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                              <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">删除</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
                                                </asp:DataGrid></div>
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
                                                
                                                <a href="javascript:showUserDialog('clientselect.aspx');">将此项目应用到多个单位</a>
                                                &nbsp;&nbsp;<a href="#" onclick="javascript:window.close();">取消</a>
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
