<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientItemSelect.aspx.cs"
    Inherits="OA.Total.ClientItemSelect" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位项目折扣选择</title>
    <base target="_self" />
    <link href="../Css/style.css" rel="stylesheet" />
    
    <link href="../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        function check() {
            var clientagio = document.getElementById('txtitemagio').value;
            if (clientagio == "" || clientagio == null) {
                alert('项目折扣不能为空!');
                document.getElementById('txtitemagio').focus();
                return false;
            }
            var reg = /^\d+(\.\d+)?$/g;
            if (clientagio != "" && reg.test(clientagio) == false) {
                alert('项目折扣必须为数字!');
                document.getElementById('txtitemagio').focus();
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
        function test()
        { window.opener.reloadpage(); }
        function SelectAll(tempControl) {
            //将除头模板中的其它所有的CheckBox取反 
            var theBox = tempControl;
            xState = theBox.checked;

            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++)
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id && elem[i].id != "chkcomputeprice" && elem[i].id != "chkitemagio") {
                //alert(elem[i].id);
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
        display: none; z-index: 2; width: 550; border-left: solid #000000 1px; border-top: solid #000000 1px;
        border-bottom: solid #000000 1px; border-right: solid #000000 1px; cursor: move;"
        cellspacing="0" cellpadding="0" oncontextmenu="return false">
        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
            border-top: #aecdd5 solid 8px; border-left: #aecdd5 solid 4px; border-right: #aecdd5 solid 4px;
            border-bottom: #aecdd5 solid 8px;" border="0">
            <tbody>
                <tr style="color: Red">
                    <td align="left" style="border-bottom: #aecdd5 solid 2px; width: 100;">
                        提示信息:
                    </td>
                    <td align="right" style="border-bottom: #aecdd5 solid 2px;">
                        <a href="#" onclick="closediv()">关闭</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divrmsg" style="word-wrap: break-word; float: left; text-align: left; overflow: hidden;
                            color: #313131;">
                            提示信息</div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <table cellpadding="0" width="98%" height="600px" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="4">
                                    <asp:Label runat="server" ID="labclientname"></asp:Label>
                                    单位项目折扣选择</font>
                                <input type="hidden" id="hidrmsg" runat="server" />
                                <input type="hidden" id="labclientno" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <table cellspacing="0" cellpadding="0" bgcolor="#fcfcfc" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                套餐:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="input_var" ID="drpclientcombolist"
                                                    OnSelectedIndexChanged="drpclientcombolist_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                检测方法:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" CssClass="input_var" ID="drpdiagmethod">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                专业类型:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" CssClass="input_var" ID="drpspecialtype">
                                                </asp:DropDownList>
                                            </td>
                                             <td align="left">
                                                专业小组:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="drpspecialsection"  CssClass="input_var" >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                项目标记：<asp:DropDownList ID="drpItemFlag" runat="server">
                                                            <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                                                            <asp:ListItem Value="1">在用</asp:ListItem>
                                                            <asp:ListItem Value="2">停用</asp:ListItem>
                                                        </asp:DropDownList>
                                            </td>
                                            <td>
                                                项目分类属性：<asp:DropDownList ID="drpItemType" runat="server">
                                                              </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                项目关键字:
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" CssClass="input_text" ID="txtitemname"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;<asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" /></td>
                                        </tr>
                                       
                                    </tbody>
                                </table>
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
                                                    OnPageIndexChanged="dgclient_PageIndexChanged">
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
                                                        <asp:TemplateColumn HeaderText="英文名">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.ename") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="套餐项目价格">
                                                            <HeaderStyle HorizontalAlign="left" Width="25%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="labitemprice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemPrice") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="项目折扣价">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Width="40px" ID="txtclientitemagioprice" Text='<%# DataBinder.Eval(Container, "DataItem.ItemPrice") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填项"
                                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtclientitemagioprice"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtclientitemagioprice"
                                                                    ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom">
                                                    </PagerStyle>
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
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                就诊类型:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:DropDownList runat="server" ID="drpsicktypelist">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                项目折扣:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:TextBox runat="server" Width="30px" CssClass="input_text" ID="txtitemagio" Text="10"></asp:TextBox>
                                                <font color="red">如:95折应该填9.5</font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                开始时间:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:TextBox runat="server" ID="txtbegindate" CssClass="input_text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"></asp:TextBox>
                                            </td>
                                            <td align="right" style="border-bottom: #aecdd5 solid 1px;">
                                                结束时间:
                                            </td>
                                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:TextBox runat="server" ID="txtenddate" CssClass="input_text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"></asp:TextBox>
                                                <input type="button" id="btnclear" value="清空" class="btn_blue" onclick="document.getElementById('txtenddate').value='';" />为空表示长期有效
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="left" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:CheckBox runat="server" ID="chkcomputeprice" />是否重新计算项目历史价格
                                                &nbsp;<asp:CheckBox runat="server" ID="chkitemagio" Checked="true" />是否按项目折扣计算项目结算价格 &nbsp;
                                                <asp:Button runat="server" ID="btn" Text="保 存" CssClass="buttonstyle" OnClick="btn_Click" />&nbsp;&nbsp;
                                                <input type="button" id="btnclose" value="关 闭" class="buttonstyle" onclick="javascript:window.returnValue='0';window.close();" />
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
