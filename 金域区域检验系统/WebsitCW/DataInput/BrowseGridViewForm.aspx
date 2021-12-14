<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseGridViewForm.aspx.cs" Inherits="OA.DataInput.BrowseGridViewForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="../UserControlLib/GridViewUserControl.ascx" TagName="GridViewUserControl" TagPrefix="uc3" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据列表页面</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />

    <script src="../Util/CommonJS.js" type="text/javascript"></script>

    <script type="text/jscript">
        // onunload="return returnSelectData()"
        function returnSelectData()
        {
            var obj = window.document.getElementById("GridViewUserControl1");
            if(obj != null)
            {
                for(var i=0; i<obj.childNodes.length;i++)
                    alert(obj.childNodes.item(i).innerHTML);
            }
            else
            {
                alert("NULL");
            }
        }
    </script>

</head>
<body class="ListBody" style="background-image: inherit">
    <form id="form1" runat="server">

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    </div>
    <asp:UpdatePanel ID="updatePanelMain" runat="server">
        <ContentTemplate>
            <div style="z-index: 1">
                <uc3:GridViewUserControl ID="GridViewUserControl1" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
