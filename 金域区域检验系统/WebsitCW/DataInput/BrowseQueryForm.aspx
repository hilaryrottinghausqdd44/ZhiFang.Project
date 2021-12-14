<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseQueryForm.aspx.cs" Inherits="OA.DataInput.BrowseQueryForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControlLib/QueryFormWebControl.ascx" TagName="QueryFormWebControl" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查询页面</title>

    <script src="../Util/CommonJS.js" type="text/javascript"></script>
    <script src="../Includes/JS/JsModuleDist.js" type="text/javascript"></script>

    <script type="text/jscript">
    </script>

</head>
<body class="ListBody" style="background-image: inherit">
    <form id="form1" runat="server">

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    </div>
    <asp:UpdatePanel ID="updatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="z-index: 1">
                <uc2:QueryFormWebControl ID="QueryFormWebControl1" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    </form>
</body>
</html>
