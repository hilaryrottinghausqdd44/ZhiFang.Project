<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="EmployeeDailyDiary.aspx.cs" Inherits="OA.EmployeeEvents.Employee.EmployeeDailyDiary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>日记</title>
    <link href="../../ModuleManage/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../../Includes/JS/calendarDateTime.js"></script>

    <script type="text/javascript">       
        function openchild(subWindow, subWidth, subHeight) {
            var receiver = window.showModalDialog(subWindow, window, "dialogWidth=" + subWidth + "px;dialogHeight=" + subHeight + "px;help:no;status:no");
            if (receiver == "0") {
                location.href = "ModuleDefault.aspx";
            }
            else {
                //alert('没有接收到父窗体的值');
            }
        }
        function $(s) {
            return document.getElementById ? document.getElementById(s) : document.all[s];
        }       
        //保存提交记录
        function SaveRj(type) {
            //提醒
            var txttx = document.getElementById('txttx').value;
            //日记
            var txtrj = document.getElementById('txtrj').value;
            //代签员工编号
            var uid = document.getElementById("hiddenEmpID").value;
            //代签日期
            var rdate = document.getElementById("txtrdate").value;

            //传入标志
            var misself = document.getElementById("litisSelf").value;
            //传入日期
            var mrdate = document.getElementById("litrdate").value;
            if(mrdate.length <= 0)
            {
                mrdate = document.getElementById("txtToday").value;
            }
            //当前用户编号
            var muid = document.getElementById("lituid").value;
            //传入用户编号
            var msignid = document.getElementById("litsignID").value;
            //记录编号
            var id = document.getElementById("litid").value;
            
            OA.EmployeeEvents.Employee.EmployeeDailyDiary.SaveRj(txttx, txtrj, 'fj', type, uid, rdate, misself, mrdate, muid, msignid, id, GetCallresult);
            
            return false;
        
        }
        //回调结果
        function GetCallresult(result) {
            var r = result.value;
            if (r.indexOf('@') > 0) {
                //保存成功返回记录编号
                var PID = r.substring(r.indexOf('@') + 1);
                document.getElementById('litid').value = PID;
          
                var allFileInfos = document.frames["frmAttaches"].document.all["allFileInfos"].value;
                
                
                if(allFileInfos != null && allFileInfos != "")
                {
                    window.frames["frmAttaches"].saveFileWithPID(PID,allFileInfos);
                }
                
          
                alert(r.substring(0, r.indexOf('@')));
                
            }
            else {
                alert(r);
            }


            var misself = document.getElementById("litisSelf").value.toLowerCase();
            if (misself == "yes") {
                //当是本人时给日期和员工编号赋值便于刷新页面
                document.getElementById('txtrdate').value = document.getElementById('litrdate').value;
                document.getElementById('hiddenEmpID').value = document.getElementById('lituid').value;
            }
            //刷新页面
            var btnname = "<%=btnsearch.ClientID %>";
            document.getElementById(btnname).click();
        }
        
        //选择员工信息
        function chooseEmp() {
            var userID; //用户信息：350|李健
            userID = showModalDialog('../../RBAC/Organizations/searchperson.aspx', '', 'width=700px,height=650px,resizable=yes,left=' + (screen.availWidth - 740) / 2 + ',top=' + (screen.availHeight - 650) / 2);
            var hiddenEmpID = document.getElementById("hiddenEmpID");
            if (userID != null && userID != "") {
                hiddenEmpID.value = userID.substring(0, userID.indexOf('|'));
                
                $('showname').value = userID.substring(userID.indexOf('|') + 1);                
                return true;
            }
            return false;
        }
    var txRowCount;
    var rjRowCount;
    var selectTaRow;
    
    function setUpMaxRow(newRow,obj,textarea)
    {
        var ta = document.getElementById(textarea);
        var o = ta.createTextRange().getClientRects();
        var rowCount;
        if(textarea=='txttx')
	    {
	        txRowCount = newRow;
	        rowCount = txRowCount;
	        if(o.length > newRow)
	        {
	            ta.style.overflow = "";
	        }
	    }
	    else
	    {
	        rjRowCount = newRow;
	        rowCount = rjRowCount;
	        if(o.length > newRow)
	        {
	            ta.style.overflow = "";
	        }
	    }

        if(rowCount == "All")
        {
            var ota = ta.createTextRange().getClientRects();
            ta.rows = ota.length+1;
            ta.style.overflow = "hidden";
        }
        else
        {
            ta.rows = newRow;
        }
        if(selectTaRow != undefined)
        {
            selectTaRow.style.backgroundColor = "";
        }
        obj.style.backgroundColor = "#aabbcc";
        selectTaRow = obj;
    }
    function changTextAreaRow(obj)
    {   
	    var o=obj.createTextRange().getClientRects();  //文本内容占有的行数o.length
	    var row = obj.rows;
	    
	    var rowCount;
	    if(obj.id=='txttx')
	    {
	        rowCount = txRowCount;
	    }
	    else
	    {
	        rowCount = rjRowCount
	    }
	    
	    if(rowCount == "All")
        {
            if(event.keyCode==13)
	        {
	            obj.rows = obj.rows+1;
	        }
            if(o.length == (row+1))
            {
                obj.rows = obj.rows+1;
            }
            return;
        }
        
	    if(event.keyCode==13)
	    {
		    if(row>10)
		    {
			    obj.style.overflow = "";
		    }
		    else
		    {
		        if(o.length == row)
		        {
		            obj.rows = obj.rows+1;
		            row = row+1;
		        }
		    }
	    }
    	else
    	{  
	        if(o.length>row)   
	        {
		        if(row>=10)
		        {
			        obj.style.overflow = "";
		        }
		        else
		        {
			        obj.rows = obj.rows+1;
		        }
	        }
	        else
	        {
		        obj.style.overflow = "hidden";
	        }
	    }
    }
    function chechOverFlow()
    {
        var txttx = document.getElementById("txttx");
        var o=txttx.createTextRange().getClientRects();  //文本内容占有的行数o.length
        if(o.length>10)
        {
            txttx.style.overflow = "";
        }
        var txtrj = document.getElementById("txtrj");
        o=txtrj.createTextRange().getClientRects();  //文本内容占有的行数o.length
        if(o.length>10)
        {
            txtrj.style.overflow = "";
        }
    }
    </script>

    <style type="text/css">
        .iframe111
        {
            border: 0;
            width: 100%;
            height: 60px;
            margin: 0;
            overflow: auto;
            padding: 0;
        }
        textarea
        {
            width: 100%;
            border-style: groove;
        }
        .rowSet
        {
            text-decoration: underline;
            cursor: pointer;
        }
    </style>
</head>
<body onload="chechOverFlow();">
    <form id="form1" runat="server">
    <div style="text-align: right;">
        <img src="../../Images/diary/pre.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.history.go(-1);" alt="前一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/next.gif" onmouseover="this.style.cursor='pointer'" onclick="window.history.go(1);" alt="后一页" width="60" height="24" />&nbsp;
        <img src="../../Images/diary/main.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.parent.location=window.parent.location;" alt="主页" width="60" height="24" />&nbsp;
    </div>
    <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff" cellpadding="10" width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
        <tbody>
            <tr>
                <td>
                    <asp:ImageButton ID="preDay" ImageUrl="../../Images/diary/left.JPG" ToolTip="昨天" runat="server" BorderStyle="None" OnClick="preDay_Click" />
                    <asp:TextBox ID="txtToday" onfocus="setday(this)" runat="server" OnTextChanged="txtToday_TextChanged"></asp:TextBox>
                    <asp:ImageButton ID="nextDay" ImageUrl="../../Images/diary/right.JPG" ToolTip="明天" runat="server" BorderStyle="None" OnClick="nextDay_Click" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table id="table1" style="display: none;">
                        <tr>
                            <td>
                                <input type="button" id="btnselect" value="员工选择" onclick="chooseEmp();" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" contentEditable="false" Width="50px" ID="showname"></asp:TextBox>
                                <asp:HiddenField ID="hiddenEmpID" runat="server" />
                            </td>
                            <td>
                                日期:<input type="text" id="txtrdate" runat="server" onfocus="setday(this)" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnsearch" Text="查询" OnClick="btnsearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    提醒: <span class="rowSet" onclick="setUpMaxRow('5',this,'txttx');">5</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('10',this,'txttx');">10</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('20',this,'txttx');">20</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('30',this,'txttx');">30</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('All',this,'txttx');">全部</span><br />
                    <textarea id="txttx" runat="server" onkeydown="changTextAreaRow(this);" style="overflow: hidden; width: 99%" name="textarea" rows="10" cols="50"></textarea>
                </td>
            </tr>
            <tr valign="top">
                <td align="left">
                    日记: <span class="rowSet" onclick="setUpMaxRow('5',this,'txtrj');">5</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('10',this,'txtrj');">10</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('20',this,'txtrj');">20</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('30',this,'txtrj');">30</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('All',this,'txtrj');">全部</span><br />
                    <textarea id="txtrj" runat="server" onkeydown="changTextAreaRow(this);" name="textarea" rows="10" style="overflow: hidden; width: 99%" cols="50"></textarea>
                </td>
            </tr>
            <tr valign="top">
                <td style="margin: 0px; padding: 0px;">
                    <div id="div2">
                        <iframe id="attach2" class="iframe111" name="frmAttaches" frameborder="no" runat="server"></iframe>
                    </div>
                </td>
            </tr>
            <tr valign="top">
                <td align="right">
                    <input type="button" id="btnSave" runat="server" onclick="SaveRj('1');" value="保  存" />
                    &nbsp;&nbsp;
                    <input type="button" id="btnSubmit" runat="server" onclick="SaveRj('2');" value="提  交" /><!-- class="buttonstyle" -->
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="litrdate" runat="server" />
    <input type="hidden" id="litisSelf" value="yes" runat="server" />
    <input type="hidden" id="lituid" runat="server" />
    <input type="hidden" id="litsignID" runat="server" />
    <input type="hidden" id="litid" runat="server" />
    </form>
</body>
</html>
