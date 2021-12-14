<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPrint_Weblis_QinDaoLanXin.aspx.cs"
    Inherits="ZhiFang.WebLis.ReportPrint.ReportPrint_Weblis_QinDaoLanXin" %>

<script language="javascript">
<!--
    self.moveTo(0, 0);
    self.resizeTo(screen.availWidth, screen.availHeight);
//-->
</script>

<script type="text/javascript" src="../js/Tools.js"></script>

<script language="javascript" type="text/javascript">
    function printpr()   //预览函数
    {
        try {
            document.all("qingkongyema").click(); //打印之前去掉页眉，页脚
            document.all("dayinDiv").style.display = "none"; //打印之前先隐藏不想打印输出的元素（此例中隐藏“打印”和“打印预览”两个按钮）
            var OLECMDID = 7;
            var PROMPT = 1;
            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser);
            WebBrowser1.ExecWB(OLECMDID, PROMPT);
            WebBrowser1.outerHTML = "";
            var formnoa = String(getQueryString("ReportFormID")).split(',');
            document.all("dayinDiv").style.display = ""; //打印之后将该元素显示出来（显示出“打印”和“打印预览”两个按钮，方便别人下次打印）
            for(var i=0;i<formnoa.length;i++) {
                SetPrintFlag(formnoa[i].split(';')[0], "true");
            }
            
        }
        catch (e) {
//            alert('预览失败！' + e.message + "@@@" + e.description + "@@@" + e.number + "@@@" + e.name);
        }
    }
    function printTure()   //打印函数
    {
        document.all('qingkongyema').click(); //同上
        document.all("dayinDiv").style.display = "none"; //同上
        window.print();
        document.all("dayinDiv").style.display = "";
        var formnoa = String(getQueryString("ReportFormID")).split(',');
        for (var i = 0; i < formnoa.length; i++) {
            SetPrintFlag(formnoa[i].split(';')[0], "true");
        }
        
    }
    function doPage() {
        layLoading.style.display = "none"; //同上
    }
    function WindowClose() {
        window.close();
    }
    function SetPrintFlag(Fromno, flag) {
        if (String(flag) == "true") {
            var t = Report.Ashx.ReportPrint.SetReportFromFullPrintFlag(Fromno, "1");
        }
        else {
            alert('打印失败！报告单：' + Fromno);
        }
    }
</script>
<script type="text/javascript" src="/ceshi--reportformprint_weblis/ajaxpro/prototype.ashx"></script>
<script type="text/javascript" src="/ceshi--reportformprint_weblis/ajaxpro/core.ashx"></script>
<script type="text/javascript" src="/ceshi--reportformprint_weblis/ajaxpro/converter.ashx"></script>
<script type="text/javascript" src="/ceshi--reportformprint_weblis/ajaxpro/Report.Ashx.ReportPrint,Report.ashx"></script>
<script language="VBScript">
dim hkey_root,hkey_path,hkey_key
hkey_root="HKEY_CURRENT_USER"
hkey_path="\Software\Microsoft\Internet Explorer\PageSetup"
//设置网页打印的页眉页脚为空
function pagesetup_null()
on error resume next
Set RegWsh = CreateObject("WScript.Shell")
hkey_key="\header" 
RegWsh.RegWrite hkey_root+hkey_path+hkey_key,""
hkey_key="\footer"
RegWsh.RegWrite hkey_root+hkey_path+hkey_key,""
end function
//设置网页打印的页眉页脚为默认值
function pagesetup_default()
on error resume next
Set RegWsh = CreateObject("WScript.Shell")
hkey_key="\header" 
RegWsh.RegWrite hkey_root+hkey_path+hkey_key,"&w&b页码，&p/&P"
hkey_key="\footer"
RegWsh.RegWrite hkey_root+hkey_path+hkey_key,"&u&b&d"
end function
</script>

<%Response.Write(strTable); %>
<form name="theform" action="PrintAll.asp" method="post">
<div id='dayinDiv'>
    <span style="cursor: hand" onclick="printSet();">打印设置</span>&nbsp;&nbsp; <span style="cursor: hand"
        onclick="printpr();">打印预览</span>&nbsp;&nbsp; <span style="cursor: hand" onclick="printTure();">
            打印</span>&nbsp;&nbsp; </font>
    <input type="hidden" name="qingkongyema" id="qingkongyema" class="tab" value="清空页码"
        onclick="pagesetup_null();">&nbsp;&nbsp;
    <input type="hidden" class="tab" value="恢复页码" onclick="pagesetup_default();"></div>
</form>
