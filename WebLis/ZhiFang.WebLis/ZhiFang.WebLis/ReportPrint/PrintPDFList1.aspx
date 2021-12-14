<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPDFList1.aspx.cs"
    Inherits="ZhiFang.WebLis.ReportPrint.PrintPDFList1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批量浏览打印文件</title>
     <link type="text/css" rel="stylesheet" href="../Css/jyjgcxstyle.css" />
	<script type="text/javascript" src="../js/calendarb.js"></script>
    <script type="text/javascript" src="../js/Tools.js"></script>
    <script type="text/javascript">
        //测试在iframe打开另一页面
        function showpdf(fpath) {
            window.frames['frame1'].location.href = "PrintPDF.aspx?reportfile=" + fpath;
        }
        function ChangeButtonState(flag) {

            //表示正在打印中按钮不可操作
            if (flag == "1") {
                document.getElementById("btnsave").disabled = true;
                document.getElementById("btndao").disabled = true;
            }
            else {
                document.getElementById("btnsave").disabled = false;
                document.getElementById("btndao").disabled = false;
            }
        }
        //判断是否有选择的
        //记录选择行数数组
        var selectrows = null;
        var selectnum = 0;
        var Fromnoa = null;
        var printtype = 0; //套打 1,非套打 0
        function JudgeSelectRow(printtypeflag) {
            var Fromno = GetCheckBoxValue('FromNoCheckBox');
            if (String(Fromno) == "") {
                alert('请选择要打印的报告单');
                return false;
            }
            else {
                Fromnoa = String(Fromno).split(',');
                printsave1(0,1);
                ChangeButtonState(1);
            }

        }
        var stime = null;
        var rownum = 1;
        //循环间隔时间
        var ftime = 10000;
        //打印操作
        //completeflag 0加载未完成 1 加载完成
        function printsave1(rownum, completeflag) {
            //取文件路径
            var fpath = "";
            var t = "TmpReportImagePath";
            var strB = String(Fromnoa[rownum]).split(';');
            fpath = "../" + t + "//" + strB[0].replace(' 00:00:00', '000000') + ".pdf";
            if (completeflag == 1) {
                window.frames['frame1'].location.href = "PrintPDF.aspx?reportfile=" + fpath;
                divprint.innerHTML = "正在打印:  <img src='../../Images/loading.gif'/>";
            }
            if (window.frames["frame1"] != null && window.frames["frame1"].document != null
		                && window.frames["frame1"].document.readyState == "complete") {
                if (window.frames["frame1"].GetIsNoPdf()) {
                    window.frames["frame1"].PrintPdf();
                    rownum++;
                    if (rownum < Fromnoa.length) {
                        stime = setTimeout("printsave1(" + rownum + ",'1')", ftime);
                    }
                    else {
                        document.getElementById("divprint").innerHTML = "打印完成";
                        clearTimeout(stime);
                    }
                }
            }
            else {
                setTimeout("printsave1(" + rownum + ",'0')", 2000);
            }
        }
      
        //终止时钟停止打印
        function stopprint() {
            if (stime != null) {
                clearTimeout(stime);
                window.status = "已停止";
                divprint.innerHTML = "已停止,该打印:" + orgName + "   " + cname;
                ChangeButtonState(0);
            }
        }
        //更新打印次数
        function updateprinttimes(id, times) {
            OA.DBQuery.Print.PrintPDF.UpdatePrintTimes(id, GetCallresult);
        }
        //改变选中行背景色
        var changecolorobj = null;
        function ChangeBgColor(obj) {
            obj.style.backgroundColor = "#ddf3dd";
            if (changecolorobj != null) {
                changecolorobj.style.backgroundColor = "white";
                changecolorobj = obj;
            }
            else {
                changecolorobj = obj;
            }
        }
        //用于记录翻页操作
        function RunPage(StartPage, PageCount) {
            Form1.hPageBegins.value = StartPage;
            Form1.hPageSize.value = PageCount;
            Form1.submit();
        }
    </script>
</head>
<body onload="">
    <form id="Form1" method="post" runat="server">
    <input type="hidden" id="OrderColumnFlag" value="asc" runat="server" />
    <input type="hidden" id="PageName" value="TechnicianPrint1.aspx" runat="server" />
    <input type="hidden" id="hPageSize" name="hPageSize" value="<%=hPageSize%>" />
    <input type="hidden" id="hPageBegins" name="hPageBegins" value="<%=hPageBegins %>" />
    <input type="hidden" id="Hidden1" name="hPageBegins" value="<%=path %>" />
    <input type="hidden" id="CheckBoxFlag" value="0" runat="server" />
    <input type="hidden" id="OrderColumn" value="" runat="server" />
    <input type="hidden" id="tmpclassid" value="1" runat="server" />
    <div id="divSubsMe">
        <%
            int iPages = 0;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int intSubMeRows = 10;
                if (intSubMeRows > iCount)
                    intSubMeRows = iCount;
                iPages = iCount / hPageSize;
                iPages++;
                if ((int)iCount / hPageSize == (double)iCount / hPageSize)
                    iPages--;
                string SubMeDataLines = iCount.ToString();
                if (Int32.Parse(SubMeDataLines) > intSubMeRows) 
                    intSubMeRows = Int32.Parse(SubMeDataLines);
                string tableEName = "";
        %>
        <table id="TableSubMeData" width="100%" cellspacing="0" cellpadding="1"
            border="0">
            <tr>
                <td align="left" width="10%" title="" style="font-weight: bold; font-size: 9pt">
                    &nbsp;检验报告单
                </td>
                <td align="left" width="10%" title="<%=tableEName%>" style="font-weight: bold;
                    font-size: 9pt">
                    <br />
                    <%hPageBegins = hPageBegins > iPages ? iPages : hPageBegins;%>
                    <input type="text" style="border: #666666 1px solid;" size="2" onkeypress="if(event.keyCode==13) {RunPage(this.value,<%=hPageSize%>);return;}event.returnValue=IsDigit();"
                        id="txtiPageBegins" value="<%=hPageBegins%>" />/<%=iPages%>页(共<%=iCount%>条记录)&nbsp;&nbsp;
                </td>
                <td nowrap align="right" class="small" style="font-size: 9pt" width="60%">
                    <a href="#" disabled nochange="No" onclick="AddNewSubMe(this)"></a>&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" width="30%" colspan="2">
                    <div id="showclasslist" runat="server">
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="contlefttableout">
                                <table width="100%" cellspacing="1" cellpadding="0" border="0" class="contlefttable">
                                    <tr class='tablehead' onmousemove="javascript:this.style.backgroundColor='#ddf3dd';this.style.cursor='hand';"
                                        onmouseout="javascript:this.style.backgroundColor='white';" style="font-weight: bold">
                                        <td>
                                            <div class='tablehead' id="tablehead" runat="server" style="background-color: Gray">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td style="background-color: #FFFFFF">
                                            <div >
                                                <div class="scrollTable" id="tablelist" runat="server">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="float: left;">
                        <input type="button" id="btnsave" value="打印" onclick="JudgeSelectRow(0);" />
                        &nbsp;&nbsp;
                        <input type="button" id="btnstop" value="停止" onclick="stopprint();" />
                    </div>
                    
                </td>
                <td width="60%" valign="top">
                    <%  
string tmpfile = "";
tmpfile = filepath[0].Replace("../", "");
                    %>
                    <div id="divprint" style="float: left; background-color: #ddf3dd; font-size: larger;">
                    </div>
                    <iframe id="frame1" style="width: 100%; height: 631px; margin-top: 0px;" 
                        scrolling="auto" frameborder="1"
                        src="PrintPDF.aspx?reportfile=<%=tmpfile%>"></iframe>
                </td>
            </tr>
        </table>
        <%
}
            else
            {
                Response.Write("没有设置批量操作的字段，请在功能设置-按钮处设置");
            }		    
			
        %>
    </div>
    </form>
</body>
</html>
