<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftTree.aspx.cs" Inherits="TreeItem.TreeUI.LeftTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

  <script type="text/javascript">
    //得到名称为posy的cookie值
    function GetCookie(name)
    { 
        var arg = name + "="; 
        var alen = arg.length; 
        var clen = document.cookie.length; 
        var i = 0; 
        while(i < clen)
        { 
            var j = i + alen; 
            //判断是否是所指定的key
            if(document.cookie.substring(i,j) == arg) 
            return getCookieVal(j); 
            i = document.cookie.indexOf("   ",i)+1; 
            if(i == 0)
            break;    
        } 
        return null; 
    } 
    //得到key的值value 
    function getCookieVal(offset)
    { 
        var endstr = document.cookie.indexOf(";",offset); 
        if (endstr == -1) 
        endstr = document.cookie.length; 
        return unescape(document.cookie.substring(offset,endstr)); 
    } 
    //设置当前滑动坐标值到缓存
    function SetCookie(name,value)
     {        
       document.cookie = name + "=" + escape(value) 
     }
   </script>

</head>
<body onload="document.documentElement.scrollTop=GetCookie('posy')" onunload="SetCookie('posy',document.documentElement.scrollTop)">
    <form id="formhidden" runat="server" method="post">
         <input id="btn_click" type="hidden" runat="server"  name="btn_click" />
    <div> 
 <asp:Button ID="btnOrderby"  runat="server" Target = "mainFrame"  Text="节点排序"       
           onclick="btnOrderby_Click"  />
           
           <asp:Button ID="btnNei" runat="server" Target = "mainFrame" Text="内容管理"       
             onclick="btnNei_Click"   />
            <asp:Button ID="btntree" runat="server" Target = "mainFrame"  Text="节点管理" 
            onclick="btntree_Click"   />
          <asp:Button ID="btnllan" runat="server"  Target = "mainFrame" Text="浏览页面" 
            onclick="btnllan_Click"     />
        <br /> 
        
        <asp:TreeView ID="tvMenu" runat="server"  ImageSet="Custom"
             ShowLines="true" Width="100%" ExpandDepth="0" Target="middle">
            <ParentNodeStyle Font-Bold="False"  />
            <HoverNodeStyle Font-Underline="false" ForeColor="Purple" />
            <SelectedNodeStyle Font-Underline="true" HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
    </div>      
    </form>
</body>
</html>
