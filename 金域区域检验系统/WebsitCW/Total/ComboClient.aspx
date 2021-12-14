<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComboClient.aspx.cs" Inherits="OA.Total.ComboClient" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>套餐送检单位</title>
    <link href="../Css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../Css/calendar1.js"></script>

    <script type="text/javascript">

        function checkprice(obj) {
            var reg = /^\d+$/g;
            if (reg.test(obj.value) == false) {
                alert("项目价格请输入数字！");
                obj.focus();
                return false;
            }
            return true;
        }
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
                if (i == 0) break;
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
            document.cookie = name + "=" + escape(value)
        }
        //开始时间输入框中
        function judgedateb(txtbegindate) {
            var txtenddate = txtbegindate.parentNode.parentNode.cells[6].firstChild;
            if (txtenddate.value.length > 0) {
                var reg = new RegExp("-", "g");
                var evalue = parseInt(txtenddate.value.replace(reg, ""));
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
            var txtbegindate = txtenddate.parentNode.parentNode.cells[5].firstChild;
            if (txtbegindate.value.length > 0) {
                var reg = new RegExp("-", "g");
                var bvalue = parseInt(txtbegindate.value.replace(reg, ""));
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
    </script>

    <script type="text/javascript">
        //套餐单位列表
        function LoadComboOrgList() {
            var cbid = document.getElementById("labcbid").value;
            var key = document.getElementById("txtitemname").value;
            if (cbid.length > 0) {
                OA.Total.ComboClient.GetComboOrgList(cbid, key, GetCallComboOrgresult);
            }
        }
        function GetCallComboOrgresult(result) {
            var TB_ComboOrg_Obj = result.value;
            //alert(TB_ComboOrg_Obj);
            if (TB_ComboOrg_Obj != null) {
                if (tbcomboorg.rows.length > 0) 
                {
                    for (var i = tbcomboorg.rows.length; i > 0; i--) 
                    {
                        tbcomboorg.deleteRow();
                    }                    
                }
                var aRow = tbcomboorg.insertRow();
                aRow.style.background = "#a3f1f5"
                var aCell0 = aRow.insertCell();
                aCell0.align = "center";
                aCell0.innerHTML = "序号";

                var aCell6 = aRow.insertCell();
                aCell6.align = "center";
                aCell6.style.display = "none";

                var aCell7 = aRow.insertCell();
                aCell7.align = "center";
                aCell7.style.display = "none";

                var aCell1 = aRow.insertCell();
                aCell1.align = "center";
                aCell1.style.width = "50px";
                aCell1.innerHTML = "单位名称";

                var aCell2 = aRow.insertCell();
                aCell2.align = "center";
                aCell2.style.width = "50px";
                aCell2.innerHTML = "单位折扣";

                var aCell3 = aRow.insertCell();
                aCell3.align = "center";
                aCell3.style.width = "80px";
                aCell3.innerHTML = "开始时间";

                var aCell4 = aRow.insertCell();
                aCell4.align = "center";
                aCell4.style.width = "80px";
                aCell4.innerHTML = "结束时间";

                var aCell5 = aRow.insertCell();
                aCell5.innerHTML = "备注";

                //alert(TB_ComboOrg_Obj.length);
                for (var i = 0; i < TB_ComboOrg_Obj.length; i++) {
                    var bRow = tbcomboorg.insertRow();
                    bRow.onmouseover = new Function("this.style.backgroundColor='#ddf3dd';");
                    bRow.onmouseout = new Function("this.style.backgroundColor='white';");
                    bRow.id = TB_ComboOrg_Obj[i].CoId;
                    var aCell0 = bRow.insertCell();
                    aCell0.align = "center";
                    aCell0.innerHTML = i + 1;

                    var aCell6 = bRow.insertCell();
                    aCell6.align = "center";
                    aCell6.innerHTML = TB_ComboOrg_Obj[i].CoId;
                    aCell6.style.display = "none";

                    var aCell7 = bRow.insertCell();
                    aCell7.align = "center";
                    aCell7.innerHTML = TB_ComboOrg_Obj[i].ClientNo;
                    aCell7.style.display = "none";

                    var aCell1 = bRow.insertCell();
                    aCell1.align = "center";
                    aCell1.innerHTML = TB_ComboOrg_Obj[i].ClientName;

                    var aCell2 = bRow.insertCell();
                    aCell2.align = "center";
                    aCell2.innerHTML = "<input id='ClientAgio" + i + "' style='width:30px' value='" + TB_ComboOrg_Obj[i].ClientAgio + "' />";

                    var aCell3 = bRow.insertCell();
                    aCell3.align = "center";
                    aCell3.innerHTML = "<input id='BeginDate" + i + "' style='width:80px' onfocus='calendar()' onpropertychange='judgedateb(this);' value='" + TB_ComboOrg_Obj[i].BeginDate + "' />";

                    var aCell4 = bRow.insertCell();
                    aCell4.align = "center";
                    aCell4.innerHTML = "<input id='EndDate" + i + "' style='width:80px' onfocus='calendar()' onpropertychange='judgedatee(this);' value='" + TB_ComboOrg_Obj[i].EndDate + "' />";

                    var aCell5 = bRow.insertCell();
                    aCell5.innerHTML = "<a href='#' onclick='DeleteRow()'>删除</a>";
                }
            }
        }
        //增加一条套餐单位
        function InsertRow(clientno, clientname) {

            //先判断有没有相同单位记录存在
            for (var i = 0; i < tbcomboorg.rows.length; i++) {
                //取得在表中已存在的单位编号
                var tbclienno = tbcomboorg.rows[i].cells[2].innerHTML;
                if (clientno == tbclienno) {
                    alert('此单位已在该套餐中存在');
                    return false;
                }
            }
            var tb_comoboOrg_obj = new Object();
            tb_comoboOrg_obj.CoId = "";
            tb_comoboOrg_obj.ClientNo = clientno;
            tb_comoboOrg_obj.ClientName = clientname;
            tb_comoboOrg_obj.ClientAgio = "0";
            tb_comoboOrg_obj.BeginDate = "";
            tb_comoboOrg_obj.EndDate = "";

            if (tb_comoboOrg_obj != null) {
                var bRow = tbcomboorg.insertRow();
                bRow.onmouseover = new Function("this.style.backgroundColor='#ddf3dd';");
                bRow.onmouseout = new Function("this.style.backgroundColor='white';");
                bRow.id = tb_comoboOrg_obj.CoId;
                var i = tbcomboorg.rows.length + 1;
                var aCell0 = bRow.insertCell();
                aCell0.align = "center";
                aCell0.innerHTML = i + 1;

                var aCell6 = bRow.insertCell();
                aCell6.align = "center";
                aCell6.innerHTML = tb_comoboOrg_obj.CoId;
                aCell6.style.display = "none";

                var aCell7 = bRow.insertCell();
                aCell7.align = "center";
                aCell7.innerHTML = tb_comoboOrg_obj.ClientNo;
                aCell7.style.display = "none";

                var aCell1 = bRow.insertCell();
                aCell1.align = "center";
                aCell1.innerHTML = tb_comoboOrg_obj.ClientName;

                var aCell2 = bRow.insertCell();
                aCell2.align = "center";
                aCell2.innerHTML = "<input id='ClientAgio" + i + "' style='width:30px' value='" + tb_comoboOrg_obj.ClientAgio + "' />";

                var aCell3 = bRow.insertCell();
                aCell3.align = "center";
                aCell3.innerHTML = "<input id='BeginDate" + i + "' style='width:80px' onfocus='calendar()' onpropertychange='judgedateb(this);' value='" + tb_comoboOrg_obj.BeginDate + "' />";

                var aCell4 = bRow.insertCell();
                aCell4.align = "center";
                aCell4.innerHTML = "<input id='EndDate" + i + "' style='width:80px' onfocus='calendar()' onpropertychange='judgedatee(this);' value='" + tb_comoboOrg_obj.EndDate + "' />";

                var aCell5 = bRow.insertCell();
                aCell5.innerHTML = "<a href='#' onclick='DeleteRow()'>删除</a>";
            }
        }
        //删除行
        function DeleteRow() {
            var tbcomboorg = document.getElementById("tbcomboorg");
            if (tbcomboorg.rows.length > 1) {
                var currRowIndex = event.srcElement.parentNode.parentNode.rowIndex;
                tbcomboorg.deleteRow(currRowIndex);
            }
        }
        //保存已选定的套餐单位
        function SaveComboOrg() {
            //先判断有没有相同单位记录存在
            var combotmp = "";
            if (tbcomboorg.rows.length > 1) {
                var flag = "0";
                var comboid = document.getElementById("labcbid").value;    
                for (var i = 1; i < tbcomboorg.rows.length; i++) 
                {
                    var tbclientno = tbcomboorg.rows[i].cells[2].innerHTML;
                    var tbclientagio = tbcomboorg.rows[i].cells[4].firstChild.value;
                    var tbclientbegindate = tbcomboorg.rows[i].cells[5].firstChild.value;
                    var tbclientenddate = tbcomboorg.rows[i].cells[6].firstChild.value;
                    //判断折扣,日期不能为空
                    var reg = /^\d+(\.\d+)?$/g;
                    if (tbclientagio == "" || reg.test(tbclientagio) == false) {
                        flag = "1";
                        tbcomboorg.rows[i].cells[4].firstChild.style.backgroundColor = 'red';
                    }
                    else { tbcomboorg.rows[i].cells[4].firstChild.style.backgroundColor = ''; }
                    //开始时间
                    if (tbclientbegindate == "") {
                        flag = "1";
                        tbcomboorg.rows[i].cells[5].firstChild.style.backgroundColor = 'red';
                    }
                    else { tbcomboorg.rows[i].cells[5].firstChild.style.backgroundColor = ''; }
                    //结束时间
                    if (tbclientenddate == "") {
                        flag = "1";
                        tbcomboorg.rows[i].cells[6].firstChild.style.backgroundColor = 'red';
                    }
                    else { tbcomboorg.rows[i].cells[6].firstChild.style.backgroundColor = ''; }
                    //判断开始时间不能大小结束时间
                    if (tbclientbegindate.length > 0 && tbclientenddate.length > 0) {
                        var reg = new RegExp("-", "g");
                        var bvalue = parseInt(tbclientbegindate.replace(reg, ""));
                        var evalue = parseInt(tbclientenddate.replace(reg, ""));
                        if (bvalue > evalue) {
                            flag = "1";
                            tbcomboorg.rows[i].cells[5].firstChild.style.backgroundColor = 'red';
                            tbcomboorg.rows[i].cells[6].firstChild.style.backgroundColor = 'red';
                        }
                        else {
                            tbcomboorg.rows[i].cells[5].firstChild.style.backgroundColor = '';
                            tbcomboorg.rows[i].cells[6].firstChild.style.backgroundColor = '';
                        }
                    }     
                    combotmp = combotmp + tbclientno + '#' + tbclientagio + '#' + tbclientbegindate + '#' + tbclientenddate + '@';
                    //alert(tbclientno + ',' + tbcomboorg.rows[i].cells[3].innerHTML + ',' + tbclientagio + ',' + tbclientbegindate + ',' + tbclientenddate);
                }
                if (flag == "0") {
                    OA.Total.ComboClient.SaveComboOrg(comboid, combotmp.substring(0, combotmp.length - 1), GetCallSaveComboOrgresult);
                }
            } else {alert('请选择送检单位');}
        }
        function GetCallSaveComboOrgresult(result) {
            var flag = result.value;
            if (flag != null && flag == "0") {
                alert('保存已选套餐单位成功');
            }
            else 
            {
                if (flag.indexOf('#') > 0) 
                {
                    //存在时间交叉
                    var pam1 = new Array;
                    pam1 = flag.split("#");
                    for (var j = 0; j < pam1.length; j++) 
                    {
                        //先判断有没有相同单位记录存在
                        for (var i = 1; i < tbcomboorg.rows.length; i++) 
                        {
                            //取得在表中已存在的单位编号
                            var tbclienno = tbcomboorg.rows[i].cells[2].innerHTML;
                            if (pam1[j].length > 0) 
                            {
                                if (pam1[j] == tbclienno) 
                                {
                                    tbcomboorg.rows[i].cells[5].firstChild.style.backgroundColor = 'red';
                                    tbcomboorg.rows[i].cells[6].firstChild.style.backgroundColor = 'red';
                                }
                                else {
                                    tbcomboorg.rows[i].cells[5].firstChild.style.backgroundColor = '';
                                    tbcomboorg.rows[i].cells[6].firstChild.style.backgroundColor = '';
                                } 
                            }
                        }
                    }
                    alert('单位时间存在交叉');
                }
                else 
                {
                    alert('保存已选套餐单位失败' + flag);
                }
            }
        }
    </script>

    <base target="_self" />
</head>
<body onload="document.all('divdg').scrollTop=GetCookie('posy')">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="myScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" width="98%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <font size="12">套餐送检单位折扣管理</font>
                                <input type="hidden" id="labcbid" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Silver" valign="top" width="50%">
                                
                                <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            已有单位名:<input type="text" id="txtitemname" />&nbsp; &nbsp;&nbsp;<input type="button"
                                                id="btnsearchitem" class="buttonstyle" value="查询" onclick="LoadComboOrgList();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>已有单位:</strong><br />
                                            <div id="divdg" align="center" style="overflow: auto; width: 100%; height: 550px">
                                                <table id="tbcomboorg" width="100%" style="cursor: hand; padding: 0px; margin: 0px;
                                                    font-size: 12px" border="1" bordercolor="BlanchedAlmond" cellpadding="0" cellspacing="0">
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="color: Silver" valign="top" width="50%">
                                <asp:UpdatePanel ID="UpdatePanelItemList" runat="server">
                                    <ContentTemplate>
                                        <table bordercolor="#00CCFF" border="1" cellpadding="0" style="padding: 0px; margin: 0px;"
                                            cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    单位名:<asp:TextBox runat="server" ID="txtcbnamekey"></asp:TextBox>&nbsp;<asp:Button
                                                        runat="server" ID="btnsearch" Text="查询" CssClass="buttonstyle" OnClick="btnsearch_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>单位列表:</strong>
                                                    <br />
                                                    <asp:DataGrid ID="dgclient" runat="server" PageSize="25" Font-Size="Smaller" BorderWidth="1px"
                                                        CellPadding="3" BorderStyle="None" AllowPaging="true" BackColor="White" AutoGenerateColumns="False"
                                                        BorderColor="#A7C4F7" Width="100%" OnItemDataBound="dgclient_ItemDataBound" OnPageIndexChanged="dgclient_PageIndexChanged">
                                                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                                        <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                                        <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                                        </HeaderStyle>
                                                        <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn SortExpression="单位名" HeaderText="单位名">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="labclientno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.clientno") %>'
                                                                        Visible="False"></asp:Label>
                                                                    <asp:Label ID="labclientname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="英文名" HeaderText="英文名">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.ename") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="区域" HeaderText="区域">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.clientarea") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="等级" HeaderText="等级">
                                                                <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.clientstyle") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="left" Mode="NumericPages"></PagerStyle>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <input type="button" id="btnSave" value="保存已有套餐单位折扣" class="buttonstyle" onclick="SaveComboOrg();" />
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
