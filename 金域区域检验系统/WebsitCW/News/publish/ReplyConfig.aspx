<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyConfig.aspx.cs" Inherits="OA.News.publish.ReplyConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>评论配置</title>
    <base target="_self"></base>
    <script language="javascript">
    //人员选择
    function getSearchArea(OldArea)
    {
        if(OldArea == "全部" || OldArea=="-1")
        {
            OldArea = "部门@人员@岗位";
        }
        else
        {
            if(OldArea.indexOf("部门")<0)
            {
                OldArea = OldArea+"@"+"部门";
            }
            if(OldArea.indexOf("人员")<0)
            {
                OldArea = OldArea+"@"+"人员";
            }
            if(OldArea.indexOf("岗位")<0)
            {
                OldArea = OldArea+"@"+"岗位";
            }
            while(OldArea.indexOf('@') == 0)
            {
                OldArea = OldArea.substr(1,OldArea.length-1);
            }
        }
        return OldArea;
    }
    function chooseEmp(obj1,obj2,selectNo)
    {
        var area = "";
        if(selectNo == '1')
        {
            //浏览
            var hiddenScanReply = document.getElementById("hiddenScanReply").value;
            area = getSearchArea(hiddenScanReply);
        }
        if(selectNo == '2')
        {
            //回复
            var hiddenScanReply = document.getElementById("hiddenReReply").value;
            area = getSearchArea(hiddenScanReply);
        }
        if(selectNo == '3')
        {
            //审核
            var hiddenScanReply = document.getElementById("hiddenReplyChecker").value;
            area = getSearchArea(hiddenScanReply);
        }
        var userID;
        userID = showModalDialog('../../RBAC/Organizations/chooseRoles.aspx?queryType='+area,'','DialogWidth:600px;DialogHeight:650px;resizable:yes;left:' + (screen.availWidth-740)/2 + ';top:' + (screen.availHeight-650)/2 );

        if(userID!=null && userID!="")
        {
            var obj11 = document.getElementById(obj1);
            obj11.innerHTML = userID;
            
            var obj21 = document.getElementById(obj2);
            obj21.value = userID;
        }
        return false;
    }
    //验证用户输入
    function checkUserInPut()
    {
        var pageSize = document.getElementById("pageSize");
        var size = pageSize.value;
        if(isNaN(size))
        {
            alert("单页评论数必须是数字！");
            pageSize.focus();
            return false;
        }
        return true;
    }
    //保存默认配置
    function setDefaultChonfig()
    {
        var flag = window.confirm("确定要重置配置信息吗？");
        if(!flag)
        {
            return false;
        }
        var RadioReplyEachOther = document.getElementById("RadioReplyEachOther");
        RadioReplyEachOther.checked = true;
        
        var RadioWriterSet = document.getElementById("RadioWriterSet");
        RadioWriterSet.checked = true;
        
        var AllEmpScanReply = document.getElementById("AllEmpScanReply");
        AllEmpScanReply.click();
        
        var labelReReplyAll = document.getElementById("labelReReplyAll");
        labelReReplyAll.click();
        
        var chkIsNeedSh = document.getElementById("chkIsNeedSh");
        if(chkIsNeedSh.checked)
        {
            chkIsNeedSh.click();
        }
        
        var pageSize = document.getElementById("pageSize");
        pageSize.value="20";
        
        var RadioSignCharacterName = document.getElementById("RadioSignCharacterName");
        RadioSignCharacterName.click();
    }
    </script>

</head>
<body style="font-size: small;">
    <form id="form1" runat="server">
    <div>
        <table border="1" style="width: 100%; height: 100%; padding: 0; margin: 0; border-style: groove;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 90px" valign="middle" align="left">
                    评论对象
                </td>
                <td>
                    <input id="RadioReplyMainNew" type="radio" name="replyObject" runat="server" /><label for="RadioReplyMainNew">仅评论主题</label>&nbsp;&nbsp;
                    <input id="RadioReplyEachOther" type="radio" name="replyObject" runat="server" /><label for="RadioReplyEachOther">允许评论交流</label>
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="left">
                    是否匿名
                </td>
                <td>
                    <asp:RadioButton ID="RadioNoFillName" GroupName="FillName" runat="server" Text="匿名" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioFillRealName" GroupName="FillName" runat="server" Text="实名" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioWriterSet" GroupName="FillName" runat="server" Text="用户选择是否实名" />
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="left">
                    评论查看
                </td>
                <td>
                    <a href="#" onclick="labelReplyScan.innerText='全部';hiddenScanReply.value='-1';" id="AllEmpScanReply">全部</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="chooseEmp('labelReplyScan','hiddenScanReply','1');" id="ChooseEmpScanReply">选择浏览人员</a>
                    <asp:Label ID="labelReplyScan" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="left" nowrap>
                    评论回复
                </td>
                <td>
                    <a href="#" onclick="labelReReply.innerText='全部';hiddenReReply.value='-1';" id="labelReReplyAll">全部</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="chooseEmp('labelReReply','hiddenReReply','2');" id="ReReplyChoose">选择回复人员</a>
                    <asp:Label ID="labelReReply" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    审核设置
                </td>
                <td>
                    <asp:CheckBox ID="chkIsNeedSh" runat="server" Text="需要审核" onclick="if(this.checked){div_checker.style.display='';}else{div_checker.style.display='none';}" />
                    <span id="div_checker" runat="server">
                        <a href="#" onclick="chooseEmp('labelReplyChecker','hiddenReplyChecker','3');" id="CheckerChoose">选择审核人员</a>
                        <asp:Label ID="labelReplyChecker" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="width: 67px" valign="middle" align="left" nowrap>
                    单页评论数
                </td>
                <td>
                    <asp:TextBox ID="pageSize" Text="20" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" nowrap>
                    允许个性签名
                </td>
                <td>
                    <asp:RadioButton ID="RadioSignCharacterName" GroupName="characterName" Text="是" runat="server" />
                    <asp:RadioButton ID="RadioSignNothing" GroupName="characterName" Text="否" runat="server" />
                </td>
            </tr>
        </table>
        <table id="Table2" style="height: 24px" cellspacing="1" cellpadding="1" width="100%" border="0">
            <tr>
                <td valign="middle" align="left">
                    <font face="宋体">
                        <asp:Button ID="btnSaveConfig" Text="保存" runat="server" onclick="btnSaveConfig_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSaveDefault" Text="恢复默认" runat="server" onclick="btnSaveDefault_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="buttonClose" onclick="window.close()" type="button" value="关闭">
                    </font>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hiddenReader" value="全部" runat="server" /><!-- 主题阅读者 -->
    <input type="hidden" id="hiddenReply" value="全部" runat="server" /><!-- 主题回复者 -->
    
    <input type="hidden" id="hiddenScanReply" value="全部" runat="server" /><!-- 回复阅读者 -->
    <input type="hidden" id="hiddenReReply" value="全部" runat="server" /><!-- 再回复者 -->
    
    <input type="hidden" id="hiddenReplyChecker" value="-1" runat="server" /><!-- 回复审核者 -->
    <input type="hidden" id="hiddenAllowReply" runat="server" value="1" /><!-- 是否允许回复（1:是  0否） -->
    
    <input type="hidden" id="hiddenIsNewConfig" runat="server" /><!-- 是否是新配置（1新配置；0原有配置） -->
    </form>
</body>
</html>
