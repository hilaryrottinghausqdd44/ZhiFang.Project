<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="import_E.aspx.cs" Inherits="OA.RBAC.Organizations.import_E" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../Includes/CSS/ioffice.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #Spreadsheet1
        {
            height: 398px;
            width: 100%;
        }
        .hideText
        {
        	display:none;
        	width:0px;
        }
        .style1
        {
            color: #999999;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function window_onload() {

        }
        function chkForm() {
            if (! window.confirm('导入数据可能要花比较长的时间,确认要导入吗?')) {
                return false;
            }
            //alert(document.getElementById('Spreadsheet1'));
            var tSheetObj = document.getElementById('Spreadsheet1').ActiveSheet;
            var tUsedRows = tSheetObj.UsedRange.EntireRow.Count;
            var tUsedCols = tSheetObj.UsedRange.EntireColumn.Count;
            //            alert(tUsedRows);display:none;
//            alert(tUsedCols);
//            return false;
            var strXML = "";
            var cellValue = "";
            for (var tRow = 1; tRow <= tUsedRows; tRow++) {
                for (var tCol = 1; tCol <= tUsedCols; tCol++) {
                    cellValue = "";
                    try{
                        if (tSheetObj.Cells(tRow, tCol)) {
                            cellValue = tSheetObj.Cells(tRow, tCol).value;
                        }

                        if (cellValue.indexOf(",") >= 0 || cellValue.indexOf("\t") >= 0) {
                            alert('导入数据中包含,[逗号]暂时不支持,请去掉EXCEL中的[逗号]后再进行导入');
                            return false;
                        }
                    }
                    catch(e){
                        cellValue = "";
                    }
                    strXML += cellValue + ",";
                }
                strXML += "\t";
            }
            //alert(strXML);
            if (document.getElementById("HiddenExcel")) {
                document.getElementById("HiddenExcel").value = strXML;
            }
//            else if (document.all["HiddenExcel"]) {
//                document.all["HiddenExcel"].value = strXML;
//            }
            //document.getElementById("HiddenExcel").value = strXML;
            
            return true;
        }
    </script>
</head>
<body onload="return window_onload()">
    <form id="form1" runat="server" onsubmit="return chkForm()">
    <div>
    
        <asp:CheckBox ID="CheckBoxSN99" runat="server" Checked="True" 
            Text="按地区(分类)创建分级组织机构(每级最多99个部门)" />
        <br />
        <asp:CheckBox ID="CheckBoxDeptF" runat="server" Checked="True" 
            Text="按单位或名称先创建部门" />
        <br />
        <asp:TextBox ID="TextBoxDeptID" runat="server"></asp:TextBox>
        <asp:TextBox ID="HiddenExcel" runat="server" CssClass="hideText"></asp:TextBox>
        &nbsp;请选手工指定要导入的上级机构编号ID 如(0), 如(327), 去组织机构中右键查一下机构的ID<br />
        <span class="style1">双击指定导入的上级机构</span>(如只往第二级部门内导入,可不指定, 直接导入为第一级部门<br />
        <asp:Button ID="ButtonImport" runat="server" Text="导入" 
            onclick="ButtonImport_Click" />
&nbsp;注意:下面如果不能显示EXCEL2003控件,请<a href="../../Includes/Office/Default.htm" target=_blank>安装</a>;请先在EXCEL文件中编辑,再粘贴到下面的控件中<br />
    
    </div>
<table style="width:100%;">
    <tr>
        <td colspan="3">
            <OBJECT id="Spreadsheet1" classid="clsid:0002E559-0000-0000-C000-000000000046" 
                VIEWASTEXT>
			<PARAM NAME="HTMLURL" VALUE="">
			<PARAM NAME="HTMLData" VALUE="&lt;html xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;
xmlns=&quot;http://www.w3.org/TR/REC-html40&quot;&gt;
 
&lt;head&gt;
&lt;style type=&quot;text/css&quot;&gt;
&lt;!--tr
	{mso-height-source:auto;}
td
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:11pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc03C4B2F8
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:General;
	font-size:11pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
.wc03C43A3C
	{white-space:nowrap;
	font-family:宋体;
	mso-number-format:&quot;\@&quot;;
	font-size:12pt;
	font-weight:auto;
	font-style:auto;
	text-decoration:auto;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-color-source:auto;
	text-align:general;
	vertical-align:middle;
	border-top:none;
	border-left:none;
	border-right:none;
	border-bottom:none;
	mso-protection:locked;}
--&gt;
&lt;/style&gt;
&lt;/head&gt;
 
&lt;body&gt;
&lt;!--[if gte mso 9]&gt;&lt;xml&gt;
 &lt;x:ExcelWorkbook&gt;
  &lt;x:ExcelWorksheets&gt;
   &lt;x:ExcelWorksheet&gt;
    &lt;x:OWCVersion&gt;11.0.0.8304         &lt;/x:OWCVersion&gt;
    &lt;x:Label Style='font-family:宋体;font-size:11pt;border-top:solid .5pt silver;
     border-left:solid .5pt silver;border-right:solid .5pt silver;border-bottom:
     solid .5pt silver'&gt;
     &lt;x:Caption&gt;Microsoft Office 电子表格&lt;/x:Caption&gt;
    &lt;/x:Label&gt;
    &lt;x:Name&gt;Sheet&lt;/x:Name&gt;
    &lt;x:WorksheetOptions&gt;
     &lt;x:Selected/&gt;
     &lt;x:Height&gt;12039&lt;/x:Height&gt;
     &lt;x:Width&gt;23548&lt;/x:Width&gt;
     &lt;x:DisableUndo/&gt;
     &lt;x:DoNotDisplayOfficeLogo/&gt;
     &lt;x:ViewableRange&gt;$1:$65536&lt;/x:ViewableRange&gt;
     &lt;x:Selection&gt;1:1&lt;/x:Selection&gt;
     &lt;x:TopRowVisible&gt;0&lt;/x:TopRowVisible&gt;
     &lt;x:LeftColumnVisible&gt;4&lt;/x:LeftColumnVisible&gt;
     &lt;x:ProtectContents&gt;False&lt;/x:ProtectContents&gt;
     &lt;x:DefaultRowHeight&gt;255&lt;/x:DefaultRowHeight&gt;
     &lt;x:StandardWidth&gt;2304&lt;/x:StandardWidth&gt;
    &lt;/x:WorksheetOptions&gt;
   &lt;/x:ExcelWorksheet&gt;
  &lt;/x:ExcelWorksheets&gt;
  &lt;x:MaxHeight&gt;80%&lt;/x:MaxHeight&gt;
  &lt;x:MaxWidth&gt;80%&lt;/x:MaxWidth&gt;
 &lt;/x:ExcelWorkbook&gt;
&lt;/xml&gt;&lt;![endif]--&gt;
 
&lt;table class=wc03C4B2F8 cellpadding=0 cellspacing=0 x:str&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;col width=&quot;72&quot;&gt;
 &lt;tr class=wc03C43A3C height=&quot;19&quot; style='mso-height-source:userset;mso-height-alt:
  285'&gt;
  &lt;td class=wc03C43A3C x:str=&quot;编码&quot;&gt;编码&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;单位或名称&quot;&gt;单位或名称&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;科室&quot;&gt;科室&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;地区&quot;&gt;地区&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;地区代码&quot;&gt;地区代码&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;地址&quot;&gt;地址&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;邮编&quot;&gt;邮编&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;电话&quot;&gt;电话&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;传真&quot;&gt;传真&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;联系人&quot;&gt;联系人&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;收信人&quot;&gt;收信人&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;EMail&quot;&gt;EMail&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;医院等级&quot;&gt;医院等级&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;修改日期&quot;&gt;修改日期&lt;/td&gt;
  &lt;td class=wc03C43A3C x:str=&quot;手机&quot;&gt;手机&lt;/td&gt;
 &lt;/tr&gt;
&lt;/table&gt;
 
&lt;/body&gt;
 
&lt;/html&gt;
"
			<PARAM NAME="DataType" VALUE="HTMLDATA">
			<PARAM NAME="AutoFit" VALUE="0">
			<PARAM NAME="DisplayColHeaders" VALUE="-1">
			<PARAM NAME="DisplayGridlines" VALUE="-1">
			<PARAM NAME="DisplayHorizontalScrollBar" VALUE="-1">
			<PARAM NAME="DisplayRowHeaders" VALUE="-1">
			<PARAM NAME="DisplayTitleBar" VALUE="-1">
			<PARAM NAME="DisplayToolbar" VALUE="-1">
			<PARAM NAME="DisplayVerticalScrollBar" VALUE="-1">
			<PARAM NAME="EnableAutoCalculate" VALUE="-1">
			<PARAM NAME="EnableEvents" VALUE="-1">
			<PARAM NAME="MoveAfterReturn" VALUE="-1">
			<PARAM NAME="MoveAfterReturnDirection" VALUE="0">
			<PARAM NAME="RightToLeft" VALUE="0">
			<PARAM NAME="ViewableRange" VALUE="1:65536">
		</OBJECT>
</td>
    </tr>
    <tr>
        <td>
            <br />
            导入数据情况提示在下面:</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            
            <asp:TextBox ID="TextBoxArea" runat="server" Height="176px" ReadOnly="True" 
                TextMode="MultiLine" Width="726px"></asp:TextBox>
            
        </td>
    </tr>
</table>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="清理全部数据" />
&nbsp;<asp:CheckBox ID="CheckBoxDelDept" runat="server" Text="同时删除部门与机构表" />
    </form>
    <p>
        注意,此[清理全部数据将执行以下功能]</p>
    <p>
        1, 删除全部部门 [可选]</p>
    <p>
        2, 删除全部员工</p>
    <p>
        3, 删除全部帐号 (admin 超级管理员除外)</p>
    <p>
        4, 删除员工与部门所属关系</p>
    <p>
        5, 删除所有角色的模块权限</p>
    <p>
        &nbsp;</p>
</body>
</html>
