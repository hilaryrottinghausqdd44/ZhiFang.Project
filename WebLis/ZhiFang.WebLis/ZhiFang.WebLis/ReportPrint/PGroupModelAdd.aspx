<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PGroupModelAdd.aspx.cs" Inherits="ZhiFang.WebLis.ReportPrint.PGroupModelAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html >
<head runat="server">
    <title>小组模板设置添加</title>
    <link href="../Css/Default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/Tools.js"></script>
    <script language="javascript">
        var FirstTimesFlag = true; //统计次数
        var SelectRow = -1; //当按下键盘上 上下  键时选中的行编号
        var tmpc = "";
        var tmpn = "";
        var flag = true;
        function SelectClient(objstr) {

            document.getElementById('UpdatePanel1').style.position = 'absolute';
            document.getElementById('UpdatePanel1').style.index = '998';
            document.getElementById('hiddeneleid').value = objstr;
            if (!FirstTimesFlag) {
                FirstTimesFlag = true;
                return;
            }
            SelectRow = -1; //每次查询之前将选中行置为-1
            var obj = document.getElementById(objstr);
            var ttop = obj.offsetTop;
            ttop += obj.offsetHeight;
            var tleft = obj.offsetLeft;
            var hiddenLeft = document.getElementById("hiddenLeft");
            hiddenLeft.value = tleft;
            var hiddenTop = document.getElementById("hiddenTop");
            hiddenTop = ttop;
            if (Sys.ie == "8.0") {
                var o = obj.offsetParent;
                var countt = 0;
                var countl = 0;
                while (o) {
                    countt += o.offsetTop;
                    countl += o.offsetLeft;
                    o = o.offsetParent;
                }
                document.getElementById('UpdatePanel1').style.top = countt + 30;
                document.getElementById('UpdatePanel1').style.left = countl+10;
                //document.getElementById('UpdatePanel1').style.top = "121px";
                //document.getElementById('UpdatePanel1').style.left = "0px";
            }
            else {
                var o = obj.offsetParent;
                var countt = 0;
                var countl = 0;
                while (o) {
                    countt += o.offsetTop;
                    countl += o.offsetLeft;
                    o = o.offsetParent;
                }
                document.getElementById('UpdatePanel1').style.top = countt + 30;
                document.getElementById('UpdatePanel1').style.left = countl+10;
            }
            var Div_Input = document.getElementById("Div_Input");

            var hiddenUserInput = document.getElementById("hiddenUserInput");
            hiddenUserInput.value = obj.value;

            var btnSearchSJDW = document.getElementById("btnSearchSJDW");
            btnSearchSJDW.click();
        }
        var SelectRow = -1; //当按下键盘上 上下  键时选中的行编号
        function SelectNextClient(obj) {
            var Div_Input = document.getElementById("Div_Input");
            if (Div_Input.style.display != "none") {
                var tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
                var totalRow = tableUserInputSJDWList.rows.length;
                if (totalRow > 0) {
                    //event.keyCode=38   向上
                    if (event.keyCode == 38) {
                        if (SelectRow != -1) {
                            tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#a3f1f5';
                        }
                        if (SelectRow == 0 || SelectRow == -1) {
                            SelectRow = totalRow - 1;
                        }
                        else {
                            SelectRow = SelectRow - 1;
                        }
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#AAA';
                    }
                    //event.keyCode=40   向下
                    if (event.keyCode == 40) {
                        if (SelectRow != -1) {
                            tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#a3f1f5';
                        }
                        if (SelectRow == (totalRow - 1)) {
                            SelectRow = 0;
                        }
                        else {
                            SelectRow = SelectRow + 1;
                        }
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#AAA';
                    }
                    //event.keyCode=13   回车键
                    if (event.keyCode == 13) {
                        if (SelectRow != -1) {
                            tableUserInputSJDWList.rows[SelectRow].click();

                            //GetClient(tableUserInputSJDWList.rows[SelectRow][].click())

                        }
                        else {
                            if (obj.value != "") {
                                tableUserInputSJDWList.rows[0].click();
                            }
                        }
                        event.keyCode = 9;
                    }
                }
            }
        }
        function GetTmpClient(c, n) {
            tmpc = c;
            tmpn = n;
        }
        function SelectValue(o) {

            if (!FirstTimesFlag) {
            }
            else {

                if (tmpc == "" && tmpn == "") {
                    if (o.value != "") {
                        tableUserInputSJDWList.rows[0].click();
                    }
                }
                else {

                }
                flag = false;
                GetClient(tmpc, tmpn);
            }
        }
        function GetClient(id, name) {
            FirstTimesFlag = false;
            var obj = document.getElementById(document.getElementById("hiddeneleid").value);
            obj.value = name;
            tmpc = "";
            tmpn = "";

            DisplayDiv(true);
        }
        function DisplayDiv(flag) {
            if (flag) {
                var Div_Input = document.getElementById("Div_Input");
                Div_Input.style.display = "none";
                SelectRow = -1; //恢复选中行号为-1
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
    <asp:ScriptManager ID="myScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color:#0099cc;margin-top:10px">
    <tr style="background-color:#FFFFFF">
    <td align="center" colspan="2"><div style="font-size:20px; font-weight:bold; margin:10px">小组模板设置<asp:Label ID="Label1" runat="server" Text="添加"></asp:Label></div> </td>
    </tr>
    <tr style="background-color:#FFFFFF" ><td align="right" width="20%" style="padding:10px">小组:
    </td><td width="80%"  style="padding:10px">
    <asp:DropDownList ID="DDL_Section" runat="server">
        </asp:DropDownList>
        </td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">模板:</td>
    <td style="padding:10px">
    <asp:DropDownList ID="DDL_Model" runat="server">
        </asp:DropDownList></td></tr>   
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">特殊项目:</td><td style="padding:10px">
    <input style="display:none" type="text" onfocus="SelectClient('TxtBox_SpecialtyItem')" onpropertychange="SelectClient('TxtBox_SpecialtyItem')"  onkeydown="SelectNextClient(this)" onblur="SelectValue(this)" id="TxtBox_SpecialtyItem" /><asp:DropDownList ID="DDL_SpecialtyItem" runat="server">
        </asp:DropDownList></td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">优先级:</td><td style="padding:10px"><asp:TextBox ID="TxtBox_Sort" runat="server"></asp:TextBox></td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">送检单位:</td><td style="padding:10px">
        <asp:DropDownList ID="DDL_Client" runat="server">
        </asp:DropDownList></td></tr>
        <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">最小项目数:</td><td style="padding:10px">
        <asp:TextBox ID="TxtBox_Min" runat="server"></asp:TextBox></td></tr>
        <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">最大项目数:</td><td style="padding:10px">
        <asp:TextBox ID="TxtBox_Max" runat="server"></asp:TextBox></td></tr>
    <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">使用标志:</td><td style="padding:10px">
        <asp:DropDownList ID="DDL_UserFlag" runat="server">
        <asp:ListItem Selected=True Value="0">否</asp:ListItem>
        <asp:ListItem Value="1">是</asp:ListItem>
        </asp:DropDownList>
        </td></tr>
        <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">是否带图:</td><td style="padding:10px">
        <asp:DropDownList ID="DDL_ImageFlag" runat="server">
        <asp:ListItem Selected=True Value="0">否</asp:ListItem>
        <asp:ListItem Value="1">是</asp:ListItem>
        </asp:DropDownList>
        </td></tr>
        <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">是否有抗生素:</td><td style="padding:10px">
        <asp:DropDownList ID="DDL_AntiFlag" runat="server">
        <asp:ListItem Selected=True Value="0">否</asp:ListItem>
        <asp:ListItem Value="1">是</asp:ListItem>
        </asp:DropDownList>
        </td></tr>
        <tr style="background-color:#FFFFFF"><td align="right" style="padding:10px">客户类型:</td><td style="padding:10px">
        <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="0">医院</asp:ListItem>
        <asp:ListItem Value="1">中心</asp:ListItem>
        </asp:DropDownList>
        </td></tr>
             <tr><td colspan="2" align="center" valign="top" style="background-color:#FFFFFF;padding:10px">
                 <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" />&nbsp;&nbsp;<input type="button" value="关闭" onclick="window.close();" /></td></tr>
    </table>
     <div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <div id="Div_Input" style="position: absolute; z-index: 999; display: none; font-size:12px" runat="server">
                <table id="tableUserInputSJDWList" border="1" cellpadding="0" cellspacing="0" runat="server" width="150">
                </table>
                <iframe style="position:absolute;width:100%;height:100%;_filter:alpha(opacity=0);opacity=0;border-style:none; display:none"></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
        <ContentTemplate>
    <asp:Button ID="btnSearchSJDW" runat="server" OnClick="btnSearchSJDW_Click" style="display:none"  />
    <!-- 用户选中的送检单位编号 -->
            <input type="hidden" id="hiddenSelectedClient" runat="server" />
            <!-- 查询到的送检单位数量,可以省略 -->
            <input type="hidden" id="hiddenTestCount" runat="server" />
            <!-- 用户在 送检单位 文本框中输入的内容 -->
            <input type="hidden" id="hiddenUserInput" runat="server" />
            <!-- 对照控件的横坐标 -->
            <input type="hidden" id="hiddenLeft" runat="server" />
            <!-- 对照控件的纵坐标 -->
            <input type="hidden" id="hiddenTop" runat="server" />
            <!-- 用户选中的文本框的ID -->
            <input type="hidden" id="hiddeneleid" runat="server" />
            <!-- tmpBarCode -->
            <input type="hidden" id="hiddentmpBarCode" runat="server" />
            </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>