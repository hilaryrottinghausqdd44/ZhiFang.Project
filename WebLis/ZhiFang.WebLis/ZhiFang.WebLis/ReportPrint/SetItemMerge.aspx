<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetItemMerge.aspx.cs" Inherits="ZhiFang.WebLis.ReportPrint.SetItemMerge" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合并项目配置界面</title>
    <script type="text/javascript" language="javascript">
        function Selected(l1, l2, Flag) {
            var cl = document.getElementById("Clientlist");
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
        function Search() {
            var o = document.getElementById("DropDownList2");
            var LB1 = document.getElementById("ListBox1");
            var LB2 = document.getElementById("ListBox2");
            var cl = document.getElementById("Clientlist");
            cl.value = "";
            if (o.value != "") {
                var v = ZhiFang.WebLis.ReportPrint.SetItemMerge.SearchClient(o.value);
                if (v != null) {
                    LB1.options.length = 0;
                    LB2.options.length = 0
                    var va = String(v.value).split('@');
                    if (va[0].toString() != "") {
                        var selectList = va[0].toString().split(';');
                        for (var i = 0; i < selectList.length; i++) {
                            if (selectList[i] != "") {
                                var selected = selectList[i].split(',')
                                var opt = document.createElement("option");
                                opt.value = selected[0];
                                opt.text = selected[1];
                                LB1.options.add(opt);
                            }
                        }
                    }
                    if (va[1]!="") {
                        var unSelectList = va[1].toString().split(';');
                        for (var j = 0; j < unSelectList.length; j++) {
                            if (unSelectList[j] != "") {
                                var unSelected = unSelectList[j].split(',')
                                var opt = document.createElement("option");
                                opt.value = unSelected[0];
                                opt.text = unSelected[1];
                                cl.value += opt.value + ",";
                                LB2.options.add(opt);
                            }
                        }
                    }
                    alert(cl.value);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <input id='Clientlist' type="hidden" value="" runat="server" />
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color:#0099cc">
    <tr style="background-color:#FFFFFF">
    <td align="center" colspan="3" >客户单位：<asp:DropDownList ID="DropDownList1" runat="server">
       </asp:DropDownList></td>
    </tr>
    <tr style="background-color:#FFFFFF">
    <td align="center" colspan="3" >小组名称:<asp:DropDownList ID="DropDownList2" runat="server">
       </asp:DropDownList>&nbsp; <input type="button" value="查询" onclick="Search();" /></td>
    </tr>
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
            Text="保存" onclick="Button1_Click"/></td></tr>
    </table>
    <div>
    
    </div>
    </form>
</body>
</html>
