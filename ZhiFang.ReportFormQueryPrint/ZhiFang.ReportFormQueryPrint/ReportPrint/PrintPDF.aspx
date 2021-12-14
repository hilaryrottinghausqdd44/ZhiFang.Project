<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPDF.aspx.cs" Inherits="ZhiFang.ReportFormQueryPrint.ReportPrint.PrintPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>PDF文件显示打印</title>
   <script language="javascript" type="text/javascript">

       //打印
       function Print(cmdid, cmdexecopt) {
           if (cmdid == '7') {
               parent.daoHangTiao.document.body.style.display = 'none';
               parent.leftFrame.document.body.style.display = 'none';
           }
           document.body.focus();

           try {
               var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';


               document.body.insertAdjacentHTML('BeforeEnd', WebBrowser);
               WebBrowser1.ExecWB(cmdid, cmdexecopt, null, null);
               WebBrowser1.outerHTML = "";


               if (cmdid == 6 && cmdexecopt == 6) {
                   alert('打印完成');
               }

           }
           catch (e) { }
           finally {
               parent.daoHangTiao.document.body.style.display = '';
               parent.leftFrame.document.body.style.display = '';
               //document.all("printmenu").style.display="";
           }
       }



       function PageSetup_Default() {
           try {
               var Wsh = new ActiveXObject("WScript.Shell");
               HKEY_Key = "header";
               Wsh.RegWrite(HKEY_Root + HKEY_Path + HKEY_Key, "aa");
               HKEY_Key = "footer";
               Wsh.RegWrite(HKEY_Root + HKEY_Path + HKEY_Key, "&u&b&d");
           }
           catch (e) { }
       }
       //直接打印pdf
       function PrintPdf() {
           var p = document.getElementById('pdf');
           p.printAll();
       }
       function PrintPdf1() {
           var p = document.getElementById('pdf');
           //           var id = "<%=reportid%>";
           //           if (id.length > 0) {
           //               OA.DBQuery.Print.PrintPDF.UpdatePrintTimes(id, GetCallresult);
           //           }
           p.printAll();
       }
       //回调结果
       function GetCallresult(result) {
       }
       //打印预览
       function PrintPreview() {
           var p = document.getElementById('pdf');
           //p.print();
           p.printWithDialog();
       }
       //判断文件是否存在
       function GetIsNoPdf() {
           var p = document.getElementById('pdf');
           //           p.execCommand();
           if (p)
           { return true; }
           else { return false; }
       }
		</script>
    <style type="text/css">
        #pdf
        {
            height: 433px;
            margin-top: 0px;
        }
    </style>
</head>
<body style="margin:0;padding:0;">
  <form id="form1" runat="server">
  <%if (tmpfilepath != "")
         { %>
           <object classid="clsid:CA8A9780-280D-11CF-A24D-444553540000" 
               style="height: 600px; width: 100%;margin:0;padding:0;" border="0"
           	id="pdf" name="pdf" VIEWASTEXT>
           	<param name="toolbar" value="false"/>
           	<param name="_Version" value="65539"/>
           	<param name="_ExtentX" value="20108"/>
           	<param name="_ExtentY" value="10866"/>
           	<param name="_StockProps" value="0"/>
           	<param onunload="" />

               <param name="SRC" value="../<%=tmpfilepath %>"/>
               
           </object>
       <%} %>
    </form>
</body>
</html>
