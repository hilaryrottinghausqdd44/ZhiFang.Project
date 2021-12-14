<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBussiness.aspx.cs" Inherits="OA.MenuMain.AddBussiness" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>业务员增加</title>
    <link href="Css/style.css" rel="stylesheet" charset="utf-8" />    

    <link href="../JavaScriptFile/datetime/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScriptFile/datetime/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
  function GetValue()
{
   
    
//用js获取服务器控件的值时首先要得到服务器控件的ClientID
    
   alert (FindControllerID("form1","item")) ;

}

function FindControllerID(WebForm,IDString)  
{  
  for(var i = 0;i <WebForm.document.all.length;i++)  
  {  
  var idString = WebForm.document.all[i].id;  
  var MatchIDString = "(" + IDString + "){1}$";  
  if(idString.match(MatchIDString) != null)  
  {  
  return WebForm.document.all[i].id;  
  break;  
  }  
    
    
  }  
  return null;  
   
}  

    </script>

    <script type="text/javascript">
     var num = 0;
        function GetClientSearch() {
            //键盘事件
            var key = window.event.keyCode;
            if (key != 40 && key != 38 && key != 13) {
                //送检单位关键字
                var clientkey = document.getElementById('txtBox').value;
                PopupDiv.innerHTML = "";
                showimage();
                MenuMain.MenuLock.GetClientListByKey(clientkey, GetCallclientresult);
            }  
            else
            { changediv(key); }
        }
        //回调结果
        function GetCallclientresult(result) {
            var r = result.value;
            //alert(r);
            if (r != "" && r != null) {
                PopupDiv.innerHTML = r;
                showDIV();
                num = PopupDiv.childNodes.length - 1;
            }
            else
            { hidDIV(); }
            //取得查找的数据
            closeimage();
        }

        function DivSetVisible(state) {
            var DivRef = document.getElementById('PopupDiv');
            var IfrRef = document.getElementById('DivShim');
            if (state) {
                DivRef.style.display = "block";
                IfrRef.style.width = DivRef.offsetWidth;
                IfrRef.style.height = DivRef.offsetHeight;
                IfrRef.style.top = DivRef.style.top + 35;
                IfrRef.style.left = DivRef.style.left;
                IfrRef.style.zIndex = DivRef.style.zIndex - 1;
                IfrRef.style.display = "block";
            }
            else {
                DivRef.style.display = "none";
                IfrRef.style.display = "none";
            }
        }
        function DivSetVisible1(state, top, left, width) {
            //alert('DivSetVisible,top'+top+',left'+left);
            var arr = getOjbPosition();
            var left = arr[0];
            var top = arr[1];
            var DivRef = document.getElementById('PopupDiv');
            //num = DivRef.childNodes.length;
            var IfrRef = document.getElementById('DivShim');
            if (state) {
                DivRef.style.display = "block";
                DivRef.style.top = top;
                DivRef.style.left = left;
                DivRef.style.width = width - 10;
                IfrRef.style.width = DivRef.offsetWidth;
                IfrRef.style.height = DivRef.offsetHeight;
                IfrRef.style.top = DivRef.style.top;
                IfrRef.style.left = DivRef.style.left;
                IfrRef.style.zIndex = DivRef.style.zIndex - 1;
                IfrRef.style.display = "block";

            }
            else {
                DivRef.style.display = "none";
                IfrRef.style.display = "none";
            }
        }
        function SelectV(str) {
            var str1 = str.innerText;
            $('txtBox').value = str1.substring(str1.indexOf(')') + 1);
            DivSetVisible(false);
        }
        function mouseOver(e) {
            e.style.background = "#0080ff";
        }
        function mouseOut(e) {
            e.style.background = "#F2F5EF";
        }


        //
        var k = 0;
        var selecto = null;
        function changediv(key) {
            if (PopupDiv.style.display == "block") {
                //下
                if (key == 40) {
                    k++;
                    if (k >= num) {
                        k = 0;
                    }
                }
                //上
                if (key == 38) {

                    k--;
                    if (k < 0) {
                        k = num - 1;
                    }
                }
                for (var i = 0; i < num; i++) {
                    if (i == k) {
                        PopupDiv.childNodes[i].style.background = "#0080ff";
                        selecto = PopupDiv.childNodes[i];
                    }
                    else {
                        PopupDiv.childNodes[i].style.background = "";
                    }
                }
                if (key == 13) {
                    SelectV(selecto);
                }
            }
        }
        //显示出下拉div
        function showDIV() {
            var txt = window.document.getElementById("txtBox");
            var txtTop = txt.offsetTop + txt.offsetHeight;
            var txtLeft = txt.offsetLeft;
            var txtwidth = txt.offsetWidth;
            if (txt.value == "") {
                hidDIV();
                closeimage();
            }
            else {
                DivSetVisible1(true, txtTop, txtLeft, txtwidth);
            }
        }
        //关闭下拉div
        function hidDIV() {
            DivSetVisible1(false);
        }
        //用于div里记录的上下翻动
        function onkey() {
            //键盘事件
            var key = window.event.keyCode;
            changediv(key);
            if (key == 13) {
                event.keyCode = 9;
            }
        }
        function $(s) {
            return document.getElementById ? document.getElementById(s) : document.all[s];
        }
        //得到输入框坐标
        function getOjbPosition() {
            var arr = new Array();
            var obj = document.getElementById("txtBox");
            var x = obj.offsetLeft;
            var y = obj.offsetTop + obj.offsetHeight;
            while (obj.offsetParent) {
                obj = obj.offsetParent;
                x += obj.offsetLeft;
                y += obj.offsetTop;
            }
            arr[0] = x;
            arr[1] = y;
            return arr;
        }


        //显示图片
        function showimage() {

            if (document.getElementById('autocompleteAnimatecontainerId') && document.getElementById('autocompleteAnimateImageId')) {
                document.getElementById('autocompleteAnimatecontainerId').style.display = "block";
            }
            else {
                var arr = getOjbPosition();
                var left = arr[0];
                var top = arr[1];
                var obj = document.getElementById("txtBox");
                var c = document.createElement("DIV");
                c.id = "autocompleteAnimatecontainerId";
                c.style.top = top - obj.offsetHeight + 7;
                c.style.left = left + obj.offsetWidth - 15;
                c.style.zIndex = "10";
                c.style.position = "absolute";
                c.style.display = "block";
                c.style.width = "14";
                ig = document.createElement("IMG");
                ig.id = "autocompleteAnimateImageId";
                ig.src = "image/indicator.gif";
                ig.width = "14";
                ig.height = "10";
                document.body.appendChild(c);
                c.appendChild(ig);
            }
        }
        //关闭图片
        function closeimage() {
            var c = document.getElementById('autocompleteAnimatecontainerId');
            if (c) {
                c.style.display = "none";
            }
        }
    </script>

    <script language="javascript" type="text/javascript">   
        function GetCookie (name)
        {   
            var arg = name + "="; 
            var alen = arg.length; 
            var clen = document.cookie.length; 
            var i = 0; 
            while (i < clen) 
            { 
                var j = i + alen; 
                if (document.cookie.substring(i, j) == arg) 
                return getCookieVal (j); 
                i = document.cookie.indexOf(" ", i) + 1; 
                if (i == 0) break;   
            } 
            return null; 
        } 
  
        function getCookieVal (offset)
        { 
            var endstr = document.cookie.indexOf (";", offset); 
            if (endstr == -1) 
                endstr = document.cookie.length; 
            return unescape(document.cookie.substring(offset, endstr)); 
        } 
        
        function SetCookie (name, value) 
        { 
            document.cookie = name + "=" + escape (value) 
        } 
    </script>

    <style type="text/css">
        *
        {
            margin: 0 auto;
        }
        body
        {
            font-size: 12px;
            clear: both;
        }
        .style1
        {
        }
        .style2
        {
            width: 295px;
        }
        .style3
        {
            width: 117px;
        }
        .style4
        {
            width: 117px;
            height: 62px;
        }
        .style5
        {
            width: 295px;
            height: 62px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 220px; width: 500px; margin-top: 10px;">
        <div style="height: 25px; width: 502px; background-color: #3eb7fa; text-align: left;
            line-height: 20px; font-size: 14px;">
            添加业务员</div>
        <div style="width: 403px; float: left; line-height: 25px; height: 122px;">
            <table style="width: 81%;" align="center">
                <tr>
                    <td class="style3">
                        &nbsp; 业务员名称：
                    </td>
                    <td width="100px">
                        &nbsp;
                        <asp:DropDownList ID="ddbusiness" runat="server" CssClass="input_text" Height="20px"
                            Width="85px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        &nbsp; 开始时间：
                    </td>
                    <td class="style2">
                        &nbsp;
                        <asp:TextBox ID="begin" runat="server" CssClass="input_text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                            Width="89px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        &nbsp; 结束时间：
                    </td>
                    <td class="style2">
                        &nbsp;
                        <asp:TextBox ID="end" runat="server" CssClass="input_text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                            Width="87px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1" align="center" colspan="2">
                        <asp:Button ID="Button1" runat="server" CssClass="btn_blue" OnClick="Button1_Click"
                            Text="保存" Width="41px" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" CssClass="btn_blue" OnClick="Button2_Click"
                            Text="关闭" Width="43px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
