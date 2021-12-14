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
    <script src="../ui/util/util.js" type="text/javascript"></script>
    <script src="../ui/util/base64.js" type="text/javascript"></script>
    <script language="javascript" charset="UTF-8" type="text/javascript">
        /**版本号*/
        var SYS_VERSION = '1.0.0.0';
        var phost = "";
        function OpenWindowFuc(ptitle, pwidth, pheight, purl, sn) {
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

        function OpenWindowFucCallback(ptitle, pwidth, pheight, purl, sn, func) {
            $('#win').window({
                title: ptitle,
                width: pwidth,
                height: pheight,
                content: "<iframe src='" + purl + "' width='100%' height='100%' frameborder=0></iframe>",
                modal: true,
                onClose: function () {
                    func();
                }
            }).window('open').window('center');

        };


        function CloseWindowFuc() {
            $('#win').window('close');
        }

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
            //获取是否存在未读新闻
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: '../ServiceWCF/NewsService.svc/GetHasReadNewsFlag?guid=' + generateMixed(10),
                success: function (data) {
                    if (data.success == true) {
                        if (Number(data.ResultDataValue) == 0) {
                            var aa = $("#newsimage")[0].src;
                            $("#newsimage").attr('src', aa.replace("News1.png", "News.png"));
                        }
                        else {
                            var aa = $("#newsimage")[0].src;
                            $("#newsimage").attr('src', aa.replace("News.png", "News1.png"));
                        }
                    } else {
                        //$.messager.alert('提示', '插入数据失败！失败信息：' + data.msg);
                    }
                }
            });
            ShowMsgList(false);
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
                    var url = att.NavigateUrl;
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
                    url += "&v=" + SYS_VERSION;
                    $('#tabs').tabs('add', {
                        title: text,
                        closable: true,
                        content: "<iframe id='" + att.SN + "' src='" + url + "' width='100%' height='100%' frameborder=0></iframe>"
                    });
                    //                    }

                    //新闻列表
                    if (text == "新闻列表") {
                        var aa = $("#newsimage")[0].src;
                        $("#newsimage").attr('src', aa.replace("News1.png", "News.png"));
                    }
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

        function changePwdWin() {
            OpenWindowFuc("修改密码", Math.floor(window.screen.width * 0.22), Math.floor(window.screen.height * 0.22), '../ChangePwd.aspx', null);
        }
        function ShowNews() {
            var roots = $("#Tree").tree('getRoots');
            for (var i = 0; i < roots.length; i++) {
                var tmpnodes = roots[i].children;
                if (tmpnodes && tmpnodes.length && tmpnodes.length > 0) {
                    for (var j = 0; j < tmpnodes.length; j++) {
                        if (tmpnodes[j].attributes && tmpnodes[j].text == "新闻列表") {
                            Open(tmpnodes[j].text, tmpnodes[j].attributes);
                        }
                    }
                }
            }
            var aa = $("#newsimage")[0].src;
            //alert(aa.replace("News1.png", "News.png"));
            //$("#newsimage").attr("src", "../Images/News1.png");
            $("#newsimage").attr('src', aa.replace("News1.png", "News.png"));
        }
        function ShowMsgList(flag) {
            $.ajax({
                type: 'get',
                contentType: 'application/json',
                url: '../ServiceWCF/NewsService.svc/GetZhiFangLIIPUrl?guid=' + generateMixed(10),
                success: function (data) {
                    if (data.success) {
                        var account = Shell.util.Cookie.getCookie("ZhiFangUser");
                        var pwd = Shell.util.Cookie.getCookie("ZhiFangPwd");
                        //var b = new Base64();
                        //pwd = b.decode(pwd);
                        var phost = data.ResultDataValue;
                        if (flag) {
                            var purl = phost + '/ui/layui/views/msg/search/listByLabCode.html?account=' + account + '&pwd=' + pwd + '&AutoCloseFlag=1&guid=' + generateMixed(10);
                            $('#win').window({
                                title: '消息列表申请单',
                                width: Math.floor(window.screen.width * 0.9),
                                height: Math.floor(window.screen.height * 0.7),
                                content: "<iframe src='" + purl + "' width='100%' height='100%' frameborder=0></iframe>",
                                modal: true,
                                collapsible: false,
                                minimizable: false,
                                maximizable: false,
                                closable: true

                            }).window('open').window('center');
                        }
                        else {
                            $.ajax({
                                type: 'get',
                                contentType: 'application/json',
                                url: '../ServiceWCF/NewsService.svc/GetZhiFangLIIPMsg?flag=false&guid=' + generateMixed(10),
                                success: function (data) {
                                    if (data.success == true) {
                                        if (data.ResultDataValue == "{\"result\":\"true\"}") {
                                            var purl = phost + '/ui/layui/views/msg/search/listByLabCode.html?account=' + account + '&pwd=' + pwd + '&AutoCloseFlag=1&guid=' + generateMixed(10);
                                            $('#win').window({
                                                title: '消息列表申请单',
                                                width: Math.floor(window.screen.width * 0.9),
                                                height: Math.floor(window.screen.height * 0.7),
                                                content: "<iframe src='" + purl + "' width='100%' height='100%' frameborder=0></iframe>",
                                                modal: true,
                                                collapsible: false,
                                                minimizable: false,
                                                maximizable: false,
                                                closable: false

                                            }).window('open').window('center');
                                        }
                                    } else {
                                        //$.messager.alert('提示', '插入数据失败！失败信息：' + data.msg);
                                    }
                                }
                            });
                        }
                    }
                    else {
                        $("#Messageslabel").css("display", "none");
                        $("#Messagesimage").css("display", "none");
                    }
                }
            });           
        }
    </script>
</head>
<body class="easyui-layout" style="background-image: url(../Images/MainBackground.jpg)">
    <form id="form1" runat="server">
        <div data-options="region:'north',iconCls:'icon-search',noheader:true" title="标题"
            style="width: 100%; height: 80px; background-image: url(../Images/TitleBackgroud.png); background-color: #0f51b4; background-repeat: no-repeat">
            <div style="width: 100px; float: left; margin-left: 10px; margin-top: 10px;">
                <img src="../Images/User.png" style="margin-left: 10px; margin-top: 5px;" />
            </div>
            <div style="float: left; margin-left: 10px; margin-top: 30px;">
                <asp:Label ID="Label1" runat="server"
                    ForeColor="White" Font-Bold="true" Text="Label" Font-Size="Larger"></asp:Label>
            </div>

            <div style="float: right; margin-right: 30px; margin-top: 30px; cursor: pointer; color: white; font-weight: bold;" onclick="location.href='../login.aspx';">退出登录</div>
            <div style="float: right; margin-right: 0px; margin-top: 10px; cursor: pointer" onclick="location.href='../login.aspx';">
                <img src="../Images/Exit.png" style="margin-top: 5px;" />
            </div>
            <div style="float: right; margin-right: 30px; margin-top: 30px; cursor: pointer; color: white; font-weight: bold;" onclick="changePwdWin();">密码管理</div>
            <div style="float: right; margin-right: 0px; margin-top: 10px; cursor: pointer;" onclick="changePwdWin();">
                <img id="Passwordimage" src="../Images/Password.png" />
            </div>
            <div style="float: right; margin-right: 30px; margin-top: 30px; cursor: pointer; color: white; font-weight: bold;" onclick="ShowNews();">新闻通知</div>
            <div style="float: right; margin-right: 0px; margin-top: 10px; cursor: pointer;" onclick="ShowNews();">
                <img id="newsimage" src="../Images/News.png" />
            </div>
            <div id="Messageslabel" style="float: right; margin-right: 30px; margin-top: 30px; cursor: pointer; color: white; font-weight: bold;" onclick="ShowMsgList(true);">消息</div>
            <div style="float: right; margin-right: 0px; margin-top: 10px; cursor: pointer;" onclick="ShowMsgList(true);">
                <img id="Messagesimage" src="../Images/Messages.png" />
            </div>
        </div>
        <div id="TreePanel" data-options="region:'west',split:true" title="模块树" style="width: 230px;">
            <div style="width: 100%; height: 100%;">
                <div data-options="region:'north'" style="height: 32px">
                    <div style="padding: 2px; border: 0px solid #95B8E7;">
                        <a href="#" id="TreeExpandAll" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-arrow_out'">展开</a> <a href="#" id="TreeCollapseAll" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-arrow_in'">收缩</a> <a href="#" id="TreeReload" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-reload'">刷新</a>
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

                    <div style="background-image: url(../Images/zfLogon.gif); background-position: center; background-repeat: no-repeat; width: 100%; height: 100%;">
                    </div>
                </div>
            </div>
        </div>
        <div id="win">
        </div>
    </form>
</body>
</html>
