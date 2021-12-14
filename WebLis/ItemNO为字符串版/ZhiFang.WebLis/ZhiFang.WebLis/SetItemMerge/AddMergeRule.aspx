<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMergeRule.aspx.cs" Inherits="ZhiFang.WebLis.SetItemMerge.AddMergeRule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function CheckUrl() {
            var a = opener.location.href;
            var b = String(a);
            if (b.indexOf(LogisticsClientManager.aspx) < 0) {
                document.write("非法进入！请从正确的路径进入界面！");
            }
        }
        function Selected(l1, l2, Flag) {
            var cl = document.getElementById("Itemlist");
            var LB1 = l1;
            var LB2 = l2;
            if (LB1.options.length == 0) {
                alert("没有项可添加！");
                return;
            }
            if (Flag != "All") {
                if (LB1.selectedIndex < 0) {
                    alert("请选择添加项！");
                    return;
                }
                var text = LB1.options[LB1.selectedIndex].text;
                var value = LB1.options[LB1.selectedIndex].value;
                var opt = document.createElement("option");
                opt.value = value;
                opt.text = text;
                LB2.options.add(opt);
                var tmpwidth = LB1.style.pixelWidth;
                LB1.removeChild(LB1.options[LB1.selectedIndex]);
                LB1.style.width = tmpwidth + "px";
                if (LB1.id == "ListBox1") {
                    cl.value += value + ",";
                }
                else {
                    cl.value = String(cl.value).replace((value + ","), "");
                }
            }
            else {
                var tmpc = "";
                for (var i = 0; i < LB1.options.length; i++) {
                    var opt = document.createElement("option");
                    opt.value = LB1.options[i].value;
                    opt.text = LB1.options[i].text;
                    LB2.options.add(opt);
                    tmpc += opt.value + ",";
                }
                if (LB1.id == "ListBox1") {
                    cl.value += tmpc;
                }
                else {
                    cl.value = "";
                }
                LB1.options.length = 0;
            }
        }  
    </script>
</head>
<body onload="CheckUrl();" style="width:100%; padding:0px; margin:0px;">
    <form id="form1" runat="server">
    <input id='Itemlist' type="hidden" value="" runat="server" />
   <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color:#0099cc">
    <tr style="background-color:#FFFFFF">
    <td align="center" colspan="3" >合并规则名称：<asp:TextBox ID="txtItemMergeCName" runat="server"></asp:TextBox></td>
    </tr>
    <tr style="background-color:#FFFFFF">
        <td align="center"  colspan="3">小组分类:<asp:DropDownList ID="dropSectionType" 
                runat="server" onselectedindexchanged="dropSectionType_SelectedIndexChanged" 
               AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  项目名称：<asp:TextBox ID="txtItemCName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp; 
            <asp:Button ID="Button2" runat="server" 
            Text="查询" onclick="Button2_Click"/></td>
    </tr>
    <tr style="background-color:#FFFFFF;font:20px;font-weight:bold"><td align="center">未选</td><td></td><td align="center">已选</td></tr>
    <tr style="background-color:#FFFFFF">
    <td width="45%" align="center"><asp:ListBox ID="ListBox1" runat="server" Width="100%" Height="350"></asp:ListBox></td>
    <td align="center">
    <input type="button" value=">>" onclick="Selected(document.getElementById('ListBox1'),document.getElementById('ListBox2'),'All')" style="width:35px" /><br /><br />
    <input type="button" value=">" onclick="Selected(document.getElementById('ListBox1'),document.getElementById('ListBox2'),'One')" style="width:35px" /><br /><br />
    <input type="button" value="<" onclick="Selected(document.getElementById('ListBox2'),document.getElementById('ListBox1'),'One')" style="width:35px" /><br /><br />
    <input type="button" value="<<" onclick="Selected(document.getElementById('ListBox2'),document.getElementById('ListBox1'),'All')" style="width:35px" />
    </td>
    <td width="45%" align="center"><asp:ListBox ID="ListBox2" runat="server" Width="100%" Height="350"></asp:ListBox></td></tr>
    <tr><td colspan="3" align="center">
        <asp:Button ID="Button1" runat="server" 
            Text="保存" onclick="Button1_Click"/>&nbsp;&nbsp;<input type="button" value="关闭" onclick="window.close();" /></td></tr>
          
    </table>
    </form>
</body>
</html>
