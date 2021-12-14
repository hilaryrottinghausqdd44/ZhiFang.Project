<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPrint_Weblis_Fr3.aspx.cs"
    Inherits="ZhiFang.WebLis.ReportPrint.ReportPrint_Weblis_Fr3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Weblis报告查询打印</title>
    <link type="text/css" rel="stylesheet" href="../Css/jyjgcxstyle.css" />
    <script type="text/javascript" src="../js/calendarb.js"></script>
    <script type="text/javascript" src="../js/Tools.js"></script>
    <script type="text/javascript" src="../js/Print_fr3.js"></script>
    <script type="text/javascript" src="../js/PrinterOperatorInterface.js"></script>
    <script type="text/javascript" language="javascript">
        //         function Init() {
        //             window.resizeTo(window.screen.width, window.screen.height);
        //             window.moveTo(0, 0);
        //             //            parent.parent.document.getElementById("MainList").rows = "0,100%";
        //             //            
        //             //            parent.document.getElementById("fset").cols = "0,0,100%";

        //         }
        function DeleteReportForm() {
            var Fromno = GetCheckBoxValue('FromNoCheckBox');
            if (String(Fromno) == "") {
                alert('请选择要删除的报告单！');
                return false;
            }
            else {
                if (confirm("删除操作不可恢复！")) {
                    ZhiFang.WebLis.Ashx.ReportPrint.DeleteReportFormByReportFormID(Fromno, callback_DeleteReportForm);                   
                }
            }
        }
        function callback_DeleteReportForm(result) {
            if (result != null && result.value != null) {
                alert(result.value);
                var o = document.getElementById('linkGetAllFrom');
                o.click();
            }
        }
        function ReportExcel() {
            
            var o = document.getElementById('LinkExcel');
                o.click();
        }

        function CheckSuperAccount() {
            ZhiFang.WebLis.Ashx.ReportPrint.CheckSuperAccount(callback_CheckSuperAccount);
        }
        function callback_CheckSuperAccount(result) {
            if (result != null && result.value != null) {
                if (result.value != 'true') {
                    document.getElementById('btn_Del_Reportform').style.display = 'none';
                }
            }
        }
    </script>
</head>
<body onload="CheckSuperAccount();">
    <form id="form1" runat="server">
    <div id="111">
        <input type="hidden" id="tmpshowtypetd" value="ShowFormTypeList0" runat="server" />
        <input type="hidden" id="PageSizeList" value="" runat="server" />
        <input type="hidden" id="ReportFormPageSizeList" value="" runat="server" />
        <input type="hidden" id="PageName" value="TechnicianPrint1.aspx" runat="server" />
        <input type="hidden" id="tmpclassid" value="1" runat="server" />
        <input type="hidden" id="PIndex" value="0" runat="server" />
        <input type="hidden" id="CheckBoxFlag" value="0" runat="server" />
        <input type="hidden" id="OrderColumn" value="" runat="server" />
        <input type="hidden" id="OrderColumnFlag" value="desc" runat="server" />
        <div class="topstyle" style="display:none">
            <div class="font24">
            </div>
            <div class="topstyler">
            </div>
            <div style="clear: both">
            </div>
        </div>
        <div class="searchstyle">
            <!--#include file="../SearchTools/DaJia_Weblis.html"-->
        </div>
        <div class="contentstyle">
            <div class="contleft">
                <div id="showclasslist" runat="server">
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button1" Style="display: none" runat="server" OnClick="Button1_Click"
                            Text="Button" />
                        <asp:Button ID="Button2" Style="display: none" runat="server" Text="Button" OnClick="Button2_Click">
                        </asp:Button>
                        <asp:Button ID="Button3" Style="display: none" runat="server" Text="Button" OnClick="Button3_Click" />
                        
                        <asp:LinkButton ID="linkGetAllFrom" Style="display: none" runat="server" OnClick="linkGetAllItem_Click">
                        </asp:LinkButton><input id="FirstFormNo" type="hidden" value="" runat="server" /><input
                            id="tmpcondition" type="hidden" value="" runat="server" />
                        <div class="contlefttableout">
                            <table width="100%" cellspacing="1" cellpadding="0" border="0" class="contlefttable">
                                <tr style="background-color: #FFFFFF">
                                    <th>
                                        <div style="font-size: 16px; font-weight: bold; margin: 5px">
                                            报告列表
                                        </div>
                                    </th>
                                </tr>
                                <tr class='tablehead'>
                                    <td>
                                        <div id="tablehead" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #FFFFFF">
                                        <div class="scrollTable">
                                            <div id="tablelist" runat="server">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="contright">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="ShowFormTypeList" runat="server">
                        </div>
                        <div id="sT1" class="scrollTable1">
                            <div id="ShowFormHtml" runat="server" class="table2style">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <table width="100%" height="40" bgcolor="#eaf7fd" cellspacing="1" cellpadding="0"
                border="1">
                <tr>
                    <td width="100" align="right">
                        Title选择：
                    </td>
                    <td width="200">
                        <input type="radio" id="ReportFormTitle_center" name="ReportFormTitle" value="Center"
                            checked="checked" />中心
                        <input type="radio" id="ReportFormTitle_client" name="ReportFormTitle" value="Client" />医院
                        <input type="radio" id="ReportFormTitle_batch" name="ReportFormTitle" value="Batch" />套打
                        <input type="radio" id="ReportFormTitle_test" name="ReportFormTitle" value="TiJian" />体检
                    </td>
                    <td width="100" align="right">
                        合并选择：
                    </td>
                    <td width="200">
                        <input type="radio" id="Merge_Single" name="Merge" value="A5" checked="checked" />不合并
                        <input type="radio" id="Merge_Merge" name="Merge" value="A4" />合并
                        <input type="radio" id="Merge_MergeEn" name="Merge" value="EA4" />合并英文
                    </td>
                    <td width="100" align="right">
                        纸张选择：
                    </td>
                    <td width="200">
                        <input type="radio" id="PageSize_A5" name="PageSize" value="A5" checked="checked" />A5
                        <input type="radio" id="PageSize_A4" name="PageSize" value="A4" />A4
                    </td>
                    <td width="100" align="right">
                        打印方向：
                    </td>
                    <td width="200">
                        <input type="radio" id="Print_horizontal" name="Print_direction" value="1" checked="checked" />纵打
                        <input type="radio" id="Print_vertical" name="Print_direction" value="2" />横打
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px" colspan="3">
                        <input onclick="OpenWindowModal()" type="button" value="生成HTML" style="width: 70px;
                            display: none" />
                        <input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"
                            type='button' onclick="dlpd(document.URL.substr(0, document.URL.lastIndexOf('/') ).substr(0, document.URL.substr(0, document.URL.lastIndexOf('/')).lastIndexOf('/')+1)+'ReportPrint');"
                            value='打印当前' />
                        <input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"
                            type="button" onclick="dlp(document.URL.substr(0, document.URL.lastIndexOf('/') ).substr(0, document.URL.substr(0, document.URL.lastIndexOf('/')).lastIndexOf('/')+1)+'ReportPrint');"
                            value="批量打印" />
                        <input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"
                            type='button' onclick="dlpp(document.URL.substr(0, document.URL.lastIndexOf('/') ).substr(0, document.URL.substr(0, document.URL.lastIndexOf('/')).lastIndexOf('/')+1)+'ReportPrint');"
                            value='预览打印' />
                        <input id='btn_Del_Reportform' class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"
                            type='button' onclick="DeleteReportForm();" value='删除' />
                            <%--<input id='Button4' class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"
                            type='button' onclick="ReportExcel();" value='导出Excel' />--%><asp:LinkButton ID="LinkExcel" runat="server" Style="display: none" onclick="LinkExcel_Click"></asp:LinkButton>
                            <input id="strwhere" type="hidden" value="" runat="server" />
                    </td>
                    <td>
                        <input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"
                            onclick='Report_PrinterSet();' id="Report_PrinterSet_button" type='button' value='报告默认打印机设置'
                            style="display: none" />
                    </td>
                    <td>
                        <input class="btn1_mouseout" onmouseover="this.className='btn1_mouseover'" onmouseout="this.className='btn1_mouseout'"
                            onclick='SelectPageSize();' type='button' value='选择打印' style="display: none" /><div
                                id="ReportPageSizeList" style="display: none">
                                <input type="checkbox" /></div>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div style="z-index: 1000; position: absolute; width: 100px; display: none; height: 100px;"
            id="ItemListDIV">
        </div>
        <div style="z-index: 1000; position: absolute; display: none;" class="show" id="Report_PrinterSetDiv"
            style="display: none">
            <h3>
                报告默认打印机设置</h3>
            <div id="Report_PrinterListDiv">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
