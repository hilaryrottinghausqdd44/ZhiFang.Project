<%@ Page validateRequest="false" enableEventValidation="false" Language="c#" AutoEventWireup="True"
    Inherits="FTP.edit" CodePage="936" Codebehind="edit.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>edit</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="styles.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			function ConfirmDelete(){
				if(confirm("Really delete? '<%= Request.QueryString["File"].ToString() %>'"))
	   	 			document.location="ContentPane.aspx?Delete=1&Type=File&Name=<%=Request.QueryString["File"].ToString() %>&Path=<%=Request.QueryString["Path"].ToString() %>";
			}			
    </script>

</head>
<body>
    <form id="edit" method="post" runat="server">
    <p align="center">
        <asp:ImageButton ID="homeLink" runat="server" ToolTip="Back" ImageUrl="pics/icons/back.gif"
            OnClick="homeLink_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Header1" runat="server" Width="85%" Height="20px" ForeColor="Navy"
            BackColor="#C0C0FF"></asp:Label><br>
        <br>
        <asp:Button ID="SaveBtn" runat="server" Text="Save" OnClick="SaveBtn_Click"></asp:Button>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input onclick="javascript:ConfirmDelete()" type="button" value="Delete">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SaveAs" runat="server" Width="59px" Text="Save As:" OnClick="SaveAs_Click">
        </asp:Button>
        <asp:TextBox ID="SaveAsTxt" runat="server" Width="74px"></asp:TextBox><br>
        <br>
        <asp:TextBox ID="CodeText" runat="server" Width="95%" Height="500px" TextMode="MultiLine"
            Wrap="False"></asp:TextBox>
        <br>
    </p>
    </form>
</body>
</html>
