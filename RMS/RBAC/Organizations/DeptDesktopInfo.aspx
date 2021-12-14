<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptDesktopInfo.aspx.cs" Inherits="OA.RBAC.Organizations.DeptDesktopInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">


        function window_onload() {
            if (document.getElementById('hpara').value != "")
                runFrame(document.getElementById('hpara').value);
        }
        function showUserDialog(tag) {
            strurl = '../../SystemModules/mdOneNews.aspx';
            listurl = '../../News/Browse/homepage.aspx'; //要链接的页面
            listname = '首页';
            moreurl = '../../SystemModules/SelectNewsPage.aspx';

            //strurl = strurl + "?moreurl=" + escape(moreurl);
            
            var r = window.showModalDialog(strurl, this, 'dialogWidth:750px;dialogHeight:600px;');
            //alert(r);
             if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                var id = r.substring(0, r.indexOf(','));
                var name = r.substring(r.indexOf(',') + 1);
                document.getElementById('txt_para').value = name;
                document.getElementById('hpara').value = id;
            }
            
        }

        function runFrame(id) {
            document.frames['frmPreview'].location.href = '../../News/Browse/homepage.aspx?id=' + id;
        }
    </script>
    <script language="javascript" for="hpara" event="onpropertychange">
        if (this.value != "")
                runFrame(this.value);
    </script>
</head>
<body onload="return window_onload()">
    <form id="form1" runat="server">
    <div>
    
        ID <input id="hpara" runat="server" type="text" />
        桌面名<asp:textbox id="txt_para" runat="server" Width="175px"></asp:textbox><INPUT type="button" value="浏览" onclick="showUserDialog('a')">
        <asp:Button ID="ButtonSave" runat="server" onclick="ButtonSave_Click" 
            Text="保存" />
&nbsp;<iframe id="frmPreview" src="" width="100%" height="450"
    </div></form>
</body>
</html>
