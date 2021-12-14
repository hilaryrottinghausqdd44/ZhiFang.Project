<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePlans.aspx.cs" Inherits="OA.EmployeeEvents.Employee.EmployeePlans" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>计划于总结</title>
    <style type="text/css">
        BODY
        {
            font-size: 12px;
        }
        table
        {
            width: 98%;
            border: 1;
            border-width: 1px;
        }
        iframe
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
        .imgBtn
        {
            width: 55px;
            height: 19px;
            border: 0;
            cursor: pointer;
        }
        .size
        {
            font-size: 14px;
            font-weight: bold;
        }
        .rowSet
        {
            text-decoration: underline;
            cursor: pointer;
        }
    </style>

    <script type="text/javascript">
    var subPID = "";
    function save()
    {
        var strSavePlanKind = document.getElementById("ddlPlanClass").value;
        var strSavePlanClass = document.getElementById("ddlTimeClass").value;
        var strSaveContents = document.getElementById("textareaInPutBody").value;
        var signID = document.getElementById("hiddenEmpID").value;
        var loginID = document.getElementById("hiddenLoginID").value;
        var hiddenPC = document.getElementById("hiddenPC").value;
        var hiddenSignYear = document.getElementById("hiddenSignYear").value;
        
        if(strSaveContents == "")
        {
            alert("总结或计划内容不能为空！");
            return false;
        }
        
        var PID = OA.EmployeeEvents.Employee.EmployeePlans.save(strSavePlanKind,strSavePlanClass,strSaveContents,signID,loginID,hiddenPC,hiddenSignYear);
        subPID = PID.value;
        if(subPID == "" || subPID == "Error")
        {
            alert("保存失败！");
            return false;
        }
        var allFileInfos = document.frames["frmAttaches"].document.all["allFileInfos"].value;

        if(allFileInfos != null && allFileInfos != "")
        {
            window.frames["frmAttaches"].saveFileWithPID(subPID,allFileInfos);
        }
        alert("保存成功！");
    }
    function sub()
    {
        var strSavePlanKind = document.getElementById("ddlPlanClass").value;
        var strSavePlanClass = document.getElementById("ddlTimeClass").value;
        var strSaveContents = document.getElementById("textareaInPutBody").value;
        var signID = document.getElementById("hiddenEmpID").value;
        var loginID = document.getElementById("hiddenLoginID").value;
        var hiddenPC = document.getElementById("hiddenPC").value;
        var hiddenSignYear = document.getElementById("hiddenSignYear").value;
        
        if(strSaveContents == "")
        {
            alert("总结或计划内容不能为空！");
            return false;
        }
        
        var backValues = OA.EmployeeEvents.Employee.EmployeePlans.sub(strSavePlanKind,strSavePlanClass,strSaveContents,signID,loginID,hiddenPC,hiddenSignYear);
        
        if(backValues.value == "")
        {
            alert("提交失败！");
            return false;
        }
        var allFileInfos = document.frames["frmAttaches"].document.all["allFileInfos"].value;

        alert(allFileInfos);

        if(allFileInfos != null && allFileInfos != "")
        {
            window.frames["frmAttaches"].saveFileWithPID(backValues.value,allFileInfos);
        }
        
        //OA.EmployeeEvents.Employee.EmployeePlans.sub(strSavePlanKind,strSavePlanClass,strSaveContents);
        
        var btnSave = document.getElementById("btnSave");
        btnSave.disabled = true;
        var btnSub = document.getElementById("btnSub");
        btnSub.disabled = true;
        var textareaInPutBody = document.getElementById("textareaInPutBody");
        textareaInPutBody.readOnly = true; 
        
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
    
    
    function showAttach()
    {
        var attach1 = window.frames["attach1"].document.all["fileList"].innerHTML;
        if(attach1 == null || attach1 == "")
        {
            var div1 = document.getElementById("div1");
            div1.style.display = "none";
        }
        else
        {
            var scanBtn = window.frames["attach1"].document.getElementById("imgBtnScan");
            if(scanBtn != undefined)
            {
                scanBtn.style.display = "none";
            }
        }
        var textareaInPutBody = document.getElementById("textareaInPutBody");
        var o=textareaInPutBody.createTextRange().getClientRects();  //文本内容占有的行数o.length
        if(o.length > textareaInPutBody.rows)
        {
            textareaInPutBody.style.overflow = "auto";
        }
        textareaInPutBody.focus = true;
        //=======================================================================================================
        var textareaBody1 = document.getElementById("textareaBody1");
        o=textareaBody1.createTextRange().getClientRects();  //文本内容占有的行数o.length
        if(o.length>5)
        {
            textareaBody1.style.overflow = "";
        }
        var labelBody2 = document.getElementById("labelBody2");
        o=labelBody2.createTextRange().getClientRects();  //文本内容占有的行数o.length
        if(o.length>5)
        {
            labelBody2.style.overflow = "";
        }
    }
    function chooseEmp()
    {
        var userID;//用户信息：350|李健
        userID = showModalDialog('../../RBAC/Organizations/searchperson.aspx','','width=700px,height=650px,resizable=yes,left=' + (screen.availWidth-740)/2 + ',top=' + (screen.availHeight-650)/2 );
        if(userID!=null && userID!="")
        {
            var signID = OA.EmployeeEvents.Employee.EmployeePlans.chooseEmployee(userID);
            var btnSave = document.getElementById("btnSave");
            btnSave.disabled = false;
            var btnSub = document.getElementById("btnSub");
            btnSub.disabled = false;
            document.getElementById("hiddenEmpID").value = signID;
        }
        return false;
    }
    </script>

</head>
<body onload="showAttach();">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="left">
                    <asp:DropDownList ID="ddlPlanClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlanClass_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlTimeClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTimeClass_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnChooseEmp" Text="员工选择" runat="server" />
                </td>
                <td align="right">
                    <img src="../../Images/diary/pre.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.history.go(-1);" alt="前一页" width="60" height="24" />&nbsp;
                    <img src="../../Images/diary/next.gif" onmouseover="this.style.cursor='pointer'" onclick="window.history.go(1);" alt="后一页" width="60" height="24" />&nbsp;
                    <img src="../../Images/diary/main.gif" onmouseover="this.style.cursor='pointer'" onclick="javascript:window.parent.location=window.parent.location;" alt="主页" width="60" height="24" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="1" cellpadding="0" width="99%" cellspacing="0" id="headTable" class="headTable">
            <tr align="center" valign="middle" height="65">
                <td width="25%">
                    <asp:Label ID="labelFirstDate" runat="server"></asp:Label>
                    <asp:Button ID="btnLastZJ" Text="总结" runat="server" OnClick="btnLastZJ_Click" />
                </td>
                <td width="25%" style="background-image: url(../../Images/diary/bg-0106.gif)">
                    <asp:Label ID="labelCurDate" runat="server"></asp:Label>
                    <asp:Button ID="btnCurJH" Text="计划" runat="server" OnClick="btnCurJH_Click" />
                    <asp:Button ID="btnCurZJ" Text="总结" runat="server" OnClick="btnCurZJ_Click" />
                </td>
                <td width="25%">
                    <asp:Label ID="labelNextDate" runat="server"></asp:Label>
                    <asp:Button ID="btnNextJH" Text="计划" runat="server" OnClick="btnNextJH_Click" />
                </td>
                <td width="25%">
                    <table>
                        <tr>
                            <td width="40%">
                                未录入：
                            </td>
                            <td bgcolor="Red">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                未提交：
                            </td>
                            <td bgcolor="Blue">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                已提交：
                            </td>
                            <td bgcolor="green">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                已审批：
                            </td>
                            <td bgcolor="yellow">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td align="left">
                    <span class="size">
                        <asp:Label ID="labelTitle1" runat="server"></asp:Label>：
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="rowSet" onclick="setUpMaxRow('5',this,'textareaBody1');">5</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('10',this,'textareaBody1');">10</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('20',this,'textareaBody1');">20</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('30',this,'textareaBody1');">30</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('All',this,'textareaBody1');">全部</span><br />
                    <textarea ID="textareaBody1" readonly="readonly" style="overflow: hidden; width: 99%" rows="5" cols="50" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1">
                        <iframe id="attach1" name="attach1" frameborder="no" runat="server"></iframe>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="size">主管审核意见：</span><br />
                    <span class="rowSet" onclick="setUpMaxRow('5',this,'labelBody2');">5</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('10',this,'labelBody2');">10</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('20',this,'labelBody2');">20</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('30',this,'labelBody2');">30</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('All',this,'labelBody2');">全部</span><br />
                    <textarea ID="labelBody2" readonly="readonly" style="overflow: hidden; width: 99%" rows="5" cols="50" runat="server"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <span class="size">
                        <asp:Label ID="labelInputTitle" runat="server"></asp:Label>：</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="rowSet" onclick="setUpMaxRow('5',this,'textareaInPutBody');">5</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('10',this,'textareaInPutBody');">10</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('20',this,'textareaInPutBody');">20</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('30',this,'textareaInPutBody');">30</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('All',this,'textareaInPutBody');">全部</span><br />
                    <textarea id="textareaInPutBody" onkeydown="changTextAreaRow(this);" style="overflow: hidden; width: 99%" name="textarea" rows="10" cols="50" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td bgcolor="#aabbcc" height="20px;">
                    <div id="div2">
                        <iframe id="attach2" name="frmAttaches" frameborder="no" runat="server"></iframe>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td align="right">
                    <input id="btnSave" type="button" value="保存" onclick="save();" runat="server" />
                    <input id="btnSub" type="button" value="提交" onclick="sub();" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiddenEmpID" runat="server" /><!-- signID -->
    <asp:HiddenField ID="hiddenLoginID" runat="server" /><!-- loginID -->
    <asp:HiddenField ID="hiddenPC" Value="2" runat="server" /><!-- boolPC:计划1，总结2 -->
    <input type="hidden" id="hiddenSignNo" runat="server" /><!-- SignNo -->
    <input type="hidden" id="hiddenSignYear" runat="server" /><!-- SignYear -->
    </form>
</body>
</html>
