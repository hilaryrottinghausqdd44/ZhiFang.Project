<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeblisReportFormQueryPrint.aspx.cs" Inherits="ZhiFang.WebLis.ReportPrint.WeblisReportFormQueryPrint" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Weblis报告查询打印</title>
    <link type="text/css" rel="stylesheet" href="../Css/jyjgcxstyle.css" />
	<script type="text/javascript" src="../js/calendarb.js"></script>
	<script type="text/javascript" src="../js/Tools.js"></script>
	<script type="text/javascript" src="../js/LodopPrinter_weblis.js"></script>
	<script type="text/javascript" src="../js/PrinterOperatorInterface.js"></script>
</head>
<body onload='Search();' >
<object id="LODOP" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width=0 height=0 codebase="../js/lodop.cab#version=6,0,2,3"> 
	<embed id="LODOP_EM" type="application/x-print-lodop" width=0 height=0 pluginspage="../js/install_lodop.exe"></embed>
</object>
    <form id="form1" runat="server">
    <div>
    <input type="hidden" id="tmpshowtypetd" value="ShowFormTypeList0" runat="server" />
    <input type="hidden" id="PageSizeList" value="" runat="server" />
    <input type="hidden" id="ReportFormPageSizeList" value="" runat="server" />
    <input type="hidden" id="PageName" value="TechnicianPrint1.aspx" runat="server" />
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
<div class="searchstyle"><!--#include file="../SearchTools/Date_CName_PatNo.html"--></div>
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
						<th ><div style="font-size:16px; font-weight:bold; margin:5px">报告列表
                               
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
    <table width="100%" height="40" bgcolor="#eaf7fd"  cellspacing="1" cellpadding="0" border="1">
    <tr>
    <td width="100" align="right">Title选择：</td>
    <td width="200">
    <input type="radio" id="ReportFormTitle_center" name="ReportFormTitle" value="Center" checked="checked" />中心
    <input type="radio" id="ReportFormTitle_client" name="ReportFormTitle" value="Client" />医院
    <input type="radio" id="ReportFormTitle_batch" name="ReportFormTitle" value="Batch" />套打
    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td> 
    </tr>
    <tr>
    <td width="100" align="right">合并选择：</td>
    <td width="200">
    <input type="radio" id="Merge_Single" name="Merge" value="A5"  checked="checked"  />不合并
    <input type="radio" id="Merge_Merge" name="Merge" value="A4" />合并
    <input type="radio" id="Merge_MergeEn" name="Merge" value="EA4" />合并英文
    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    </tr>
						<tr >
						
						<td style="width:300px" colspan="2">
						<input onclick ="OpenWindowModal()" type ="button" value ="生成HTML" style="width: 70px; display:none"  />
						<input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"   type ='button' onclick="dlpd(document.URL.substr(0, document.URL.lastIndexOf('/') ).substr(0, document.URL.substr(0, document.URL.lastIndexOf('/')).lastIndexOf('/')+1)+'ReportPrint');" value='打印当前' />
						<input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"   type ="button" onclick="dlp(document.URL.substr(0, document.URL.lastIndexOf('/') ).substr(0, document.URL.substr(0, document.URL.lastIndexOf('/')).lastIndexOf('/')+1)+'ReportPrint');" value ="批量打印" />
						<input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"   type ='button' onclick="dlpp(document.URL.substr(0, document.URL.lastIndexOf('/') ).substr(0, document.URL.substr(0, document.URL.lastIndexOf('/')).lastIndexOf('/')+1)+'ReportPrint');" value='预览打印' /></td>
						<td><input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"  onclick='Report_PrinterSet();' id="Report_PrinterSet_button" type='button' value='报告默认打印机设置'  style="display:none" /></td>
						<td><input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"   onclick='SelectPageSize();' type='button' value='选择打印'  style="display:none" /><div id="ReportPageSizeList" style="display:none" ><input type="checkbox" /></div></td>
						<td>&nbsp;</td>						
						</tr>
					</table>
    </div>
	<div style="Z-INDEX: 1000; POSITION: absolute; WIDTH: 100px; DISPLAY: none; HEIGHT: 100px; " id="ItemListDIV"></div>
	<div style="Z-INDEX: 1000; POSITION: absolute; DISPLAY: none; " class="show" id="Report_PrinterSetDiv" style="display:none"><h3>报告默认打印机设置</h3>
	<div id="Report_PrinterListDiv"></div>
	</div>
    </form>
</body>
</html>
