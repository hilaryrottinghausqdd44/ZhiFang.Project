<%@ Page Language="c#" CodeBehind="defaultFPat.aspx.cs" AutoEventWireup="true" Inherits="OA.Main._defaultFPat" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationSettings.AppSettings["MyTitle"]%>
        -主页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <style type="text/css">
        hand
        {
            cursor: hand;
        }
    </style>
    <style type="text/css">
        <!
        -- .STYLE1
        {
            font-size: 12px;
        }
        body
        {
            background-image: url(images/bg-0003.jpg);
        }
        -- ></style>

    <script type="text/javascript" language="javascript">
        function JumpUrl(sUrl)//weblis
        {
            window.location.href='/weblis' + sUrl;
        }
	    
    </script>

</head>
<body bgcolor="#999999">
    <form id="Form1" method="post" runat="server">
    <table width="492" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 100px;
        border: #CCCCCC solid 1px;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Width="424px" Font-Size="Smaller" ForeColor="Red"
                    Visible="False" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
