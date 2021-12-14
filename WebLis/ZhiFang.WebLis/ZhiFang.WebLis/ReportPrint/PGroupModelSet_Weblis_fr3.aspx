<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PGroupModelSet_Weblis.aspx.cs"
    Inherits="ZhiFang.WebLis.ReportPrint.PGroupModelSet_Weblis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小组模板设定_Weblis</title>
    <link href="../Css/Default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/Tools.js"></script>
    <script language="javascript">
        function DelPGroupMode(id) {
            if (confirm("确定删除？")) {
                document.getElementById('tmpId').value = id;
                var o = document.getElementById('Button1');
                o.click();
            }
            else {
                return;
            }
        }
        function EditPGroupModel(id) {
            WOpenMid('PGroupModelAdd_Weblis.aspx?Id=' + id, 'EditPGroupModel');
        }
    </script>
</head>
<body style="width: 100%; padding: 0px; margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color: #0099cc">
            <tr style="background-color: #FFFFFF">
                <td align="center">
                    <div style="font-size: 20px; font-weight: bold; margin: 10px">
                        小组报告模板设定</div>
                </td>
                <td align="center" width="10%">
                    <input type="button" value="添加" onclick="WOpenMid('PGroupModelAdd_Weblis.aspx','PGroupModelAdd');" />
                </td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="center" colspan="2">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Style="display: none" /><asp:TextBox
                                ID="tmpId" runat="server" Style="display: none"></asp:TextBox>
                            <asp:DataList ID="DataList1" runat="server" Width="100%">
                                <HeaderTemplate>
                                    <table width="100%" class="tablehead" cellspacing="1" cellpadding="0" border="0">
                                        <tr>
                                            <td width="60" align="right">
                                                小组名称
                                            </td>
                                            <td width="160">
                                                模板名称
                                            </td>
                                            <td width="60">
                                                送检单位
                                            </td>
                                            <td width="60">
                                                特殊项目
                                            </td>
                                            <td width="60">
                                                项目或图片数下限
                                            </td>
                                            <td width="60">
                                                项目或图片数上限
                                            </td>
                                            <td width="60">
                                                优先级别
                                            </td>
                                            <td width="50">
                                                使用标志
                                            </td>
                                            <td width="50">
                                                就诊类型
                                            </td>
                                            <td width="50">
                                                模板抬头类型
                                            </td>
                                            <td width="50">
                                                操作
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" height="25" bgcolor="#ffffff" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'"
                                        onmouseout="this.bgColor='#ffffff';this.style.color='#000000'" cellspacing="1"
                                        cellpadding="0" border="0">
                                        <tr title="<%# DataBinder.Eval(Container.DataItem, "Id")%>">
                                            <td width="60" align="right">
                                                <%# DataBinder.Eval(Container.DataItem, "SectionName")%>
                                            </td>
                                            <td width="160">
                                                <%# DataBinder.Eval(Container.DataItem, "PrintFormatName")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "ClientName")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "SpecialtyItemName")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "ItemMinNumber")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "ItemMaxNumber")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "Sort")%>
                                            </td>
                                            <td width="50">
                                                <%# DataBinder.Eval(Container.DataItem, "UseFlag").ToString() == "1" ? "是" : "否"%>
                                            </td>
                                            <td width="50">
                                                <%# DataBinder.Eval(Container.DataItem, "SickTypeNo").ToString() == "1" ? "住院" :""%><%# DataBinder.Eval(Container.DataItem, "SickTypeNo").ToString() == "2" ? "门诊" : ""%><%# DataBinder.Eval(Container.DataItem, "SickTypeNo").ToString() == "3" ? "体检" : ""%>
                                                <td width="50">
                                                    <%# DataBinder.Eval(Container.DataItem, "ModelTitleType").ToString() == "1" ? "中心" : ""%><%# DataBinder.Eval(Container.DataItem, "ModelTitleType").ToString() == "0" ? "医院" : ""%>
                                                </td>
                                                <td width="50">
                                                    <a href="javascript:EditPGroupModel('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">
                                                        修改</a>&nbsp;&nbsp;<a href="javascript:DelPGroupMode('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">删除</a>
                                                </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <table width="100%" height="25" bgcolor="#BFDBE1" onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'"
                                        onmouseout="this.bgColor='#BFDBE1';this.style.color='#000000'" cellspacing="1"
                                        cellpadding="0" border="0">
                                        <tr title="<%# DataBinder.Eval(Container.DataItem, "Id")%>">
                                            <td width="60" align="right">
                                                <%# DataBinder.Eval(Container.DataItem, "SectionName")%>
                                            </td>
                                            <td width="160">
                                                <%# DataBinder.Eval(Container.DataItem, "PrintFormatName")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "ClientName")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "SpecialtyItemName")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "ItemMinNumber")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "ItemMaxNumber")%>
                                            </td>
                                            <td width="60">
                                                <%# DataBinder.Eval(Container.DataItem, "Sort")%>
                                            </td>
                                            <td width="50">
                                                <%# DataBinder.Eval(Container.DataItem, "UseFlag").ToString() == "1" ? "是" : "否"%>
                                            </td>
                                            <td width="50">
                                                <%# DataBinder.Eval(Container.DataItem, "SickTypeNo").ToString() == "1" ? "住院" :""%><%# DataBinder.Eval(Container.DataItem, "SickTypeNo").ToString() == "2" ? "门诊" : ""%><%# DataBinder.Eval(Container.DataItem, "SickTypeNo").ToString() == "3" ? "体检" : ""%>
                                                <td width="50">
                                                    <%# DataBinder.Eval(Container.DataItem, "ModelTitleType").ToString() == "1" ? "中心" : ""%><%# DataBinder.Eval(Container.DataItem, "ModelTitleType").ToString() == "0" ? "医院" : ""%>
                                                </td>
                                                <td width="50">
                                                    <a href="javascript:EditPGroupModel('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">
                                                        修改</a>&nbsp;&nbsp;<a href="javascript:DelPGroupMode('<%# DataBinder.Eval(Container.DataItem, "Id")%>')">删除</a>
                                                </td>
                                        </tr>
                                    </table>
                                </AlternatingItemTemplate>
                            </asp:DataList></ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
