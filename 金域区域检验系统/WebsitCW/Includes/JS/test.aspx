<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="OA.Includes.JS.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <script src="JsModuleDist.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onload() {

        }
        function test() {
            //alert(SplitString('新闻',','));
            ModuleDist('500,新闻系统',
                   '/oa2008/documents/NewsList.aspx?id=首页&NewsNum&moreurl=/OA/news/browse/CategoryNews.aspx',
                   '新闻编号',
                  '287',
                  'frmNews,/OA2008/news/browse/homepage.aspx,ID,{新闻编号:287};[height=500:width=600:status=yes:toolbar=no:menubar=no:location=no:scrollbars=yes:resizable=yes],/OA2008/news/browse/homepage.aspx,ID,{新闻编号:289}',
                  '',
                  '',
                   true);
        }

// ]]>
    </script>
</head>
<body onload="return window_onload()">
    <form id="form1" runat="server">
    <div>
        <input type="button" value="测试" onclick="test()" />
    </div>
    </form>
</body>
</html>
