<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetMergeRule.aspx.cs" Inherits="ZhiFang.WebLis.SetItemMerge.SetMergeRule" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目规则设置</title>
      <link href="../Css/Default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/Tools.js"></script>
    <script type="text/javascript" language="javascript">
        function ShowItemListAll(MergeRuleName) {
            document.getElementById('SelectedList').innerHTML = "加载中...";
            ZhiFang.WebLis.SetItemMerge.SetMergeRule.ShowItemList(MergeRuleName, 0, 20, 3, ShowItemListAllcallback);
        }
        function ShowItemListAllcallback(o) {
            if (o != null && o.value != null) {
                var htmla = o.value;
                if (htmla[0] != null) {
                    document.getElementById('SelectedList').innerHTML = htmla[0];
                }
                else {
                    document.getElementById('SelectedList').innerHTML = "暂无！";
                }
            }
        }
        function EditPGroupModel(MergeRuleName) {
            WOpenMid('AddMergeRule.aspx?MergeRuleName=' + MergeRuleName, 'AddMergeRule');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <table width="100%" cellspacing="1" cellpadding="0" border="0" style="background-color: #0099cc">
        <tr style="background-color: #FFFFFF">
            <td align="center">
                <div style="font-size: 20px; font-weight: bold; margin: 10px">
                    项目规则设置</div>
            </td>
            <td align="center" width="10%">
                <input type="button" value="添加"  OnClick="WOpenMid('AddMergeRule.aspx','AddMergeRule');"/>
            </td>
        </tr>
    </table>
    <table width="100%" cellspacing="1" cellpadding="0" border="0"
       style="background-color: #0099cc" >
        <tr style="background-color: #FFFFFF">
            <td align="left" width="35%" valign="top" >
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Style="display: none" />
                        <asp:TextBox ID="tmpId" runat="server" Style="display: none"></asp:TextBox>
                        <asp:DataList ID="DataList1" runat="server" Width="100%" CellSpacing="0" CellPadding="0"
                            border="0">
                            <HeaderTemplate>
                                <table width="100%" class="tablehead" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td width="100" align="center">
                                           合并规则名称
                                        </td>
                                        <td width="50" align="center">
                                            操作
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table width="100%" height="25"  onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'"
                                        onmouseout="this.bgColor='#ffffff';this.style.color='#000000'" cellspacing="0" cellpadding="0" border="0">
                                    <tr title="<%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>" onclick="ShowItemListAll('<%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>')">
                                        <td width="100">
                                            <%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>
                                        </td>
                                      <%--   <td width="100">
                                            <%# DataBinder.Eval(Container.DataItem, "SectionName")%>
                                        </td>--%>
                                        <td width="50">
                                            <a  href="javascript:EditPGroupModel('<%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>')">修改</a>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table width="100%" height="25"  onmousemove="this.bgColor='#3F7885';this.style.color='#ffffff'" onmouseout="this.bgColor='#ffffff';this.style.color='#000000'" cellspacing="0" cellpadding="0" border="0">
                                    <tr title="<%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>" onclick="ShowItemListAll('<%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>')">
                                        <td width="100">
                                            <%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>
                                        </td>
                                      <%--   <td width="100">
                                            <%# DataBinder.Eval(Container.DataItem, "SectionName")%>
                                        </td>--%>
                                        <td width="50">
                                            <a  href="javascript:EditPGroupModel('<%# DataBinder.Eval(Container.DataItem, "MergeRuleName")%>')">修改</a>
                                        </td>
                                    </tr>
                                </table>
                            </AlternatingItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td  valign="top"><div id="SelectedList" style="border:#0099cc solid 1px;height:500px;">&nbsp;</div></td> 
        </tr>
    </table>
    <div>
    </div>
    </form>
</body>
</html>
