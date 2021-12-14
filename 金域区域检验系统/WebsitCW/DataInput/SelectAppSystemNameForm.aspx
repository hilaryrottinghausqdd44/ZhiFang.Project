<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectAppSystemNameForm.aspx.cs"
    Inherits="OA.DataInput.SelectAppSystemNameForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择应用系统名称页面</title>

    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script src="../Util/CommonJS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function btnOkClick()
        {
            //取选择的应用系统
    	    var selectValue = getRadioButtonListSelectValue("rblSelectOne");
            if(selectValue == "")
            {
                alert("请选应用系统！");
            }
            else
            {
                window.returnValue = selectValue;
	            self.close();
	        }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="448" border="1">
        <tr>
            <td style="white-space: nowrap; width: 1%; text-align: center">
                <asp:Label ID="lblAppSystem" runat="server" CssClass="LabelTitle">应用系统名称</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="rblSelectOne" runat="server" CssClass="RadioButtonList" />
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap; width: 1%; text-align: center">
                <input id="btnOK" name="btnOK" type="button" value="确定" onclick="return btnOkClick()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
