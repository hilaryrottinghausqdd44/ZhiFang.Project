<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatChartSort.aspx.cs" Inherits="OA.YHY.StatChartSort" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>统计图分类</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script src="../Util/CommonJS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    
        function showRight(url)
        {
            //alert(url);
            window.parent.document.getElementById("RightTop").src = url;
        }
    
        //删除统计图分组
        function deleteStatGroup(chartSortName, statName, groupName)
        {
            if(confirm('真的要删除该统计图分组吗？'))
            {
               OA.YHY.StatChartSort.deleteSTATGroup(chartSortName, statName,groupName,GetCallResult);
            }
            
        }
        //删除统计图
        function deleteStat(chartSortName, statName)
        {
            if(confirm('真的要删除该统计图吗？'))
            {
               OA.YHY.StatChartSort.deleteSTAT(chartSortName,statName,GetCallResult);
            }
            
        }
        //AJAX异步调用返回处理:为了刷新原来的统计图分类,返回值是当前现选择的统计图分类
        function GetCallResult(result)
        {
            var r = result.value;
            if(r != null)
            {
                //window.location.href = "StatChartSort.aspx?statSort="+r;
                window.location.href = "StatChartSort.aspx?selectStatSortName="+r;
            }
        }
        //显示帮助
        function showHelp(url, param)
        {
            openWinPositionSizeUrl(url, "_blank", param);
        }
        
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="448" border="0">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblChartSort" runat="server" CssClass="LabelTitle">统计图分类</asp:Label>
                <asp:ImageButton ID="btnShowHelp" AlternateText="显示帮助" ImageUrl="~/App_Themes/zh-cn/Images/STAT/help.gif" runat="server" OnClick="btnShowHelp_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="rblChartSort" runat="server" AutoPostBack="True" CssClass="RadioButtonList" OnSelectedIndexChanged="rblChartSort_SelectedIndexChanged">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="LabelTitle">统计图列表:</asp:Label>
                <div id="divShowAdd" runat="server" class="Button" title="统计图列表">
                    <asp:Button ID="btnAddStatChart" runat="server" Text="添加" OnClick="btnAddStatChart_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataList ID="datalistStatChart" runat="server" ItemStyle-BorderWidth="1">
                    <ItemTemplate>
                        <a href='StatSetup.aspx?statName=<%#Server.UrlEncode(DataBinder.Eval(Container.DataItem, "Value").ToString())%>' target="RightTop" class="LinkA">修改</a>
                        <asp:LinkButton CssClass="LinkButton" ID="btnDeleteStatChart" runat="server" OnCommand="btnDeleteStatChart_Command" CommandName="btnDeleteStatChart" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Value").ToString()%>'>删除</asp:LinkButton>
                        <a href='ShowStatChartALLForm.aspx?statName=<%#Server.UrlEncode(DataBinder.Eval(Container.DataItem, "Value").ToString())%>' target="RightTop" class="LinkA">
                            <asp:Image ImageUrl="~/App_Themes/zh-cn/Images/STAT/dian.gif" runat="server" />
                            <%# DataBinder.Eval(Container.DataItem, "Value")%>
                        </a>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TreeView ID="tvSTAT" runat="server">
                </asp:TreeView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
