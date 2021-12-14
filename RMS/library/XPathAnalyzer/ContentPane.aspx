<%@ Page Language="c#" AutoEventWireup="True" Inherits="FTP.ContentPane"
    Trace="False" CodePage="936" Codebehind="ContentPane.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ContentPane</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="styles.css" type="text/css" rel="stylesheet">

    <script language="javascript">
			function ReturnFile(str)
			{
				//window.parent.returnValue=str;
				if(window.opener.document.all['txtXML']!=null)
					window.opener.document.all['txtXML'].value=str;
				else
					alert('没有找到一个叫 txtXML 的表单');
				window.close();
			}
			function confirmDeleteFile(filePath, fileName){				
				if(confirm("Really delete '" + fileName + "'?"))
   	 				document.location.href = filePath;   	 			   	 			
			}
			
			function confirmDeleteFolder(filePath, folderName){				
				if(confirm("Really delete '" + folderName + "' and all it's subfiles and subfolders?")){
					if(parent.frames.length > 0)
   	 					parent.MenuPane.location.href = "MenuPane.aspx";
   	 				document.location.href = filePath;
   	 			}   	 			
			}
			
			function newFile(path)
			{
				var fileName = window.prompt("Enter the name of the new file", "");			
				if(fileName != null)
					document.location.href = "contentPane.aspx?newFile=" + fileName + "&Path=" + path;
			}
			
			function newFolder(path)
			{
				var folderName = window.prompt("Enter the name of the new folder", "");			
				if(folderName != null)
					document.location.href = "contentPane.aspx?newFolder=" + folderName + "&Path=" + path;
			}
			
			function rename(filePath, fileName)
			{
				var newName = window.prompt("Enter the new name for '" + fileName + "'", fileName);			
				if(newName != null)
					document.location.href = filePath + "&NewName=" + newName;
			}
			
			var winOpts = 'resizeable=no,scrollbars=yes,left=125,top=175,width=295,height=96';
			function popUp(pPage) 
			{
				popUp = window.open(pPage,'popWin',winOpts);
			}   
			
    </script>

</head>
<body bgcolor="#e7e7ef" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="Form1" runat="server">
    <table cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td style="width: 520px">
                &nbsp;
                <asp:ImageButton ID="UpBtn" runat="server" ImageUrl="pics/icons/up.gif" ToolTip="Up one level (Alt-U)"
                    OnClick="UpBtn_Click"></asp:ImageButton>&nbsp;
                <asp:ImageButton ID="GoRoot" runat="server" ImageUrl="pics/icons/home.gif" ToolTip="Back to Root Directory (Alt-H)"
                    OnClick="GoRoot_Click"></asp:ImageButton>&nbsp;
                <asp:ImageButton ID="RefreshBtn" runat="server" ImageUrl="pics/icons/refresh.gif"
                    ToolTip="Refresh this page (Alt-R)" OnClick="Refresh_Click"></asp:ImageButton>&nbsp;
                <a href='<%= "javascript:newFolder(\"" + this.path + "\")" %>'>
                    <img alt="New Folder" src="pics/icons/newFolder.gif" border="0"></a>&nbsp; <a href='<%= "javascript:newFile(\"" + this.path + "\")" %>'>
                        <img alt="New File" src="pics/icons/newFile.gif" border="0"></a>&nbsp; <a href='<%= "javascript:popUp(\"upload.aspx?Path=" + this.path + "\")"%>'>
                            <img alt="Upload a file to this directory" src="pics/icons/upload.gif" border="0"></a>
                &nbsp; <a href='<%= "contentPane.aspx?Thumbnails=1&amp;Path=" + this.path %>'>
                    <img alt="View the images in this directory as thumbnails" src="pics/icons/thumbs.gif"
                        border="0"></a> &nbsp;
            </td>
            <tr>
                <td style="width: 520px">
                    <p align="left">
                        Directory:&nbsp;
                        <asp:TextBox ID="currentPathTxt" runat="server" OnTextChanged="pathBoxChange" CssClass="headerInputBox"
                            Width="215px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="GoButton" ImageUrl="pics/icons/go.gif" runat="server" OnClick="GoButton_Click">
                        </asp:ImageButton>&nbsp; Ext(.扩展名)
                        <asp:TextBox ID="fileExtFilter" runat="server" CssClass="headerInputBox" Width="60px"></asp:TextBox></p>
                </td>
            </tr>
    </table>
    <hr>
    <p>
        <asp:Label ID="MessageLbl" runat="server" ForeColor="#0000C0" Font-Bold="True"></asp:Label><asp:PlaceHolder
            ID="imagesHolder" runat="server"></asp:PlaceHolder>
        <asp:Panel ID="filesFoldersPanel" runat="server">
            <table cellspacing="0" cellpadding="3" border="0">
                <tr>
                    <th align="left" bgcolor="#000000">
                        <!-- icon -->
                        &nbsp;
                    </th>
                    <th align="left" bgcolor="#000000">
                        Name
                    </th>
                    <td align="left" bgcolor="#000000">
                        <asp:Label ID="pathHeading" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </td>
                    <th align="left" width="50" bgcolor="#000000">
                        <!-- edit -->
                        &nbsp;
                    </th>
                    <th align="left" width="16" bgcolor="#000000">
                        <!-- delete -->
                        &nbsp;
                    </th>
                    <th align="left" width="50" bgcolor="#000000">
                        <!-- rename -->
                        &nbsp;
                    </th>
                    <th align="left" width="75" bgcolor="#000000">
                        Size
                    </th>
                    <th align="left" bgcolor="#000000">
                        Last Modified
                    </th>
                </tr>
                <asp:PlaceHolder ID="FilesFolders" runat="server"></asp:PlaceHolder>
            </table>
        </asp:Panel>
    </p>
    <p>
    </p>
    <p>
        <font face="宋体"></font>&nbsp;</p>
    </form>
</body>
</html>
