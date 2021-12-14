<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipsMait.aspx.cs" Inherits="OA.QMSEquips.Config.EquipsMait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
    function addType()
    {
        window.opener.location = window.opener.location;window.close();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="PanelEquipType" Visible="false" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" width="100%" id="table1">
            <tr>
                <td colspan="2" align="center">
                    <b>添加新类别</b>
                </td>
            </tr>
            <tr>
                <td align="right">
                    英文编号
                </td>
                <td>
                    <asp:TextBox ID="typeEName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="typeEName" Display="Dynamic" ErrorMessage="仪器英文编号必须填写"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    中文名
                </td>
                <td>
                    <asp:TextBox ID="TypeCName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TypeCName" Display="Dynamic" ErrorMessage="仪器中文名不能为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    分组名
                </td>
                <td>
                    <asp:DropDownList ID="dropProtectClass" Width="100" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" Text="确定" OnClick="btnSave_Click" EnableViewState="False" UseSubmitBehavior="False" />
                </td>
                <td align="center">
                    <input type="button" value="取消" onclick="window.close();" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <font size="2" color="red">注：类别英文编号保存后将不能修改，请确定后在保存 </font>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelEquips" Visible="false" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" width="100%" id="table2">
            <tr>
                <td colspan="2" align="center">
                    <b>添加维护对象</b>
                </td>
            </tr>
            <tr>
                <td align="right">
                    编号
                </td>
                <td>
                    <asp:TextBox ID="equipID" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="equipID" Display="Dynamic" ErrorMessage="仪器编号必须填写"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    名称
                </td>
                <td>
                    <asp:TextBox ID="equipName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="equipName" Display="Dynamic" ErrorMessage="仪器名不能为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="addEquip" runat="server" Text="确定" OnClick="addEquip_Click" />
                </td>
                <td align="center">
                    <input type="button" value="取消" onclick="window.close();" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <font size="2" color="red">注：编号保存后将不能修改，请确定后在保存 </font>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelRecordProject" Visible="false" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" width="100%" id="table3">
            <tr>
                <td colspan="2" align="center">
                    <b>添加新项目</b>
                </td>
            </tr>
            <tr>
                <td align="right">
                    维护项目名称
                </td>
                <td>
                    <asp:TextBox ID="recordProject" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="recordProject" Display="Dynamic" ErrorMessage="项目名称必须填写"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    使用表格
                </td>
                <td>
                    <asp:DropDownList ID="forTable" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    项目输入方式
                </td>
                <td>
                    <asp:DropDownList ID="recordType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="addRecordProject" runat="server" Text="确定" OnClick="addRecordProject_Click" />
                </td>
                <td align="center">
                    <input type="button" value="取消" onclick="window.close();" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelInputForm" Visible="false" runat="server">
        <table id="tableForm" border="1" cellspacing="0" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <td colspan="2" align="center">
                        <b>项目维护方式设置</b>
                    </td>
                </tr>
            </thead>
            <tr>
                <td style="width: 50%">
                    <b>应用类型</b>
                </td>
                <td align="left">
                    <asp:DropDownList ID="useTable" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <b>录入方式</b>
                </td>
                <td align="left">
                    <asp:DropDownList ID="InputType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="saveTableForm" runat="server" Text="确定" OnClick="saveTableForm_Click" />
                </td>
                <td align="center">
                    <input type="button" value="取消" onclick="window.close();" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <b>已有应用类型</b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:CheckBoxList ID="useTableList" runat="server">
                    </asp:CheckBoxList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="delUseTable" runat="server" Text="确定" OnClick="delUseTable_Click" />
                </td>
                <td align="center">
                    <input type="button" value="取消" onclick="window.close();" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelDelProID" Visible="false" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <td colspan="2" align="center">
                        确定要删除维护项目：<br />
                        <asp:Label ForeColor="red" ID="proName" runat="server"></asp:Label>吗？
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="delProg" runat="server" Text="确定" OnClick="delProg_Click" />
                    </td>
                    <td>
                        <input type="button" value="取消" onclick="window.close();" />
                    </td>
                </tr>
            </thead>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
