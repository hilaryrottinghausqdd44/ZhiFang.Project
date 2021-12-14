<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="OA.Main.MainForm" %>

<%@ Register src="../UserControlLib/LogonHeader.ascx" tagname="LogonHeader" tagprefix="uc1" %>
<%@ Register src="../UserControlLib/LogonFooter.ascx" tagname="LogonFooter" tagprefix="uc2" %>
<%@ Register src="../UserControlLib/GridViewUserControl.ascx" tagname="GridViewUserControl" tagprefix="uc3" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>主操作页面</title>
</head>
    <FRAMESET  ID="framesetMainForm" NAME="framesetMainForm" ROWS="*,50" SCROLLING="no"  FRAMEBORDER ="0"> 
        <FRAME  NAME="content" SRC="./DesktopMain.aspx" SCROLLING="auto"  FRAMEBORDER="0" > 
        <FRAME  NAME="bottom" SRC="./DesktopFooter.aspx" SCROLLING="no"  FRAMEBORDER="0"> 
    </FRAMESET > 
</html>
    