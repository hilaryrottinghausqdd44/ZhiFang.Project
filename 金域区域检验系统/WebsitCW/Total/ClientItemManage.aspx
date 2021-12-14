<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="ClientItemManage.aspx.cs"
    Inherits="OA.Total.ClientItemManage" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户项目折扣维护</title>
    <link href="../Css/style.css" rel="stylesheet" />

    <link href="../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>

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
            document.getElementById('hidrmsg').value = "";
            table1.style.display = 'none';
            table2.style.display = 'none';
        }
        function showOpenWindow(strurl) {
            strurl = strurl + "?clientno=" + document.getElementById('labclientno').value + "&clientname=" + document.getElementById('labclientname').outerText;
            window.open(strurl, 'new2', 'width=800px,height=650px,top=30,left=100px,scollbars=yes');
        }
        function showUserDialog(strurl) {
            var sheight = screen.height - 70;
            var swidth = screen.width - 10;
            strurl = strurl + "?clientno=" + document.getElementById('labclientno').value + "&clientname=" + escape(document.getElementById('labclientname').outerText);
            var r = window.showModalDialog(strurl, this, 'dialogWidth:' + swidth + 'px;dialogHeight:' + sheight + 'px;minimize:yes;maximize:yes;');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                window.location.href = "clientitemmanage.aspx?clientno=" + document.getElementById('labclientno').value + "&clientname=" + escape(document.getElementById('labclientname').outerText);
            }
        }
        //刷新页面
        function reloadpage() {
            window.location.href = "clientitemmanage.aspx?clientno=" + document.getElementById('labclientno').value + "&clientname=" + document.getElementById('labclientname').outerText;
        }

        function SelectAll(tempControl) {
            //将除头模板中的其它所有的CheckBox取反 
            var theBox = tempControl;
            xState = theBox.checked;

            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++)
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id && elem[i].id != "chkcompute") {
                if (elem[i].checked != xState)
                    elem[i].click();
            }
        }

        //开始时间输入框中
        function judgedateb(txtbegindate) {
            //dgclient_ctl03_txtbdate dgclient_ctl03_txtedate
            var txtenddate = txtbegindate.id.substring(0, txtbegindate.id.length - 8) + 'txtedate';
            //alert(txtbegindate.value + ',' + txtenddate);
            var txtedatevalue = document.getElementById(txtenddate).value;
            if (txtedatevalue.length > 0) {
                var reg = new RegExp("-", "g");
                var evalue = parseInt(txtedatevalue.replace(reg, ""));
                var bvalue = parseInt(txtbegindate.value.replace(reg, ""));
                if (bvalue > evalue) {
                    alert('开始时间不能大于结束时间');
                    return false;
                }
                return true;
            }
        }
        //开始时间不能大于结束时间
        function judgedatee(txtenddate) {
            var txtbegindate = txtenddate.id.substring(0, txtenddate.id.length - 8) + 'txtbdate';
            var txtbdatevalue = document.getElementById(txtbegindate).value;
            if (txtbdatevalue.length > 0) {
                var reg = new RegExp("-", "g");
                var bvalue = parseInt(txtbdatevalue.replace(reg, ""));
                var evalue = parseInt(txtenddate.value.replace(reg, ""));
                if (bvalue > evalue) {
                    alert('开始时间不能大于结束时间');
                    return false;
                }
                return true;
            }
            else {
                alert('开始时间不能为空');
                return false;
            }
        }
        //项目折扣价格更新
        function ComputeAgioPrice(itemagio) {
            //dgclient_ctl03_txtclientitemagio    labtxtlowprice
            var itemagiovalue = itemagio.value;
            var reg = /^\d+(\.\d+)?$/g;
            if (itemagiovalue != "" && reg.test(itemagiovalue) == true) {
                //取得套餐项目价格
                var itemprice = itemagio.parentNode.parentNode.cells[4].innerHTML;
                if (itemprice.length > 0) {
                    itemprice = parseFloat(itemprice);
                    var itemagioprice = (itemprice * parseFloat(itemagiovalue) * 0.1).toFixed(2);
                    itemagio.parentNode.parentNode.cells[6].childNodes[0].firstChild.value = itemagioprice;
                    var lowprice = document.getElementById(itemagio.id.substring(0, 15) + 'labtxtlowprice').innerHTML;

                    if (parseFloat(itemagioprice) - parseFloat(lowprice) < 0) {
                        document.getElementById(itemagio.id.substring(0, 15) + 'labtxtlowprice').style.display = "block";
                        itemagio.parentNode.parentNode.cells[6].childNodes[0].firstChild.title = "低于此项目的最低价" + lowprice;
                        itemagio.parentNode.parentNode.cells[6].childNodes[0].firstChild.style.color = "red";
                        alert('低于此项目最低价' + lowprice);
                    }
                    else {
                        document.getElementById(itemagio.id.substring(0, 15) + 'labtxtlowprice').style.display = "none";
                        itemagio.parentNode.parentNode.cells[6].childNodes[0].firstChild.style.color = "Black";
                        itemagio.parentNode.parentNode.cells[6].childNodes[0].firstChild.title = "";
                    }
                }
                else {
                    alert('套餐项目价格不能为空');

                }
            }
            else { return false; }
            return true;
        }
        //根据套餐项目价格和项目折扣后价格计算折扣
        function ComputeAgio(itemagioprice) {
            //dgclient_ctl03_txtclientitemagioprice
            var itemagiopricevalue = itemagioprice.value;
            var reg = /^\d+(\.\d+)?$/g;
            if (itemagiopricevalue != "" && reg.test(itemagiopricevalue) == true) {
                //取得套餐项目价格
                //alert(itemagiopricevalue + ',' + itemagioprice.parentNode.parentNode.parentNode.cells[4].innerHTML);
                var itemprice = itemagioprice.parentNode.parentNode.parentNode.cells[4].innerHTML;
                if (itemprice.length > 0) {
                    itemprice = parseFloat(itemprice);
                    var itemagio = itemagioprice.parentNode.parentNode.parentNode.cells[5].firstChild.value;
                    if (itemprice == 0) {
                        itemagioprice.parentNode.parentNode.parentNode.cells[5].firstChild.value = 0;
                    } else {
                        itemagioprice.parentNode.parentNode.parentNode.cells[5].firstChild.value = ((itemagiopricevalue / itemprice) * 10).toFixed(2);
                    }
                    var lowprice = document.getElementById(itemagioprice.id.substring(0, 15) + 'labtxtlowprice').innerHTML;

                    if (parseFloat(itemagiopricevalue) - parseFloat(lowprice) < 0) {
                        document.getElementById(itemagioprice.id.substring(0, 15) + 'labtxtlowprice').style.display = "block";
                        document.getElementById(itemagioprice.id.substring(0, 15) + 'labtxtlowprice').style.color = "Red";
                        itemagioprice.parentNode.parentNode.parentNode.cells[6].childNodes[0].firstChild.title = "低于此项目的最低价" + lowprice;
                        itemagioprice.parentNode.parentNode.parentNode.cells[6].childNodes[0].firstChild.style.color = "Red";
                        alert('低于此项目最低价' + lowprice);
                    }
                    else {
                        document.getElementById(itemagioprice.id.substring(0, 15) + 'labtxtlowprice').style.display = "none";
                        itemagioprice.parentNode.parentNode.parentNode.cells[6].childNodes[0].firstChild.style.color = "Black";
                        itemagioprice.parentNode.parentNode.parentNode.cells[6].childNodes[0].firstChild.title = "";
                    }
                } else {
                    alert('套餐项目价格不能为空');
                }
            }
            else { return false; }
            return true;
        }
        
    </script>

    <style>
        .show
        {
            display: block;
            color: Red;
        }
        .hide
        {
            display: none;
        }
    </style>
    <base target="_self" />
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
                                    <asp:Label runat="server" ID="labclientname"></asp:Label>客户项目折扣维护</font>
                                <input type="hidden" id="hidrmsg" runat="server" />
                                <input type="hidden" id="labclientno" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                <table cellspacing="0" cellpadding="0" bgcolor="#fcfcfc" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                就诊类型:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="drpsicktypelist">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;
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
                                                <asp:DropDownList runat="server" ID="drpspecialsection" CssClass="input_var">
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
                                            <td>
                                                项目关键字:<asp:TextBox runat="server" CssClass="input_text" ID="txtitemname"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;<asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle"
                                                    OnClick="btnsearchitem_Click" />
                                            </td>
                                            <td>
                                                &nbsp;<a href="javascript:showUserDialog('ClientItemSelect.aspx');">客户套餐项目选择</a>
                                            </td>
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
                                                    OnPageIndexChanged="dgclient_PageIndexChanged" OnItemCommand="dgclient_ItemCommand">
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
                                                        <asp:TemplateColumn HeaderText="项目名">
                                                            <HeaderStyle HorizontalAlign="Center" Width="20%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                (<asp:Label ID="labitemno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.itemno") %>'></asp:Label>)
                                                                <asp:Label ID="labitemname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cname") %>'></asp:Label>
                                                                <asp:Label ID="labciaid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ciaid") %>'
                                                                    Visible="False"></asp:Label>
                                                                <asp:Label ID="labcbid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cbid") %>'
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
                                                        <asp:TemplateColumn HeaderText="就诊类型">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="labsicktypeno" Text='<%# DataBinder.Eval(Container, "DataItem.sicktypeno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="套餐项目价格">
                                                            <HeaderStyle HorizontalAlign="Center" Width="12%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.itemprice") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="项目折扣">
                                                            <HeaderStyle HorizontalAlign="left" Width="10%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Width="50px" ID="txtclientitemagio" onblur="ComputeAgioPrice(this);"
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.clientitemagio") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填项"
                                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtclientitemagio"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtclientitemagio"
                                                                    ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="项目折扣价格">
                                                            <HeaderStyle HorizontalAlign="left" Width="12%" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <div style="float: left">
                                                                    <asp:TextBox runat="server" Width="50px" ID="txtclientitemagioprice" onblur="ComputeAgio(this);"
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.itemagioprice") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填项"
                                                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtclientitemagioprice"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtclientitemagioprice"
                                                                        ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                                </div>
                                                                <div>
                                                                    <asp:Label runat="server" CssClass="hide" ID="labtxtlowprice" Text='<%# DataBinder.Eval(Container, "DataItem.lowprice") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="开始时间">
                                                            <HeaderStyle HorizontalAlign="left" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Width="80px" ID="txtbdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});" onpropertychange="judgedateb(this);"
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.begindate") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填项"
                                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtbdate"></asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="结束时间">
                                                            <HeaderStyle HorizontalAlign="left" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Width="80px" ID="txtedate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});" onpropertychange="judgedatee(this);"
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.enddate") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="left" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Save">保存</asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">删除</asp:LinkButton>
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
                                            <td colspan="3" align="center" style="border-bottom: #aecdd5 solid 1px;">
                                                <asp:CheckBox runat="server" ID="chkcompute" />是否对已更改项目折扣重新计算价格&nbsp;&nbsp;<asp:Button
                                                    runat="server" ID="btn" Text="批量保存" CssClass="buttonstyle" OnClick="btn_Click" />&nbsp;&nbsp;
                                                &nbsp;&nbsp;<asp:Button runat="server" ID="btndelete" CssClass="buttonstyle" Text="批量删除"
                                                    OnClick="btndelete_Click" />&nbsp;&nbsp;
                                                <input type="button" id="btnclose" value="取  消" class="buttonstyle" onclick="javascript:window.close();" />
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
