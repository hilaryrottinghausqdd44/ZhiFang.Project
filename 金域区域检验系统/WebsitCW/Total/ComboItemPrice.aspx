<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComboItemPrice.aspx.cs"
    Inherits="OA.Total.ComboItemPrice" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>套餐项目价格</title>
    <base target="_self" />
    <link href="../Css/style.css" rel="stylesheet" />
    <link href="../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        //得到名称为posy的cookie值
        function GetCookie(name) {
            var arg = name + "=";
            var alen = arg.length;
            var clen = document.cookie.length;
            var i = 0;
            while (i < clen) {
                var j = i + alen;
                //判断是否是所指定的key
                if (document.cookie.substring(i, j) == arg)
                    return getCookieVal(j);
                i = document.cookie.indexOf("   ", i) + 1;
                if (i == 0)
                    break;
            }
            return null;
        }
        //得到key的值value 
        function getCookieVal(offset) {
            var endstr = document.cookie.indexOf(";", offset);
            if (endstr == -1)
                endstr = document.cookie.length;
            return unescape(document.cookie.substring(offset, endstr));
        }
        //设置当前滑动坐标值到缓存
        function SetCookie(name, value) {
            document.cookie = name + "=" + escape(value);
        }
        //套餐单位列表
        function LoadComboOrgList() {
            var cbid = document.getElementById("labcbid").value;
            var key = document.getElementById("txtitemname").value;
            if (cbid.length > 0) {
                OA.Total.ComboItemPrice.GetComboOrgList(cbid, key, GetCallComboOrgresult);
            }
        }
        function GetCallComboOrgresult(result) {
            var dataset = result.value;
            //alert(dataset.Tables[0].Rows.length);
            var tbcomboorg = document.getElementById("tbcomboorg");

            if (dataset.Tables[0].Rows.length > 0) {
                for (var i = tbcomboorg.rows.length; i > 0; i--) {
                    tbcomboorg.deleteRow();
                }
            }
            var aRow = tbcomboorg.insertRow();
            aRow.style.background = "#a3f1f5"

            var aCell0 = aRow.insertCell();
            aCell0.align = "center";
            aCell0.style.width = "30px";
            aCell0.innerHTML = "序号";

            var aCell6 = aRow.insertCell();
            aCell6.align = "center";
            aCell6.style.display = "none";

            var aCell1 = aRow.insertCell();
            aCell1.align = "center";
            aCell1.style.width = "80px";
            aCell1.innerHTML = "项目名称";

            var aCell2 = aRow.insertCell();
            aCell2.align = "center";
            aCell2.style.width = "80px";
            aCell2.innerHTML = "英文名称";

            var aCell4 = aRow.insertCell();
            aCell4.align = "center";
            aCell4.style.width = "60px";
            aCell4.innerHTML = "原始价格";

            var aCell3 = aRow.insertCell();
            aCell3.align = "center";
            aCell3.style.width = "60px";
            aCell3.innerHTML = "价格";


            //alert(dataset.Tables[0].Rows.length);
            for (var i = 0; i < dataset.Tables[0].Rows.length; i++) {
                var bRow = tbcomboorg.insertRow();
                bRow.onmouseover = new Function("this.style.backgroundColor='#ddf3dd';");
                bRow.onmouseout = new Function("this.style.backgroundColor='white';");
                //bRow.id = dataset.Tables[0].Rows[i].ciid;

                var aCell0 = bRow.insertCell();
                aCell0.align = "center";
                aCell0.innerHTML = i + 1;

                var aCell6 = bRow.insertCell();
                aCell6.align = "center";
                aCell6.innerHTML = dataset.Tables[0].Rows[i].itemno;
                aCell6.style.display = "none";

                var aCell7 = bRow.insertCell();
                aCell7.align = "center";
                aCell7.innerHTML = dataset.Tables[0].Rows[i].cname;

                var aCell1 = bRow.insertCell();
                aCell1.align = "center";
                aCell1.innerHTML = dataset.Tables[0].Rows[i].ename;

                var aCell3 = bRow.insertCell();
                aCell3.align = "center";
                aCell3.innerHTML = dataset.Tables[0].Rows[i].price;

                var aCell2 = bRow.insertCell();
                aCell2.align = "center";
                aCell2.innerHTML = "<input id='Itemprice" + i + "' style='width:50px' value='" + dataset.Tables[0].Rows[i].itemprice + "' />";


            }
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
            table1.style.display = 'none';
            table2.style.display = 'none';
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

        function showTestItemUserDialog(cbid, paritemno, ciid) 
        {
            strurl = "combotestitemprice.aspx?cbid=" + cbid + "&paritemno=" + paritemno + "&ciid=" + ciid;
            var r = window.showModalDialog(strurl, this, 'dialogWidth:600px;dialogHeight:500px;');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') 
            {
                return;
            }
            else 
            {
                if (r == "1") 
                {
                    document.getElementById("btnmiddletest").click();                    
                }
            }
        }

        function CloseExcel(flag, filename) {
            location.href = "downloadfile.aspx?file=Excel/" + escape(filename);
        }
    </script>

    
</head>
<body onload="divdg.scrollTop=GetCookie('posy')" onunload="SetCookie('posy',divdg.scrollTop)">

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
    <table cellpadding="0" width="99%" height="550px" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                套餐项目价格管理
                                <input type="hidden" id="hidrmsg" runat="server" />
                                <input type="hidden" id="labcbid" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%" style="color: #ff3300; border-bottom: #aecdd5 solid 1px;">
                                项目关键字:<input type="text" id="txtitemname" runat="server" />&nbsp; 项目类型：<asp:DropDownList
                                    ID="drpItemType" runat="server">
                                    <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                                    <asp:ListItem Value="1">单项</asp:ListItem>
                                    <asp:ListItem Value="2">组合项目</asp:ListItem>
                                    <asp:ListItem Value="3">组套项目</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList
                                    runat="server" ID="drpisprofile" Visible="false">
                                    <asp:ListItem Value="2" Selected="True">全部</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                                组合项目与子项价格:<asp:DropDownList
                                    runat="server" ID="drpsameprice">
                                    <asp:ListItem Value="2" Selected="True">未选</asp:ListItem>
                                    <asp:ListItem Value="1">相等</asp:ListItem>
                                    <asp:ListItem Value="0">不相等</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                                项目标记:<asp:DropDownList ID="drpItemFlag" runat="server">
                                    <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                                    <asp:ListItem Value="1">在用</asp:ListItem>
                                    <asp:ListItem Value="2">停用</asp:ListItem>
                                </asp:DropDownList>&nbsp; 
                                <asp:Button runat="server" ID="btnsearchitem" Text="查询" CssClass="buttonstyle" OnClick="btnsearchitem_Click" />
                                <asp:Button runat="server" ID="btnmiddletest" Width=0 Height=0 CssClass="buttonstyle" OnClick="btnmiddletest_Click" />
                                &nbsp;<asp:Button ID="btnExport" runat="server" Text="导出Excel" 
                                    CssClass="buttonstyle" onclick="btnExport_Click" />
                                &nbsp;<asp:Button runat="server" ID="btnCreateComboItem" Text="提取套餐项目" CssClass="buttonstyle"
                                    OnClick="btnCreateComboItem_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="100%">
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="labmsg"></asp:Label>
                                            <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 500px">
                                                <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                    font-size: 12px" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                </table>
                                                <asp:DataGrid ID="dgcomboitem" runat="server" Style="cursor: hand; padding: 0px;
                                                    margin: 0px; font-size: 12px" PageSize="50" Font-Size="Smaller" BorderWidth="1px"
                                                    CellPadding="3" BorderStyle="None" AllowPaging="True" BackColor="White" AutoGenerateColumns="False"
                                                    BorderColor="BlanchedAlmond" Width="98%" OnItemDataBound="dgcomboitem_ItemDataBound"
                                                    OnPageIndexChanged="dgcomboitem_PageIndexChanged" OnItemCommand="dgcomboitem_ItemCommand">
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
                                                                <asp:Label ID="labtestitemprice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.testitemprice") %>'
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
                                                        <asp:TemplateColumn SortExpression="组合项目" HeaderText="组合项目">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <a href="#" onclick="javascript:showTestItemUserDialog(<%# DataBinder.Eval(Container, "DataItem.cbid") %>,<%# DataBinder.Eval(Container, "DataItem.itemno") %>,<%# DataBinder.Eval(Container, "DataItem.ciid") %>);">
                                                                    <asp:Label runat="server" ID="labisprofile" Text='<%# DataBinder.Eval(Container, "DataItem.isprofile") %>'></asp:Label>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="项目状态" HeaderText="项目状态">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.Visible").ToString() == "1" ? "在用" : "禁用"%>
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
                                                                <asp:Label ID="lblTempItemPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemPrice") %>' Visible="false"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="必填项"
                                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtcomboitemprice"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtcomboitemprice"
                                                                    ErrorMessage="必须为数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d+)?$"
                                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="历史价格" SortExpression="历史价格">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="LastItemPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastItemPrice") %>'></asp:Label>                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="修改日期" SortExpression="修改日期">
                                                            <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="CreateDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreateDate")%>'></asp:Label>
                                                                
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
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom" PageButtonCount="30">
                                                    </PagerStyle>
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnsave" Text="批量保存套餐项目价格" CssClass="buttonstyle"
                                    OnClick="btnsave_Click" />
                                &nbsp;&nbsp;重算数据的日期范围
                                    <asp:TextBox runat="server" ID="txtDateBegin" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});" Width="65px"></asp:TextBox>
                                    -<asp:TextBox runat="server" ID="txtDateEnd" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});" Width="65px"></asp:TextBox>
                                    <asp:CheckBox runat="server" ID="chkcompute" />对已更改的项目是否重新计算价格
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
