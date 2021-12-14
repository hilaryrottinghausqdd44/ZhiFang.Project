<%@ Page Language="C#" ValidateRequest="false"  AutoEventWireup="true"  EnableEventValidation="false" 
 CodeBehind="TreeEditNews.aspx.cs" Inherits="OA.TreeItem.TreeUI.TreeEditNews" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>内容添加管理 </title>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div>
 <table cellspacing="1" id="contype"  cellpadding="0" width="95%"
        bgcolor="#efefef" border="1">
        <tr>
            <td>
                <iframe id="Iframe1" src="../../includes/eWebEditor/ewebeditor.htm?id=content1&style=standard500"
                    frameborder="0" width="100%" scrolling="no" height="400"></iframe>
                <asp:Button runat="server" ID="btntest" Text="确定" OnClick="btntest_Click" />               
                <input id="content1" type="hidden" runat="server" name="content1"   />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
