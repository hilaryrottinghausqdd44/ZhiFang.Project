<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientDetialInfo.aspx.cs"
    Inherits="OA.Total.ScLab.ClientDetialInfo" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title>客户：<%=ClientName%> 明细列表</title>
    <link href="../../Css/style.css" rel="stylesheet" />       

    <link href="../../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>
    
    <script language="javascript" src="Lodop/LodopFuncs.js"></script>
    <script src="../../JavaScriptFile/jquery-1.8.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        .table_head td {BORDER-RIGHT:black 1px  solid;BORDER-TOP:black 1px  solid;BORDER-LEFT:black 1px  solid;BORDER-BOTTOM:black 1px  solid;background-color:Silver;}
        #table_list { background-color:White;border-color:#A7C4F7;border-width:1px;border-style:None;font-size:Smaller;width:100%;border-collapse:collapse;}
        #table_list tr {color:Black;background-color:White;font-size:Smaller;font-weight:bold;height:26px;}
        #table_list td {BORDER-RIGHT:black 1px  solid;BORDER-TOP:black 1px  solid;BORDER-LEFT:black 1px  solid;BORDER-BOTTOM:black 1px  solid}
        
        .black_overlay{ 
            display: none; 
            position: absolute; 
            top: 0%; 
            left: 0%; 
            width: 100%; 
            height: 100%; 
            background-color: black; 
            z-index:1001; 
            -moz-opacity: 0.8; 
            opacity:.80; 
            filter: alpha(opacity=60); 
        } 
        .white_content { 
            display: none; 
            position: absolute; 
            top: 25%; 
            left: 25%; 
            width: 45%; 
            height: 45%; 
            padding: 20px; 
            border: 10px solid DeepSkyBlue; 
            background-color: white; 
            z-index:1002; 
            overflow: auto; 
        } 
    </style>
    
    <script type="text/javascript">

        function EditItemPrice(NRNO) {
            var ItemPrice = $("#text_1_" + NRNO).val();
            var ItemAgio = $("#text_2_" + NRNO).val();
            var ItemAgioPrice = $("#text_3_" + NRNO).val();
            $.ajax({
                type: "post",
                url: "../HandlerBLL/CheckAccount.ashx",
                data: { action: "EditItem", itemprice: ItemPrice, itemagio: ItemAgio, itemagioprice: ItemAgioPrice, no: NRNO },
                dataType: "json",
                success: function(data) {                   
                    if (data != null) {
                        var result = data.ResultDataValue;
                        if (result == "1") {
                            alert("修改成功");
                        } else {
                            alert("修改失败");
                        }
                    }
                }
            });
        }

        function CalcPriceInList(obj, NRNO, type) {//alert("aa");
            //type=1,原价改，是原价+折扣=折后
            //type=2,折扣改，是原价+折扣=折后
            //type=3,折后改，是原价+折后=折扣
            if (type == 1) {
                var price = obj.value;
                var agio = $("#text_2_" + NRNO).val();
                if (price != null && price != "" && agio != null && agio != "") {
                    var agioprice = parseFloat(price) * parseFloat(agio) / 10;
                    $("#text_3_" + NRNO).attr("value", agioprice);
                }
            } else if (type == 2) {
                var price = $("#text_1_" + NRNO).val();
                var agio = obj.value;
                if (price != null && price != "" && agio != null && agio != "") {
                    var agioprice = parseFloat(price) * parseFloat(agio) / 10;
                    $("#text_3_" + NRNO).attr("value", agioprice);
                }
            } else if (type == 3) {
                var price = $("#text_1_" + NRNO).val();
                var agioprice = obj.value;
                if (price != null && price != "" && agioprice != null && agioprice != "") {
                    var agio = (parseFloat(agioprice) * 10) / parseFloat(price);
                    $("#text_2_" + NRNO).attr("value", agio);
                }
            }
        }        

        function CalcPrice(obj, type) {
            var price = $("#txtPrice").val();
            if (type == 0) {
                //输入项目价格、折扣后，自动计算折后价格（表单里使用）
                var agio = obj.value;                
                if (price != null && price != "" && agio != null && agio != "") {
                    var agioprice = parseFloat(price) * parseFloat(agio) / 10;
                    $("#txtAgioPrice").attr("value", agioprice);
                }
            } else {
                //输入项目价格、折后价格后，自动计算折扣（表单里使用）
                var agioprice = obj.value;
                if (price != null && price != "" && agioprice != null && agioprice != "") {
                    var agio = (parseFloat(agioprice) * 10) / parseFloat(price);
                    $("#txtAgio").attr("value", agio);
                }
            }
        }

        function OpenDiv() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }

        function closediv() {
            $("#light").hide();
            $("#fade").hide();
        }

        function savedata() {
            var clientno = "<%=ClientNo%>";
            var date = $("#hiddenDate").val();
            var itemno = $("#ddlItems").val();
            if (itemno == null || itemno == "") {
                alert("请选择项目！");
                return false;
            }
            //alert(clientno + "-" + date + "-" + itemno);
            if (date != "") {
                $.ajax({
                    type: "post",
                    url: "../HandlerBLL/CheckAccount.ashx",
                    data: { action: "EditItemBatch", itemprice: $("#txtPrice").val(), itemagio: $("#txtAgio").val(), itemagioprice: $("#txtAgioPrice").val(), clientno: clientno, itemno: itemno, date: date },
                    dataType: "json",
                    success: function(data) {
                        if (data != null) {
                            var result = data.ResultDataValue;
                            var row = data.count;
                            if (parseInt(row) > 0) {
                                alert("批量修改成功");
                            } else {
                                alert("批量修改失败 " + result);
                            } 
                        }
                    }
                });
            }
        }





        $(function() {
            //全选/反选
            $("#chkAll").click(function() {
                if (this.checked) {
                    $("input[name='chkNRequestItemNo']").each(function() {
                        this.checked = true;
                    });
                } else {
                    $("input[name='chkNRequestItemNo']").each(function() {
                        this.checked = false;
                    });
                }

            });

            $("#btnClearData").click(function() {
                var checkcount = $("input[name = 'chkNRequestItemNo']:checked").length;
                if (checkcount > 0) {
                    var select = "";
                    $('input[name="chkNRequestItemNo"]:checked').each(function() {
                        select = select + $(this).val() + ',';
                    })
                    select = select.substring(0, select.lastIndexOf(','));
                    $.ajax({
                        type: "post",
                        url: "../HandlerBLL/CheckAccount.ashx",
                        data: { action: "ClearAccountData", nrequestitemnolist: select },
                        dataType: "json",
                        success: function(data) {
                            if (data != null) {
                                var result = data.ResultDataValue;
                                if (result) {
                                    //alert("数据剔除成功！");
                                    $("#btnRefresh").trigger("click");
                                } else {
                                    alert("数据剔除失败！");
                                }
                            }
                        }
                    })


                } else {
                    alert("请选中要剔除的数据！");
                }
            });
            $("#btnAddData").click(function() {
                var clientno = "<%=ClientNo%>";
                var clientname = "<%=ClientName %>";
                var accountmonth = "<%=AccountMonth%>";
                var sheight = screen.height - 70;
                var swidth = screen.width - 10;
                var url = "ClientUnAccountData.aspx?ClientNo=" + clientno + "&ClientName=" + clientname + "&AccountMonth=" + accountmonth;
                var win = window.showModalDialog(url, window, "scroll:yes;status:no;center:yes;minimize:yes;maximize:yes;dialogHeight:" + sheight + ";dialogWidth:" + swidth + ";");
                $("#btnRefresh").trigger("click");
            });

            $("#btnPrint").click(function() {
                var clientno = "<%=ClientNo%>";
                var clientname = "<%=ClientName %>";
                var accountmonth = "<%=AccountMonth%>";
                var sheight = screen.height - 70;
                var swidth = screen.width - 10;
                var url = "ClientDetialInfo_Print.aspx?ClientNo=" + clientno + "&ClientName=" + clientname + "&AccountMonth=" + accountmonth;
                var sFeathers = "scrollbars=yes,height=" + sheight + ",width=" + swidth;
                window.open(url, 'openprint', sFeathers);
                //PrintOneURL(url);
            });

            $("#btnSearchMissingData").click(function() {
                var clientno = "<%=ClientNo%>";
                var clientname = "<%=ClientName %>";
                var accountmonth = "<%=AccountMonth%>";
                var accountdayset = "<%=AccountDaySet%>";
                var sheight = screen.height - 70;
                var swidth = screen.width - 10;
                var url = "ClientUnAccountData.aspx?ClientNo=" + clientno + "&ClientName=" + clientname + "&AccountMonth=" + accountmonth + "&accountdayset=" + accountdayset;
                var win = window.showModalDialog(url, window, "scroll:yes;status:no;center:yes;minimize:yes;maximize:yes;dialogHeight:" + sheight + ";dialogWidth:" + swidth + ";");
                $("#btnRefresh").trigger("click");
            });

        })


        var LODOP; //声明为全局变量      
        function PrintOneURL(URL) {
            LODOP = getLodop();
            LODOP.PRINT_INIT("打印控件功能演示_Lodop功能_按网址打印");
            LODOP.SET_PRINT_PAGESIZE(1, 0, 0, "A4");
            LODOP.ADD_PRINT_URL(50, "5%", "90%", "90%", URL);
            //LODOP.SET_PRINT_STYLEA(0, "HOrient", 3);
            LODOP.SET_PRINT_STYLEA(0, "VOrient", 3);
            //LODOP.SET_PRINT_STYLEA(0, "TableRowThickNess", 25);
            //LODOP.ADD_PRINT_HTM(26, "5%", "90%", 109, "金沙洲医院测试");
            //		LODOP.SET_SHOW_MODE("MESSAGE_GETING_URL",""); //该语句隐藏进度条或修改提示信息
            //		LODOP.SET_SHOW_MODE("MESSAGE_PARSING_URL","");//该语句隐藏进度条或修改提示信息
            LODOP.PREVIEW();
        };	

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hiddenDate" runat="server" />
    <div style="margin-top:10px; margin-left:10px; margin-right:10px; margin-bottom:10px;">
    
    <div id="searchcon" style="text-align:right">
            姓名：<asp:TextBox ID="txtCName" runat="server" Width="60px" CssClass="input_text"></asp:TextBox>
            日期：
            <asp:TextBox runat="server" ID="txtDateBegin" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});" Width="65px"></asp:TextBox>
            -<asp:TextBox runat="server" ID="txtDateEnd" CssClass="input_text" ondblclick="WdatePicker({dateFmt:'yyyy-MM-dd'});" Width="65px"></asp:TextBox>
            条码号：<asp:TextBox ID="txtSerialNo" runat="server" CssClass="input_text"></asp:TextBox>
            项目名称：<asp:TextBox ID="txtItem" runat="server" CssClass="input_text"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="buttonstyle" 
                onclick="btnSearch_Click" />
        </div>
        <div style="margin-bottom:2px">            
            <input type="button" id="btnBatchUpdate" value="批量修改" onclick="OpenDiv();" class="buttonstyle" />
            <asp:Button ID="btnRefresh" runat="server" Text="刷新列表" 
                onclick="btnRefresh_Click" CssClass="buttonstyle" /> 
            <input type="button" id="btnClearData" value="剔除选中数据" class="buttonstyle" />
            <input type="button" id="btnAddData" value="处理未结算数据" class="buttonstyle" />
            <input type="button" id="btnSearchMissingData" value="查看是否遗漏数据" class="buttonstyle" />           
            <input type="button" id="btnPrint" value="打印" class="buttonstyle" />
            <asp:Button ID="btnPrintFrx" runat="server" onclick="btnPrintFrx_Click" Text="Frx打印" Visible="false" />
&nbsp;<input type="button" id="btnClose" value="关闭" onclick="window.close();" class="buttonstyle" />
            <asp:Label ID="lblInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="table_list">
            <tr class="table_head">
                <td>
                    <input id="chkAll" name="chkAll" type="checkbox"  />
                </td>
                <td>
                    姓名
                </td>
                <td>
                    日期
                </td>
                <td>
                    条码号
                </td>
                <td>
                    项目名称
                </td>
                <td>
                    项目价格
                </td>
                <td>
                    项目折扣
                </td>
                <td>
                    折后价格
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rep_list" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <input id="chkNRequestItemNo" name="chkNRequestItemNo" type="checkbox" value='<%# DataBinder.Eval(Container, "DataItem.NRequestItemNo") %>' />
                        </td>
                        <td>
                            <%# Eval("CName")%>
                        </td>
                        <td>
                            <%# Convert.ToDateTime(Eval("inceptDate").ToString()).ToString("yyyy-MM-dd")%>
                        </td>
                        <td>
                            <%# Eval("SerialNo")%>
                        </td>
                        <td>
                            <%# Eval("ItemNamecw")%>
                        </td>
                        <td>
                            <input id="text_1_<%# Eval("NRequestItemNo")%>" type="text" value='<%# Eval("ItemPrice")%>' style="width:50px" onchange="CalcPriceInList(this,'<%# Eval("NRequestItemNo")%>',1);" />
                        </td>
                        <td>
                            <input id="text_2_<%# Eval("NRequestItemNo")%>" type="text" value='<%# Eval("ItemAgio")%>' style="width:50px" onchange="CalcPriceInList(this,'<%# Eval("NRequestItemNo")%>',2);" />
                        </td>
                        <td>
                            <input id="text_3_<%# Eval("NRequestItemNo")%>" type="text" value='<%# Eval("ItemAgioPrice")%>' style="width:50px" onchange="CalcPriceInList(this,'<%# Eval("NRequestItemNo")%>',3);" />
                        </td>
                        <td>
                            <input type="button" value="保存" onclick="EditItemPrice('<%# Eval("NRequestItemNo")%>');" class="buttonstyle"  />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                     <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            标本汇总：<asp:Label ID="lblFormCount" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            折前汇总：<asp:Label ID="lblSumItemPrice" runat="server" Text=""></asp:Label>
                        </td>
                        <td</td>
                        <td></td>
                        <td>
                            折后汇总：<asp:Label ID="lblSumItemAgioPrice" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                    </tr></FooterTemplate></asp:Repeater></table></div><div style="text-align:center" runat="server" id="div_page">
              <asp:LinkButton ID="linkFirst" runat="server" onclick="linkFirst_Click">首页</asp:LinkButton>&nbsp;<asp:LinkButton ID="linkUp" runat="server" onclick="linkUp_Click">上一页</asp:LinkButton>&nbsp;<asp:LinkButton ID="linkNext" runat="server" onclick="linkNext_Click">下一页</asp:LinkButton>&nbsp;<asp:LinkButton ID="linkLast" runat="server" onclick="linkLast_Click">尾页</asp:LinkButton>&nbsp;<asp:Label ID="lblPageInfo" runat="server" Text="当前第N页，共M页"></asp:Label></div>
              
              <div id="light" class="white_content">       
        <h5 style="text-align:center">批量修改项目价格信息</h5><table width="100%" border="0" cellspacing="1" cellpadding="0" bgcolor="#E5E5E5">
            <tr style="background-color:White">
                <td width="20%" align="center" >
                    选择项目
                </td>
                <td>
                    <asp:DropDownList ID="ddlItems" runat="server" Width="154px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="background-color:White">
                <td width="20%" align="center" >
                    项目价格
                </td>
                <td>
                    <input type="text" id="txtPrice" />
                </td>
            </tr>
            <tr style="background-color:White">
                <td align="center">
                    折扣
                </td>
                <td>
                    <input type="text" id="txtAgio" onchange="CalcPrice(this,0);" />
                </td>
            </tr>
            <tr style="background-color:White">
                <td align="center">
                    折后价格
                </td>
                <td>
                    <input type="text" id="txtAgioPrice" onchange="CalcPrice(this,1);" />
                </td>
            </tr>
            <tr style="background-color:White">
                <td colspan="2" align="center">
                    <input type="button" value="保存" onclick="savedata()" />
                    <input type="button" va </td></tr></table></div><div id="fade" class="black_overlay"></div> 
    <div>
    
        </div>    
    </form>
</body>
</html>
