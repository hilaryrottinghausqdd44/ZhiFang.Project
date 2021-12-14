<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputReplyMsg.aspx.cs" Inherits="OA.News.publish.InputReplyMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>回复</title>
    <base target="_self">
    </base>
    <style type="text/css">
        table
        {
            width: 98%;
        }
        .rowSet
        {
            cursor: pointer;
        }
    </style>

    <script type="text/javascript">
    var rjRowCount;
    var selectTaRow;
    
    function setUpMaxRow(newRow,obj,textarea)
    {
        var ta = document.getElementById(textarea);
        var o = ta.createTextRange().getClientRects();
        var rowCount;

        rjRowCount = newRow;
        rowCount = rjRowCount;
        if(o.length > newRow)
        {
            ta.style.overflow = "";
        }

        if(rowCount == "All")
        {
            var ota = ta.createTextRange().getClientRects();
            if(ota.length<10)
            {
                ta.rows=10;
            }
            else
            {
                ta.rows = ota.length;
            }
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

	    rowCount = rjRowCount

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
		    if(row>rjRowCount)
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
    </script>

    <script type="text/javascript">
    function changeCheck(obj)
    {
        var RadioReplyAll = document.getElementById('RadioReplyAll');
        var RadioChooseReply = document.getElementById('RadioChooseReply');
        var LabelReplyerList = document.getElementById('LabelReplyerList');
        var hiddenReplyer = document.getElementById("hiddenReplyer");
        
        if(!obj.checked)
        {
            //不允许回复
            RadioReplyAll.disabled=true;
            RadioChooseReply.disabled=true;
            LabelReplyerList.innerText='';
        }
        if(obj.checked)
        {
            //允许回复
            RadioReplyAll.disabled=false;
            RadioReplyAll.checked=true;
            RadioChooseReply.disabled=false;
            LabelReplyerList.innerText='全部';
            hiddenReplyer.value='-1';
        }
    }
    //选择员工
    //label:页面上显示的内容    hidden：隐藏控件中存储信息与label相同
    function chooseEmp(label,hidden,userClass)
    {
        var hiddenNewsReader = document.getElementById("hiddenNewsReader");     //新闻阅读者
        var hiddenNewsReplyer = document.getElementById("hiddenNewsReplyer");   //新闻回复者

        var userList = "部门@人员@岗位@";
        if(userClass == "1")
        {
            //选择阅读者
            if(hiddenNewsReader.value != "-1" && hiddenNewsReader.value != "")
            {
                userList = hiddenNewsReader.value;
            }
        }
        if(userClass == "2")
        {
            //选择回复者
            if(hiddenNewsReplyer.value == "-1" || hiddenNewsReplyer.value == "")
            {
                if(hiddenNewsReader.value != "-1" && hiddenNewsReader.value != "")
                {
                    userList = hiddenNewsReader.value;
                }
            }
            else
            {
                userList = hiddenNewsReplyer.value;
            }
        }
        var UserInfos = "";
        UserInfos = window.showModalDialog('../../RBAC/Organizations/chooseRoles.aspx?queryType='+userList,window,"status:false;dialogWidth=700px;dialogHeight=380px;edge:Raised; enter: Yes; help: No; resizable: No; status: No");
        
        if(UserInfos != null)
        {
            var showLabel = document.getElementById(label);
            showLabel.innerText = UserInfos;
            
            var hiddenLabel = document.getElementById(hidden);
            hiddenLabel.value = UserInfos;
        }
    }
    //匿名
    function ReplyWithOutName(obj)
    {   
        var userName = document.getElementById("userName");
        if(obj.checked)
        {
            userName.innerText="匿名";
        }
        else
        {
            userName.innerText=document.getElementById("hiddenUName").value;
        }
    }
    //提交
    function subReply()
    {
        var hiddenAlreadyConfog = document.getElementById("hiddenAlreadyConfog");
        if(hiddenAlreadyConfog.value == "0")
        {
            alert("还没有配置系统的回复权限,暂时不能进行回复。");
            return;
        }
        //需要保存的内容
        var uName = "";  //用户名
        var uID = "";    //用户编号
        var uIPAddress = "";    //客户端IP地址
        var replyContent = "";  //回复的内容
        var hiddenReplyRecordID = document.getElementById("hiddenReplyRecordID"); //回复的记录编号
        
        var textareaInPutBody = document.getElementById("textareaInPutBody");
        var reSpace=/^\s*(.*?)\s*$/;
        var str = textareaInPutBody.value.replace(reSpace,"$1");
        if(str == "")
        {
            alert("请填写回复内容！");return;
        }
        if(textareaInPutBody.value.length>1000)
        {
            alert("回复内容过长，应在1000个字符以内！");return;
        }
        replyContent = textareaInPutBody.value;

        var chkAnonymous = document.getElementById("chkAnonymous");//是否匿名
        
        if(chkAnonymous != null && !chkAnonymous.checked)
        {
            //实名
            var userName = document.getElementById("hiddenUName");
            uName = userName.value;
            var hiddenUserID = document.getElementById("hiddenUserID");
            uID = hiddenUserID.value;
            var hiddenUserIPAddress = document.getElementById("hiddenUserIPAddress");
            uIPAddress = hiddenUserIPAddress.value;
        }
        else
        {
            var hiddenNM = document.getElementById("hiddenNM");
            if(hiddenNM.value == "1")
            {
                var userName = document.getElementById("hiddenUName");
                uName = userName.value;
                var hiddenUserID = document.getElementById("hiddenUserID");
                uID = hiddenUserID.value;
                var hiddenUserIPAddress = document.getElementById("hiddenUserIPAddress");
                uIPAddress = hiddenUserIPAddress.value;
            }
        }
        
        var AllowReply = "1";   //允许继续回复
        var Reader = "-1";
        var Replyer = "-1";
        var hiddenPass = document.getElementById("hiddenPass");
        
        //用户有设置回复的权限
        var chkAllowReply = document.getElementById("chkAllowReply");
        var hiddenReplyReader = document.getElementById("hiddenReplyReader");//阅读者（部分）
        Reader = hiddenReplyReader.value;
        if(chkAllowReply != null && !chkAllowReply.checked)
        {
            //不允许继续回复
            AllowReply = "0";
            Replyer = "";
        }
        else
        {
            var hiddenReplyer = document.getElementById("hiddenReplyer");//回复者（全部）
            Replyer = hiddenReplyer.value;
        }
        
        var hiddenSystemNameID = document.getElementById("hiddenSystemNameID");
        ReplyMainName = hiddenSystemNameID.value;
        
        var addSucceed = OA.News.publish.InputReplyMsg.saveReply(hiddenReplyRecordID.value, uID, uName, replyContent, uIPAddress, ReplyMainName, AllowReply, Reader, Replyer,hiddenPass.value);
        if(addSucceed.value)
        {
            alert("添加成功！");
            if(window.opener!=null)
            {
                window.opener.location.href = window.opener.location.href;
            }
            window.returnValue = "S";
            window.close();
        }
        else
        {
            alert("添加失败！");
        }
        
    }
    </script>

</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <div>
        <table border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td height="10">
                    <div id="userQX">
                        &nbsp;阅读者：<asp:RadioButton ID="RadioReplyReaderAll" onclick="LabelReaderList.innerText = '全部';hiddenReplyReader.value='-1';" runat="server" GroupName="Reader" Text="全部" />&nbsp;
                        <asp:RadioButton ID="RadioButton1" onclick="chooseEmp('LabelReaderList','hiddenReplyReader','1')" runat="server" GroupName="Reader" Text="选择阅读者" />
                        <asp:Label ID="LabelReaderList" runat="server">全部</asp:Label><br />
                        <asp:CheckBox ID="chkAllowReply" Text="允许回复" onclick="changeCheck(this);" runat="server" /><br />
                        <asp:RadioButton ID="RadioReplyAll" onclick="LabelReplyerList.innerText = '全部';hiddenReplyer.value='-1';" runat="server" GroupName="Replyer" Text="全部" />&nbsp;
                        <asp:RadioButton ID="RadioChooseReply" onclick="chooseEmp('LabelReplyerList','hiddenReplyer','2')" runat="server" GroupName="Replyer" Text="选择回复者" />
                        <asp:Label ID="LabelReplyerList" runat="server">全部</asp:Label><br />
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;我要回复：<asp:Label ID="userName" runat="server"></asp:Label>&nbsp;<asp:CheckBox ID="chkAnonymous" onclick="ReplyWithOutName(this);" runat="server" Text="匿名" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<span class="rowSet" onclick="setUpMaxRow('5',this,'textareaInPutBody');">5</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('10',this,'textareaInPutBody');">10</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('20',this,'textareaInPutBody');">20</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('30',this,'textareaInPutBody');">30</span>&nbsp; <span class="rowSet" onclick="setUpMaxRow('All',this,'textareaInPutBody');">全部</span><br />
                    <textarea id="textareaInPutBody" onkeydown="changTextAreaRow(this);" style="overflow: hidden; width: 98%; margin-left: 5px;" name="textarea" rows="10" runat="server"></textarea>
                    <br />
                    <span runat="server" id="divSub">
                        <input type="button" value="提交" onclick="subReply();" />
                    </span>
                    <asp:Button ID="btnSaveReply" runat="server" Text="保存" onclick="btnSaveReply_Click" />
                    <input type="button" value="关闭" onclick="window.close()" />
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hiddenReplyReader" value="-1" runat="server" /><!-- 当前回复阅读者 -->
    <input type="hidden" id="hiddenReplyer" value="-1" runat="server" /><!-- 当前回复回复者 -->
    <input type="hidden" id="hiddenNM" value="2" runat="server" /><!-- 是否实名(0:匿名，1;实名，2:用户选择是否实名) -->
    <input type="hidden" id="hiddenUName" value="" runat="server" /><!-- 用户名 -->
    <input type="hidden" id="hiddenUserID" value="" runat="server" /><!-- 用户编号 -->
    <input type="hidden" id="hiddenReplyRecordID" runat="server" /><!-- 回复的记录编号 -->
    <input type="hidden" id="hiddenNewsReader" runat="server" /><!-- 主记录阅读者,当前回复的可选范围 -->
    <input type="hidden" id="hiddenNewsReplyer" runat="server" /><!-- 主记录回复者，当前回复的可选范围 -->
    <input type="hidden" id="hiddenUserIPAddress" runat="server" /><!-- 用户机器的IP地址 -->
    <input type="hidden" id="hiddenSystemArgs" runat="server" />
    <input type="hidden" id="hiddenSystemNameID" runat="server" />
    <input type="hidden" id="hiddenPass" runat="server" /><!-- 1审核通过或不需要审核，0未通过审核 -->
    <input type="hidden" id="hiddenIsNeedCheck" runat="server" /><!-- 0：不需要；1：需要 -->
    <input type="hidden" id="hiddenAlreadyConfog" runat="server" />
    </form>
</body>
</html>
