<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ZhiFang.WebLis.Main.Main" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>检验区域平台-WebLIS平台</title>
    <link href="../ui/easyui/demo/demo.css" rel="stylesheet" type="text/css" />
    <link href="../ui/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../ui/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../ui/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="../ui/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../ui/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script language="javascript" charset="UTF-8" type="text/javascript">
        /**版本号*/
        var SYS_VERSION = '1.0.0.0';
        function OpenWindowFuc(ptitle, pwidth, pheight, purl,sn) {
            $('#win').window({
                title: ptitle,
                width: pwidth,
                height: pheight,
                content: "<iframe src='" + purl + "' width='100%' height='100%' frameborder=0></iframe>",
                modal: true,
                onClose: function () {
                    if (sn != null && sn != "") {
                        if (document.getElementById(sn) != null) {
                            document.getElementById(sn).contentWindow.ContentReLoad();
                        }
                    }
                }
            }).window('open').window('center');

        };
        $(function () {
            $("#Tree").tree({
                lines: true,
                url: '../ServiceWCF/RBACService.svc/RBAC_GetModuleTreeByCookie?guid=' + generateMixed(10),
                method: 'get',
                animate: true,
                onClick: function (node) {
                    if (node.attributes) {
                        Open(node.text, node.attributes);
                    }
                }
            });
            $("#TreeExpandAll").bind('click', function () {
                $("#Tree").tree('expandAll');
            });
            $("#TreeCollapseAll").bind('click', function () {
                $("#Tree").tree('collapseAll');
            });
            $("#TreeReload").bind('click', function () {
                $("#Tree").tree('reload');
            });
        });
      
        function Open(text, attributes) {
            if ($("#tabs").tabs('exists', text)) {
                $('#tabs').tabs('select', text);
            } else {
                var att = eval("({" + attributes + "})");
                if (att.NavigateUrl != "#") {
                    //alert(String(att.Para).indexOf('IsEasyUiModule=true'));
                    //                    if (String(att.Para).indexOf('IsEasyUiModule=true')>=0) {
                    //                        $('#tabs').tabs('add', {
                    //                            title: text,
                    //                            closable: true,
                    //                            //content: "<iframe src='" + att.NavigateUrl + "' width='99.5%' height='98%' frameborder=0></iframe>"
                    //                            href: att.NavigateUrl
                    //                        });
                    //                    }
                    //                    else {
                    var url=att.NavigateUrl;
                    if (url.indexOf("?") >= 0) {
                        url += "&SN=" + att.SN;
                        if (att.Para) {
                            var para = att.Para.replace(/＆amp;/g, "&");
                            url += "&" + para;
                            url = encodeURI(encodeURI(url));
                        }
                    }
                    else {
                        url += "?SN=" + att.SN;
                        if (att.Para) {
                            var para = att.Para.replace(/＆amp;/g, "&");
                            url += "&" + para;
                            url = encodeURI(encodeURI(url));
                        }
                    }
                    //版本号
                    url += "&v="+SYS_VERSION;
                    $('#tabs').tabs('add', {
                        title: text,
                        closable: true,
                        content: "<iframe id='" + att.SN + "' src='" + url + "' width='100%' height='100%' frameborder=0></iframe>"
                    });
                    //                    }
                }
            }
        }
        var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

        function generateMixed(n) {
            var res = "";
            for (var i = 0; i < n; i++) {
                var id = Math.ceil(Math.random() * 35);
                res += chars[id];
            }
            return res;
        }

        //ganwh add 修改密码 2015-6-24
        function changePwdWin() {
            OpenWindowFuc("修改密码", Math.floor(window.screen.width * 0.22), Math.floor(window.screen.height * 0.22), '../ChangePwd.aspx', null);
        }
    </script>
</head>
<body class="easyui-layout" style="background-image: url(../Images/MainBackground.jpg)">
    <form id="form1" runat="server">
    <div data-options="region:'north',iconCls:'icon-search',noheader:true" title="标题"
        style="width: 100%; height: 80px; background-image: url(../Images/TitleBackgroud.jpg);
        background-color: #0c84b6; background-repeat: no-repeat">
        <div style="width: 230px; float: left">
            <img src="../Images/User.png" style="padding: 5px" /><asp:Label ID="Label1" runat="server"
                ForeColor="White" Font-Bold="true" Text="Label"></asp:Label>
        </div>
        
        <div style="float: right; cursor: pointer" onclick="location.href='../login.aspx';">
            <img src="../Images/Exit.png" style="padding: 5px" /></div>
       <div style="float: right; cursor: pointer;width:25px;height: 25px;"  onclick="changePwdWin();">
            <img src="../Images/icon/icon_key.gif" style="margin-top: 25px;margin-right: 25px;" />密码</div>
    </div>
    <div id="TreePanel" data-options="region:'west',split:true" title="模块树" style="width: 230px;">
        <div style="width: 100%; height: 100%;">
            <div data-options="region:'north'" style="height: 32px">
                <div style="padding: 2px; border: 0px solid #95B8E7;">
                    <a href="#" id="TreeExpandAll" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-arrow_out'">
                        展开</a> <a href="#" id="TreeCollapseAll" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-arrow_in'">
                            收缩</a> <a href="#" id="TreeReload" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-reload'">
                                刷新</a>
                </div>
            </div>
            <div data-options="region:'center'">
                <ul id="Tree" class="easyui-tree">
                </ul>
            </div>
        </div>
    </div>
    <div data-options="region:'center',noheader:true,iconCls:'icon-app-grid-16'">
        <div class="easyui-tabs" fit="true" border="false" id="tabs">
            <div title="首页">
            
                <div style="background-image: url(../Images/zfLogon.gif); background-position: center;
                    background-repeat: no-repeat; width: 100%; height: 100%;">
                </div>
            </div>
        </div>
    </div>
    <div id="win">
    </div>
    </form>
</body>
</html>
