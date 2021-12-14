<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ZhiFang.WeiXin.UserInterface.BackStageManagementSystem.Main" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <title>Main</title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="stylesheets/theme.css" />
    <link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../easyui/jquery.min.js" type="text/javascript"></script>
    <script src="../easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {

        })
        function AddModule(modulename, modulecode, url) {
            if ($("#tabs").tabs('exists', modulename)) {
                $('#tabs').tabs('select', modulename);
            } else {
                if (url != "#") {
                    $('#tabs').tabs('add', {
                        title: modulename,
                        closable: true,
                        content: "<iframe id='" + modulecode + "' src='" + url + "' width='100%' height='100%' frameborder=0></iframe>"
                    });
                }
            }
        }
    </script>
    <!-- Demo page code -->
    <style type="text/css">
        #line-chart
        {
            height: 300px;
            width: 800px;
            margin: 0px auto;
            margin-top: 1em;
        }
        .brand
        {
            font-family: georgia, serif;
        }
        .brand .first
        {
            color: #ccc;
            font-style: italic;
        }
        .brand .second
        {
            color: #fff;
            font-weight: bold;
        }
    </style>
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="lib/html5.js"></script>
    <![endif]-->
    <!-- Le fav and touch icons -->
</head>
<!--[if lt IE 7 ]> <body class="ie ie6"> <![endif]-->
<!--[if IE 7 ]> <body class="ie ie7 "> <![endif]-->
<!--[if IE 8 ]> <body class="ie ie8 "> <![endif]-->
<!--[if IE 9 ]> <body class="ie ie9 "> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<body class="">
    <!--<![endif]-->
    <div class="navbar">
        <div class="navbar-inner">
            <ul class="nav pull-right">
                <li id="fat-menu" class="dropdown"><a href="#" role="button" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user"></i>管理员 <i class="icon-caret-down"></i>
                </a>
                    <ul class="dropdown-menu">
                        <li><a tabindex="-1" href="#">我的账户</a></li>
                        <li class="divider"></li>
                        <li><a tabindex="-1" class="visible-phone" href="#">设置</a></li>
                        <li class="divider visible-phone">电话</li>
                        <li><a tabindex="-1" href="sign-in.html">退出</a></li>
                    </ul>
                </li>
            </ul>
            <a class="brand" href="demo.zhifang.com.cn"><span class="second">智方科技</span> <span
                class="first">微信平台</span></a>
        </div>
    </div>
    <div class="sidebar-nav">
        <a href="#dashboard-menu" class="nav-header" data-toggle="collapse"><i class="icon-dashboard">
        </i>账户维护</a>
        <ul id="dashboard-menu" class="nav nav-list collapse in">
            <li><a href="#" onclick="AddModule('微信用户','WeiXinAccount','../ui/WeiXinAccount/B_WeiXinAccount.html')">微信用户</a></li>
            <li><a href="#" onclick="AddModule('微信用户组','WeiXinUserGroup','../ui/dictionary/WeiXinUserGroup/WeiXinUserGroup.html')">
                微信用户组</a></li>
            <li><a href="#" onclick="AddModule('应用系统账户类型','B_AccountType','../ui/dictionary/B_AccountType/B_AccountType.html')">应用系统账户类型</a></li>
            <li><a href="#" onclick="AddModule('应用系统账户类型','B_AccountType','../ui/dictionary/B_AccountType/B_AccountType.html')">应查询子账户</a></li>            
        </ul>
        <a href="#accounts-menu" class="nav-header" data-toggle="collapse"><i class="icon-briefcase">
        </i>业务字典<%--<span class="label label-info">+3</span>--%></a>
        <ul id="accounts-menu" class="nav nav-list collapse">
           <li><a href="#" onclick="AddModule('医院字典','B_Hospital','../ui/dictionary/B_Hospital/B_Hospital.html')">医院字典</a></li>
            <li><a href="#" onclick="AddModule('医院分类','B_HospitalType','../ui/dictionary/B_HospitalType/B_HospitalType.html')">医院分类</a></li>
            <li><a href="#" onclick="AddModule('医院级别','B_HospitalLevel','../ui/dictionary/B_HospitaLevel/B_HospitaLevel.html')">医院级别</a></li>
            <li><a href="#" onclick="AddModule('国家','B_Country','../ui/dictionary/B_Country/B_Country.html')">国家</a></li>
            <li><a href="#" onclick="AddModule('省份','B_Province','../ui/dictionary/B_Province/B_Province.html')">省份</a></li>
            <li><a href="#" onclick="AddModule('城市','B_City','../ui/dictionary/B_City/B_City.html')">城市</a></li>
            <li><a href="#" onclick="AddModule('性别','B_Sex','../ui/dictionary/Sex/Sex.html')">性别</a></li>
            <li><a href="#" onclick="AddModule('专业','B_Specialty','../ui/dictionary/Specialty/Specialty.html')">专业</a></li>
            <li><a href="#" onclick="AddModule('头像图标','B_Icons','../ui/dictionary/Icons/Icons.html')">头像图标</a></li>
            <li><a href="#" onclick="AddModule('头像图标类型','B_IconsType','../ui/dictionary/IconsType/IconsType.html')">头像图标类型</a></li>
        </ul>        
        <a href="#" class="nav-header"><i class="icon-question-sign"></i>Help</a>
        <a href="#" class="nav-header"><i class="icon-comment"></i>Faq</a>
    </div>
    <div class="content">
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
    <script src="lib/bootstrap/js/bootstrap.js"></script>
    <script type="text/javascript">
        $("[rel=tooltip]").tooltip();
        $(function () {
            $('.demo-cancel-click').click(function () { return false; });
        });
    </script>
</body>
</html>
