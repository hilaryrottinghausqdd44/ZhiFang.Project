<%@ Page Language="c#" AutoEventWireup="True" Inherits="FTP.upload" Codebehind="upload.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>upload</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="styles.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form enctype="multipart/form-data" runat="server">
    <p>
        <table class="borderTableLight" id="Table1" cellspacing="0" cellpadding="7" border="0">
            <tr>
                <td bgcolor="background">
                    <strong><font color="#ffffff">Upload a file</font></strong>
                </td>
            </tr>
            <tr>
                <td bgcolor="background">
                    &nbsp;&nbsp;
                    <input id="fileToUpload" style="width: 156px; height: 22px" type="file" size="6"
                        name="fileToUpload" runat="server">&nbsp;
                    <asp:Button ID="UploadBtn" runat="server" Text="Upload" Height="22px" Width="61px"
                        OnClick="UploadBtn_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </p>
    </form>
</body>
</html>
