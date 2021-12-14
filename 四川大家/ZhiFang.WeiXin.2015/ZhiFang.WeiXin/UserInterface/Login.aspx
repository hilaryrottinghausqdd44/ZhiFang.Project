<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZhiFang.WeiXin.UserInterface.Login" %>
<!DOCTYPE html>
<html >
<head runat="server">
     <meta charset="utf-8"/>
    <title>WeiXinAdmin</title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" type="text/css" href="BackStageManagementSystem/lib/bootstrap/css/bootstrap.css"/>    
    <link rel="stylesheet" type="text/css" href="BackStageManagementSystem/stylesheets/theme.css"/>
    <link rel="stylesheet" href="BackStageManagementSystem/lib/font-awesome/css/font-awesome.css"/>
    <script src="BackStageManagementSystem/lib/jquery-1.7.2.min.js" type="text/javascript"></script>
    <!-- Demo page code -->
    <style type="text/css">
        #line-chart {
            height:300px;
            width:800px;
            margin: 0px auto;
            margin-top: 1em;
        }
        .brand { font-family: georgia, serif; }
        .brand .first {
            color: #ccc;
            font-style: italic;
        }
        .brand .second {
            color: #fff;
            font-weight: bold;
        }
    </style>

    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="lib/html5.js"></script>
    <![endif]-->

    <!-- Le fav and touch icons -->
    <link rel="shortcut icon" href="../assets/ico/favicon.ico">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="../assets/ico/apple-touch-icon-57-precomposed.png">
  </head>

  <!--[if lt IE 7 ]> <body class="ie ie6"> <![endif]-->
  <!--[if IE 7 ]> <body class="ie ie7 "> <![endif]-->
  <!--[if IE 8 ]> <body class="ie ie8 "> <![endif]-->
  <!--[if IE 9 ]> <body class="ie ie9 "> <![endif]-->
  <!--[if (gt IE 9)|!(IE)]><!--> 
  <body class=""> 
  <!--<![endif]-->
     <form id="form1" runat="server">
    <div class="navbar">
        <div class="navbar-inner">
                <ul class="nav pull-right">
                    
                </ul>
                <a class="brand" href="demo.zhifang.com.cn"><span class="second">智方科技</span>　<span class="first">微信平台</span></a>
        </div>
    </div>
        <div class="row-fluid">
    <div class="dialog">
        <div class="block">
            <p class="block-heading">Sign In</p>
            <div class="block-body">
                <form>
                    <label>Username</label><asp:TextBox ID="TextBox1" class="span12" runat="server"></asp:TextBox>
                    <label>Password</label><asp:TextBox ID="TextBox2" class="span12" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Button ID="btn" runat="server" class="btn btn-primary pull-right" 
                        Text="登陆" onclick="btn_Click" />
                    <label class="remember-me">随机验证码</label>
                    <div class="clearfix"></div>
                </form>
            </div>
        </div>
    </div>
</div>
    <script src="BackStageManagementSystem/lib/bootstrap/js/bootstrap.js"></script>
    <script type="text/javascript">
        $("[rel=tooltip]").tooltip();
        $(function () {
            $('.demo-cancel-click').click(function () { return false; });
        });
    </script>
    </form>
  </body>
</html>
