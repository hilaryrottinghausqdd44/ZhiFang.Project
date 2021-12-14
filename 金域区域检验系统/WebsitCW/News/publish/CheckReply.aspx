<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckReply.aspx.cs" Inherits="OA.News.publish.CheckReply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审核回复</title>
    <style type="text/css">
        /*
        title：新闻标题
        replytext:回复标题
        replybtn:按钮
        tableReply:回复信息所在表格
        tdFirstReplyInfo:回复信息所在表格第一列
        divFirstReplyInfo:回复信息所在表格第一列内部的div
        detailTable:回复详细信息所在表格
        */
        body{font-size: 12px;}
        .tableReply{ width:100%; height:18px;}
        a{text-decoration: none; }
        .title{color:LightGreen;}
        .replytext{color:blue;}
        .replybtn{color:BlueViolet;}
        a:hover{color: green;}
        a:hover span{color: green;text-decoration: underline;}
        .detailTable{width: 95%; background-color:Beige;}
    </style>

    <script type="text/javascript">
    function openNews(newsID)
    {
        window.location.href = "../../news/browse/showpagelast.aspx?id="+newsID;
    }
    function passAll(replyId,status)
    {
        //设置回复是否全部通过passed=1：全部通过，passed=0：全部不通过
        var hiddenSystemNameID = document.getElementById("hiddenSystemNameID");
        var backValue = OA.News.publish.CheckReply.setReplyCheckStatus(replyId,hiddenSystemNameID.value,status);
        if(backValue.value=="0")
        {
            alert("审核失败！");
        }
        if(backValue.value == "1")
        {
            window.location.reload();
        }
    }
    function restoreCurReply(obj)
    {
        //恢复已删除回复
        var backValue = OA.News.publish.CheckReply.restoreReply(obj);

        if(backValue.value=="0")
        {
            alert("回复失败！");
        }
        if(backValue.value == "1")
        {
            window.location.reload();
        }
    }
    function delAll(obj,count)
    {
        //删除回复
        var sMsg = "确定要删除该条回复吗？";
        if(count == "1")
        {
            sMsg = "确定要删除全部回复吗？这将无法恢复！";
            var hiddenSystemNameID = document.getElementById("hiddenSystemNameID");
            count = hiddenSystemNameID.value;
        }
        var ensureDelReply = window.confirm(sMsg);
        if(ensureDelReply)
        {
            var backValue = OA.News.publish.CheckReply.delReply(obj,count);
            if(backValue.value=="0")
            {
                alert("删除失败！");
            }
            else if(backValue.value == "1")
            {
                window.location.reload();
            }
            else
            {
                alert(backValue.value);
            }
        }
    }
    function modify(replyId)
    {
        //修改自己的回复
        var url = "../../News/publish/InputReplyMsg.aspx?replyId="+replyId;
        window.open(url,'window',"status=false,width=700,height=340,scrollbars=yes")
        return false;
    }
    function showNewPage(index)
    {
        //显示的index页的回复内容
        if(isNaN(index))
        {
            alert("页码必须为数字形式，请却认输入是否正确！");
            return;
        }
        alert(index);
    }
    //展开回复
    //replyId:当前回复的编号
    //permitReply:是否允许回复（true：允许；false：不允许）
    //td：当前列，value:允许阅读的列表；
    function showDetailReply(replyId,lab,permitReply,td)
    {
        var div = document.getElementById("div_"+replyId);
        if(div == null)
        {
            return;
        }
        var table = document.getElementById("table_" + replyId)
        if(div && div.value == "1")
        {
            div.value = "0";
            div.style.display = "none";
            table.style.display = "none";
            return;
        }
        if(table)
        {
            table.style.display = "";
        }
        div.style.display = "";
        
        div.innerHTML = "正在联系服务器，查看回复内容";
        var uID = document.getElementById("hiddenEmplyeeID").value;
        var hiddenAdmin = document.getElementById("hiddenAdmin").value;
        var hiddenIsNeedCheck = document.getElementById("hiddenIsNeedCheck");
        
        var backValue = OA.News.publish.CheckReply.getReplyDetailInfo(replyId,uID,hiddenAdmin,hiddenIsNeedCheck.value);
        var labValue = "<textarea onkeydown='return false;' oncontextmenu='return false;' style='border:0px;overflow:visible;width:98%;font-size:12px;background-color:Beige;'>" + backValue.value + "</textarea>";
        div.innerHTML = labValue;
        if(permitReply)
        {
            var SystemArgs = document.getElementById("hiddenSystemArgs");
            var SystemNameID = document.getElementById("hiddenSystemNameID");
            var btn = "<hr /><input type='button' value='回复' onclick='replyThis("+replyId+",\""+SystemArgs.value+"\",\""+SystemNameID.value+"_"+replyId+"\",this);' />";
            div.innerHTML += btn;
        }
        div.value = "1";
        div.style.border = "1";
    }
    //打开回复页面
    function replyThis(replyId,args,SysID,obj)
    {
        var fream = document.getElementById("fream_"+replyId);
        var div = document.getElementById("div_"+replyId);

        var url = "../../News/publish/InputReplyMsg.aspx?SystemArgs="+args+"&SystemNameID="+SysID;
        var backValue = window.showModalDialog(url,window,"status:false;dialogWidth=700px;dialogHeight=380px;edge:Raised; enter: Yes; help: No; resizable: No; status: No");

        if(backValue!=null)
        {
            window.location.reload(true);
        }
    }
    function ReplyNews(args,SysID)
	{
	    var url = "../../News/publish/InputReplyMsg.aspx?SystemArgs="+args;
        window.open(url,'window',"status=false,width=700,height=340,scrollbars=yes")
        return false;
	}
    </script>

</head>
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <div>
        <table id="replyList" runat="server" width="100%">
            <tr>
                <td align="right">
                    <input type="button" value="全部通过" onclick="passAll('','1');" />
                    <input type="button" style="color: Red;" value="全部不通过" onclick="passAll('','0');" />
                    <input type="button" style="color: Red;" value="全部关闭" onclick="passAll('','2');" />
                    <input type="button" style="color: Red;" value="全部删除" onclick="delAll('','1');" />
                </td>
            </tr>
            <tr id="trReply" runat="server">
                <td >
                    <asp:Button id="btnReply" Text="回复" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="text-align: right; width: 98%;"></span>
                </td>
            </tr>
            
        </table>
    </div>
    <input type="hidden" id="hiddenAdmin" runat="server" /><!-- 管理员 -->
    <input type="hidden" id="hiddenNewsReader" runat="server" /><!-- 新闻阅读者 -->
    <input type="hidden" id="hiddenNewsReplyer" runat="server" /><!-- 新闻回复者 -->
    <input type="hidden" id="hiddenSystemArgs" runat="server" />
    <input type="hidden" id="hiddenSystemNameID" runat="server" />
    <input type="hidden" id="hiddenCurPageIndex" runat="server" /><!-- 当前页码 -->
    <input type="hidden" id="hiddenPageSize" runat="server" /><!-- 页面显示的回复数量 -->
    <input type="hidden" id="hiddenEmplyeeID" runat="server" /><!-- 员工编号 -->
    <input type="hidden" id="hiddenDepmentID" runat="server" /><!-- 部门编号（"1，2"） -->
    <input type="hidden" id="hiddenPostID" runat="server" /><!-- 岗位编号 -->
    <input type="hidden" id="hiddenIsNeedCheck" runat="server" />
    </form>
</body>
</html>