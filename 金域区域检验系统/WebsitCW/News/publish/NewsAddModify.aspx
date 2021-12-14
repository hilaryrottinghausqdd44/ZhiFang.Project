<%@ Page Language="c#" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="True"
    Inherits="OA.News.publish.NewsAddModify" CodeBehind="NewsAddModify.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>新闻添加、修改页面</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../../Includes/CSS/ioffice.css">

    <script language="JavaScript">
		<!--
        var clicked = false;
        function VerifyFormEmployee() {
            if (clicked) {
                alert('正在操作，请稍候');
                return false;
            }
            if (Form1.TxtBoxTitle.value.length == 0) {
                alert('文章标题不能为空，谢谢合作啦！');
                return false;
            }
            else {
                var F1 = judge(Form1.TxtBoxTitle.value);
                if (F1 == 1) {
                    alert('请不要在文章标题中输入以下字符：<，>，#，&，"，\'。谢谢合作啦！');
                    return false;
                }
            }
            clicked = true;
        }
        function buttClick() {
            this.close;
        }
        function judge(value) {
            var Btitle = value;
            var status = new Array(6);
            var F2;
            status[0] = "#";
            status[1] = "&";
            status[2] = "<";
            status[3] = ">";
            status[4] = "\"";
            status[5] = "'";
            for (var i = 0; i < status.length; i++) {
                for (var j = 0; j < Btitle.length; j++) {
                    if (Btitle.indexOf(status[i], j) > -1) {
                        F2 = 1
                        return F2;
                    }
                    else {
                        F2 = 0;
                    }
                }
            }
            return F2;
        }
        function showpicset() {
            var myCheck = document.all["chboxtatch"];
            if (myCheck != null) {
                if (myCheck.checked)
                    document.all["trTatch"].style.display = "";
                else
                    document.all["trTatch"].style.display = "none";
            }
        }
        function SysConfig() {
            var CheckBoxPermitReply = document.getElementById("CheckBoxPermitReply");
            //alert(linkbtnSetConfig.Checked)
            if (CheckBoxPermitReply.checked) {
                var hiddenSystemArgs = document.getElementById("hiddenSystemArgs");
                var hiddenSystemNameID = document.getElementById("hiddenSystemNameID");
                var url = "../../News/publish/ReplyConfig.aspx?SystemArgs=" + hiddenSystemArgs.value + "&SystemNameID=" + hiddenSystemNameID.value;
                window.open(url, "NewsAddModify", "status=false,width=700px,height=220px");
            }
        }
        function SetIfPermitReplyd(obj) {
            var linkbtnSetConfig = document.getElementById("linkbtnSetConfig");
            var CheckBoxQR = document.getElementById("CheckBoxQR");
            var CheckBoxTC = document.getElementById("CheckBoxTC");
            var btnSelectStyle = document.getElementById("btnSelectStyle");

            var btnSetAdvanced = document.getElementById("btnSetAdvanced");
            if (obj.checked) {
                btnSetAdvanced.disabled = false;
                linkbtnSetConfig.disabled = false;
                CheckBoxQR.disabled = false;
                CheckBoxTC.disabled = false;
                btnSelectStyle.disabled = false;
            }
            else {
                btnSetAdvanced.disabled = true;
                linkbtnSetConfig.disabled = true;
                CheckBoxQR.disabled = true;
                CheckBoxTC.disabled = true;
                btnSelectStyle.disabled = true;
            }
        }
        function setAdvanced() {
            var divAdvancedConfig = document.getElementById("divAdvancedConfig");
            if (divAdvancedConfig.style.display == "") {
                divAdvancedConfig.style.display = "none";
            }
            else {
                divAdvancedConfig.style.display = "";
            }
        }
		//-->
    </script>

    <script language="javascript" id="clientEventHandlersJS">
		<!--

        function window_onload() {
            showpicset();
            var CheckBoxPermitReply = document.getElementById("CheckBoxPermitReply");
            if (CheckBoxPermitReply.checked) {
                linkbtnSetConfig.disabled = false;
            }
            else {
                linkbtnSetConfig.disabled = true;
            }
        }

		//-->
    </script>

    <!--
    <script type="text/javascript" src="../../includes/ewebeditor/js/buttons.js"></script>
<script type="text/javascript" src="../../includes/ewebeditor/js/ewebeditor.js"></script>
<script type="text/javascript" src="../../includes/ewebeditor/language/zh-cn.js"></script>
<script type="text/javascript">    fR("../../includes/ewebeditor/asp/i.asp?action=config&style=" + an.StyleName);</script>
<script type="text/javascript">    lang.Init(); fR("language/zh-cn.js");</script>
<link href='../../includes/ewebeditor/language/zh-cn.css' type='text/css' rel='stylesheet'/>
<link href='../../includes/ewebeditor/skin/blue1/editor.css' type='text/css' rel='stylesheet'/>;
-->

    <script type="text/javascript">
        function videoupload() {
            //alert('aaa');

            var strurl = '../../includes/ewebeditorb/filesmanage.aspx';
            var r = window.showModalDialog(strurl, this, 'dialogWidth:530px;dialogHeight:500px;help:no;status:no');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                //alert(r);
                window.frames["eWebEditor1"].frames["eWebEditor"].focus();
                window.frames["eWebEditor1"].frames["eWebEditor"].document.selection.createRange().pasteHTML(r);
            }

        }
        //插入或修改网页帧
        function addiframe() {
            var strurl = '../../news/news/manage/htmledit/dialog/iframe.htm?type=0';
            var r = window.showModalDialog(strurl, this, 'dialogWidth:600px;dialogHeight:405px;help:no;scroll:no;status:no');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                //alert(r);
                window.frames["eWebEditor1"].frames["eWebEditor"].focus();
                window.frames["eWebEditor1"].frames["eWebEditor"].document.selection.createRange().pasteHTML(r);
            }

        }
        //插入或修改超级链接
        function createlink() {

            //ShowDialog('dialog/creatlink.htm', 450, 385, true);
            var strurl = '../../news/news/manage/htmledit/dialog/creatlink.htm?type=0';
            var r = window.showModalDialog(strurl, this, 'dialogWidth:450px;dialogHeight:385px;help:no;scroll:no;status:no');
            if (r == '' || typeof (r) == 'undefined' || typeof (r) == 'object') {
                return;
            }
            else {
                //alert(r);
                //window.frames["eWebEditor1"].frames["eWebEditor"].focus();
                //window.frames["eWebEditor1"].frames["eWebEditor"].document.selection.createRange().pasteHTML(r);
            }
        }
        
    </script>

    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">
</head>
<body language="javascript" bottommargin="0" leftmargin="0" topmargin="0" onload="return window_onload()"
    rightmargin="0">
    <form id="Form1" onsubmit="javascript:return VerifyFormEmployee()" method="post"
    runat="server">
    <table id="Table1" style="border-collapse: collapse" cellpadding="2" width="100%"
        align="center" border="1" height="93%">
        <tr>
            <td align="center" width="90">
                信息分类
            </td>
            <td>
                <asp:Label ID="LabelTitle" runat="server" Width="496px" ForeColor="#99ccff" Font-Bold="true"
                    Font-Size="12"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 67px" valign="middle" nowrap align="center" width="67">
                文章标题
            </td>
            <td>
                <asp:TextBox ID="TxtBoxTitle" runat="server" Width="584px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 67px" valign="middle" align="center">
                关键字
            </td>
            <td>
                <p>
                    <asp:TextBox ID="TxtBoxkeyword" runat="server" Width="584px"></asp:TextBox><br>
                    可以输入多个关键字，用全角或半角的<font color="red">，</font> 号分开。</p>
            </td>
        </tr>
        <tr>
            <td style="width: 67px" valign="middle" align="center">
                文摘
            </td>
            <td>
                <asp:TextBox ID="TextBoxdig" runat="server" Width="584px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <tr height="70%">
                <td style="width: 67px; height: 70%" valign="middle" align="center">
                    文章内容
                </td>
                <td style="height: 340px">
                    <!--src=../news/manage/htmledit/eWebEditor.asp?id=content1&amp;style=standard_light,standard-->
                    <iframe id="eWebEditor1" src="../../includes/eWebEditor/ewebeditor.htm?id=content1&style=standard500"
                        frameborder="0" width="100%" scrolling="no" height="100%"></iframe>
                    <!--<iframe id="eWebEditor1" src="../news/manage/htmledit/eWebEditor.asp?id=content1&amp;style=standard_light"
                        frameborder="0" width="100%" scrolling="no" height="100%"></iframe>-->
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="center">
                </td>
                <td>
                    <div class="Btn" title="视频文件管理" style="float: left" onclick="videoupload();">
                        <img class="Ico" src="../../Includes/eWebEditor/SysImage/icon16/mid.gif">视频文件管理</div>
                    <div class="Btn" title="插入或修改网页帧" style="float: left" onclick="addiframe();">
                        <img class="Ico" src="../news/manage/htmledit/buttonimage/standard/iframe.gif">插入或修改网页帧</div>
                    <div class="Btn" title="插入或修改超级链接" style="float: left" onclick="createlink()">
                        <img class="Ico" src="../news/manage/htmledit/buttonimage/standard/CreateLink.gif">插入或修改超级链接</div>
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="center">
                    文章属性
                </td>
                <td>
                    <asp:CheckBox ID="CheckBoxPic" runat="server" Text="图片新闻 "></asp:CheckBox>
                    <input id="chboxtatch" title="附件" onclick="javascript:showpicset()" type="checkbox"
                        name="chboxtatch" runat="server">粘贴附件
                    <input id="content1" type="hidden" value="<%=InHTML(Content)%>" name="content1">
                    <asp:CheckBox ID="CheckBoxPermitReply" AutoPostBack="false" Text="允许回复" runat="server" />
                    <a id="linkbtnSetConfig" href="#" onclick="SysConfig();">回复配置</a> (选择允许回复后,要点击回复配置,并保存,因为系统要求知道允许哪些角色进行评论)
                    <input type="button" id="btnSetAdvanced" value="高级" onclick="setAdvanced();" />
                    <div id="divAdvancedConfig" style="display: none;">
                        <fieldset style="width:150px">
                            <legend>回复列表样式：</legend>
                            <asp:Label ID="labelReplyListStyle" runat="server">默认</asp:Label><input type="button"
                            id="btnSelectStyle" value="选择新样式" />
                        </fieldset>
                        
                        <fieldset style="width:200px">
                            <legend>回复录入方式：</legend>
                            <input type="radio" name="TCFS" id="CheckBoxQR" runat="server" /><label for="CheckBoxQR">嵌入回复</label><input
                            type="radio" name="TCFS" id="CheckBoxTC" runat="server" /><label for="CheckBoxTC">弹出回复</label>
                        </fieldset>
                        
                         <fieldset style="width:100px">
                            <legend>回复列表默认展开</legend>
                            <input type="checkbox" name="chkExpand" id="chkExpand" runat="server" checked="checked" /><label for="chkExpand">展开</label>
                        </fieldset>
                        
                    </div>
                </td>
            </tr>
            <tr id="trTatch" style="display: none">
                <td valign="middle" align="center">
                    附件内容
                </td>
                <td>
                    <p>
                        <iframe id="tatchEditor1" src="AtatchFiles.aspx?Catagory=<%=Catagory%>&amp;ID1=<%=ID1%>"
                            frameborder="0" width="100%" scrolling="no" height="160"></iframe>
                        &nbsp;</p>
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="center">
                    文章信息
                </td>
                <td>
                    <div style="display: inline; width: 40px; height: 8px" align="center" ms_positioning="FlowLayout">
                        时间:
                    </div>
                    <asp:TextBox ID="TextBoxBuildTime" runat="server" Columns="17"></asp:TextBox>
                    <div style="display: inline; width: 40px; height: 8px" align="center" ms_positioning="FlowLayout">
                        作者:
                    </div>
                    <asp:TextBox ID="TextBoxauthor" runat="server" Columns="10"></asp:TextBox>
                    <div style="display: inline; width: 96px; height: 10px" align="center" ms_positioning="FlowLayout">
                        文章来源：
                    </div>
                    <asp:TextBox ID="TextBoxSource" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="center">
                    审核人
                </td>
                <td>
                    <asp:TextBox ID="TextBoxchecker" runat="server"></asp:TextBox><asp:CheckBox ID="CheckBoxCH"
                        runat="server" Text="是否通过审核"></asp:CheckBox>
                    &nbsp;显示排序<asp:TextBox ID="TextBoxOrderBy" runat="server" Text="1"></asp:TextBox>
                </td>
            </tr>
    </table>
    <table id="Table2" style="height: 24px" cellspacing="1" cellpadding="1" width="100%"
        border="0">
        <tr>
            <td valign="middle" align="center" style="width: 120px">
                <asp:Button ID="ButtonAdd" runat="server" Text="发布" CausesValidation="False" CommandArgument="false"
                    OnClick="ButtonAdd_Click"></asp:Button>
            </td>
            <td valign="middle" align="center" style="width: 114px">
                <font face="宋体">
                    <input id="Reset" type="reset" value="清除内容" name="BottonReset"></font>
            </td>
            <td valign="middle" align="center" style="width: 180px">
                <font face="宋体">
                    <input id="buttonClose" onclick="window.close()" type="button" value="关闭窗体"></font>
            </td>
            <td valign="middle" align="center" style="width: 149px">
                <font face="宋体">
                    <input id="buttonAddNew" style="display: none" type="button" value="添加信息" runat="server"
                        onserverclick="buttonAddNew_ServerClick"></font>
            </td>
            <td valign="middle" align="center">
            </td>
        </tr>
    </table>
    <asp:Label ID="Label3" runat="server" Width="100%" BorderWidth="1px" BorderStyle="Solid"
        BorderColor="#FF8080"></asp:Label>
    <input type="hidden" id="hiddenSystemArgs" runat="server" />
    <input type="hidden" id="hiddenSystemNameID" runat="server" />
    </form>
</body>
</html>
