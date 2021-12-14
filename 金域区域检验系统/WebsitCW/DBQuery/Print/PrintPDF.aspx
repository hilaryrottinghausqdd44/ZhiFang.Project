<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPDF.aspx.cs" Inherits="OA.DBQuery.Print.PrintPDF" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PDF文件显示打印</title>
    <script language="javascript" type="text/javascript">
		
			//打印
			function Print(cmdid,cmdexecopt)
			{
				//document.all("printmenu").style.display="none";
				if(cmdid == '7')
				{
				   parent.daoHangTiao.document.body.style.display='none';
				   parent.leftFrame.document.body.style.display='none';
				}
				document.body.focus();
				
				try
				{
					var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>'; 

                    
					document.body.insertAdjacentHTML('BeforeEnd', WebBrowser); 
					WebBrowser1.ExecWB(cmdid,cmdexecopt,null,null); 
					WebBrowser1.outerHTML = ""; 
					
					
					if(cmdid==6 && cmdexecopt==6)
					{					  
						alert('打印完成');
					}
					
				}
				catch(e){}
				finally
				{
				      parent.daoHangTiao.document.body.style.display='';
				      parent.leftFrame.document.body.style.display='';
					//document.all("printmenu").style.display="";
				}
			}
			  
					
			  
			function PageSetup_Default(){
			try{
					var Wsh=new ActiveXObject("WScript.Shell");
					HKEY_Key="header";
					Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key,"aa");
					HKEY_Key="footer";
					Wsh.RegWrite(HKEY_Root+HKEY_Path+HKEY_Key,"&u&b&d");
				}
			catch(e){}
			}
			//直接打印pdf
			function PrintPdf() 
			{
			    //alert('process');
			    var p= document.getElementById('pdf');			   
			    p.printAll();
			}
			function PrintPdf1() {
			    var p = document.getElementById('pdf');
			    //p.print();
			    var id = "<%=reportid%>";
			    if (id.length > 0) {
			        OA.DBQuery.Print.PrintPDF.UpdatePrintTimes(id, GetCallresult);
			    }
			    p.printAll();
			}
			//回调结果
			function GetCallresult(result) {
			}
			//打印预览
			function PrintPreview() 
			{
			    var p = document.getElementById('pdf');
			    p.print();
			}
			//判断文件是否存在
			function GetIsNoPdf() 
			{
			    var p = document.getElementById('pdf');
			    if (p)
			    { return true; }
			    else {return false;}
 			}
		</script>
</head>
<body>
    <form id="form1" runat="server">       
      
       <table border="0" width="100%" height="100%" style="height:100%" id="tablePDF">
       <%if (tmpfilepath != "")
         { %>
       <tr style=""><td style="">
           <object classid="clsid:CA8A9780-280D-11CF-A24D-444553540000" width="100%" height="100%" border="0"
           	id="pdf" name="pdf" VIEWASTEXT>
           	<param name="toolbar" value="false">
           	<param name="_Version" value="65539">
           	<param name="_ExtentX" value="20108">
           	<param name="_ExtentY" value="10866">
           	<param name="_StockProps" value="0">
           	<param name="SRC" value="PDFReader.aspx?reportfile=<%=tmpfilepath %>">
           </object>

       </td>
       </tr>
       <%} %>
       <tr style="height:15px">
       <td><input type="button" id="btnprintpreview" visible="false" value="打印向导" runat="server" onclick="PrintPreview();" />
       &nbsp;&nbsp;<input type="button" visible="false" id="btnprint" value="直接打印" runat="server" onclick="PrintPdf1();" />
      
       </td></tr></table>
       
    </form>
</body>
</html>
