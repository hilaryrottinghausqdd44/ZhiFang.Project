<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="OA.Total.test" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>测试</title>
    <script type="text/javascript">
        function showopen() 
        {
            window.showModalDialog('Progress.aspx', this, 'dialogHeight: 100px; dialogWidth: 350px; edge: Raised; center: Yes; help: No; resizable: No; status: No;scroll:No;');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" width="98%" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                <asp:Button runat="server" ID="btntest" Text="cs" onclick="btntest_Click" />
    
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
