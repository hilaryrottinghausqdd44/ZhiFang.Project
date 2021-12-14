<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSTAT.aspx.cs" Inherits="OA.eSTAT.ShowSTAT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>统计图设置主页面</title>

    <script type="text/javascript">
        
        //设置左边的页面URL
        function setLeftURL(urlLeft)
        {   
            //alert(urlLeft);
            window.document.getElementById("Left").src = urlLeft;
        }   


    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="taable1" width="100%" style="height: 540px">
        <tr>
            <td style="width: 30%; height: 100%">
                <iframe id="Left" src="" style="height: 100%; width: 100%" scrolling="auto"></iframe>
            </td>
            <td style="width: 70%; height: 100%">
                <iframe id="RightTop" src="" style="height: 100%; width: 100%" scrolling="auto"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
