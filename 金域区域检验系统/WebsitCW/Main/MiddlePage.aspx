<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiddlePage.aspx.cs" Inherits="OA.Main.MiddlePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function showdiv() {
            var text = '<%=Client %>';
            if (text == "") {
                hidediv();
            } else {
                document.getElementById("info").innerHTML = text;
                document.getElementById("bg").style.display = "block";
                document.getElementById("show").style.display = "block";
            }
        }
        function hidediv() {
            document.getElementById("bg").style.display = 'none';
            document.getElementById("show").style.display = 'none';
            window.parent.location.href = 'index.aspx';
        }
    </script>
    <style type="text/css">
        #bg{ display: none;  position: absolute;  top: 0%;  left: 0%;  width: 100%;  height: 100%;  background-color: black;  z-index:1001;  -moz-opacity: 0.7;  opacity:.10;  filter: alpha(opacity=70);}
        #show{display: none;  position: absolute;  top: 25%;  left: 22%;  width: 53%;  height: 49%;  padding: 8px;  border: 8px solid #E8E9F7;  background-color: white;  z-index:1002;  overflow: auto;}
    </style>

</head>
<body onload="showdiv();">
    <form id="Form1" method="post" runat="server">
     <div id="bg"></div>
    <div id="show" style="font-size:13px">
        <div id="info"></div>
        <%--<input id="btnclose" type="button" value="确认并关闭" onclick="hidediv();" />--%>
        <asp:Button ID="Button1" runat="server" Text="确认并关闭" onclick="Button1_Click"/>
    </div>
    
    </form>
</body>
</html>
