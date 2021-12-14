<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TechnicianPrint.aspx.cs" Inherits="ZhiFang.WebLis.TechnicianPrint" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技师站打印</title>
    <link type="text/css" rel="stylesheet" href="../Css/jyjgcxstyle.css" />
	<script type="text/javascript" src="../js/calendarb.js"></script>
	<script type="text/javascript" src="../js/Tools.js"></script>
	<script type="text/javascript" src="../js/LodopPrinter.js"></script>
	<script type="text/javascript" src="../js/PrinterOperatorInterface.js"></script>
	<script type="text/javascript" src="../js/Station_Report_Printer.js"></script>
	<script language="javascript">
	    function onloadfunction() {
	        var urlall = location.href;
	        var urla = urlall.split('?');
	        var paramat;
	        if (urla.length > 1) {
	            QuickSearch(urla[1]);
	        }
	    }	
	</script>
</head>
<body onload="onloadfunction()">
    <form id="form1" runat="server">
    <div>
    <input type="hidden" id="tmpshowtypetd" value="ShowFormTypeList0" runat="server" />
    <input type="hidden" id="PageSizeList" value="" runat="server" />
    <input type="hidden" id="ReportFormPageSizeList" value="" runat="server" />
    <input type="hidden" id="PageName" value="TechnicianPrint.aspx" runat="server" />
    <input type="hidden" id="tmpclassid" value="0" runat="server" />
    <input type="hidden" id="PIndex" value="0" runat="server" />
    <input type="hidden" id="CheckBoxFlag" value="0" runat="server" />
    <input type="hidden" id="OrderColumn" value="" runat="server" />
    <input type="hidden" id="OrderColumnFlag" value="asc" runat="server" />
<div class="topstyle">
    <div class="font24"></div>
    <div class="topstyler"></div>
    <div style="clear:both"></div>
</div>
<div class="searchstyle"><!--#include file="../SearchTools/Normal.html"--></div>
<div class="contentstyle">
<div class="contleft">
<div id="showclasslist" runat="server"></div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                        <ContentTemplate>
                        <asp:Button  ID="Button1" style="display:none" runat="server" onclick="Button1_Click" Text="Button" />
                        <asp:Button ID="Button2"  style="display:none" runat="server" Text="Button" onclick="Button2_Click"></asp:Button>
       <asp:Button ID="Button3" style="display:none" runat="server" Text="Button" onclick="Button3_Click" />
                        <asp:LinkButton ID="linkGetAllFrom" style="display:none" runat="server" OnClick="linkGetAllItem_Click">
                        </asp:LinkButton><input id="FirstFormNo" type="hidden" value="" runat="server" /><input id="tmpcondition" type="hidden" value="" runat="server" />
                <div class="contlefttableout">
				<table width="100%" cellspacing="1" cellpadding="0" border="0" class="contlefttable">                    
                      <tr style="background-color:#FFFFFF">
						<th ><div style="font-size:16px; font-weight:bold; margin:10px">报告列表
                               
                            </div></th>
						</tr>
						<tr class='tablehead'><td ><div id="tablehead" runat="server" ></div></td></tr>                         
						<tr>
    <td style="background-color:#FFFFFF">
    <div class="scrollTable" >                       
                           <div id="tablelist" runat="server"></div>
                        </div>
                    </td>
                    </tr>
                </table></div>
                </ContentTemplate>
                        </asp:UpdatePanel>
</div>
<div class="contright"> <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                        <ContentTemplate>
                        <div id="ShowFormTypeList" runat="server"  ></div>
            <div id="sT1" class="scrollTable1">
            <div id="ShowFormHtml" runat="server" class="table2style" ></div>
            </div>
               </ContentTemplate>
                        </asp:UpdatePanel> 
						</div>
    </div>
	<div style="Z-INDEX: 1000; POSITION: absolute; WIDTH: 100px; DISPLAY: none; HEIGHT: 100px; " id="ItemListDIV"></div>
	<div style="Z-INDEX: 1000; POSITION: absolute; DISPLAY: none; " class="show" id="Report_PrinterSetDiv"><h3>报告默认打印机设置</h3>
	<div id="Report_PrinterListDiv"></div>
	</div>
    </form>
</body>
</html>