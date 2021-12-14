<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesktopMain.aspx.cs" Inherits="OA.Main.DesktopMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Œﬁ±ÍÃ‚“≥</title>

    <script type="text/javascript">
    
      function pageLoad() {
      }
    
    </script>

</head>
    <FRAMESET ID="framesetDesktop" NAME="framesetDesktop" ROWS="50,*" SCROLLING="no" FRAMEBORDER="no"> 
        <FRAME ID="top" NAME="top" SRC="./DesktopHeader.aspx" SCROLLING="no"  FRAMEBORDER="0" > 
        <FRAMESET ID="framesetDesktopContent" NAME="framesetDesktopContent" COLS="200,*" FRAMEBORDER="no"> 
            <FRAME ID="left" NAME="left" SRC="./DesktopLeft.aspx" SCROLLING="auto"  FRAMEBORDER="0" > 
            <FRAME ID="content" NAME="content" SRC="DesktopContent.aspx" SCROLLING="auto"  FRAMEBORDER="0" > 
        </FRAMESET> 
    </FRAMESET> 
</html>



