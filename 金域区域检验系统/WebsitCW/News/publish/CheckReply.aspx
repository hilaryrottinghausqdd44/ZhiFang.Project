<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckReply.aspx.cs" Inherits="OA.News.publish.CheckReply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��˻ظ�</title>
    <style type="text/css">
        /*
        title�����ű���
        replytext:�ظ�����
        replybtn:��ť
        tableReply:�ظ���Ϣ���ڱ��
        tdFirstReplyInfo:�ظ���Ϣ���ڱ���һ��
        divFirstReplyInfo:�ظ���Ϣ���ڱ���һ���ڲ���div
        detailTable:�ظ���ϸ��Ϣ���ڱ��
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
        //���ûظ��Ƿ�ȫ��ͨ��passed=1��ȫ��ͨ����passed=0��ȫ����ͨ��
        var hiddenSystemNameID = document.getElementById("hiddenSystemNameID");
        var backValue = OA.News.publish.CheckReply.setReplyCheckStatus(replyId,hiddenSystemNameID.value,status);
        if(backValue.value=="0")
        {
            alert("���ʧ�ܣ�");
        }
        if(backValue.value == "1")
        {
            window.location.reload();
        }
    }
    function restoreCurReply(obj)
    {
        //�ָ���ɾ���ظ�
        var backValue = OA.News.publish.CheckReply.restoreReply(obj);

        if(backValue.value=="0")
        {
            alert("�ظ�ʧ�ܣ�");
        }
        if(backValue.value == "1")
        {
            window.location.reload();
        }
    }
    function delAll(obj,count)
    {
        //ɾ���ظ�
        var sMsg = "ȷ��Ҫɾ�������ظ���";
        if(count == "1")
        {
            sMsg = "ȷ��Ҫɾ��ȫ���ظ����⽫�޷��ָ���";
            var hiddenSystemNameID = document.getElementById("hiddenSystemNameID");
            count = hiddenSystemNameID.value;
        }
        var ensureDelReply = window.confirm(sMsg);
        if(ensureDelReply)
        {
            var backValue = OA.News.publish.CheckReply.delReply(obj,count);
            if(backValue.value=="0")
            {
                alert("ɾ��ʧ�ܣ�");
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
        //�޸��Լ��Ļظ�
        var url = "../../News/publish/InputReplyMsg.aspx?replyId="+replyId;
        window.open(url,'window',"status=false,width=700,height=340,scrollbars=yes")
        return false;
    }
    function showNewPage(index)
    {
        //��ʾ��indexҳ�Ļظ�����
        if(isNaN(index))
        {
            alert("ҳ�����Ϊ������ʽ����ȴ�������Ƿ���ȷ��");
            return;
        }
        alert(index);
    }
    //չ���ظ�
    //replyId:��ǰ�ظ��ı��
    //permitReply:�Ƿ�����ظ���true������false��������
    //td����ǰ�У�value:�����Ķ����б�
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
        
        div.innerHTML = "������ϵ���������鿴�ظ�����";
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
            var btn = "<hr /><input type='button' value='�ظ�' onclick='replyThis("+replyId+",\""+SystemArgs.value+"\",\""+SystemNameID.value+"_"+replyId+"\",this);' />";
            div.innerHTML += btn;
        }
        div.value = "1";
        div.style.border = "1";
    }
    //�򿪻ظ�ҳ��
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
                    <input type="button" value="ȫ��ͨ��" onclick="passAll('','1');" />
                    <input type="button" style="color: Red;" value="ȫ����ͨ��" onclick="passAll('','0');" />
                    <input type="button" style="color: Red;" value="ȫ���ر�" onclick="passAll('','2');" />
                    <input type="button" style="color: Red;" value="ȫ��ɾ��" onclick="delAll('','1');" />
                </td>
            </tr>
            <tr id="trReply" runat="server">
                <td >
                    <asp:Button id="btnReply" Text="�ظ�" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="text-align: right; width: 98%;"></span>
                </td>
            </tr>
            
        </table>
    </div>
    <input type="hidden" id="hiddenAdmin" runat="server" /><!-- ����Ա -->
    <input type="hidden" id="hiddenNewsReader" runat="server" /><!-- �����Ķ��� -->
    <input type="hidden" id="hiddenNewsReplyer" runat="server" /><!-- ���Żظ��� -->
    <input type="hidden" id="hiddenSystemArgs" runat="server" />
    <input type="hidden" id="hiddenSystemNameID" runat="server" />
    <input type="hidden" id="hiddenCurPageIndex" runat="server" /><!-- ��ǰҳ�� -->
    <input type="hidden" id="hiddenPageSize" runat="server" /><!-- ҳ����ʾ�Ļظ����� -->
    <input type="hidden" id="hiddenEmplyeeID" runat="server" /><!-- Ա����� -->
    <input type="hidden" id="hiddenDepmentID" runat="server" /><!-- ���ű�ţ�"1��2"�� -->
    <input type="hidden" id="hiddenPostID" runat="server" /><!-- ��λ��� -->
    <input type="hidden" id="hiddenIsNeedCheck" runat="server" />
    </form>
</body>
</html>