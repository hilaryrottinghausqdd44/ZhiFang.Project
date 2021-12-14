<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailDataDisplayForm.aspx.cs" Inherits="OA.DataInput.DetailDataDisplayForm" %>

<%@ Register src="../UserControlLib/DetailDataInputUserControl.ascx" tagname="DetailDataInputUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>


    <script language="javascript" type="text/javascript" src="../Util/CommonJS.js"></script>
    <script language="javascript" type="text/javascript">
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
    <form id="form1" runat="server">
    <div>
        <uc1:DetailDataInputUserControl ID="DetailDataInputUserControl1" runat="server" />
    </div>
    </form>
</body>
</html>
