<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseEmp_Single.aspx.cs" Inherits="ZhiFang.ProjectProgressMonitorManage.WeiXin.webapp.ui.Common.ChooseEmp_Single" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width,initial-scale=1, minimum-scale=1.0" />

    <meta name="description" content="" />
    <meta name="author" content="" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-control" content="no-cache" />
    <meta http-equiv="Cache" content="no-cache" />

    <title>选择员工</title>

    <link rel="stylesheet" type="text/css" href="js/style.css" />

    <script src="../src/jquery.min.js"></script>
    <script src="../util/util.js" type="text/javascript" charset="utf-8"></script>
    <script src="../js/component.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/ChooseEmp_Single.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.charfirst.pinyin.js"></script>
    <script type="text/javascript" src="js/sort.js"></script>
    <script>
        function setopenervalue() {
            parent.document.getElementById("todaycontentmemo").value = '123';
            //if (opener)
            //{
            //    alert('123123123');
            //}
            //opener.document.getElementById("todaycontentmemo").value = '123';
            //history.back();
        }
        function setliheight() {
            var LiHeight = 12;
            //alert(LiHeight);
            Initials.find('li').height(LiHeight);

        }
        function setcheck(domid1) {
            alert(domid1 + 'aaa' + domid2);
            $("#" + domid1).css("display", "none");
            $("#" + domid2).css("display", "block");
        }
    </script>
</head>
<body >
    <%=aaa %>
</body>
</html>
