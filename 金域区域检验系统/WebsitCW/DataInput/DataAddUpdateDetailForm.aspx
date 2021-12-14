<%@ Page Language="C#" AutoEventWireup="false" Codebehind="DataAddUpdateDetailForm.aspx.cs" Inherits="OA.DataInput.DataAddUpdateDetailForm"%>

<%@ Register src="../DataUserControlLib/OADetailDataInputUserControl.ascx" tagname="OADetailDataInputUserControl" tagprefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>详细信息</title>
    <base target="_selt" />

    <script language="javascript" type="text/javascript" src="../JavaScriptFile/CommonJS.js"></script>

    <script language="javascript" type="text/javascript">
        alert("onload");
		//绑定事件 onload="resetWindowSize()"
		if(window.attachEvent)
		{
			window.attachEvent("onload", iframeAutoFit);
		}
		else if(window.addEventListener)
		{
			window.addEventListener('load', iframeAutoFit, false);
		}
    </script>
</head>
<body onload="resetWindowSize()">
    <form id="form11" runat="server">
    <div>
                        <uc1:OADetailDataInputUserControl ID="OADetailDataInputUserControl1" runat="server" />
    </div>
    <table id="table11" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
