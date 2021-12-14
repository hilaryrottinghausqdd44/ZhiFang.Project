<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm9.aspx.cs" Inherits="OA.DBQuery.samples.WebForm9" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function showDIV() {
            var txt = window.document.getElementById("txtBox");
            var txtTop = txt.offsetTop;
            var txtLeft = txt.offsetLeft;
            var txtwidth = txt.offsetWidth;
            window.parent.testshow(txtTop, txtLeft, txtwidth);
        }
        function hidDIV() {
            window.parent.DivSetVisible(false);

        }
        function SetV(v) {
            //alert(v);
            $('txtBox').value = v;
        }
        function $(s) {
            return document.getElementById ? document.getElementById(s) : document.all[s];
        }
        function onkey() {

            //键盘事件
            var key = window.event.keyCode
            window.parent.changediv(key);
            if (key == 13) {
                event.keyCode = 9;
             }
        }
        
        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox onFocus="showDIV()" ID="txtBox" onkeydown="onkey();" runat="server"></asp:TextBox>
    </form>
</body>
</html>