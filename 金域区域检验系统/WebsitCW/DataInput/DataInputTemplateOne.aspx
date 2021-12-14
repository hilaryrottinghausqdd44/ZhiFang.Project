<%@ Page Language="C#" AutoEventWireup="true" Inherits="OA.DataInput.DataInputTemplateOne"
    CodeBehind="DataInputTemplateOne.aspx.cs" %>

<%@ Register Src="../UserControlLib/DataGroupUserControl.ascx" TagName="DataGroupUserControl"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControlLib/QueryFormWebControl.ascx" TagName="QueryFormWebControl"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControlLib/GridViewUserControl.ascx" TagName="GridViewUserControl"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>应用系统页面</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    </script>

</head>
<body class="ListBody" style="background-image: inherit">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    </div>
    <asp:UpdatePanel ID="updatePanelMain" runat="server">
        <ContentTemplate>
            <uc1:DataGroupUserControl ID="DataGroupUserControl1" runat="server" />
            <div style="z-index: 777">
                <uc2:QueryFormWebControl ID="QueryFormWebControl1" runat="server" />
            </div>
            <div style="z-index: -1">
                <asp:UpdatePanel ID="updatePanelChild" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc3:GridViewUserControl ID="GridViewUserControl1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="text-align: center">
        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
