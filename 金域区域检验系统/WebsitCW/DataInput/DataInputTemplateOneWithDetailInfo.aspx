<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataInputTemplateOneWithDetailInfo.aspx.cs" Inherits="OA.DataInput.DataInputTemplateOneWithDetailInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="../UserControlLib/DataGroupUserControl.ascx" TagName="DataGroupUserControl" TagPrefix="uc1" %>
<%@ Register Src="../UserControlLib/QueryFormWebControl.ascx" TagName="QueryFormWebControl" TagPrefix="uc2" %>
<%@ Register Src="../UserControlLib/GridViewUserControl.ascx" TagName="GridViewUserControl" TagPrefix="uc3" %>
<%@ Register src="../UserControlLib/DetailDataInputUserControl.ascx" tagname="DetailDataInputUserControl" tagprefix="uc4" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <div style="z-index: 2">
        <asp:UpdatePanel ID="updatePanelMain" runat="server">
            <ContentTemplate>
                <div style="z-index: 3">
                    <uc1:DataGroupUserControl ID="DataGroupUserControl1" runat="server" />
                </div>
                <div style="z-index: 3333">
                    <uc2:QueryFormWebControl ID="QueryFormWebControl1" runat="server" />
                </div>
                <div style="z-index: -1">
                    <asp:UpdatePanel ID="updatePanelChild" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc3:GridViewUserControl ID="GridViewUserControl1" runat="server" />
                            <asp:UpdatePanel ID="updatePanelDetail" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <uc4:DetailDataInputUserControl ID="DetailDataInputUserControl1" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="text-align: center">
        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
