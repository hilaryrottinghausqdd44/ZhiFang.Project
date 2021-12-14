<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectTableNameFromAppSystemNameForm.aspx.cs" Inherits="OA.DataInput.SelectTableNameFromAppSystemNameForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择应用系统下的表名称页面</title>

    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function btnOkClick()
        {
            //取选择的表
    	    var allNum = document.all.rblSelectOne.length
    	    var selectValue = "";
            for(var i=0;i<allNum;i++)
            {
                var radioID="rblSelectOne_"+i;
                var obj = document.getElementById(radioID);
                if(obj == null)
                {
                    continue;
                }
                if(obj.checked)
                {
			        selectValue = obj.value;
			        break;
                }      
            }
            if(selectValue == "")
            {
                alert("请选择表！");
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
                <asp:Label ID="lblTable" runat="server" CssClass="LabelTitle">表名称</asp:Label>
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
