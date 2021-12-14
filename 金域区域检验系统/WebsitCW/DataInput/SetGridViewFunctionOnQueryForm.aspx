<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetGridViewFunctionOnQueryForm.aspx.cs"
    Inherits="OA.DataInput.SetGridViewFunctionOnQueryForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置数据列表的处理功能</title>

    <script language="javascript" type="text/javascript">
        //选择应用系统
        function btnSelectAppSystemClick()
        {
            var url = "SelectAppSystemNameForm.aspx";
            var r = window.showModalDialog(url, "_blank", "dialogWidth=600;dialogHeight=300;center=yes;resizable=yes;scroll=yes;status=yes");
	        if (r == null || typeof(r) == 'undefined'||typeof(r)=='object')
	        {
		        return;
	        }
	        var ret = window.document.getElementById("txtAppSystem");
	        ret.value = r;
	        window.document.getElementById("txtTable").value = "";
	        window.document.getElementById("txtField").value = "";
        }
        //选择表
        function btnSelectTableClick()
        {
	        var obj = window.document.getElementById("txtAppSystem");
	        var systemName = obj.value;
	        if(systemName == "")
	        {
	            alert("请先设置“应用系统”！");
	            return;
	        }
	        //编码
	        systemName = escape(systemName);
            var url = "SelectTableNameFromAppSystemNameForm.aspx?systemName=" + systemName;
            //alert(url);
            var r = window.showModalDialog(url, "_blank", "dialogWidth=600;dialogHeight=300;center=yes;resizable=yes;scroll=yes;status=yes");
	        if (r == null || typeof(r) == 'undefined'||typeof(r)=='object')
	        {
		        return;
	        }
	        var ret = window.document.getElementById("txtTable");
	        ret.value = r;
	        window.document.getElementById("txtField").value = "";
        }
        //选择字段
        function btnSelectFieldClick()
        {
	        var obj = window.document.getElementById("txtTable");
	        var tableName = obj.value;
	        if(tableName == "")
	        {
	            alert("请先设置“表”！");
	            return;
	        }
	        //编码
	        tableName = escape(tableName);
	        //取应用系统
	        obj = window.document.getElementById("txtAppSystem");
	        var systemName = obj.value;
	        //编码
	        systemName = escape(systemName);
            var url = "SelectFieldNameFromAppSystemTableForm.aspx?systemName=" + systemName + "&tableName=" + tableName;
            //alert(url);
            var r = window.showModalDialog(url, "_blank", "dialogWidth=600;dialogHeight=300;center=yes;resizable=yes;scroll=yes;status=yes");
	        if (r == null || typeof(r) == 'undefined'||typeof(r)=='object')
	        {
		        return;
	        }
	        //显示
	        var ret = window.document.getElementById("txtField");
	        ret.value = r;
        }
        
        //确定
        function btnOkClick()
        {
            var url = window.document.getElementById("txtURL").value;
	        if(url == "")
	        {
	            alert("请先设置“URL”！");
	            return;
	        }
            var systemName = window.document.getElementById("txtAppSystem").value;
            var tableName = window.document.getElementById("txtTable").value;
            var fieldName = window.document.getElementById("txtField").value;
	        if(fieldName == "")
	        {
	            alert("请先设置“字段”！");
	            return;
	        }
	        //编码
	        //systemName = escape(systemName);
	        //tableName = escape(tableName);
	        //fieldName = escape(fieldName);
	        url = url + "?systemName=" + systemName + "&tableName=" + tableName + "&fieldName=" + fieldName;
            //alert(url);
            //window.open(url, "_blank", "","");
            window.returnValue = url;
            self.close();
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="448" border="1">
        <tr>
            <td style="white-space: nowrap; width: 1%; text-align: right">
                <asp:Label ID="Label1" runat="server">URL</asp:Label>
            </td>
            <td style="white-space: nowrap; width: 98%; text-align: right">
                <asp:TextBox ID="txtURL" Width="98%" runat="server" Text="DataAddAndUpdateForm.aspx" />
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap; width: 1%; text-align: right">
                <asp:Label ID="lblAppSystem" runat="server">应用系统名称</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAppSystem" runat="server" Text="" Width="88%" Enabled="false" BackColor="LightGray" />
                <input id="btnSelectAppSystem" name="btnSelectAppSystem" type="button" value="..." onclick="return btnSelectAppSystemClick()" />
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap; width: 1%; text-align: right">
                <asp:Label ID="Label2" runat="server">表名称</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTable" runat="server" Text="" Width="88%" Enabled="false" BackColor="LightGray" />
                <input id="btnSelectTable" name="btnSelectTable" type="button" value="..." onclick="return btnSelectTableClick()" />
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap; width: 1%; text-align: right">
                <asp:Label ID="Label3" runat="server">传入字段</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtField" runat="server" Text="" Width="88%" Enabled="false" BackColor="LightGray" />
                <input id="btnSelectField" name="btnSelectField" type="button" value="..." onclick="return btnSelectFieldClick()" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="white-space: nowrap; width: 98%; text-align:center">
                <input id="btnOK" name="btnOK" type="button" value="确定" onclick="return btnOkClick()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
