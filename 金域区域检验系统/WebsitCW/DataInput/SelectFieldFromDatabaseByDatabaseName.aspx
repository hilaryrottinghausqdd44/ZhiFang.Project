<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectFieldFromDatabaseByDatabaseName.aspx.cs" Inherits="OA.DataInput.SelectFieldFromDatabaseByDatabaseName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>从物理数据库中选择字段</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function btnOK_click()
        {
            var ret = "<%=allSelectFieldName %>";
            alert(ret);
            window.returnValue = ret;
            self.close();
        }
        function btnCancel_click()
        {
            window.returnValue = null;
            self.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="tableMain" style="width: auto;" border="1" class="Table">
        <tr>
            <td style="background-color: #f1e3ff; white-space: nowrap" colspan="2">
                <asp:Label ID="lblTitle" runat="server" CssClass="LabelTitle" Visible="True" Font-Bold="True" />
            </td>
        </tr>
        <tr>
            <td style="background-color: Yellow; text-align: center; white-space: nowrap">
                数据库中的表名称
            </td>
            <td style="vertical-align: top">
                <asp:RadioButtonList ID="rblTable" runat="server" AutoPostBack="True" CssClass="RadioButtonList" OnSelectedIndexChanged="rblTable_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td style="background-color: Yellow; text-align: center; white-space: nowrap">
                数据库中表的字段
            </td>
            <td style="vertical-align: top">
                <asp:CheckBoxList ID="cblField" runat="server" CssClass="CheckBoxList" AutoPostBack="false" />
            </td>
        </tr>
        <tr>
            <td style="background-color: Yellow; text-align: center; white-space: nowrap">
                <asp:Button ID="btnOK" runat="server" Text="确定" CssClass="Button" OnClick="btnOK_Click" />
                <input id="btnCancel" type="button" value="取消" class="Button" onclick="btnCancel_click()" />
            </td>
            <td style="background-color: Yellow; text-align: left; white-space: nowrap">
                <asp:CheckBox ID="chkSelectALL" runat="server" Text="全选" AutoPostBack="true" OnCheckedChanged="chkSelectALL_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
