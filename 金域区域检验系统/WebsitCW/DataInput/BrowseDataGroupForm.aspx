<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseDataGroupForm.aspx.cs" Inherits="OA.DataInput.BrowseDataGroupForm" %>

<%@ Register src="../UserControlLib/DataGroupUserControl.ascx" tagname="DataGroupUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据分组页面</title>
    <script src="../Util/CommonJS.js" type="text/javascript"></script>

    <script type="text/jscript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:DataGroupUserControl ID="DataGroupUserControl1" runat="server" />
    </div>
    </form>
</body>
</html>
