<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Register TagPrefix="uc1" TagName="top1" Src="top1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="top2" Src="top2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bot" Src="Bot.ascx" %>
<%@ Register TagPrefix="uc1" TagName="image1" Src="image1.ascx" %>

<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="false" Inherits="ahccl.Labmain._default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>安徽省临床检验中心</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript">

        var divStr = '<div id="ad" style="position:absolute; display:none;">';
        divStr += '<table border="0" cellpadding="0" cellspacing="0" id="adTable" width="30" height="30">'
        divStr += '<tr>';
        divStr += '<td>';
        divStr += '<a id="adLink" href="#" target="_blank"></a>';
        divStr += '</td>';
        divStr += '</tr>';
        divStr += '</table>';
        divStr += '</div>';
        document.writeln(divStr);
        var x = 50,y = 60 //浮动层的初始位置，分别对应层的初始X坐标和Y坐标 
        var xin = true, yin = true //判断层的X坐标和Y坐标是否在在控制范围之内，xin为真是层向右移动，否则向左；yin为真是层向下移动，否则向上 
        var step = 1 //层移动的步长，值越大移动速度越快 
        var delay = 25 //层移动的时间间隔,单位为毫秒，值越小移动速度越快 
        var objAD=document.getElementById("ad") //捕获id为ad的层作为漂浮目标
        var adTable  = document.getElementById("adTable");
				
        function showAD(){
            var HiddenDSP = document.getElementById("HiddenDSP");
            if(HiddenDSP.value == "0")
            {
                return;
            }
            objAD.style.display = "";

					
            //宽度
            var hiddenW = document.getElementById("hiddenW");
            adTable.style.width = hiddenW.value;

            //宽度
            var hiddenH = document.getElementById("hiddenH");
            adTable.style.height = hiddenH.value;
					
            var hiddenBG = document.getElementById("hiddenBG");
            adTable.style.backgroundColor = hiddenBG.value;

            //速度
            var hiddenV = document.getElementById("hiddenV");
            delay = hiddenV.value;

            //显示的字符串
            var hiddenS = document.getElementById("hiddenS");
            var adLink = document.getElementById("adLink");
            adLink.innerHTML = hiddenS.value;


            var hiddenU = document.getElementById("hiddenU");
            var adLink = document.getElementById("adLink");
            adLink.href = hiddenU.value;

        }

        function floatAD() 
        {
            var L=T=0 //层移动范围的左边界(L)和上边界(T)坐标 
            var R= document.body.clientWidth-objAD.offsetWidth //层移动的右边界 
            var B = document.body.clientHeight-objAD.offsetHeight //层移动的下边界 
            objAD.style.left = x + document.body.scrollLeft //更新层的X坐标，实现X轴方向上的运动；document.body.scrollLeft为文档区域的滚动
            条向右拉的距离，
            以保证在滚动条右拉时层仍在可见范围内 
            objAD.style.top = y + document.body.scrollTop //更新层的Y坐标，实现Y轴方向上的运动；document.body.scrollTop为文档区域的滚动条
            向下拉的距离，以保
            证在滚动条下拉时层仍在可见范围内 
            x = x + step*(xin?1:-1) //通过判断层的范围决定层在X轴上的运动方向 
            if (x < L) { xin = true; x = L} //层超出左边界时的处理 
            if (x > R){ xin = false; x = R} //层超出右边界时的处理 
            y = y + step*(yin?1:-1) //通过判断层的范围决定层在Y轴上的运动方向 
            if (y < T) { yin = true; y = T } //层超出上边界时的处理 
            if (y > B) { yin = false; y = B } //层超出下边界时的处理
        }
        var itl= setInterval("floatAD()", delay) //每delay秒执行一次floatAD函数 
        objAD.onmouseover=function(){clearInterval(itl)} //层在鼠标移上时清除上面的间隔事件，实现层在的鼠标移上时停止运动的效果 
        objAD.onmouseout=function()
        {
            var hiddenV = document.getElementById("hiddenV");
            delay = hiddenV.value;
            itl=setInterval("floatAD()", delay)
        } //层在鼠标移开时开始间隔事件，实现层在的鼠标移开时继续运动的效果 

    </script>
    <style type="text/css">
        .STYLE1 {
            FONT-SIZE: 12px;
            COLOR: #666666;
            FONT-FAMILY: "宋体";
        }

        A:link {
            COLOR: #666666;
            TEXT-DECORATION: none;
        }

        A:visited {
            COLOR: #666666;
            TEXT-DECORATION: none;
        }

        A:hover {
            COLOR: #ff0000;
            TEXT-DECORATION: none;
        }

        A:active {
            COLOR: #666666;
            TEXT-DECORATION: none;
        }

        .STYLE9 {
            FONT-SIZE: 12px;
            FONT-FAMILY: "宋体";
        }

        .STYLE5 {
            FONT-FAMILY: "宋体";
        }

        .STYLE22 {
            FONT-SIZE: 12px;
            COLOR: #333333;
            FONT-FAMILY: "宋体";
        }

        .blank {
            BORDER-RIGHT: #2a4988 1px dashed;
            BORDER-TOP: #2a4988 1px dashed;
            BORDER-LEFT: #2a4988 1px dashed;
            BORDER-BOTTOM: #2a4988 1px dashed;
        }

        img {
            border-width: 0px 0px 0px 0px;
        }

        moreMeeting {
            cursor: pointer;
        }

            moreMeeting:active {
                border: 1;
            }
    </style>
    <script language="javascript" type="text/javascript" src="flash.js"> </script>
    <script language="javascript">
        function showMeetings(id)
        {
            var url = "http://localhost/labs/ReturnReceipt/WebReturnReceipt.aspx?id="+id;
            window.open(url);//,"width=600;height=500;"toolbar:no; ScrollBar=auto
        }
        function showMoreMeeting()
        {
            var url = "http://www.ahccl.org/labMain/ad/MeetingList.aspx";
            window.showModalDialog(url,"scrollbar:no");
        }
        function checked1()	
        {
				
				
            if(window.document.all["textUserid"].value=="")
            {
                alert("请输入用户名！")
                window.document.all["textUserid"].focus();
                return false;
            }
            else
            {
                if(window.document.all["textPassword"].value=="")
                {
                    alert("请输入密码！")
                    window.document.all["textPassword"].focus();
                }
                else
                {
                    Form1.submit();
                }
            }
				
        }
        function sss()	
        {	
						
            if(document.getElementById("txtUserName").value=="")
            {
                alert("请输入用户名！");
                return false;
            }
            else
            {
                if(document.getElementById("txtPassword").value=="")
                {
                    alert("请输入密码！")
                }
                else
                {
                    form3.submit();
                }
            }
        } 
        function WinOpen(id)
        {
            window.open('IQCEQASystemMaintenance/eachnews.aspx?
id='+id,'','width=680px,height=620px,scrollbars=yes,resizable=yes,left=' + 
(screen.availWidth-620)/2 + ',top=0' );	
        }
        var oPopup = window.createPopup();
        function CPop(strContent)
        {
            var oPopBody = oPopup.document.body;
            oPopBody.style.backgroundColor = "lightyellow";
            oPopBody.style.border = "solid black 1px";
				
            oPopBody.innerHTML="";         //先清空内容
            var ss = strContent.split("|");
            for(i=0;i<ss.length-1;i++)
            {
                //alert(ss[i]);
                var filename=ss[i].split("/");
                oPopBody.innerHTML += "<a href=\"#\" onclick=\"parent.location.href=
\'IQCEQASystemMaintenance/downloadattach.aspx?file="+ss
                [i]+"\'\">"+filename[2]+"</a><br>";
            }
				
            oPopup.show(window.event.x-80, window.event.y, 180, 180, document.body);
				
				
        }
        function   keyEnter(){   
					
			    
            if   (event.keyCode   ==   13)   
            {   
                //alert('dd');
                //document.all
                event.keyCode   =   9;   
            }       
        } 
        function keyEnter1()
        {
            if   (event.keyCode   ==   13)   
            {   
                checked1();
            } 
        }    
			
        function SubmitMsg()
        {
            return true;
            if(
            document.getElementById("txtUserNameID").value.indexOf('js')>=0 
            || document.getElementById("txtUserNameID").value.indexOf('JS')>=0
            || document.getElementById("txtUserNameID").value==''
            || document.getElementById("txtUserNameID").value.indexOf(' ')>=0
            || document.getElementById("txtUserNameID").value.indexOf(' ')>=0
            || document.getElementById("txtPasswordID").value==''
            || document.getElementById("txtPasswordID").value.indexOf(' ')>=0
				
            )
            {
                alert("实验室质控系统正在升级,停止使用\n请于本月23号-27号再登录本系统进行质控数据上报\n\n2007-04-16 到2007-04
-20 检验测定
\n2007-04-23 到2007-04-27进行质评数据上报");
                return false;
            }
            else
                return true;
        }  
    </script>
</head>
<body onload="showAD();">
    <form id="form1" name="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="900" align="center" border="0">
            <tr>
                <td align="center">
                    <uc1:top2 id="Top2" runat="server"></uc1:top2>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <uc1:top1 id="Top11" runat="server"></uc1:top1></td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="900" border="0">
                        <tr>
                            <td width="200">
                                <table height="145" cellspacing="0" cellpadding="0" align="center" bgcolor="#f4f4f4" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <img src="indeximages/news-head.jpg" width="200" height="38" />
                                            </td>
                                        </tr>
                                        <tr valign="top" align="middle">
                                            <td align="center" valign="top">
                                                <style type="text/css">
                                                    IMG {
                                                        BORDER-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        BORDER-LEFT: 0px;
                                                        BORDER- BOTTOM: 0px;
                                                    }

                                                    DIV {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    FORM {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    IMG {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    UL {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    OL {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    LI {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    DL {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    DT {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    DD {
                                                        BORDER-RIGHT: 0px;
                                                        PADDING-RIGHT: 0px;
                                                        BORDER-TOP: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        BORDER-LEFT: 0px;
                                                        PADDING-TOP: 0px;
                                                        BORDER-BOTTOM: 0px;
                                                    }

                                                    H1 {
                                                        PADDING-RIGHT: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        PADDING-TOP: 0px;
                                                    }

                                                    H2 {
                                                        PADDING-RIGHT: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        PADDING-TOP: 0px;
                                                    }

                                                    H3 {
                                                        PADDING-RIGHT: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        PADDING-TOP: 0px;
                                                    }

                                                    H4 {
                                                        PADDING-RIGHT: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        PADDING-TOP: 0px;
                                                    }

                                                    H5 {
                                                        PADDING-RIGHT: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        PADDING-TOP: 0px;
                                                    }

                                                    H6 {
                                                        PADDING-RIGHT: 0px;
                                                        PADDING-LEFT: 0px;
                                                        PADDING-BOTTOM: 0px;
                                                        MARGIN: 0px;
                                                        PADDING-TOP: 0px;
                                                    }

                                                    TABLE {
                                                        FONT-SIZE: 12px;
                                                    }

                                                    TD {
                                                        FONT-SIZE: 12px;
                                                    }

                                                    TR {
                                                        FONT-SIZE: 12px;
                                                    }

                                                    TH {
                                                        FONT-SIZE: 12px;
                                                    }

                                                    A:visited {
                                                        COLOR: #666666;
                                                        TEXT-DECORATION: none;
                                                    }

                                                    A:hover {
                                                        COLOR: #666666;
                                                        TEXT-DECORATION: underline;
                                                    }

                                                    A:active {
                                                    }

                                                    .focusPic {
                                                        MARGIN: 0px auto;
                                                        WIDTH: 190px;
                                                        border: #e9e5e5 solid 1px;
                                                    }

                                                        .focusPic .pic {
                                                            PADDING-RIGHT: 0px;
                                                            PADDING-LEFT: 0px;
                                                            PADDING-BOTTOM: 0px;
                                                            MARGIN: 0px auto;
                                                            WIDTH: 190px;
                                                            PADDING-TOP: 2px;
                                                            HEIGHT: 130px;
                                                        }

                                                        .focusPic .adPic {
                                                            MARGIN: 0px auto 5px;
                                                            OVERFLOW: hidden;
                                                            WIDTH: 190px;
                                                            HEIGHT: 10px;
                                                        }

                                                        .focusPic .Unknown {
                                                            BACKGROUND: url(images-news/adpic.gif);
                                                        }

                                                        .focusPic .adPic .text {
                                                            PADDING-RIGHT: 4px;
                                                            PADDING-LEFT: 0px;
                                                            FLOAT: right;
                                                            PADDING- BOTTOM: 0px;
                                                            WIDTH: 140px;
                                                            PADDING-TOP: 9px;
                                                        }

                                                            .focusPic .adPic .text A {
                                                                COLOR: #1f3a87;
                                                            }

                                                                .focusPic .adPic .text A:hover {
                                                                    COLOR: #bc2931;
                                                                }

                                                        .focusPic H2 {
                                                            PADDING-RIGHT: 0px;
                                                            PADDING-LEFT: 12px;
                                                            FONT-SIZE: 14px;
                                                            FLOAT: left;
                                                            PADDING- BOTTOM: 3px;
                                                            WIDTH: 190px;
                                                            PADDING-TOP: 4px;
                                                            TEXT-ALIGN: left;
                                                        }

                                                        .focusPic P {
                                                            PADDING-RIGHT: 0px;
                                                            PADDING-LEFT: 12px;
                                                            FLOAT: left;
                                                            PADDING- BOTTOM: 10px;
                                                            MARGIN: 0px;
                                                            WIDTH: 190px;
                                                            LINE-HEIGHT: 160%;
                                                            PADDING-TOP: 0px;
                                                            TEXT-ALIGN: left;
                                                        }

                                                            .focusPic P IMG {
                                                                MARGIN: 0px 0px 2px;
                                                            }

                                                        .focusPic .more {
                                                            MARGIN: 0px auto;
                                                            WIDTH: 190px;
                                                        }

                                                            .focusPic .more .textNum {
                                                                PADDING-RIGHT: 0px;
                                                                PADDING-LEFT: 0px;
                                                                FLOAT: right;
                                                                PADDING- BOTTOM: 4px;
                                                                MARGIN: 0px 8px 0px 0px;
                                                                PADDING-TOP: 0px;
                                                            }

                                                                .focusPic .more .textNum .text {
                                                                    PADDING-RIGHT: 6px;
                                                                    PADDING-LEFT: 0px;
                                                                    FONT-WEIGHT: bold;
                                                                    FLOAT: left;
                                                                    PADDING-BOTTOM: 0px;
                                                                    COLOR: #666;
                                                                    PADDING-TOP: 7px;
                                                                }

                                                                .focusPic .more .textNum .num {
                                                                    FLOAT: left;
                                                                    WIDTH: 113px;
                                                                    HEIGHT: 19px;
                                                                }

                                                                .focusPic .more .textNum .bg1 {
                                                                    BACKGROUND: url(images-news/num1.gif);
                                                                }

                                                                .focusPic .more .textNum .bg2 {
                                                                    BACKGROUND: url(images-news/num2.gif);
                                                                }

                                                                .focusPic .more .textNum .bg3 {
                                                                    BACKGROUND: url(images-news/num3.gif);
                                                                }

                                                                .focusPic .more .textNum .bg4 {
                                                                    BACKGROUND: url(images-news/num4.gif);
                                                                }

                                                                .focusPic .more .textNum .num UL {
                                                                    FLOAT: left;
                                                                    WIDTH: 113px;
                                                                }

                                                                .focusPic .more .textNum .num LI {
                                                                    PADDING-RIGHT: 0px;
                                                                    DISPLAY: block;
                                                                    PADDING-LEFT: 0px;
                                                                    FONT- WEIGHT: bold;
                                                                    FLOAT: left;
                                                                    PADDING-BOTTOM: 0px;
                                                                    WIDTH: 28px;
                                                                    COLOR: #fff;
                                                                    PADDING-TOP: 6px;
                                                                    LIST-STYLE-TYPE: none;
                                                                }

                                                                    .focusPic .more .textNum .num LI A {
                                                                        PADDING-RIGHT: 5px;
                                                                        PADDING-LEFT: 5px;
                                                                        PADDING-BOTTOM: 0px;
                                                                        COLOR: #fff;
                                                                        PADDING-TOP: 0px;
                                                                    }

                                                                        .focusPic .more .textNum .num LI A:visited {
                                                                            COLOR: #fff;
                                                                        }

                                                                        .focusPic .more .textNum .num LI A:hover {
                                                                            COLOR: #ff0;
                                                                        }
                                                </style>
                                                <script language="JavaScript" type="text/javascript">
                                                    var nn;
                                                    nn=1;
                                                    setTimeout('change_img()',2000);
                                                    function change_img()
                                                    {
                                                        if(nn>4) nn=1
                                                        setTimeout('setFocus1('+nn+')',2000);
                                                        nn++;
                                                        tt=setTimeout('change_img()',2000);
                                                    }
                                                    function setFocus1(i)
                                                    {
                                                        selectLayer1(i);
                                                    }
                                                    function selectLayer1(i)
                                                    {
                                                        switch(i)
                                                        {
                                                            case 1:
                                                                document.getElementById("focusPic1").style.display="block";
                                                                document.getElementById("focusPic2").style.display="none";
                                                                document.getElementById("focusPic3").style.display="none";
                                                                document.getElementById("focusPic4").style.display="none";
                                                                document.getElementById("focusPic1nav").style.display="block";
                                                                document.getElementById("focusPic2nav").style.display="none";
                                                                document.getElementById("focusPic3nav").style.display="none";
                                                                document.getElementById("focusPic4nav").style.display="none";
                                                                break;
                                                            case 2:
                                                                document.getElementById("focusPic1").style.display="none";
                                                                document.getElementById("focusPic2").style.display="block";
                                                                document.getElementById("focusPic3").style.display="none";
                                                                document.getElementById("focusPic4").style.display="none";
                                                                document.getElementById("focusPic1nav").style.display="none";
                                                                document.getElementById("focusPic2nav").style.display="block";
                                                                document.getElementById("focusPic3nav").style.display="none";
                                                                document.getElementById("focusPic4nav").style.display="none";
                                                                break;
                                                            case 3:
                                                                document.getElementById("focusPic1").style.display="none";
                                                                document.getElementById("focusPic2").style.display="none";
                                                                document.getElementById("focusPic3").style.display="block";
                                                                document.getElementById("focusPic4").style.display="none";
                                                                document.getElementById("focusPic1nav").style.display="none";
                                                                document.getElementById("focusPic2nav").style.display="none";
                                                                document.getElementById("focusPic3nav").style.display="block";
                                                                document.getElementById("focusPic4nav").style.display="none";
                                                                break;
                                                            case 4:
                                                                document.getElementById("focusPic1").style.display="none";
                                                                document.getElementById("focusPic2").style.display="none";
                                                                document.getElementById("focusPic3").style.display="none";
                                                                document.getElementById("focusPic4").style.display="block";
                                                                document.getElementById("focusPic1nav").style.display="none";
                                                                document.getElementById("focusPic2nav").style.display="none";
                                                                document.getElementById("focusPic3nav").style.display="none";
                                                                document.getElementById("focusPic4nav").style.display="block";
                                                                break;
                                                        }
                                                    }
                                                </script>
                                                <div class="focusPic">
                                                    <div id="focusPic1" style="display: block">
                                                        <div class="pic">
                                                            <table id="image1_DataList1" style="border-collapse: collapse"
                                                                cellspacing="0"
                                                                cellpadding="2" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <img
                                                                                id="image1_DataList1__ctl0_Image1" style="width: 190px; height: 130px" alt="" src="images-news/5.jpg" border="0">
                                                                            <div align="center"
                                                                                style="height: 13px">
                                                                                <a
                                                                                    title="十会-2"
                                                                                    href="#">十会-2</a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div id="focusPic2" style="display: none">
                                                        <div class="pic">
                                                            <table id="image1_Datalist2" style="border-
collapse: collapse"
                                                                cellspacing="0" cellpadding="2" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <img
                                                                                id="image1_Datalist2__ctl0_Image2" style="width: 190px; height: 130px" alt="" src="images-news/1.jpg" border="0">
                                                                            <div
                                                                                align="center">
                                                                                <a
                                                                                    title="十会-1"
                                                                                    href="#">十会-1</a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div id="focusPic3" style="display: none">
                                                        <div class="pic">
                                                            <table id="image1_DataList3" style="border-
collapse: collapse"
                                                                cellspacing="0" cellpadding="2" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <img
                                                                                id="image1_DataList3__ctl0_Image3" style="width: 190px; height: 130px" alt="" src="images-news/3.jpg" border="0">
                                                                            <div
                                                                                align="center">
                                                                                <a
                                                                                    title="2008年质控工作"
                                                                                    暨质控网络应用学习班 href="#">2008年质控工作暨质控网络应</a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div id="focusPic4" style="display: none">
                                                        <div class="pic">
                                                            <table id="image1_DataList4" style="border-
collapse: collapse"
                                                                cellspacing="0" cellpadding="2" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <img
                                                                                id="image1_DataList4__ctl0_Image4" style="width: 190px; height: 130px" alt="" src="images-news/4.jpg" border="0">
                                                                            <div
                                                                                align="center">
                                                                                <a
                                                                                    title="2008年总结会"
                                                                                    href="#">十一会总结1</a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="more">
                                                        <div class="textNum">
                                                            <div class="text"></div>
                                                            <div class="num bg1" id="focusPic1nav"
                                                                style="display: block">
                                                                <ul>
                                                                    <li><a
                                                                        href="javascript:setFocus1(1);"
                                                                        target="_self">1</a> </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(2);"
                                                                        target="_self">2</a> </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(3);"
                                                                        target="_self">3</a> </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(4);"
                                                                        target="_self">4</a> </li>
                                                                </ul>
                                                            </div>
                                                            <div class="num bg2" id="focusPic2nav"
                                                                style="display: none">
                                                                <ul>
                                                                    <li><a
                                                                        href="javascript:setFocus1(1);"
                                                                        target="_self">1</a> </li>
                                                                    <li>2 </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(3);"
                                                                        target="_self">3</a> </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(4);"
                                                                        target="_self">4</a> </li>
                                                                </ul>
                                                            </div>
                                                            <div class="num bg3" id="focusPic3nav"
                                                                style="display: none">
                                                                <ul>
                                                                    <li><a
                                                                        href="javascript:setFocus1(1);"
                                                                        target="_self">1</a> </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(2);"
                                                                        target="_self">2</a> </li>
                                                                    <li>3 </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(4);"
                                                                        target="_self">4</a> </li>
                                                                </ul>
                                                            </div>
                                                            <div class="num bg4" id="focusPic4nav"
                                                                style="display: none">
                                                                <ul>
                                                                    <li><a
                                                                        href="javascript:setFocus1(1);"
                                                                        target="_self">1</a> </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(2);"
                                                                        target="_self">2</a> </li>
                                                                    <li><a
                                                                        href="javascript:setFocus1(3);"
                                                                        target="_self">3</a> </li>
                                                                    <li>4 </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td valign="top" width="700">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" width="65%">
                                            <table cellspacing="0" cellpadding="0" width="98%" align="center"
                                                border="0">
                                                <tr>
                                                    <td background="indeximages/xinwen.jpg" height="27">
                                                        <div align="right"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%"
                                                            border="0">
                                                            <tr>
                                                                <td colspan="3" height="10">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg"
                                                                    colspan="3"
                                                                    height="7"></td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td colspan="3" height="5"></td>
                                                            </tr>
                                                            <% 
                                                                for (int i = 0; i < 8; i++)
                                                                {
                                                                    if (dtCentorNews != null && dtCentorNews.Rows.Count > i)
                                                                    //for(int i=0;i<dtCentorNews.Rows.Count;i++)
                                                                    {
                                                                        string id = dtCentorNews.Rows[i]["id"].ToString();												
                                                            %>
                                                            <tr valign="top">
                                                                <td width="25">
                                                                    <div align="center">
                                                                        <img height="10" src="indeximages/news-biao.jpg" width="12" />
                                                                    </div>
                                                                </td>
                                                                <td width="350">
                                                                    <span class="STYLE9">
                                                                        <a href="EachNews.aspx?id=<%=id%>" title="<%=dtCentorNews.Rows[i]["title"].ToString()%>" target="_blank"><%=CString(dtCentorNews.Rows[i]["title"].ToString(),28)%></a>
                                                                    </span>
                                                                </td>
                                                                <td width="60">
                                                                    <div align="center">
                                                                        <span class="STYLE9">
                                                                            <%=Convert.ToDateTime(dtCentorNews.Rows[i]["buildtime"]).ToString("yyyy-MM-dd").Trim()%>
                                                                        </span>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%
                                                                }
                                              else
                                              {
                                                            %>
                                                            <tr height="10">
                                                                <td width="25">
                                                                    <div align="center">
                                                                        <img height="10"
                                                                            src="indeximages/news-biao.jpg" width="12" />
                                                                    </div>
                                                                </td>
                                                                <td><span class="STYLE9">&nbsp;</span></td>
                                                                <td><span class="STYLE9">&nbsp;</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg"
                                                                    colspan="3"
                                                                    height="7"></td>
                                                            </tr>
                                                            <%
                                                }
                                              }
                                                            %>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="35%" align="right" valign="top">
                                            <table width="98%" border="0" bgcolor="#fbfbfb" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="13" height="13">
                                                        <img src="indeximages/1-lefttop.gif" width="13" height="13" /></td>
                                                    <td background="indeximages/x-bg.gif"></td>
                                                    <td width="17">
                                                        <img src="indeximages/2-righttop.gif" width="13" height="13" /></td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="2" background="indeximages/y-bg.gif"></td>
                                                    <td height="25" align="center" bgcolor="#fbfbfb">
                                                        <img src="indeximages/zuixinhuiyi.gif" width="210"
                                                            height="25"></td>
                                                    <td rowspan="2" background="indeximages/y-bg-.gif"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" bgcolor="fbfbfb">
                                                        <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                                            <tr>
                                                                <td colspan="3" height="10" align="right">
                                                                    <img id="more" class="moreMeeting" src="indeximages/bokedluntan-more.gif"
                                                                        alt="更多" onclick="showMoreMeeting();" style="cursor: hand" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <% 
                                                                DataSet ds = new DataSet();
                                                                SqlDataAdapter command = new SqlDataAdapter("SELECT DISTINCT  Meeting.Id as id, Meeting.MeetingTitle as MeetingTitle,Meeting.MeetingContent as MeetingContent, Meeting.MeetingEndTime as MeetingEndTime FROM MeetingCD RIGHT OUTER JOIN Meeting ON MeetingCD.MeetingId = Meeting.Id WHERE (MeetingCD.CD = '0000') AND (Meeting.Flag = 1) order by id desc", new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"].ToString().Trim()));
                                                                command.Fill(ds, "ds");
                                                                DataTable dtMeeting1 = ds.Tables[0];
                                                                if (dtMeeting1 == null)
                                                                {
                                                                    return;
                                                                }
                                                                for (int i = 0; i < 8; i++)
                                                                {
                                                                    if (dtMeeting1 != null && dtMeeting1.Rows.Count > i)
                                                                    {
                                                                        string id = dtMeeting1.Rows[i]["id"].ToString();
                                                                        DateTime endtime;//会议结束时间
                                                                        try
                                                                        {
                                                                            endtime = Convert.ToDateTime(dtMeeting1.Rows[i]["MeetingEndTime"]);
                                                                        }
                                                                        catch
                                                                        {
                                                                            continue;
                                                                        }
                                                                        DateTime dateTimeValue;
                                                                        dateTimeValue = DateTime.Now;//当前时间																
                                                            %>
                                                            <tr height="12">
                                                                <td width="10">
                                                                    <div align="center">
                                                                        <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" /></div>
                                                                </td>
                                                                <td><span class="STYLE9"><a href="#" onclick="window.open('../labmain/ad/detailMeetingInfo.aspx?id=<%=dtMeeting1.Rows[i]["id"].ToString()%>');" title="<%=dtMeeting1.Rows[i]["MeetingTitle"].ToString()%>"><%=CString(dtMeeting1.Rows[i]["MeetingTitle"].ToString(),14)%> </a></span>

                                                                </td>
                                                            </tr>
                                                            <!--td align="right">
                                                  <%if (endtime > dateTimeValue)
                                                    {%>
                                                <a href="#"onClick="showMeetings(<%=dtMeeting1.Rows[i]["Id"].ToString()%>)">
                                                  <u>参加</u>
                                                  </a>
												<%}%>																		
                                                  <span class="STYLE9"><%=Convert.ToDateTime(dtMeeting1.Rows[i]["MeetingEndTime"]).ToString("MM-dd").Trim()%></span>
											 </td-->
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%}
                                                                else
                                                                {%>
                                                            <tr height="10">
                                                                <td width="10">
                                                                    <div align="center">
                                                                        <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" /></div>
                                                                </td>
                                                                <td><span class="STYLE9">&nbsp;</span></td>
                                                                <td><span class="STYLE9">&nbsp;</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%}
                                                            }%>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img src="indeximages/3-leftbottom.gif" width="13" height="13" /></td>
                                                    <td background="indeximages/x-bg-.gif"></td>
                                                    <td>
                                                        <img src="indeximages/4-rightbottom.gif" width="13" height="13" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="5" colspan="2"></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <table cellspacing="0" cellpadding="0" width="180" align="center" border="0">
                                    <tr>
                                        <td background="indeximages/dd2.jpg" height="139">
                                            <form id="form2" name="form2" method="post" onsubmit="return SubmitMsg();" action="http://www.AHCCL.ORG/AHLabs/labmain/lablogin.aspx">
                                                <table height="116" cellspacing="0" cellpadding="0" width="81%" align="center" border="0">
                                                    <tr>
                                                        <td width="37%">&nbsp;</td>
                                                        <td width="63%" height="30">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="STYLE9" height="29">
                                                            <div align="right">用户名：</div>
                                                        </td>
                                                        <td height="28" align="center">
                                                            <input name="txtUserName" id="txtUserNameID" type="text" style="width: 70px; height: 20px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="33">
                                                            <div align="right"><span class="STYLE9">密&nbsp;&nbsp;码：</span></div>
                                                        </td>
                                                        <td height="33" align="center">
                                                            <input name="txtPassword" id="txtPasswordID" type="password" style="width: 70px; height: 20px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" colspan="2">
                                                            <table width="100%" height="24" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td height="22" valign="bottom">
                                                                        <div align="center">
                                                                            <a href="#">
                                                                                <input type="submit" name="Submit" value="登陆" /></a>
                                                                        </div>
                                                                    </td>
                                                                    <td height="20" valign="bottom">
                                                                        <div align="center"><a href="#">
                                                                            <input type="reset" name="Submit2" value="重置" /></a></div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </form>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="51" align="center">
                                            <img src="indeximages/syzn.jpg" style="cursor: pointer;" onclick="window.open('http://www.ahccl.org/labMain/helpfile.aspx?id=1');" width="176" height="45"></td>
                                    </tr>
                                    <tr>
                                        <td height="51" align="center">
                                            <img src="indeximages/zpxt.jpg" style="cursor: pointer;" onclick="window.open('http://www.ahccl.org/labMain/helpfile.aspx?id=2');" width="176" height="45"></td>
                                    </tr>
                                    <tr>
                                        <td height="5"></td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="65%" align="left" valign="top">
                                            <table cellspacing="0" cellpadding="0" width="98%" align="left" border="0">
                                                <tr>
                                                    <td background="indeximages/tongzhi11.jpg" height="27">
                                                        <div align="right"><a href="centerInfo.aspx?id=通知公告" target="_blank">
                                                            <img height="5" src="indeximages/bokedluntan-more.gif" width="38"></a></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td colspan="3" height="10">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <% 
                                                                //try
                                                                //{ 
                                                                DataSet dsdtInform = new DataSet();
                                                                SqlDataAdapter commanddtInform = new SqlDataAdapter("SELECT top 100 id,title,buildtime,Catagory FROM OA_Information WHERE Catagory='通知公告' ORDER BY id DESC", new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"].ToString().Trim()));
                                                                commanddtInform.Fill(dsdtInform, "ds");
                                                                DataTable dtInformtzgg = dsdtInform.Tables[0];
                                                                //Response.Write(dtInformtzgg.Rows.Count + "@@@" + dtInformtzgg.Rows[dtInformtzgg.Rows.Count - 1][0].ToString() + "@@@" + dtInformtzgg.Rows[dtInformtzgg.Rows.Count - 1][1].ToString());
                                                                //}
                                                                //catch(Exception e)
                                                                //{
                                                                //}
                                                                for (int i = 0; i < 9; i++)
                                                                {
                                                                    if (dtInformtzgg != null && dtInformtzgg.Rows.Count > i)
                                                                    {
                                                                        string id = dtInformtzgg.Rows[i]["id"].ToString();
                                                                        string buildtime = "";
                                                                        try
                                                                        {
                                                                            buildtime = Convert.ToDateTime(dtInformtzgg.Rows[i]["buildtime"]).ToShortDateString();
                                                                        }
                                                                        catch
                                                                        {
                                                                        }
                                                                        DateTime dateTimeValue, dateTimeValue2;
                                                                        dateTimeValue2 = DateTime.Now;
                                                                        dateTimeValue = Convert.ToDateTime(dtInformtzgg.Rows[i]["buildtime"]);
                                                                        System.TimeSpan subtractTime = (dateTimeValue2 - dateTimeValue);
                                                                        int days = Convert.ToInt32(subtractTime.TotalDays);
                                                            %>
                                                            <tr>
                                                                <td width="10">
                                                                    <div align="center">
                                                                        <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" /></div>

                                                                </td>
                                                                <td nowrap width="340" height="12">
                                                                    <span class="STYLE9">
                                                                        <%if (days < 7)
                                                                          {%>
                                                                        <a href="EachNews.aspx?id=<%=id%>" title='<%=dtInformtzgg.Rows[i]["title"].ToString()%>' target="_blank">
                                                                            <%=CString(dtInformtzgg.Rows[i]["title"].ToString(),26)%>
                                                                        </a>
                                                                        <img src="images/new.gif" width="28" height="11">
                                                                        <%}
                                                                          else
                                                                          {%>
                                                                        <a href="EachNews.aspx?id=<%=id%>" title='<%=dtInformtzgg.Rows[i]["title"].ToString()%>' target="_blank">
                                                                            <%=CString(dtInformtzgg.Rows[i]["title"].ToString(),26)%>
                                                                        </a>
                                                                        <%}%>
                                                                    </span>
                                                                </td>
                                                                <td width="60">
                                                                    <div align="center">
                                                                        <span class="STYLE9">
                                                                            <%=Convert.ToDateTime(dtInformtzgg.Rows[i]["buildtime"]).ToString("yyyy-MM-dd").Trim()%>
                                                                        </span>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%}else{%>
                                                            <tr height="10">
                                                                <td width="10">
                                                                    <div align="center">
                                                                        <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" /></div>
                                                                </td>
                                                                <td><span class="STYLE9">&nbsp;</span></td>
                                                                <td><span class="STYLE9">&nbsp;</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%}
                        }%>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="35%" align="right" valign="top">
                                            <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                                <tr>
                                                    <td background="indeximages/wenjian.jpg" height="27">
                                                        <div align="right">
                                                            <a href="centerInfo.aspx?id=最新更新" target="_blank">
                                                                <img height="5"
                                                                    src="indeximages/bokedluntan-more.gif"
                                                                    width="38"></a>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td colspan="3" height="10">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%
                                                                for (int i = 0; i < 9; i++)
                                                                {
                                                                    if (dtUpdate != null && dtUpdate.Rows.Count > i)
                                                                    {
                                                                        string id = dtUpdate.Rows[i]["id"].ToString();
                                                                        string buildtime = "";
                                                                        try
                                                                        {
                                                                            buildtime = Convert.ToDateTime(dtUpdate.Rows[i]["buildtime"]).ToShortDateString();
                                                                        }
                                                                        catch
                                                                        { }
                                                                        DateTime dateTimeValue, dateTimeValue2;
                                                                        dateTimeValue2 = DateTime.Now;
                                                                        dateTimeValue = Convert.ToDateTime(dtUpdate.Rows[i]["buildtime"]);
                                                                        System.TimeSpan subtractTime = (dateTimeValue2 - dateTimeValue);
                                                                        int days = Convert.ToInt32(subtractTime.TotalDays);
                                                            %>
                                                            <tr>
                                                                <td width="10">
                                                                    <div align="center">
                                                                        <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" /></div>
                                                                </td>
                                                                <td height="12" colspan="2">
                                                                    <span class="STYLE9">
                                                                        <%if (days < 7)
                                                                          {%>
                                                                        <a href="EachNews.aspx?id=<%=id%>" title='<%=dtUpdate.Rows[i]["title"].ToString()%>' target="_blank">
                                                                            <%=CString(dtUpdate.Rows[i]["title"].ToString(),14)%></a>
                                                                        <img src="images/new.gif" width="28" height="11"><%}
                                                                          else
                                                                          {%>
                                                                        <a href="EachNews.aspx?id=<%=id%>" title='<%=dtUpdate.Rows[i]["title"].ToString()%>' target="_blank">
                                                                            <%=CString(dtUpdate.Rows[i]["title"].ToString(),18)%>
                                                                        </a>
                                                                        <%}%>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%}
                                    else
                                    {%>
                                                            <tr height="10">
                                                                <td width="10">
                                                                    <div align="center">
                                                                        <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" /></div>
                                                                </td>
                                                                <td width="180"><span class="STYLE9">&nbsp;</span></td>
                                                                <td width="60"><span class="STYLE9">&nbsp;</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                            </tr>
                                                            <%}
                                }%>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <table cellSpacing="0" cellPadding="0" width="200" border="0">
						<tr>
							<td><IMG height="38" src="indeximages/news-head1.jpg" width="200"></td>
						</tr>
						<tr>
							<td><IMG height="2" src="indeximages/news-body1.jpg" width="200"></td>
						</tr>
						<tr>
							<td background="indeximages/news-body3.jpg">
								<script>
								    function TJPzoomswitch(obj) {
								        TJPon[obj]=((TJPon[obj])?(0):(1));
								        return TJPon[obj];
								    }
								    function TJPzoomif(obj,highres) {
								        if(TJPon[obj]) 
								        {TJPzoom(obj,highres);}
								    }
								    function TJPzoom(obj,highres) {
																		
								        TJPzoomratio=TJPzoomheight/TJPzoomwidth;
								        if(TJPzoomoffsetx > 1) 
								        {
								            TJPzoomoffset='dumb';
																		
								            TJPzoomoffsetx=TJPzoomoffsetx/TJPzoomwidth;
																		
								            TJPzoomoffsety=TJPzoomoffsety/TJPzoomheight;
								        }
								        if(!obj.style.width) {
								            if(obj.width > 0) {
								                //educated guess
																		
								                obj.style.width=obj.width+'px';
																		
								                obj.style.height=obj.height+'px';
								            }
								        }
								        if(typeof(highres) != typeof('')) 
								        {highres=obj.src}
								        var TJPstage=document.createElement("div");
																		
								        TJPstage.style.width=obj.style.width;
																		
								        TJPstage.style.height=obj.style.height;
																		
								        TJPstage.style.overflow='hidden';
																		
								        TJPstage.style.position='absolute';
								        if(typeof(TJPstage.style.filter) != typeof(nosuchthing)) 
								        {
								            //hi IE
								            if(navigator.appVersion.indexOf('Mac') == -1) 
								            { //hi Mac IE
																		
								                TJPstage.style.filter='alpha(opacity=0)';
																		
								                TJPstage.style.backgroundColor='#ffffff';
								            }
								        } else {
								            //hi decent gentlemen
																		
								            TJPstage.style.backgroundImage='transparent';
								        }
								        TJPstage.setAttribute('onmousemove','TJPhandlemouse(event,this);');
								        TJPstage.setAttribute('onmousedown','TJPhandlemouse(event,this);');
								        TJPstage.setAttribute('onmouseup','TJPhandlemouse(event,this);');
								        TJPstage.setAttribute('onmouseout','TJPhandlemouse(event,this);');
								        if(navigator.userAgent.indexOf('MSIE')>-1) 
								        {
								            TJPstage.onmousemove =function() {TJPhandlemouse(event,this);}
								            TJPstage.onmousedown = function() {TJPhandlemouse(event,this);}
								            TJPstage.onmouseup = function() {TJPhandlemouse(event,this);}
								            TJPstage.onmouseout = function() {TJPhandlemouse(event,this);}
								        }
																		
								        obj.parentNode.insertBefore(TJPstage,obj);																		 
																		
								        TJPwin=document.createElement("div");
																		
								        TJPwin.style.width='0px';
																		
								        TJPwin.style.height='0px';
																		
								        TJPwin.style.overflow='hidden';
																		
								        TJPwin.style.position='absolute';
																		
								        TJPwin.style.display='none';
								        tw1='<div style="position:absolute;overflow:hidden;margin:';
								        TJPwin.innerHTML= tw1+TJPshadowthick+'px  0 0 '+TJPshadowthick+'px; background-color:'+TJPbordercolor+'; width:'+(TJPzoomwidth-TJPshadowthick*2)+'px;height:'+(TJPzoomheight-TJPshadowthick*2)+'px"></div>' + tw1+(TJPshadowthick+TJPborderthick)+'px 0 0 '+(TJPshadowthick+TJPborderthick)+'px; width:'+(TJPzoomwidth-TJPshadowthick*2-TJPborderthick*2)+'px;height:'+(TJPzoomheight-TJPshadowthick*2-TJPborderthick*2)+'px;"><img src="'+obj.src+'" style="position:absolute;margin:0;padding:0;border:0; width:'+(TJPzoomamount*parseInt(obj.style.width))+'px;height:'+(TJPzoomamount*parseInt(obj.style.height))+'px;" />'+((obj.src!=highres)?('<img src="'+highres+'" style="position:absolute;margin:0;padding:0;border:0; width:'+(TJPzoomamount*parseInt(obj.style.width))+'px;height:'+(TJPzoomamount*parseInt(obj.style.height))+'px;" onload="if(this.parentNode) {this.parentNode.parentNode.getElementsByTagName(\'div\')[2].style.display=\'none\';}" />'):(''))+'</div>';
								        if(highres != obj.src) 
								        {
								            TJPwin.innerHTML+='<div style="position:absolute; margin:'+(TJPshadowthick+TJPborderthick)+'px 0 0 '+(TJPshadowthick+TJPborderthick)+'px;">'+TJPloading+'</div>';
								        }
								        if(TJPshadowthick>0) {
								            st1='<span  style="position:absolute; display:inline-block; margin: ';
																		
								            st2='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=\'scale\',src=';
								            st3='filter:alpha(opacity=0);margin:0;padding:0;border:0;"/></span>';
								            TJPwin.innerHTML+=st1+'0 0 0 0;width:'+TJPshadowthick*2+'px; height:'+TJPshadowthick*2+'px;'+st2+'\''+TJPshadow+'nw.png\')"><img src="'+TJPshadow+'nw.png" style="width:'+TJPshadowthick*2+'px;height:'+TJPshadowthick*2+'px;'+st3 +st1+'0 0 0 '+(TJPzoomwidth-TJPshadowthick*2)+'px; width:'+TJPshadowthick*2+'px; height:'+TJPshadowthick*2+'px;'+st2+'\''+TJPshadow+'ne.png\')"><img src="'+TJPshadow+'ne.png" style="width:'+TJPshadowthick*2+'px; height:'+TJPshadowthick*2+'px;'+st3 + st1+''+(TJPzoomheight-TJPshadowthick*2)+'px 0 0 0px; width:'+TJPshadowthick*2+'px; height:'+TJPshadowthick*2+'px;'+st2+'\''+TJPshadow+'sw.png\',sizingMethod=\'scale\')"><img src="'+TJPshadow+'sw.png" style="width:'+TJPshadowthick*2+'px; height:'+TJPshadowthick*2+'px;'+st3 + st1+''+(TJPzoomheight-TJPshadowthick*2)+'px 0 0 '+(TJPzoomwidth-TJPshadowthick*2)+'px; width:'+TJPshadowthick*2+'px; height:'+TJPshadowthick*2+'px;'+st2+'\''+TJPshadow+'se.png\',sizingMethod=\'scale\')"><img src="'+TJPshadow+'se.png" style="width:'+TJPshadowthick*2+'px; height:'+TJPshadowthick*2+'px;'+st3 +st1+'0 0 0 '+(TJPshadowthick*2)+'px; width:'+(TJPzoomwidth-TJPshadowthick*4)+'px; height:'+TJPshadowthick*2+'px;'+st2+'\''+TJPshadow+'n.png\',sizingMethod=\'scale\')"><img src="'+TJPshadow+'n.png" style="width:'+(TJPzoomwidth-TJPshadowthick*4)+'px; height:'+TJPshadowthick*2+'px;'+st3 +st1+''+(TJPshadowthick*2)+'px 0 0 0; width:'+(TJPshadowthick*2)+'px; height:'+(TJPzoomheight-TJPshadowthick*4)+'px;'+st2+'\''+TJPshadow+'w.png\',sizingMethod=\'scale\')"><img src="'+TJPshadow+'w.png" style="width:'+(TJPshadowthick*2)+'px; height:'+(TJPzoomheight-TJPshadowthick*4)+'px;'+st3 +st1+''+(TJPshadowthick*2)+'px 0 0 '+(TJPzoomwidth-TJPshadowthick*2)+'px; width:'+(TJPshadowthick*2)+'px; height:'+(TJPzoomheight-TJPshadowthick*4)+'px;'+st2+'\''+TJPshadow+'e.png\',sizingMethod=\'scale\')"><img src="'+TJPshadow+'e.png" style="width:'+(TJPshadowthick*2)+'px; height:'+(TJPzoomheight-TJPshadowthick*4)+'px;'+st3 + st1+''+(TJPzoomheight-TJPshadowthick*2)+'px 0 0 '+(TJPshadowthick*2)+'px; width:'+(TJPzoomwidth-TJPshadowthick*4)+'px; height:'+TJPshadowthick*2+'px;'+st2+'\''+TJPshadow+'s.png\',sizingMethod=\'scale\')"><img src="'+TJPshadow+'s.png" style="width:'+(TJPzoomwidth-TJPshadowthick*4)+'px; height:'+TJPshadowthick*2+'px;'+st3;
								        }			        ;
								        //marker - zoomer
																		
								        obj.parentNode.insertBefore(TJPwin,TJPstage);
								        TJPresize(obj);
								    }
								    function TJPresize(obj) 
								    {
								        sbr=0; sbl=0;
								        if(TJPzoomwidth-2*TJPborderthick-3*TJPshadowthick < 22) {sbr=1}
								        if(TJPzoomheight-2*TJPborderthick-3*TJPshadowthick < 22) {sbr=1}
								        if(TJPzoomwidth > parseFloat(obj.style.width)) {sbl=1;}
								        if(TJPzoomheight > parseFloat(obj.style.height)) {sbl=1}
																		 
								        if(sbr==1 && sbl == 1) 
								        {
																		
								            TJPzoomwidth=parseFloat(obj.style.width)/2;
																		
								            TJPzoomheight=parseFloat(obj.style.height)/2;
																		
								            TJPzoomratio=TJPzoomheight/TJPzoomwidth;
								        }
								        if(sbr==1) {
								            if(TJPzoomwidth<TJPzoomheight) 
								            {																		
								                TJPzoomheight=TJPzoomheight/TJPzoomwidth*(22+2*TJPborderthick+3*TJPshadowthick); TJPzoomwidth=22+2*TJPborderthick+3*TJPshadowthick;
								            } 
								            else 
								            {					
								                TJPzoomwidth=TJPzoomwidth/TJPzoomheight*(22+2*TJPborderthick+3*TJPshadowthick); TJPzoomheight=22+2*TJPborderthick+3*TJPshadowthick;
								            }
								        }
																		 
								        if(sbl==1) 
								        {
								            if(parseFloat(obj.style.width)/parseFloat(obj.style.height) > TJPzoomwidth/TJPzoomheight) 
								            {																		
								                TJPzoomheight=parseFloat(obj.style.height);																		
								                TJPzoomwidth=TJPzoomheight/TJPzoomratio;
								            } 
								            else 
								            {																		
								                TJPzoomwidth=parseFloat(obj.style.width);																		
								                TJPzoomheight=TJPzoomratio*TJPzoomwidth;
								            }
								        }
																		
								        TJPzoomwidth=Math.floor(TJPzoomwidth/2)*2;																		
								        TJPzoomheight=Math.floor(TJPzoomheight/2)*2;																		
								        ww=obj.parentNode.getElementsByTagName('div')[0];
								        ww.style.width=TJPzoomwidth+'px';																		
								        ww.style.height=TJPzoomheight+'px';																		
								        w=ww.getElementsByTagName('div')[0];
								        w.id="firDiv";			
								        w.style.width=TJPzoomwidth-TJPshadowthick*2+'px';																		
								        w.style.height=TJPzoomheight-TJPshadowthick*2+'px';																		
								        w=ww.getElementsByTagName('div')[1];
								        w.id="secDiv";
								        w.style.width=TJPzoomwidth-TJPshadowthick*2-TJPborderthick*2+'px';																		
								        w.style.height=TJPzoomheight-TJPshadowthick*2-TJPborderthick*2+'px';
								        if(TJPshadowthick > 0) 
								        {
																		
								            w=ww.getElementsByTagName('span')[1]; w.style.margin='0 0 0 '+(TJPzoomwidth-TJPshadowthick*2)+'px';
																		
								            w=ww.getElementsByTagName('span')[2]; w.style.margin=(TJPzoomheight-TJPshadowthick*2)+'px 0 0 0px';
																		
								            w=ww.getElementsByTagName('span')[3]; w.style.margin=(TJPzoomheight-TJPshadowthick*2)+'px 0 0 '+(TJPzoomwidth-TJPshadowthick*2)+'px';
																		
								            w=ww.getElementsByTagName('span')[6]; w.style.margin=(TJPshadowthick*2)+'px 0 0 '+(TJPzoomwidth-TJPshadowthick*2)+'px';
																		
								            w=ww.getElementsByTagName('span')[7]; w.style.margin=(TJPzoomheight-TJPshadowthick*2)+'px 0 0 '+(TJPshadowthick*2)+'px';
								            www=(TJPzoomwidth-TJPshadowthick*4)+'px';
																		
								            w=ww.getElementsByTagName('span')[4]; w.style.width=www;
																		
								            w=w.getElementsByTagName('img')[0]; w.style.width=www;
																		
								            w=ww.getElementsByTagName('span')[7]; w.style.width=www;
																		
								            w=w.getElementsByTagName('img')[0]; w.style.width=www;
																		  
								            www=(TJPzoomheight-TJPshadowthick*4)+'px';
																		
								            w=ww.getElementsByTagName('span')[5]; w.style.height=www;
																		
								            w=w.getElementsByTagName('img')[0]; w.style.height=www;
																		
								            w=ww.getElementsByTagName('span')[6]; w.style.height=www;
																		
								            w=w.getElementsByTagName('img')[0]; w.style.height=www;
								        }
								    }
								    function TJPfindposy(obj) {
								        var curtop = 0;
								        if(!obj) {return 0;}
								        if (obj.offsetParent) {
								            while (obj.offsetParent) {
								                curtop += obj.offsetTop
								                obj = obj.offsetParent;
								            }
								        } else if (obj.y) {
								            curtop += obj.y;
								        }
								        return curtop;
								    }
								    function TJPfindposx(obj) {
								        var curleft = 0;
								        if(!obj) {return 0;}
								        if (obj && obj.offsetParent) {
								            while (obj.offsetParent) {
								                curleft += obj.offsetLeft
								                obj = obj.offsetParent;
								            }
								        } else if (obj.x) {
								            curleft += obj.x;
								        }
								        return curleft;
								    }
								    function TJPhandlemouse(evt,obj) {
								        var evt = evt?
								            evt:window.event?window.event:null; if(!evt) { return false; }
								        if(evt.pageX) {
								            nowx=evt.pageX-TJPfindposx(obj)-TJPadjustx;
								            nowy=evt.pageY-TJPfindposy(obj)-TJPadjusty;
								        } else {
								            if(document.documentElement && document.documentElement.scrollTop) 
								            {
								                nowx=evt.clientX+document.documentElement.scrollLeft-TJPfindposx(obj)-TJPadjustx;
								                nowy=evt.clientY+document.documentElement.scrollTop-TJPfindposy(obj)-TJPadjusty;
								            } else {
								                nowx=evt.x+document.body.scrollLeft-TJPfindposx(obj)-TJPadjustx;
								                nowy=evt.y+document.body.scrollTop-TJPfindposy(obj)-TJPadjusty;
								            }
								        }
								        if(evt.type == 'mousemove') 
								        {
								            TJPsetwin(obj,nowx,nowy);
								        } 
								        else if(evt.type == 'mousedown') 
								        {
								            TJPmouse=1; 
								            //left: 1, middle: 2, right: 3
								            TJPmousey=nowy;
								            TJPmousex=nowx;
								        } 
								        else if(evt.type =='mouseup') 
								        {
								            TJPmouse=0;
								        } 																	
																		
								        else if(evt.type =='mouseout') 
								        {
								            TJPmouse=0;
																			
								            if(navigator.appVersion.indexOf('Mac') == -1 || navigator.appVersion.indexOf('MSIE') == -1) 
								            { //hi Mac IE
								                var newx=obj.parentNode;
																				
								                newx.removeChild(newx.getElementsByTagName('div')[0]);
																				
								                newx.removeChild(newx.getElementsByTagName('div')[0]);
								            }
								        }
																		
								    }
								    function TJPsetwin(obj,nowx,nowy) {																		
								        obj.parentNode.getElementsByTagName('div')[0].style.display='block';
								        if(TJPzoomoffset=='smart') {
																		
								            TJPzoomoffsetx=.1+.8*nowx/parseFloat(obj.style.width);																		
								            TJPzoomoffsety=.1+.8*nowy/parseFloat(obj.style.height);
								        }																		
								        stage=obj.parentNode.getElementsByTagName('div')[0];
								        if(TJPmouse == 1) 
								        {
								            if(Math.abs(nowy-TJPmousey) >= 1) 
								            {
								                TJPzoomamount*=((nowy>TJPmousey)?(0.909):(1.1));
								                TJPmousey=nowy;
								                if(TJPzoomamount < TJPzoomamountmin) {TJPzoomamount=TJPzoomamountmin;}
								                if(TJPzoomamount > TJPzoomamountmax) {TJPzoomamount=TJPzoomamountmax;}
																		
								                stage.getElementsByTagName('div')[1].getElementsByTagName('img')[0].style.width=  parseInt(obj.style.width)*TJPzoomamount+'px';
																		
								                stage.getElementsByTagName('div')[1].getElementsByTagName('img')[0].style.height=  parseInt(obj.style.height)*TJPzoomamount+'px';
								                if(stage.getElementsByTagName('div')[1].getElementsByTagName('img')[1]) 
								                {																			
								                    stage.getElementsByTagName('div')[1].getElementsByTagName('img')[1].style.width= stage.getElementsByTagName('div')[1].getElementsByTagName('img')[0].style.width;																			
								                    stage.getElementsByTagName('div')[1].getElementsByTagName('img')[1].style.height= stage.getElementsByTagName('div')[1].getElementsByTagName('img')[0].style.height;
								                }
								            }
								            if(Math.abs(nowx-TJPmousex) >= 12 && TJPzoomwindowlock==0) 
								            {
								                TJPzoomwidth*=((nowx>TJPmousex)?(1.1):(0.909));																		
								                TJPzoomheight=TJPzoomwidth*TJPzoomratio;
								                TJPresize(obj);
								                TJPmousex=nowx;
								            }
								        }
																		
								        stage.style.marginLeft=nowx-(TJPzoomwidth -2*TJPborderthick-2*TJPshadowthick)*TJPzoomoffsetx-TJPborderthick-TJPshadowthick+'px';
								        stage.style.marginTop= nowy-(TJPzoomheight-2*TJPborderthick-2*TJPshadowthick)*TJPzoomoffsety-TJPborderthick-TJPshadowthick+'px';
								        clip1=0; 
								        clip2=TJPzoomwidth; clip3=TJPzoomheight; clip4=0;
								        nwidth=TJPzoomwidth; 
								        nheight=TJPzoomheight;
								        tmp=(1-2*TJPzoomoffsetx)*(TJPborderthick+TJPshadowthick);
																		 
								        if(nowx-TJPzoomwidth*TJPzoomoffsetx < tmp) 
								        {																		
								            clip4=TJPzoomwidth*TJPzoomoffsetx-nowx + tmp;
								        } 
								        else if(parseFloat(nowx-TJPzoomwidth*TJPzoomoffsetx+TJPzoomwidth) > parseFloat(obj.style.width)+tmp) 
								        {
								            clip2= TJPzoomwidth*TJPzoomoffsetx - nowx + parseFloat(obj.style.width)+tmp;																		
								            nwidth=TJPzoomwidth*TJPzoomoffsetx-nowx+parseInt(obj.style.width)+TJPborderthick+TJPshadowthick;
								        }
																		 
								        tmp=(1-2*TJPzoomoffsety)*(TJPborderthick+TJPshadowthick);
																		 
								        if(nowy-TJPzoomheight*TJPzoomoffsety < tmp) 
								        {																		
								            clip1=TJPzoomheight*TJPzoomoffsety-nowy+tmp;
								        } else if(parseFloat(nowy-TJPzoomheight*TJPzoomoffsety+TJPzoomheight) > parseFloat(obj.style.height)+tmp) 
								        {
								            clip3= TJPzoomheight*TJPzoomoffsety - nowy + parseFloat(obj.style.height)+tmp;																		
								            nheight=TJPzoomheight*TJPzoomoffsety - nowy + parseFloat(obj.style.height)+TJPborderthick+TJPshadowthick;
								        }
																		
								        stage.style.width=nwidth+'px';																		
								        stage.style.height=nheight+'px';
								        stage.style.clip='rect('+clip1+'px,'+clip2+'px,'+clip3+'px,'+clip4+'px)';
								        if(nowy-TJPzoomoffsety*(TJPzoomheight-2*TJPborderthick-2*TJPshadowthick) < 0) { t=-(nowy-TJPzoomoffsety*(TJPzoomheight-2*TJPborderthick-2*TJPshadowthick))} 
								        else if(nowy-TJPzoomoffsety*(TJPzoomheight-2*TJPborderthick-2*TJPshadowthick) > parseFloat(obj.style.height)-TJPzoomheight+TJPborderthick*2+TJPshadowthick*2) { t=-TJPzoomamount*parseFloat(obj.style.height)+TJPzoomheight-TJPborderthick*2-TJPshadowthick*2-((nowy-TJPzoomoffsety*(TJPzoomheight-2*TJPborderthick-2*TJPshadowthick))-(parseFloat(obj.style.height)-TJPzoomheight+TJPborderthick*2+TJPshadowthick*2)); }
								        else { t=(-TJPzoomamount*parseFloat(obj.style.height)+TJPzoomheight-TJPborderthick*2-TJPshadowthick*2)/(parseFloat(obj.style.height)-TJPzoomheight+TJPborderthick*2+TJPshadowthick*2)*(nowy-TJPzoomoffsety*(TJPzoomheight-2*TJPborderthick-2*TJPshadowthick)) }
																		
								        stage.getElementsByTagName('div')[1].getElementsByTagName('img')[0].style.marginTop=t+'px';
								        if(stage.getElementsByTagName('div')[1].getElementsByTagName('img')[1]) 
								        {																		
								            stage.getElementsByTagName('div')[1].getElementsByTagName('img')[1].style.marginTop=t+'px';
								        }
								        if(nowx-TJPzoomoffsetx*(TJPzoomwidth-2*TJPborderthick-2*TJPshadowthick) < 0) { t=-(nowx-TJPzoomoffsetx*(TJPzoomwidth-2*TJPborderthick-2*TJPshadowthick))} 
								        else if(nowx-TJPzoomoffsetx*(TJPzoomwidth-2*TJPborderthick-2*TJPshadowthick) > parseFloat(obj.style.width)-TJPzoomwidth+TJPborderthick*2+TJPshadowthick*2) { t=-TJPzoomamount*parseFloat(obj.style.width)+TJPzoomwidth-TJPborderthick*2-TJPshadowthick*2-((nowx-TJPzoomoffsetx*(TJPzoomwidth-2*TJPborderthick-2*TJPshadowthick))-(parseFloat(obj.style.width)-TJPzoomwidth+TJPborderthick*2+TJPshadowthick*2)); }
								        else { t=(-TJPzoomamount*parseFloat(obj.style.width)+TJPzoomwidth-TJPborderthick*2-TJPshadowthick*2)/(parseFloat(obj.style.width)-TJPzoomwidth+TJPborderthick*2+TJPshadowthick*2)*(nowx-TJPzoomoffsetx*(TJPzoomwidth-2*TJPborderthick-2*TJPshadowthick)) }
																		
								        stage.getElementsByTagName('div')[1].getElementsByTagName('img')[0].style.marginLeft=t+'px';
								        if(stage.getElementsByTagName('div')[1].getElementsByTagName('img')[1]) 
								        {																		
								            stage.getElementsByTagName('div')[1].getElementsByTagName('img')[1].style.marginLeft=t+'px';
								        }
								    }
								    function TJPinit() {
								        TJPadjustx=0; 
								        TJPadjusty=0;
								        if(navigator.userAgent.indexOf('MSIE')>-1) {TJPadjustx=2;TJPadjusty=2;}
								        if(navigator.userAgent.indexOf('Opera')>-1) {TJPadjustx=0; TJPadjusty=0;}
								        if(navigator.userAgent.indexOf('Safari')>-1) {TJPadjustx=1; TJPadjusty=2;}
								    }
								    // configuration - do    not modify the following, instead read the behaviors.html file in the tutorial!
								    var TJPon=new Array();
								    var TJPadjustx,TJPadjusty;
								    var TJPmouse=0; 
								    var TJPmousey; 
								    var TJPmousex;
								    var TJPloading='<div style="background-color: #ffeb77; color: #333333; padding:2px; font-family: verdana,arial,helvetica; font-size: 10px;">Loading...</div>';
								    var TJPzoomwidth=225;
								    var TJPzoomheight=225;
								    var TJPzoomratio;
								    var TJPzoomwindowlock=0;
								    var TJPzoomoffsetx=.5;
								    var TJPzoomoffsety=.5;
								    var TJPzoomoffset;
								    var TJPzoomamount=2;
								    var TJPzoomamountmax=12;
								    var TJPzoomamountmin=1;
								    var TJPborderthick=2;
								    var TJPbordercolor='#ffffff';
								    var TJPshadowthick=8;
								    var TJPshadow='dropshadow/';
								    TJPinit();
								</script>
								<IMG onMouseOver="TJPzoom(this);" height="125" src="indeximages/map.jpg" width="190">
							</td>
						</tr>
						<tr>
							<td><IMG height="2" src="indeximages/news-body2.jpg" width="200"></td>
						</tr>
					</table>
                            </td>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="27" background="indeximages/liuyan-bg.gif">
                                            <img src="indeximages/zxwj1.jpg" width="153" height="27" />
                                        </td>
                                        <td background="indeximages/liuyan-bg.gif">
                                            <div align="right"><a href="centerInfo.aspx?id=文件发布" target="_blank">
                                                <img height="5" src="indeximages/bokedluntan-more.gif" width="38"></a></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td colspan="3" height="10">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                </tr>
                                                <%
                                                    for (int i = 0; i < 7; i++)
                                                    {
                                                        if (dtCentorfile != null && dtCentorfile.Rows.Count > i)
                                                        {
                                                            string id = dtCentorfile.Rows[i]["id"].ToString();
                                                            string buildtime = "";
                                                            try
                                                            {
                                                                buildtime = Convert.ToDateTime(dtCentorfile.Rows[i]["buildtime"]).ToShortDateString();
                                                            }
                                                            catch
                                                            { }
                                                            DateTime dateTimeValue, dateTimeValue2;
                                                            dateTimeValue2 = DateTime.Now;
                                                            dateTimeValue = Convert.ToDateTime(dtCentorfile.Rows[i]["buildtime"]);
                                                            System.TimeSpan subtractTime = (dateTimeValue2 - dateTimeValue);
                                                            int days = Convert.ToInt32(subtractTime.TotalDays);
                                                %>
                                                <tr>
                                                    <td width="15">
                                                        <div align="center">
                                                            <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" /></div>
                                                    </td>
                                                    <td width="414" height="12">
                                                        <span class="STYLE9"><%if (days < 7)
                                                                               {%>
                                                            <a href="EachNews.aspx?id=<%=id%>" title='<%=dtCentorfile.Rows[i]["title"].ToString()%>' target="_blank"><%=CString(dtCentorfile.Rows[i]["title"].ToString(),29)%></a>
                                                            <img src="images/new.gif" width="28" height="11">
                                                            <%}
                                                                               else
                                                                               {%>
                                                            <a href="EachNews.aspx?id=<%=id%>" title='<%=dtCentorfile.Rows[i]["title"].ToString()%>' target="_blank">
                                                                <%=CString(dtCentorfile.Rows[i]["title"].ToString(),40)%>
                                                            </a>
                                                            <%}%>
                                                        </span>
                                                    </td>
                                                    <td width="72">
                                                        <div align="center">
                                                            <span class="STYLE9">
                                                                <%=Convert.ToDateTime(dtCentorfile.Rows[i]["buildtime"]).ToString("yyyy-MM-dd").Trim()%>
                                                            </span>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                </tr>
                                                <%}
                                            else
                                            {%>
                                                <tr height="10">
                                                    <td width="15" align="center">
                                                        <div align="center">
                                                            <img src="indeximages/zhuanjiaboke-icon.gif" width="3" height="5" />
                                                        </div>
                                                    </td>
                                                    <td><span class="STYLE9">&nbsp;</span></td>
                                                    <td><span class="STYLE9">&nbsp;</span></td>
                                                    <tr>
                                                        <td background="indeximages/diandiandian.jpg" colspan="3" height="7"></td>
                                                    </tr>
                                                </tr>
                                                <%}
                                        }%>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>                        
                    </table>

                </td>
            </tr>

            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="900" border="0">
                        <tr height="10">
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="900" border="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="100%"
                                                border="0">
                                                <tr>
                                                    <td height="148">
                                                        <uc1:Bot id="Bot"
                                                            runat="server">
                                                        </uc1:Bot>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hiddenW" runat="server" name="HiddenW"><!-- 浮动广告宽度 -->
        <input type="hidden" id="HiddenH" runat="server" name="HiddenH"><!-- 浮动广告高度 -->
        <input type="hidden" id="HiddenBG" runat="server" name="HiddenBG"><!-- 浮动广告背景颜色 -->
        <input type="hidden" id="HiddenV" runat="server" name="HiddenV"><!-- 浮动广告移动速度 -->
        <input type="hidden" id="HiddenS" runat="server" name="HiddenS"><!-- 浮动广告显示的内容，允许超文本 -->
        <input type="hidden" id="HiddenU" runat="server" name="HiddenU"><!-- 浮动广告单击后连接的地址 -->
        <input type="hidden" id="HiddenDSP" runat="server" name="hiddenDSP">
        <!-- 控制浮动广告是否显示：0：不显示；1显示 -->
        <input type="hidden" id="HiddenIsTopic" value="1" runat="server" name="HiddenIsTopic"><!-- 是否只显示置顶信息（0：否；1：是） 
-->
    </form>
</body>
</html>

</body>
</HTML>
