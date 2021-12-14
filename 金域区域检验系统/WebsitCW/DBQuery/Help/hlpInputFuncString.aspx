<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hlpInputFuncString.aspx.cs" Inherits="OA.DBQuery.Help.hlpInputFuncString" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设置输入规则</title>
    <style type="text/css">
        .style1
        {
            background-color: #FF9999;
        }
        .style2
        {
            color: #0000CC;
        }
    a:link
	{color:blue;
	text-decoration:underline;
	text-underline:single;
        }
    </style>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function window_onunload() {
//            var a = "33 *0.15";
//            var b = eval(a);
//            alert(b);
        }

// ]]>
    </script>
</head>
<body onunload="return window_onunload()">
    <p>
        数据在录入时产生的动作:如</p>
    <p>
        1, 以下是一个边输入字母边选择数据列表的功能</p>
    <p style="margin-left: 40px; background-color: #FF9999">
        获取列表(表:智方用户资源,CustomersData;字段:HospName,Province,HospAddress,HospNamePY;宽度:200;行数:15;显示字段:HospName,HospNamePY)<br />
        <span class="style2">最好用onkeyup解发</span></p>
    <form id="form1" runat="server">
    <div>
    
        2, 以下是一个边输入汉字,边生成拼音字头的功能:</div>
    </form>
<p style="margin-left: 40px">
    <span class="style1">生成拼音字头()
    <br />
&nbsp;<span class="style2">最好用onpropertychange触发</span></span></p>
<p>
    3, 以下是一个边输入边生成运算结果的功能,如实现输入项目费用,产生项目费用*15%的比例结果</p>
<p style="margin-left: 40px">
&nbsp;<span class="style1">计算结果(math.sum([费用字段英文名] * 0.15)) 或&nbsp; 计算结果([费用字段英文名] * 
    0.15)<br />
    <span class="style2">最好用onchange触发, 其中math.round([费用字段英文名] * 
    0.15)基本上为javascript脚本再加上界面上的字段结果</span></span></p>
<p class="MsoNormal">
    <b style="margin-left: 80px"><span style="color:red">功能描述</span></b><span 
        lang="EN-US" style="color:red">:</span><span style="color:black">单表系统录入时可自动计算多个字段的数据<span 
            lang="EN-US"><o:p></o:p></span></span></p>
    <p class="MsoNormal">
        <b style="margin-left: 80px"><span style="color:red">配置办法<span l:</span></span></b></p>
<p class="MsoNormal" style="margin-left: 120px">
    <span lang="EN-US" style="color:black">1,</span><span style="color:black">手动计算时<span 
        lang="EN-US">,</span>设置功能名称<span 
        lang="EN-US">[</span>如计算一下<span lang="EN-US">],</span>选择要计算的字段<span 
        lang="EN-US">,</span>去掉调用外部功能选择项<span lang="EN-US">,</span>设置执行功能时机为<span 
        lang="EN-US">onclick,</span>指定计算完成返回到哪个字段<span lang="EN-US">[</span></span><b><span 
        style="color:#00B0F0">字段中文名</span></b><span lang="EN-US" 
        style="color:black">]<o:p></o:p></span></p>
<p class="MsoNormal" style="margin-left: 120px">
    <span lang="EN-US" style="color:black">2,</span><span style="color:black">自动计算时<span 
        lang="EN-US">,</span>功能名称为空<span 
        lang="EN-US">, </span>选择要计算字段<span lang="EN-US">,</span>去掉调用外部功能选择项<span 
        lang="EN-US">,</span>执行功能时机为<span lang="EN-US">onpropertychange, </span>
    指定计算完成返回到哪个字段<span lang="EN-US">[</span>字段中文名<span lang="EN-US">]<o:p></o:p></span></span></p>
<p class="MsoNormal" style="margin-left: 80px">
    <b><span style="color:red">示例pan style="color:red">示例<span lang="EN-US">:</span></span></b><span 
            lang="EN-US" style="color:black"> </span><span style="color:black">功能规则<span 
            lang="EN-US"> [</span></span><b><span style="color:#00B0F0">字段英文名</span></b><span 
            lang="EN-US" style="color:black">] </span><span style="color:black">
        加上数学逻辑运算符<sp对象方法属性等</span><b><span lang="EN-US" 
            style="color:red"><o:p></o:p></span></b></p>
    <p class="MsoNormal" style="margin-left: 120px">
        <span lang="EN-US" style="color:black">1, </span><span style="color:black">
        自动计算出费用总计中的<span lang="EN-US">15%, </span>语法<span lang="EN-US">:<b><u>[</u></b></span><b><u>总计字段英文名<span 
            lang="EN-US">] * 0.15</span></u></b><span lang="EN-US">, </span>可进行数学<span 
            lang="EN-US"> + - * /</span>等运算<span lang="EN-US">,</span>如</span><b><u><span 
            lang="EN-US" style="color:#00B0F0">[PlanTotalFee] * 0.15</span></u></b><span 
            lang="EN-US" style="color:black"><o:p></o:p></span></p>
    <p class="MsoNormal" style="margin-left: 120px">
        <span lang="EN-US" style="color:black">2, </span><span style="color:black">
        自动计算多项费用的合计<span lang="EN-US">(Sum), </span>语法<span lang="EN-US">:<b><u>[</u></b></span><b><u>金额总计<span 
            lang="EN-US">] </span>–<span lang="EN-US"> ([</span>支出<span lang="EN-US">1] 
        + [</span>支出…<span lang="EN-US">] + [</span>支出<span lang="EN-US">n])</span></u></b><span 
            lang="EN-US"><o:p></o:p></span></span></p>
    <p class="MsoNormal" style="margin-left: 120px">
        <span lang="EN-US" style="color:black">3, </span><span style="color:black">
        折分数据规则<span lang="EN-US">, </span>某个样本编号字段为 ‘<span lang="EN-US">z100-301</span>’<span 
            lang="EN-US">, </span>可以在三个字段上分别显示<span lang="EN-US"> z,100,301</span>等<span 
            lang="EN-US"><o:p></o:p></span></span></p>
    <p class="MsoNormal" style="margin-left: 80px">
        <span lang="EN-US" style="color:black">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </span><span style="color:black">语法n style="color:black">语法<span lang="EN-US">: </span><b><u>‘<span 
            lang="EN-US">[</span>样本编号<span lang="EN-US">]</span>’<span lang="EN-US">.substr(0,1),
        </span>‘<span lang="EN-US">[</span>样本编号<span lang="EN-US">]</span>’<span 
            lang="EN-US">.substr(1,3), </span>‘<span lang="EN-US">[</span>样本编号<span 
            lang="EN-US">]</s.substr(5,3)</span></u></b><span 
            lang="EN-US"><o:p></o:p></span></span></p>
    <p class="MsoNormal" style="margin-left: 120px">
        <span lang="EN-US" style="color:black">4, </span><span style="color:black">替换数据<span 
            lang="EN-US">, </span>语法 <b><u>‘<span lang="EN-US">[</span>描述<span 
            lang="EN-US">]</span>’<span lang="EN-US">.replace(/a/g,</span>’<span 
            lang="EN-US">A</span>’<span lang="EN-US">)</span></u></b><span lang="EN-US">,</span>可以把<span 
            lang="EN-US">[</span>描述<span lang="EN-US">]</span>字段中的全部<span lang="EN-US">a</span>替换为<span 
            lang="EN-US">A<o:p></o:p></span></span></p>
    <p class="MsoNormal" style="margin-left: 80px">
        <b><span style="color:red">更多参考n style="color:red":<o:p></o:p></span></span></b></p>
    <p class="MsoNormal" style="margin-left: 120px">
        <span style="color:black">可以使用<span lang="EN-US">javascript(<a 
            href="http://www.w3school.com.cn/js/">http://www.w3school.com.cn/js/</a>)</span>语言的函数<span 
            lang="EN-US">,</span>对象进行规则定义<span lang="EN-US">.<o:p></o:p></span></span></p>
    <p style="margin-left: 40px">
        &nbsp;</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
</body>
</html>
