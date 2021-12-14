<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatSetup.aspx.cs" Inherits="OA.YHY.StatSetup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>统计图定义</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="tblStatInfo" cellspacing="0" cellpadding="0" style="text-align: center; width: 560px" border="1">
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <table id="tblStatInfo1" cellspacing="0" cellpadding="0" width="100%" border="1">
                    <tr>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right; width: 15%">
                            统计图类别
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: left; width: 35%">
                            <asp:TextBox ID="txtChartSort" runat="server" Width="0%" Visible="False" Enabled="False"></asp:TextBox>
                            <asp:DropDownList ID="ddlChartSort" runat="server" AutoPostBack="True" CssClass="DropDownList" Width="100%" OnSelectedIndexChanged="ddlChartSort_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            统计名称
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: left;">
                            <asp:TextBox ID="txtStatName" runat="server" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            统计标题
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: left;">
                            <asp:TextBox ID="txtTitle" runat="server" Width="96%"></asp:TextBox>
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            宽度*高度
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: left">
                            <asp:TextBox ID="txtWidth" runat="server" Width="40px" MaxLength="4"></asp:TextBox>*
                            <asp:TextBox ID="txtHeight" runat="server" Width="40px" MaxLength="4"></asp:TextBox>(单位:像素)
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            横坐标标题
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: left;">
                            <asp:TextBox ID="txtXTitle" runat="server" Width="96%"></asp:TextBox>
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            纵坐标标题
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: left">
                            <asp:TextBox ID="txtYTitle" runat="server" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                统计图类型
            </td>
            <td style="white-space: nowrap; text-align: left">
                <asp:RadioButtonList ID="selectChartType" runat="server" CssClass="RadioButtonList" RepeatColumns="4">
                    <asp:ListItem Value="Combo" Selected="True">直方图</asp:ListItem>
                    <asp:ListItem Value="ComboHorizontal">水平直方图</asp:ListItem>
                    <asp:ListItem Value="Pies">饼图</asp:ListItem>
                    <asp:ListItem Value="Ring">环型图</asp:ListItem>
                    <asp:ListItem Value="Column">柱型图</asp:ListItem>
                    <asp:ListItem Value="AreaLine">面积图</asp:ListItem>
                    <asp:ListItem Value="Spline">折线图</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" colspan="2">
                <asp:CheckBox ID="chkShowValue" runat="server" Text="显示结果" CssClass="CheckBoxList" Checked="True"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right" colspan="2">
                <table id="tblStatInfo2" cellspacing="0" cellpadding="0" style="width: 100%;" border="1">
                    <tr>
                        <td class="LabelInput" style="text-align: right; width: 15%">
                            数据库名称
                        </td>
                        <td style="text-align: left; width: 35%">
                            <asp:DropDownList ID="ddlDataBase" runat="server" AutoPostBack="True" CssClass="DropDownList" Width="100%" OnSelectedIndexChanged="ddlDataBase_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="LabelInput" style="text-align: right; width: 15%">
                            表名称
                        </td>
                        <td style="text-align: left; width: 35%">
                            <asp:DropDownList ID="ddlTableName" runat="server" AutoPostBack="True" CssClass="DropDownList" Width="100%" OnSelectedIndexChanged="ddlTableName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            第一统计分类项
                        </td>
                        <td style="white-space: nowrap; text-align: left">
                            <asp:DropDownList ID="ddlFirstField" runat="server" Width="100px" AutoPostBack="False" CssClass="DropDownList" OnSelectedIndexChanged="ddlFirstField_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlFirstFieldType" runat="server" Width="100px" Visible="False" CssClass="DropDownList">
                            </asp:DropDownList>
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            第二统计分类项
                        </td>
                        <td class="LabelInput" style="white-space: nowrap; text-align: right;">
                            <asp:DropDownList ID="ddlSecondField" runat="server" AutoPostBack="True" CssClass="DropDownList" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right">
                统计项
            </td>
            <td class="LabelInput" style="white-space: nowrap; text-align: left">
                <asp:CheckBox ID="chkRecCount" runat="server" Text="记录数" Checked="true" />
                <asp:CheckBoxList ID="cblStatItem" runat="server" CssClass="CheckBoxList" RepeatColumns="4">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                统计项(多选)
            </td>
            <td>
                <asp:Table ID="tableDetailDataInput" CellPadding="0" CellSpacing="0" runat="server" GridLines="Both" CssClass="GridView" Width="100%" EnableViewState="false">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right">
                <asp:CheckBox ID="chkSelectALL" runat="server" Text="查询字段" AutoPostBack="true" OnCheckedChanged="chkSelectALL_CheckedChanged" />
            </td>
            <td class="LabelInput" style="white-space: nowrap; text-align: left">
                <asp:CheckBoxList ID="cblQueryField" runat="server" CssClass="CheckBoxList" RepeatColumns="4">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right">
                引用相对地址
            </td>
            <td class="LabelInput" style="text-align: left">
                <asp:TextBox ID="txtUrl" runat="server" Enabled="False" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right">
                引用绝对地址
            </td>
            <td class="LabelInput" style="text-align: left">
                <asp:TextBox ID="txtUrlAbs" runat="server" Enabled="False" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right">
                引用相对地址(带查询)
            </td>
            <td class="LabelInput" style="text-align: left">
                <asp:TextBox ID="txtUrlQuery" runat="server" Enabled="False" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right">
                引用绝对地址(带查询)
            </td>
            <td class="LabelInput" style="text-align: left">
                <asp:TextBox ID="txtUrlAbsQuery" Wrap="false" runat="server" Enabled="False" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap; text-align: right">
                统计分组引用方法
            </td>
            <td class="LabelTitle" style="text-align: left; color: Blue">
                groupName=分组列表,分组列表中用逗号分割,如果没有groupName参数,则显示所有的分组,如果分组列表为空,则不显示分组!
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
